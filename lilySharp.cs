using Microsoft.Win32;
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
	public class LilyParent : System.Windows.Forms.Form
	{
		public delegate void onNotifyDelegate(NotifyEvent notify);
		public delegate void eventDelegate(string line);
		private delegate void dispatchDelegate(Hashtable table);

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem fileItem;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem exitItem;

		private System.Windows.Forms.MenuItem connectItem;
		private LilyWindow console;
		private DiagConsole diag;
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
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem joinedItem;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem ignoreItem;
		private System.ComponentModel.IContainer components;

		public event onNotifyDelegate UpdateUser;

		/// <summary>
		/// Contructerator
		/// </summary>
		public LilyParent()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			// Socket event subscriptions
			Sock.Instance.DataRecieved += new SockEventHandler(sock_DataRecieved);
			Sock.Instance.DiscRecieved += new SockEventHandler(sock_DiscRecieved);
			Sock.Instance.UserRecieved += new SockEventHandler(sock_UserRecieved);
			Sock.Instance.Disconnected += new SockEventHandler(sock_Disconnected);
			Sock.Instance.ConnectComplete += new SockEventHandler(sock_ConnectComplete);
			Sock.Instance.UserStateChanged += new SockEventHandler(sock_UserStateChanged);
			Sock.Instance.MessageRecieved += new SockEventHandler(sock_MessageRecieved);

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
			joinedDiscList = new JoindDiscWnd(msgPanel);
			joinedDiscList.MdiParent = this;

			// Create Diagnostic console
			diag = new DiagConsole();
			diag.MdiParent = this;

			// Create console
			console = new LilyWindow();
			console.MdiParent = this;

			Util.Database = new HashDb();
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
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.joinedItem = new System.Windows.Forms.MenuItem();
			this.windowItem = new System.Windows.Forms.MenuItem();
			this.cascadeItem = new System.Windows.Forms.MenuItem();
			this.horizontalItem = new System.Windows.Forms.MenuItem();
			this.verticalItem = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.ignoreItem = new System.Windows.Forms.MenuItem();
			this.helpItem = new System.Windows.Forms.MenuItem();
			this.diagConsoleItem = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.connectBtn = new System.Windows.Forms.ToolBarButton();
			this.disconnectBtn = new System.Windows.Forms.ToolBarButton();
			this.discBtn = new System.Windows.Forms.ToolBarButton();
			this.toolbarLst = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.fileItem,
																					  this.discList,
																					  this.windowItem,
																					  this.menuItem5,
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
																					 this.createItem,
																					 this.menuItem4,
																					 this.joinedItem});
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
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "-";
			// 
			// joinedItem
			// 
			this.joinedItem.Index = 3;
			this.joinedItem.Text = "J&oined";
			this.joinedItem.Click += new System.EventHandler(this.joinedItem_Click);
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
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.ignoreItem});
			this.menuItem5.Text = "User";
			// 
			// ignoreItem
			// 
			this.ignoreItem.Enabled = false;
			this.ignoreItem.Index = 0;
			this.ignoreItem.Text = "Ignore Settings";
			this.ignoreItem.Click += new System.EventHandler(this.ignoreItem_Click);
			// 
			// helpItem
			// 
			this.helpItem.Index = 4;
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
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 543);
			this.statusBar.Name = "statusBar";
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(752, 22);
			this.statusBar.TabIndex = 1;
			this.statusBar.Text = "Not Connected";
			this.statusBar.PanelClick += new System.Windows.Forms.StatusBarPanelClickEventHandler(this.statusBar_PanelClick);
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
			this.discBtn.ToolTipText = "Show Joined Discussions";
			// 
			// toolbarLst
			// 
			this.toolbarLst.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.toolbarLst.ImageSize = new System.Drawing.Size(16, 16);
			this.toolbarLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolbarLst.ImageStream")));
			this.toolbarLst.TransparentColor = System.Drawing.Color.Transparent;
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
			this.Text = "LilySharp v0.60 Alpha";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.LilyParent_Closing);
			this.Load += new System.EventHandler(this.LilyParent_Load);
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
			connectPanel.Text = "Connecting...";
			try
			{
				Sock.Instance.Connect();
			}
			catch (SockException)
			{
				// User cancelled the connect
				connectPanel.Text = "Not connected";
				return;
			}
			catch
			{
				connectPanel.Text = "Cannot connect";
				return;
			}


			connectItem.Visible = false;
			detachItem.Visible = true;
			connectBtn.Visible = false;
			disconnectBtn.Visible = true;
			if(console == null) 
			{
				console = new LilyWindow();
				console.MdiParent = this;
				console.Show();
			}
		}

		/// <summary>
		/// Updates the GUI and gets my discussion and ignore information after connection to server
		///   has been established.
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event arguments</param>
		private void sock_ConnectComplete(object sender, SockEventArgs e)
		{
			connectPanel.Text = "Connected";
			
			Sock.Instance.Write("/set message_echo yes");
			Sock.Instance.Write("#$# client LilySharp 0.60");
			Sock.Instance.PostMessage(new LeafMessage("/where " + Util.Database.Me.Name, new ProcessResponse(myDiscsRecieved)));
			//Sock.Instance.PostMessage(new LeafMessage("/ignore", new ProcessResponse(ignoreSettingsRecieved)));
			
			joinItem.Enabled = true;
			createItem.Enabled = true;
			ignoreItem.Enabled = true;
		}

		/// <summary>
		/// Detaches from the server, closes the connection, and returns the GUI to a not connected state
		/// </summary>
		private void sock_Disconnected(object sender, SockEventArgs e)
		{
			// Update the GUI
			connectPanel.Text = "Not Connected";
			detachItem.Visible = false;
			connectItem.Visible = true;
			connectBtn.Visible = true;
			disconnectBtn.Visible = false;
			joinItem.Enabled = false;
			createItem.Enabled = false;
			ignoreItem.Enabled = false;

			// Cleanup client db
			Util.Database.Clear();
			joinedDiscList.Clear();
		}
		
	
		/// <summary>
		/// Handles all the data events
		/// </summary>
		/// <param name="sender">The sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void sock_DataRecieved(object sender, SockEventArgs e)
		{
			if(e.Attributes["SOURCE"] as string == "#0")
			{
				if(e.Attributes["NAME"] as string == "whoami")
				{
					Util.Database.Me = (IUser)Util.Database[e.Attributes["VALUE"] as string];
				}
				else if(e.Attributes["NAME"] as string == "events")
				{
					// It's safe to ignore this.
					//   If there's an event my client doesn't know about, there's nothing I can do here to help that
					//   If there are events my client does handle that arn't listed, nothing will break, it's just unused code.
				}
				else
				{
					console.Post("*** data ***");
					foreach(DictionaryEntry entry in e.Attributes)
						console.Post(entry.Key + " = " + entry.Value);
					console.Post("******************");
				}
			}
			else
			{

				if(e.Attributes["NAME"] as string == "members")
				{
					((DiscussionWindow)Util.Database[e.Attributes["SOURCE"]].Window).AddMembers( ((string)e.Attributes["VALUE"]).Split( ((string)e.Attributes["LIST"]).ToCharArray()));
				}
			}
		}
	
		private void sock_UserStateChanged(object sender, SockEventArgs e)
		{
			switch (e.Notify.Event)
			{
				case "blurb":
					((IUser)e.Notify.Source).Blurb = e.Notify.Value;
					break;
				case "rename":
					e.Notify.Source.Name = e.Notify.Value;
					break;
				case "destroy":
					joinedDiscList.Remove(e.Notify.Recipients[0] as IDiscussion);
					Util.Database.Remove(e.Notify.Recipients[0].Handle);
					break;
				case "retitle":
					((IDiscussion)e.Notify.Recipients[0]).Title = e.Notify.Value;
					break;
				case "detach":
				case "disconnect":
					((IUser)e.Notify.Source).State = States.Detached;
					break;
				case "attach":
				case "here":
				case "connect":
					((IUser)e.Notify.Source).State = States.Here;
					break;
				case "away":
					((IUser)e.Notify.Source).State = States.Away;
					break;
				case "join":
					if(e.Notify.Source == Util.Database.Me)
						foreach(IDiscussion disc in e.Notify.Recipients)
						{
							this.createDiscWindow(disc);
							joinedDiscList.Add(disc);
						}

					
					if( e.Notify.Recipients[0].Window != null)
						((DiscussionWindow)e.Notify.Recipients[0].Window).AddMembers(new String[] {e.Notify.Source.Handle});
					break;
				case "quit":
					if(e.Notify.Source == Util.Database.Me)
					{
						foreach(IDiscussion disc in e.Notify.Recipients)
						{
							joinedDiscList.Remove(e.Notify.Recipients[0] as IDiscussion);
							disc.Window = null;
						}
					}
					break;
				case "create":
					if(e.Notify.Source == Util.Database.Me)
					{
						createDiscWindow(e.Notify.Recipients[0] as IDiscussion);
						joinedDiscList.Add(e.Notify.Recipients[0] as IDiscussion);
					}
					break;
				case "drename":
					e.Notify.Recipients[0].Name = e.Notify.Value;
					break;
				default:
					break;
			}
		}

		private void sock_MessageRecieved(object sender, SockEventArgs e)
		{
			switch(e.Notify.Event)
			{
				case "private":
				case "public":
					// We need to be able to handle multiple recpients
					foreach(ILilyObject recipient in e.Notify.Recipients)
					{
							/*
							 * Private message
							 */
						if(recipient is IUser)
						{
							// Someone sent me a message
							if(recipient == Util.Database.Me)
							{
								if(e.Notify.Source.Window == null)
									createPM(e.Notify.Source);

								e.Notify.Source.Window.Post(e.Notify);
							}
								// I sent someone else a message
							else if(e.Notify.Source == Util.Database.Me)
							{
								if(recipient.Window == null)
									createPM(recipient);

								recipient.Window.Post(e.Notify);
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
							recipient.Window.Post(e.Notify);
						}

					}
					break;
				case "emote":
					foreach(ILilyObject recipient in e.Notify.Recipients)
					{
						if(recipient.Window == null)
							return;
						else
						{
							((DiscussionWindow)recipient.Window).PostEmote(e.Notify);
							if(!recipient.Window.Visible)
								joinedDiscList.AddMsg( (IDiscussion)recipient);
						}
					}
					break;
				default:
					break;
			}
		}


		/// <summary>
		/// Handles notify events
		/// </summary>
		/// <param name="sender">The sender of the event</param>
		/// <param name="e">Event arguments</param>
		/// <remarks>
		/// UpdateUser is not called uniformly, but per each event, because some events require
		///  it be called before the status change, and some after.
		/// </remarks>
		private void handleNotifyItem(object sender, SockEventArgs e)
		{
			NotifyEvent notify = new NotifyEvent(e.Attributes);

			// If I can't find the source of the event in my database, the database
			//   Is probably corrupt.  Let's resynch it then.
			if(notify.Source == null)
			{
				console.Post("*** Null source detected.  Possible database corruption.");
				console.Post("*** One event ( " + notify.Event + " from " + e.Attributes["SOURCE"] + " ) lost!");
				console.Post("Resynching database...");
				Sock.Instance.Write("#$# slcp-sync");
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
							if(recipient is IUser)
							{
								// Someone sent me a message
								if(recipient == Util.Database.Me)
								{
									if(notify.Source.Window == null)
										createPM(notify.Source);

									notify.Source.Window.Post(notify);
								}
								// I sent someone else a message
								else if(notify.Source == Util.Database.Me)
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
					case "unidle":
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "attach":
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
						if(notify.Source == Util.Database.Me)
							foreach(IDiscussion disc in notify.Recipients)
							{
								this.createDiscWindow(disc);
								joinedDiscList.Add(disc);
							}


						if( notify.Recipients[0].Window != null)
							((DiscussionWindow)notify.Recipients[0].Window).AddMembers(new String[] {notify.Source.Handle});

						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "quit":
						if(notify.Source == Util.Database.Me)
						{
							joinedDiscList.Remove(notify.Recipients[0] as IDiscussion);
						}
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "destroy":
						if(UpdateUser != null) UpdateUser(notify);
						joinedDiscList.Remove(notify.Recipients[0] as IDiscussion);
						Util.Database.Remove(notify.Recipients[0].Handle);
						break;
					case "retitle":
						if(UpdateUser != null) UpdateUser(notify);
						((IDiscussion)notify.Recipients[0]).Title = notify.Value;

						if(notify.Recipients[0] is IDiscussion && notify.Recipients[0].Window != null)
						{
							joinedDiscList.Remove(notify.Recipients[0] as IDiscussion);
							joinedDiscList.Add(notify.Recipients[0] as IDiscussion);
						}
						break;
					case "ignore":
					case "unignore":
						if(UpdateUser != null) UpdateUser(notify);
						break;
					case "create":
						if(notify.Source == Util.Database.Me)
						{
							createDiscWindow(notify.Recipients[0] as IDiscussion);
							joinedDiscList.Add(notify.Recipients[0] as IDiscussion);
						}

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
						foreach(DictionaryEntry entry in e.Attributes)
							console.Post(entry.Key + " = " + entry.Value);
						break;
					default:
						console.Post("*** Unhandled Event: " + notify.Event + " ***");
						foreach(DictionaryEntry entry in e.Attributes)
							console.Post(entry.Key + " = " + entry.Value);
						break;
				}
			}
			catch(Exception ex)
			{
				/*
				 * It's time for monster debuggin output!  Wheeeeeeeeeee
				 */
				console.Post("*** Exception ***");
				console.Post("Event: " + notify.Event);
				console.Post("Error: " + ex.Message);
				if(notify.Source != null) console.Post("Source: " + notify.Source.Name);
				if(notify.Recipients != null)
					foreach(ILilyObject recip in notify.Recipients)
						console.Post("Recipient: " + recip.Name);
				if(notify.Value != "") console.Post("Value: " + notify.Value);

			}
		}
	
		/// <summary>
		/// Adds a new discussion to the database.
		/// </summary>
		/// <param name="sender">The sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void sock_DiscRecieved(object sender, SockEventArgs e)
		{
			IDiscussion newDisc = new Discussion(e.Attributes);
				/*
				* If we get a %DISC of an existing discussion (someone was permitted)
				*   we should link the window back to the discussion so we don't have 
				*   two windows for the same disc
				*/
			if(Util.Database[newDisc.Handle] != null)
			{
				newDisc.Window = Util.Database[newDisc.Handle].Window;
				((DiscussionWindow)newDisc.Window).LObject = newDisc;
			}

			Util.Database[newDisc.Handle] = newDisc;
		}

		/// <summary>
		/// Adds a new user to the database
		/// </summary>
		/// <param name="sender">The sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void sock_UserRecieved(object sender, SockEventArgs e)
		{
			IUser newUser = new User(e.Attributes);

			/*
			 * TODO:  Make this part of the IUser class, so this can be done in one line here
			 */
			if(Util.Database[newUser.Handle] != null)
			{
				((IUser)Util.Database[newUser.Handle]).Blurb   = newUser.Blurb;
				((IUser)Util.Database[newUser.Handle]).Finger  = newUser.Finger;
				((IUser)Util.Database[newUser.Handle]).Info    = newUser.Info;
				((IUser)Util.Database[newUser.Handle]).Memo    = newUser.Memo;
				((IUser)Util.Database[newUser.Handle]).Name    = newUser.Name;
				((IUser)Util.Database[newUser.Handle]).Pronoun = newUser.Pronoun;
				((IUser)Util.Database[newUser.Handle]).State   = newUser.State;
			}
			else
			{
				Util.Database[newUser.Handle] = newUser;
			}
		}
		
		/// <summary>
		/// Populates the list of my discussions.
		/// </summary>
		/// <param name="msg">The message sent to the server, along with the response</param>
		private void myDiscsRecieved(LeafMessage msg)
		{
			/*
		 	 * This is used to get what discussions I'm a member of.  If the server can't
			 *   find me, then there must have been some important lost data
			 * 
			 * TODO:  Attempt to re-get my lilyObject.  Try to be a little more presistent.
			 */
			if(msg.Response.StartsWith("(could find no user to match to"))
			{
				MessageBox.Show("Critical Error: Current user is unknown!  Possible communication error.", "Unrecoverable error");
				Sock.Instance.Disconnect();
				return;
			}

			// Go to the start of the disc list
			int startIndex = msg.Response.IndexOf(' ');
			startIndex += "are a member of ".Length;

			// Itterate through the list, and populate =)
			foreach(String discName in msg.Response.Substring(startIndex).Split(','))
			{
				createDiscWindow(Util.Database.GetByName(discName.Trim()) as IDiscussion);
				joinedDiscList.Add(Util.Database.GetByName(discName.Trim()) as IDiscussion);
			}
		}

		/// <summary>
		/// Populates the ignore settings of the users we are ignoring
		/// </summary>
		/// <param name="msg">Message sent to the server, along with the response</param>
		private void ignoreSettingsRecieved(LeafMessage msg)
		{
			// Get ignore settings  $1 = Who I'm ignoring, $2 = Who's ignoring me
			Match ignoreMatch = Regex.Match(msg.Response, @"\(you are currently ignoring (.*) and being ignored by (.*)\)");

			// If noone is ignoring me, and I'm ignoring noone, don't need to do anything more
			if(!ignoreMatch.Success || ( ignoreMatch.Result("$1") == "no one" && ignoreMatch.Result("$2") == "no one"))
				return;
	
			// Populate the ignore settings
			foreach(string ignoreString in ignoreMatch.Result("$1").Split(",".ToCharArray()))
				Util.PopulateIgnoreSettings(ignoreString, Util.Database);
		}


		/// <summary>
		/// Disconnects from the lily server nicely before exiting.  Also saves window size/position.
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Allows the canceling of the event</param>
		private void LilyParent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = false;
			if(Sock.Instance.Active) Sock.Instance.Disconnect();

			// Save Window State
			RegistryKey formSize = Registry.CurrentUser.OpenSubKey("Software\\VulpineSoft\\LilySharp", true);
				if(formSize == null)
				{
					formSize = Registry.CurrentUser.CreateSubKey("Software\\VulpineSoft\\LilySharp");
					if(formSize == null) return;
				}
				
				formSize.SetValue("MainWindowWidth", ((Control)sender).Width);
				formSize.SetValue("MainWindowHeight", ((Control)sender).Height);
				formSize.SetValue("MainWindowTop", ((Control)sender).Top);
				formSize.SetValue("MainWindowLeft", ((Control)sender).Left);
			formSize.Close();
		}
	
		/// <summary>
		/// Detaches from the lily server
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void detachItem_Click(object sender, System.EventArgs e)
		{
			Sock.Instance.Disconnect();
		}


		/// <summary>
		/// Creates the window for a discussion, and requests the user list of the discussion
		/// </summary>
		/// <param name="handle">The object ID of the discussion to create the window for</param>
		private void createDiscWindow(IDiscussion disc)
		{
			if(disc.Window != null) return;
			disc.Window = new DiscussionWindow(disc as IDiscussion);
			disc.Window.MdiParent = this;

			Sock.Instance.Write("#$# what " + disc.Handle);
		}

		/// <summary>
		/// Creates a Private Message Window
		/// </summary>
		/// <param name="user">The lily IUser to create the PM window for</param>
		public void createPM(ILilyObject user)
		{
			user.Window = new PrivateMsg(user as IUser);
			user.Window.MdiParent = this;
			user.Window.Show();
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
						if(!(window is JoindDiscWnd))
						{
							if(window is DiagConsole)
								((DiagConsole)window).AllowClose = true;
							else
								((LilyWindow)window).AllowClose = true;
						}
					}
					break;
			}
			base.WndProc(ref msg);
		}

		#region Menu Clicked Events

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
		/// Displays the console
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void consoleItem_Click(object sender, System.EventArgs e)
		{
			//if(console == null) console = new LilyWindow();
			console.Show();
		}

		/// <summary>
		/// Brings up the join discussion dialog box
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void joinItem_Click(object sender, System.EventArgs e)
		{
			JoinDiscDlg joinDlg = new JoinDiscDlg();
			joinDlg.Show();
		}

		/// <summary>
		/// Shows the create discussion dialog
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void createItem_Click(object sender, System.EventArgs e)
		{
			CreateDlg create = new CreateDlg();
			create.Show();			
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
					Sock.Instance.Disconnect();
			}
			else if(e.Button == discBtn)
				if(discBtn.Pushed) joinedDiscList.Show();
				else               joinedDiscList.Hide();

		}

		/// <summary>
		/// Displays the diagnostic console
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void diagConsoleItem_Click(object sender, System.EventArgs e)
		{
			diag.Show();
		}

		/// <summary>
		/// Displays or hides the joined discussions list when the user double-clicks on
		///   the message notification panel
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void statusBar_PanelClick(object sender, System.Windows.Forms.StatusBarPanelClickEventArgs e)
		{
			if(e.StatusBarPanel == msgPanel && e.Button == MouseButtons.Left && e.Clicks >= 2)
			{
				if(joinedDiscList.Visible)
				{
					joinedDiscList.Hide();
				}
				else
				{
					joinedDiscList.Show();
				}
			}
		}

		private void joinedItem_Click(object sender, System.EventArgs e)
		{
			if(!joinedDiscList.Visible)
			{
				joinedDiscList.Show();
			}
		}

		/// <summary>
		/// Displays the advanced ignore settings dialog
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void ignoreItem_Click(object sender, System.EventArgs e)
		{
			IgnoreDlg ignore = new IgnoreDlg();
			ignore.Owner = this;
			ignore.Show();
		}

		#endregion

		/// <summary>
		/// Resets the window's size/position to where it was when it was last closed
		/// If there is no size information, it creates it with default values
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void LilyParent_Load(object sender, System.EventArgs e)
		{
			// Set form size
			RegistryKey formSize = Registry.CurrentUser.OpenSubKey("Software\\VulpineSoft\\LilySharp", true);
				if(formSize == null)   // No size information; create new entries
				{
					formSize = Registry.CurrentUser.CreateSubKey("Software\\VulpineSoft\\LilySharp");
					formSize.SetValue("MainWindowWidth", this.Width);
					formSize.SetValue("MainWindowHeight", this.Height);
					formSize.SetValue("MainWindowTop", this.Top);
					formSize.SetValue("MainWindowLeft", this.Left);
				}
				else
				{
					this.Width = int.Parse(formSize.GetValue("MainWindowWidth", this.Width).ToString());
					this.Height = int.Parse(formSize.GetValue("MainWindowHeight", this.Height).ToString());
					this.Top = int.Parse(formSize.GetValue("MainWindowTop", this.Top).ToString());
					this.Left = int.Parse(formSize.GetValue("MainWindowLeft", this.Left).ToString());
				}
			formSize.Close();
		}

	}
}
