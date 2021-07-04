using WpfApplication2.BusinessLayer.Notebooks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApplication2
{
    public partial class ConfirmNotebook_RequestFrm : Form
    {
        string LoginUser = "";
        public ConfirmNotebook_RequestFrm(string User)
        {
            InitializeComponent();
            LoginUser = User;
        }

        NoteBookServices All = new NoteBookServices();
        private void ConfirmNotebook_RequestFrm_Load(object sender, EventArgs e)
        {
            string x = "";


            try
            {
                List<NoteBookData> list = All.SelectAllNotebook_Request();
                dataGridView1.DataSource = list;
                if (list != null)
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].Visible = false;
                    }
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].ReadOnly = true;
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        int prov_type_Code = int.Parse(list[i].ProvTypeCode.ToString());
                        //-------------- get Provider Type ----------------
                        NoteBookData obj = All.SelectProviderTypeNameById(prov_type_Code);
                        dataGridView1.Rows[i].Cells["Type_Name"].Value = obj.Type_Name;
                        //------------- get Provider Name --------------------
                        int prov_Code = int.Parse(list[i].Prov_Code.ToString());
                        NoteBookData obj2 = All.SelectProviderNameById(prov_Code);
                        dataGridView1.Rows[i].Cells["Prov_Name"].Value = obj2.Prov_Name;
                        //---------------get Request_Date--------------------------
                        DateTime d=Convert.ToDateTime(list[i].Request_Date);
                        x = d.ToShortDateString();
                        dataGridView1.Rows[i].Cells["Request_Date"].Value = x;
                        
                        //-----------------get Notebook Type ----------------------
                        try
                        {
                            int NotebookType_Code = int.Parse(list[i].Notebook_Type_Code.ToString());
                            NoteBookData NotebookObj = All.SelectNotebookTypeById(NotebookType_Code);
                            dataGridView1.Rows[i].Cells["NotebookName"].Value = NotebookObj.NotebookName;
                        }
                        catch { }
                        //-----------------get Notebook count-----------------------
                        dataGridView1.Rows[i].Cells["Batch_Count"].Value = list[i].Batch_Count;

                        //----------------Confirm Notebook_Request-----------------          
                    }
                    //------------add checkbox for confirmation---------------------
                    DataGridViewCheckBoxColumn chkConfirm = new DataGridViewCheckBoxColumn();
                    chkConfirm.HeaderText = "تاكيد";
                    chkConfirm.DataPropertyName = "column_Confirm";
                    chkConfirm.Name = "column_Confirm";
                    dataGridView1.Columns.AddRange(chkConfirm);

                    //------------add Button for Deliver Notebook---------------------
                    DataGridViewButtonColumn btnDeliver = new DataGridViewButtonColumn();
                    btnDeliver.HeaderText = "تسليم";
                    btnDeliver.DefaultCellStyle.NullValue = "تسليم";
                    btnDeliver.DataPropertyName = "column_btnDeliver";
                    btnDeliver.Name = "column_btnDeliver";
                    dataGridView1.Columns.AddRange(btnDeliver);

                    //-----------Show only Important Data
                    dataGridView1.Columns["column_btnDeliver"].Width = 200;
                    dataGridView1.Columns["ORDER_NUM"].Visible = true;
                    dataGridView1.Columns["Type_Name"].Visible = true;
                    dataGridView1.Columns["Prov_Name"].Visible = true;
                    dataGridView1.Columns["Request_Date"].Visible = true;
                    dataGridView1.Columns["Request_date"].DefaultCellStyle.Format = "dd-MMM-yy";
                    dataGridView1.Columns["NotebookName"].Visible = true;
                    dataGridView1.Columns["Batch_Count"].Visible = true;
                    dataGridView1.Columns["column_Confirm"].ReadOnly = false;
                    dataGridView1.Columns["column_btnDeliver"].ReadOnly = false;
                    dataGridView1.Columns["ORDER_NUM"].DisplayIndex = 0;
                }
                else
                {
                    MessageBox.Show("لا يوجد طلبات لتأكيدها او ربما جميعها تم تاكيدها من قبل");
                    btnSaveData.Enabled = false;
                }
            }
            catch
            {
            }
        }
        NoteBookData obj = new NoteBookData();
        int OrderNo;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NoteBookData obj = new NoteBookData();
            //----------select max of Notebook Transaction-----------------------
            #region Selectmax_NotebookTransaction
            string max2 = All.SelectMaxNotebookTransaction();
            int maxx2 = 0;
            if (max2 == "")
            {
                obj.TransAction_Code = 1;
            }
            else
            {
                maxx2 = int.Parse(max2) + 1;
                obj.TransAction_Code = maxx2;
            }
            #endregion
            var senderGrid = (DataGridView)sender;
            obj.Order_Num = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Order_Num"].Value.ToString());
            obj.ProvTypeCode = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["ProvTypeCode"].Value.ToString());
            obj.Prov_Code = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Prov_Code"].Value.ToString());
            try
            {
                obj.Request_Date = dataGridView1.Rows[e.RowIndex].Cells["Request_Date"].Value.ToString();
            }
            catch { }
            obj.Notebook_Type_Code = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Notebook_Type_Code"].Value.ToString());
            obj.Batch_Count = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Batch_Count"].Value.ToString());
            obj.Created_By = LoginUser;
            try
            {
                obj.Created_Date = dataGridView1.Rows[e.RowIndex].Cells["Request_Date"].Value.ToString();
                obj.Prov_Name = dataGridView1.Rows[e.RowIndex].Cells["Prov_Name"].Value.ToString();
            }
            catch { }

            //--------------------Check press تسليم --------------------------------
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["column_Confirm"].Value) == false)//----the use unchecked the Confirmation and want to deliver
                {
                    if (MessageBox.Show(" هل تريد تاكيد الطلب وتسليمة فى الحال", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        int affected = All.UpdateNotebook_Request_AfterConfirm(obj, obj.Order_Num);
                        if (affected >= 1)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["column_Confirm"].Value = true;
                        }
                        //---------------------Insert in Notebook After press Confirm--------------------             
                        int affected2 = All.InsertRequest_InNotebook_AfterPressDeliver(obj);
                        if (affected2 >= 1)
                        {
                            //---------------------
                        }
                        DeliverNotebookFrm frm = new DeliverNotebookFrm("Admin");
                        frm.OrderNo = obj.Order_Num;
                        frm.BatchCount = obj.Batch_Count;
                        frm.ProvNameFromConfirm = obj.Prov_Name;
                        frm.ShowDialog();
                        //------------------------- After that Remove it from DataGridView --------------
                        if (frm.Saved == true)
                        {
                            #region UpdateDatagrid
                            List<NoteBookData> list = All.SelectAllNotebook_Request();
                            dataGridView1.DataSource = list;
                            if (list != null)
                            {
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {
                                    dataGridView1.Columns[i].Visible = false;
                                }
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {
                                    dataGridView1.Columns[i].ReadOnly = true;
                                }
                                for (int i = 0; i < list.Count; i++)
                                {
                                    int prov_type_Code = int.Parse(list[i].ProvTypeCode.ToString());
                                    //-------------- get Provider Type ----------------
                                    NoteBookData obj3 = All.SelectProviderTypeNameById(prov_type_Code);
                                    dataGridView1.Rows[i].Cells["Type_Name"].Value = obj3.Type_Name;
                                    //------------- get Provider Name --------------------
                                    int prov_Code = int.Parse(list[i].Prov_Code.ToString());
                                    NoteBookData obj2 = All.SelectProviderNameById(prov_Code);
                                    dataGridView1.Rows[i].Cells["Prov_Name"].Value = obj2.Prov_Name;
                                    //---------------get Request_Date--------------------------
                                    dataGridView1.Rows[i].Cells["Request_Date"].Value = list[i].Request_Date;
                                    //-----------------get Notebook Type ----------------------
                                    try
                                    {
                                        int NotebookType_Code = int.Parse(list[i].Notebook_Type_Code.ToString());
                                        NoteBookData NotebookObj = All.SelectNotebookTypeById(NotebookType_Code);
                                        dataGridView1.Rows[i].Cells["NotebookName"].Value = NotebookObj.NotebookName;
                                    }
                                    catch { }
                                    //-----------------get Notebook count-----------------------
                                    dataGridView1.Rows[i].Cells["Batch_Count"].Value = list[i].Batch_Count;

                                    //----------------Confirm Notebook_Request-----------------          
                                }
                                //------------add checkbox for confirmation---------------------
                                DataGridViewCheckBoxColumn chkConfirm = new DataGridViewCheckBoxColumn();
                                chkConfirm.HeaderText = "تاكيد";
                                chkConfirm.DataPropertyName = "column_Confirm";
                                chkConfirm.Name = "column_Confirm";
                                dataGridView1.Columns.AddRange(chkConfirm);

                                //------------add Button for Deliver Notebook---------------------
                                DataGridViewButtonColumn btnDeliver = new DataGridViewButtonColumn();
                                btnDeliver.HeaderText = "تسليم";
                                btnDeliver.DefaultCellStyle.NullValue = "تسليم";
                                btnDeliver.DataPropertyName = "column_btnDeliver";
                                btnDeliver.Name = "column_btnDeliver";
                                dataGridView1.Columns.AddRange(btnDeliver);

                                //-----------Show only Important Data
                                dataGridView1.Columns["column_btnDeliver"].Width = 200;
                                dataGridView1.Columns["ORDER_NUM"].Visible = true;
                                dataGridView1.Columns["Type_Name"].Visible = true;
                                dataGridView1.Columns["Prov_Name"].Visible = true;
                                dataGridView1.Columns["Request_Date"].Visible = true;
                                dataGridView1.Columns["NotebookName"].Visible = true;
                                dataGridView1.Columns["Batch_Count"].Visible = true;
                                dataGridView1.Columns["column_Confirm"].ReadOnly = false;
                                dataGridView1.Columns["column_btnDeliver"].ReadOnly = false;
                                dataGridView1.Columns["ORDER_NUM"].DisplayIndex = 0;
                            }
                            #endregion
                        }

                    }
                }
                else
                {
                    int affected = All.UpdateNotebook_Request_AfterConfirm(obj, obj.Order_Num);
                    if (affected >= 1)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["column_Confirm"].Value = true;
                    }
                    int affected2 = All.InsertRequest_InNotebook_AfterPressDeliver(obj);
                    if (affected2 >= 1)
                    {
                        //---------------------
                    }
                    DeliverNotebookFrm frm = new DeliverNotebookFrm(LoginUser);
                    frm.OrderNo = obj.Order_Num;
                    frm.BatchCount = obj.Batch_Count;
                    frm.ProvNameFromConfirm = obj.Prov_Name;
                    frm.NoteBookType = obj.Notebook_Type_Code;
                    frm.ShowDialog();
                    //------------------------- After that Remove it from DataGridView --------------
                    if (frm.Saved == true)
                    {
                        #region UpdateDatagrid
                        List<NoteBookData> list = All.SelectAllNotebook_Request();
                        dataGridView1.DataSource = list;
                        if (list != null)
                        {
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                dataGridView1.Columns[i].Visible = false;
                            }
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                dataGridView1.Columns[i].ReadOnly = true;
                            }
                            for (int i = 0; i < list.Count; i++)
                            {
                                int prov_type_Code = int.Parse(list[i].ProvTypeCode.ToString());
                                //-------------- get Provider Type ----------------
                                NoteBookData obj3 = All.SelectProviderTypeNameById(prov_type_Code);
                                dataGridView1.Rows[i].Cells["Type_Name"].Value = obj3.Type_Name;
                                //------------- get Provider Name --------------------
                                int prov_Code = int.Parse(list[i].Prov_Code.ToString());
                                NoteBookData obj2 = All.SelectProviderNameById(prov_Code);
                                dataGridView1.Rows[i].Cells["Prov_Name"].Value = obj2.Prov_Name;
                                //---------------get Request_Date--------------------------
                                dataGridView1.Rows[i].Cells["Request_Date"].Value = list[i].Request_Date;
                                //-----------------get Notebook Type ----------------------
                                try
                                {
                                    int NotebookType_Code = int.Parse(list[i].Notebook_Type_Code.ToString());
                                    NoteBookData NotebookObj = All.SelectNotebookTypeById(NotebookType_Code);
                                    dataGridView1.Rows[i].Cells["NotebookName"].Value = NotebookObj.NotebookName;
                                }
                                catch { }
                                //-----------------get Notebook count-----------------------
                                dataGridView1.Rows[i].Cells["Batch_Count"].Value = list[i].Batch_Count;

                                //----------------Confirm Notebook_Request-----------------          
                            }
                            //------------add checkbox for confirmation---------------------
                            DataGridViewCheckBoxColumn chkConfirm = new DataGridViewCheckBoxColumn();
                            chkConfirm.HeaderText = "تاكيد";
                            chkConfirm.DataPropertyName = "column_Confirm";
                            chkConfirm.Name = "column_Confirm";
                            dataGridView1.Columns.AddRange(chkConfirm);

                            //------------add Button for Deliver Notebook---------------------
                            DataGridViewButtonColumn btnDeliver = new DataGridViewButtonColumn();
                            btnDeliver.HeaderText = "تسليم";
                            btnDeliver.DefaultCellStyle.NullValue = "تسليم";
                            btnDeliver.DataPropertyName = "column_btnDeliver";
                            btnDeliver.Name = "column_btnDeliver";
                            dataGridView1.Columns.AddRange(btnDeliver);

                            //-----------Show only Important Data
                            dataGridView1.Columns["column_btnDeliver"].Width = 200;
                            dataGridView1.Columns["ORDER_NUM"].Visible = true;
                            dataGridView1.Columns["Type_Name"].Visible = true;
                            dataGridView1.Columns["Prov_Name"].Visible = true;
                            dataGridView1.Columns["Request_Date"].Visible = true;
                            dataGridView1.Columns["NotebookName"].Visible = true;
                            dataGridView1.Columns["Batch_Count"].Visible = true;
                            dataGridView1.Columns["column_Confirm"].ReadOnly = false;
                            dataGridView1.Columns["column_btnDeliver"].ReadOnly = false;
                            dataGridView1.Columns["ORDER_NUM"].DisplayIndex = 0;
                        }
                        else
                        {
                            dataGridView1.DataSource = null;
                        }
                        #endregion
                    }

                }

            }

        }
        private void btnSaveData_Click(object sender, EventArgs e)
        {
            bool saved = false;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["column_Confirm"].Value) == true)
                {

                    NoteBookData obj = new NoteBookData();
                    //----------select max of Notebook Transaction-----------------------
                    #region Selectmax_NotebookTransaction
                    string max2 = All.SelectMaxNotebookTransaction();
                    int maxx2 = 0;
                    if (max2 == "")
                    {
                        obj.TransAction_Code = 1;
                    }
                    else
                    {
                        maxx2 = int.Parse(max2) + 1;
                        obj.TransAction_Code = maxx2;
                    }
                    #endregion
                    obj.Order_Num = int.Parse(row.Cells["Order_Num"].Value.ToString());
                    obj.ProvTypeCode = int.Parse(row.Cells["ProvTypeCode"].Value.ToString());
                    obj.Prov_Code = int.Parse(row.Cells["Prov_Code"].Value.ToString());
                    try
                    {
                        obj.Request_Date = row.Cells["Request_Date"].Value.ToString();
                    }
                    catch { }
                    obj.Notebook_Type_Code = int.Parse(row.Cells["Notebook_Type_Code"].Value.ToString());
                    obj.Batch_Count = int.Parse(row.Cells["Batch_Count"].Value.ToString());
                    obj.Created_By = LoginUser;
                    try
                    {
                        obj.Created_Date = row.Cells["Request_Date"].Value.ToString();
                        obj.Prov_Name = row.Cells["Prov_Name"].Value.ToString();
                    }
                    catch { }

                    OrderNo = int.Parse(row.Cells["Order_Num"].Value.ToString());
                    obj.Order_Num = int.Parse(row.Cells["Order_Num"].Value.ToString());
                    int affected = All.UpdateNotebook_Request_AfterConfirm(obj, OrderNo);
                    if (affected >= 1)
                    {
                        saved = true;
                    }
                    //---------------------Insert in Notebook After press Confirm--------------------             
                    int affected2 = All.InsertRequest_InNotebook_AfterPressDeliver(obj);
                    if (affected2 >= 1)
                    {
                        //---------------------
                    }
                }
            }
            if (saved)
            {
                MessageBox.Show("تم الحفظ بنجاح");
                //------------------------- After that Remove it from DataGridView --------------
                #region UpdateDatagrid
                List<NoteBookData> list = All.SelectAllNotebook_Request();
                dataGridView1.DataSource = list;
                if (list != null)
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].Visible = false;
                    }
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].ReadOnly = true;
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        int prov_type_Code = int.Parse(list[i].ProvTypeCode.ToString());
                        //-------------- get Provider Type ----------------
                        NoteBookData obj3 = All.SelectProviderTypeNameById(prov_type_Code);
                        dataGridView1.Rows[i].Cells["Type_Name"].Value = obj3.Type_Name;
                        //------------- get Provider Name --------------------
                        int prov_Code = int.Parse(list[i].Prov_Code.ToString());
                        NoteBookData obj2 = All.SelectProviderNameById(prov_Code);
                        dataGridView1.Rows[i].Cells["Prov_Name"].Value = obj2.Prov_Name;
                        //---------------get Request_Date--------------------------
                        dataGridView1.Rows[i].Cells["Request_Date"].Value = list[i].Request_Date;
                        //-----------------get Notebook Type ----------------------
                        try
                        {
                            int NotebookType_Code = int.Parse(list[i].Notebook_Type_Code.ToString());
                            NoteBookData NotebookObj = All.SelectNotebookTypeById(NotebookType_Code);
                            dataGridView1.Rows[i].Cells["NotebookName"].Value = NotebookObj.NotebookName;
                        }
                        catch { }
                        //-----------------get Notebook count-----------------------
                        dataGridView1.Rows[i].Cells["Batch_Count"].Value = list[i].Batch_Count;

                        //----------------Confirm Notebook_Request-----------------          
                    }
                    //------------add checkbox for confirmation---------------------
                    DataGridViewCheckBoxColumn chkConfirm = new DataGridViewCheckBoxColumn();
                    chkConfirm.HeaderText = "تاكيد";
                    chkConfirm.DataPropertyName = "column_Confirm";
                    chkConfirm.Name = "column_Confirm";
                    dataGridView1.Columns.AddRange(chkConfirm);

                    //------------add Button for Deliver Notebook---------------------
                    DataGridViewButtonColumn btnDeliver = new DataGridViewButtonColumn();
                    btnDeliver.HeaderText = "تسليم";
                    btnDeliver.DefaultCellStyle.NullValue = "تسليم";
                    btnDeliver.DataPropertyName = "column_btnDeliver";
                    btnDeliver.Name = "column_btnDeliver";
                    dataGridView1.Columns.AddRange(btnDeliver);

                    //-----------Show only Important Data
                    dataGridView1.Columns["column_btnDeliver"].Width = 200;
                    dataGridView1.Columns["ORDER_NUM"].Visible = true;
                    dataGridView1.Columns["Type_Name"].Visible = true;
                    dataGridView1.Columns["Prov_Name"].Visible = true;
                    dataGridView1.Columns["Request_Date"].Visible = true;
                    dataGridView1.Columns["NotebookName"].Visible = true;
                    dataGridView1.Columns["Batch_Count"].Visible = true;
                    dataGridView1.Columns["column_Confirm"].ReadOnly = false;
                    dataGridView1.Columns["column_btnDeliver"].ReadOnly = false;
                    dataGridView1.Columns["ORDER_NUM"].DisplayIndex = 0;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
                #endregion
            }
        }
    }
}
