using Microsoft.Win32;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Base class for the IDiscussion and private message windows.  Also functions as the console window
	/// </summary>
	public class LilyWindow : System.Windows.Forms.Form
	{
		protected System.Windows.Forms.Panel panel1;
		protected System.Windows.Forms.RichTextBox chatArea;
		protected System.Windows.Forms.Button sendBtn;
		protected System.Windows.Forms.TextBox userText;
		private System.Windows.Forms.ContextMenu chatAreaMenu;
		private System.Windows.Forms.MenuItem clearItem;
		private System.Windows.Forms.MenuItem scrollLockItem;
		protected bool allowClose;
		protected ILilyObject lilyObject;
		protected char prefix;
		protected LilyParent mainWindow;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem copyItem;

		#region Imported Structs/Methods

		private const Int32 CFM_LINK = 0x20;
		private const Int32 CFE_LINK = 0x20;
		private const Int32 CFM_COLOR = 0x40000000;
		private const Int32 CFE_AUTOCOLOR = 0x40000000;
		private const Int32 SCF_SELECTION = 0x1;
		private const Int32 WM_USER  = 0x400;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem tearItem;
		private System.Windows.Forms.MenuItem joinItem;
		private const Int32 EM_SETCHARFORMAT = WM_USER + 68;

		[ StructLayout (LayoutKind.Sequential)]
			private struct STRUCT_CHARFORMAT2
			{
				public Int32  cbSize;
				public Int32  dwMask;
				public Int32  dwEffects;
				public Int32  yHeight;
				public Int32  yOffset;
				public Int32  crTextColor;
				public byte   bCharSet;
				public byte   bPitchAndFamily;
			    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
				public char[] szFaceName;
				public Int16  wWeight;
				public Int16  sSpacing;
				public Int32  lcid;
				public Int32  dwReserved;
				public Int16  sStyle;
				public Int16  wKerning;
				public byte   bUnderlineType;
				public byte   bAnimation;
				public byte   bRevAuthor;
				public byte   bReserved1;
			}

		[DllImport("user32.dll")]
			private static extern Int32 SendMessage(IntPtr hWnd, Int32 msg, Int32 wParm, IntPtr lParm);

		[DllImport("user32.dll")]
			private static extern bool HideCaret(IntPtr hwnd);

		[DllImport("kernel32.dll")]
			private static extern int GetLastError();
		
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor needed to get the form designer to work properly
		/// </summary>
		public LilyWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
	
			this.AcceptButton = sendBtn;
			if(this.GetType() == typeof(LilyWindow)) Sock.Instance.NewInfo += new SockEventHandler(sock_NewInfo);
		}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			((LilyParent)MdiParent).UpdateUser -= new LilyParent.onNotifyDelegate(this.onNotify);
			// TODO: Fix NullReference Exception here
			//ParentForm.MdiChildActivate -= new EventHandler(this.LilyWindow_Activated);
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.chatArea = new System.Windows.Forms.RichTextBox();
			this.chatAreaMenu = new System.Windows.Forms.ContextMenu();
			this.clearItem = new System.Windows.Forms.MenuItem();
			this.scrollLockItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.tearItem = new System.Windows.Forms.MenuItem();
			this.joinItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.copyItem = new System.Windows.Forms.MenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.userText = new System.Windows.Forms.TextBox();
			this.sendBtn = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// chatArea
			// 
			this.chatArea.ContextMenu = this.chatAreaMenu;
			this.chatArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chatArea.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chatArea.HideSelection = false;
			this.chatArea.Name = "chatArea";
			this.chatArea.ReadOnly = true;
			this.chatArea.Size = new System.Drawing.Size(448, 389);
			this.chatArea.TabIndex = 3;
			this.chatArea.Text = "";
			this.chatArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatArea_KeyDown);
			this.chatArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chatArea_KeyPress);
			this.chatArea.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.chatArea_LinkClicked);
			// 
			// chatAreaMenu
			// 
			this.chatAreaMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.clearItem,
																						 this.scrollLockItem,
																						 this.menuItem1,
																						 this.tearItem,
																						 this.joinItem,
																						 this.menuItem2,
																						 this.copyItem});
			this.chatAreaMenu.Popup += new System.EventHandler(this.chatAreaMenu_Popup);
			// 
			// clearItem
			// 
			this.clearItem.Index = 0;
			this.clearItem.Text = "Clear";
			this.clearItem.Click += new System.EventHandler(this.clearItem_Click);
			// 
			// scrollLockItem
			// 
			this.scrollLockItem.Index = 1;
			this.scrollLockItem.RadioCheck = true;
			this.scrollLockItem.Text = "Scroll Lock";
			this.scrollLockItem.Click += new System.EventHandler(this.scrollLockItem_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "-";
			// 
			// tearItem
			// 
			this.tearItem.Index = 3;
			this.tearItem.Text = "Tear";
			this.tearItem.Click += new System.EventHandler(this.tearItem_Click);
			// 
			// joinItem
			// 
			this.joinItem.Index = 4;
			this.joinItem.Text = "Join";
			this.joinItem.Visible = false;
			this.joinItem.Click += new System.EventHandler(this.joinItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 5;
			this.menuItem2.Text = "-";
			// 
			// copyItem
			// 
			this.copyItem.Index = 6;
			this.copyItem.Text = "Copy";
			this.copyItem.Click += new System.EventHandler(this.copyItem_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.userText,
																				 this.sendBtn});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 389);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(448, 24);
			this.panel1.TabIndex = 0;
			// 
			// userText
			// 
			this.userText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.userText.Name = "userText";
			this.userText.Size = new System.Drawing.Size(373, 20);
			this.userText.TabIndex = 1;
			this.userText.Text = "";
			this.userText.WordWrap = false;
			// 
			// sendBtn
			// 
			this.sendBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.sendBtn.Location = new System.Drawing.Point(373, 0);
			this.sendBtn.Name = "sendBtn";
			this.sendBtn.Size = new System.Drawing.Size(75, 24);
			this.sendBtn.TabIndex = 0;
			this.sendBtn.Text = "Send";
			this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
			// 
			// LilyWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 413);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.chatArea,
																		  this.panel1});
			this.Name = "LilyWindow";
			this.Text = "Console";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.window_Closing);
			this.Load += new System.EventHandler(this.LilyWindow_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Allows access to the variable that determines if the window can close
		/// </summary>
		/// <value>Allows access to the variable that determines if the window can close</value>
		public bool AllowClose
		{
			set{ allowClose = value;}
		}

		/*
		public override LilyParent MdiParent
		{
			//get { return new LilyParent();}
			//get {return Parent.FindForm() as LilyParent;}
		}*/

		protected void ignoreResponse(LeafMessage msg)
		{
			post("*** " + msg.Response, Color.DarkGreen);
			Match match = Regex.Match(msg.Response, @".*you are (now|no longer) ignoring (.*)\)");
			if(match.Success)
				Util.PopulateIgnoreSettings(match.Result("$2"), Util.Database);
			else
				post("*** Regex failed (ignore) ***\n", Color.Red);
		}

		private void commandResponse(LeafMessage msg)
		{
			if(msg.Response == "") return;     // If there is nothing to write, don't write anything!
			chatArea.SelectionFont = new Font( "Lucida Console",
												8.25F, 
												System.Drawing.FontStyle.Regular,
												System.Drawing.GraphicsUnit.Point,
												((System.Byte)(0)));

			chatArea.Select(chatArea.Text.Length, 0);     // Go to the end of the text
			chatArea.SelectionColor = Color.Gray;
			chatArea.AppendText(msg.Response);

			chatArea.SelectionFont = new Font(  "Microsoft Sans Serif", 
												8.25F, 
												System.Drawing.FontStyle.Regular, 
												System.Drawing.GraphicsUnit.Point, 
												((System.Byte)(0)));
		}

		/// <summary>
		/// Handles client-specific commands and sends all other text to the lily server.
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		/// <remarks>
		/// <list type="bullet">Client commands
		/// <item><description>&id itemName - displays the object ID of the IUser/IDiscussion matching the name</description></item>
		/// <item><description>&name oID - displays the name of the IUser/IDiscussion with the given object ID</description></item>
		/// <item><description>&state itemName - displays the state of the IUser/IDiscussion matching the name</description></item>
		/// </list>
		/// </remarks>
		protected virtual void sendBtn_Click(object sender, System.EventArgs e)
		{
			if(userText.Text == string.Empty)
				return;

			if(userText.Text.StartsWith("/"))
			{
				LeafMessage msg = new LeafMessage(userText.Text, new ProcessResponse(commandResponse));
				Sock.Instance.PostMessage(msg);
			}
			else if(userText.Text.StartsWith("&id "))
			{
				string name = userText.Text.Substring(4);
				string id = Util.Database.GetByName(name).Handle;
				if(id == null)
					post(name + " was not found in my object database.\n", Color.DarkBlue);
				else
					post(name + " Object Id: " + id + "\n", Color.DarkBlue);
			}
			else if(userText.Text.StartsWith("&name "))
			{
				string id = userText.Text.Substring(6);
				ILilyObject item = (ILilyObject)Util.Database[id];
				if(item == null)
					post(id + " was not found in my object database.\n", Color.DarkBlue);
				else
					post(id + " is " + item.Name + "\n", Color.DarkBlue);
			}
			else if(this.lilyObject != null)
			{
				// Need to replace spaces in the name with underscores, or else lily will think the line ends early
				Sock.Instance.Write(prefix + this.lilyObject.Name.Replace(' ', '_') + ";" + userText.Text);
			}
			else
			{
				Sock.Instance.Write(userText.Text);
			}
			
			userText.Clear();
		}

		/// <summary>
		/// Opens the link's page in the default web browser
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		protected virtual void chatArea_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo(e.LinkText);
				startInfo.UseShellExecute = true;
				System.Diagnostics.Process.Start(startInfo);
			}
			catch(Win32Exception)
			{
				MessageBox.Show("Error Starting process " + e.LinkText);
			}
		}

		/// <summary>
		/// Displays the message in the IUser/IDiscussion's window
		/// </summary>
		/// <param name="notify">Notify event that holds the message information</param>
		public virtual void Post(NotifyEvent notify)
		{
			/*
			 * Inform if multiple recipients
			 */
			if(notify.Recipients.Length > 1)
			{
				string recipList = new string(' ',0);
				foreach(ILilyObject recip in notify.Recipients)
				{
					recipList += " " + recip.Name + ",";
				}
				post("(Sent to:" + recipList.Substring(0,recipList.Length - 1) + " )", Color.Black);
			}

			// Sender and time
			post("(" + notify.Time.TimeOfDay + ") <" + notify.Source.Name + "> ", Color.Black);

			// Message
			if(notify.Source == Util.Database.Me)
				post(notify.Value + "\n", Color.Red);
			else 
				post(notify.Value + "\n", Color.Blue);
		}
	
		/// <summary>
		/// Displays the text in the IUser/IDiscussion/console window
		/// </summary>
		/// <param name="message">Text to display</param>
		public virtual void Post(string message)
		{
			chatArea.AppendText(message + "\n");
		}

		/// <summary>
		/// Displays the given text in the window in the given color
		/// </summary>
		/// <param name="message">Text to display</param>
		/// <param name="color">Color of the text</param>
		/// <remarks>
		/// This is a utility function that should do all of the displaying.
		/// </remarks>
		protected virtual void post(string message, Color color)
		{
			chatArea.Select(chatArea.Text.Length, 0);     // Go to the end of the text
			chatArea.SelectionColor = color;
			chatArea.AppendText(message);
		}

		/// <summary>
		/// Needs to be overriden in IDiscussion/IUser windows to handle status change Notify events
		/// </summary>
		/// <param name="notify">The Notify event to act on</param>
		protected virtual void onNotify(NotifyEvent notify)
		{
		}
	
		/// <summary>
		/// If the window is not allowed to close, hide it instead
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		/// <remarks>
		/// This is used to keep from having to constantly move data around so the discussion's messages are not lost
		/// </remarks>
		protected virtual void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(!allowClose)
			{
				e.Cancel = true;
				Hide();
			}
		}

		/// <summary>
		/// Clears the chat area
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void clearItem_Click(object sender, System.EventArgs e)
		{
			chatArea.Clear();
		}

		/// <summary>
		/// Keeps the window from scrolling so the user can review easier
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		/// <remarks>
		/// Scroll lock currently does not work when the chat area is focused.  I'm working on it.
		/// </remarks>
		private void scrollLockItem_Click(object sender, System.EventArgs e)
		{
			scrollLockItem.Checked = !scrollLockItem.Checked;
			chatArea.HideSelection = scrollLockItem.Checked;
		}

		/// <summary>
		/// Disables the copy menuitem if there is no text selected
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void chatAreaMenu_Popup(object sender, System.EventArgs e)
		{
			if(chatArea.SelectedText.Length == 0)
				copyItem.Enabled = false;
			else
				copyItem.Enabled = true;
		}

		/// <summary>
		/// Copies the selected text
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void copyItem_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(chatArea.SelectedText, true);
		}
	
		/// <summary>
		/// NOT ENABLED: Flags a selectiong of text as a link
		/// </summary>
		/// <param name="link">True toggles the link on, false off</param>
		/// <remarks>
		/// The links don't persist between hiding/showing of the window.  May have to do with the underlying RichEdit control being too old a version.
		/// Uses the Win32 API to send the link setting message to the underlying RichEdit Control.
		/// </remarks>
		private void setSelectionLink(bool link)
		{
			// Create and populate the CHARFORMAT2 struct
			STRUCT_CHARFORMAT2 cf2;
			cf2.bAnimation = 0;
			cf2.bCharSet = 0;
			cf2.bPitchAndFamily = 0;
			cf2.bReserved1 = 0;
			cf2.bRevAuthor = 0;
			cf2.bUnderlineType = 0;
			cf2.cbSize = 0;
			cf2.dwReserved = 0;
			cf2.lcid = 0;
			cf2.sSpacing = 0;
			cf2.sStyle = 0;
			cf2.szFaceName = new String(' ', 32).ToCharArray();
			cf2.wKerning = 0;
			cf2.wWeight = 0;
			cf2.yHeight = 0;
			cf2.yOffset = 0;
			cf2.dwMask = CFM_LINK;
			cf2.dwEffects =  link ? CFE_LINK: 0;
			cf2.crTextColor = 0;
			cf2.cbSize = Marshal.SizeOf(cf2) + Marshal.SizeOf(cf2.cbSize);      // Need to add SizeOf cbSize because SizeOf(cf2) does not take the size of that parameter into account

			// Allocate the memory, and load in the CHARFORMAT2 struct into the lparam
			IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf2));
			Marshal.StructureToPtr(cf2, lParam, false);

			// Send the formatting message to the RichEdit control
			Int32 result = SendMessage(chatArea.Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam);

			// If there's an error, show the error number (Getting the actual error text is more work than it's worth
			if(result == 0)
                MessageBox.Show("Link setting error: " + GetLastError());

			// Always be kind to memory, and free it after use
			Marshal.FreeCoTaskMem(lParam);
		}

		public void LilyWindow_Activated(object sender, System.EventArgs e)
		{
			userText.Focus();
		}


		#region Input Redirection to UserText
		private bool redirToUserText = false;
		private void chatArea_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			ArrayList nonFocusKeys = new ArrayList(new Keys[] {Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.PageDown, Keys.PageUp, Keys.End, Keys.Home});

			if(!nonFocusKeys.Contains(e.KeyCode) && !e.Alt && !e.Control)
			{
				redirToUserText = true;
			}
			
		}

		private void chatArea_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(redirToUserText) 
			{
				e.Handled = true;
				userText.Focus();
				userText.AppendText(new string(e.KeyChar, 1));
			}
			
		}
		#endregion

		private void tearItem_Click(object sender, System.EventArgs e)
		{
			mainWindow = MdiParent as LilyParent;
			this.MdiParent = null;
			joinItem.Visible = true;
			tearItem.Visible = false;
		}

		private void joinItem_Click(object sender, System.EventArgs e)
		{
			MdiParent = mainWindow;
			tearItem.Visible = true;
			joinItem.Visible = false;
			this.Location = new Point(10,10);
		}

		private void LilyWindow_Load(object sender, System.EventArgs e)
		{
			// TODO: Fix MdiChild activation
		//	mdiParent.MdiChildActivate += new EventHandler(this.LilyWindow_Activated);
		}

	
		private void sock_NewInfo(object sender, SockEventArgs e)
		{
			Post(e.Line);
		}
	}
}
