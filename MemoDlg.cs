using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Summary description for MemoDlg.
	/// </summary>
	public class MemoDlg : System.Windows.Forms.Form, ILeafCmd
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RichTextBox memoBox;
		private System.Windows.Forms.Button closeBtn;
		private System.Windows.Forms.ComboBox memoList;
		private LilyParent parent;
		private ILilyObject   source;
		private System.Windows.Forms.Label dateLable;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="parent">This window's parent</param>
		/// <param name="source">The user/discussion whos memos are being displayed</param>
		public MemoDlg(LilyParent parent, ILilyObject source)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.parent = parent;
			this.source = source;
			this.Text = source.Name + "'s Memos";

			LeafMessage msg = new LeafMessage("/memo " + source.Name.Replace(' ','_'), "list", this);
			parent.PostMessage(msg);

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
			this.closeBtn = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.memoList = new System.Windows.Forms.ComboBox();
			this.memoBox = new System.Windows.Forms.RichTextBox();
			this.dateLable = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.closeBtn});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 285);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 24);
			this.panel1.TabIndex = 0;
			// 
			// closeBtn
			// 
			this.closeBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.closeBtn.Location = new System.Drawing.Point(309, 0);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(75, 24);
			this.closeBtn.TabIndex = 0;
			this.closeBtn.Text = "Close";
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.dateLable,
																				 this.memoList});
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(384, 40);
			this.panel2.TabIndex = 1;
			// 
			// memoList
			// 
			this.memoList.Location = new System.Drawing.Point(0, 8);
			this.memoList.Name = "memoList";
			this.memoList.Size = new System.Drawing.Size(144, 21);
			this.memoList.TabIndex = 0;
			this.memoList.SelectedIndexChanged += new System.EventHandler(this.memoList_SelectedIndexChanged);
			// 
			// memoBox
			// 
			this.memoBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.memoBox.Location = new System.Drawing.Point(0, 40);
			this.memoBox.Name = "memoBox";
			this.memoBox.ReadOnly = true;
			this.memoBox.Size = new System.Drawing.Size(384, 245);
			this.memoBox.TabIndex = 2;
			this.memoBox.Text = "Retrieving memo...";
			this.memoBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.memoBox_LinkClicked);
			// 
			// dateLable
			// 
			this.dateLable.Dock = System.Windows.Forms.DockStyle.Right;
			this.dateLable.Location = new System.Drawing.Point(184, 0);
			this.dateLable.Name = "dateLable";
			this.dateLable.Size = new System.Drawing.Size(200, 40);
			this.dateLable.TabIndex = 1;
			this.dateLable.Text = "Date:";
			this.dateLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MemoDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(384, 309);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.memoBox,
																		  this.panel2,
																		  this.panel1});
			this.Name = "MemoDlg";
			this.Text = "MemoDlg";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// Required for ILeafCmd.  Either populates the memo ComboBox, or displays the memo
		/// </summary>
		/// <param name="msg"></param>
		public void ProcessResponse(LeafMessage msg)
		{
			// Populate the combobox
			if(msg.Tag == "list")
			{
				/*
				 * Check to make sure that all memos are not private
				 */
				if(Regex.Match(msg.Response, @"(.*((have)|(has)) no notes on \w* memo pad)").Success)
				{
					MessageBox.Show(source.Name + " has no public memos");
					return;
				}
	
				/*
				 * Retrieve each memo, sepearate it into its component parts, store it, and add it to the ComboBox
				 */
				Memo memo;
				foreach(string str in msg.Response.Split(new char[] {'\n'}))
				{
					Match match = Regex.Match(str, @"^(\d+\s+)?(.*?)\s+(\d+)\s+(\w+)\s+(...) (...) (..) (..:..:..) (....) (...)$");
					if(match.Success)
					{
						memo = new Memo();
						memo.ServerNumber = match.Result("$1");
						memo.Name         = match.Result("$2");
						memo.Size         = match.Result("$3");
						memo.DayOfWeek    = match.Result("$5");
						memo.Month        = match.Result("$6");
						memo.Day          = match.Result("$7");
						memo.Time         = match.Result("$8");
						memo.Year         = match.Result("$9");

						memoList.Items.Add(memo);
					}
				}
				memoList.SelectedIndex = 0;
			}
			// Display the memo
			if(msg.Tag == "memo")
			{
				memoBox.Text = msg.Response.Replace("\n*","\n").Remove(0,1);
			}
		}

		/// <summary>
		/// Ask the server for the selected memo
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arugments</param>
		private void memoList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.dateLable.Text = "Date: " + ((Memo)memoList.SelectedItem).GetDate();
			LeafMessage msg = new LeafMessage("/memo " + source.Name.Replace(' ', '_') + " \"" + memoList.SelectedItem + "\"", "memo", this);
			parent.PostMessage(msg);
			memoBox.Text = "Retrieving memo...";
		}

		/// <summary>
		/// Closes the window
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void closeBtn_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Open the link in the default application
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void memoBox_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo(e.LinkText);
				startInfo.UseShellExecute = true;
				System.Diagnostics.Process.Start(startInfo);
			}
			catch(Win32Exception)
			{
				MessageBox.Show("Error Starting process " + e.LinkText);
			}
		}
	}

	/// <summary>
	/// Holds the data for a memo
	/// </summary>
	struct Memo
	{
		public string ServerNumber;
		public string Name;
		public string Size;
		public string DayOfWeek;
		public string Month;
		public string Day;
		public string Time;
		public string Year;

		/// <summary>
		/// Returns the name of the memo.  Used for display purpouses, such as in a ComboBox
		/// </summary>
		/// <returns>The name of the memo</returns>
		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Strings all the date elements together.  May stor the date as a DateTime object in the future
		/// </summary>
		/// <returns>The date elements joined together</returns>
		public string GetDate()
		{
			return DayOfWeek + " " + Month + " " + Day + " " + Time + " " + Year;
		}
	}
}
