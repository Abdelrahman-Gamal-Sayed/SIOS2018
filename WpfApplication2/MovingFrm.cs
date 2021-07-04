using WpfApplication2.BusinessLayer.MessengerRequest;
using WpfApplication2.BusinessLayer.Moving_Messenger;
using WpfApplication2.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApplication2.BusinessLayer;

namespace WpfApplication2
{
    public partial class MovingFrm : Form
    {
        DateTimePicker dtp = new DateTimePicker();
        Rectangle rec;
        MovingMessServices AllObj = new MovingMessServices();
        MessangerServices abdo = new MessangerServices();


        public MovingFrm()
        {
            InitializeComponent();
          //  WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


            dataGridView1.Controls.Add(dtp);
            dtp.Visible = false;
            //dtp.Format = DateTimePickerFormat.Custom;
            dtp.Format = DateTimePickerFormat.Short;

            //dtp.CustomFormat = "dd-MM-yyyy";
            dtp.TextChanged += dtp_TextChanged;
        }
        string dtpValue = "";
        void dtp_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = dtp.Text.ToString();
            dataGridView1.CurrentCell.Value = dtpValue = dtp.Text.ToString();

        }

        private void rdAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAddress.Checked)
            {
                string Address = "Company_Address";
                List<MovingMessData> list = AllObj.SelectAllMessMovingSort(Address);
                dataGridView1.DataSource = list;
                for (int i = 0; i < list.Count; i++)
                {
                    string HoldDate = list[i].Hold;
                    try
                    {
                        if (HoldDate != null && HoldDate != "")
                        {
                            dataGridView1.Rows[i].Cells["HoldDate"].Value = DateTime.Parse(HoldDate.ToString()).ToShortDateString();
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                    catch { }
                }
            }
        }

        private void rdAreaSort_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAreaSort.Checked)
            {
                string Area = "Area";
                List<MovingMessData> list = AllObj.SelectAllMessMovingSort(Area);
                dataGridView1.DataSource = list;
                for (int i = 0; i < list.Count; i++)
                {
                    string HoldDate = list[i].Hold;
                    try
                    {
                        if (HoldDate != null && HoldDate != "")
                        {
                            dataGridView1.Rows[i].Cells["HoldDate"].Value = DateTime.Parse(HoldDate.ToString()).ToShortDateString();
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }

                    }
                    catch { }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dtp.Visible = false;
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["HoldDate"].Value != null)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["column_Done"].ReadOnly = true;
                    dataGridView1.Rows[e.RowIndex].Cells["column_Done"].Style.BackColor = Color.Gray;

                    dataGridView1.Rows[e.RowIndex].Cells["Name"].ReadOnly = true;
                    dataGridView1.Rows[e.RowIndex].Cells["Name"].Style.ForeColor = Color.Red;

                    List<MessangerData> listMessNames = abdo.SelectAllMessengersForMoving();
                    cmbMessName.DataSource = listMessNames;
                    dataGridView1.Rows[e.RowIndex].Cells["Name"].Style.NullValue = "Disabeld";


                }
            }
            catch { }

        }

        private void rdCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCompany.Checked)
            {
                string compName = "CompanyName";
                List<MovingMessData> list = AllObj.SelectAllMessMovingSort(compName);
                dataGridView1.DataSource = list;

                for (int i = 0; i < list.Count; i++)
                {
                    string date = list[i].Hold;
                    try
                    {

                        if (date != null && date != "")
                        {
                            dataGridView1.Rows[i].Cells["HoldDate"].Value = DateTime.Parse(date.ToString()).ToShortDateString();

                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }

                    }
                    catch { }
                }
            }
        }

        private void rdDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDate.Checked)
            {
                string date = "Dates";
                List<MovingMessData> list = AllObj.SelectAllMessMovingSort(date);
                dataGridView1.DataSource = list;
                for (int i = 0; i < list.Count; i++)
                {
                    string HoldDate = list[i].Hold;
                    try
                    {
                        if (date != null && date != "")
                        {
                            dataGridView1.Rows[i].Cells["HoldDate"].Value = DateTime.Parse(HoldDate.ToString()).ToShortDateString();
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                    catch { }
                }
            }
        }

        private void btnShowSearch2_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = true;
            grbInformation.Visible = true;
            dtpHold.Visible = false;
            dtpHold.MinDate = DateTime.Now;
        }

        private void btnSaveMoving_Click(object sender, EventArgs e)
        {
            List<int> arr_Row_Deleted = new List<int>();
            if (dataGridView1.Rows.Count > 0)
            {
                MovingMessData obj = new MovingMessData();
                try
                {
                    //------test
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        obj.ReqCode = int.Parse(row.Cells["ReqCode"].Value.ToString());
                        if (row.Cells["HoldDate"].Value != null)
                        {
                            obj.Hold = row.Cells["HoldDate"].Value.ToString();
                            int affected = AllObj.UpdateMessangerRequest_If_Hold(obj, obj.ReqCode);
                            if (affected > 0)
                            {
                                row.DefaultCellStyle.BackColor = Color.Beige;
                                row.DefaultCellStyle.ForeColor = Color.Black;

                            }
                        }
                        else
                        {
                            if (row.Cells["Name"].Value != null && row.Cells["HoldDate"].Value == null)//means he isn't select hold date
                            {
                                obj.MessName = row.Cells["Name"].Value.ToString();
                                obj.CompanyName = row.Cells["CompanyName"].Value.ToString();
                                obj.Branch = row.Cells["Branch"].Value.ToString();
                                obj.Area = row.Cells["Area"].Value.ToString();
                                obj.ContactPerson = row.Cells["ContactPerson"].Value.ToString();
                                obj.Address = row.Cells["Address"].Value.ToString();
                                obj.Phone = row.Cells["Phone"].Value.ToString();
                                obj.Dept = row.Cells["Dept"].Value.ToString();
                                try
                                {
                                    obj.Date = row.Cells["Date"].Value.ToString();
                                }
                                catch { }
                                obj.MessengerType = row.Cells["MessengerType"].Value.ToString();
                                obj.RequestResons = row.Cells["RequestResons"].Value.ToString();
                                //-----mark as moved in Mess_Req to not display again---------
                                int affected2 = AllObj.UpdateMessangerRequest_To_Moved(obj.ReqCode);
                                //-------insert in Messmoving
                                int affected = AllObj.InsertMessangerMoving(obj);
                                if (affected > 0 && affected2 > 0)
                                {
                                    arr_Row_Deleted.Add(obj.ReqCode);
                                }
                            }
                        }
                    }

                    for (int n = dataGridView1.Rows.Count - 1; n >= 0; n--)
                    {
                        int req = int.Parse((dataGridView1.Rows[n].Cells["ReqCode"].Value.ToString()));
                        for (int i = 0; i < arr_Row_Deleted.Count; i++)
                        {
                            if (req == arr_Row_Deleted[i])
                            {
                                dataGridView1.Rows.RemoveAt(n);
                            }
                        }
                    }
                    if (dataGridView1.Rows.Count == 0)
                    {
                        grbSort.Enabled = false;
                    }

                }
                catch { }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int Req = int.Parse(txtReqCodeSearch.Text);
                MovingMessData obj = AllObj.SelectMessengerMovingById(Req);
                if (obj != null)
                {
                    txtReqCode.Text = obj.ReqCode.ToString();
                    txtCompanyName.Text = obj.CompanyName;
                    txtContactPerson.Text = obj.ContactPerson;
                    txtDept.Text = obj.Dept;
                    txtPhone.Text = obj.Phone;
                    txtReqDate.Text = obj.Date.ToString();
                    txtResons.Text = obj.RequestResons;
                    txtCompanyAddress.Text = obj.Address;
                    List<MessangerData> lst = AllObj.SelectAllMessengers();
                    cmbSearchMessNames.DataSource = lst;
                    cmbSearchMessNames.DisplayMember = "Name";
                    cmbSearchMessNames.ValueMember = "Id";


                }
                else
                {
                    MessageBox.Show("غير موجود");
                }
            }
            catch { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (dtpHold.Visible == false)
            {
                int Req = int.Parse(txtReqCode.Text);
                MovingMessData obj = new MovingMessData();
                obj.MessName = cmbSearchMessNames.Text;
                int affedcted = AllObj.UpdateMessengerMoving2(obj, Req);
                if (affedcted >= 1)
                {
                    MessageBox.Show("تمت عملية التحديث");
                    #region Clear
                    txtReqCode.Text = "";
                    txtReqDate.Text = "";
                    txtResons.Text = "";
                    txtPhone.Text = "";
                    txtDept.Text = "";
                    txtContactPerson.Text = "";
                    txtCompanyName.Text = "";
                    txtCompanyAddress.Text = "";
                    dtpHold.Checked = false;
                    #endregion
                }
            }
            else
            {
                MovingMessData obj = new MovingMessData();
                int Req = int.Parse(txtReqCode.Text);
                obj.Hold = dtpHold.Value.ToString();
                int affedcted = AllObj.DeleteMessengerMovig(Req);
                int affected2 = AllObj.UpdateMessangerRequest_If_Hold(obj, Req);

                if (affedcted >= 1 && affected2 >= 1)
                {
                    MessageBox.Show("تمت عملية التحديث");
                    ////-----Refresh DataGridView-----------
                    list3 = AllObj.SelectAllMessMoving();
                    dataGridView1.DataSource = list3;
                    for (int i = 0; i < list3.Count; i++)
                    {
                        string date = list3[i].Hold;
                        try
                        {
                            dataGridView1.Rows[i].Cells["HoldDate"].Value = DateTime.Parse(date.ToString()).ToShortDateString();
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                        catch { }
                    }
                    #region Clear
                    txtReqCode.Text = "";
                    txtReqDate.Text = "";
                    txtResons.Text = "";
                    txtPhone.Text = "";
                    txtDept.Text = "";
                    txtContactPerson.Text = "";
                    txtCompanyName.Text = "";
                    txtCompanyAddress.Text = "";
                    dtpHold.Checked = false;
                    dtpHold.Visible = false;
                    #endregion
                }
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "HoldDate")
            {
                rec = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                dtp.Size = new Size(rec.Width, rec.Height);
                dtp.Location = new Point(rec.X, rec.Y);
                dtp.Visible = true;
            }

        }
        DataGridViewComboBoxColumn cmbMessName;
        BindingList<MovingMessData> list3;
        private void MovingFrm_Load(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
            grbInformation.Visible = false;
            dtpHold.CustomFormat = "dd/MM/yyyy";

            dtpHold.MinDate = DateTime.Now;
            list3 = AllObj.SelectAllMessMoving();
            dataGridView1.DataSource = list3;
            for (int i = 0; i < list3.Count; i++)
            {
                string date = list3[i].Hold;
                try
                {
                    dataGridView1.Rows[i].Cells["HoldDate"].Value = DateTime.Parse(date.ToString()).ToShortDateString();
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                catch { }
            }

            if (list3 != null)
            {
                #region Disaple all columns except MessName to update---------
                dataGridView1.Columns["CompanyName"].ReadOnly = true;
                dataGridView1.Columns["ReqCode"].ReadOnly = true;
                dataGridView1.Columns["Branch"].ReadOnly = true;
                dataGridView1.Columns["ReqCode"].ReadOnly = true;
                dataGridView1.Columns["Area"].ReadOnly = true;
                dataGridView1.Columns["ContactPerson"].ReadOnly = true;
                dataGridView1.Columns["Address"].ReadOnly = true;
                dataGridView1.Columns["Phone"].ReadOnly = true;
                dataGridView1.Columns["Dept"].ReadOnly = true;
                dataGridView1.Columns["Date"].ReadOnly = true;
                dataGridView1.Columns["MessengerType"].ReadOnly = true;
                dataGridView1.Columns["RequestResons"].ReadOnly = true;
                dataGridView1.Columns["RequestResons"].Width = 300;
                dataGridView1.Columns["Hold"].Visible = false;
                dataGridView1.Columns["HoldDate"].DisplayIndex = 13;
                #endregion
                dataGridView1.Columns["MessName"].Visible = false;
                //------------fill all mess Names in comboBox-------------------------
                cmbMessName = new DataGridViewComboBoxColumn();
                List<MessangerData> listMessNames = abdo.SelectAllMessengersForMoving();
                cmbMessName.DataSource = listMessNames;
                cmbMessName.HeaderText = "  اختار اسم المندوب";
                cmbMessName.DataPropertyName = "Name";
                cmbMessName.Name = "Name";
                cmbMessName.DisplayMember = "Name";
                cmbMessName.Width = 200;
                cmbMessName.DefaultCellStyle.NullValue = "--     اختار مندوب     --";
                dataGridView1.Columns.AddRange(cmbMessName);


            }
            else
            {
                MessageBox.Show("لا توجد طلبات جديدة لتحريكها او تم تحريك كل الطلبات بالفعل");
                grbSort.Enabled = false;
                dataGridView1.Controls.Remove(dtp);
                dataGridView1.Columns["HoldDate"].Visible = false;
                dataGridView1.Enabled = false;
                btnSaveMoving.Enabled = false;
            }
        }

        private void MovingFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MovingFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();

        }
        List<string> PreviosMessNameList = new List<string>();

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MovingMessData obj = new MovingMessData();
            try
            {
                obj.MessName = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                if (PreviosMessNameList.Contains(obj.MessName))
                {
                    MessageBox.Show("هذا المندوب تم اختيارة من قبل");
                }
                else
                {
                    PreviosMessNameList.Add(obj.MessName);
                }
            }
            catch { }


            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["HoldDate"].Value != null)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["column_Done"].ReadOnly = true;
                    dataGridView1.Rows[e.RowIndex].Cells["column_Done"].Style.BackColor = Color.Gray;

                    dataGridView1.Rows[e.RowIndex].Cells["Name"].ReadOnly = true;
                    dataGridView1.Rows[e.RowIndex].Cells["Name"].Style.ForeColor = Color.Red;


                    List<MessangerData> listMessNames = abdo.SelectAllMessengersForMoving();
                    cmbMessName.DataSource = listMessNames;
                    dataGridView1.Rows[e.RowIndex].Cells["Name"].Style.NullValue = "Disabeld";


                }
            }
            catch { }

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            dtp.Visible = false;
        }

        private void txtReqCodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dtpHold_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkHold_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHold.Checked)
            {
                dtpHold.Visible = true;
            }
            else
            {
                dtpHold.Visible = false;
            }
        }

    }
}
