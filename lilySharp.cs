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
		public delegate void eventDelegate(string line);
		private delegate void dispatchDelegate(Hashtable table);

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
		private LilyWindow console;
		private DiagConsole diag;
		private IUser me;
		private HashDb database = new HashDb();
		private ArrayList commandQueue = new ArrayList();
		private LoginDialog loginDlg;
		private JoindDiscWnd joinedDiscList;
		private System.Windows.Forms.StatusBarPanel connectPanel;
		private System.Windows.Forms.StatusBarPanel msgPanel;
		private System.Windows.Forms.MenuItem detachItem;
		private System.Windows.Forms.MenuItem windowItem;
		private System.Windows.Forms.MenuItem cascadeItem;
		private System.Windows.Forms.MenuItem horizontalItem;
		private System.Windows.Forms.MenuItem verticalItem;
		private System.Windows.Forms.MenuItem discList;
		private System.Windows.Forms.MenuItem joinItem;
		private System.Windows.Forms.MenuItem createItem;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton discBtn;
		private System.Windows.Forms.ImageList toolbarLst;
		private System.Windows.Forms.ToolBarButton connectBtn;
		private System.Windows.Forms.ToolBarButton disconnectBtn;
		private System.Windows.Forms.MenuItem helpItem;
		private System.Windows.Forms.MenuItem diagConsoleItem;
		private System.ComponentModel.IContainer components;

		public event updateUserDelegate UpdateUser;

		/// <summary>
		/// Contructerator
		/// </summary>
		public LilyParent()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


			/*
			 * Create status bar
			 */
			// Status panel
			connectPanel = new StatusBarPanel();
			connectPanel.Text = "Not Connected";

			// Message notification panel
			msgPanel = new StatusBarPanel();
			msgPanel.Text = "No new messages";
			msgPanel.Alignment = HorizontalAlignment.Center;
			msgPanel.BorderStyle = StatusBarPanelBorderStyle.Sunken;
			msgPanel.AutoSize = StatusBarPanelAutoSize.Contents;

			StatusBarPanel spacer = new StatusBarPanel();
			spacer.AutoSize = StatusBarPanelAutoSize.Spring;
			
			statusBar.Panels.Add(connectPanel);
			statusBar.Panels.Add(spacer);
			statusBar.Panels.Add(msgPanel);

			// Window of joined discussions
			joinedDiscList = new JoindDiscWnd(this, msgPanel);

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LilyParent));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.fileItem = new System.Windows.Forms.MenuItem();
			this.connectItem = new System.Windows.Forms.MenuItem();
			this.detachItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.exitItem = new System.Windows.Forms.MenuItem();
			this.discList = new System.Windows.Forms.MenuItem();
			this.joinItem = new System.Windows.Forms.MenuItem();
			this.createItem = new System.Windows.Forms.MenuItem();
			this.windowItem = new System.Windows.Forms.MenuItem();
			this.cascadeItem = new System.Windows.Forms.MenuItem();
			this.horizontalItem = new System.Windows.Forms.MenuItem();
			this.verticalItem = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.connectBtn = new System.Windows.Forms.ToolBarButton();
			this.disconnectBtn = new System.Windows.Forms.ToolBarButton();
			this.discBtn = new System.Windows.Forms.ToolBarButton();
			this.toolbarLst = new System.Windows.Forms.ImageList(this.components);
			this.helpItem = new System.Windows.Forms.MenuItem();
			this.diagConsoleItem = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.fileItem,
																					  this.discList,
																					  this.windowItem,
																					  this.helpItem});
			// 
			// fileItem
			// 
			this.fileItem.Index = 0;
			this.fileItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.connectItem,
																					 this.detachItem,
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
			// detachItem
			// 
			this.detachItem.Index = 1;
			this.detachItem.Text = "&Detach";
			this.detachItem.Visible = false;
			this.detachItem.Click += new System.EventHandler(this.detachItem_Click);
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
																					 this.createItem});
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
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(752, 22);
			this.statusBar.TabIndex = 1;
			this.statusBar.Text = "Not Connected";
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.connectBtn,
																						this.disconnectBtn,
																						this.discBtn});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.toolbarLst;
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(752, 25);
			this.toolBar1.TabIndex = 3;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// connectBtn
			// 
			this.connectBtn.ImageIndex = 1;
			this.connectBtn.ToolTipText = "Connect";
			// 
			// disconnectBtn
			// 
			this.disconnectBtn.ImageIndex = 2;
			this.disconnectBtn.ToolTipText = "Disconnect";
			this.disconnectBtn.Visible = false;
			// 
			// discBtn
			// 
			this.discBtn.ImageIndex = 0;
			this.discBtn.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.discBtn.ToolTipText = "Show Joined IDiscussions";
			// 
			// toolbarLst
			// 
			this.toolbarLst.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.toolbarLst.ImageSize = new System.Drawing.Size(16, 16);
			this.toolbarLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolbarLst.ImageStream")));
			this.toolbarLst.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// helpItem
			// 
			this.helpItem.Index = 3;
			this.helpItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.diagConsoleItem});
			this.helpItem.Text = "&Help";
			// 
			// diagConsoleItem
			// 
			this.diagConsoleItem.Index = 0;
			this.diagConsoleItem.Text = "Diag Console";
			this.diagConsoleItem.Click += new System.EventHandler(this.diagConsoleItem_Click);
			// 
			// LilyParent
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(752, 565);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.toolBar1,
																		  this.statusBar});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "LilyParent";
			this.Text = "LilySharp v0.53.1 Alpha";
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
		public HashDb Database
		{
			get { return database;}
		}

		/// <summary>
		/// Allows access to the user's entry in the Lily database
		/// </summary>
		/// <value>Allows access to the user's entry in the Lily database</value>
		public IUser Me
		{
			get { return me; }
		}

		public JoindDiscWnd JoinedDiscList
		{
			get{ return joinedDiscList;}
		}

		public ToolBarButton DiscBtn
		{
			get{ return this.discBtn;}
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
			 */
			try
			{
				loginDlg = new LoginDialog();
				if(loginDlg.ShowDialog() == DialogResult.Cancel)
					return;

				connectPanel.Text = "Connecting...";
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
				connectPanel.Text = "Cannot Connect";
				return;
			}

			/*
			 * Update the GUI
			 */
			connectItem.Visible = false;
			detachItem.Visible = true;
			connectBtn.Visible = false;
			disconnectBtn.Visible = true;
			if(console == null) 
			{
				console = new LilyWindow(this);
				console.MdiParent = this;
				console.Show();
			}
			if(diag == null)
			{
				diag = new DiagConsole();
				diag.MdiParent = this;
			}
		

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
				{
					//If there is an error, it's ok because we are disconnecting anyway
				}

				// Close streams
				outStream.Close();
				inStream.Close();
				stream.Close();
				tcpClient.Close();

				//update GUI
				connected = false;
				connectPanel.Text = "Not Connected";
				detachItem.Visible = false;
				connectItem.Visible = true;
				connectBtn.Visible = true;
				disconnectBtn.Visible = false;
				joinItem.Enabled = false;
				createItem.Enabled = false;

				// Cleanup client db
				database.Clear();
				joinedDiscList.Clear();
			}
		}

		/// <summary>
		/// Handles all the waterlogin events
		/// </summary>
		/// <param name="line">The waterlogin event from the server</param>
		private void handleWaterlogin(Hashtable waterlogin)
		{
			/*
			 * Handle server requests
			 */
			if(waterlogin["CHALLENGE"] != null)
			{
				if(waterlogin["CHALLENGE"].ToString() == "auth")
				{
					if(loginDlg.LoginValid)
					{
						outStream.WriteLine("%waterlogin RESPONSE=auth AUTHTYPE=plaintext LOGIN=" + loginDlg.UserName.Length + "=" + loginDlg.UserName + " PASSWORD=" + loginDlg.Password.Length + "=" + loginDlg.Password);
					}
					else
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
				}
				else if(waterlogin["CHALLENGE"].ToString() == "blurb")
				{
					if(loginDlg.BlurbValid)
					{
						outStream.WriteLine("%waterlogin RESPONSE=blurb VALUE=" + loginDlg.Blurb.Length + "=" + loginDlg.Blurb);
					}
					else
					{
						BlurbDlg blurbDlg = new BlurbDlg();

						blurbDlg.ShowDialog();
						outStream.WriteLine("%waterlogin RESPONSE=blurb VALUE=" + blurbDlg.Blurb.Length + "=" + blurbDlg.Blurb);
					}
				}
				else if(waterlogin["CHALLENGE"].ToString() == "name")
				{
					SelectName namePopup = new SelectName( waterlogin["NAMELIST"] as string);
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
					foreach(DictionaryEntry entry in waterlogin)
						console.Post(entry.Key + " = " + entry.Value);
				}
			}
				/*
				 * Handle server responses
				 */
			else if(waterlogin["RESULT"] != null)
			{
				if(waterlogin["RESULT"].ToString() == "auth")
				{
					if(waterlogin["STATUS"].ToString() == "success")
					{
						console.Post("Login successful");
					}
					else
					{
						loginDlg.LoginValid = false;
						MessageBox.Show("Unable to log in.\nPlease check your username and password", "Unable to Login");
					}
				}
				else if(waterlogin["RESULT"].ToString() == "blurb")
				{
					if(waterlogin["STATUS"].ToString() == "success")
					{
						console.Post("Blurb accepted");
					}
					else
					{
						loginDlg.BlurbValid = false;
						MessageBox.Show(waterlogin["TEXT"].ToString(), "Blurb error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				else if(waterlogin["RESULT"].ToString() == "name")
				{
					if(waterlogin["STATUS"].ToString() == "success")
					{
						console.Post("Name accepted");
					}
					else
					{
						MessageBox.Show("A program error has occured in name selection.\nPlease inform the author", "You should not see this", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			else if(waterlogin["ATTRIBUTE"] != null)
			{
				console.Post(waterlogin["ATTRIBUTE"] + " = " + waterlogin["VALUE"]);
			}
			else if(waterlogin["NOTICE"] != null)
			{
				// We don't need to do anything with notices
			}
			else if(waterlogin["START"] != null)
			{
				// Don't need to do anything here either.
			}
			else if(waterlogin["END"] != null)
			{
				connectPanel.Text = "Connected";
				outStream.WriteLine("/set message_echo yes");
				outStream.WriteLine("#$# client LilySharp 0.60");
				this.PostMessage(new LeafMessage("/where " + me.Name, "where", this));
				joinItem.Enabled = true;
				createItem.Enabled = true;
			}
			else
			{
				console.Post("*** waterlogin ***");
				foreach(DictionaryEntry entry in waterlogin)
					console.Post(entry.Key + " = " + entry.Value);
				console.Post("******************");
			}
		}

		/// <summary>
		/// Handles all the data events
		/// </summary>
		/// <param name="line">The data event from the server</param>
		private void handleDataItem(Hashtable dataAttributes)
		{
			if((string)dataAttributes["SOURCE"] == "#0")
			{
				if((string)dataAttributes["NAME"] == "whoami")
				{
					me = (IUser)database[dataAttributes["VALUE"]];
				}
				else
				{
					console.Post("*** data ***");
					foreach(DictionaryEntry entry in dataAttributes)
						console.Post(entry.Key + " = " + entry.Value);
					console.Post("******************");
				}
			}
			else
			{

				if(dataAttributes["NAME"] as string == "members")
				{
			
					((DiscussionWindow)database[dataAttributes["SOURCE"]].Window).AddMembers( ((string)dataAttributes["VALUE"]).Split( ((string)dataAttributes["LIST"]).ToCharArray()));
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
		private void handleNotifyItem(Hashtable notifyAttributes)
		{
			NotifyEvent notify = new NotifyEvent(notifyAttributes, database);

			if(notify.Source == null)
			{
				console.Post("*** Null source detected.  Possible database corruption.");
				console.Post("*** One event ( " + notify.Event + " from " + notifyAttributes["SOURCE"] + " ) lost!");
				console.Post("Resynching database...");
				outStream.WriteLine("#$# slcp-sync");
				return;
			}
			try
			{

				switch(notify.Event)
				{
					case "private":
					case "public":
						// We need to be able to handle multiple recpients
						foreach(ILilyObject recipient in notify.Recipients)
						{
							/*
							 * Private message
							 */
							if(recipient.GetType() == typeof(IUser))
							{
								// Someone sent me a message
								if(recipient == me)
								{
									if(notify.Source.Window == null)
										createPM(notify.Source);

									notify.Source.Window.Post(notify);
								}
								// I sent someone else a message
								else if(notify.Source == me)
								{
									if(recipient.Window == null)
										createPM(recipient);

									recipient.Window.Post(notify);
								}
								// This PM has nothing to do with me
								else
								{
									continue;
								}
							}

							/*
							 * Discussion
							 */
							else
							{
								// I'm not a member of this discussion
								if(recipient.Window == null)
									continue;

								// Display new message notification
								if(!recipient.Window.Visible)
									joinedDiscList.AddMsg( recipient as IDiscussion);

								// Post the message
								recipient.Window.Post(notify);
							}

						}
						break;
					case "emote":
						foreach(ILilyObject recipient in notify.Recipients)
						{
							if(recipient.Window == null)
								return;
							else
							{
								((DiscussionWindow)recipient.Window).PostEmote(notify);
								if(!recipient.Window.Visible)
									joinedDiscList.AddMsg( (IDiscussion)recipient);
							}
						}
						break;
					case "blurb":
						if(UpdateUser != null) UpdateUser(notify);
						((IUser)notify.Source).Blurb = notify.Value;
						break;
					case "detach":
					case "disconnect":
						((IUser)notify.Source).State = States.Detached;
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "attach":
					case "unidle":
					case "here":
					case "connect":
						((IUser)notify.Source).State = States.Here;
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "away":
						((IUser)notify.Source).State = States.Away;
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "rename":
						if(UpdateUser != null) UpdateUser(notify);
						notify.Source.Name = notify.Value;
						break;
					case "join":
						if(notify.Source == me)
							foreach(IDiscussion disc in notify.Recipients)
							{
								this.createDiscWindow(disc.Handle);
								joinedDiscList.Add(disc);
							}


						if( notify.Recipients[0].Window != null)
							((DiscussionWindow)notify.Recipients[0].Window).AddMembers(new String[] {notify.Source.Handle});

						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "quit":
						if(notify.Source == me)
						{
							joinedDiscList.ClearMsg(notify.Recipients[0] as IDiscussion);
							joinedDiscList.Remove(notify.Recipients[0] as IDiscussion);
						}
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "destroy":
						if(UpdateUser != null) UpdateUser(notify);
						database.Remove(notify.Recipients[0].Handle);
						break;
					case "retitle":
						if(UpdateUser != null) UpdateUser(notify);
						((IDiscussion)notify.Recipients[0]).Title = notify.Value;

						if(notify.Recipients[0].GetType() == typeof(IDiscussion) && notify.Recipients[0].Window != null)
						{
							joinedDiscList.Remove(notify.Recipients[0] as IDiscussion);
							joinedDiscList.Add(notify.Recipients[0] as IDiscussion);
						}
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
						console.Post(notify.Event + " Event: ");
						foreach(DictionaryEntry entry in notifyAttributes)
							console.Post(entry.Key + " = " + entry.Value);
						break;
					default:
						console.Post("*** Unhandled Event: " + notify.Event + " ***");
						foreach(DictionaryEntry entry in notifyAttributes)
							console.Post(entry.Key + " = " + entry.Value);
						break;
				}
			}
			catch(Exception e)
			{
				/*
				 * It's time for monster debuggin output!  Wheeeeeeeeeee
				 */
				console.Post("*** Exception ***");
				console.Post("Event: " + notify.Event);
				console.Post("Error: " + e.Message);
				if(notify.Source != null) console.Post("Source: " + notify.Source.Name);
				if(notify.Recipients != null)
					foreach(ILilyObject recip in notify.Recipients)
						console.Post("Recipient: " + recip.Name);
				if(notify.Value != "") console.Post("Value: " + notify.Value);

			}
		}
	
		/// <summary>
		/// Handles the %DISC SLCP message
		/// </summary>
		/// <param name="line">The %disc message from the server</param>
		private void handleDiscItem(Hashtable discAttributes)
		{
			IDiscussion newDisc = new Discussion(discAttributes);
				/*
				* If we get a %DISC of an existing discussion (someone was permitted)
				*   we should link the window back to the discussion so we don't have 
				*   two windows for the same disc
				*/
			if(database[newDisc.Handle] != null)
				newDisc.Window = database[newDisc.Handle].Window;

			database[newDisc.Handle] = newDisc;
		}

		/// <summary>
		/// Handles the %USER SLCP messages
		/// </summary>
		/// <param name="line">The %IUser message from the server</param>
		private void handleUserItem(Hashtable userAttributes)
		{
			IUser newUser = new User(userAttributes);
			if(database[newUser.Handle] != null)
			{
				((IUser)database[newUser.Handle]).Blurb   = newUser.Blurb;
				((IUser)database[newUser.Handle]).Finger  = newUser.Finger;
				((IUser)database[newUser.Handle]).Info    = newUser.Info;
				((IUser)database[newUser.Handle]).Memo    = newUser.Memo;
				((IUser)database[newUser.Handle]).Name    = newUser.Name;
				((IUser)database[newUser.Handle]).Pronoun = newUser.Pronoun;
				((IUser)database[newUser.Handle]).State   = newUser.State;
			}
			else
			{
				database[newUser.Handle] = newUser;
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
							break;
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
					console.Post(command);
					break;

				// End of the command: Send the response to the calling ILeafCmd, and remove the msg from the queue
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
			//console.Post("*** ID: " + ID + "\n*** Command: " + command);
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

					Invoke(new eventDelegate(diag.Post), new object[] { line });

					wordEnd = line.IndexOf(' ');
					if(wordEnd == -1) wordEnd = line.Length;

					command = line.Substring(0, wordEnd);
					switch(command)
					{
						case "%WATERLOGIN":
							Invoke(new dispatchDelegate(handleWaterlogin), new object[]{parse(line)});
							break;
						case "%DISC":
							Invoke(new dispatchDelegate(handleDiscItem), new object[]{parse(line)});
							break;
						case "%USER":
							Invoke(new dispatchDelegate(handleUserItem), new object[]{parse(line)});
							break;
						case "%DATA":
							Invoke(new dispatchDelegate(handleDataItem), new object[] {parse(line)});
							break;
						case "%NOTIFY":
							Invoke(new dispatchDelegate(handleNotifyItem), new object[]{parse(line)});
							break;
						case "%begin":
						case "%command":
						case "%end":
							Invoke(new eventDelegate(handleCommandItem), new Object[]{line});
							break;
						case "%options":
							break;
						case "***":
							Invoke(new eventDelegate(console.Post), new object[]{line});
							break;
						default:
							this.Invoke(new eventDelegate(console.Post), new object[] {"*** UNHANDLED IDENTIFIER: " + line + " ***"});
							break;
					}
				}
				catch(IOException)
				{
					// IUser terminated the connection
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
		private void detachItem_Click(object sender, System.EventArgs e)
		{
			if(connected)
				disconnect();
		}

		/// <summary>
		/// Parses a line of SLCP text into token-value pairs
		/// </summary>
		/// <param name="str">SLCP line to parse</param>
		/// <returns>A Hashtable with the tokens as keys, and the values as values</returns>
		private Hashtable parse(string str)
		{
			Hashtable table = new Hashtable();
			
			str += " ";  //Pad the end of the line for the last token
			int delimIndex;
			int nextDelimIndex;
			string token;

			/*
			 * Itterate through all the tokens, and populate the hash
			 */
			while(str.Length > 0)
			{
				delimIndex = str.IndexOfAny(new char[]{' ','='});
				token = str.Substring(0, delimIndex);

				//The token is valueless
				if(str[delimIndex] == ' ')
				{
					table[token] = string.Empty;
					str = str.Substring(delimIndex).TrimStart(new char[]{' '});
				}

				//There is a value
				else
				{
					nextDelimIndex = str.IndexOfAny(new char[]{' ','='}, delimIndex + 1);
					if(nextDelimIndex == -1)
						return table;

					//The value does not contain spaces
					if(str[nextDelimIndex] == ' ')
					{
						table[token] = str.Substring(delimIndex + 1, nextDelimIndex - delimIndex - 1);
						str = str.Substring(nextDelimIndex).TrimStart(new char[]{' '});
					}

					//The value may contain a space
					else
					{
						int valLength;

						// Get the lenght of the value
						try
						{
							valLength = int.Parse(str.Substring(delimIndex + 1, nextDelimIndex - delimIndex - 1));
						}
						catch(FormatException e)
						{
							MessageBox.Show(e + " is not a a valid length");
							str = str.Substring(str.IndexOf(' '));
							continue;
						}
						
						table[token] = str.Substring(nextDelimIndex + 1, valLength);
						str = str.Substring(nextDelimIndex + valLength  + 1).TrimEnd(new char[]{' '});
							
					}
				}
			}

			//parse(str, table);
			return table;
		}
		
		/// <summary>
		/// Creates the window for a discussion, and requests the user list of the discussion
		/// </summary>
		/// <param name="handle">The object ID of the discussion to create the window for</param>
		private void createDiscWindow(string handle)
		{
			IDiscussion disc = ((IDiscussion)database[handle]);
			if(disc.Window != null) return;
			disc.Window = new DiscussionWindow( (IDiscussion)disc, this);
			disc.Window.MdiParent = this;

			outStream.WriteLine("#$# what " + disc.Handle);
		}

		/// <summary>
		/// Creates a Private Message Window
		/// </summary>
		/// <param name="user">The lily IUser to create the PM window for</param>
		public void createPM(ILilyObject user)
		{
			user.Window = new PrivateMsg((IUser)user, this);
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
		/// Gets the Object ID of the IUser or Disucssion
		/// </summary>
		/// <param name="name">The name of the IUser or IDiscussion to get the Object ID of</param>
		/// <returns>The Object ID of the IUser or IDiscussion</returns>
		public string getObjectId(string name)
		{
			foreach(DictionaryEntry item in database)
			{
				try
				{
					if(((ILilyObject)item.Value).Name.ToUpper() == name.ToUpper())
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
					joinedDiscList.AllowClose = true;
					foreach(Form window in this.MdiChildren)
					{
						if(window.GetType() != typeof(JoindDiscWnd))
						{
							if(window.GetType() == typeof(DiagConsole))
								((DiagConsole)window).AllowClose = true;
							else
								((LilyWindow)window).AllowClose = true;
						}
					}
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
			if(console == null) console = new LilyWindow(this);
			console.Show();
		}

		/// <summary>
		/// Brings up the join discussion dialog box, and sends the server the join event
		/// TODO: Make this a non-modal dialog using the ILeafCmd interface
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
						joinedDiscList.Add((IDiscussion)database[ getObjectId(id.Trim()) ]);
					}
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Handle clicked toolbar buttons
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == connectBtn)
				connect();
			else if(e.Button == disconnectBtn)
			{
				if(MessageBox.Show("Do you really want to detach?", "Detach Confermation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					disconnect();
			}
			else if(e.Button == discBtn)
				if(discBtn.Pushed) joinedDiscList.Show();
				else               joinedDiscList.Hide();

		}

		private void diagConsoleItem_Click(object sender, System.EventArgs e)
		{
			diag.Show();
		}
	}
}
