using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// The GUI window for a discussion
	/// </summary>
	///
	public class DiscussionWindow : LilyWindow
	{
		private System.Windows.Forms.Splitter splitter1;
		
		private ToolTip blurbTip;
		private IUser rightClickedUser;
		private System.Windows.Forms.ContextMenu userListMenu;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem infoItem;
		private System.Windows.Forms.MenuItem memoItem;
		private System.Windows.Forms.MenuItem fingerItem;
		private System.Windows.Forms.MenuItem pmItem;
		private System.Windows.Forms.ListBox userList;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem ignoreItem;
		private System.Windows.Forms.MenuItem notifySetItem;
		private System.Windows.Forms.MenuItem ignoreHereItem;
		private System.Windows.Forms.MenuItem ignorePrivateItem;
		private System.Windows.Forms.MenuItem ignoreInDiscItem;
		private System.Windows.Forms.MenuItem ignoreInDiscAllItem;
		private System.Windows.Forms.MenuItem ignoreInDiscAddItem;
		private System.Windows.Forms.MenuItem ignoreInDiscExceptionsAddItem;
		private System.Windows.Forms.MenuItem ignoreInDiscExceptionItem;
		private System.Windows.Forms.MenuItem ignoreInDiscSeperator;
		private System.Windows.Forms.MenuItem ignoreInDiscExceptionsSeperator;

		private delegate void writeText(string text, Color color);
		private delegate void showWindow();

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="disc">The discussion this window is for</param>
		/// <param name="parent">This window's parent</param>
		public DiscussionWindow(IDiscussion disc, LilyParent parent) : base(parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.lilyObject = disc;
			this.Text = disc.Name + " [ " + disc.Title + " ]";

			blurbTip = new ToolTip();
			allowClose = false;
			prefix = '-';
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
			
			this.lilyObject.Window = null;
			base.Dispose( disposing );
			
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.userListMenu = new System.Windows.Forms.ContextMenu();
			this.infoItem = new System.Windows.Forms.MenuItem();
			this.memoItem = new System.Windows.Forms.MenuItem();
			this.fingerItem = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.ignoreItem = new System.Windows.Forms.MenuItem();
			this.ignorePrivateItem = new System.Windows.Forms.MenuItem();
			this.ignoreInDiscItem = new System.Windows.Forms.MenuItem();
			this.ignoreInDiscAllItem = new System.Windows.Forms.MenuItem();
			this.ignoreInDiscSeperator = new System.Windows.Forms.MenuItem();
			this.ignoreInDiscAddItem = new System.Windows.Forms.MenuItem();
			this.ignoreInDiscExceptionItem = new System.Windows.Forms.MenuItem();
			this.ignoreInDiscExceptionsAddItem = new System.Windows.Forms.MenuItem();
			this.ignoreInDiscExceptionsSeperator = new System.Windows.Forms.MenuItem();
			this.ignoreHereItem = new System.Windows.Forms.MenuItem();
			this.notifySetItem = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.pmItem = new System.Windows.Forms.MenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.userList = new System.Windows.Forms.ListBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 261);
			this.panel1.Size = new System.Drawing.Size(405, 24);
			this.panel1.Visible = true;
			// 
			// chatArea
			// 
			this.chatArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chatArea.Size = new System.Drawing.Size(405, 261);
			this.chatArea.Visible = true;
			// 
			// sendBtn
			// 
			this.sendBtn.Location = new System.Drawing.Point(330, 0);
			this.sendBtn.Visible = true;
			// 
			// userText
			// 
			this.userText.Size = new System.Drawing.Size(330, 20);
			this.userText.Visible = true;
			// 
			// userListMenu
			// 
			this.userListMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.infoItem,
																						 this.memoItem,
																						 this.fingerItem,
																						 this.menuItem7,
																						 this.ignoreItem,
																						 this.notifySetItem,
																						 this.menuItem5,
																						 this.pmItem});
			this.userListMenu.Popup += new System.EventHandler(this.userListMenu_Popup);
			// 
			// infoItem
			// 
			this.infoItem.Index = 0;
			this.infoItem.Text = "Info";
			this.infoItem.Click += new System.EventHandler(this.infoItem_Click);
			// 
			// memoItem
			// 
			this.memoItem.Index = 1;
			this.memoItem.Text = "Memo";
			this.memoItem.Click += new System.EventHandler(this.memoItem_Click);
			// 
			// fingerItem
			// 
			this.fingerItem.Index = 2;
			this.fingerItem.Text = "Finger";
			this.fingerItem.Click += new System.EventHandler(this.fingerItem_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 3;
			this.menuItem7.Text = "-";
			// 
			// ignoreItem
			// 
			this.ignoreItem.Index = 4;
			this.ignoreItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.ignorePrivateItem,
																					   this.ignoreInDiscItem,
																					   this.ignoreHereItem});
			this.ignoreItem.Text = "Ignore";
			// 
			// ignorePrivateItem
			// 
			this.ignorePrivateItem.Index = 0;
			this.ignorePrivateItem.Text = "Privately";
			this.ignorePrivateItem.Click += new System.EventHandler(this.ignorePrivateItem_Click);
			// 
			// ignoreInDiscItem
			// 
			this.ignoreInDiscItem.Index = 1;
			this.ignoreInDiscItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.ignoreInDiscAllItem,
																							 this.ignoreInDiscAddItem,
																							 this.ignoreInDiscSeperator,
																							 this.ignoreInDiscExceptionItem});
			this.ignoreInDiscItem.Text = "In Disc";
			// 
			// ignoreInDiscAllItem
			// 
			this.ignoreInDiscAllItem.Index = 0;
			this.ignoreInDiscAllItem.Text = "All";
			this.ignoreInDiscAllItem.Click += new System.EventHandler(this.ignoreInDiscAllItem_Click);
			// 
			// ignoreInDiscSeperator
			// 
			this.ignoreInDiscSeperator.Index = 2;
			this.ignoreInDiscSeperator.Text = "-";
			// 
			// ignoreInDiscAddItem
			// 
			this.ignoreInDiscAddItem.Index = 1;
			this.ignoreInDiscAddItem.Text = "Add...";
			this.ignoreInDiscAddItem.Click += new System.EventHandler(this.ignoreInDiscAddItem_Click);
			// 
			// ignoreInDiscExceptionItem
			// 
			this.ignoreInDiscExceptionItem.Index = 3;
			this.ignoreInDiscExceptionItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																									  this.ignoreInDiscExceptionsAddItem,
																									  this.ignoreInDiscExceptionsSeperator});
			this.ignoreInDiscExceptionItem.Text = "Except";
			// 
			// ignoreInDiscExceptionsAddItem
			// 
			this.ignoreInDiscExceptionsAddItem.Index = 0;
			this.ignoreInDiscExceptionsAddItem.Text = "Add...";
			this.ignoreInDiscExceptionsAddItem.Click += new System.EventHandler(this.ignoreInDiscExceptionsAddItem_Click);
			// 
			// ignoreInDiscExceptionsSeperator
			// 
			this.ignoreInDiscExceptionsSeperator.Index = 1;
			this.ignoreInDiscExceptionsSeperator.Text = "-";
			// 
			// ignoreHereItem
			// 
			this.ignoreHereItem.Index = 2;
			this.ignoreHereItem.Text = "Here";
			this.ignoreHereItem.Click += new System.EventHandler(this.ignoreHereItem_Click);
			// 
			// notifySetItem
			// 
			this.notifySetItem.Index = 5;
			this.notifySetItem.Text = "Notify Settings...";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 6;
			this.menuItem5.Text = "-";
			// 
			// pmItem
			// 
			this.pmItem.Index = 7;
			this.pmItem.Text = "Private Message";
			this.pmItem.Click += new System.EventHandler(this.pmItem_Click);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(405, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 285);
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			// 
			// userList
			// 
			this.userList.ContextMenu = this.userListMenu;
			this.userList.Dock = System.Windows.Forms.DockStyle.Right;
			this.userList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.userList.HorizontalScrollbar = true;
			this.userList.IntegralHeight = false;
			this.userList.Location = new System.Drawing.Point(408, 0);
			this.userList.Name = "userList";
			this.userList.Size = new System.Drawing.Size(88, 285);
			this.userList.TabIndex = 2;
			this.userList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.userList_MouseMove);
			this.userList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.userList_DrawItem);
			// 
			// DiscussionWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 285);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.chatArea,
																		  this.panel1,
																		  this.splitter1,
																		  this.userList});
			this.Name = "DiscussionWindow";
			this.Text = "DiscussionWindow";
			this.VisibleChanged += new System.EventHandler(this.DiscussionWindow_VisibleChanged);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// Displays the window if necessary, then displays the message sent to the discussion.
		/// </summary>
		/// <param name="notify">The message to show</param>
		public override void Post(NotifyEvent notify)
		{
			base.Post(notify);
		}

		/// <summary>
		/// Displays the window if necessary, then displays the message sent to the emote discussion
		/// </summary>
		/// <param name="notify">The emote message to show</param>
		public void PostEmote(NotifyEvent notify)
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
			post("(" + notify.Time.TimeOfDay + ") ", Color.Black);

			// Message
			if(notify.Source == ((LilyParent)MdiParent).Me) 
				post(notify.Source.Name + notify.Value + "\n", Color.Red);
			else 
				post(notify.Source.Name + notify.Value + "\n", Color.Blue);
		}

		/// <summary>
		/// Adds members to the member list of the discussion
		/// </summary>
		/// <param name="members">A list of members to add to the member list</param>
		public void AddMembers(string[] members)
		{
			foreach(string member in members)
			{
				if(!userList.Items.Contains(mdiParent.Database[member]))
					userList.Items.Add( mdiParent.Database[member]);
			}
			userList.Sorted = true;
		}

		/// <summary>
		/// Remove the user from the member list
		/// </summary>
		/// <param name="member">The user to remove</param>
		public void RemoveMember(IUser member)
		{
			userList.Items.Remove(member);
		}

		/// <summary>
		/// Color-codes the user list according to state
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void userList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{

			e.DrawBackground();

			SolidBrush myBrush = new SolidBrush(Color.Orange);
			if(e.Index != -1)
			{
				switch( ((IUser)userList.Items[e.Index]).State)
				{
					case States.Here:
						myBrush = new SolidBrush(Color.Black);
						break;
					case States.Away:
						myBrush = new SolidBrush(Color.Maroon);
						break;
					case States.Detached:
						myBrush = new SolidBrush(Color.Gray);
						break;
					case States.Disconnected:
						myBrush = new SolidBrush(Color.Red);
						break;
					default:
						break;
				}
			}

			if( (e.State & DrawItemState.Selected) !=0)
			{
				myBrush.Color = Color.FromArgb(myBrush.Color.ToArgb() | userList.BackColor.ToArgb());
			}

			// Handle scrollbar
			if(e.Graphics.MeasureString(userList.Items[e.Index].ToString(), e.Font).Width > userList.HorizontalExtent) 
				userList.HorizontalExtent = (int)e.Graphics.MeasureString(userList.Items[e.Index].ToString(), e.Font).Width;
            
			e.Graphics.DrawString(userList.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
			e.DrawFocusRectangle();
		}

		/// <summary>
		/// Updates blurb and state information for the user tooltip
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void userList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(userList.IndexFromPoint(e.X, e.Y) != -1)
			{
				IUser hoveredUser = (IUser)userList.Items[userList.IndexFromPoint(e.X, e.Y)];
				if(hoveredUser.Blurb == "")
					blurbTip.SetToolTip(userList, "State: " + hoveredUser.State);
				else
					blurbTip.SetToolTip(userList, hoveredUser.Blurb + "\nState: " + hoveredUser.State);
			}
			else
				blurbTip.RemoveAll();
		}

		/// <summary>
		/// Handles status changes
		/// </summary>
		/// <param name="notify">SLCP notify event</param>
		protected override void onNotify(NotifyEvent notify)
		{
			if( userList.Items.Contains( notify.Source) && notify.Notify)
			{
				if(notify.Stamp) chatArea.AppendText(notify.Time.TimeOfDay.ToString() + " ");
				switch(notify.Event)
				{
					case "away":
						post(notify.Source + " is now away\n", Color.Maroon);
						break;
					case "here":
						post(notify.Source + " is now here\n", Color.Maroon);
						break;
					case "connect":
						post(notify.Source + " has connected\n", Color.Maroon);
						break;
					case "blurb":
						post(notify.Source + "[" + ((IUser)notify.Source).Blurb + "] -> [" + notify.Value + "]\n", Color.Maroon);
						break;
					case "detach":
						post(notify.Source + " has detached " + notify.Value + "\n", Color.Maroon);
						break;
					case "attach":
						post(notify.Source + " has attached\n", Color.Maroon);
						break;
					case "quit":
						foreach(ILilyObject recip in notify.Recipients)
						{
							// See if this discussion is the one being quit
							if(recip == this.lilyObject)
							{
								post(notify.Source + " has quit\n", Color.Maroon);
								userList.Items.Remove(notify.Source);

								// If I am the one quitting...
								if(notify.Source == mdiParent.Me)
								{
									allowClose = true;
									this.lilyObject.Window = null;

										// If the window is open, keep it open so the user can still review
									if(Visible)
									{
										post("*** You have left this discussion ***\n", Color.Red);
										sendBtn.Enabled = false;
									}
										// If the window is closed, just end its life
									else
										Dispose();
								}
							}
						}
						break;
					case "disconnect":
						if(notify.Value == "idled to death")
							post(notify.Source + " has idled to death\n", Color.Maroon);
						else
							post(notify.Source + " has disconnected\n", Color.Maroon);
						break;
					case "unidle":
						post(notify.Source + " is no longer idle\n", Color.Maroon);
						break;
					case "rename":
						post(notify.Source.Name + " is now known as " + notify.Value + "\n", Color.Maroon);
						break;
					case "create":
						post(notify.Source.Name + " created " + notify.Recipients[0] + "\n", Color.Maroon);
						break;
					case "destroy":
						if(notify.Recipients[0] == this.lilyObject)
						{
							post("*** This discussion has been destroyed by " + notify.Source + " ***", Color.Red);
							sendBtn.Enabled = false;
							this.allowClose = true;
							mdiParent.Database[this.lilyObject.Handle] = null;
						}
						else
						{
							post(notify.Source.Name + " destroyed " + notify.Recipients[0] + "\n", Color.Maroon);
						}
						break;
					case "join":
						if(notify.Recipients[0] == this.lilyObject)
						{
							userList.Sorted = true;
							post(notify.Source.Name + " has joined\n", Color.Maroon);
						}
						break;
					case "retitle":
						if(notify.Recipients[0] == this.lilyObject)
							this.Text = this.lilyObject.Name + " [ " + notify.Value + " ]";

                        post(notify.Source.Name + " has changed the title of " + notify.Recipients[0].Name + " to " + notify.Value + "\n", Color.Maroon);
						break;
					case "ignore":
						if(notify.Value == "")
							post(notify.Source + " is no longer ignoring you\n", Color.Maroon);
						else
							post(notify.Source.Name + " is ignoring you " + notify.Value.TrimStart(new char[]{'{'}).TrimEnd(new char[]{'}'}) + "\n", Color.Maroon);
						break;
					case "unignore":
						post(notify.Source.Name + " is no longer ignoring you " + notify.Value + "\n", Color.Maroon);
						break;
					default:
						post("*** Unhandled event***", Color.Red);
						post("     Event: " + notify.Event + "\n", Color.Red);
						post("    Source: " + notify.Source.Name + "\n", Color.Red);
						if(notify.Recipients != null)
						{
							post("     Recip: ", Color.Red);
							foreach(ILilyObject recip in notify.Recipients)
							{
								post(recip.Name + ", ", Color.Red);
							}
							post("\n", Color.Black);
						}
						break;
				} // end case
			} // end if
			
			if(userList.Items.Contains(notify.Source))
				userList.Invalidate();
		}

		/// <summary>
		/// Shows the discussion when it's clicked in the discussion menu
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void disc_Click(object sender, System.EventArgs e)
		{
			if(!Visible) Show();
		}

		/// <summary>
		/// Disable info/memo/finger menu items if the user does not have those options
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event Arguments</param>
		private void userListMenu_Popup(object sender, System.EventArgs e)
		{
			// Disable all menu items if no user was right-clicked on
			if(userList.IndexFromPoint(userList.PointToClient(Cursor.Position)) == -1)
			{
				infoItem.Enabled = false;
				memoItem.Enabled = false;
				fingerItem.Enabled = false;
				pmItem.Enabled = false;
				ignoreItem.Enabled = false;
				notifySetItem.Enabled = false;
				return;
			}

			// Edit the menu to reflect the properties of the right-clicked user
			rightClickedUser = (IUser)userList.Items[userList.IndexFromPoint(userList.PointToClient(Cursor.Position))];
			infoItem.Enabled = rightClickedUser.Info;
			memoItem.Enabled = rightClickedUser.Memo;
			fingerItem.Enabled = rightClickedUser.Finger;
			pmItem.Enabled = true;

			/*
			 * Set ignore checks
			 */
			ignoreItem.Enabled = true;
			ignorePrivateItem.Checked = rightClickedUser.IgnoreSettings.Private;
			
			ignoreHereItem.Checked = rightClickedUser.IgnoreSettings.Public;
			if(rightClickedUser.IgnoreSettings.Exceptions.Contains(this.lilyObject))
				ignoreHereItem.Checked = !ignoreHereItem.Checked;
			
			ignoreInDiscAllItem.Checked = rightClickedUser.IgnoreSettings.Public;

			ignoreInDiscAddItem.Visible = !ignoreInDiscAllItem.Checked;
			ignoreInDiscExceptionItem.Visible = ignoreInDiscAllItem.Checked;

			/*
			 * Clear ignore exceptions
			 */
			foreach(MenuItem item in ignoreInDiscItem.MenuItems)
				if(item != this.ignoreInDiscAllItem && item != this.ignoreInDiscAddItem && item != this.ignoreInDiscSeperator && item != this.ignoreInDiscExceptionItem)
					ignoreInDiscItem.MenuItems.Remove(item);

			foreach(MenuItem item in ignoreInDiscExceptionItem.MenuItems)
				if(item != this.ignoreInDiscExceptionsAddItem && item != this.ignoreInDiscExceptionsSeperator)
					ignoreInDiscExceptionItem.MenuItems.Remove(item);

			/*
			 * Pouplate ignore exceptions
			 */
			if(rightClickedUser.IgnoreSettings.Public)
			{
				foreach(IDiscussion disc in rightClickedUser.IgnoreSettings.Exceptions)
				{
					this.ignoreInDiscExceptionItem.MenuItems.Add(new MenuItem(disc.Name));
				}
			}
			else
			{
				foreach(IDiscussion disc in rightClickedUser.IgnoreSettings.Exceptions)
				{
					this.ignoreInDiscItem.MenuItems.Add(new MenuItem(disc.Name));
				}
			}

			notifySetItem.Enabled = true;
		}

		/// <summary>
		/// Displays a user's info when the menu item is clicked
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void infoItem_Click(object sender, System.EventArgs e)
		{
			InfoDlg info = new InfoDlg(mdiParent, rightClickedUser);
			info.Show();
		}

		/// <summary>
		/// Displays a user's memo when the menu item is clicked
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void memoItem_Click(object sender, System.EventArgs e)
		{
			MemoDlg memo = new MemoDlg(mdiParent, this.rightClickedUser);
			memo.Show();
		}

		/// <summary>
		/// Displays a user's finger info when the menu item is clicked
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void fingerItem_Click(object sender, System.EventArgs e)
		{
			FingerDlg finger = new FingerDlg(mdiParent, rightClickedUser);
			finger.Show();
		}

		/// <summary>
		/// Creates a new Private Message window to the user
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void pmItem_Click(object sender, System.EventArgs e)
		{
			mdiParent.createPM(rightClickedUser);
		}

		private void DiscussionWindow_VisibleChanged(object sender, System.EventArgs e)
		{
			if(Visible) mdiParent.JoinedDiscList.ClearMsg(this.lilyObject as IDiscussion);
		}

		private void ignoreHereItem_Click(object sender, System.EventArgs e)
		{
			ignoreHereItem.Checked = !ignoreHereItem.Checked;
			if(ignoreHereItem.Checked)
			{
				// Send ignore message
				mdiParent.PostMessage(new LeafMessage("/ignore " + rightClickedUser.Name + " " + this.lilyObject.Name, "ignore",  new ProcessResponse(ignoreResponse)));
			}
			else
			{
				// Send unignore message
				mdiParent.PostMessage(new LeafMessage("/unignore " + rightClickedUser.Name + " " + this.lilyObject.Name, "ignore",  new ProcessResponse(ignoreResponse)));
			}
		}

		private void ignorePrivateItem_Click(object sender, System.EventArgs e)
		{
			// set menuitem check
			ignorePrivateItem.Checked = !ignorePrivateItem.Checked;

			// Send ignore message
			if(ignorePrivateItem.Checked)
				mdiParent.PostMessage(new LeafMessage("/ignore " + rightClickedUser.Name.Replace(' ','_'), "ignore",  new ProcessResponse(ignoreResponse)));
			else
				mdiParent.PostMessage(new LeafMessage("/unignore " + rightClickedUser.Name.Replace(' ','_'), "ignore",  new ProcessResponse(ignoreResponse)));

		}

		private void ignoreInDiscAllItem_Click(object sender, System.EventArgs e)
		{
			// Set menuitem check
			ignoreInDiscAllItem.Checked = !ignoreInDiscAllItem.Checked;

			// Send ignore message
			if(ignoreInDiscAllItem.Checked)
				mdiParent.PostMessage(new LeafMessage("/ignore " + rightClickedUser.Name.Replace(' ','_') + " public", "ignore",  new ProcessResponse(ignoreResponse)));
			else
				mdiParent.PostMessage(new LeafMessage("/unignore " + rightClickedUser.Name.Replace(' ','_') + " public", "ignore",  new ProcessResponse(ignoreResponse)));
		}

		private void ignoreInDiscAddItem_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ignoreInDiscExceptionsAddItem_Click(object sender, System.EventArgs e)
		{
		
		}

	}
}
