using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Summary description for AddServer.
	/// </summary>
	public class AddServerDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button addBtn;
		private System.Windows.Forms.TextBox nameField;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddServerDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.AcceptButton = addBtn;
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
			this.nameField = new System.Windows.Forms.TextBox();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.addBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Server Name";
			// 
			// nameField
			// 
			this.nameField.Location = new System.Drawing.Point(8, 32);
			this.nameField.Name = "nameField";
			this.nameField.Size = new System.Drawing.Size(248, 20);
			this.nameField.TabIndex = 1;
			this.nameField.Text = "";
			// 
			// cancelBtn
			// 
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(184, 64);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.TabIndex = 2;
			this.cancelBtn.Text = "Cancel";
			// 
			// addBtn
			// 
			this.addBtn.Location = new System.Drawing.Point(96, 64);
			this.addBtn.Name = "addBtn";
			this.addBtn.TabIndex = 3;
			this.addBtn.Text = "Add";
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// AddServerDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(264, 93);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.addBtn,
																		  this.cancelBtn,
																		  this.nameField,
																		  this.label1});
			this.Name = "AddServerDlg";
			this.Text = "AddServer";
			this.ResumeLayout(false);

		}
		#endregion

		public string Server
		{
			get{ return nameField.Text;}
		}

		private void addBtn_Click(object sender, System.EventArgs e)
		{
			if(nameField.Text.Trim() == "")
				MessageBox.Show("You have to enter a server name");
			else
				this.DialogResult = DialogResult.OK;
		}
	}
}
