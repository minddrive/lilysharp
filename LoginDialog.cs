using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Server selection and username/password dialog
	/// </summary>
	public class LoginDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox nameField;
		private System.Windows.Forms.TextBox passField;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox serverBox;
		private System.Windows.Forms.TextBox portField;
		private System.Windows.Forms.Button addServerBtn;
		private System.Windows.Forms.Button connectBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox blurbField;
		private bool loginValid = true;
		private bool blurbValid = true;
		private System.Windows.Forms.Button removeBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		public LoginDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			serverBox.Items.Add("rpi.lily.org");
			serverBox.Items.Add("santropez.netel.rpi.edu");
			serverBox.Items.Add("keep-talking.swapspace.com");
			serverBox.SelectedIndex = 0;

			this.AcceptButton = connectBtn;
		}

		/// <summary>
		/// A warpper to the parent class to reduce casts
		/// </summary>
		/// <value>A warpper to the parent class to reduce casts</value>
		private LilyParent mdiParent
		{
			get{ return (LilyParent)MdiParent; }
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
			this.blurbField = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.passField = new System.Windows.Forms.TextBox();
			this.nameField = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.removeBtn = new System.Windows.Forms.Button();
			this.addServerBtn = new System.Windows.Forms.Button();
			this.portField = new System.Windows.Forms.TextBox();
			this.serverBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.connectBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.blurbField,
																					this.label5,
																					this.passField,
																					this.nameField,
																					this.label2,
																					this.label1});
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(232, 120);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "IUser Information";
			// 
			// blurbField
			// 
			this.blurbField.Location = new System.Drawing.Point(88, 88);
			this.blurbField.Name = "blurbField";
			this.blurbField.Size = new System.Drawing.Size(120, 20);
			this.blurbField.TabIndex = 3;
			this.blurbField.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 23);
			this.label5.TabIndex = 2;
			this.label5.Text = "Blurb";
			// 
			// passField
			// 
			this.passField.Location = new System.Drawing.Point(88, 56);
			this.passField.Name = "passField";
			this.passField.PasswordChar = '*';
			this.passField.Size = new System.Drawing.Size(120, 20);
			this.passField.TabIndex = 1;
			this.passField.Text = "";
			// 
			// nameField
			// 
			this.nameField.Location = new System.Drawing.Point(88, 24);
			this.nameField.Name = "nameField";
			this.nameField.Size = new System.Drawing.Size(120, 20);
			this.nameField.TabIndex = 0;
			this.nameField.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Password";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Username";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.removeBtn,
																					this.addServerBtn,
																					this.portField,
																					this.serverBox,
																					this.label4,
																					this.label3});
			this.groupBox2.Location = new System.Drawing.Point(248, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(320, 96);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Connect to...";
			// 
			// removeBtn
			// 
			this.removeBtn.Location = new System.Drawing.Point(208, 64);
			this.removeBtn.Name = "removeBtn";
			this.removeBtn.Size = new System.Drawing.Size(96, 23);
			this.removeBtn.TabIndex = 7;
			this.removeBtn.Text = "Remove Server";
			this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
			// 
			// addServerBtn
			// 
			this.addServerBtn.Location = new System.Drawing.Point(120, 64);
			this.addServerBtn.Name = "addServerBtn";
			this.addServerBtn.TabIndex = 6;
			this.addServerBtn.Text = "Add Server";
			this.addServerBtn.Click += new System.EventHandler(this.addServerBtn_Click);
			// 
			// portField
			// 
			this.portField.Location = new System.Drawing.Point(56, 64);
			this.portField.Name = "portField";
			this.portField.Size = new System.Drawing.Size(48, 20);
			this.portField.TabIndex = 3;
			this.portField.Text = "7777";
			// 
			// serverBox
			// 
			this.serverBox.Location = new System.Drawing.Point(56, 24);
			this.serverBox.Name = "serverBox";
			this.serverBox.Size = new System.Drawing.Size(248, 21);
			this.serverBox.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 23);
			this.label4.TabIndex = 1;
			this.label4.Text = "Port";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "Server";
			// 
			// connectBtn
			// 
			this.connectBtn.Location = new System.Drawing.Point(400, 112);
			this.connectBtn.Name = "connectBtn";
			this.connectBtn.TabIndex = 4;
			this.connectBtn.Text = "Connect";
			this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(488, 112);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.TabIndex = 5;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// LoginDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 149);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cancelBtn,
																		  this.connectBtn,
																		  this.groupBox2,
																		  this.groupBox1});
			this.Name = "LoginDialog";
			this.Text = "LoginDialog";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Allows access to the address of the server
		/// </summary>
		/// <value>Allows access to the address of the server</value>
		public string Server
		{
			get{ return serverBox.SelectedItem.ToString();}
		}

		/// <summary>
		/// Allows access to the server port
		/// </summary>
		/// <value>Allows access to the server port</value>
		public int Port
		{
			get{ return int.Parse(portField.Text);}
		}

		/// <summary>
		/// Allows access to the user's name
		/// </summary>
		/// <value>Allows access to the user's name</value>
		public string UserName
		{
			get{ return nameField.Text;}
		}

		/// <summary>
		/// Allows access to the user's password
		/// </summary>
		/// <value>Allows access to the user's password</value>
		public string Password
		{
			get{ return passField.Text;}
		}

		/// <summary>
		/// Allows access to the user's blurb
		/// </summary>
		/// <value>Allows access to the user's blurb</value>
		public string Blurb
		{
			get{ return blurbField.Text;}
		}

		/// <summary>
		/// Allows access to the flag indicating the username/password are valid
		/// </summary>
		/// <remarks>
		/// If an invalid username/password are given, a seperate dialog is shown to allow the values to
		///   be re-entered.  We do not want to use the values in this dialog in that case
		/// </remarks>
		public bool LoginValid
		{
			get{ return loginValid;}
			set{ loginValid = value;}
		}

		/// <summary>
		/// Allows access to the flag indicating the blurb is  valid
		/// </summary>
		/// <remarks>
		/// If an invalid blurb is given, a seperate dialog is shown to allow the blurb to
		///   be re-entered.  We do not want to use the value in this dialog in that case
		/// </remarks>
		public bool BlurbValid
		{
			get{ return blurbValid;}
			set{ blurbValid = value;}
		}

		/// <summary>
		/// Close the dialog without connecting
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void cancelBtn_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Verify all the necessary data is there and is sane, then return DialogResult.OK
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void connectBtn_Click(object sender, System.EventArgs e)
		{
			/*
			 * Perform data validation
			 */
			if(nameField.Text.Trim() == "")
			{
				MessageBox.Show("You have to specify a user name", "Invalid Login Information");
				return;
			}
			if(passField.Text.Trim() == "")
			{
				MessageBox.Show("You have to enter your password", "Invalid Login Information");
				return;
			}
			try
			{
				int i = int.Parse(portField.Text);
				if( i < 1 || i > 32565)
				{
					MessageBox.Show("Port number out of range", "Invalid Login Information");
					return;
				}
			}
			catch(FormatException)
			{
				MessageBox.Show("The Port must be a number", "Invalid Login Information");
				return;
			}
			
			// All the data is ok, connect
			this.DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// Open the add server dialog to add a server to the list
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arugments</param>
		private void addServerBtn_Click(object sender, System.EventArgs e)
		{
			AddServerDlg addServer = new AddServerDlg();
			if(addServer.ShowDialog() == DialogResult.OK)
			{
				serverBox.Items.Add(addServer.Server);
				serverBox.SelectedIndex = serverBox.Items.Count - 1;
			}
		}

		/// <summary>
		/// NOT YET IMPLEMENTED: Removes a server from the server list
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void removeBtn_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("This will be implemented after saving preferences", "Not yet implemented");
		}

	
	}
}
