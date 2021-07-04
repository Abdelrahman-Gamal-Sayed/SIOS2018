namespace WpfApplication2
{
    partial class MovingFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            this.btnSaveMoving = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnShowSearch2 = new System.Windows.Forms.Button();
            this.rdCompany = new System.Windows.Forms.RadioButton();
            this.rdAreaSort = new System.Windows.Forms.RadioButton();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cmbSearchMessNames = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpHold = new System.Windows.Forms.DateTimePicker();
            this.chkHold = new System.Windows.Forms.CheckBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtReqDate = new System.Windows.Forms.TextBox();
            this.rdAddress = new System.Windows.Forms.RadioButton();
            this.grbSort = new System.Windows.Forms.GroupBox();
            this.rdDate = new System.Windows.Forms.RadioButton();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtResons = new System.Windows.Forms.TextBox();
            this.txtDept = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCompanyAddress = new System.Windows.Forms.TextBox();
            this.txtReqCodeSearch = new System.Windows.Forms.TextBox();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.HoldDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtReqCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbInformation = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.grbSort.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grbInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveMoving
            // 
            this.btnSaveMoving.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveMoving.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnSaveMoving.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnSaveMoving.Location = new System.Drawing.Point(859, 6);
            this.btnSaveMoving.Name = "btnSaveMoving";
            this.btnSaveMoving.Size = new System.Drawing.Size(169, 38);
            this.btnSaveMoving.TabIndex = 2;
            this.btnSaveMoving.Text = "حفظ البيانات";
            this.btnSaveMoving.UseVisualStyleBackColor = true;
            this.btnSaveMoving.Click += new System.EventHandler(this.btnSaveMoving_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnShowSearch2);
            this.panel1.Controls.Add(this.btnSaveMoving);
            this.panel1.Location = new System.Drawing.Point(81, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1032, 51);
            this.panel1.TabIndex = 13;
            // 
            // btnShowSearch2
            // 
            this.btnShowSearch2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowSearch2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnShowSearch2.Location = new System.Drawing.Point(718, 6);
            this.btnShowSearch2.Name = "btnShowSearch2";
            this.btnShowSearch2.Size = new System.Drawing.Size(116, 38);
            this.btnShowSearch2.TabIndex = 3;
            this.btnShowSearch2.Text = "بحث";
            this.btnShowSearch2.UseVisualStyleBackColor = true;
            this.btnShowSearch2.Click += new System.EventHandler(this.btnShowSearch2_Click);
            // 
            // rdCompany
            // 
            this.rdCompany.AutoSize = true;
            this.rdCompany.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.rdCompany.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rdCompany.Location = new System.Drawing.Point(567, 29);
            this.rdCompany.Name = "rdCompany";
            this.rdCompany.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rdCompany.Size = new System.Drawing.Size(81, 21);
            this.rdCompany.TabIndex = 6;
            this.rdCompany.TabStop = true;
            this.rdCompany.Text = "بالشركة";
            this.rdCompany.UseVisualStyleBackColor = true;
            this.rdCompany.CheckedChanged += new System.EventHandler(this.rdCompany_CheckedChanged);
            // 
            // rdAreaSort
            // 
            this.rdAreaSort.AutoSize = true;
            this.rdAreaSort.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.rdAreaSort.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rdAreaSort.Location = new System.Drawing.Point(335, 29);
            this.rdAreaSort.Name = "rdAreaSort";
            this.rdAreaSort.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rdAreaSort.Size = new System.Drawing.Size(84, 21);
            this.rdAreaSort.TabIndex = 14;
            this.rdAreaSort.TabStop = true;
            this.rdAreaSort.Text = "بالمنطقة";
            this.rdAreaSort.UseVisualStyleBackColor = true;
            this.rdAreaSort.CheckedChanged += new System.EventHandler(this.rdAreaSort_CheckedChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Location = new System.Drawing.Point(6, 23);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(114, 37);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "تعديل";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cmbSearchMessNames
            // 
            this.cmbSearchMessNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchMessNames.FormattingEnabled = true;
            this.cmbSearchMessNames.Location = new System.Drawing.Point(193, 83);
            this.cmbSearchMessNames.MaxDropDownItems = 20;
            this.cmbSearchMessNames.Name = "cmbSearchMessNames";
            this.cmbSearchMessNames.Size = new System.Drawing.Size(206, 21);
            this.cmbSearchMessNames.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(364, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "اسم المندوب";
            // 
            // dtpHold
            // 
            this.dtpHold.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHold.Location = new System.Drawing.Point(193, 152);
            this.dtpHold.Name = "dtpHold";
            this.dtpHold.Size = new System.Drawing.Size(206, 20);
            this.dtpHold.TabIndex = 3;
            this.dtpHold.ValueChanged += new System.EventHandler(this.dtpHold_ValueChanged);
            // 
            // chkHold
            // 
            this.chkHold.AutoSize = true;
            this.chkHold.Location = new System.Drawing.Point(193, 128);
            this.chkHold.Name = "chkHold";
            this.chkHold.Size = new System.Drawing.Size(82, 17);
            this.chkHold.TabIndex = 2;
            this.chkHold.Text = "تأجيل الطلب";
            this.chkHold.UseVisualStyleBackColor = true;
            this.chkHold.CheckedChanged += new System.EventHandler(this.chkHold_CheckedChanged);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(193, 32);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.ReadOnly = true;
            this.txtPhone.Size = new System.Drawing.Size(206, 20);
            this.txtPhone.TabIndex = 1;
            // 
            // txtReqDate
            // 
            this.txtReqDate.Location = new System.Drawing.Point(474, 32);
            this.txtReqDate.Name = "txtReqDate";
            this.txtReqDate.ReadOnly = true;
            this.txtReqDate.Size = new System.Drawing.Size(206, 20);
            this.txtReqDate.TabIndex = 1;
            // 
            // rdAddress
            // 
            this.rdAddress.AutoSize = true;
            this.rdAddress.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.rdAddress.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rdAddress.Location = new System.Drawing.Point(218, 30);
            this.rdAddress.Name = "rdAddress";
            this.rdAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rdAddress.Size = new System.Drawing.Size(77, 21);
            this.rdAddress.TabIndex = 12;
            this.rdAddress.TabStop = true;
            this.rdAddress.Text = "بالعنوان";
            this.rdAddress.UseVisualStyleBackColor = true;
            this.rdAddress.CheckedChanged += new System.EventHandler(this.rdAddress_CheckedChanged);
            // 
            // grbSort
            // 
            this.grbSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbSort.Controls.Add(this.rdDate);
            this.grbSort.Controls.Add(this.rdAddress);
            this.grbSort.Controls.Add(this.rdCompany);
            this.grbSort.Controls.Add(this.rdAreaSort);
            this.grbSort.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.grbSort.Location = new System.Drawing.Point(420, 12);
            this.grbSort.Name = "grbSort";
            this.grbSort.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grbSort.Size = new System.Drawing.Size(693, 71);
            this.grbSort.TabIndex = 12;
            this.grbSort.TabStop = false;
            this.grbSort.Text = "ترتيب";
            // 
            // rdDate
            // 
            this.rdDate.AutoSize = true;
            this.rdDate.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.rdDate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rdDate.Location = new System.Drawing.Point(476, 29);
            this.rdDate.Name = "rdDate";
            this.rdDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rdDate.Size = new System.Drawing.Size(63, 21);
            this.rdDate.TabIndex = 7;
            this.rdDate.TabStop = true;
            this.rdDate.Text = "باليوم";
            this.rdDate.UseVisualStyleBackColor = true;
            this.rdDate.CheckedChanged += new System.EventHandler(this.rdDate_CheckedChanged);
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(776, 191);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.ReadOnly = true;
            this.txtContactPerson.Size = new System.Drawing.Size(206, 20);
            this.txtContactPerson.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(385, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "التليفون";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(666, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "تاريخ الطلب";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnSearch.Location = new System.Drawing.Point(21, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(939, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "الشخص المتصل";
            // 
            // txtResons
            // 
            this.txtResons.Location = new System.Drawing.Point(474, 137);
            this.txtResons.Multiline = true;
            this.txtResons.Name = "txtResons";
            this.txtResons.ReadOnly = true;
            this.txtResons.Size = new System.Drawing.Size(206, 80);
            this.txtResons.TabIndex = 1;
            // 
            // txtDept
            // 
            this.txtDept.Location = new System.Drawing.Point(474, 83);
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(206, 20);
            this.txtDept.TabIndex = 1;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(776, 83);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.ReadOnly = true;
            this.txtCompanyName.Size = new System.Drawing.Size(206, 20);
            this.txtCompanyName.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(639, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "اسباب الزيارة";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(666, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "القسم";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(955, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "اسم الشركة";
            // 
            // txtCompanyAddress
            // 
            this.txtCompanyAddress.Location = new System.Drawing.Point(778, 137);
            this.txtCompanyAddress.Name = "txtCompanyAddress";
            this.txtCompanyAddress.ReadOnly = true;
            this.txtCompanyAddress.Size = new System.Drawing.Size(206, 20);
            this.txtCompanyAddress.TabIndex = 1;
            // 
            // txtReqCodeSearch
            // 
            this.txtReqCodeSearch.Location = new System.Drawing.Point(102, 3);
            this.txtReqCodeSearch.Name = "txtReqCodeSearch";
            this.txtReqCodeSearch.Size = new System.Drawing.Size(167, 20);
            this.txtReqCodeSearch.TabIndex = 0;
            this.txtReqCodeSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReqCodeSearch_KeyPress);
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.label10);
            this.panelSearch.Controls.Add(this.txtReqCodeSearch);
            this.panelSearch.Location = new System.Drawing.Point(442, 424);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(376, 29);
            this.panelSearch.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(280, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "ادخل كود الطلب";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HoldDate});
            this.dataGridView1.Location = new System.Drawing.Point(81, 106);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(1032, 256);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView1_Scroll);
            // 
            // HoldDate
            // 
            this.HoldDate.HeaderText = "Hold";
            this.HoldDate.Name = "HoldDate";
            this.HoldDate.Width = 150;
            // 
            // txtReqCode
            // 
            this.txtReqCode.Location = new System.Drawing.Point(776, 32);
            this.txtReqCode.Name = "txtReqCode";
            this.txtReqCode.ReadOnly = true;
            this.txtReqCode.Size = new System.Drawing.Size(206, 20);
            this.txtReqCode.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(953, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "عنوان الشركة";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(968, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "رقم الطلب";
            // 
            // grbInformation
            // 
            this.grbInformation.Controls.Add(this.btnUpdate);
            this.grbInformation.Controls.Add(this.cmbSearchMessNames);
            this.grbInformation.Controls.Add(this.label7);
            this.grbInformation.Controls.Add(this.dtpHold);
            this.grbInformation.Controls.Add(this.chkHold);
            this.grbInformation.Controls.Add(this.txtPhone);
            this.grbInformation.Controls.Add(this.txtReqDate);
            this.grbInformation.Controls.Add(this.txtContactPerson);
            this.grbInformation.Controls.Add(this.label9);
            this.grbInformation.Controls.Add(this.label6);
            this.grbInformation.Controls.Add(this.label3);
            this.grbInformation.Controls.Add(this.txtResons);
            this.grbInformation.Controls.Add(this.txtDept);
            this.grbInformation.Controls.Add(this.txtCompanyName);
            this.grbInformation.Controls.Add(this.label8);
            this.grbInformation.Controls.Add(this.label5);
            this.grbInformation.Controls.Add(this.label2);
            this.grbInformation.Controls.Add(this.txtCompanyAddress);
            this.grbInformation.Controls.Add(this.txtReqCode);
            this.grbInformation.Controls.Add(this.label4);
            this.grbInformation.Controls.Add(this.label1);
            this.grbInformation.Location = new System.Drawing.Point(81, 452);
            this.grbInformation.Name = "grbInformation";
            this.grbInformation.Size = new System.Drawing.Size(1027, 231);
            this.grbInformation.TabIndex = 14;
            this.grbInformation.TabStop = false;
            this.grbInformation.Text = "Info";
            // 
            // MovingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 706);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grbSort);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.grbInformation);
            this.Name = "MovingFrm";
            this.Text = "MovingFrm";
            this.Load += new System.EventHandler(this.MovingFrm_Load);
            this.panel1.ResumeLayout(false);
            this.grbSort.ResumeLayout(false);
            this.grbSort.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grbInformation.ResumeLayout(false);
            this.grbInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaveMoving;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnShowSearch2;
        private System.Windows.Forms.RadioButton rdCompany;
        private System.Windows.Forms.RadioButton rdAreaSort;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cmbSearchMessNames;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpHold;
        private System.Windows.Forms.CheckBox chkHold;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtReqDate;
        private System.Windows.Forms.RadioButton rdAddress;
        private System.Windows.Forms.GroupBox grbSort;
        private System.Windows.Forms.RadioButton rdDate;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtResons;
        private System.Windows.Forms.TextBox txtDept;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCompanyAddress;
        private System.Windows.Forms.TextBox txtReqCodeSearch;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoldDate;
        private System.Windows.Forms.TextBox txtReqCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbInformation;
    }
}