using System;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Arguments for socket events
	/// </summary>
	public class SockEventArgs : EventArgs
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public SockEventArgs()
		{
			Line = "";
		}

		/// <summary>
		/// Consturctor
		/// </summary>
		/// <param name="str">Line sent fromt he server triggering this event</param>
		public SockEventArgs(string str)
		{
			Line = str;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="str">Line sent from the server triggering this event</param>
		/// <param name="att">A parsed version of the line received fromt he server</param>
		public SockEventArgs(string str, Hashtable att)
		{
			Line = str;
			Attributes = att;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="str">Line sent from the server triggering this event</param>
		/// <param name="note">The notify object representing the line from the server</param>
		/// <remarks>
		/// This is only used for %notify events sent from the server.  Since this is the most
		///   common event, it warrents a dedicated constructor
		/// </remarks>
		public SockEventArgs(string str, NotifyEvent note)
		{
			Line = str;
			Notify = note;
		}

		public string Line;
		public Hashtable Attributes;
		public NotifyEvent Notify;
	}

	/// <summary>
	/// An exception class for the socket
	/// </summary>
	/// <remarks>
	/// This is used primarily to differentiate between a user-terminated connection attempt (cancel button clicked)
	///   and an abnormally aborted connection attempt (server not responding, global thermonuclear war, etc.)
	/// </remarks>
	public class SockException : Exception
	{}

	public delegate void SockEventHandler(object source, SockEventArgs e);

	/// <summary>
	/// A singleton class that handles all server communication
	/// </summary>
	/// <remarks>
	/// Inherits from control to get the Invoke() method so I can make the threading transparent
	///   to the other controls.
	/// </remarks>
	public sealed class Sock : Control
	{

		public event SockEventHandler LineRecieved;
		public event SockEventHandler NotifyRecieved;
		public event SockEventHandler UserStateChanged;
		public event SockEventHandler MessageRecieved;
		public event SockEventHandler UserRecieved;
		public event SockEventHandler DiscRecieved;
		public event SockEventHandler DataRecieved;
		public event SockEventHandler OtherRecieved;
		public event SockEventHandler NewInfo;
		public event SockEventHandler ConnectComplete;
		public event SockEventHandler Disconnected;


		private static TcpClient client;
		private StreamWriter outStream;
		private StreamReader inStream;
		private bool connected = false;
		private string userName, password, blurb;
		private delegate void emptyDelegate();

		/// <summary>
		/// Allows access to the connection status
		/// </summary>
		/// <value>True if currently connected, false otherwise</value>
		public bool Active
		{
			get{ return connected;}
		}

		// This would more naturally suit a hashtable, but we won't have the ID number
		//   of the command until we hear back from the server, so there wouldn't be
		//   any way of adding the command to the queue initially.  Since the queue will
		//   almost never, if ever, have very many items, the performance hit in cycling
		//   through all the entries in the queue is minimal.
		private static ArrayList commandQueue = new ArrayList();

		public static readonly Sock Instance = new Sock();

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		/// Private so multiple instances cannot be created 
		/// </remarks>
		private Sock()
		{
			// Need a handle to use Invoke();
			this.CreateHandle();
			connected = false;
		}

		/// <summary>
		/// Negotiate the connection to the server
		/// </summary>
		public void Connect()
		{
			/*
			 * Establish a connection to the lily server
			 */
			try
			{
				// Get the login information
				LoginDialog loginDlg = new LoginDialog();
				if(loginDlg.ShowDialog() == DialogResult.Cancel)
				{
					throw new SockException(); // Reset the GUI
				}
				Cursor.Current = Cursors.WaitCursor;
				userName = loginDlg.UserName;
				password = loginDlg.Password;
				blurb = loginDlg.Blurb;
				

				// Setup the connection
				client = new TcpClient(loginDlg.Server, loginDlg.Port);
				outStream = new StreamWriter(client.GetStream());
				outStream.AutoFlush = true;
				inStream  = new StreamReader(client.GetStream());
				connected = true;
			}
			catch(SockException e)
			{
				Cursor.Current = Cursors.Default;
				throw e;
			}
			catch(Exception e)
			{
				Cursor.Current = Cursors.Default;
				MessageBox.Show("Unable to establish connection to server\n" + e.Message, "Cannot connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw e;
			}


			/*
			 * Login
			 */
			waitForLogin();
			Write("#$# options +slcp +leaf-cmd +prompt +prompt2 +waterlogin");

			/*
			 * Start the listening thread
			 */
			Thread listen = new Thread(new ThreadStart(ListenThread));
			listen.Start();

			/*
			 * TODO: Setup ping to ensure connection status
			 */
		}

		/// <summary>
		/// Terminate the connection to the server.  Try to disconnect nicely if possible
		/// </summary>
		public void Disconnect()
		{
			if(connected)
			{
				try
				{
					// Can't use our Write method, because if we did, multiple failures of Write
					//  would cause multiple calls to disconnect: infinite recurrsion
					outStream.WriteLine("/set message_echo no");
					outStream.WriteLine("/detach");
				}
				catch(IOException)
				{
					//If there is an error, it's ok because we are disconnecting anyway
				}

				// Close streams
				outStream.Close();
				inStream.Close();
				client.Close();
				connected = false;
			}
			
			if(Disconnected != null) Disconnected(this, new SockEventArgs());
			if(NewInfo != null) NewInfo(this, new SockEventArgs("*** Disconnected ***"));
		}

		/// <summary>
		/// Sends the given message to the server, and queues it up for a response
		/// </summary>
		/// <param name="msg">The message to send to the server</param>
		/// <remarks>
		/// This is the main method classes will be using to communicate with the server
		/// </remarks>
		public void PostMessage(LeafMessage msg)
		{
			commandQueue.Add(msg);  // Add the message to the pending response queue
			Write(msg.Command);     // Send the message to the server
		}

		/// <summary>
		/// Sends a line of text to the server.
		/// </summary>
		/// <param name="line">The text to send to the server</param>
		/// <remarks>
		/// This allows us to catch all socket exceptions in one place, rather than having to pepper
		///   our code with try-catch statements everywhere
		/// </remarks>
		public void Write(string line)
		{
			if(!connected) return;  // Don't send the message if we're not connected
			try
			{
				outStream.WriteLine(line);
			}
			catch(IOException e)
			{
				MessageBox.Show("Error sending to server: " + e.Message + "\nDisconnected", "Unable to write to stream");
				Disconnect();
			}
		}

		/// <summary>
		/// Reads in data from the server until the login prompt is received.
		/// </summary>
		/// <remarks>
		/// Since there is no terminating character sent after "login:" is sent from the server, we have to
		///   read in the data character by character until the prompt is read in.
		/// TODO: Allow this to be interrupted, so there can't be an infinite loop here
		/// </remarks>
		private void waitForLogin()
		{
			string str = "";

			/*
			 * Wait for the login prompt
			 */
			while( !(str.EndsWith("login:")))
			{
				try
				{
					str += (char)inStream.Read();
				}
				catch(Exception e)
				{
					MessageBox.Show(e.Message, "Read error during login", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Disconnect();
				}
			}
		}

		/// <summary>
		/// The heart of the socket class.  Listens for incoming data from the server, and reacts
		///   on that data accordingly
		/// </summary>
		/// <remarks>
		/// Most methods are invoked from here to ensure they run in the proper thread
		/// </remarks>
		private void ListenThread()
		{
			string command, line = null;
			int wordEnd;
			
			while(connected)
			{

				/*
				 * Get the next line from the server
				 */
				try
				{
					line = inStream.ReadLine();
				}
				catch(IOException)
				{
					// User terminated the connection
					Invoke(new emptyDelegate(Disconnect));
					break;
				}
				catch
				{
					MessageBox.Show("Error reading from server.\nConnection terminated.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Invoke(new emptyDelegate(Disconnect));
					break;
				}

				// Don't need to do anything for blank lines
				if(line.Trim() == "") continue;

				if(LineRecieved != null) Invoke( new SockEventHandler(h_LineRecieved), new object[] {this, new SockEventArgs(line)});

				// Get what type of message was recieved
				wordEnd = line.IndexOf(' ');
				if(wordEnd == -1) wordEnd = line.Length;
				command = line.Substring(0, wordEnd);

				switch(command)
				{
					case "%begin":
					case "%command":
					case "%end":
						handleCommandItem(line);
						break;
					case "%WATERLOGIN":
						handleWaterLogin(Util.Parse(line));
						break;
					case "%DISC":
						if(DiscRecieved != null) Invoke( new SockEventHandler(h_DiscRecieved), new object[] {this, new SockEventArgs(line, Util.Parse(line))});
						break;
					case "%USER":
						if(UserRecieved != null) Invoke( new SockEventHandler(h_UserRecieved), new object[] {this, new SockEventArgs(line, Util.Parse(line))});
						break;
					case "%DATA":
						if(DataRecieved != null) Invoke( new SockEventHandler(h_DataRecieved), new object[] {this, new SockEventArgs(line, Util.Parse(line))});
						break;
					case "%NOTIFY":
						if(NotifyRecieved != null) Invoke( new SockEventHandler(handleNotify), new object[] {this, new SockEventArgs(line, Util.Parse(line))});
						break;
					default:
						if(OtherRecieved != null) Invoke( new SockEventHandler(h_OtherRecieved), new object[] {this, new SockEventArgs(line, Util.Parse(line))});
						break;
				}

			}
		}

		/// <summary>
		/// Handles leaf-cmd messages from the server
		/// </summary>
		/// <param name="line">Line sent by the server</param>
		/// <remarks>
		/// This is where the server responds to commands sent from other classes.
		/// </remarks>
		private void handleCommandItem(string line)
		{

			int ID = 0;
			string command;
			/*
			* Get the command ID
			*/
			try
			{
				ID = int.Parse(line.Substring( line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') -1));
			}
			catch(FormatException e)
			{
				MessageBox.Show("Err: " + e.Message + " is not a number", "Parse error");
				return;
			}

			/*
			* Act on the command
			*/
			switch(line.Substring(0, line.IndexOf(' ') ).ToUpper() )
			{
					// Start of command: Get the ID, and store the information in the appropriate message
				case "%BEGIN":
					command = line.Substring( line.IndexOf(']') + 2);
					foreach(LeafMessage msg in commandQueue)
					{
						if(command == msg.Command)
						{
							msg.ID = ID;
							return;
						}
					}
					break;

					// Heart of the command: Get the server response and save it for processiong
				case "%COMMAND":
					command = line.Substring( line.IndexOf(']') + 2);
					foreach(LeafMessage msg in commandQueue)
					{
						if(msg.ID == ID)
						{
							msg.Response = command;
							return;
						}
					}
					break;

					// End of the command: Send the response to the calling object, and remove the msg from the queue
				case "%END":
					command = "-none-";
					foreach(LeafMessage msg in commandQueue)
					{
						if(msg.ID == ID)
						{
							try
							{
								this.Invoke(new emptyDelegate(msg.End));
							}
							catch(Exception e)
							{
								MessageBox.Show("Exception: " + e.Message);
							}
							commandQueue.Remove(msg);
							return;
						}
					}

					break;
				default:
					command = "-error-";
					MessageBox.Show("Unhandled command identifier: " + line.Substring(0, line.IndexOf(' ') ));
					break;
			}
		}

		/// <summary>
		/// Handles the logging into the server
		/// </summary>
		/// <param name="waterlogin">A parsed version of the line received from the server</param>
		private void handleWaterLogin(Hashtable waterlogin)
		{
			/*
			 * Handle server requests
			 */
			if(waterlogin["CHALLENGE"] != null)
			{
				string challenge = waterlogin["CHALLENGE"].ToString();
				switch(challenge)
				{
					case "auth":
						Write("%waterlogin RESPONSE=auth AUTHTYPE=plaintext LOGIN=" + userName.Length + "=" + userName + " PASSWORD=" + password.Length + "=" + password);
						break;
					case "blurb":
						Write("%waterlogin RESPONSE=blurb VALUE=" + blurb.Length + "=" + blurb);
						break;
					case "name":
						SelectName namePopup = new SelectName( waterlogin["NAMELIST"] as string);
						if(namePopup.ShowDialog() == DialogResult.OK)
						{
							userName = namePopup.UserName;
							Write("%waterlogin RESPONSE=name VALUE=" + userName.Length + "=" + userName);
						}
						else
						{
							Disconnect();
						}
						break;
				}
			} // end challenge

			/*
			 * Handles the response to the login requests
			 */
			else if(waterlogin["RESULT"] != null)
			{
				string result = waterlogin["RESULT"].ToString();
				string status = waterlogin["STATUS"].ToString();
				switch(result)
				{
					case "auth":
						if(status == "success")
						{
							Invoke(new SockEventHandler(h_NewInfo), new object[] {this, new SockEventArgs("Login successful")});

							// We don't need these anymore, and it's not a good idea to have the password floating around in memory forever
							userName = null;
							password = null;
						}
						else
						{
							MessageBox.Show("Unable to log in.\nPlease check your username and password.", "Unable to login");
							UserPassDlg login = new UserPassDlg();
							if(login.ShowDialog() == DialogResult.OK)
							{
								userName = login.UserName;
								password = login.Password;
							}
							else
							{
								Disconnect();
							}
						}
						break;
					case "blurb":
						if(status == "success")
						{
							Invoke(new SockEventHandler(h_NewInfo), new object[] {this, new SockEventArgs("Blurb accepted")});
						}
						else
						{
							MessageBox.Show(waterlogin["TEXT"].ToString(), "Blurb Error");
							BlurbDlg bDlg = new BlurbDlg();
							if(bDlg.ShowDialog() == DialogResult.OK)
								blurb = bDlg.Blurb;
							else
								blurb = "";
						}
						break;
					case "name":
						if(status != "success")
						{
							MessageBox.Show("A program error has occured in name selection.\nSelected name is not valid.", "Name selection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							Disconnect();
						}
						break;
				}
			} // End Result

			else if(waterlogin["ATTRIBUTE"] != null)
			{
				if(NewInfo != null) Invoke( new SockEventHandler(h_NewInfo), new object[] {this, new SockEventArgs(waterlogin["ATTRIBUTE"].ToString() + " = " + waterlogin["VALUE"].ToString())});
			}
			else if(waterlogin["END"] != null)
			{
				if(NewInfo != null) Invoke(new SockEventHandler(h_NewInfo), new object[] {this, new SockEventArgs("Connection completed")});
				if(ConnectComplete != null) Invoke( new SockEventHandler(ConnectComplete), new object[] {this, new SockEventArgs()});
			}
			else if(waterlogin["NOTICE"] != null || waterlogin["START"] != null)
			{
				// No need to do anything with these
			}
		}
	
		/// <summary>
		/// Handles %notify events
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void handleNotify(object sender, SockEventArgs e)
		{	
			// Parse the notify
			NotifyEvent notify = new NotifyEvent(e.Attributes);

			/*
			 * If I can't find the sender of the event in my database, the database is probably corrupt
			 *   resynch the database then
			 */
			if(notify.Source == null)
			{
				if(NewInfo != null)
				{
					NewInfo(this, new SockEventArgs("*** Null source detected.  Possible database corruption.\n" +
						"*** One event ( " + notify.Event + " from " + e.Attributes["SOURCE"] + " ) lost!\n" +
						"Resynching user..."));
				}

				// Don't need to synch the entire database, only the user we don't have on file
				Write("#$# slcp-sync " + notify.Source);
				return;
			}

			switch (notify.Event)
			{
					// Handle messages
				case "public":
				case "private":
				case "emote":
					if(MessageRecieved != null) MessageRecieved(this, new SockEventArgs(e.Line, notify));
					break;
					// Handle status changes that require notification before the change (ie. Need access to the old value)
				case "blurb":
				case "rename":
				case "destroy":
				case "retitle":
				case "drename":
					if(notify.Notify && NotifyRecieved != null) NotifyRecieved(this, new SockEventArgs(e.Line, notify));
					if(UserStateChanged != null) UserStateChanged(this, new SockEventArgs(e.Line, notify));
					break;

					// Handle events that require the database be updated before the user is notified
					//   ie. The user's state has to be properly modified before the user list of the discussion is updated
					//         so the proper colors are used
				case "detach":
				case "disconnect":
				case "attach":
				case "here":
				case "connect":
				case "away":
				case "join":
				case "quit":
				case "create":
					if(UserStateChanged != null) UserStateChanged(this, new SockEventArgs(e.Line, notify));
					if(notify.Notify && NotifyRecieved != null) NotifyRecieved(this, new SockEventArgs(e.Line, notify));
					break;

					// Handle events that don't update the database, but only provide new information to the user
				case "unidle":
				case "ignore":
				case "unignore":
				case "info":
				case "sysmsg":
				case "sysalert":
					if(notify.Notify && NotifyRecieved != null) NotifyRecieved(this, new SockEventArgs(e.Line, notify));
					break;
				case "appoint":
				case "unapoint":
				case "permit":
				case "deptermit":
				case "Review":
				case "pa":
				case "game":
				case "consult":
					// TODO: Respond to these events
					break;
				default:
					break;
			}
		}

		#region Event triggering method hack
		/*
		 * Since events can't be invoked directly, these methods are invoked witht he sole purpose
		 *  of triggering the appropriate event.  These are events that require no processing by
		 *  the socket.
		 */
		private void h_DiscRecieved(object sender, SockEventArgs e)   { DiscRecieved(sender, e);   }
		private void h_UserRecieved(object sender, SockEventArgs e)   { UserRecieved(sender, e);   }
		private void h_DataRecieved(object sender, SockEventArgs e)   { DataRecieved(sender, e);   }
		private void h_OtherRecieved(object sender, SockEventArgs e)  { OtherRecieved(sender, e);  }
		private void h_NewInfo(object sender, SockEventArgs e)        { NewInfo(sender, e);        }
		private void h_LineRecieved(object sender, SockEventArgs e)   { LineRecieved(sender, e);   }
		private void h_Disconnected(object sender, SockEventArgs e)   { Disconnected(sender, e);   }
		private void h_ConnectComplete(object sender, SockEventArgs e){ ConnectComplete(sender, e);}
		#endregion
	}
}
