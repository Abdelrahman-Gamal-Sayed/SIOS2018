using WpfApplication2.BusinessLayer.Moving_Messenger;
using WpfApplication2.BusinessLayer.Messenger_Confirmation;
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
    public partial class MessengerConfirmation : Form
    {
        public MessengerConfirmation()
        {
            InitializeComponent();
        }
            ConfirMessServices AllObj = new ConfirMessServices();
        private void MessengerConfirmation_Load(object sender, EventArgs e)
        {
            BindingList<ConfirMessData> list = AllObj.SelectAllMessMoving();
            dataGridView1.DataSource = list;
            //------------add checkbox for confirmation---------------------
            DataGridViewCheckBoxColumn chkDone = new DataGridViewCheckBoxColumn();
            chkDone.HeaderText = "تم";
            chkDone.DataPropertyName = "column_Done";
            chkDone.Name = "column_Done";
            dataGridView1.Columns.AddRange(chkDone);

            try
            {
                //------------add TextBox for confirmation---------------------
                DataGridViewTextBoxColumn txtComments = new DataGridViewTextBoxColumn();
                txtComments.HeaderText = "ملاحظات";
                txtComments.DataPropertyName = "Column_Comments";
                txtComments.Name = "Column_Comments";
                dataGridView1.Columns.AddRange(txtComments);
            }
            catch { }


            if (list == null)
            {
                MessageBox.Show("لا يوجد تحركات لتأكيد وصولها  او  ربما تم تأكيد جميعها من قبل");
                dataGridView1.DataSource = null;
                btnSaveMessConfirm.Enabled = false;
                dataGridView1.Columns["Column_Done"].Visible = false;
                dataGridView1.Columns["Column_Comments"].Visible = false;

            }
            try
            {
                #region Disaple all columns except MessName to update---------
                dataGridView1.Columns["CompanyName"].ReadOnly = true;
                dataGridView1.Columns["ReqCode"].ReadOnly = true;
                dataGridView1.Columns["MessName"].ReadOnly = true;
                dataGridView1.Columns["Branch"].ReadOnly = true;
                dataGridView1.Columns["ReqCode"].ReadOnly = true;
                dataGridView1.Columns["Area"].ReadOnly = true;
                dataGridView1.Columns["ContactPerson"].ReadOnly = true;
                dataGridView1.Columns["Address"].ReadOnly = true;
                dataGridView1.Columns["Phone"].ReadOnly = true;
                dataGridView1.Columns["Dept"].ReadOnly = true;
                dataGridView1.Columns["Date"].ReadOnly = true;
                dataGridView1.Columns["MessengerType"].ReadOnly = true;
                dataGridView1.Columns["RequestResons"].Width = 300;
                #endregion
            }
            catch { }





            //---------Hide Default Which commes from ConfirmationMessData columns----------------
            try
            {
                dataGridView1.Columns["Done"].Visible = false;
                dataGridView1.Columns["Comments"].Visible = false;
            }
            catch { }

        }

        private void btnSaveMessConfirm_Click(object sender, EventArgs e)
        {
            bool CheckDone = false;
            bool CheckComments = false;
            List<int> reqList = new List<int>();
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        ConfirMessData obj = new ConfirMessData();
                        obj.ReqCode = int.Parse(row.Cells["ReqCode"].Value.ToString());

                        //--------check if this messenger already exist------------------
                        ConfirMessData OldMessenger = AllObj.SelectMessengerMovingById(obj.ReqCode);
                        if (OldMessenger != null)//-------update it-------------
                        {
                            if (obj.ReqCode == OldMessenger.ReqCode)//---means that it is exist so update it
                            {
                                if (Convert.ToBoolean(row.Cells["column_Done"].Value) != false)
                                {

                                    int affected = AllObj.InsertMessangerConfirm_For_Request(obj, obj.ReqCode);
                                    int affected2 = AllObj.UpdateMess_Moving_AfterConfirm(obj, obj.ReqCode);
                                    if (affected > 0 && affected2 > 0)
                                    {
                                        CheckDone = true;
                                        reqList.Add(obj.ReqCode);

                                    }
                                }

                                if (row.Cells["Column_Comments"].Value != null)
                                {
                                  //  obj.Comments = row.Cells["Column_Comments"].Value.ToString();
                                    int affected = AllObj.InsertMessangerConfirm_For_Request(obj, obj.ReqCode);
                                    int affected2 = AllObj.UpdateMess_Moving_AfterConfirm2(obj, obj.ReqCode);
                                    if (affected > 0 && affected2 > 0)
                                    {
                                        CheckComments = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (CheckDone || CheckComments)
                {
                    MessageBox.Show("تمت عملية الحفظ");
                }

                for (int n = dataGridView1.Rows.Count - 1; n >= 0; n--)
                {
                    int req = int.Parse((dataGridView1.Rows[n].Cells["ReqCode"].Value.ToString()));
                    for (int i = 0; i < reqList.Count; i++)
                    {
                        if (req == reqList[i])
                        {
                            dataGridView1.Rows.RemoveAt(n);
                        }
                    }
                }



                if (dataGridView1.Rows.Count == 0)
                {
                    dataGridView1.DataSource = null;
                    btnSaveMessConfirm.Enabled = false;
                    dataGridView1.Columns["Column_Done"].Visible = false;
                    dataGridView1.Columns["Column_Comments"].Visible = false;
                }
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
