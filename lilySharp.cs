using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace lilySharp
{


	/// <summary>
	/// The main Parent window of the Lily client
	/// </summary>
	/// <remarks>
	/// An SLCP-based .NET GUI client for Lily
	/// </remarks>
	public class LilyParent : System.Windows.Forms.Form, ILeafCmd
	{
		public delegate void updateUserDelegate(NotifyEvent notify);
		public delegate void createDiscWindowDelegate(LilyItem disc);
		public delegate void createPMDelegate(LilyItem user);
		public delegate void eventDelegate(string line);
		public delegate void whereDelegate(String[] discs);
		public delegate void EmptyInvokeDelegate();

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem fileItem;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem exitItem;

		private TcpClient tcpClient;
		private NetworkStream stream;
		private StreamWriter outStream;
		private StreamReader inStream;
		private System.Windows.Forms.MenuItem connectItem;
		private bool connected = false;
		private Thread listen;
		private LilyWindow system;
		private User me;
		private Hashtable handles = new Hashtable();
		private ArrayList commandQueue = new ArrayList();
		private LoginDialog loginDlg;
		private System.Windows.Forms.MenuItem disconnectItem;
		private System.Windows.Forms.MenuItem windowItem;
		private System.Windows.Forms.MenuItem cascadeItem;
		private System.Windows.Forms.MenuItem horizontalItem;
		private System.Windows.Forms.MenuItem verticalItem;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem discList;
		private System.Windows.Forms.MenuItem joinItem;
		private System.Windows.Forms.MenuItem createItem;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;

		public event updateUserDelegate UpdateUser;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LilyParent()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{

			if( disposing )
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.fileItem = new System.Windows.Forms.MenuItem();
			this.connectItem = new System.Windows.Forms.MenuItem();
			this.disconnectItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.exitItem = new System.Windows.Forms.MenuItem();
			this.discList = new System.Windows.Forms.MenuItem();
			this.joinItem = new System.Windows.Forms.MenuItem();
			this.createItem = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.windowItem = new System.Windows.Forms.MenuItem();
			this.cascadeItem = new System.Windows.Forms.MenuItem();
			this.horizontalItem = new System.Windows.Forms.MenuItem();
			this.verticalItem = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.fileItem,
																					  this.discList,
																					  this.windowItem});
			// 
			// fileItem
			// 
			this.fileItem.Index = 0;
			this.fileItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.connectItem,
																					 this.disconnectItem,
																					 this.menuItem3,
																					 this.menuItem1,
																					 this.menuItem2,
																					 this.exitItem});
			this.fileItem.Text = "&File";
			// 
			// connectItem
			// 
			this.connectItem.Index = 0;
			this.connectItem.Text = "&Connect";
			this.connectItem.Click += new System.EventHandler(this.connectItem_Click);
			// 
			// disconnectItem
			// 
			this.disconnectItem.Index = 1;
			this.disconnectItem.Text = "&Disconnect";
			this.disconnectItem.Visible = false;
			this.disconnectItem.Click += new System.EventHandler(this.disconnectItem_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 3;
			this.menuItem1.Text = "Console";
			this.menuItem1.Click += new System.EventHandler(this.consoleItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.Text = "-";
			// 
			// exitItem
			// 
			this.exitItem.Index = 5;
			this.exitItem.Text = "E&xit";
			this.exitItem.Click += new System.EventHandler(this.exitItem_Click);
			// 
			// discList
			// 
			this.discList.Index = 1;
			this.discList.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.joinItem,
																					 this.createItem,
																					 this.menuItem5});
			this.discList.Text = "Discussions";
			// 
			// joinItem
			// 
			this.joinItem.Enabled = false;
			this.joinItem.Index = 0;
			this.joinItem.Text = "&Join";
			this.joinItem.Click += new System.EventHandler(this.joinItem_Click);
			// 
			// createItem
			// 
			this.createItem.Enabled = false;
			this.createItem.Index = 1;
			this.createItem.Text = "&Create";
			this.createItem.Click += new System.EventHandler(this.createItem_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "-";
			// 
			// windowItem
			// 
			this.windowItem.Index = 2;
			this.windowItem.MdiList = true;
			this.windowItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.cascadeItem,
																					   this.horizontalItem,
																					   this.verticalItem});
			this.windowItem.Text = "&Window";
			// 
			// cascadeItem
			// 
			this.cascadeItem.Index = 0;
			this.cascadeItem.Text = "&Cascade";
			this.cascadeItem.Click += new System.EventHandler(this.cascadeItem_Click);
			// 
			// horizontalItem
			// 
			this.horizontalItem.Index = 1;
			this.horizontalItem.Text = "Tile &Horizontal";
			this.horizontalItem.Click += new System.EventHandler(this.horizontalItem_Click);
			// 
			// verticalItem
			// 
			this.verticalItem.Index = 2;
			this.verticalItem.Text = "Tile &Vertical";
			this.verticalItem.Click += new System.EventHandler(this.verticalItem_Click);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 543);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(752, 22);
			this.statusBar.TabIndex = 1;
			this.statusBar.Text = "Not Connected";
			// 
			// LilyParent
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(752, 565);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.statusBar});
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "LilyParent";
			this.Text = "LilySharp v0.41 Alpha";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.LilyParent_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new LilyParent());

		}


		#region Properties
		/// <summary>
		/// Allows access to the input stream
		/// </summary>
		/// <value>Allows access to the input stream</value>
		public StreamReader In
		{
			get { return inStream; }
		}
		
		/// <value>Allows access to the output stream</value>
		public StreamWriter Out
		{
			get { return outStream; }
		}

	    /// <summary>
	    /// Allows access to the client Lily database
	    /// </summary>
		/// <value>Allows access to the client Lily database</value>
		public Hashtable Handles
		{
			get { return handles;}
		}

		/// <summary>
		/// Allows access to the user's entry in the Lily database
		/// </summary>
		/// <value>Allows access to the user's entry in the Lily database</value>
		public User Me
		{
			get { return me; }
		}

		/// <summary>
		/// Allows access to the discussion list menu so discussions can add themselves to it
		/// </summary>
		/// <value>Allows access to the discussion list menu so discussions can add themselves to it</value>
		public MenuItem DiscMenu
		{
			get{ return this.discList;}
		}

		#endregion

		/// <summary>
		/// Detaches from the lily server, and exits the application
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Exit event argument</param>
		private void exitItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// Initiates the connection when the user hits the connect button.
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Arguments for the event</param>
		private void connectItem_Click(object sender, System.EventArgs e)
		{
			connect();
		}

		/// <summary>
		/// Initiates the connection to the server
		/// </summary>
		private void connect()
		{
			/*
			 * Establish a connection to the lily server
			 * TODO: Make the server/port user configurable
			 */
			try
			{
				/*
				ServerDlg selectDlg = new ServerDlg();
				if(selectDlg.ShowDialog() == DialogResult.Cancel)
					return;
				*/
				loginDlg = new LoginDialog();
				if(loginDlg.ShowDialog() == DialogResult.Cancel)
					return;

				statusBar.Text = "Connecting...";
				tcpClient = new TcpClient(loginDlg.Server, loginDlg.Port);
				stream    = tcpClient.GetStream();
				outStream = new StreamWriter(stream);
				outStream.AutoFlush = true;
				inStream  = new StreamReader(stream);
				connected = true;
			}
			catch
			{
				MessageBox.Show("Unable to establish connection to server", "Cannot connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
				statusBar.Text = "Cannot Connect";
				return;
			}

			/*
			 * Update the GUI
			 */
			connectItem.Visible = false;
			disconnectItem.Visible = true;
			if(system == null) system = new LilyWindow(this);
			system.MdiParent = this;
			system.Show();


			/*
			 * Login
			 */
			waitForLogin();
			outStream.WriteLine("#$# options +slcp +leaf-cmd +prompt +prompt2 +waterlogin");

			/*
			 * Start the listening thread
			 */
			listen = new Thread(new ThreadStart(ListenThread));
			listen.Start();
		}

		/// <summary>
		/// Reads in data from the server until the login prompt is encountered
		/// </summary>
		private void waitForLogin()
		{
			string str = new string(' ', 0);

			/*
			 * Wait for the login prompt
			 */
			while( !(str.EndsWith("login:")))
			{
				str += (char)inStream.Read();
			}
		}

		/// <summary>
		/// Detaches from the server, closes the connection, and returns the GUI to a not connected state
		/// </summary>
		private void disconnect()
		{
			if(connected)
			{
				try
				{
					outStream.WriteLine("/set message_echo no");
					outStream.WriteLine("/detach");
				}
				catch(IOException)
				{}
				outStream.Close();
				inStream.Close();
				stream.Close();
				tcpClient.Close();
				connected = false;
				statusBar.Text = "Not Connected";
				disconnectItem.Visible = false;
				connectItem.Visible = true;
				joinItem.Enabled = false;
				createItem.Enabled = false;
				handles.Clear();
				this.discList.MenuItems.Clear();
			}
		}

		/// <summary>
		/// Handles all the waterlogin events
		/// </summary>
		/// <param name="line">The waterlogin event from the server</param>
		private void handleWaterlogin(string line)
		{
			/*
			 * Handle server requests
			 */
			if(Parse(line, "CHALLENGE") != "")
			{
				string challenge = Parse(line, "CHALLENGE");
				if(challenge == "auth")
				{
					if(loginDlg == null)
					{
						LoginDlg login = new LoginDlg();
						if(login.ShowDialog() == DialogResult.OK)
						{
							outStream.WriteLine("%waterlogin RESPONSE=auth AUTHTYPE=plaintext LOGIN=" + login.UserName.Length + "=" + login.UserName + " PASSWORD=" + login.Password.Length + "=" + login.Password);
						}
						else
						{
							disconnect();
						}
					}
					else
					{
						outStream.WriteLine("%waterlogin RESPONSE=auth AUTHTYPE=plaintext LOGIN=" + loginDlg.UserName.Length + "=" + loginDlg.UserName + " PASSWORD=" + loginDlg.Password.Length + "=" + loginDlg.Password);
					}
				}
				else if(challenge == "blurb")
				{
					BlurbDlg blurb = new BlurbDlg();
					if(blurb.ShowDialog() == DialogResult.OK)
					{
						outStream.WriteLine("%waterlogin RESPONSE=blurb VALUE=" + blurb.Blurb.Length + "=" + blurb.Blurb);
					}
					else
					{
						outStream.WriteLine("%waterlogin RESPONSE=blurb VALUE=0=");
					}
					
				}
				else if(challenge == "name")
				{
					SelectName namePopup = new SelectName( Parse(line, "NAMELIST"));
					if(namePopup.ShowDialog() == DialogResult.OK)
					{
						string name = namePopup.UserName;
						outStream.WriteLine("%waterlogin RESPONSE=name VALUE=" + name.Length + "=" + name);
					}
					else
					{
						this.disconnect();
					}
				}
				else
				{
					system.Post(line);
				}
			}
				/*
				 * Handle server responses
				 */
			else if( Parse(line, "RESULT") != "")
			{
				string result = Parse(line, "RESULT");
				string status = Parse(line, "STATUS");
				if(result == "auth")
				{
					if(status == "success")
					{
						system.Post("Login successful");
					}
					else
					{
						loginDlg.Dispose();
						loginDlg = null;
						MessageBox.Show("Unable to log in.\nPlease check your username and password", "Unable to Login");
					}
				}
				else if(result == "blurb")
				{
					if(status == "success")
					{
						system.Post("Blurb accepted");
					}
					else
					{
						MessageBox.Show(Parse(line, "TEXT"), "Blurb error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				else if(result == "name")
				{
					if(status == "success")
					{
						system.Post("Name accepted");
					}
					else
					{
						MessageBox.Show("A program error has occured in name selection.\nPlease inform the author", "You should not see this", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			else if( Parse(line, "ATTRIBUTE") != "")
			{
				system.Post( Parse(line, "ATTRIBUTE") + " = " + Parse(line, "VALUE"));
			}
			else if( line.ToUpper() == "%WATERLOGIN END")
			{
				statusBar.Text = "Connected";
				outStream.WriteLine("/set message_echo yes");
				this.PostMessage(new LeafMessage("/where " + me.Name, "where", this));
				joinItem.Enabled = true;
				createItem.Enabled = true;
			}
			else
			{
				system.Post("Waterlogin: " + line);
			}
		}

		/// <summary>
		/// Handles all the data events
		/// </summary>
		/// <param name="line">The data event from the server</param>
		private void handleDataItem(string line)
		{
			string source = Parse(line, "SOURCE");
						
			if(source == "#0")
			{
				if(Parse(line, "NAME") == "whoami")
				{
					me = (User)handles[Parse(line, "VALUE")];
					/*
					statusBar.Text = "Connected";
					outStream.WriteLine("/set message_echo yes");
					Where += new whereDelegate(this.getUserDiscs);
					outStream.WriteLine("/where " + me.Name);
					*/
				}
				else
				{
					system.Post(line);
				}
			}
			else
			{
				string name = Parse(line, "NAME");
				if(name == "members")
				{
			
					((DiscussionWindow)((Discussion)handles[source]).Window).AddMembers(Parse(line, "VALUE").Split(new char[] {','}));
				}
			}
		}
	
		/// <summary>
		/// Handles notify events
		/// </summary>
		/// <param name="line">The notify event from the server</param>
		/// <remarks>
		/// Update user is not called uniformly, but per each event, because some events require it be called before the status change, and some after.
		/// </remarks>
		private void handleNotifyItem(string line)
		{
			NotifyEvent notify = new NotifyEvent(line, handles);

			if(notify.Source == null)
			{
				system.Post("*** Null source detected.  Possible database corruption.");
				system.Post("*** One event ( " + notify.Event + " from " + Parse(line, "SOURCE") + " ) lost!");
				system.Post("Resynching database...");
				outStream.WriteLine("#$# slcp-sync");
				return;
			}
			try
			{

				switch(notify.Event)
				{
					case "private":
						if(notify.Source == me)
						{
							if(notify.Recipients[0].Window == null)
								this.Invoke(new createPMDelegate(createPM), new object[] {notify.Recipients[0]});

							notify.Recipients[0].Window.Post(notify);
							return;
						}
						else
						{
							if(notify.Source.Window == null)
								this.Invoke(new createPMDelegate(createPM), new object[]{notify.Source});

							notify.Source.Window.Post(notify);
						}
						break;
					case "public":
						// We need to be able to handle multiple recpients
						foreach(LilyItem recipient in notify.Recipients)
						{
							if( recipient.GetType() == typeof(User)  && recipient != me)
							{
								continue;
							}

							if( recipient.Window == null)
							{
								if(recipient.GetType() == typeof(User))
									this.Invoke(new createPMDelegate(createPM), new object[]{recipient});
								else
									continue;
							}

							recipient.Window.Post(notify);
						}
						break;
					case "emote":
						foreach(LilyItem recipient in notify.Recipients)
						{
							if(recipient.Window == null)
								return;
							else
								((DiscussionWindow)recipient.Window).PostEmote(notify);
						}
						break;
					case "blurb":
						if(UpdateUser != null) UpdateUser(notify);
						((User)notify.Source).Blurb = notify.Value;
						break;
					case "detach":
					case "quit":
					case "disconnect":
						((User)notify.Source).State = User.States.Detached;
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "attach":
					case "unidle":
					case "here":
					case "connect":
						((User)notify.Source).State = User.States.Here;
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "away":
						((User)notify.Source).State = User.States.Away;
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "rename":
						if(UpdateUser != null) UpdateUser(notify);
						notify.Source.Name = notify.Value;
						break;
					case "join":
						if(notify.Source == me)
							foreach(Discussion disc in notify.Recipients)
								this.createDiscWindow(disc.Handle);


						if( notify.Recipients[0].Window != null)
							((DiscussionWindow)notify.Recipients[0].Window).AddMembers(new String[] {notify.Source.Handle});

						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "destroy":
						if(UpdateUser != null) UpdateUser(notify);
						handles.Remove(notify.Recipients[0].Handle);
						break;
					case "retitle":
						if(UpdateUser != null) UpdateUser(notify);
						((Discussion)notify.Recipients[0]).Title = notify.Value;
						break;
					case "ignore":
					case "unignore":
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "appoint":
					case "unapoint":
					case "permit":
					case "deptermit":
					case "Review":
					case "sysmsg":
					case "sysalert":
					case "pa":
					case "game":
					case "consult":
						system.Post(notify.Event + " Event: " + line);
						break;
					default:
						system.Post("*** Unhandled Event: " + notify.Event + " ***");
						system.Post(line);
						break;
				}
			}
			catch(Exception e)
			{
				/*
				 * It's time for monster debuggin output!  Wheeeeeeeeeee
				 */
				system.Post("*** Exception ***");
				system.Post("Event: " + notify.Event);
				system.Post("Error: " + e.Message);
				if(notify.Source != null) system.Post("Source: " + notify.Source.Name);
				if(notify.Recipients != null)
					foreach(LilyItem recip in notify.Recipients)
						system.Post("Recipient: " + recip.Name);
				if(notify.Value != "") system.Post("Value: " + notify.Value);

			}
		}
	
		/// <summary>
		/// Handles the %DISC SLCP message
		/// </summary>
		/// <param name="line">The %disc message from the server</param>
		private void handleDiscItem(string line)
		{
			Discussion newDisc = new Discussion(line);
				/*
				* If we get a %DISC of an existing discussion (someone was permitted)
				*   we should link the window back to the discussion so we don't have 
				*   two windows for the same disc
				*/
			if(handles[newDisc.Handle] != null)
				newDisc.Window = ((Discussion)handles[newDisc.Handle]).Window;

			handles[newDisc.Handle] = newDisc;
		}

		/// <summary>
		/// Handles the %USER SLCP messages
		/// </summary>
		/// <param name="line">The %User message from the server</param>
		private void handleUserItem(string line)
		{
			User newUser = new User(line);
			if(handles[newUser.Handle] != null)
			{
				((User)handles[newUser.Handle]).Blurb   = newUser.Blurb;
				((User)handles[newUser.Handle]).Finger  = newUser.Finger;
				((User)handles[newUser.Handle]).Info    = newUser.Info;
				((User)handles[newUser.Handle]).Memo    = newUser.Memo;
				((User)handles[newUser.Handle]).Name    = newUser.Name;
				((User)handles[newUser.Handle]).Pronoun = newUser.Pronoun;
				((User)handles[newUser.Handle]).State   = newUser.State;
			}
			else
			{
				handles[newUser.Handle] = newUser;
			}
		}

		/// <summary>
		/// Handles leaf-cmd messages
		/// </summary>
		/// <param name="line">The leaf-cmd message from the server</param>
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

			switch(line.Substring(0, line.IndexOf(' ') ).ToUpper() )
			{
				case "%BEGIN":
					command = line.Substring( line.IndexOf(']') + 2);
					foreach(LeafMessage msg in commandQueue)
					{
						if(command == msg.Command)
						{
							msg.ID = ID;
							break;
						}
					}
					break;
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
					//system.Post("[" + ID + "] " + command);
					system.Post(command);
					break;
				case "%END":
					command = "-none-";
					foreach(LeafMessage msg in commandQueue)
					{
						if(msg.ID == ID)
						{
							msg.End();
							commandQueue.Remove(msg);
							break;
						}
					}
					break;
				default:
					command = "-error-";
					break;
			}
			//system.Post("*** ID: " + ID + "\n*** Command: " + command);
		}

		/// <summary>
		/// Accepts messages from objects implementing the ILeafCmd interface
		/// </summary>
		/// <param name="msg">The message</param>
		/// <remarks>
		/// Adds the message to the command queue, and sends the command to the server
		/// </remarks>
		public void PostMessage(LeafMessage msg)
		{
			commandQueue.Add(msg);
			outStream.WriteLine(msg.Command);
		}

		/// <summary>
		/// Reads in messages from the server, and sends them to the appropriate method.
		/// </summary>
		/// <remarks>
		/// Runs in a seperate thread.  All event handling methdos are invoked in the main thread to ensure thread safety.
		/// </remarks>
		public void ListenThread()
		{
			string line = null, command;
			int wordEnd;
			while(connected)
			{
				try
				{
					line = inStream.ReadLine();
					/*
					 * The connection has been broken off by the remote host
					 */
					if(line == null)
					{
						MessageBox.Show("Connection reset by host", "Disconnected");
						disconnect();
						break;
					}
					
					if(line.Trim() == "") continue;

					wordEnd = line.IndexOf(' ');
					if(wordEnd == -1) wordEnd = line.Length;

					command = line.Substring(0, wordEnd);
					switch(command)
					{
						case "%WATERLOGIN":
							Invoke(new eventDelegate(handleWaterlogin), new object[]{line});
							break;
						case "%DISC":
							Invoke(new eventDelegate(handleDiscItem), new object[]{line});
							break;
						case "%USER":
							Invoke(new eventDelegate(handleUserItem), new object[]{line});
							break;
						case "%DATA":
							Invoke(new eventDelegate(handleDataItem), new object[] {line});
							break;
						case "%NOTIFY":
							Invoke(new eventDelegate(handleNotifyItem), new object[]{line});
							break;
						case "%begin":
						case "%command":
						case "%end":
							Invoke(new eventDelegate(handleCommandItem), new Object[]{line});
							break;
						case "%options":
							break;
						case "***":
							Invoke(new eventDelegate(system.Post), new object[]{line});
							break;
						default:
							this.Invoke(new eventDelegate(system.Post), new object[] {"*** UNHANDLED IDENTIFIER: " + line + " ***"});
							break;
					}


					/*
					else if(line.StartsWith("*** Redirecting connection to new port **"))
					{
						MessageBox.Show("Connection has been redirected", "Connection lost", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						disconnect();
					}
					else if(line.StartsWith("*** Redirecting old connection to this port ***"))
					{
						outStream.WriteLine("#$# slcp-sync");
					}
					else if(line.StartsWith("(message sent to"))
					{
					}
					*/

				}
				catch(IOException)
				{
					// User terminated the connection
					break;
				}
				catch(Exception e)
				{
					if(connected)
					{
						MessageBox.Show("Err: " + e.Message + "\nLine: " + line + "\nTrace: " + e.StackTrace, "Error");
						
					}
				}

			}
		}

		/// <summary>
		/// Disconnects from the lily server nicely before exiting
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Allows the canceling of the event</param>
		private void LilyParent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = false;
			disconnect();
		}
	
		/// <summary>
		/// Detaches from the lily server
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void disconnectItem_Click(object sender, System.EventArgs e)
		{
			if(connected)
				disconnect();
		}

		/// <summary>
		/// Retrieves the value of the specified token in the SLCP or waterlogin message.
		/// </summary>
		/// <param name="str">The message to search</param>
		/// <param name="token">The token to get the value of</param>
		/// <returns>The value of the token, or an empty string if the token is not found</returns>
		/// <remarks>
		/// This is the heart of the application. It steps through the message's tokens recursively, until it has reached the end of the message, or the token has been found
		/// </remarks>
		public static string Parse(string str, string token)
		{
			string tok, val, type;
			int equals     = str.IndexOf('=');
			int space      = str.IndexOf(' ');
			int nextEquals = str.IndexOf('=', equals + 1);

			// No tokens left
			if(str.Length == 0) return "";
			
			
			/*
			 * Determine next token type
			 */
			if(space == -1) space = str.Length;
			
			if(equals == -1 )
				type = "noValue";
			else if(space < equals)
				type = "noValue";
			else if(nextEquals == -1 || space < nextEquals)
				type = "fixedValue";
			else
				type = "variableValue";
			




			//if(space == -1) space = str.Length;

			if(type == "noValue")
			{
				//MessageBox.Show("noValue\nString:" + str);
				tok = str.Substring(0, space);
				
				if(tok.ToUpper() == token)
					return "";
				else
				{
					// Need to make sure we don't run off the end of the string
					if(str.Length < space + 1)
						return "";
					else
						return Parse(str.Substring(space + 1), token);
				}
			}
			else if(type == "fixedValue")
			{
				//MessageBox.Show("fixedValue\nString:" + str);
				tok = str.Substring(0, equals);
				val = str.Substring(equals + 1, space - equals - 1);

				if(tok.ToUpper() == token)
					return val;
				else
				{
					if(str.Length < space + 1)
						return "";
					else
						return Parse(str.Substring(space + 1), token);
				}

			}
			else if(type == "variableValue")
			{
				//MessageBox.Show("variableValue\nString:" + str);
				int valLength;

				try
				{
					valLength = int.Parse(str.Substring(equals + 1, nextEquals - equals - 1));
				}
				catch(FormatException)
				{
					MessageBox.Show("A number was not found where one was expected");
					return "";
				}

				tok = str.Substring(0, equals);
				val = str.Substring(nextEquals + 1, valLength);

				if(tok.ToUpper() == token)
					return val;
				else
				{
					if(str.Length < nextEquals + valLength + 2)
						return "";
					else
						return Parse(str.Substring(nextEquals + valLength + 2), token);
				}
			}
			else
			{
				MessageBox.Show("Unrecognizeable token encountered");
				return "";
			}

		}

		/// <summary>
		/// Creates the window for a discussion, and requests the user list of the discussion
		/// </summary>
		/// <param name="handle">The object ID of the discussion to create the window for</param>
		private void createDiscWindow(string handle)
		{
			Discussion disc = ((Discussion)handles[handle]);
			if(disc.Window != null) return;
			disc.Window = new DiscussionWindow( (Discussion)disc, this);
			disc.Window.MdiParent = this;

			outStream.WriteLine("#$# what " + disc.Handle);
		}

		/// <summary>
		/// Creates a Private Message Window
		/// </summary>
		/// <param name="user">The lily User to create the PM window for</param>
		public void createPM(LilyItem user)
		{
			user.Window = new PrivateMsg((User)user, this);
			user.Window.MdiParent = this;
			user.Window.Show();
		}

		/// <summary>
		/// Cascades the visible windows
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void cascadeItem_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.Cascade);
		}

		/// <summary>
		/// Tiles the visible windows horizontally
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void horizontalItem_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		/// <summary>
		/// Tiles the visible windows vertically
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void verticalItem_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}
		/// <summary>
		/// Gets the Object ID of the User or Disucssion
		/// </summary>
		/// <param name="name">The name of the User or Discussion to get the Object ID of</param>
		/// <returns>The Object ID of the User or Discussion</returns>
		public string getObjectId(string name)
		{
			foreach(DictionaryEntry item in handles)
			{
				try
				{
					if(((LilyItem)item.Value).Name.ToUpper() == name.ToUpper())
						return (String)item.Key;
				}
				catch(NullReferenceException e)
				{
					MessageBox.Show("Message: " + e.Message + "\nSource: " + e.Source, "NullReferenceException in getObjectID");
				}
			}

			return null;
		}

		/// <summary>
		/// Catches the close event of the parent, and allows all child windows to close
		/// </summary>
		/// <param name="msg">The Windows Message passed to the application</param>
		/// <remarks>
		/// This is needed for .NET version 1.0.3  In .NET version 1.1, this can be accomplished in the application's closing event
		/// </remarks>
		protected override void WndProc(ref Message msg)
		{
			switch(msg.Msg)
			{
				case 0x0010:     //WM_CLOSE message
				//	if(system != null) system.AllowClose = true;
					foreach(LilyWindow window in this.MdiChildren)
						window.AllowClose = true;
					break;
			}
			base.WndProc(ref msg);
		}

		/// <summary>
		/// Brings up the console if it exists, or creates and displays the consol if it doesn't exist
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void consoleItem_Click(object sender, System.EventArgs e)
		{
			if(system == null) system = new LilyWindow(this);
			system.Show();
		}

		/// <summary>
		/// Brings up the join discussion dialog box, and sends the server the join event
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void joinItem_Click(object sender, System.EventArgs e)
		{
			JoinDiscDlg joinDlg = new JoinDiscDlg(this);
			if(joinDlg.ShowDialog() == DialogResult.OK)
			{
		        createDiscWindow(joinDlg.selectedDisc.Handle);
				outStream.WriteLine("/join " + joinDlg.selectedDisc);
			}
		}

		/// <summary>
		/// Shows the create discussion dialog
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void createItem_Click(object sender, System.EventArgs e)
		{
			CreateDlg create = new CreateDlg(this);
			create.Show();			
		}
	
		/// <summary>
		/// Processes the responses to LeafMessages.  Needed for the ILeafCmd interface
		/// </summary>
		/// <param name="msg">The message to respond to</param>
		public void ProcessResponse(LeafMessage msg)
		{
			system.Post("*** PROCESSING: " + msg.Response);
			switch (msg.Tag)
			{
				case "where":
					if(msg.Response.StartsWith("(could find no user to match to"))
					{
						MessageBox.Show("Critical Error: Current user is unknown!  Possible communication error.", "Unrecoverable error");
						disconnect();
						return;
					}

					int startIndex = msg.Response.IndexOf(' ');
					startIndex += "are a member of ".Length;
					foreach(String id in msg.Response.Substring(startIndex).Split(','))
					{
						createDiscWindow(getObjectId(id.Trim()));
					}
					break;
				default:
					break;
			}
		}
	}
}
