using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Dialog prompting the user for their blurb.  TODO: Enforce length restrictions
	/// </summary>
	public class BlurbDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox blurbBox;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		public BlurbDlg()
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
			this.blurbBox = new System.Windows.Forms.TextBox();
			this.okBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// blurbBox
			// 
			this.blurbBox.Location = new System.Drawing.Point(16, 32);
			this.blurbBox.Name = "blurbBox";
			this.blurbBox.Size = new System.Drawing.Size(216, 20);
			this.blurbBox.TabIndex = 0;
			this.blurbBox.Text = "";
			// 
			// okBtn
			// 
			this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okBtn.Location = new System.Drawing.Point(160, 64);
			this.okBtn.Name = "okBtn";
			this.okBtn.TabIndex = 1;
			this.okBtn.Text = "Ok";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 3;
			this.label1.Text = "Enter your blurb:";
			// 
			// BlurbDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(240, 101);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1,
																		  this.okBtn,
																		  this.blurbBox});
			this.Name = "BlurbDlg";
			this.Text = "BlurbDlg";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Allows access to the user's blurb
		/// </summary>
		/// <value>Allows access to the user's blurb</value>
		public string Blurb
		{
			get { return blurbBox.Text; }
		}
	}
}
