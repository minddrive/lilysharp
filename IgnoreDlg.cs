using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Summary description for IgnoreDlg.
	/// </summary>
	public class IgnoreDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ListBox ignoredUserList;
		private System.Windows.Forms.Button removeUserBtn;
		private System.Windows.Forms.Button addUserBtn;
		private System.Windows.Forms.CheckBox publicChk;
		private System.Windows.Forms.CheckBox privateChk;
		private System.Windows.Forms.Label discLbl;
		private System.Windows.Forms.ListBox discList;
		private System.Windows.Forms.Button addDiscBtn;
		private System.Windows.Forms.Button removeDiscBtn;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button cancelBtn;
		private Hashtable newSettings, currentSettings;
		private bool closing = false;
		private int pendingChanges;
		/// <summary>
		/// Speeds things up by reducing method calls
		/// </summary>
		private string selectedUser;
		private Ignore selectedUserIgnore;
		private System.Windows.Forms.Button applyBtn;
		private System.Windows.Forms.Splitter splitter2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public IgnoreDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			pendingChanges = 0;
			this.CancelButton = cancelBtn;

			Sock.Instance.PostMessage(new LeafMessage("/ignore", new ProcessResponse(ignoreReceived)));
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
			this.ignoredUserList = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.addUserBtn = new System.Windows.Forms.Button();
			this.removeUserBtn = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.discList = new System.Windows.Forms.ListBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.removeDiscBtn = new System.Windows.Forms.Button();
			this.addDiscBtn = new System.Windows.Forms.Button();
			this.discLbl = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.publicChk = new System.Windows.Forms.CheckBox();
			this.privateChk = new System.Windows.Forms.CheckBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.okBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.applyBtn = new System.Windows.Forms.Button();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.ignoredUserList,
																					this.panel1});
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 333);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Ignored Users";
			// 
			// ignoredUserList
			// 
			this.ignoredUserList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ignoredUserList.Location = new System.Drawing.Point(3, 16);
			this.ignoredUserList.Name = "ignoredUserList";
			this.ignoredUserList.Size = new System.Drawing.Size(258, 264);
			this.ignoredUserList.TabIndex = 0;
			this.ignoredUserList.SelectedIndexChanged += new System.EventHandler(this.ignoredUserList_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.addUserBtn,
																				 this.removeUserBtn});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.DockPadding.All = 7;
			this.panel1.Location = new System.Drawing.Point(3, 290);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(258, 40);
			this.panel1.TabIndex = 1;
			// 
			// addUserBtn
			// 
			this.addUserBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.addUserBtn.Location = new System.Drawing.Point(101, 7);
			this.addUserBtn.Name = "addUserBtn";
			this.addUserBtn.Size = new System.Drawing.Size(75, 26);
			this.addUserBtn.TabIndex = 1;
			this.addUserBtn.Text = "Add User";
			this.addUserBtn.Click += new System.EventHandler(this.addUserBtn_Click);
			// 
			// removeUserBtn
			// 
			this.removeUserBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.removeUserBtn.Enabled = false;
			this.removeUserBtn.Location = new System.Drawing.Point(176, 7);
			this.removeUserBtn.Name = "removeUserBtn";
			this.removeUserBtn.Size = new System.Drawing.Size(75, 26);
			this.removeUserBtn.TabIndex = 0;
			this.removeUserBtn.Text = "Remove";
			this.removeUserBtn.Click += new System.EventHandler(this.removeUserBtn_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.discList,
																					this.panel3,
																					this.discLbl,
																					this.panel2});
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(264, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(288, 333);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Settings";
			// 
			// discList
			// 
			this.discList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.discList.Location = new System.Drawing.Point(3, 56);
			this.discList.Name = "discList";
			this.discList.Size = new System.Drawing.Size(282, 225);
			this.discList.TabIndex = 3;
			// 
			// panel3
			// 
			this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.removeDiscBtn,
																				 this.addDiscBtn});
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.DockPadding.All = 7;
			this.panel3.Location = new System.Drawing.Point(3, 290);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(282, 40);
			this.panel3.TabIndex = 5;
			// 
			// removeDiscBtn
			// 
			this.removeDiscBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.removeDiscBtn.Enabled = false;
			this.removeDiscBtn.Location = new System.Drawing.Point(125, 7);
			this.removeDiscBtn.Name = "removeDiscBtn";
			this.removeDiscBtn.Size = new System.Drawing.Size(75, 26);
			this.removeDiscBtn.TabIndex = 1;
			this.removeDiscBtn.Text = "Remove";
			this.removeDiscBtn.Click += new System.EventHandler(this.removeDiscBtn_Click);
			// 
			// addDiscBtn
			// 
			this.addDiscBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.addDiscBtn.Enabled = false;
			this.addDiscBtn.Location = new System.Drawing.Point(200, 7);
			this.addDiscBtn.Name = "addDiscBtn";
			this.addDiscBtn.Size = new System.Drawing.Size(75, 26);
			this.addDiscBtn.TabIndex = 0;
			this.addDiscBtn.Text = "Add Disc";
			this.addDiscBtn.Click += new System.EventHandler(this.addDiscBtn_Click);
			// 
			// discLbl
			// 
			this.discLbl.Dock = System.Windows.Forms.DockStyle.Top;
			this.discLbl.Location = new System.Drawing.Point(3, 40);
			this.discLbl.Name = "discLbl";
			this.discLbl.Size = new System.Drawing.Size(282, 16);
			this.discLbl.TabIndex = 2;
			this.discLbl.Text = "In Discussion";
			// 
			// panel2
			// 
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.publicChk,
																				 this.privateChk});
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(3, 16);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(282, 24);
			this.panel2.TabIndex = 4;
			// 
			// publicChk
			// 
			this.publicChk.Enabled = false;
			this.publicChk.Location = new System.Drawing.Point(24, 0);
			this.publicChk.Name = "publicChk";
			this.publicChk.TabIndex = 0;
			this.publicChk.Text = "Public";
			this.publicChk.CheckedChanged += new System.EventHandler(this.publicChk_CheckedChanged);
			// 
			// privateChk
			// 
			this.privateChk.Enabled = false;
			this.privateChk.Location = new System.Drawing.Point(144, 0);
			this.privateChk.Name = "privateChk";
			this.privateChk.TabIndex = 1;
			this.privateChk.Text = "Private";
			this.privateChk.CheckedChanged += new System.EventHandler(this.privateChk_CheckedChanged);
			// 
			// panel4
			// 
			this.panel4.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.okBtn,
																				 this.cancelBtn,
																				 this.applyBtn});
			this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel4.Location = new System.Drawing.Point(0, 333);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(552, 24);
			this.panel4.TabIndex = 4;
			// 
			// okBtn
			// 
			this.okBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.okBtn.Location = new System.Drawing.Point(322, 0);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(75, 24);
			this.okBtn.TabIndex = 0;
			this.okBtn.Text = "OK";
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.cancelBtn.Location = new System.Drawing.Point(397, 0);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(80, 24);
			this.cancelBtn.TabIndex = 1;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// applyBtn
			// 
			this.applyBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.applyBtn.Enabled = false;
			this.applyBtn.Location = new System.Drawing.Point(477, 0);
			this.applyBtn.Name = "applyBtn";
			this.applyBtn.Size = new System.Drawing.Size(75, 24);
			this.applyBtn.TabIndex = 2;
			this.applyBtn.Text = "Apply";
			// 
			// splitter2
			// 
			this.splitter2.Location = new System.Drawing.Point(264, 0);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(3, 333);
			this.splitter2.TabIndex = 5;
			this.splitter2.TabStop = false;
			// 
			// IgnoreDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.splitter2,
																		  this.groupBox2,
																		  this.groupBox1,
																		  this.panel4});
			this.Name = "IgnoreDlg";
			this.Text = "Ignore Settings";
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ignoredUserList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Don't do anything if noone is selected
			if(ignoredUserList.SelectedIndex == -1)
				return;

			// get user info
			selectedUser = ignoredUserList.SelectedItem as string;
			selectedUserIgnore = newSettings[selectedUser] as Ignore;

			// Set ignore checks
			privateChk.Checked = selectedUserIgnore.Private;
			publicChk.Checked  = selectedUserIgnore.Public;

			// populate discussion list
			discList.Items.Clear();
			discLbl.Text = selectedUserIgnore.Public ? "Except in Disc" : "In Disc";
			foreach(IDiscussion disc in selectedUserIgnore.Exceptions)
			{
				discList.Items.Add(disc);
			}

			if(discList.Items.Count == 0)
				removeDiscBtn.Enabled = false;
			else
			{
				discList.SelectedIndex = 0;
				removeDiscBtn.Enabled = true;
			}
		}

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			// Don't close the form yet, need to make sure all ignore changes have been sent and verified
			//Hide();
			//closing = true;
			// TODO: Add ignore update code here.
			Close();
		
		}

		private void cancelBtn_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void publicChk_CheckedChanged(object sender, System.EventArgs e)
		{
			selectedUserIgnore.Public = publicChk.Checked;
			discLbl.Text = selectedUserIgnore.Public ? "Except in Disc" : "In Disc";
		}

		private void privateChk_CheckedChanged(object sender, System.EventArgs e)
		{
			selectedUserIgnore.Private = privateChk.Checked;
		}


		private void ignoreReceived(LeafMessage msg)
		{
			currentSettings = Util.ParseIgnore(msg.Response);
			newSettings = currentSettings.Clone() as Hashtable;
			foreach(DictionaryEntry entry in currentSettings)
			{
				ignoredUserList.Items.Add(entry.Key);
			}

			if(ignoredUserList.Items.Count > 0)
			{
				ignoredUserList.SelectedIndex = 0;
				removeUserBtn.Enabled = true;
				privateChk.Enabled = true;
				publicChk.Enabled  = true;
				addDiscBtn.Enabled = true;
			}
		}

		private void removeUserBtn_Click(object sender, System.EventArgs e)
		{
			selectedUserIgnore.Public = false;
			selectedUserIgnore.Private = false;
			selectedUserIgnore.Exceptions.Clear();
			ignoredUserList.Items.Remove(ignoredUserList.SelectedItem);

			if(ignoredUserList.Items.Count > 0)
			{
				ignoredUserList.SelectedIndex = 0;
			}
			else
			{
				removeUserBtn.Enabled = false;
				publicChk.Enabled = false;
				privateChk.Enabled = false;
				publicChk.Checked = false;
				privateChk.Checked = false;
				addDiscBtn.Enabled = false;
				removeDiscBtn.Enabled = false;
				discList.Items.Clear();
			}

		}

		private void addUserBtn_Click(object sender, System.EventArgs e)
		{
			GetObjectDlg addUser = new GetObjectDlg(GetObjectDlg.ObjType.User);
			
			if(addUser.ShowDialog() == DialogResult.OK)
			{
				ignoredUserList.Items.Add(addUser.User.Name);
				newSettings[addUser.User.Name] = new Ignore(false, false);
				ignoredUserList.SelectedItem = addUser.User.Name;

				if(!removeUserBtn.Enabled) removeUserBtn.Enabled = true;
			}
			else
			{
				return;
			}
		}

		private void removeDiscBtn_Click(object sender, System.EventArgs e)
		{
			selectedUserIgnore.Exceptions.Remove(discList.SelectedItem);
			discList.Items.Remove(discList.SelectedItem);

			if(discList.Items.Count > 0)
			{
				discList.SelectedIndex = 0;
			}
			else
			{
				removeDiscBtn.Enabled = false;
			}
		}

		private void addDiscBtn_Click(object sender, System.EventArgs e)
		{
			GetObjectDlg addDisc = new GetObjectDlg(GetObjectDlg.ObjType.Discussion);

			if(addDisc.ShowDialog() == DialogResult.OK)
			{
				discList.Items.Add(addDisc.Discussion);
				selectedUserIgnore.Exceptions.Add(addDisc.Discussion);
				discList.SelectedItem = addDisc.Discussion;

				if(!removeDiscBtn.Enabled) removeDiscBtn.Enabled = true;
			}
			else
			{
				return;
			}
		}

	}
}
