using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
//using System.Windows.Forms;
using Microsoft.Win32;

namespace WpfApplication2
{ 
    public static class User
    {


        // public static TabItem policyTab { get; set; }
        public static DataTable DtAllCompanys { get; set; }
        public static Page1 page1 { get; set; }
        public static Page3 page3 { get; set; }
        public static page3English page3English { get; set; }
        public static string Mail { get; set; }
        public static string Manegar { get; set; }
        public static string REQUESHR { get; set; }
        public static string Claim { get; set; }
        public static string Opration { get; set; }
        public static string Policy { get; set; }
        public static string FULL_CONTRACT { get; set; }
        public static Button btnLeftMenuShow { get; set; }

        public struct Noti
        {
            public static string HrRequests { get; set; }
        };

        public static int temp;
        /// <summary>
        /// اسم الى داخل على السيستم
        /// </summary>
        public static string Name { get; set; }
        /// <summary>
        /// اسم  شركة الى داخل على السيستم
        /// </summary>
        public static string CompanyName { get; set; }

        /// <summary>
        /// اسم قسم الى داخل على السيستم
        /// </summary>

        public static string Department { get; set; }
        /// <summary>
        /// نوع الواد الى داخل على السيستم
        /// </summary>

        public static string Type { get; set; }
        /// <summary>
        /// رقم شركة الى داخل على السيستم
        /// </summary
        public static string CompanyID { get; set; }

        public static string Company_ASO { get; set; }
        /// <summary>
        /// كود الواد الى داخل على السيستم
        /// </summary
        public static string Code { get; set; }

        /// <summary>
        /// Hr request (y,n)
        /// </summary>
        public static string hr_request { get; set; }

        /// <summary>
        /// شكوى العميل
        /// </summary>
        public static string complainMember { get; set; }

        /// <summary>
        /// الادارة الطبية
        /// </summary>
        public static string medicalManage { get; set; }

        public static string revise { get; set; }
        static DB db = new DB();
        /// <summary>
        /// بترجع داتا تيبل فيها ارقام و اسامى الموظفين الى فشركة الى داخل على السيستم
        ///  CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH 
        /// </summary>
        /// <returns></returns>
        public static DataTable Employee_in_Company() { return db.RunReader(" select distinct   CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE (TERMINATE_FLAG='N' or TERMINATE_FLAG is NULL) and C_COMP_ID=" + CompanyID + " ORDER BY CARD_ID ").Result; }
        /// <summary>
        /// بترجع داتا تيبل فيها ارقام و اسامى الموظفين الى الى كود الشركة باعتو
        ///  CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH 
        /// </summary>
        /// <param name="Company_ID"></param>
        /// <returns></returns>
        public static DataTable Employee_in_Company(string Company_ID) { return db.RunReader(" select distinct   CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE (TERMINATE_FLAG='N' or TERMINATE_FLAG is NULL) and C_COMP_ID=" + Company_ID + " and  ROWNUM <= 50 ORDER BY CARD_ID ").Result; }

        /// <summary>
        /// بترجع داتا تيبل فيها كل اسامى و ارقام الشركات
        /// C_COMP_ID   و  C_ANAME
        /// </summary>
        /// <param name="Company_ID"></param>
        /// <returns> </returns>
        public static DataTable ALL_Company() { return DtAllCompanys; }
        /// <summary>
        /// order Columns in datatable ex: DataTable_Name.SetColumnsOrder("coulomn1","coulomn2","coulomn3");
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnNames"></param>
        public static void SetColumnsOrder(this DataTable table, params String[] columnNames)
        {
            int columnIndex = 0;
            foreach (var columnName in columnNames)
            {
                table.Columns[columnName].SetOrdinal(columnIndex);
                columnIndex++;
            }
        }

        /// <summary>
        /// Array byte  الي BitmapImage تحويل من 
        /// by joba
        /// </summary>
        /// <param name="imageC"></param>
        /// <returns></returns>
        public static byte[] ToArray(this System.Windows.Media.Imaging.BitmapImage imageC)
        {
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            System.Windows.Media.Imaging.JpegBitmapEncoder encoder = new System.Windows.Media.Imaging.JpegBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(imageC));
            encoder.Save(memStream);

            return memStream.ToArray();


        }
        /// <summary>
        /// 
        ///   تضغ البانات الي في الاكسل في تاتا تابل  
        /// By Joba
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable ExcelTODataTable(string filePath)
        {
            try
            {
                DataTable dtexcel = new DataTable();
                bool hasHeaders = false;
                string HDR = hasHeaders ? "Yes" : "No";
                string strConn;
                if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConn);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Closed)
                {

                }
                conn.Close();
                conn.Open();

                //   conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                //Looping Total Sheet of Xl File
                /*foreach (DataRow schemaRow in schemaTable.Rows)
                {
                }*/
                //Looping a first Sheet of Xl File
                DataRow schemaRow = schemaTable.Rows[0];
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_"))
                {
                    string query = "SELECT  * FROM [" + sheet + "]";
                    System.Data.OleDb.OleDbDataAdapter daexcel = new System.Data.OleDb.OleDbDataAdapter(query, conn);
                    dtexcel.Locale = System.Globalization.CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);
                }

                conn.Close();
                return dtexcel;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;

            }



        }



        public static void CleanAll(this Panel s)
        {

            foreach (System.Windows.UIElement child in s.Children)
            {
                GroupBox gb = child as GroupBox;
                if (gb != null)
                    ((Grid)gb.Content).CleanAll();

                Grid dg = child as Grid;
                if (dg != null)
                    dg.CleanAll();
                CheckBox chb = child as CheckBox;
                if (chb != null)
                    chb.IsChecked = false;
                TextBox txt = child as TextBox;
                if (txt != null)
                    txt.Text = "";
                ComboBox cbx = child as ComboBox;
                if (cbx != null)
                    cbx.Text = "";
                DatePicker dp = child as DatePicker;
                if (dp != null)
                    dp.Text = "";

            }

        }

        public static void DeleteDirectory(string target_dir)
        {
            if (Directory.Exists(target_dir))
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    try
                    {

                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }
                    catch (IOException ioex)
                    {


                        System.Windows.MessageBox.Show(ioex.Message);
                    }
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);
            }
        }


    }
}
