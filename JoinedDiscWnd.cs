using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Summary description for JoindDiscWnd.
	/// </summary>
	public class JoindDiscWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView discList;
		public bool AllowClose = false;
		/// <summary>
		/// Contains the list of discussions to search through for opening windows, getting info, etc.
		/// </summary>
		private ArrayList discs = new ArrayList();
		private int msgCount = 0;
		private System.Windows.Forms.ImageList listViewImgList;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem infoItem;
		private System.Windows.Forms.MenuItem memoItem;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem quitItem;
		private System.Windows.Forms.ContextMenu discContextMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem autosortItem;
		private StatusBarPanel notifyPanel;

		public JoindDiscWnd(LilyParent parent, StatusBarPanel notifyPanel)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.MdiParent = parent;
			this.notifyPanel = notifyPanel;
			
			discList.Columns.Add("Name", 94, HorizontalAlignment.Left);
			discList.Columns.Add("New Msgs", discList.Width - discList.Columns[0].Width - 20, HorizontalAlignment.Center);
			discList.ListViewItemSorter = new DiscListItemComparer(0, discList.Sorting);
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(JoindDiscWnd));
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.discList = new System.Windows.Forms.ListView();
			this.discContextMenu = new System.Windows.Forms.ContextMenu();
			this.autosortItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.infoItem = new System.Windows.Forms.MenuItem();
			this.memoItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.quitItem = new System.Windows.Forms.MenuItem();
			this.listViewImgList = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 325);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(184, 32);
			this.panel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.discList});
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(184, 325);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Discussions";
			// 
			// discList
			// 
			this.discList.ContextMenu = this.discContextMenu;
			this.discList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.discList.FullRowSelect = true;
			this.discList.GridLines = true;
			this.discList.Location = new System.Drawing.Point(3, 16);
			this.discList.Name = "discList";
			this.discList.Size = new System.Drawing.Size(178, 306);
			this.discList.SmallImageList = this.listViewImgList;
			this.discList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.discList.TabIndex = 0;
			this.discList.View = System.Windows.Forms.View.Details;
			this.discList.DoubleClick += new System.EventHandler(this.discList_DoubleClick);
			this.discList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.discList_ColumnClick);
			// 
			// discContextMenu
			// 
			this.discContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.autosortItem,
																							this.menuItem1,
																							this.infoItem,
																							this.memoItem,
																							this.menuItem3,
																							this.quitItem});
			this.discContextMenu.Popup += new System.EventHandler(this.discContextMenu_Popup);
			// 
			// autosortItem
			// 
			this.autosortItem.Checked = true;
			this.autosortItem.Index = 0;
			this.autosortItem.RadioCheck = true;
			this.autosortItem.Text = "New Msgs on Top";
			this.autosortItem.Click += new System.EventHandler(this.autosortItem_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.Text = "-";
			// 
			// infoItem
			// 
			this.infoItem.Index = 2;
			this.infoItem.Text = "Info";
			this.infoItem.Click += new System.EventHandler(this.infoItem_Click);
			// 
			// memoItem
			// 
			this.memoItem.Index = 3;
			this.memoItem.Text = "Memo";
			this.memoItem.Click += new System.EventHandler(this.memoItem_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 4;
			this.menuItem3.Text = "-";
			// 
			// quitItem
			// 
			this.quitItem.Index = 5;
			this.quitItem.Text = "Quit";
			this.quitItem.Click += new System.EventHandler(this.quitItem_Click);
			// 
			// listViewImgList
			// 
			this.listViewImgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.listViewImgList.ImageSize = new System.Drawing.Size(16, 16);
			this.listViewImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listViewImgList.ImageStream")));
			this.listViewImgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// JoindDiscWnd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(184, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1,
																		  this.panel1});
			this.Name = "JoindDiscWnd";
			this.Text = "Joined Discussions";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.JoindDiscWnd_Closing);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Add a discussion to the joined discussion list
		/// </summary>
		/// <param name="disc">The discussion to add to the list</param>
		public void Add(IDiscussion disc)
		{
			if(discs.Contains(disc)) return;

			ListViewItem discRow = new ListViewItem(disc.Name);
			discRow.SubItems.Add("0");
			discList.Items.Add(discRow);

			discs.Add(disc);
		}

		/// <summary>
		/// Remove a discussion from the joined discs (user quit)
		/// </summary>
		/// <param name="disc">IDiscussion to remove from the list</param>
		public void Remove(IDiscussion disc)
		{
			discs.Remove(disc);
			foreach(ListViewItem item in discList.Items)
			{
				if(item.Text == disc.Name)
				{
					msgCount -= int.Parse(item.SubItems[1].Text);
					discList.Items.Remove(item);
					break;
				}
			}

			setTooltip();
		}
		/// <summary>
		/// Increase the number of unread messages in the given discussion by one
		/// </summary>
		/// <param name="disc">The discussion of the unread message</param>
		public void AddMsg(IDiscussion disc)
		{
			// Increment the message counter, the disc's message counter, and enable the message icon
			foreach(ListViewItem item in discList.Items)
			{
				if(item.Text == disc.Name)
				{
					int discMsgCount = int.Parse(item.SubItems[1].Text) + 1;
					item.SubItems[1].Text = discMsgCount.ToString();
					item.ImageIndex = 0;
					break;
				}
			}
			msgCount++;

			//Update message notification in the statusbar
			if(msgCount == 1)
				notifyPanel.Text = "1 New Message";
			else
				notifyPanel.Text = msgCount + " New Messages";
			if(notifyPanel.Icon == null)
				notifyPanel.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("lilySharp.message.ico"));

			//Update the tooltip
			setTooltip();

			//Sort by message count if desired
			if(autosortItem.Checked && ((DiscListItemComparer)discList.ListViewItemSorter).Collumn != 1)
			{
				discList.Sorting = SortOrder.Descending;
				discList.ListViewItemSorter = new DiscListItemComparer(1, discList.Sorting, DiscListItemComparer.ItemType.Number);
			}
			else
				discList.Sort();
		}

		/// <summary>
		/// Flag all unread messages in the given discussion as read
		/// </summary>
		/// <param name="disc">The discussion who's messages have been read</param>
		public void ClearMsg(IDiscussion disc)
		{
			// Clear the New Messages column for the disc, and update the mesage counter
			foreach(ListViewItem item in discList.Items)
			{
				if(item.Text == disc.Name)
				{
					msgCount -= int.Parse(item.SubItems[1].Text);
					item.SubItems[1].Text = "0";
					item.ImageIndex = -1;
				}
			}
			
			//discList.Sort();
			setTooltip();

			// Update the new message area
			if(msgCount == 0)
			{
				notifyPanel.Text = "No New Messages";
				notifyPanel.Icon = null;
				notifyPanel.ToolTipText = "";
			}
			else if(msgCount == 1)
				notifyPanel.Text = "1 New Message";
			else
				notifyPanel.Text = msgCount + " New Messages";
		}

		/// <summary>
		/// Remove all discussions from the list
		/// </summary>
		public void Clear()
		{
			discList.Items.Clear();
			discs.Clear();
			msgCount = 0;
			notifyPanel.ToolTipText = "";
			notifyPanel.Icon = null;
		}

		private IDiscussion selectedDisc
		{
			get
			{
				foreach(IDiscussion disc in discs)
					if(disc.Name == discList.SelectedItems[0].Text)
						return disc;
				return null;
			}
		}

		public new void Show()
		{
			((LilyParent)MdiParent).DiscBtn.Pushed = true;
			base.Show();
		}

		public new void Hide()
		{
			((LilyParent)MdiParent).DiscBtn.Pushed = false;
			base.Hide();
		}

		private void setTooltip()
		{
			notifyPanel.ToolTipText = "";

			foreach(ListViewItem item in discList.Items)
			{
				if(item.SubItems[1].Text != "0")
				{
					if(notifyPanel.ToolTipText != "")
						notifyPanel.ToolTipText += "\n";
					notifyPanel.ToolTipText += item.SubItems[0].Text + ": " + item.SubItems[1].Text;
				}
			}

		}
		/// <summary>
		/// Hide the window, instead of closing it.  We need this window to keep track of messages at all times
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void JoindDiscWnd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			((LilyParent)MdiParent).DiscBtn.Pushed = false;
			if(!AllowClose)
			{
				this.Hide();
				e.Cancel = true;
			}
		}

		/// <summary>
		/// Opens a dicsussion's window when it's double-clicked
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void discList_DoubleClick(object sender, System.EventArgs e)
		{
			selectedDisc.Window.Show();
		}
	
		/// <summary>
		/// Sort the collumns
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void discList_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Swap the sort order
			discList.Sorting = (discList.Sorting == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;

			if(e.Column == 0)
			{
				discList.ListViewItemSorter = new DiscListItemComparer(0, discList.Sorting);
			}
			if(e.Column == 1)
			{
				discList.ListViewItemSorter = new DiscListItemComparer(1, discList.Sorting, DiscListItemComparer.ItemType.Number);
			}
		}

		/// <summary>
		/// Update the context menu depending on the discussion information
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void discContextMenu_Popup(object sender, System.EventArgs e)
		{
			if(selectedDisc == null)
			{
				memoItem.Enabled = false;
				infoItem.Enabled = false;
				quitItem.Enabled = false;
			}
			memoItem.Enabled = selectedDisc.Memo;
			infoItem.Enabled = selectedDisc.Info;
		}

		/// <summary>
		/// Displays discussion info
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void infoItem_Click(object sender, System.EventArgs e)
		{
            InfoDlg infoDlg = new InfoDlg(MdiParent as LilyParent, selectedDisc);
			infoDlg.Show();
		}

		/// <summary>
		/// Displays the discussion's memos
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void memoItem_Click(object sender, System.EventArgs e)
		{
			MemoDlg memoDlg = new MemoDlg(MdiParent as LilyParent, selectedDisc);
			memoDlg.Show();
		}

		/// <summary>
		/// Quits the selected discussion
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private void quitItem_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("Really quit " + discList.SelectedItems[0].Text, "Quit Confermation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				((LilyParent)MdiParent).Out.WriteLine("/quit \"" + discList.SelectedItems[0].Text + "\"");
				discs.Remove(selectedDisc);
				discList.Items.Remove(discList.SelectedItems[0]);
			}
		}

		/// <summary>
		/// Check/uncheck the sort by messages item (value is checked in other methods)
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arugments</param>
		private void autosortItem_Click(object sender, System.EventArgs e)
		{
			autosortItem.Checked = !autosortItem.Checked;
		}
	}


	/// <summary>
	/// Used to compair ListView elements for sorting
	/// </summary>
	class DiscListItemComparer : IComparer
	{
		private int col = 0;
		private int sortModifier = 1;
		private ItemType itemType = ItemType.String;
		public enum ItemType {String, Number};

		/// <summary>
		/// Allows access to the sorted column number
		/// </summary>
		/// <value>Allows access to the sorted column number</value>
		public int Collumn
		{
			get{ return col;}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="collumn">Collumn to sort</param>
		/// <param name="order">Order to sort</param>
		/// <param name="type">Type of item being sorted</param>
		public DiscListItemComparer(int collumn, SortOrder order, ItemType type)
		{
			col = collumn;
			sortModifier = (order == SortOrder.Ascending) ? 1 : -1;
			itemType = type;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="collumn">Collumn to sort</param>
		/// <param name="order">Order to sort</param>
		public DiscListItemComparer(int collumn, SortOrder order)
		{
			col = collumn;
			sortModifier = (order == SortOrder.Ascending) ? 1 : -1;
		}

		/// <summary>
		/// Compairs two objects
		/// </summary>
		/// <param name="x">First object to compair</param>
		/// <param name="y">Second object to compair</param>
		/// <returns>0 if the values are equal, -1 or 1 if the objects are not equal (depending on sorting order)</returns>
		/// <remarks>
		/// Multiplying by sortModifier modifies the result for ascending, desending
		/// </remarks>
		public int Compare(object x, object y)
		{
			/*
			if(x == null && y != null)
				return sortModifier;
			else if(y == null && x != null)
				return -sortModifier;
			else if(y == null && x == null)
				return 0;
*/
			if(itemType == ItemType.String)
				return String.Compare( ((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text)*sortModifier;
			else if(itemType == ItemType.Number)
			{
				int i = int.Parse( ((ListViewItem)x).SubItems[col].Text);
				int j = int.Parse( ((ListViewItem)y).SubItems[col].Text);
				
				return sortModifier * ((i == j) ? 0 : (i < j) ? -1 : 1);
			}
			else
			{
				return 0;
			}
		}
	}
}
