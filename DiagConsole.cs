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
		private int lineCount;
		private int maxLines;
		private System.Windows.Forms.ContextMenu msgAreaMenu;
		private System.Windows.Forms.MenuItem clearItem;
		private System.Windows.Forms.MenuItem lineLimitItem;
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
			Sock.Instance.LineRecieved += new SockEventHandler(LineRecieved);

			lineCount = 0;
			maxLines = 1000;
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
			this.otherBox = new System.Windows.Forms.CheckBox();
			this.waterBox = new System.Windows.Forms.CheckBox();
			this.commandBox = new System.Windows.Forms.CheckBox();
			this.notifyBox = new System.Windows.Forms.CheckBox();
			this.groupBox = new System.Windows.Forms.CheckBox();
			this.dataBox = new System.Windows.Forms.CheckBox();
			this.discBox = new System.Windows.Forms.CheckBox();
			this.userBox = new System.Windows.Forms.CheckBox();
			this.msgArea = new System.Windows.Forms.RichTextBox();
			this.msgAreaMenu = new System.Windows.Forms.ContextMenu();
			this.clearItem = new System.Windows.Forms.MenuItem();
			this.lineLimitItem = new System.Windows.Forms.MenuItem();
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
			// otherBox
			// 
			this.otherBox.Location = new System.Drawing.Point(280, 40);
			this.otherBox.Name = "otherBox";
			this.otherBox.TabIndex = 7;
			this.otherBox.Text = "Other";
			// 
			// waterBox
			// 
			this.waterBox.Location = new System.Drawing.Point(280, 8);
			this.waterBox.Name = "waterBox";
			this.waterBox.Size = new System.Drawing.Size(112, 24);
			this.waterBox.TabIndex = 6;
			this.waterBox.Text = "%WATERLOGIN";
			// 
			// commandBox
			// 
			this.commandBox.Location = new System.Drawing.Point(192, 40);
			this.commandBox.Name = "commandBox";
			this.commandBox.Size = new System.Drawing.Size(80, 24);
			this.commandBox.TabIndex = 5;
			this.commandBox.Text = "Command";
			// 
			// notifyBox
			// 
			this.notifyBox.Location = new System.Drawing.Point(192, 8);
			this.notifyBox.Name = "notifyBox";
			this.notifyBox.Size = new System.Drawing.Size(80, 24);
			this.notifyBox.TabIndex = 4;
			this.notifyBox.Text = "%NOTIFY";
			// 
			// groupBox
			// 
			this.groupBox.Location = new System.Drawing.Point(96, 40);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(80, 24);
			this.groupBox.TabIndex = 3;
			this.groupBox.Text = "%GROUP";
			// 
			// dataBox
			// 
			this.dataBox.Location = new System.Drawing.Point(96, 8);
			this.dataBox.Name = "dataBox";
			this.dataBox.Size = new System.Drawing.Size(80, 24);
			this.dataBox.TabIndex = 2;
			this.dataBox.Text = "%DATA";
			// 
			// discBox
			// 
			this.discBox.Location = new System.Drawing.Point(8, 40);
			this.discBox.Name = "discBox";
			this.discBox.Size = new System.Drawing.Size(72, 24);
			this.discBox.TabIndex = 1;
			this.discBox.Text = "%DISC";
			// 
			// userBox
			// 
			this.userBox.Location = new System.Drawing.Point(8, 8);
			this.userBox.Name = "userBox";
			this.userBox.Size = new System.Drawing.Size(72, 24);
			this.userBox.TabIndex = 0;
			this.userBox.Text = "%USER";
			// 
			// msgArea
			// 
			this.msgArea.ContextMenu = this.msgAreaMenu;
			this.msgArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.msgArea.HideSelection = false;
			this.msgArea.Name = "msgArea";
			this.msgArea.ReadOnly = true;
			this.msgArea.Size = new System.Drawing.Size(456, 277);
			this.msgArea.TabIndex = 1;
			this.msgArea.Text = "";
			this.msgArea.WordWrap = false;
			// 
			// msgAreaMenu
			// 
			this.msgAreaMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.clearItem,
																						this.lineLimitItem});
			// 
			// clearItem
			// 
			this.clearItem.Index = 0;
			this.clearItem.Text = "Clear";
			this.clearItem.Click += new System.EventHandler(this.clearItem_Click);
			// 
			// lineLimitItem
			// 
			this.lineLimitItem.Index = 1;
			this.lineLimitItem.Text = "Line Limit";
			this.lineLimitItem.Click += new System.EventHandler(this.lineLimitItem_Click);
			// 
			// DiagConsole
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(456, 349);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.msgArea,
																		  this.panel1});
			this.Name = "DiagConsole";
			this.Text = "Diagnostic Console";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DiagConsole_Closing);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		
		private void LineRecieved(object sender, SockEventArgs e)
		{
			if(InvokeRequired) Invoke(new SockEventHandler(LineRecieved), new Object[]{sender, e});

			int typeEnd = e.Line.IndexOf(' ');
			string msgType;

			if(typeEnd == -1)
			{
				if(otherBox.Checked) msgArea.AppendText(e.Line);
				return;
			}
			else
				msgType = e.Line.Substring(0, typeEnd);

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

			/*
			if(lineCount < maxLines && maxLines > 0)
				lineCount++;
			else
				msgArea.Text = msgArea.Text.Remove(0, msgArea.Text.IndexOf("\n") +1);
			*/
			
			

			msgArea.AppendText(e.Line + '\n');
		}

		private void DiagConsole_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(!AllowClose)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void clearItem_Click(object sender, System.EventArgs e)
		{
			msgArea.Clear();
			lineCount = 0;
		}

		private void lineLimitItem_Click(object sender, System.EventArgs e)
		{
			MaxLineDlg maxDlg = new MaxLineDlg(maxLines);
			if(maxDlg.ShowDialog() == DialogResult.OK)
			{
				maxLines = maxDlg.Limit;

				if(lineCount > maxLines)
				{
					int diff = lineCount - maxLines;
					
					int index = 0;
					for(int i = 0; i < diff; i++)
					{
						index = msgArea.Text.IndexOf('\n',index);
						index++;
					}
					
					msgArea.Text = msgArea.Text.Substring(index);
				}

			}
		}
	}
}
