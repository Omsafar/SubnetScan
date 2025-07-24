using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ListViewSorter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form_listView : System.Windows.Forms.Form
	{
		private ListViewColumnSorter lvwColumnSorter = null;

        private System.Windows.Forms.ImageList imageList_all;
        private GroupBox groupBox_sortOptions;
        private RadioButton radioButton_sortByImage;
        private RadioButton radioButton_sortByCheckbox;
        private RadioButton radioButton_sortByText;
        private ListView listView_example;
        private ColumnHeader columnHeader_name;
        private ColumnHeader columnHeader_price;
        private ColumnHeader columnHeader_currency;
        private ColumnHeader columnHeader_nullable;
        private ColumnHeader columnHeader_numbermix;
		private System.ComponentModel.IContainer components;

		public Form_listView()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			myInitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_listView));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Agent",
            "1000",
            "$",
            "",
            "5,308"}, 0, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Software ABC",
            "500",
            "€"}, 2, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Software XYZ",
            "233",
            "$",
            "not null",
            "10"}, 2, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Monitor",
            "199",
            "$"}, 4, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Device",
            "14",
            "SEK",
            "not null",
            "02389"}, 5, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "PCMCIA Device",
            "99",
            "$"}, 6, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Calculator",
            "3",
            "€"}, 3, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Big Number",
            "99999999",
            "GBP",
            "not null",
            "3.458"}, 3);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Empty",
            "",
            ""}, 3);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "JustNumbers",
            "",
            "",
            "",
            "25693,699"}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "MoreNumbers",
            "",
            "",
            "",
            "0,00589633333"}, -1);
            this.imageList_all = new System.Windows.Forms.ImageList(this.components);
            this.groupBox_sortOptions = new System.Windows.Forms.GroupBox();
            this.radioButton_sortByImage = new System.Windows.Forms.RadioButton();
            this.radioButton_sortByCheckbox = new System.Windows.Forms.RadioButton();
            this.radioButton_sortByText = new System.Windows.Forms.RadioButton();
            this.listView_example = new System.Windows.Forms.ListView();
            this.columnHeader_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_currency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_nullable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_numbermix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox_sortOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList_all
            // 
            this.imageList_all.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_all.ImageStream")));
            this.imageList_all.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_all.Images.SetKeyName(0, "");
            this.imageList_all.Images.SetKeyName(1, "");
            this.imageList_all.Images.SetKeyName(2, "");
            this.imageList_all.Images.SetKeyName(3, "");
            this.imageList_all.Images.SetKeyName(4, "");
            this.imageList_all.Images.SetKeyName(5, "");
            this.imageList_all.Images.SetKeyName(6, "");
            // 
            // groupBox_sortOptions
            // 
            this.groupBox_sortOptions.Controls.Add(this.radioButton_sortByImage);
            this.groupBox_sortOptions.Controls.Add(this.radioButton_sortByCheckbox);
            this.groupBox_sortOptions.Controls.Add(this.radioButton_sortByText);
            this.groupBox_sortOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_sortOptions.Location = new System.Drawing.Point(0, 0);
            this.groupBox_sortOptions.Name = "groupBox_sortOptions";
            this.groupBox_sortOptions.Size = new System.Drawing.Size(539, 64);
            this.groupBox_sortOptions.TabIndex = 1;
            this.groupBox_sortOptions.TabStop = false;
            this.groupBox_sortOptions.Text = "First column sort options";
            // 
            // radioButton_sortByImage
            // 
            this.radioButton_sortByImage.AutoSize = true;
            this.radioButton_sortByImage.Location = new System.Drawing.Point(268, 19);
            this.radioButton_sortByImage.Name = "radioButton_sortByImage";
            this.radioButton_sortByImage.Size = new System.Drawing.Size(89, 17);
            this.radioButton_sortByImage.TabIndex = 2;
            this.radioButton_sortByImage.Text = "by image/text";
            this.radioButton_sortByImage.UseVisualStyleBackColor = true;
            this.radioButton_sortByImage.CheckedChanged += new System.EventHandler(this.radioButton_sortByImage_CheckedChanged);
            // 
            // radioButton_sortByCheckbox
            // 
            this.radioButton_sortByCheckbox.AutoSize = true;
            this.radioButton_sortByCheckbox.Location = new System.Drawing.Point(136, 19);
            this.radioButton_sortByCheckbox.Name = "radioButton_sortByCheckbox";
            this.radioButton_sortByCheckbox.Size = new System.Drawing.Size(108, 17);
            this.radioButton_sortByCheckbox.TabIndex = 1;
            this.radioButton_sortByCheckbox.Text = "by checkbox/text";
            this.radioButton_sortByCheckbox.UseVisualStyleBackColor = true;
            this.radioButton_sortByCheckbox.CheckedChanged += new System.EventHandler(this.radioButton_sortByCheckbox_CheckedChanged);
            // 
            // radioButton_sortByText
            // 
            this.radioButton_sortByText.AutoSize = true;
            this.radioButton_sortByText.Checked = true;
            this.radioButton_sortByText.Location = new System.Drawing.Point(12, 19);
            this.radioButton_sortByText.Name = "radioButton_sortByText";
            this.radioButton_sortByText.Size = new System.Drawing.Size(75, 17);
            this.radioButton_sortByText.TabIndex = 0;
            this.radioButton_sortByText.TabStop = true;
            this.radioButton_sortByText.Text = "just by text";
            this.radioButton_sortByText.UseVisualStyleBackColor = true;
            this.radioButton_sortByText.CheckedChanged += new System.EventHandler(this.radioButton_sortByText_CheckedChanged);
            // 
            // listView_example
            // 
            this.listView_example.CheckBoxes = true;
            this.listView_example.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_name,
            this.columnHeader_price,
            this.columnHeader_currency,
            this.columnHeader_nullable,
            this.columnHeader_numbermix});
            this.listView_example.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Checked = true;
            listViewItem2.StateImageIndex = 1;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.Checked = true;
            listViewItem7.StateImageIndex = 1;
            listViewItem8.Checked = true;
            listViewItem8.StateImageIndex = 1;
            listViewItem9.StateImageIndex = 0;
            listViewItem10.StateImageIndex = 0;
            listViewItem11.StateImageIndex = 0;
            this.listView_example.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11});
            this.listView_example.Location = new System.Drawing.Point(0, 64);
            this.listView_example.Name = "listView_example";
            this.listView_example.Size = new System.Drawing.Size(539, 229);
            this.listView_example.SmallImageList = this.imageList_all;
            this.listView_example.TabIndex = 2;
            this.listView_example.UseCompatibleStateImageBehavior = false;
            this.listView_example.View = System.Windows.Forms.View.Details;
            this.listView_example.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_example_ColumnClick);
            // 
            // columnHeader_name
            // 
            this.columnHeader_name.Text = "Name";
            this.columnHeader_name.Width = 139;
            // 
            // columnHeader_price
            // 
            this.columnHeader_price.Text = "Price";
            this.columnHeader_price.Width = 68;
            // 
            // columnHeader_currency
            // 
            this.columnHeader_currency.Text = "Currency";
            this.columnHeader_currency.Width = 79;
            // 
            // columnHeader_nullable
            // 
            this.columnHeader_nullable.Text = "Null?";
            // 
            // columnHeader_numbermix
            // 
            this.columnHeader_numbermix.Text = "NumberMix";
            this.columnHeader_numbermix.Width = 113;
            // 
            // Form_listView
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(539, 293);
            this.Controls.Add(this.listView_example);
            this.Controls.Add(this.groupBox_sortOptions);
            this.Name = "Form_listView";
            this.Text = "ListViewForm";
            this.groupBox_sortOptions.ResumeLayout(false);
            this.groupBox_sortOptions.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form_listView());
		}

		private void myInitializeComponent()
		{
			lvwColumnSorter = new ListViewColumnSorter();
			this.listView_example.ListViewItemSorter = lvwColumnSorter;
            this.listView_example.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView_example.AutoArrange = true;

            lvwColumnSorter._SortModifier = ListViewColumnSorter.SortModifiers.SortByText;
            //this.listView_example.Sort();
		}

		private void listView_example_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			ListView myListView = (ListView)sender;

			// Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.ColumnToSort)
			{
				// Reverse the current sort direction for this column.
                if (lvwColumnSorter.OrderOfSort == System.Windows.Forms.SortOrder.Ascending)
				{
                    lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Descending;
				}
				else
				{
                    lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.ColumnToSort = e.Column;
                lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			myListView.Sort();
		}

        private void radioButton_sortByText_CheckedChanged(object sender, EventArgs e)
        {
            lvwColumnSorter._SortModifier = ListViewColumnSorter.SortModifiers.SortByText;
            //this.listView_example.Sort();
        }

        private void radioButton_sortByCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            lvwColumnSorter._SortModifier = ListViewColumnSorter.SortModifiers.SortByCheckbox;
            //this.listView_example.Sort();
        }

        private void radioButton_sortByImage_CheckedChanged(object sender, EventArgs e)
        {
            lvwColumnSorter._SortModifier = ListViewColumnSorter.SortModifiers.SortByImage;
            //this.listView_example.Sort();
        }
	}
}
