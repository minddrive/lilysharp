using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Displays a user's info
	/// </summary>
	public class InfoDlg : System.Windows.Forms.Form, ILeafCmd
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button closeBtn;
		private System.Windows.Forms.RichTextBox infoBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InfoDlg(LilyParent parent, ILilyObject infoSource)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Text = infoSource.Name + "'s info";
			LeafMessage msg = new LeafMessage("/info \"" + infoSource.Name + "\"", this);
			parent.PostMessage(msg);
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
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.closeBtn = new System.Windows.Forms.Button();
			this.infoBox = new System.Windows.Forms.RichTextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.closeBtn});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 237);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(440, 24);
			this.panel1.TabIndex = 0;
			// 
			// closeBtn
			// 
			this.closeBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.closeBtn.Location = new System.Drawing.Point(365, 0);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(75, 24);
			this.closeBtn.TabIndex = 0;
			this.closeBtn.Text = "Close";
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// infoBox
			// 
			this.infoBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.infoBox.Name = "infoBox";
			this.infoBox.ReadOnly = true;
			this.infoBox.Size = new System.Drawing.Size(440, 237);
			this.infoBox.TabIndex = 1;
			this.infoBox.Text = "Retrieving info...";
			this.infoBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.infoBox_LinkClicked);
			// 
			// InfoDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 261);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.infoBox,
																		  this.panel1});
			this.Name = "InfoDlg";
			this.Text = "InfoDlg";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		/// <summary>
		/// Necessary for the ILeafCmd interface.  Retrieves the user's blurb, and adds it to the text area
		/// </summary>
		/// <param name="msg">Message to process the response to</param>
		public void ProcessResponse(LeafMessage msg)
		{
			infoBox.Text = msg.Response.Replace("\n*","\n").Remove(0,1);
		}

		/// <summary>
		/// Closes the dialog
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void closeBtn_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Open the link in the default application
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void infoBox_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
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
	}
}
