using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Discussion Joining dialog.
	/// </summary>
	public class JoinDiscDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button joinBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView discList;
		private System.Windows.Forms.GroupBox groupBox1;
		private LilyParent parent;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="parent">The parent of the dialog</param>
		public JoinDiscDlg(LilyParent parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.parent = parent;
			discList.Columns.Add("Name", -1, HorizontalAlignment.Left);
			discList.Columns.Add("Title", -1, HorizontalAlignment.Left);
			discList.Columns.Add("Emote", -2, HorizontalAlignment.Center);
			discList.Columns.Add("Private", -2, HorizontalAlignment.Center);
			discList.Columns.Add("Info", -2, HorizontalAlignment.Center);
			discList.Columns.Add("Memo", -2, HorizontalAlignment.Center);
			discList.Columns.Add("Invulnerable", -2, HorizontalAlignment.Center);
			discList.Columns.Add("Moderated", -2, HorizontalAlignment.Center);
			foreach(DictionaryEntry entry in parent.Handles)
			{
				Discussion disc;
				ListViewItem discRow;
				if(entry.Value.GetType() == typeof(Discussion) && ((Discussion)entry.Value).Window == null)
				{
					disc = (Discussion)entry.Value;
					discRow = new ListViewItem(disc.Name);

					discRow.SubItems.Add(disc.Title);

					discRow.SubItems.Add(asciiCheckbox(!disc.Connect));
					discRow.SubItems.Add(asciiCheckbox(disc.Private));
					discRow.SubItems.Add(asciiCheckbox(disc.Info));
					discRow.SubItems.Add(asciiCheckbox(disc.Memo));
					discRow.SubItems.Add(asciiCheckbox(disc.Invulnerable));
					discRow.SubItems.Add(asciiCheckbox(disc.Moderated));
					
					discList.Items.Add(discRow);

				}
			}

			if(discList.Items.Count == 0)
			{
				MessageBox.Show("There are no discussions you are not a member of");
			}
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
			this.joinBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.panel1 = new System.Windows.Forms.Panel();
			this.discList = new System.Windows.Forms.ListView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// joinBtn
			// 
			this.joinBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.joinBtn.Location = new System.Drawing.Point(490, 0);
			this.joinBtn.Name = "joinBtn";
			this.joinBtn.Size = new System.Drawing.Size(75, 24);
			this.joinBtn.TabIndex = 1;
			this.joinBtn.Text = "Join";
			this.joinBtn.Click += new System.EventHandler(this.joinBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Dock = System.Windows.Forms.DockStyle.Right;
			this.cancelBtn.Location = new System.Drawing.Point(565, 0);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 24);
			this.cancelBtn.TabIndex = 2;
			this.cancelBtn.Text = "Cancel";
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.joinBtn,
																				 this.cancelBtn});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 253);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(640, 24);
			this.panel1.TabIndex = 5;
			// 
			// discList
			// 
			this.discList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.discList.FullRowSelect = true;
			this.discList.GridLines = true;
			this.discList.HideSelection = false;
			this.discList.Location = new System.Drawing.Point(3, 16);
			this.discList.MultiSelect = false;
			this.discList.Name = "discList";
			this.discList.Size = new System.Drawing.Size(634, 234);
			this.discList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.discList.TabIndex = 0;
			this.discList.View = System.Windows.Forms.View.Details;
			this.discList.DoubleClick += new System.EventHandler(this.discList_DoubleClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.discList});
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(640, 253);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Discussion List";
			// 
			// JoinDiscDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 277);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1,
																		  this.panel1});
			this.Name = "JoinDiscDlg";
			this.Text = "Join Discussion";
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		
		/// <summary>
		/// Allows access to the discussion the user wants to join
		/// </summary>
		/// <value>Allows access to the discussion the user wants to join</value>
		public Discussion selectedDisc
		{
			get
			{
				string discName = discList.SelectedItems[0].SubItems[0].Text;
				return (Discussion)parent.Handles[parent.getObjectId(discName)];
			}
		}

		/// <summary>
		/// Overrides the base class's implementation to prevent the showing of the dialog if there are no discussions to join
		/// </summary>
		/// <returns>DialogResult.Cancel if there are no discussions to join, the user's choice otherwise</returns>
		public new DialogResult ShowDialog()
		{
			if(discList.Items.Count == 0)
				return DialogResult.Cancel;
			else
				return base.ShowDialog();
		}

		private string asciiCheckbox(bool term)
		{
			if(term) return "[X]";
			else     return "[   ]";
		}

		private void discList_DoubleClick(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void joinBtn_Click(object sender, System.EventArgs e)
		{
			if(discList.SelectedIndices.Count == 0)
			{
				MessageBox.Show("You must select a discussion to join", "Unable to join");
				return;
			}
			this.DialogResult = DialogResult.OK;
		}
	}
}
