using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class SelectName : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox userNameBox;
		private System.Windows.Forms.Button OkBtn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cancelBtn;
		private string name;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SelectName(string nameList)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.userNameBox.Items.AddRange(nameList.Split( new char[] {','} ) );
			this.userNameBox.SelectedIndex = 0;
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
			this.userNameBox = new System.Windows.Forms.ComboBox();
			this.OkBtn = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// userNameBox
			// 
			this.userNameBox.Location = new System.Drawing.Point(24, 24);
			this.userNameBox.Name = "userNameBox";
			this.userNameBox.Size = new System.Drawing.Size(121, 21);
			this.userNameBox.TabIndex = 1;
			// 
			// OkBtn
			// 
			this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkBtn.Location = new System.Drawing.Point(112, 88);
			this.OkBtn.Name = "OkBtn";
			this.OkBtn.TabIndex = 2;
			this.OkBtn.Text = "OK";
			this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.userNameBox});
			this.groupBox1.Location = new System.Drawing.Point(16, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(168, 64);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Select a Name";
			// 
			// cancelBtn
			// 
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(16, 88);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.TabIndex = 4;
			this.cancelBtn.Text = "Cancel";
			// 
			// SelectName
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(208, 133);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cancelBtn,
																		  this.groupBox1,
																		  this.OkBtn});
			this.Name = "SelectName";
			this.Text = "User Name Selection";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OkBtn_Click(object sender, System.EventArgs e)
		{
			name = (string) this.userNameBox.Items[userNameBox.SelectedIndex];
		}
	
		public string UserName
		{
			get
			{ return name; }
		}
	}
}
