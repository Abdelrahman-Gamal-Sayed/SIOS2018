//using dmc.Bussiness_Layer.Notebooks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApplication2.BusinessLayer.Notebooks;



namespace WpfApplication2
{


    public partial class DeliverNotebookFrm : Form
    {
        string loginUser = "";
        //-------from confirm form ------------
        public int OrderNo { set; get; }
        public int BatchCount { set; get; }
        public string ProvNameFromConfirm { set; get; }
        public int  NoteBookType { set; get; }
        public bool Saved { set; get; } // حتى يتم التاكد من ان المستخدم سلم الدفتر بالفعل

        NoteBookServices All = new NoteBookServices();
        Label lblProvNameFromConfirm = new Label();

        public DeliverNotebookFrm(string User)
        {
            InitializeComponent();
            loginUser = User;
        }

        private void DeliverNotebookFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dtpReceiptDate.CustomFormat = "dd-MMM-yy";
                dtpDeliverDate.CustomFormat = "dd-MMM-yy";
                dtpDeliverDate.Enabled = false;
                //----------select max of Notebook Transaction-----------------------
                #region Selectmax_NotebookTransaction

                string max2 = All.SelectMaxNotebookTransaction();
                int maxx2 = 0;
                if (max2 == "")
                {
                    txtTransCode.Text = "1";
                }

                else
                {
                    maxx2 = int.Parse(max2) + 1;
                    txtTransCode.Text = maxx2.ToString();
                }
                #endregion

                //------------------fill provider type---------------
                List<NoteBookData> ProviderType = All.SelectAllProviderTypes();
                cmbProviderType.DataSource = ProviderType;
                cmbProviderType.DisplayMember = "Type_Name";
                cmbProviderType.ValueMember = "ProvTypeCode";
                //------------------fill Provide Names--------------------
                int ProvideCode = int.Parse(cmbProviderType.SelectedValue.ToString());
                List<NoteBookData> ProviderNames = All.SelectAllProviderNames(ProvideCode);
                listBox1.DataSource = ProviderNames;
                listBox1.DisplayMember = "Prov_Name";
                listBox1.ValueMember = "Prov_Code";
                //---------------------Fill Deliver Types---------------------
                List<NoteBookData> DeliverTypes = All.SelectAllDeliverTypes();
                cmbDeliverType.DataSource = DeliverTypes;
                cmbDeliverType.DisplayMember = "Deliver_Type";
                cmbDeliverType.ValueMember = "Deliver_Code";

                //---------------------Fill Notebook Types---------------------
                List<NoteBookData> NotebookTypes = All.SelectAllNotebookTypes();
                cmbNotebookTypes.DataSource = NotebookTypes;
                cmbNotebookTypes.DisplayMember = "NotebookName";
                cmbNotebookTypes.ValueMember = "Notebook_Type_Code";

                cmbNotebookTypes.SelectedValue = NoteBookType;
                if (int.Parse(cmbProviderType.SelectedValue.ToString()) == 5)
                {
                    grbDoctor.Visible = true;

                }
                else
                {
                    grbDoctor.Visible = false;
                }
            }
            catch { }

            dtGridSerials.ReadOnly = true;
            grbProviderPerson.Visible = false;
            grbMail.Visible = false;
            grbInfoAboutMessenger.Visible = false;


            //=============Hide these if the user press تسليم  from confirm form==============
            if (OrderNo != 0)
            {
                grbDoctor.Visible = false;
                grbProviderNameSearch.Visible = false;
                cmbProviderType.Visible = false;
                label1.Visible = false;
                lblTranactionCode.Visible = false;
                txtTransCode.Visible = false;
                grbDoctor.Visible = false;
                grbReceiptInfo.Location = new Point(520, 48);
                btnNewNotebook.Visible = false;
                btnSearch.Visible = false;
                txtTransSearch.Visible = false;
                label24.Visible = false;
                if (WindowState == FormWindowState.Maximized)
                {
                    lblReceptDate.Location = new Point(230, 21);
                    dtpReceiptDate.Location = new Point(215, 37);
                    grbReceiptInfo.Width = 431;
                }

                //--------- Add Label From Provider Name -------------
                lblProvNameFromConfirm.Text = "مقدم الخدمة: " + ProvNameFromConfirm;
                lblProvNameFromConfirm.BackColor = Color.GreenYellow;
                lblProvNameFromConfirm.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
                lblProvNameFromConfirm.Location = new Point(750, 10);
                lblProvNameFromConfirm.Width = 200;
                lblProvNameFromConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.Controls.Add(lblProvNameFromConfirm);
            }
            if (BatchCount != 0)
            {
                NumericCount.Value = BatchCount;
                NumericCount.Enabled = false;
                dtGridSerials.Visible = true;
                grbSerialFrom_to.Visible = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            ///////////////////////////////////////////////////////////////////////////////////////
            grbInfoAboutMessenger.Visible = false;
            grbMail.Visible = false;
            grbProviderPerson.Visible = false;
            dtGridSerials.Visible = true;

            try
            {
                int TransCode = int.Parse(txtTransSearch.Text);
                //List<NoteBookData> listSerials = All.SelectNotebookSerialByTransCode(TransCode);
                //if (listSerials != null)
                //{
                //    //if (listSerials.Count >= 1)
                //    //{
                //    //    dtGridSerials.DataSource = listSerials;

                //    //    for (int i = 0; i < listSerials.Count; i++)
                //    //    {
                //    //        dtGridSerials.Rows[i].Cells["Key"].Value = i + 1;
                //    //    }
                //    //    for (int i = 0; i < dtGridSerials.Columns.Count; i++)
                //    //    {
                //    //        dtGridSerials.Columns[i].Visible = false;
                //    //    }
                //    //    dtGridSerials.Columns["Key"].Visible = true;
                //    //    dtGridSerials.Columns["Deliver_Serial"].Visible = true;
                //    //}
                //}




                NoteBookData obj = All.SelectNotebookByTransCode(TransCode);
                if (obj != null)
                {
                    txtTransCode.Text = obj.TransAction_Code.ToString();
                    NumericCount.Value = obj.Batch_Count;
                    NumericSerialFrom.Value = obj.Serial_From;
                    NumericSerialTo.Value = obj.Serial_To;
                    cmbNotebookTypes.SelectedValue = obj.Notebook_Type_Code;
                    cmbProviderType.SelectedValue = obj.ProvTypeCode;
                    if (obj.Order_Num > 0)//means it is a request
                    {
                        grbDoctor.Visible = false;
                        grbProviderNameSearch.Visible = false;
                        cmbProviderType.Visible = false;
                        label1.Visible = false;
                        lblTranactionCode.Visible = false;
                        txtTransCode.Visible = false;
                        grbDoctor.Visible = false;
                        grbReceiptInfo.Location = new Point(520, 48);
                        btnNewNotebook.Visible = true;
                        lblProvNameFromConfirm.Visible = true;
                        if (WindowState == FormWindowState.Maximized)
                        {
                            lblReceptDate.Location = new Point(230, 21);
                            dtpReceiptDate.Location = new Point(215, 37);
                            grbReceiptInfo.Width = 431;
                        }

                        //--------- Add Label From Provider Name -------------

                        NoteBookData obj2 = All.SelectProviderNameById(obj.Prov_Code);


                        lblProvNameFromConfirm.Text = "مقدم الخدمة: " + obj2.Prov_Name;
                        lblProvNameFromConfirm.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
                        lblProvNameFromConfirm.Location = new Point(750, 10);
                        lblProvNameFromConfirm.Width = 200;
                        lblProvNameFromConfirm.Name = "lblProvNameFromConfirm2";
                        //lblProvNameFromConfirm.TabIndex=200;
                        lblProvNameFromConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                        this.Controls.Add(lblProvNameFromConfirm);
                    }
                    else
                    {
                        btnNewNotebook.Visible = true;

                        grbDoctor.Visible = true;
                        grbProviderNameSearch.Visible = true;
                        cmbProviderType.Visible = true;
                        label1.Visible = true;
                        lblTranactionCode.Visible = true;
                        txtTransCode.Visible = true;
                        grbDoctor.Visible = true;
                        grbReceiptInfo.Location = new Point(6, 61);
                        btnNewNotebook.Visible = true;
                        lblProvNameFromConfirm.Text = "";
                        lblProvNameFromConfirm.Visible = false;
                        this.Controls.Remove(lblProvNameFromConfirm);


                    }


                    int deliverType = obj.Deliver_Code;
                    //---------مفوض-------------------
                    if (deliverType == 1)
                    {
                        grbProviderPerson.Visible = true;
                        txtDeliverPerson.Text = obj.Deliver_Person;
                        txtNationalID.Text = obj.Deliver_National_ID;
                        txtNationalIDPath.Text = obj.Deliver_National_ID_Img;
                        try
                        {
                            Deliver_ID_Img.Image = Image.FromFile(txtNationalIDPath.Text);
                        }
                        catch { }
                    }
                    //----------------مندوب------------------
                    if (deliverType == 2)
                    {
                        grbInfoAboutMessenger.Visible = true;
                        grbInfoAboutMessenger.Location = new Point(96, 376);
                        txtMessengerCode.Text = obj.Messenger_Code.ToString();
                        NoteBookData obj2 = All.SelectMessengerById(obj.Messenger_Code);
                        txtMessengerNationalID.Text = obj2.CardNum.ToString();

                    }
                    //------------------بريد سريع-----------
                    if (deliverType == 3)
                    {
                        grbMail.Visible = true;
                        txtReceiptNum.Text = obj.Receipt_Num.ToString();
                        txtReceiptName.Text = obj.Receipt_Name.ToString();
                        txtReceiptPath.Text = obj.Receipt_Img.ToString();
                        try
                        {
                            Receipt_Pic.Image = Image.FromFile(txtReceiptPath.Text);
                        }
                        catch { }
                    }
                }
                else
                {
                    MessageBox.Show("غير موجود");
                    dtGridSerials.DataSource = null;
                }
            }
            catch
            {
                MessageBox.Show("ادخل كود صحيح");
            }

        }

        private void txtProviderName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int ProvideCode = int.Parse(txtProviderName.Text);
                string ProvideName = txtProviderName.Text;
                int ProviderType = int.Parse(cmbProviderType.SelectedValue.ToString());

                List<NoteBookData> ProviderNames = All.SelectAllProviderNamesForSearch(ProvideCode, ProvideName, ProviderType);
                listBox1.DataSource = ProviderNames;
                listBox1.DisplayMember = "Prov_Name";
                listBox1.ValueMember = "Prov_Code";
            }
            catch
            {
                int ProvideCode = -1;
                string ProvideName = txtProviderName.Text;
                int ProviderType = int.Parse(cmbProviderType.SelectedValue.ToString());
                List<NoteBookData> ProviderNames = All.SelectAllProviderNamesForSearch(ProvideCode, ProvideName, ProviderType);
                listBox1.DataSource = ProviderNames;
                listBox1.DisplayMember = "Prov_Name";
                listBox1.ValueMember = "Prov_Code";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblProviderName.Text = listBox1.Text;
                int DoctorCode = int.Parse(listBox1.SelectedValue.ToString());
                NoteBookData obj = All.SelectAllDoctorInfo(DoctorCode);
                txtAddress.Text = obj.Address;
                txtDoctorPhone.Text = obj.Phone.ToString();
                txtSpeciallity.Text = obj.Speciality.ToString();
                txtDoc_Spec_Code.Text = obj.Speciality_Code.ToString();
            }
            catch { }
        }
        DataGridViewRow NewRow;
        private void cmbDeliverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDeliverType.SelectedValue.ToString() == "1")
            {
                grbProviderPerson.Visible = true;
                grbInfoAboutMessenger.Visible = false;
                grbMail.Visible = false;

            }
            if (cmbDeliverType.SelectedValue.ToString() == "2")
            {

                grbInfoAboutMessenger.Visible = true;
                grbProviderPerson.Visible = false;
                grbMail.Visible = false;
                grbInfoAboutMessenger.Location = new Point(96, 376);

                //---------------------Fill All Messengers---------------------
                List<NoteBookData> messengers = All.SelectAllMessengers();
                cmbMessengerNames.DataSource = messengers;
                cmbMessengerNames.DisplayMember = "Name";
                cmbMessengerNames.ValueMember = "Id";


            }
            if (cmbDeliverType.SelectedValue.ToString() == "3")
            {
                grbMail.Location = new Point(96, 376);
                grbMail.Visible = true;
                grbInfoAboutMessenger.Visible = false;
                grbProviderPerson.Visible = false;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NoteBookData obj = new NoteBookData();
            obj.Created_Date = dtpDeliverDate.Text;
            obj.Created_By = loginUser;
            obj.TransAction_Code = int.Parse(txtTransCode.Text);
            obj.ProvTypeCode = int.Parse(cmbProviderType.SelectedValue.ToString());
            obj.Prov_Code = int.Parse(listBox1.SelectedValue.ToString());
            //----------doctor-------
            if (obj.ProvTypeCode == 5)
            {
                try
                {
                    obj.Doc_Spec_Code = int.Parse(txtDoc_Spec_Code.Text);
                }
                catch { }
            }
            //---------grd serials-------------
            obj.Batch_Count = int.Parse(NumericCount.Value.ToString());
            obj.Serial_From = int.Parse(NumericSerialFrom.Value.ToString());
            obj.Serial_To = int.Parse(NumericSerialTo.Value.ToString());
            int grdCount = (obj.Serial_To - obj.Serial_From);

            bool Not_serialNum = false;
            bool emptSerial = false;
            int count = int.Parse(NumericCount.Value.ToString());
            if (NumericSerialTo.Value >= 1)
            {
                count = 1;
            }
            if (count >= 1)
            {
                for (int i = 0; i < dtGridSerials.Rows.Count - 1; i++)
                {
                    if (dtGridSerials.Rows[i].Cells["SerialNum"].Value != null)
                    {
                        try
                        {
                            int entered = int.Parse(dtGridSerials.Rows[i].Cells["SerialNum"].Value.ToString());
                        }
                        catch
                        {
                            Not_serialNum = true;
                            dtGridSerials.Rows[i].Cells["SerialNum"].Style.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        emptSerial = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("ادخل العدد المطلوب من فضلك");
            }

            if (Not_serialNum || emptSerial)
            {
                MessageBox.Show("ادخل جميع المسلسلات فى صورة ارقام");

            }

            else
            {
                if (count >= 1)
                {
                    //-------Delete last Old NotebookSerials------------------
                    int affected2 = All.DeleteNotebook_Serial(obj.TransAction_Code);
                    //-------Insert New  NotebookSerials ------------------
                    for (int i = 0; i < dtGridSerials.Rows.Count - 1; i++)
                    {
                        if (dtGridSerials.Rows[i].Cells["SerialNum"].Value != null)
                        {
                            try
                            {
                                obj.Deliver_Serial = int.Parse(dtGridSerials.Rows[i].Cells["SerialNum"].Value.ToString());
                                int affected = All.InsertNotebook_Serial(obj);
                            }
                            catch
                            {
                                Not_serialNum = true;
                                obj.Deliver_Serial = int.Parse(dtGridSerials.Rows[i].Cells["SerialNum"].Value.ToString());
                                int affected = All.InsertNotebook_Serial(obj);
                            }
                        }
                        else
                        {
                            emptSerial = true;
                        }

                    }
                    if (Not_serialNum || emptSerial)
                    {
                        MessageBox.Show("ادخل جميع المسلسلات فى صورة ارقام");

                    }
                }

            }
            //-----------last panel-----------------------
            try
            {
                obj.Notebook_Type_Code = int.Parse(cmbNotebookTypes.SelectedValue.ToString());
                obj.DeliverTypeCode = int.Parse(cmbDeliverType.SelectedValue.ToString());
            }
            catch { }

            bool PersonData = false;
            bool MailData = false;
            //--------------------مفوض من مقدم الخدمة----------------
            if (obj.DeliverTypeCode == 1)
            {
                if (txtDeliverPerson.Text != "" && txtNationalID.Text != "" && txtNationalIDPath.Text != "")
                {
                    obj.Deliver_Person = txtDeliverPerson.Text;
                    try
                    {
                        obj.Deliver_National_ID = txtNationalID.Text;
                    }
                    catch
                    {

                    }

                    obj.Deliver_National_ID_Img = txtNationalIDPath.Text;
                }
                else
                {
                    PersonData = true;
                }

            }
            //--------------------مندوب----------------------------------
            if (obj.DeliverTypeCode == 2)
            {
                obj.Messenger_Code = int.Parse(cmbMessengerNames.SelectedValue.ToString());
            }
            //---------------------بريد سريع -------------------------
            if (obj.DeliverTypeCode == 3)
            {
                if (txtReceiptNum.Text != "" && txtReceiptPath.Text != "" && cmbDeliverCompanyName.Text != "" && txtReceiptName.Text != "" && cmbDeliverCompanyName.Text != "")
                {
                    obj.Receipt_Num = Int64.Parse(txtReceiptNum.Text);
                    obj.Receipt_Img = txtReceiptPath.Text;
                    obj.Receipt_Date = dtpReceiptDate.Text;
                    obj.Receipt_Comp = cmbDeliverCompanyName.Text;
                    obj.Receipt_Name = txtReceiptName.Text;
                }
                else
                {
                    MailData = true;
                }

            }
            if (OrderNo == 0 && count >= 1)//means that is Not A request from Comfirm frm  so insert it--------------
            {
                if (Not_serialNum == false && emptSerial == false)
                {
                    if (PersonData == false && MailData == false)
                    {
                        NoteBookData objNotebook = All.SelectNotebookByTransCode(obj.TransAction_Code);
                        if (objNotebook == null) //means that it is a new Notebook so insert it
                        {
                            int affected2 = All.InsertNotebook(obj);
                            if (affected2 >= 1)
                            {
                                MessageBox.Show("تم الحفظ بنجاح");


                            }
                        }
                        else // update Notebook and NotebookSerial
                        {
                            int affected = All.UpdateNotebook(obj, obj.TransAction_Code);
                            if (affected >= 1)
                            {
                                MessageBox.Show("تم الحفظ بنجاح");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("ادخل كل البيانات المطلوبة بنوع التسليم");
                    }

                }

            }
            else  //means that it is  a request from Comfirm frm  so update it-------------------------
            {
                if (Not_serialNum == false && emptSerial == false && NumericCount.Value >= 1)
                {
                    if (PersonData == false && MailData == false)
                    {
                        int affected2 = All.UpdateNotebook_If_It_is_a_Request(obj, OrderNo);
                        if (affected2 >= 1)
                        {
                            MessageBox.Show("تم الحفظ بنجاح");
                            Saved = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ادخل كل البيانات المطلوبة بنوع التسليم");
                    }

                }

            }
        }

        private void cmbProviderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int ProvideCode = int.Parse(cmbProviderType.SelectedValue.ToString());
                List<NoteBookData> ProviderNames = All.SelectAllProviderNames(ProvideCode);
                listBox1.DataSource = ProviderNames;
                listBox1.DisplayMember = "Prov_Name";
                listBox1.ValueMember = "Prov_Code";
                if (int.Parse(cmbProviderType.SelectedValue.ToString()) == 5)
                {
                    grbDoctor.Visible = true;
                }
                else
                {
                    grbDoctor.Visible = false;
                }
            }
            catch { }
        }

        private void btnBrowseNationalID_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtNationalIDPath.Text = dlg.FileName;
                Deliver_ID_Img.Image = Image.FromFile(dlg.FileName);
                string directory = txtNationalIDPath.Text.ToString();
                string destination = "D:\\";
                string AllPath = destination + Path.GetFileName(directory);
                if (File.Exists(AllPath))
                {
                    File.Delete(AllPath);//--------Delete old img and add new one
                    File.Copy(directory, destination + Path.GetFileName(directory));
                    string filepath = destination + Path.GetFileName(directory);
                    txtNationalIDPath.Text = filepath;
                }
                else
                {
                    File.Copy(directory, destination + Path.GetFileName(directory));
                    string filepath = destination + Path.GetFileName(directory);
                    txtNationalIDPath.Text = filepath;
                }

            }
        }

        private void btnBrowseReceiptPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtReceiptPath.Text = dlg.FileName;
                Receipt_Pic.Image = Image.FromFile(dlg.FileName);
                string directory = txtReceiptPath.Text.ToString();
                string destination = "D:\\";
                string AllPath = destination + Path.GetFileName(directory);
                if (File.Exists(AllPath))
                {
                    File.Delete(AllPath);//--------Delete old img and add new one
                    File.Copy(directory, destination + Path.GetFileName(directory));
                    string filepath = destination + Path.GetFileName(directory);
                    txtReceiptPath.Text = filepath;
                }
                else
                {
                    File.Copy(directory, destination + Path.GetFileName(directory));
                    string filepath = destination + Path.GetFileName(directory);
                    txtReceiptPath.Text = filepath;
                }

            }
        }

        private void NumericCount_ValueChanged(object sender, EventArgs e)
        {
            NumericSerialFrom.Value = 0;
            NumericSerialTo.Value = 0;
            dtGridSerials.ReadOnly = false;
            dtGridSerials.Rows.Clear();
            dtGridSerials.Visible = true;
            NumericSerialTo.Visible = true;
            NumericSerialFrom.Visible = true;
            grbSerialFrom_to.Visible = true;
            int count = int.Parse(NumericCount.Value.ToString());
            for (int i = 0; i < count; i++)
            {

                NewRow = new DataGridViewRow();
                dtGridSerials.Rows.Add(NewRow);
                dtGridSerials.Rows[i].Cells["Key"].Value = i + 1;
                dtGridSerials.Rows[i].Cells["Key"].ReadOnly = true;

                if (dtGridSerials.RowCount == count)
                {
                    dtGridSerials.Rows[count - 1].ReadOnly = true;
                }
            }
        }

        private void cmbMessengerNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Mess_Id = int.Parse(cmbMessengerNames.SelectedValue.ToString());
                NoteBookData obj = All.SelectMessengerById(Mess_Id);
                txtMessengerCode.Text = obj.Id.ToString();
                txtMessengerNationalID.Text = obj.CardNum.ToString();
                txtMessName.Text = obj.Name.ToString();
            }
            catch { }
        }

        private void NumericSerialFrom_ValueChanged(object sender, EventArgs e)
        {
            dtGridSerials.Rows.Clear();
            dtGridSerials.ReadOnly = false;
            int from = int.Parse(NumericSerialFrom.Value.ToString());
            int to = int.Parse(NumericSerialTo.Value.ToString());
            int index = 0;
            for (int i = from; i <= to; i++)
            {
                NewRow = new DataGridViewRow();
                dtGridSerials.Rows.Add(NewRow);
                dtGridSerials.Rows[index].Cells["Key"].Value = i;
                dtGridSerials.Rows[index].Cells["Key"].ReadOnly = true;
                index++;
            }
            dtGridSerials.Rows[index].ReadOnly = true;
        }

        private void NumericSerialTo_ValueChanged(object sender, EventArgs e)
        {
            dtGridSerials.ReadOnly = false;
            dtGridSerials.Rows.Clear();
            int from = int.Parse(NumericSerialFrom.Value.ToString());
            int to = int.Parse(NumericSerialTo.Value.ToString());
            int index = 0;
            for (int i = from; i <= to; i++)
            {
                NewRow = new DataGridViewRow();
                dtGridSerials.Rows.Add(NewRow);
                dtGridSerials.Rows[index].Cells["Key"].Value = i;
                dtGridSerials.Rows[index].Cells["Key"].ReadOnly = true;
                index++;
            }
            dtGridSerials.Rows[index].ReadOnly = true;
        }

        private void dtGridSerials_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dtGridSerials.Rows[e.RowIndex].Cells["SerialNum"].Style.ForeColor = Color.Black;
        }

        private void txtReceiptNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtReceiptNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only Allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnNewNotebook_Click(object sender, EventArgs e)
        {
            //----------select max of Notebook Transaction-----------------------
            #region Selectmax_NotebookTransaction
            string max2 = All.SelectMaxNotebookTransaction();
            int maxx2 = 0;
            if (max2 == "")
            {
                txtTransCode.Text = "1";
            }

            else
            {
                maxx2 = int.Parse(max2) + 1;
                txtTransCode.Text = maxx2.ToString();
            }
            #endregion
            #region Clear
            txtMessengerCode.Text = "";
            txtMessengerNationalID.Text = "";
            txtMessName.Text = "";
            txtDoctorPhone.Text = "";

            txtNationalIDPath.Text = "";
            txtReceiptNum.Text = "";
            txtReceiptName.Text = "";
            txtReceiptPath.Text = "";

            txtTransSearch.Text = "";
            txtSpeciallity.Text = "";
            txtNationalID.Text = "";
            txtDoc_Spec_Code.Text = "";
            txtDeliverPerson.Text = "";
            txtAddress.Text = "";
            NumericCount.Value = 0;
            NumericSerialFrom.Value = 0;
            NumericSerialTo.Value = 0;
            try
            {
                Deliver_ID_Img.Image = Image.FromFile(txtNationalIDPath.Text);
                Receipt_Pic.Image = Image.FromFile(txtReceiptPath.Text);
            }
            catch { }

            grbInfoAboutMessenger.Visible = false;
            grbMail.Visible = false;
            grbProviderPerson.Visible = false;

            #endregion

            grbDoctor.Visible = true;
            grbProviderNameSearch.Visible = true;
            cmbProviderType.Visible = true;
            label1.Visible = true;
            lblTranactionCode.Visible = true;
            txtTransCode.Visible = true;
            grbDoctor.Visible = true;
            grbReceiptInfo.Location = new Point(6, 61);
            btnNewNotebook.Visible = true;
            lblProvNameFromConfirm.Visible = false;
        }

        private void txtTransSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only Allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtNationalID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only Allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
       
    }
}