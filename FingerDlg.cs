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
	/// Summary description for FingerDlg.
	/// </summary>
	public class FingerDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label nameLbl;
		private System.Windows.Forms.LinkLabel emailLbl;
		private System.Windows.Forms.LinkLabel webLbl;
		private System.Windows.Forms.Button infoBtn;
		private IUser user;
		private System.Windows.Forms.Label pseudoLbl;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FingerDlg(IUser user)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.user = user;
			this.Text = user.Name + "'s Finger Information";

			infoBtn.Enabled = user.Info;
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.webLbl = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.emailLbl = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.nameLbl = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pseudoLbl = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.infoBtn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.panel4,
																					this.panel3,
																					this.panel2,
																					this.panel1});
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 120);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Finger Information";
			// 
			// panel4
			// 
			this.panel4.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.webLbl,
																				 this.label4});
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(3, 88);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(362, 24);
			this.panel4.TabIndex = 2;
			// 
			// webLbl
			// 
			this.webLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webLbl.Enabled = false;
			this.webLbl.Location = new System.Drawing.Point(64, 0);
			this.webLbl.Name = "webLbl";
			this.webLbl.Size = new System.Drawing.Size(298, 24);
			this.webLbl.TabIndex = 7;
			this.webLbl.TabStop = true;
			this.webLbl.Text = "retrieving...";
			this.webLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.webLbl_LinkClicked);
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Left;
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 24);
			this.label4.TabIndex = 3;
			this.label4.Text = "Web Page:";
			// 
			// panel3
			// 
			this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.emailLbl,
																				 this.label3});
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 64);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(362, 24);
			this.panel3.TabIndex = 2;
			// 
			// emailLbl
			// 
			this.emailLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.emailLbl.Enabled = false;
			this.emailLbl.Location = new System.Drawing.Point(64, 0);
			this.emailLbl.Name = "emailLbl";
			this.emailLbl.Size = new System.Drawing.Size(298, 24);
			this.emailLbl.TabIndex = 6;
			this.emailLbl.TabStop = true;
			this.emailLbl.Text = "retrieving...";
			this.emailLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.emailLbl_LinkClicked);
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Left;
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 24);
			this.label3.TabIndex = 2;
			this.label3.Text = "Email:";
			// 
			// panel2
			// 
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.nameLbl,
																				 this.label2});
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(3, 40);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(362, 24);
			this.panel2.TabIndex = 2;
			// 
			// nameLbl
			// 
			this.nameLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nameLbl.Location = new System.Drawing.Point(64, 0);
			this.nameLbl.Name = "nameLbl";
			this.nameLbl.Size = new System.Drawing.Size(298, 24);
			this.nameLbl.TabIndex = 5;
			this.nameLbl.Text = "retrieving...";
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Left;
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 24);
			this.label2.TabIndex = 1;
			this.label2.Text = "Name:";
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.pseudoLbl,
																				 this.label1});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(362, 24);
			this.panel1.TabIndex = 2;
			// 
			// pseudoLbl
			// 
			this.pseudoLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pseudoLbl.Location = new System.Drawing.Point(64, 0);
			this.pseudoLbl.Name = "pseudoLbl";
			this.pseudoLbl.Size = new System.Drawing.Size(298, 24);
			this.pseudoLbl.TabIndex = 4;
			this.pseudoLbl.Text = "retrieving...";
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Pseudo:";
			// 
			// infoBtn
			// 
			this.infoBtn.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.infoBtn.Enabled = false;
			this.infoBtn.Location = new System.Drawing.Point(288, 128);
			this.infoBtn.Name = "infoBtn";
			this.infoBtn.TabIndex = 1;
			this.infoBtn.Text = "Info";
			this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
			// 
			// FingerDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 157);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.infoBtn,
																		  this.groupBox1});
			this.Name = "FingerDlg";
			this.Text = "Finger Information";
			this.Load += new System.EventHandler(this.FingerDlg_Load);
			this.groupBox1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void ProcessResponse(LeafMessage msg)
		{
			// Clear lables (erases "retrieving...")
			pseudoLbl.Text = "";
			nameLbl.Text = "";
			emailLbl.Text = "";
			webLbl.Text = "";

			// Parse finger information
			foreach(string line in msg.Response.Split(new Char[] {'\n'}))
			{
				if(line.StartsWith("* Pseudo:"))
				{
					pseudoLbl.Text = line.Substring("* Pseudo: ".Length);
				}

				else if(line.StartsWith("* Name:"))
				{
					nameLbl.Text = line.Substring("* Name: ".Length);
				}

				else if(line.StartsWith("* Email:"))
				{
					// Initialize the LinkLabel
					emailLbl.Text = line.Substring("* Email: ".Length);
					emailLbl.Links.RemoveAt(0);
					emailLbl.Enabled = true;

					// Search for and set email links
					foreach(Match match in Regex.Matches(emailLbl.Text, @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)")) 
					{
						emailLbl.Links.Add(match.Index, match.Length, match.Value);
					}
				}

				else if(line.StartsWith("* Web Page:"))
				{
					// Initialize link label
					webLbl.Text = line.Substring("* Web Page: ".Length);
					webLbl.Links.RemoveAt(0);
					webLbl.Enabled = true;

					// Search for and set links
					foreach(Match match in Regex.Matches(webLbl.Text, @"((https?://)|(www\.))[^\s]+"))
					{
						webLbl.Links.Add(match.Index, match.Length, match.Value);
					}
	
					// If there are no links found, it is probalby formatted in a non-standard way
					//   Since we are expecting a web page, assume the entire text is the link
					//   We need to append http:// as we assme that it has merely been left off
					if(webLbl.Links.Count == 0)
						webLbl.Links.Add(0, webLbl.Text.Length, "http://" + webLbl.Text);
				}
				else if(line.StartsWith("* \"/info"))
				{
					// Safe to ignore this; the info button is enabled in the constructor
				}
			}
		}

		private void infoBtn_Click(object sender, System.EventArgs e)
		{
			InfoDlg info = new InfoDlg(user);
			info.Show();
		}

		private void emailLbl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo(e.Link.LinkData as string);
				startInfo.UseShellExecute = true;
				System.Diagnostics.Process.Start(startInfo);
			}
			catch(Win32Exception)
			{
				MessageBox.Show("Error starting default email client.\nPlease make sure you have a default email client configured.", "Unable to compose message");
			}
		}

		private void webLbl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo(e.Link.LinkData as string);
				startInfo.UseShellExecute = true;
				System.Diagnostics.Process.Start(startInfo);
			}
			catch(Win32Exception)
			{
				MessageBox.Show("Error displaying web page: " + e.Link.LinkData + "\nPlease make sure you have a default broswer configured.", "Unable to open page");
			}
		}

		private void FingerDlg_Load(object sender, System.EventArgs e)
		{
			Sock.Instance.PostMessage(new LeafMessage("/finger " + user.Name.Replace(' ','_'),  new ProcessResponse(this.ProcessResponse)));
		}

	}
}
