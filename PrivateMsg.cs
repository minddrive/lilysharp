using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Private message window
	/// </summary>
	public class PrivateMsg : LilyWindow
	{
		
		private IUser user;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="user">The IUser we are private messaging with</param>
		/// <param name="parent">The parent of the window</param>
		public PrivateMsg(IUser user, LilyParent parent) : base(parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.AcceptButton = sendBtn;
			this.user = user;
			this.Text = user.Name + "[" +user.Blurb + "]";
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
			base.Dispose( disposing );
			user.Window = null;

		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 333);
			this.panel1.Size = new System.Drawing.Size(408, 24);
			this.panel1.Visible = true;
			// 
			// chatArea
			// 
			this.chatArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chatArea.Size = new System.Drawing.Size(408, 333);
			this.chatArea.Visible = true;
			// 
			// sendBtn
			// 
			this.sendBtn.Location = new System.Drawing.Point(333, 0);
			this.sendBtn.Visible = true;
			// 
			// userText
			// 
			this.userText.Size = new System.Drawing.Size(333, 20);
			this.userText.Visible = true;
			// 
			// PrivateMsg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(408, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.chatArea,
																		  this.panel1});
			this.Name = "PrivateMsg";
			this.Text = "PrivateMsg";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Send the entered text to the user
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		protected override void sendBtn_Click(object sender, System.EventArgs e)
		{
			if(userText.Text.StartsWith("/"))
				mdiParent.Out.WriteLine(userText.Text);  //send server messages directly to the server
			else
			{
				// Need to replace spaces in the user name with underscores for message sending
				mdiParent.Out.WriteLine( user.Name.Replace(' ', '_') + ";" + userText.Text);
			}
			
			userText.Clear();

		}

		/// <summary>
		/// Update the user's status
		/// </summary>
		/// <param name="notify">Event containing the update info</param>
		protected override void updateUser(NotifyEvent notify)
		{
			if(notify.Source != user) return;

			switch (notify.Event)
			{
				case "blurb":
					this.Text = user.Name + " [" + notify.Value + "]";
					post(user.Name + " [" + user.Blurb + "] --> [" + notify.Value + "]\n", Color.Maroon);
					break;
				case "away":
					post(user.Name + " is now away\n", Color.Maroon);
					break;
				case "detach":
					post(user.Name + " has detached\n", Color.Maroon);
					break;
				case "connect":
					post(user.Name + " has connected\n", Color.Maroon);
					break;
				case "here":
					post(user.Name + " is now hwere\n", Color.Maroon);
					break;
				case "attach":
					post(user.Name + " has attached\n", Color.Maroon);
					break;
				case "unidle":
					post(user.Name + " is no longer idle\n", Color.Maroon);
					break;
				case "rename":
					post(user.Name + " is now known as " + notify.Value + "\n", Color.Maroon);
					break;
				case "ignore":
					if(notify.Value == "")
						post(user.Name + " is no longer ignoring you\n", Color.Maroon);
					else
						post(user.Name + " is ignoring you " + notify.Value.TrimStart(new char[]{'{'}).TrimEnd(new char[]{'}'}) + "\n", Color.Maroon);
					break;
				default:
					break;
			}

		}
		
		/// <summary>
		/// Overrides the base class's implementation, allowing the window to always close
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		protected override void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		}
	}
}
