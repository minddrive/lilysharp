using Microsoft.Win32;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Base class for the Discussion and private message windows.  Also functions as the console window
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
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem copyItem;

		[DllImport("user32.dll")]
			private static extern bool HideCaret(IntPtr hwnd);

		[DllImport("kernel32.dll")]
			private static extern int GetLastError();

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
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.AcceptButton = sendBtn;
		}

		/// <summary>
		/// Initialize the form, and subscribe to the updateUser event
		/// </summary>
		/// <param name="parent">This window's parent</param>
		public LilyWindow(LilyParent parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.AcceptButton = sendBtn;
			this.MdiParent = parent;

			parent.UpdateUser += new LilyParent.updateUserDelegate(updateUser);
			chatArea.GotFocus += new EventHandler(this.chatArea_focused);

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
			((LilyParent)MdiParent).UpdateUser -= new LilyParent.updateUserDelegate(this.updateUser);
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
			this.chatArea.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.chatArea_LinkClicked);
			// 
			// chatAreaMenu
			// 
			this.chatAreaMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.clearItem,
																						 this.scrollLockItem,
																						 this.menuItem1,
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
			// copyItem
			// 
			this.copyItem.Index = 3;
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

		/// <summary>
		/// A wrapper for the parent class to allow use without casting
		/// </summary>
		/// <value>A wrapper for the parent class to allow use without casting</value>
		protected LilyParent mdiParent
		{
			get{ return (LilyParent)MdiParent;}
			set{ MdiParent = value;}
		}

		/// <summary>
		/// Handles client-specific commands and sends all other text to the lily server.
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		/// <remarks>
		/// <list type="bullet">Client commands
		/// <item><description>&id itemName - displays the object ID of the User/Discussion matching the name</description></item>
		/// <item><description>&name oID - displays the name of the User/Discussion with the given object ID</description></item>
		/// <item><description>&state itemName - displays the state of the User/Discussion matching the name</description></item>
		/// </list>
		/// </remarks>
		protected virtual void sendBtn_Click(object sender, System.EventArgs e)
		{
			if(userText.Text.StartsWith("&id"))
			{
				string name = userText.Text.Substring(4);
				string id = mdiParent.getObjectId(name);
				if(id == null)
					Post(name + " was not found in my object database.");
				else
					Post(name + " Object Id: " + id);
			}
			else if(userText.Text.StartsWith("&name"))
			{
				string id = userText.Text.Substring(6);
				LilyItem item = (LilyItem)mdiParent.Handles[id];
				if(item == null)
					Post(id + " was not found in my object database.");
				else
					Post(id + " is " + item.Name);
			}
			else if(userText.Text.StartsWith("&state"))
			{
				string name = userText.Text.Substring(7);
				if(mdiParent.getObjectId(name) == null)
				{
					Post(name + " was not found in my object database.");
				}
				else
				{
					User user = (User)mdiParent.Handles[mdiParent.getObjectId(name)];
					Post(name + " is " + user.State);
				}
			}
			else
			{
				((LilyParent)MdiParent).Out.WriteLine(userText.Text);
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
			ProcessStartInfo startInfo = new ProcessStartInfo(e.LinkText);
			startInfo.UseShellExecute = true;
			System.Diagnostics.Process.Start(startInfo);
		}

		/// <summary>
		/// Displays the message in the User/Discussion's window
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
				foreach(LilyItem recip in notify.Recipients)
				{
					recipList += " " + recip.Name + ",";
				}
				post("(Sent to:" + recipList.Substring(0,recipList.Length - 1) + " )", Color.Black);
			}

			// Sender and time
			post("(" + notify.Time.TimeOfDay + ") <" + notify.Source.Name + "> ", Color.Black);

			// Message
			if(notify.Source == ((LilyParent)MdiParent).Me) 
				post(notify.Value + "\n", Color.Red);
			else 
				post(notify.Value + "\n", Color.Blue);
		}
	
		/// <summary>
		/// Displays the text in the User/Discussion/console window
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
		/// Needs to be overriden in Discussion/User windows to handle status change Notify events
		/// </summary>
		/// <param name="notify">The Notify event to act on</param>
		protected virtual void updateUser(NotifyEvent notify)
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

		private void chatArea_focused(object sender, System.EventArgs e)
		{
			if(!HideCaret(this.chatArea.Handle))
				chatArea.AppendText("Caret Hiding failed: " + GetLastError().ToString() + "\n");
			
			//chatArea.AppendText(HideCaret((int)this.Handle).ToString());
		}

		private void chatArea_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.userText.Text += e.KeyChar;
			//e.Handled = true;
		}
	}
}
