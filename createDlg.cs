using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Dialog for creating a discussion
	/// </summary>
	public class CreateDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox nameField;
		private System.Windows.Forms.TextBox titleField;
		private System.Windows.Forms.CheckBox emoteBox;
		private System.Windows.Forms.CheckBox privateBox;
		private System.Windows.Forms.CheckBox moderatedBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button createBtn;
		private System.Windows.Forms.Button cancelBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="parent">This dialog's parent</param>
		public CreateDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.AcceptButton = createBtn;
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.nameField = new System.Windows.Forms.TextBox();
			this.titleField = new System.Windows.Forms.TextBox();
			this.emoteBox = new System.Windows.Forms.CheckBox();
			this.privateBox = new System.Windows.Forms.CheckBox();
			this.moderatedBox = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.createBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Title";
			// 
			// nameField
			// 
			this.nameField.Location = new System.Drawing.Point(64, 24);
			this.nameField.Name = "nameField";
			this.nameField.Size = new System.Drawing.Size(112, 20);
			this.nameField.TabIndex = 2;
			this.nameField.Text = "";
			// 
			// titleField
			// 
			this.titleField.Location = new System.Drawing.Point(64, 56);
			this.titleField.Name = "titleField";
			this.titleField.Size = new System.Drawing.Size(112, 20);
			this.titleField.TabIndex = 3;
			this.titleField.Text = "";
			// 
			// emoteBox
			// 
			this.emoteBox.Location = new System.Drawing.Point(32, 24);
			this.emoteBox.Name = "emoteBox";
			this.emoteBox.TabIndex = 4;
			this.emoteBox.Text = "Emote";
			// 
			// privateBox
			// 
			this.privateBox.Location = new System.Drawing.Point(32, 48);
			this.privateBox.Name = "privateBox";
			this.privateBox.TabIndex = 5;
			this.privateBox.Text = "Private";
			// 
			// moderatedBox
			// 
			this.moderatedBox.Location = new System.Drawing.Point(32, 72);
			this.moderatedBox.Name = "moderatedBox";
			this.moderatedBox.TabIndex = 6;
			this.moderatedBox.Text = "Moderated";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.nameField,
																					this.label1,
																					this.titleField,
																					this.label2});
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 112);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "IDiscussion Information";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.privateBox,
																					this.emoteBox,
																					this.moderatedBox});
			this.groupBox2.Location = new System.Drawing.Point(224, 16);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(152, 112);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Options";
			// 
			// createBtn
			// 
			this.createBtn.Location = new System.Drawing.Point(208, 136);
			this.createBtn.Name = "createBtn";
			this.createBtn.TabIndex = 9;
			this.createBtn.Text = "Create";
			this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.CausesValidation = false;
			this.cancelBtn.Location = new System.Drawing.Point(296, 136);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.TabIndex = 10;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// CreateDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(392, 173);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cancelBtn,
																		  this.createBtn,
																		  this.groupBox2,
																		  this.groupBox1});
			this.Name = "CreateDlg";
			this.Text = "Create IDiscussion";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// Processes success/failure responses to the create message
		/// </summary>
		/// <param name="msg"></param>
		public void ProcessResponse(LeafMessage msg)
		{
			if(msg.Response == "")  // Creation was a success
			{
				MessageBox.Show("Discussion " + nameField.Text + " has been created", "Create Succeded");
				Close();
			}
			else
			{
				MessageBox.Show(msg.Response, "Creation Failed");
			}
		}

		/// <summary>
		/// Cancels discussion creation
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void cancelBtn_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Makes sure all necessary data is there, forms the create discussion message, then dispatches it
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void createBtn_Click(object sender, System.EventArgs e)
		{
			/*
			 * Perform data validation
			 */
			if(nameField.Text.Trim() == "")
			{
				MessageBox.Show("The discussion needs a name", "Input Error");
				return;
			}
			if(nameField.Text.Trim() == "")
			{
				MessageBox.Show("The discussion has to have a title", "Input Error");
				return;
			}

			/*
			 * Form the create message
			 */
			string createStr = "/create " + nameField.Text + " \"" + titleField.Text + "\" ";
			if(emoteBox.Checked)
				createStr += "emote,";
			if(privateBox.Checked)
				createStr += "private,";
			if(moderatedBox.Checked)
				createStr += "moderated";

			createStr = createStr.TrimEnd(new char[] {','}); //Trim a trailing comma if the disc isn't moderated, but is emote or private
			LeafMessage msg = new LeafMessage(createStr, new ProcessResponse(this.ProcessResponse));
			//parent.PostMessage(msg);
			Sock.Instance.PostMessage(msg);
		}
	
	}
}
