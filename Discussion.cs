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
	public class DiscussionWindow : LilyWindow, ILeafCmd
	{
		private System.Windows.Forms.ListBox userList;
		private System.Windows.Forms.Splitter splitter1;
		
		private IDiscussion discussion;
		private ToolTip blurbTip;
		private IUser rightClickedUser;
		private System.Windows.Forms.ContextMenu userListMenu;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem infoItem;
		private System.Windows.Forms.MenuItem memoItem;
		private System.Windows.Forms.MenuItem fingerItem;
		private System.Windows.Forms.MenuItem pmItem;

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

			this.discussion = disc;
			this.Text = disc.Name + " [ " + disc.Title + " ]";

			blurbTip = new ToolTip();
			allowClose = false;
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
			
			discussion.Window = null;
			base.Dispose( disposing );
			
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.userList = new System.Windows.Forms.ListBox();
			this.userListMenu = new System.Windows.Forms.ContextMenu();
			this.infoItem = new System.Windows.Forms.MenuItem();
			this.memoItem = new System.Windows.Forms.MenuItem();
			this.fingerItem = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.pmItem = new System.Windows.Forms.MenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 261);
			this.panel1.Size = new System.Drawing.Size(472, 24);
			this.panel1.Visible = true;
			// 
			// chatArea
			// 
			this.chatArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chatArea.Size = new System.Drawing.Size(384, 261);
			this.chatArea.Visible = true;
			this.chatArea.VisibleChanged += new System.EventHandler(this.chatArea_VisibleChanged);
			// 
			// sendBtn
			// 
			this.sendBtn.Location = new System.Drawing.Point(397, 0);
			this.sendBtn.Visible = true;
			// 
			// userText
			// 
			this.userText.Size = new System.Drawing.Size(397, 20);
			this.userText.Visible = true;
			// 
			// userList
			// 
			this.userList.ContextMenu = this.userListMenu;
			this.userList.Dock = System.Windows.Forms.DockStyle.Right;
			this.userList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.userList.HorizontalScrollbar = true;
			this.userList.IntegralHeight = false;
			this.userList.Location = new System.Drawing.Point(384, 0);
			this.userList.Name = "userList";
			this.userList.Size = new System.Drawing.Size(88, 261);
			this.userList.TabIndex = 2;
			this.userList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.userList_MouseMove);
			this.userList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.userList_DrawItem);
			// 
			// userListMenu
			// 
			this.userListMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.infoItem,
																						 this.memoItem,
																						 this.fingerItem,
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
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "-";
			// 
			// pmItem
			// 
			this.pmItem.Index = 4;
			this.pmItem.Text = "Private Message";
			this.pmItem.Click += new System.EventHandler(this.pmItem_Click);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(381, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 261);
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			// 
			// IDiscussionWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 285);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.splitter1,
																		  this.chatArea,
																		  this.userList,
																		  this.panel1});
			this.Name = "IDiscussionWindow";
			this.Text = "IDiscussionWindow";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Sends the message to the discussion
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		protected override void sendBtn_Click(object sender, System.EventArgs e)
		{
			if(userText.Text.StartsWith("/"))
			{
				LeafMessage msg = new LeafMessage(userText.Text, "userCmd", this);
				mdiParent.PostMessage(msg);

				//	mdiParent.Out.WriteLine(userText.Text);
			}
				/*
				 * TODO: Check that the sender is a valid user name.  If not, just post the message
				 */
			else if(Regex.Match(userText.Text, @"^[^;:]*[;:]").Success)
			{
				LeafMessage msg = new LeafMessage(userText.Text, "msgToDest", this);
				mdiParent.PostMessage(msg);
			}
			else if(userText.Text.ToUpper() == "&RTF")
				Post(chatArea.Rtf);
			else
			{
				// Need to replace spaces in the name with underscores, or else lily will think the line ends early
				mdiParent.Out.WriteLine(discussion.Name.Replace(' ', '_') + ";" + userText.Text);
			}
			
			userText.Clear();
		}

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

			Brush myBrush = Brushes.Orange;
			if(e.Index != -1)
			{
				switch( ((IUser)userList.Items[e.Index]).State)
				{
					case States.Here:
						myBrush = Brushes.Black;
						break;
					case States.Away:
						myBrush = Brushes.Maroon;
						break;
					case States.Detached:
						myBrush = Brushes.Gray;
						break;
					case States.Disconnected:
						myBrush = Brushes.Red;
						break;
					default:
						break;
				}
			}
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
				IUser hoveredIUser = (IUser)userList.Items[userList.IndexFromPoint(e.X, e.Y)];
				if(hoveredIUser.Blurb == "")
					blurbTip.SetToolTip(userList, "State: " + hoveredIUser.State);
				else
					blurbTip.SetToolTip(userList, hoveredIUser.Blurb + "\nState: " + hoveredIUser.State);
			}
			else
				blurbTip.RemoveAll();
		}

		/// <summary>
		/// Handles status changes
		/// </summary>
		/// <param name="notify">SLCP notify event</param>
		protected override void updateUser(NotifyEvent notify)
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
							if(recip == this.discussion)
							{
								post(notify.Source + " has quit\n", Color.Maroon);
								userList.Items.Remove(notify.Source);

								// If I am the one quitting...
								if(notify.Source == mdiParent.Me)
								{
									allowClose = true;
									discussion.Window = null;

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
						if(notify.Recipients[0] == this.discussion)
						{
							post("*** This discussion has been destroyed by " + notify.Source + " ***", Color.Red);
							sendBtn.Enabled = false;
							this.allowClose = true;
							mdiParent.Database[discussion.Handle] = null;
						}
						else
						{
							post(notify.Source.Name + " destroyed " + notify.Recipients[0] + "\n", Color.Maroon);
						}
						break;
					case "join":
						if(notify.Recipients[0] == this.discussion)
						{
							userList.Sorted = true;
							post(notify.Source.Name + " has joined\n", Color.Maroon);
						}
						break;
					case "retitle":
						if(notify.Recipients[0] == this.discussion)
							this.Text = discussion.Name + " [ " + notify.Value + " ]";

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

		public void ProcessResponse(LeafMessage msg)
		{
			if(msg.Tag == "userCmd")
			{
			
				chatArea.SelectionFont = new Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				chatArea.SelectionColor = Color.Gray;

				chatArea.AppendText(msg.Response);

				chatArea.SelectionFont = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				chatArea.SelectionColor = Color.Black;
			}
			else if(msg.Tag == "msgToDest")
			{
				chatArea.SelectionColor = Color.DarkGray;
				chatArea.AppendText(msg.Response);
			}

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
				return;
			}

			// Edit the menu to reflect the properties of the right-clicked user
			rightClickedUser = (IUser)userList.Items[userList.IndexFromPoint(userList.PointToClient(Cursor.Position))];
			infoItem.Enabled = rightClickedUser.Info;
			memoItem.Enabled = rightClickedUser.Memo;
			fingerItem.Enabled = rightClickedUser.Finger;
			pmItem.Enabled = true;
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
			MessageBox.Show("Implement me!");
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

		/// <summary>
		/// Indicates all unseen messages have been shown
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void chatArea_VisibleChanged(object sender, System.EventArgs e)
		{
			if(Visible) mdiParent.JoinedDiscList.ClearMsg(this.discussion);
		}
	
	}
}
