
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data.OracleClient;
using System.IO;
using System.Windows.Media.Imaging;
namespace WpfApplication2
{
    public class ReallocationServices
    {
        
        //-----------------Select Reallocate Code-------------------------//
        public static string SelectReallocateCode(int COMP_ID,string CARD_ID,int PROVTYPE,int PROVIDER_ID)
        {
            string query = string.Format("SELECT Code from REALLOCATIONTB where COMP_ID={0} and CARD_ID='{1}' and PROVTYPE={2} and PROVIDER_ID={3}", COMP_ID, CARD_ID, PROVTYPE, PROVIDER_ID);
            object affected = DBManager2.ExecutSelectMax(query);
            return affected.ToString();
        }
        //----------------select All Companies--------------------
        public static List<ReallocationData> SelectAllCompanies()
        {
            string query = "SELECT C_COMP_ID,C_ANAME from V_COMPANIES";
            DataTable res = DBManager2.ExecuteSelect(query).Tables[0];//
            List<ReallocationData> list = new List<ReallocationData>();
            ReallocationData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new ReallocationData();
                    obj.Id = int.Parse(res.Rows[i]["C_COMP_ID"].ToString());
                    obj.Name = res.Rows[i]["C_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //----------------select All Employees Cards---------------
        public static List<ReallocationData> SelectAllEmpCards(string CompId)
        {
            string query = "select distinct CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + CompId + " ORDER BY CARD_ID";
            DataTable res = DBManager2.ExecuteSelect(query).Tables[0];//
            List<ReallocationData> list = new List<ReallocationData>();
            ReallocationData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new ReallocationData();
                    obj.CARD_ID = res.Rows[i]["CARD_ID"].ToString();
                    obj.EMP_ANAME_SC = res.Rows[i]["EMP_ANAME_SC"].ToString();
                    obj.EMP_ANAME_ST = res.Rows[i]["EMP_ANAME_ST"].ToString();
                    obj.EMP_ANAME_TH = res.Rows[i]["EMP_ANAME_TH"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //----------------fill Providers_type----------------------
        public static List<ReallocationData> SelectAllProvTypes()
        {
            string query = "SELECT PRV_TYPE,TYP_ANAME from PROVIDER_TYP";
            DataTable res = DBManager2.ExecuteSelect(query).Tables[0];//
            List<ReallocationData> list = new List<ReallocationData>();
            ReallocationData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new ReallocationData();
                    obj.PRV_TYPE = int.Parse(res.Rows[i]["PRV_TYPE"].ToString());
                    obj.TYP_ANAME = res.Rows[i]["TYP_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //----------------fill ProviderNames-------------------------
        public  static List<ReallocationData> SelectAllProviderNames(int ProvideCode)
        {
            string query = string.Format("select PR_CODE,PR_ANAME from SERV_PROVIDERS where PRV_TYPE=" + ProvideCode);
            DataTable res = DBManager2.ExecuteSelect2(query).Tables[0];//
            List<ReallocationData> list = new List<ReallocationData>();
            ReallocationData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new ReallocationData();
                    obj.Prov_Code = int.Parse(res.Rows[i]["PR_CODE"].ToString());
                    obj.Prov_Name = res.Rows[i]["PR_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
        
        //----------------fill Letter type---------------------------
        public static List<ReallocationData> SelectAllLetterTypes()
        {
            string query = string.Format("select CODE,Name from LETTER_TYPE");
            DataTable res = DBManager2.ExecuteSelect2(query).Tables[0];//
            List<ReallocationData> list = new List<ReallocationData>();
            ReallocationData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new ReallocationData();
                    obj.LetterCode = int.Parse(res.Rows[i]["CODE"].ToString());
                    obj.LetterName = res.Rows[i]["Name"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }

        //--------------- Insert Reallocation-----------------------
        public static int InsertReallocation(ReallocationData obj)
        {
            string userName = User.Name;//abdo
            obj.CREATED_BY = userName;
            int affected = 0;
            
            string query = string.Format(@"INSERT INTO REALLOCATIONTB(COMP_ID,CARD_ID,SERV_DATE,PROVIDER_ID,SERV_NAME,SRV_PRICE,DIAGNOSIS,
                        LETTER_ID,NOTS,CREATED_BY,CREATED_DATE,PROVTYPE) 
                    values({0},'{1}','{2}',{3},'{4}',{5},'{6}',{7},'{8}','{9}','{10}',{11})", 
                    obj.Id, obj.CARD_ID, obj.SERV_DATE, obj.Prov_Code, obj.SERV_NAME, obj.SRV_PRICE, obj.DIAGNOSIS,
                    obj.LETTER_ID, obj.Nots, obj.CREATED_BY, obj.CREATED_DATE, obj.PRV_TYPE);
            try
            {
                 affected = DBManager2.ExecuteNonQuery(query);
                return affected;

            }
            catch (Exception ex) { string ss = ex.Message; }
            return affected;
        }
        //----------------Update FullCode (IdentityId+Date)-----------------------
        public static int UpdateFullCode(string fullCode, string OperationCode)
        {
            int affected = 0;
            string query = string.Format(@"UPDATE app.reallocationtb set FullCode='{0}'   Where CODE={1}", fullCode, OperationCode);
            try
            {
                affected = DBManager2.ExecuteNonQuery(query);

                return affected;

            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            return affected;

        }


        //----------------Update Reallocation-----------------------
        public static int UpdateReallocation(ReallocationData obj, Int64 id)
        {
                string userName = User.Name;//abdo
                obj.UPDATED_BY = userName;
                int affected = 0;
                string query = string.Format(@"UPDATE app.reallocationtb  
                                        set COMP_ID={0},CARD_ID='{1}',
                                        PROVIDER_ID={2},
                                        SERV_NAME='{3}',SRV_PRICE={4},
                                        DIAGNOSIS='{5}',LETTER_ID={6},
                                        NOTS='{7}',UPDATED_BY='{8}',PROVTYPE={9},
                                        REPLY='{10}',AGREAMENT_COST={11},SERV_DATE='{12}',UPDATED_DATE='{13}' 
                                        Where FullCode={14}", obj.Id, obj.CARD_ID,  obj.Prov_Code, obj.SERV_NAME, obj.SRV_PRICE, obj.DIAGNOSIS,
                                                              obj.LETTER_ID, obj.Nots, obj.UPDATED_BY, obj.PRV_TYPE, obj.REPLY, obj.AGREAMENT_COST, obj.SERV_DATE, obj.UPDATED_DATE, id);
                try
                {
                    affected = DBManager2.ExecuteNonQuery(query);
                    return affected;
     
                }
                catch (Exception ex)
                {
                    string ss = ex.Message;
                }
                return affected;

        }
        //----------------Update Reallocation Pic-----------------------
        public static void UpdateReallocationPic(Int64 id, string path)
        {
            FileStream fls;
            fls = new FileStream(@path, FileMode.Open, FileAccess.Read);
            //a byte array to read the image
            byte[] blob = new byte[fls.Length];
            fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
            fls.Close();

            string connstr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
            System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(connstr);
            conn.Open();
            System.Data.OracleClient.OracleCommand cmnd;
            string query;
            query = "UPDATE REALLOCATIONTB set REPLY_PICTURE=:BlobParameter where FullCode="+id;
            System.Data.OracleClient.OracleParameter blobParameters = new System.Data.OracleClient.OracleParameter();
            blobParameters.OracleType = OracleType.Blob;
            blobParameters.ParameterName = "BlobParameter";
            blobParameters.Value = blob;
            cmnd = new System.Data.OracleClient.OracleCommand(query, conn);
            cmnd.Parameters.Add(blobParameters);
            cmnd.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
           
        }
        //----------------Update Reallocation Reply-----------------------
        public static int UpdateReallocationReply(ReallocationData obj, Int64 id)
        {
            string userName = User.Name;//abdo
            obj.UPDATED_BY = userName;
            int affected = 0;
            string query = string.Format(@"UPDATE app.reallocationtb  
                                        set REPLY='{0}',AGREAMENT_COST={1},UPDATED_DATE='{2}' 
                                        Where FullCode={3}",obj.REPLY, obj.AGREAMENT_COST,obj.UPDATED_DATE, id);
            try
            {
                affected = DBManager2.ExecuteNonQuery(query);
                return affected;

            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            return affected;

        }


        //----------------select Select Reallocate By Id--------------------
        public static ReallocationData SelectReallocateById(Int64 id)
        {
            string query = "select * from REALLOCATIONTB where FullCode=" + id + "";
            DataTable res = DBManager2.ExecuteSelect(query).Tables[0];
            ReallocationData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new ReallocationData();
                    obj.Code = int.Parse(dr["Code"].ToString());
                    obj.FullCode = dr["FullCode"].ToString();
                    obj.Id = int.Parse(dr["COMP_ID"].ToString());
                    obj.CARD_ID = dr["CARD_ID"].ToString();
                    obj.SERV_DATE = dr["SERV_DATE"].ToString();
                    try
                    {
                        obj.Prov_Code = int.Parse(dr["PROVIDER_ID"].ToString());
                    }
                    catch { }
                    obj.SERV_NAME = dr["SERV_NAME"].ToString();
                    try {
                        obj.SRV_PRICE = int.Parse(dr["SRV_PRICE"].ToString());
                    }
                    catch { }
                    obj.DIAGNOSIS = dr["DIAGNOSIS"].ToString();
                    try
                    {
                        obj.LETTER_ID = int.Parse(dr["LETTER_ID"].ToString());
                    }
                    catch { }
                    try
                    {
                        obj.PRV_TYPE = int.Parse(dr["PROVTYPE"].ToString());
                    }
                    catch { }
                    obj.Nots = dr["NOTS"].ToString();
                    try
                    {
                        obj.AGREAMENT_COST =int.Parse(dr["AGREAMENT_COST"].ToString());
                    }
                    catch { }

                    obj.REPLY = dr["REPLY"].ToString();
                    try
                    {
                        obj.REPLY_PICTURE = (byte[])dr["REPLY_PICTURE"];
                    }
                    catch { }
                    return obj;
                }
            }
            return null;


        }
        //----------------select Select Reallocate By (FullCode,COMP_ID)--------------------
        public static List<ReallocationData> SelectReallocateBy(string SearchType, Int64 id)
        {
            string query =string.Format("select * from app.REALLOCATIONTB where {0}={1}",SearchType,id);
            DataTable res = DBManager2.ExecuteSelect(query).Tables[0];
            List<ReallocationData> list = new List<ReallocationData>();
            ReallocationData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new ReallocationData();
                    obj.Code = int.Parse(dr["Code"].ToString());
                    obj.FullCode = dr["FullCode"].ToString();
                    obj.Id = int.Parse(dr["COMP_ID"].ToString());
                    obj.CARD_ID = dr["CARD_ID"].ToString();
                    obj.SERV_DATE = dr["SERV_DATE"].ToString();
                    try
                    {
                        obj.Prov_Code = int.Parse(dr["PROVIDER_ID"].ToString());
                    }
                    catch { }
                    obj.SERV_NAME = dr["SERV_NAME"].ToString();
                    try
                    {
                        obj.SRV_PRICE = int.Parse(dr["SRV_PRICE"].ToString());
                    }
                    catch { }
                    obj.DIAGNOSIS = dr["DIAGNOSIS"].ToString();
                    try
                    {
                        obj.LETTER_ID = int.Parse(dr["LETTER_ID"].ToString());
                    }
                    catch { }
                    try
                    {
                        obj.PRV_TYPE = int.Parse(dr["PROVTYPE"].ToString());
                    }
                    catch { }
                    obj.Nots = dr["NOTS"].ToString();
                    try
                    {
                        obj.AGREAMENT_COST = int.Parse(dr["AGREAMENT_COST"].ToString());
                    }
                    catch { }

                    obj.REPLY = dr["REPLY"].ToString();
                    try
                    {
                        obj.REPLY_PICTURE = (byte[])dr["REPLY_PICTURE"];
                    }
                    catch { }
                    try
                    {
                        obj.CREATED_DATE = dr["CREATED_DATE"].ToString();
                    }
                    catch { }
                    try
                    {
                        obj.CREATED_BY = dr["CREATED_BY"].ToString();
                    }
                    catch { }

                    list.Add(obj);
                }
                return list;
            }
            return null;


        }
        //----------------select Select Reallocate By (Created Date)--------------------
        public static List<ReallocationData> SelectReallocateByDate(string SearchType, string date)
        {
            string query = string.Format("select * from app.REALLOCATIONTB where {0}='{1}'", SearchType, date);
            DataTable res = DBManager2.ExecuteSelect(query).Tables[0];
            List<ReallocationData> list = new List<ReallocationData>();
            ReallocationData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new ReallocationData();
                    obj.Code = int.Parse(dr["Code"].ToString());
                    obj.FullCode = dr["FullCode"].ToString();
                    obj.Id = int.Parse(dr["COMP_ID"].ToString());
                    obj.CARD_ID = dr["CARD_ID"].ToString();
                    obj.SERV_DATE = dr["SERV_DATE"].ToString();
                    try
                    {
                        obj.Prov_Code = int.Parse(dr["PROVIDER_ID"].ToString());
                    }
                    catch { }
                    obj.SERV_NAME = dr["SERV_NAME"].ToString();
                    try
                    {
                        obj.SRV_PRICE = int.Parse(dr["SRV_PRICE"].ToString());
                    }
                    catch { }
                    obj.DIAGNOSIS = dr["DIAGNOSIS"].ToString();
                    try
                    {
                        obj.LETTER_ID = int.Parse(dr["LETTER_ID"].ToString());
                    }
                    catch { }
                    try
                    {
                        obj.PRV_TYPE = int.Parse(dr["PROVTYPE"].ToString());
                    }
                    catch { }
                    obj.Nots = dr["NOTS"].ToString();
                    try
                    {
                        obj.AGREAMENT_COST = int.Parse(dr["AGREAMENT_COST"].ToString());
                    }
                    catch { }

                    obj.REPLY = dr["REPLY"].ToString();
                    try
                    {
                        obj.REPLY_PICTURE = (byte[])dr["REPLY_PICTURE"];
                    }
                    catch { }
                    try
                    {
                        obj.CREATED_DATE = dr["CREATED_DATE"].ToString();
                    }
                    catch { }
                    try
                    {
                        obj.CREATED_BY = dr["CREATED_BY"].ToString();
                    }
                    catch { }
                    list.Add(obj);
                }
                return list;
            }
            return null;


        }
        //-------reteive image from sql
        public static BitmapImage BitmapImageFromBytes(byte[] bytes)
        {
            BitmapImage image = null;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                image = new BitmapImage();
                image.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.StreamSource.Seek(0, SeekOrigin.Begin);
                image.EndInit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
            return image;
        }
    }
}
