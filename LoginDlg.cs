using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{

	/// <summary>
	/// Dialog prompting for the username and password if the inital ones are invalid
	/// </summary>
	public class LoginDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox userName;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		public LoginDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.AcceptButton = okBtn;
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
			this.userName = new System.Windows.Forms.TextBox();
			this.password = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Username";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Password";
			// 
			// userName
			// 
			this.userName.Location = new System.Drawing.Point(80, 32);
			this.userName.Name = "userName";
			this.userName.Size = new System.Drawing.Size(152, 20);
			this.userName.TabIndex = 2;
			this.userName.Text = "";
			// 
			// password
			// 
			this.password.Location = new System.Drawing.Point(80, 72);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(152, 20);
			this.password.TabIndex = 3;
			this.password.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.password,
																					this.userName,
																					this.label1,
																					this.label2});
			this.groupBox2.Location = new System.Drawing.Point(8, 16);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(264, 112);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "User Information";
			// 
			// cancelBtn
			// 
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(192, 136);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.TabIndex = 7;
			this.cancelBtn.Text = "Cancel";
			// 
			// okBtn
			// 
			this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okBtn.Location = new System.Drawing.Point(96, 136);
			this.okBtn.Name = "okBtn";
			this.okBtn.TabIndex = 5;
			this.okBtn.Text = "OK";
			// 
			// LoginDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(296, 173);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox2,
																		  this.cancelBtn,
																		  this.okBtn});
			this.Name = "LoginDlg";
			this.Text = "Login";
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	
		/// <summary>
		/// Allows access to the user name
		/// </summary>
		/// <value>Allows access to the user name</value>
		public string UserName
		{
			get { return userName.Text;}
		}

		/// <summary>
		/// Allows access to the user's password
		/// </summary>
		/// <value>Allows access to the user's password</value>
		public string Password
		{
			get { return password.Text;}
		}
	
		/// <summary>
		/// Try to focus the user name so the user doesn't have to tab to the textbox
		/// </summary>
		/// <returns></returns>
		public new DialogResult ShowDialog()
		{
			userName.Focus();
			return base.ShowDialog();
		}
	}
}
