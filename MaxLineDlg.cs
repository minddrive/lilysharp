using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Summary description for MaxLineDlg.
	/// </summary>
	public class MaxLineDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox limit;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.CheckBox unlimitedBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MaxLineDlg(int currentLimit)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			limit.Text = currentLimit.ToString();
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
			this.limit = new System.Windows.Forms.TextBox();
			this.okBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.unlimitedBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter Line limit:";
			// 
			// limit
			// 
			this.limit.Location = new System.Drawing.Point(8, 32);
			this.limit.MaxLength = 6;
			this.limit.Name = "limit";
			this.limit.Size = new System.Drawing.Size(144, 20);
			this.limit.TabIndex = 1;
			this.limit.Text = "";
			this.limit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.limit_KeyPress);
			// 
			// okBtn
			// 
			this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okBtn.Location = new System.Drawing.Point(80, 72);
			this.okBtn.Name = "okBtn";
			this.okBtn.TabIndex = 2;
			this.okBtn.Text = "OK";
			// 
			// cancelBtn
			// 
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(168, 72);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.TabIndex = 3;
			this.cancelBtn.Text = "Cancel";
			// 
			// unlimitedBox
			// 
			this.unlimitedBox.Location = new System.Drawing.Point(176, 32);
			this.unlimitedBox.Name = "unlimitedBox";
			this.unlimitedBox.Size = new System.Drawing.Size(72, 24);
			this.unlimitedBox.TabIndex = 4;
			this.unlimitedBox.Text = "No Limit";
			this.unlimitedBox.CheckedChanged += new System.EventHandler(this.unlimitedBox_CheckedChanged);
			// 
			// MaxLineDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(256, 101);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.unlimitedBox,
																		  this.cancelBtn,
																		  this.okBtn,
																		  this.limit,
																		  this.label1});
			this.Name = "MaxLineDlg";
			this.Text = "Line Limit";
			this.ResumeLayout(false);

		}
		#endregion

		public int Limit
		{
			get
			{
				if(unlimitedBox.Checked)
					return -1;
				else
					return int.Parse(limit.Text);
			}
		}

		private void unlimitedBox_CheckedChanged(object sender, System.EventArgs e)
		{
			limit.Enabled = !unlimitedBox.Checked;
		}

		private void limit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(!Char.IsDigit(e.KeyChar))
				e.Handled = true;
		}

	}
}
