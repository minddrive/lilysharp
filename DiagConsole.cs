using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Summary description for DiagConsole.
	/// </summary>
	public class DiagConsole : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RichTextBox msgArea;
		private System.Windows.Forms.CheckBox userBox;
		private System.Windows.Forms.CheckBox discBox;
		private System.Windows.Forms.CheckBox dataBox;
		private System.Windows.Forms.CheckBox groupBox;
		private System.Windows.Forms.CheckBox notifyBox;
		private System.Windows.Forms.CheckBox commandBox;
		private System.Windows.Forms.CheckBox waterBox;
		private System.Windows.Forms.CheckBox otherBox;
		public bool AllowClose;
		private System.Windows.Forms.ContextMenu msgAreaMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DiagConsole()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.msgArea = new System.Windows.Forms.RichTextBox();
			this.userBox = new System.Windows.Forms.CheckBox();
			this.discBox = new System.Windows.Forms.CheckBox();
			this.dataBox = new System.Windows.Forms.CheckBox();
			this.groupBox = new System.Windows.Forms.CheckBox();
			this.notifyBox = new System.Windows.Forms.CheckBox();
			this.commandBox = new System.Windows.Forms.CheckBox();
			this.waterBox = new System.Windows.Forms.CheckBox();
			this.otherBox = new System.Windows.Forms.CheckBox();
			this.msgAreaMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.otherBox,
																				 this.waterBox,
																				 this.commandBox,
																				 this.notifyBox,
																				 this.groupBox,
																				 this.dataBox,
																				 this.discBox,
																				 this.userBox});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 277);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(456, 72);
			this.panel1.TabIndex = 0;
			// 
			// msgArea
			// 
			this.msgArea.ContextMenu = this.msgAreaMenu;
			this.msgArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.msgArea.Name = "msgArea";
			this.msgArea.ReadOnly = true;
			this.msgArea.Size = new System.Drawing.Size(456, 277);
			this.msgArea.TabIndex = 1;
			this.msgArea.Text = "";
			// 
			// userBox
			// 
			this.userBox.Checked = true;
			this.userBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.userBox.Location = new System.Drawing.Point(8, 8);
			this.userBox.Name = "userBox";
			this.userBox.Size = new System.Drawing.Size(72, 24);
			this.userBox.TabIndex = 0;
			this.userBox.Text = "%USER";
			// 
			// discBox
			// 
			this.discBox.Checked = true;
			this.discBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.discBox.Location = new System.Drawing.Point(8, 40);
			this.discBox.Name = "discBox";
			this.discBox.Size = new System.Drawing.Size(72, 24);
			this.discBox.TabIndex = 1;
			this.discBox.Text = "%DISC";
			// 
			// dataBox
			// 
			this.dataBox.Checked = true;
			this.dataBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.dataBox.Location = new System.Drawing.Point(96, 8);
			this.dataBox.Name = "dataBox";
			this.dataBox.Size = new System.Drawing.Size(80, 24);
			this.dataBox.TabIndex = 2;
			this.dataBox.Text = "%DATA";
			// 
			// groupBox
			// 
			this.groupBox.Checked = true;
			this.groupBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.groupBox.Location = new System.Drawing.Point(96, 40);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(80, 24);
			this.groupBox.TabIndex = 3;
			this.groupBox.Text = "%GROUP";
			// 
			// notifyBox
			// 
			this.notifyBox.Checked = true;
			this.notifyBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.notifyBox.Location = new System.Drawing.Point(192, 8);
			this.notifyBox.Name = "notifyBox";
			this.notifyBox.Size = new System.Drawing.Size(80, 24);
			this.notifyBox.TabIndex = 4;
			this.notifyBox.Text = "%NOTIFY";
			// 
			// commandBox
			// 
			this.commandBox.Checked = true;
			this.commandBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.commandBox.Location = new System.Drawing.Point(192, 40);
			this.commandBox.Name = "commandBox";
			this.commandBox.Size = new System.Drawing.Size(80, 24);
			this.commandBox.TabIndex = 5;
			this.commandBox.Text = "Command";
			// 
			// waterBox
			// 
			this.waterBox.Checked = true;
			this.waterBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.waterBox.Location = new System.Drawing.Point(280, 8);
			this.waterBox.Name = "waterBox";
			this.waterBox.Size = new System.Drawing.Size(112, 24);
			this.waterBox.TabIndex = 6;
			this.waterBox.Text = "%WATERLOGIN";
			// 
			// otherBox
			// 
			this.otherBox.Checked = true;
			this.otherBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.otherBox.Location = new System.Drawing.Point(280, 40);
			this.otherBox.Name = "otherBox";
			this.otherBox.TabIndex = 7;
			this.otherBox.Text = "Other";
			// 
			// msgAreaMenu
			// 
			this.msgAreaMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Clear";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// DiagConsole
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(456, 349);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.msgArea,
																		  this.panel1});
			this.Name = "DiagConsole";
			this.Text = "DiagConsole";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DiagConsole_Closing);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		
		public void Post(string msg)
		{
			int typeEnd = msg.IndexOf(' ');
			string msgType;

			if(typeEnd == -1)
			{
				if(otherBox.Checked) msgArea.AppendText(msg);
				return;
			}
			else
				msgType = msg.Substring(0, typeEnd);

			switch (msgType)
			{
				case "%USER":
					if(!userBox.Checked) return;
					break;
				case "%DISC":
					if(!discBox.Checked) return;
					break;
				case "%DATA":
					if(!dataBox.Checked) return;
					break;
				case "%GROUP":
					if(!groupBox.Checked) return;
					break;
				case "%NOTIFY":
					if(!notifyBox.Checked) return;
					break;
				case "%WATERLOGIN":
					if(!waterBox.Checked) return;
					break;
				case "%begin":
				case "%command":
				case "%end":
					if(!commandBox.Checked) return;
					break;
				default:
					if(!otherBox.Checked) return;
					break;
			}

			msgArea.AppendText(msg + '\n');
		}

		private void DiagConsole_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(!AllowClose)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			msgArea.Clear();
		}
	}
}
