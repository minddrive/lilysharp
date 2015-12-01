using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Summary description for AddUserDlg.
	/// </summary>
	public class GetObjectDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button browseBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.TextBox userName;
		private ObjType type;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public enum ObjType{User, Discussion, Both};

		public GetObjectDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			type = ObjType.Both;
			this.Name = "Get Lily Object";
		}

		public GetObjectDlg(ObjType oType)
		{
			InitializeComponent();

			type = oType;
			this.Name = "Get " + type;
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
			this.userName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.browseBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// userName
			// 
			this.userName.Location = new System.Drawing.Point(8, 32);
			this.userName.Name = "userName";
			this.userName.Size = new System.Drawing.Size(136, 20);
			this.userName.TabIndex = 0;
			this.userName.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Name";
			// 
			// browseBtn
			// 
			this.browseBtn.Location = new System.Drawing.Point(152, 32);
			this.browseBtn.Name = "browseBtn";
			this.browseBtn.TabIndex = 2;
			this.browseBtn.Text = "Browse...";
			// 
			// cancelBtn
			// 
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(176, 72);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.TabIndex = 3;
			this.cancelBtn.Text = "Cancel";
			// 
			// okBtn
			// 
			this.okBtn.Location = new System.Drawing.Point(88, 72);
			this.okBtn.Name = "okBtn";
			this.okBtn.TabIndex = 4;
			this.okBtn.Text = "OK";
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// GetObjectDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(256, 101);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.okBtn,
																		  this.cancelBtn,
																		  this.browseBtn,
																		  this.label1,
																		  this.userName});
			this.MaximizeBox = false;
			this.Name = "GetObjectDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "AddUserDlg";
			this.ResumeLayout(false);

		}
		#endregion

		public IUser User
		{
			get{ return Util.Database.GetByName(userName.Text) as IUser;}
		}

		public IDiscussion Discussion
		{
			get{ return Util.Database.GetByName(userName.Text) as IDiscussion;}
		}

		public ILilyObject LilyObj
		{
			get{ return Util.Database.GetByName(userName.Text);}
		}

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			ILilyObject obj = Util.Database.GetByName(userName.Text);
			
			// Check existence
			if(obj == null)
			{
				MessageBox.Show("Object not found in Database.\nPlease check your spelling and try again, or use the Browse button to select from a list.", "Object not found");
				return;
			}

			// Check type
			switch(type)
			{
				case ObjType.User:
					if(!(obj is IUser))
					{
						MessageBox.Show(userName.Text + " is not a user.\nPlease check your spelling and try again, or use the Browse button to select from a list.", "Invalid Username");
						return;
					}
					break;
				case ObjType.Discussion:
					if(!(obj is IDiscussion))
					{
						MessageBox.Show(userName.Text + " is not a discussion.\nPlease check your spelling and try again, or use the Browse button to select from a list.", "Invalid Discussion Name");
						return;
					}
					break;
				default:
					break;
			}

			// Return if everything is ok
			this.DialogResult = DialogResult.OK;

		}
	}
}
