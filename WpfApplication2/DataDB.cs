using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Windows;


namespace WpfApplication2
{
    class DataDB
    {
        public static string conction = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";


        //OracleConnection con = new OracleConnection(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
        //                                    (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
        //                                    (SERVICE_NAME=ora11g)));User Id=app;Password=******");

        //OracleCommand cmd = new OracleCommand();

        //OracleDataAdapter da;
        DataTable dd;
        public bool updetopenopration(string comp_id, string card_id, Int64 contact_nom, string spasfec_date)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {
               
                con.Open();

                cmd = new OracleCommand(@"update COMP_EMPLOYEES_NEW set ACTIVE='Y',TERMINATE_FLAG='N' ,SPECIFIC_DATE=:p_stpcifcdate, TERMINATE_DATE = '' ,UPDATE_BY='" + User.Name + "' ,UPDATE_DATE=sysdate where C_COMP_ID=:p_compid and CARD_ID=:p_cardid and contract_no=:p_contactnom", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":p_compid", OracleType.Number).Value = Convert.ToInt64(comp_id);
                cmd.Parameters.Add(":p_contactnom", OracleType.Number).Value = contact_nom;
                cmd.Parameters.Add(":p_cardid", OracleType.VarChar).Value = card_id;
                if (spasfec_date != string.Empty)
                    cmd.Parameters.Add(":p_stpcifcdate", OracleType.DateTime).Value = Convert.ToDateTime(spasfec_date);
                else cmd.Parameters.Add(":p_stpcifcdate", OracleType.DateTime).Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return true;

                //  MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public bool insertoprationchangname(string v_OLD_ANAME, string V_OLD_ENAME, string v_CARD_ID, string v_NEW_ANAME, string v_NEW_ENAME, System.Windows.Controls.Image changphoto, string v_REQUEST_CODE, string v_TRANSAACTION_DATE, string v_reason, string typ)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            //db_new.RunNonQuery("INSERT INTO OPERATION_TRANSACTION(  OLD_ANAME ,OLD_ENAME ,CARD_ID ,CREATED_BY ,CREATED_DATE,NEW_ANAME,NEW_ENAME,) 
            //VALUES ('" +  + "','" +  + "','" + row[1].ToString() + "','" + User.Name + "',SYSDATE ,'" + row[2].ToString() + "','" + row[3].ToString() + "') ") 
            try
            {
                

                cmd = new OracleCommand(@"INSERT INTO OPERATION_TRANSACTION(  OLD_ANAME ,OLD_ENAME ,CARD_ID ,CREATED_BY ,CREATED_DATE,NEW_ANAME,NEW_ENAME,TRANSACTION_PHOTO,TRANSACTION,REQUEST_CODE,REASON,TRANSAACTION_DATE) 
                                                                    VALUES (:p_v_OLD_ANAME,:p_v_OLD_ENAME,:p_v_CARD_ID,'" + User.Name + "',SYSDATE ,:p_v_NEW_ANAME,:p_v_NEW_ENAME,:p_changphoto,:p_typ,:p_v_REQUEST_CODE,:p_v_reason,:p_v_TRANSAACTION_DATE)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":p_v_CARD_ID", OracleType.VarChar).Value = v_CARD_ID;

                cmd.Parameters.Add(":p_v_OLD_ANAME", OracleType.VarChar).Value = v_OLD_ANAME;
                cmd.Parameters.Add(":p_v_OLD_ENAME", OracleType.VarChar).Value = V_OLD_ENAME;
                cmd.Parameters.Add(":p_v_NEW_ANAME", OracleType.VarChar).Value = v_NEW_ANAME;
                cmd.Parameters.Add(":p_v_NEW_ENAME", OracleType.VarChar).Value = v_NEW_ENAME;
                cmd.Parameters.Add(":p_typ", OracleType.VarChar).Value = typ;
                cmd.Parameters.Add(":p_v_reason", OracleType.VarChar).Value = v_reason;
                if (v_REQUEST_CODE != string.Empty)
                    cmd.Parameters.Add(":p_v_REQUEST_CODE", OracleType.Number).Value = Convert.ToInt64(v_REQUEST_CODE);
                else cmd.Parameters.Add(":p_v_REQUEST_CODE", OracleType.Number).Value = DBNull.Value;
                if (v_TRANSAACTION_DATE != string.Empty)
                    cmd.Parameters.Add(":p_v_TRANSAACTION_DATE", OracleType.DateTime).Value = Convert.ToDateTime(v_TRANSAACTION_DATE);
                else cmd.Parameters.Add(":p_v_TRANSAACTION_DATE", OracleType.DateTime).Value = DBNull.Value;
                if (changphoto.Source == null)
                    cmd.Parameters.Add(":p_changphoto", OracleType.Blob).Value = DBNull.Value;

                else
                {
                    cmd.Parameters.Add(":p_changphoto", OracleType.Blob).Value = (changphoto.Source as System.Windows.Media.Imaging.BitmapImage).ToArray();

                }

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return true;

                //  MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable sershnotebooks(string CODE, string TYP_ANAME, string PR_ANAME, string SERIAL_NOM, string SERIAL_NOM_FROM, string SERIAL_NOM_TO, string g, string fromdate, string todate, string sereal)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                if (fromdate != string.Empty || todate != string.Empty)
                {
                    if (sereal == "")
                        cmd = new OracleCommand(@"select CODE,TYP_ANAME,PR_ANAME,ITEM_NAME,DATE_REQUEST,NUM,STATUS STATUSs,SERIAL_NOM,SERIAL_NOM_FROM,SERIAL_NOM_TO,DELIVERY_METHOD from NOTEBOOKS WHERE STATUS !='F'  and CODE like '%" + CODE + "%' and  TYP_ANAME like  '%'||:TYPtANAME ||'%'  and PR_ANAME like '%'||:PRtANAME ||'%'  and SERIAL_NOM like '%" + SERIAL_NOM + "%' and SERIAL_NOM_FROM like '%" + SERIAL_NOM_FROM + "%' and SERIAL_NOM_TO like '%" + SERIAL_NOM_TO + "%' and ITEM_NAME like '%'||:itfem ||'%'  and (to_date(DEVIERED_DATE) between :fromdate and :todate)", con);
                    else
                        cmd = new OracleCommand(@"select CODE,TYP_ANAME,PR_ANAME,ITEM_NAME,DATE_REQUEST,NUM,STATUS STATUSs,SERIAL_NOM,SERIAL_NOM_FROM,SERIAL_NOM_TO,DELIVERY_METHOD from NOTEBOOKS WHERE STATUS !='F'  and CODE like '%" + CODE + "%' and  TYP_ANAME like  '%'||:TYPtANAME ||'%'  and PR_ANAME like '%'||: PRtANAME ||'%'  and SERIAL_NOM like '%" + SERIAL_NOM + "%' and SERIAL_NOM_FROM like '%" + SERIAL_NOM_FROM + "%' and SERIAL_NOM_TO like '%" + SERIAL_NOM_TO + "%' and ITEM_NAME like '%'||:itfem ||'%' and (to_date(DEVIERED_DATE) between :fromdate and :todate) INTERSECT select CODE,TYP_ANAME,PR_ANAME,ITEM_NAME,DATE_REQUEST,NUM,STATUS STATUSs,SERIAL_NOM,SERIAL_NOM_FROM,SERIAL_NOM_TO,DELIVERY_METHOD from NOTEBOOKS   where  (SERIAL_NOM_FROM) <= TO_NUMBER('" + sereal + "' )  AND  TO_NUMBER(SERIAL_NOM_TO) >= TO_NUMBER('" + sereal + "') ", con);
                }
                else
                {
                    if (sereal == "")
                        cmd = new OracleCommand(@"select CODE,TYP_ANAME,PR_ANAME,ITEM_NAME,DATE_REQUEST,NUM,STATUS STATUSs,SERIAL_NOM,SERIAL_NOM_FROM,SERIAL_NOM_TO,DELIVERY_METHOD from NOTEBOOKS WHERE STATUS !='F'  and CODE like '%" + CODE + "%' and  TYP_ANAME like  '%'||:TYPtANAME ||'%'  and PR_ANAME like '%'||:PRtANAME ||'%'  and SERIAL_NOM like '%" + SERIAL_NOM + "%' and SERIAL_NOM_FROM like '%" + SERIAL_NOM_FROM + "%' and SERIAL_NOM_TO like '%" + SERIAL_NOM_TO + "%' and ITEM_NAME like '%'||:itfem ||'%' ", con);
                    else
                        cmd = new OracleCommand(@"select CODE,TYP_ANAME,PR_ANAME,ITEM_NAME,DATE_REQUEST,NUM,STATUS STATUSs,SERIAL_NOM,SERIAL_NOM_FROM,SERIAL_NOM_TO,DELIVERY_METHOD from NOTEBOOKS WHERE STATUS !='F'  and CODE like '%" + CODE + "%' and TYP_ANAME like  '%'||:TYPtANAME ||'%'  and PR_ANAME like '%'||:PRtANAME ||'%'  and SERIAL_NOM like '%" + SERIAL_NOM + "%' and SERIAL_NOM_FROM like '%" + SERIAL_NOM_FROM + "%' and SERIAL_NOM_TO like '%" + SERIAL_NOM_TO + "%' and ITEM_NAME like '%'||:itfem||'%'  INTERSECT select CODE,TYP_ANAME,PR_ANAME,ITEM_NAME,DATE_REQUEST,NUM,STATUS STATUSs,SERIAL_NOM,SERIAL_NOM_FROM,SERIAL_NOM_TO,DELIVERY_METHOD from NOTEBOOKS   where  SERIAL_NOM_FROM <='" + sereal + "'   AND SERIAL_NOM_TO >='" + sereal + "' ", con);
                }

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":itfem", OracleType.VarChar).Value = g;
                cmd.Parameters.Add(":TYPtANAME", OracleType.VarChar).Value = TYP_ANAME;
                cmd.Parameters.Add(":PRtANAME", OracleType.VarChar).Value = PR_ANAME;


                if (fromdate != string.Empty || todate != string.Empty)
                {

                    if (fromdate == string.Empty)
                        cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = Convert.ToDateTime("1-1-1990");
                    else
                        cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = Convert.ToDateTime(fromdate);

                    if (todate == string.Empty)
                        cmd.Parameters.Add(":todate", OracleType.DateTime).Value = DateTime.Now;
                    else
                        cmd.Parameters.Add(":todate", OracleType.DateTime).Value = Convert.ToDateTime(todate);
                }

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                //DB b = new DB();
                //dd = b.RunReader("select CODE,TYP_ANAME,PR_ANAME,ITEM_NAME,DATE_REQUEST,NUM,STATUS STATUSs,SERIAL_NOM,SERIAL_NOM_FROM,SERIAL_NOM_TO,DELIVERY_METHOD from NOTEBOOKS WHERE STATUS !='F'  and CODE like '%" + CODE + "%' and  TYP_ANAME like '%" + TYP_ANAME + "%' and PR_ANAME like '%" + PR_ANAME + "%' and SERIAL_NOM like '%" + SERIAL_NOM + "%' and SERIAL_NOM_FROM like '%" + SERIAL_NOM_FROM + "%' and SERIAL_NOM_TO like '%" + SERIAL_NOM_TO + "%' and ITEM_NAME like '%" + ITEM_NAME + "%'").Result;

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return dd; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //217.139.89..22
        //171.0.1.96 
        //job
        public void select_recol(Int32 comp_id, Int32 contract_nom, string value1, string type1, string user1, DateTime starts, DateTime ends, Int32 id)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                
                con.Open();

                cmd = new OracleCommand(@"update RECOLLECTION_PREMIUM_DATA set COMP_ID=:comp_id,CONTRACT_CO=:contract_nom
,VALUE=:value1,TYPE= :type1 ,UPDATED_BY=:user1 , START_DATE = :zz, END_DATE =:ends WHERE ID =  :id ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":contract_nom", OracleType.Number).Value = contract_nom;
                cmd.Parameters.Add(":value1", OracleType.VarChar).Value = value1;
                cmd.Parameters.Add(":type1", OracleType.VarChar).Value = type1;
                cmd.Parameters.Add(":user1", OracleType.VarChar).Value = user1;
                cmd.Parameters.Add(":zz", OracleType.DateTime).Value = starts;
                cmd.Parameters.Add(":ends", OracleType.DateTime).Value = ends;
                cmd.Parameters.Add(":id", OracleType.Number).Value = id;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //jb
        public void updatstatentracesgin(string DELIVERY_METHOD, string NAME_MESS, string CODE_CONNECTING, string DATEADMINISTRATIONSIGNATURE, string CHECK_NO)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {




                con.Open();

                cmd = new OracleCommand(@" UPDATE A_REP_CHECK_ST  SET STATUS=:STATUS ,ENTRANCESSIGN_BY='" + User.Name + "',ENTRANCESSIGN_DATE=sysdate,DELIVERY_METHOD=:DELIVERY_METHOD,    NAME_MESS=:NAME_MESS,CODE_CONNECTING=:CODE_CONNECTING,DATEADMINISTRATIONSIGNATURE=:DATEADMINISTRATIONSIGNATURE  WHERE CHECK_NO=:CHECK_NO ", con);

                cmd.Parameters.Clear();
                if (DATEADMINISTRATIONSIGNATURE == string.Empty)
                    cmd.Parameters.Add(":DATEADMINISTRATIONSIGNATURE", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":DATEADMINISTRATIONSIGNATURE", OracleType.DateTime).Value = Convert.ToDateTime(DATEADMINISTRATIONSIGNATURE);

                cmd.Parameters.Add(":DELIVERY_METHOD", OracleType.VarChar).Value = DELIVERY_METHOD;
                cmd.Parameters.Add(":CHECK_NO", OracleType.VarChar).Value = CHECK_NO;
                cmd.Parameters.Add(":NAME_MESS", OracleType.VarChar).Value = NAME_MESS;
                cmd.Parameters.Add(":CODE_CONNECTING", OracleType.VarChar).Value = CODE_CONNECTING;
                cmd.Parameters.Add(":STATUS", OracleType.VarChar).Value = "تحت مراجعة الضرائب";

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                // MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        public bool visitInsert(string cod, string fdback, string not, string crtBy, string crtDate, string visit_date, string discount, string prvTyp, string prvNam)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();
                cmd = new OracleCommand(@" insert into VISITE_RESULT (APROVAL_CODE , FEEDBACK , FLAG, NOTE,  CREATED_BY, CREATED_DATE ,VISIT_DATE, DISCOUNT, PROVIDER_TYPE, PROVIDER_NAME) values 
                                                                        (:codevi,     :fdb,      'M', :notevi,:crby,       :crdate, :visitDate, :dicnt, :pTyp, :pNam )", con);
                cmd.Parameters.Clear();


                cmd.Parameters.Add(":codevi", OracleType.VarChar).Value = cod;
                cmd.Parameters.Add(":fdb", OracleType.VarChar).Value = fdback;
                cmd.Parameters.Add(":notevi", OracleType.VarChar).Value = not;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = crtBy;
                if (crtDate != string.Empty)
                    cmd.Parameters.Add(":crdate", OracleType.DateTime).Value = Convert.ToDateTime(crtDate);
                else cmd.Parameters.Add(":crdate", OracleType.DateTime).Value = DBNull.Value;

                if (visit_date != string.Empty)
                    cmd.Parameters.Add(":visitDate", OracleType.DateTime).Value = Convert.ToDateTime(visit_date);
                else cmd.Parameters.Add(":visitDate", OracleType.DateTime).Value = DBNull.Value;

                cmd.Parameters.Add(":dicnt", OracleType.Number).Value = Convert.ToInt64(discount);
                cmd.Parameters.Add(":pTyp", OracleType.VarChar).Value = prvTyp;
                cmd.Parameters.Add(":pNam", OracleType.VarChar).Value = prvNam;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public bool visitDetailsInsert(string aprovCod, string cBy, string cmNum, string prvidNam, string prvidTyp, string fedBck, string clamVal, string suggVal, string vDNtes, string DisC, string Vdate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();
                cmd = new OracleCommand(@" insert into VISITE_RESULT (APROVAL_CODE, CREATED_BY, CREATED_DATE, CLAIM_NUM, PROVIDER_NAME, PROVIDER_TYPE, FEEDBACK, CLAIM_VALUE, SUGGEST_VALUE, VISIT_DETAILS_NOTES, FLAG, DISCOUNT, VISIT_DATE) values 
                                                                        (:coV,     :crtBy,         SYSDATE,  :clmNum,    :prvdNam,   :prvdTyp,       :feedB,     :clmVlu,    :suggVlu,       :vDNotes,           'O', :discnt,    :vstDate   )", con);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":coV", OracleType.VarChar).Value = aprovCod;
                cmd.Parameters.Add(":crtBy", OracleType.VarChar).Value = cBy;
                cmd.Parameters.Add(":clmNum", OracleType.Number).Value = Convert.ToInt64(cmNum);
                cmd.Parameters.Add(":prvdNam", OracleType.VarChar).Value = prvidNam;
                cmd.Parameters.Add(":prvdTyp", OracleType.VarChar).Value = prvidTyp;
                cmd.Parameters.Add(":feedB", OracleType.VarChar).Value = fedBck;
                cmd.Parameters.Add(":clmVlu", OracleType.Number).Value = Convert.ToInt64(clamVal);
                cmd.Parameters.Add(":suggVlu", OracleType.Number).Value = Convert.ToInt64(suggVal);
                cmd.Parameters.Add(":vDNotes", OracleType.VarChar).Value = vDNtes;
                cmd.Parameters.Add(":discnt", OracleType.Number).Value = Convert.ToInt64(DisC);
                if (Vdate != string.Empty)
                    cmd.Parameters.Add(":vstDate", OracleType.DateTime).Value = Convert.ToDateTime(Vdate);
                else cmd.Parameters.Add(":vstDate", OracleType.DateTime).Value = DBNull.Value;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
            return true;
        }

        public DataTable searchVisitInfoByDate(string txtSearch, DateTime fromDate, DateTime ToDate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {
                con.Open();

                DataTable datVisitInfo = new DataTable();
                long txtSV, txtSV1;

                cmd = new OracleCommand(@"select APROVAL_CODE, CLAIM_NUM, PROVIDER_TYPE, PROVIDER_NAME, DISCOUNT, CLAIM_VALUE, SUGGEST_VALUE, VISIT_DATE, FEEDBACK, IMG ,  FLAG 
                                                from VISITE_RESULT where (APROVAL_CODE LIKE '%'|| :txtSinfo ||'%' OR CLAIM_NUM LIKE '%'|| :txtSrinfo ||'%' OR PROVIDER_NAME LIKE '%'||  :txtPN ||'%'  OR PROVIDER_TYPE LIKE '%'|| :txtPT ||'%') and FLAG != 'D' and to_date( VISIT_DATE) between :vDfrom and :vDto ", con);

                cmd.Parameters.Clear();
                if (long.TryParse(txtSearch, out txtSV))
                {
                    cmd.Parameters.Add(":txtSinfo", OracleType.VarChar).Value = txtSV;
                    cmd.Parameters.Add(":txtSrinfo", OracleType.Number).Value = Convert.ToDouble(txtSV);
                }
                else
                {
                    cmd.Parameters.Add(":txtSinfo", OracleType.Number).Value = -1;
                    cmd.Parameters.Add(":txtSrinfo", OracleType.Number).Value = -1;
                }

                cmd.Parameters.Add(":txtPN", OracleType.VarChar).Value = txtSearch;
                cmd.Parameters.Add(":txtPT", OracleType.VarChar).Value = txtSearch;
                cmd.Parameters.Add(":vDfrom", OracleType.DateTime).Value = fromDate;
                cmd.Parameters.Add(":vDto", OracleType.DateTime).Value = ToDate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                foreach (DataRow rowz in dd.Rows)
                {
                    if (rowz[10].ToString() == "M")
                        rowz[10] = "الادارة الطبية";
                    else if (rowz[10].ToString() == "O")
                        rowz[10] = "أخرى";

                }
                return dd;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        //torb28-8 Done
        public void update_comp_employessasa(Int32 comp_id, string card_id, DateTime termin_date)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();

                cmd = new OracleCommand(@"update COMP_EMPLOYEES_NEW set TERMINATE_FLAG='Y' , TERMINATE_DATE =to_date(:termin_date)-1 where C_COMP_ID=:comp_id and CARD_ID=:card_id and contract_no=(select max(CONTRACT_NO) from COMP_EMPLOYEES_NEW where C_COMP_ID=:comp_id) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":card_id", OracleType.VarChar).Value = card_id;
                cmd.Parameters.Add(":termin_date", OracleType.DateTime).Value = termin_date;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                //    MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb28-8 Done
        public void insert_req_Del_operation(string card_id, string comp_id, Int32 max_contra, string class_code, DateTime delete_date, string reason_closs_open, string active, string notez, string created_by, string type, string date_deliver_card, Int32 comp_id_id, Int32 branch_id, string n_codez, string ne_class, string new_card)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();


                cmd = new OracleCommand(@"  insert into CLOSE_EMP_DATA_NEW (CARD_ID,C_COMP_ID,CONTRACT_NO,CLASS_CODE, CLOSE_DATE,     CLOSE_REASON    ,ACTIVE, NOTES, CREATED_BY,CREATED_DATE,TRANS_TYP,WITHDRAW_CARD_DATE,COMP_ID,BRANCH_CODE,N_CODE,N_CLASS,N_CARD,KP_CL,KP_CO)
                                                                 VALUES(:card_id,:comp_id,:max_contra,:class_code,:delete_date,:reason_closs_open,:active,:notez,:created_by,sysdate,:type,:date_deliver_card,     :comp_id_id,:branch_id,:n_codez,:ne_class,:new_card,'Y','Y') ", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":card_id", OracleType.VarChar).Value = card_id;
                cmd.Parameters.Add(":comp_id", OracleType.VarChar).Value = comp_id;
                cmd.Parameters.Add(":max_contra", OracleType.VarChar).Value = max_contra;
                cmd.Parameters.Add(":class_code", OracleType.VarChar).Value = class_code;
                cmd.Parameters.Add(":delete_date", OracleType.DateTime).Value = delete_date;
                cmd.Parameters.Add(":reason_closs_open", OracleType.VarChar).Value = reason_closs_open;
                cmd.Parameters.Add(":active", OracleType.VarChar).Value = active;
                cmd.Parameters.Add(":notez", OracleType.VarChar).Value = notez;
                cmd.Parameters.Add(":new_card", OracleType.VarChar).Value = new_card;
                cmd.Parameters.Add(":created_by", OracleType.VarChar).Value = created_by;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = type;
                cmd.Parameters.Add(":ne_class", OracleType.VarChar).Value = ne_class;
                if (date_deliver_card == string.Empty)
                    cmd.Parameters.Add(":date_deliver_card", OracleType.VarChar).Value = date_deliver_card;
                else
                    cmd.Parameters.Add(":date_deliver_card", OracleType.DateTime).Value = Convert.ToDateTime(date_deliver_card);
                if (n_codez == string.Empty)
                    cmd.Parameters.Add(":n_codez", OracleType.VarChar).Value = n_codez;
                else
                    cmd.Parameters.Add(":n_codez", OracleType.Number).Value = Convert.ToInt32(n_codez);
                cmd.Parameters.Add(":comp_id_id", OracleType.Number).Value = comp_id_id;

                cmd.Parameters.Add(":branch_id", OracleType.Number).Value = branch_id;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

            }
            catch (Exception ex)
            {
                string mess = ex.Message.Split(':')[0];

                if (mess == "ORA-00001")
                    MessageBox.Show("تم حفظ هذه العملية من قبل");
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }


        //torb18-7 Done
        public void update_register_mandob(string name, string national_id, DateTime birth_date, string user_name, string password, string type, string address, Int32 id)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                //


                con.Open();

                cmd = new OracleCommand(@"UPDATE ENUM_RUNNER_DATA SET  RUN_ANAME =:name,RUN_NATIONAL=:national_id
,RUN_BIRTHDATE=:birth_date,RUN_USERNME= :user_name ,RUN_PWD=:password , RUN_TYPE = :type, RUN_ADDRESS =:address, UPDATED_BY ='" + User.Name + "',UPDATED_DATE=sysdate WHERE RUN_ID =  :id ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":name", OracleType.VarChar).Value = name;
                cmd.Parameters.Add(":national_id", OracleType.VarChar).Value = national_id;
                cmd.Parameters.Add(":birth_date", OracleType.DateTime).Value = birth_date;
                cmd.Parameters.Add(":user_name", OracleType.VarChar).Value = user_name;
                cmd.Parameters.Add(":password", OracleType.VarChar).Value = password;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = type;
                cmd.Parameters.Add(":address", OracleType.VarChar).Value = address;
                cmd.Parameters.Add(":id", OracleType.Number).Value = id;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        //torb24-7 Done            
        public void insert_req_mandob(Int32 id, string comp_name, string comp_id, string branch_name, string gover, string area, string contact_person, string comp_address, string dept, DateTime order_date, string order_type, string vip, string another, string reason)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();

                cmd = new OracleCommand(@"  insert into ENUM_RUNNER_ORDER (ORDER_NO,COMP_NAME,COMP_ID,COMP_BRANCH,COMP_GOVER,COMP_AREA,COMP_PERSON, COMP_ADDRESS, DEPT,ORDER_DATE,ORDER_TYP,VIP,ANOTHER_REASON,CREATED_BY,CREATED_DATE,ORDER_NOTES)
                                                                      VALUES(:id,:comp_name  ,:comp_id,:branch_name  ,:gover,:area,:contact_person,:comp_address,:dept,:order_date,:order_type,:vip,:another,'" + User.Name + "',sysdate,:reason) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":id", OracleType.Number).Value = id;
                cmd.Parameters.Add(":comp_name", OracleType.VarChar).Value = comp_name;
                cmd.Parameters.Add(":comp_id", OracleType.VarChar).Value = comp_id;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":gover", OracleType.VarChar).Value = gover;
                cmd.Parameters.Add(":area", OracleType.VarChar).Value = area;
                cmd.Parameters.Add(":contact_person", OracleType.VarChar).Value = contact_person;
                cmd.Parameters.Add(":comp_address", OracleType.VarChar).Value = comp_address;
                cmd.Parameters.Add(":dept", OracleType.VarChar).Value = dept;
                cmd.Parameters.Add(":order_date", OracleType.DateTime).Value = order_date;
                cmd.Parameters.Add(":order_type", OracleType.VarChar).Value = order_type;
                cmd.Parameters.Add(":vip", OracleType.VarChar).Value = vip;
                cmd.Parameters.Add(":another", OracleType.VarChar).Value = another;
                cmd.Parameters.Add(":reason", OracleType.VarChar).Value = reason;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void insert_req_Del_operation(string card_id, string comp_id, Int32 max_contra, Int32 class_code, DateTime delete_date, string reason_closs_open, string active, string notez, string created_by, string type, string date_deliver_card)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                con.Open();


                cmd = new OracleCommand(@"  insert into CLOSE_EMP_DATA (CARD_ID,C_COMP_ID,CONTRACT_NO,CLASS_CODE,CLOSE_DATE,CLOSE_REASON,ACTIVE, NOTES, CREATED_BY,CREATED_DATE,TRANS_TYP)
                                                                 VALUES(:card_id,:comp_id,:max_contra,:class_code,:delete_date,:reason_closs_open,:active,:notez,:created_by,sysdate,:type) ", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":card_id", OracleType.VarChar).Value = card_id;
                cmd.Parameters.Add(":comp_id", OracleType.VarChar).Value = comp_id;
                cmd.Parameters.Add(":max_contra", OracleType.VarChar).Value = max_contra;
                cmd.Parameters.Add(":class_code", OracleType.Number).Value = class_code;
                cmd.Parameters.Add(":delete_date", OracleType.DateTime).Value = delete_date;
                cmd.Parameters.Add(":reason_closs_open", OracleType.VarChar).Value = reason_closs_open;
                cmd.Parameters.Add(":active", OracleType.VarChar).Value = "I";
                cmd.Parameters.Add(":notez", OracleType.VarChar).Value = notez;
                cmd.Parameters.Add(":created_by", OracleType.VarChar).Value = created_by;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = type;
                if (date_deliver_card == string.Empty)
                    cmd.Parameters.Add(":date_deliver_card", OracleType.VarChar).Value = date_deliver_card;
                else
                    cmd.Parameters.Add(":date_deliver_card", OracleType.DateTime).Value = Convert.ToDateTime(date_deliver_card);



                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        //torb25-7 Done
        public void add_new_visit(string visit_id, Int32 provider_code, DateTime visit_date, Int32 technical_id, Int32 visit_ser, Int32 visit_check, Int64 branch_code, string provider_name, string branch_name, string visit_reason, string provider_type, string created_by)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                //



                con.Open();


                cmd = new OracleCommand(@"insert into IMS_VISITS (VISIT_ID, PR_CODE, VISIT_DATE, TECHNICAL_ID, VIS_SER, VIS_CHECKED, BRANCH_CODE
, PR_NAME, BR_NAME, VIS_REASON,PRV_TYPE,CREATED_BY,CREATED_DATE)
                                values(:visit_id,:provider_code,:visit_date,:technical_id ,:visit_ser,:visit_check ,:branch_code ,:provider_name, :branch_name, :visit_reason, :provider_type, :created_by,sysdate) ", con);


                cmd.Parameters.Clear();
                cmd.Parameters.Add(":visit_id", OracleType.Number).Value = visit_id;
                cmd.Parameters.Add(":provider_code", OracleType.Number).Value = provider_code;
                cmd.Parameters.Add(":visit_date", OracleType.DateTime).Value = visit_date;
                cmd.Parameters.Add(":technical_id", OracleType.Number).Value = technical_id;
                cmd.Parameters.Add(":visit_ser", OracleType.Number).Value = visit_ser;
                cmd.Parameters.Add(":visit_check", OracleType.Number).Value = visit_check;
                cmd.Parameters.Add(":branch_code", OracleType.Number).Value = branch_code;
                cmd.Parameters.Add(":provider_name", OracleType.VarChar).Value = provider_name;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":visit_reason", OracleType.VarChar).Value = visit_reason;
                cmd.Parameters.Add(":provider_type", OracleType.VarChar).Value = provider_type;
                cmd.Parameters.Add(":created_by", OracleType.VarChar).Value = created_by;
                MessageBox.Show("تم الحفظ");
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }

        //torb25-7 Done
        public void update_hold_mandob(DateTime hold_date, string req_num)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();

                cmd = new OracleCommand(@"update ENUM_RUNNER_ORDER set HOLD_DATE=:hold_date WHERE ORDER_NO =:req_num ", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":hold_date", OracleType.DateTime).Value = hold_date;
                cmd.Parameters.Add(":req_num", OracleType.VarChar).Value = req_num;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb25-7 Done           
        public void update_req_mandobbs(Int32 id, string comp_name, string comp_id, string branch_name, string gover, string area, string contact_person, string comp_address, string dept, DateTime order_date, string order_type, string vip, string another, string reason)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                //



                con.Open();

                cmd = new OracleCommand(@"UPDATE ENUM_RUNNER_ORDER SET  COMP_NAME =:comp_name,COMP_ID=:comp_id
,COMP_BRANCH=:branch_name,COMP_GOVER= :gover ,COMP_AREA=:area , COMP_PERSON = :contact_person, COMP_ADDRESS =:comp_address,DEPT=:dept,ORDER_DATE=:order_date,ORDER_TYP=:order_type,VIP=:vip,ANOTHER_REASON=:another,ORDER_NOTES=:reason, UPDATED_BY ='" + User.Name + "',UPDATED_DATE=sysdate WHERE ORDER_NO = :idz", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":idz", OracleType.Number).Value = id;
                cmd.Parameters.Add(":comp_name", OracleType.VarChar).Value = comp_name;
                cmd.Parameters.Add(":comp_id", OracleType.VarChar).Value = comp_id;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":gover", OracleType.VarChar).Value = gover;
                cmd.Parameters.Add(":area", OracleType.VarChar).Value = area;
                cmd.Parameters.Add(":contact_person", OracleType.VarChar).Value = contact_person;
                cmd.Parameters.Add(":comp_address", OracleType.VarChar).Value = comp_address;
                cmd.Parameters.Add(":dept", OracleType.VarChar).Value = dept;
                cmd.Parameters.Add(":order_date", OracleType.DateTime).Value = order_date;
                cmd.Parameters.Add(":order_type", OracleType.VarChar).Value = order_type;
                cmd.Parameters.Add(":vip", OracleType.VarChar).Value = vip;
                cmd.Parameters.Add(":another", OracleType.VarChar).Value = another;
                cmd.Parameters.Add(":reason", OracleType.VarChar).Value = reason;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        //torb16-9 
        public DataTable search_reprint_cards(DateTime fromdate, DateTime todate, Int32 comp1, Int32 comp2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"select DISTINCT cd.CARD_COLOUR, c.EMP_CODE,c.CONTRACT_NO, c.EMP_ENAME_ST,c.EMP_ENAME_SC,c.EMP_ENAME_TH,vc.C_ANAME ,c.C_COMP_ID,c.CARD_ID , 'N' Done from app.COMP_EMPLOYEESS c  LEFT JOIN CARDS_DESIGN cd ON c.CLASS_CODE = cd.CLASS_CODE and c.CONTRACT_NO = cd.CONTRACT_NO and c.C_COMP_ID = cd.C_COMP_ID, app.V_COMPANIES vc where c.C_COMP_ID = vc.C_COMP_ID  and (c.CREATED_DATE between  :fromdate and :todate) and (c.C_COMP_ID BETWEEN :comp1 AND :comp2) and  c.CARD_ID   not in (select CARD_ID from PRINT_CARD_CONFIRM where DONE_CHECK = 'Y' )", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'



                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                cmd.Parameters.Add(":comp1", OracleType.Number).Value = comp1;
                cmd.Parameters.Add(":comp2", OracleType.Number).Value = comp2;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        //torb8-8 Done
        public DataTable select_receive_cards(DateTime datez)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                //OracleConnection con;
                //OracleCommand cmd = new OracleCommand();
                //OracleDataAdapter da;
                //con = new OracleConnection(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                //                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                //                            (SERVICE_NAME=ora11g)));User Id=app;Password=******");

                cmd = new OracleCommand(@"select distinct CARD_ID card_id ,to_char(COMP_ID)company_id ,EMP_NAME employee_name ,COMP_NAME company_name
,CONTRACT_NO contract_num ,CREATED_DATE dates
from PRINT_CARD_CONFIRM
where DONE_CHECK='Y' and to_date( CREATED_DATE)= :datez
union 
select  CUSTID card_id,to_char(COMPID) company_id,CUSTNAME employee_name ,COMPANYNAME company_name
,to_char(CONTRACTNO) contract_num,PRINTEDDATE dates
from PRINTINGXXXX
where DELIVERSTATE='Y' and to_date( PRINTEDDATE)= :datez", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":datez", OracleType.DateTime).Value = datez;

                da = new OracleDataAdapter(cmd);
                DataTable dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb8-8 Done
        public DataTable select_receive_cards_todatez(DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                //OracleConnection con;
                //OracleCommand cmd = new OracleCommand();
                //OracleDataAdapter da;
                //con = new OracleConnection(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                //                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                //                            (SERVICE_NAME=ora11g)));User Id=app;Password=******");

                cmd = new OracleCommand(@"select distinct CARD_ID card_id ,to_char(COMP_ID)company_id ,EMP_NAME employee_name ,COMP_NAME company_name
,CONTRACT_NO contract_num ,CREATED_DATE dates
from PRINT_CARD_CONFIRM
where DONE_CHECK='Y' and to_date( CREATED_DATE) between  :fromdate and  :todate
union 
select  CUSTID card_id,to_char(COMPID) company_id,CUSTNAME employee_name ,COMPANYNAME company_name
,to_char(CONTRACTNO) contract_num,PRINTEDDATE dates
from PRINTINGXXXX
where DELIVERSTATE='Y' and to_date( PRINTEDDATE) between  :fromdate and  :todate", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;

                da = new OracleDataAdapter(cmd);
                DataTable dd = new DataTable();

                da.Fill(dd); con.Close();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb16-9 
        public DataTable search_reprint_cards_in(DateTime fromdate, DateTime todate, Int32 comp1, Int32 comp2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                

                //      @"select cd.CARD_COLOUR, c.EMP_CODE,c.CONTRACT_NO, c.EMP_ENAME_ST,c.EMP_ENAME_SC,c.EMP_ENAME_TH,vc.C_ANAME ,c.C_COMP_ID,c.CARD_ID , 'N' Done  where c.C_COMP_ID = vc.C_COMP_ID  and (c.CREATED_DATE between  :fromdate and :todate) and c.CARD_ID   not in (select CARD_ID from PRINT_CARD_CONFIRM where DONE_CHECK = 'Y' )" 
                cmd = new OracleCommand(@"select cd.CARD_COLOUR, c.EMP_CODE,c.CONTRACT_NO, c.EMP_ENAME_ST,c.EMP_ENAME_SC,c.EMP_ENAME_TH,vc.C_ANAME ,c.C_COMP_ID,c.CARD_ID , 'N' Done from app.COMP_EMPLOYEESS c  LEFT JOIN CARDS_DESIGN cd ON c.CLASS_CODE = cd.CLASS_CODE and c.CONTRACT_NO = cd.CONTRACT_NO and c.C_COMP_ID = cd.C_COMP_ID, app.V_COMPANIES vc  where c.C_COMP_ID = vc.C_COMP_ID  and (c.CREATED_DATE between  :fromdate and :todate) and (c.C_COMP_ID BETWEEN :comp1 AND :comp2) and c.CARD_ID   in (select CARD_ID from PRINT_CARD_CONFIRM where DONE_CHECK = 'Y' )", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                cmd.Parameters.Add(":comp1", OracleType.Number).Value = comp1;
                cmd.Parameters.Add(":comp2", OracleType.Number).Value = comp2;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb22-7 Done
        public DataTable search_comboboxxx(DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                

                cmd = new OracleCommand(@"select distinct CREATED_BY  from IRS_APPROVAL where CREATED_DATE  between  :fromdate AND :todate", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb22-7 Done
        public DataTable search_combob2(DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                
                cmd = new OracleCommand(@"select distinct CREATED_BY  from IRS_CLAIM_REC_H where prv_no <> 99999 and CREATED_DATE  between  :fromdate AND :todate", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb22-7 Done
        public DataTable search_cbbbcx(DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                
                cmd = new OracleCommand(@"select distinct CREATED_BY  from IRS_CLAIM_REC_H where prv_no = 99999 and CREATED_DATE    between  :fromdate AND :todate", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb22-7 Done
        public DataTable search_torbx(DateTime fromdate, DateTime todate, string cd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select  distinct CREATED_BY  , count (CLAIM_NO)    from IRS_CLAIM_REC_H where prv_no <> 99999 and CREATED_DATE   between  :fromdate AND :todate and CREATED_BY like  '%'|| :cd ||'%' group by CREATED_BY", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                cmd.Parameters.Add(":cd", OracleType.VarChar).Value = cd;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb22-7 Done
        public DataTable search_nnnsasa(DateTime fromdate, DateTime todate, string cd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                

                cmd = new OracleCommand(@"select distinct CREATED_BY  , count (CLAIM_NO)   from IRS_CLAIM_REC_H where prv_no = 99999 and CREATED_DATE  between :fromdate AND :todate and  CREATED_BY like '%'|| :cd ||'%' group by CREATED_BY", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                cmd.Parameters.Add(":cd", OracleType.VarChar).Value = cd;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb22-7 Done
        public DataTable search_torb_dt(DateTime fromdate, DateTime todate, string cd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                
                if (cd == string.Empty)
                    cmd = new OracleCommand(@"select  CREATED_BY ,IRS_SERVICES_SUPER_GROUP.SUPER_GROUP_ENAME  ,count (APROV_NO)  from IRS_APPROVAL, IRS_SERVICES_SUPER_GROUP where IRS_APPROVAL.APROV_TYP = IRS_SERVICES_SUPER_GROUP.SUPER_GROUP_CODE AND CREATED_DATE  between  :fromdate AND :todate AND CREATED_BY like '%'|| :cd ||'%' group by CREATED_BY  ,IRS_SERVICES_SUPER_GROUP.SUPER_GROUP_ENAME ", con);
                else
                    cmd = new OracleCommand(@"select  CREATED_BY ,IRS_SERVICES_SUPER_GROUP.SUPER_GROUP_ENAME  ,count (APROV_NO)  from IRS_APPROVAL, IRS_SERVICES_SUPER_GROUP where IRS_APPROVAL.APROV_TYP = IRS_SERVICES_SUPER_GROUP.SUPER_GROUP_CODE AND CREATED_DATE  between  :fromdate AND :todate AND CREATED_BY like '%'|| :cd ||'%' group by CREATED_BY  ,IRS_SERVICES_SUPER_GROUP.SUPER_GROUP_ENAME ", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                cmd.Parameters.Add(":cd", OracleType.VarChar).Value = cd;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb22-7 Done
        public DataTable search_cbx_2(DateTime fromdate, DateTime todate, string code)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                
                cmd = new OracleCommand(@"select  CREATED_BY , APROV_TYP,IRS_SERVICES_SUPER_GROUP.SUPER_GROUP_ENAME  ,count (APROV_NO)  from IRS_APPROVAL, IRS_SERVICES_SUPER_GROUP where IRS_APPROVAL.APROV_TYP = IRS_SERVICES_SUPER_GROUP.SUPER_GROUP_CODE AND CREATED_DATE  between :fromdate   AND :todate AND CREATED_BY like '%'|| :code ||'%' group by CREATED_BY ,APROV_TYP ,IRS_SERVICES_SUPER_GROUP.SUPER_GROUP_ENAME ", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                cmd.Parameters.Add(":code", OracleType.VarChar).Value = code;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }



        //torb10-9 Done
        public void insert_reqMandob(Int32 idzz, string name, string national_id, DateTime date_bod, string user_name, string password, string mandob_type, string address)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                
                con.Open();

                cmd = new OracleCommand(@"  insert into ENUM_RUNNER_DATA (RUN_ID,RUN_ANAME,RUN_NATIONAL,RUN_BIRTHDATE,RUN_USERNME,RUN_PWD,RUN_TYPE,RUN_ADDRESS, CREATED_BY, CREATED_DATE) VALUES(:idzz,:name,:national_id,:date_bod,:user_name,:password,:mandob_type,:address,'" + User.Name + "',sysdate) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":idzz", OracleType.Number).Value = idzz;
                cmd.Parameters.Add(":name", OracleType.VarChar).Value = name;
                cmd.Parameters.Add(":national_id", OracleType.VarChar).Value = national_id;
                cmd.Parameters.Add(":date_bod", OracleType.DateTime).Value = date_bod;
                cmd.Parameters.Add(":user_name", OracleType.VarChar).Value = user_name;
                cmd.Parameters.Add(":password", OracleType.VarChar).Value = password;
                cmd.Parameters.Add(":mandob_type", OracleType.VarChar).Value = mandob_type;
                cmd.Parameters.Add(":address", OracleType.VarChar).Value = address;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }


        //jb
        public void insertintoAREPCHECKST(string BATSH_NO, string PROV_ID, string PROV_NAME, string CHECK_NO, string CHECK_AMT, string BATCH_REC_DATE, string FINANCE_REC_DATE, string CHECK_DATE, string SERV_DATE_STETMENT, string CHECK_BANK_NAME, string CHECK_ISSUE_NAME, string GOVERNORATE)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                
                con.Open();

                cmd = new OracleCommand(@"  insert into A_REP_CHECK_ST (CODE,BATSH_NO,PROV_ID,PROV_NAME,CHECK_NO,CHECK_AMT,BATCH_REC_DATE,FINANCE_REC_DATE,CHECK_DATE,SERV_DATE_STETMENT,CHECK_BANK_NAME,CHECK_ISSUE_NAME,GOVERNORATE, CREATED_BY, CREATED_DATE) VALUES((select case WHEN  MAX(CODE) is null THEN 1 ELSE MAX(CODE+1) end from A_REP_CHECK_ST),'" + BATSH_NO + "','" + PROV_ID + "',:PROV_NAME,:CHECK_NO,'" + CHECK_AMT + "',:BATCH_REC_DATE,:FINANCE_REC_DATE,:CHECK_DATE,'" + SERV_DATE_STETMENT + "',:CHECK_BANK_NAME,:CHECK_ISSUE_NAME,:GOVERNORATE,'" + User.Name + "',sysdate) ", con);

                cmd.Parameters.Clear();
                if (BATCH_REC_DATE == string.Empty)
                    cmd.Parameters.Add(":BATCH_REC_DATE", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":BATCH_REC_DATE", OracleType.DateTime).Value = Convert.ToDateTime(BATCH_REC_DATE);


                if (FINANCE_REC_DATE == string.Empty)
                    cmd.Parameters.Add(":FINANCE_REC_DATE", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":FINANCE_REC_DATE", OracleType.DateTime).Value = Convert.ToDateTime(FINANCE_REC_DATE);


                if (CHECK_DATE == string.Empty)
                    cmd.Parameters.Add(":CHECK_DATE", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":CHECK_DATE", OracleType.DateTime).Value = Convert.ToDateTime(CHECK_DATE);


                //cmd.Parameters.Add(":SERV_DATE_STETMENT", OracleType.DateTime).Value = SERV_DATE_STETMENT;


                cmd.Parameters.Add(":PROV_NAME", OracleType.VarChar).Value = PROV_NAME;
                cmd.Parameters.Add(":CHECK_NO", OracleType.VarChar).Value = CHECK_NO;
                cmd.Parameters.Add(":CHECK_BANK_NAME", OracleType.VarChar).Value = CHECK_BANK_NAME;
                cmd.Parameters.Add(":CHECK_ISSUE_NAME", OracleType.VarChar).Value = CHECK_ISSUE_NAME;
                cmd.Parameters.Add(":GOVERNORATE", OracleType.VarChar).Value = GOVERNORATE;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        public void insercheckrecolacation(string CODE, double CHECK_VALUE, Int64 CHECK_NUM, DateTime CHECK_DUE_DATE, DateTime CHECK_RECIEV_DATE)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                

                con.Open();

                //  INSERT INTO APPROV_CHECK(CODE, CHECK_VALUE, CHECK_NUM, CHECK_DUE_DATE, CHECK_RECIEV_DATE, CREATED_BY, CREATED_DATE) VALUES('" + codeapprov + "', '" + txtchickqountaty.Text + "', '" + txtnumcheck.Text + "', '" + date_elastarcheck.Text + "', '" + date_chickrecev.Text + "', '" + User.Name + "', sysdate) ","تم الحفظ");
                cmd = new OracleCommand(@"  INSERT INTO APPROV_CHECK(CODE, CHECK_VALUE, CHECK_NUM, CHECK_DUE_DATE, CHECK_RECIEV_DATE, CREATED_BY, CREATED_DATE) VALUES(:CODE,:CHECK_VALUE,:CHECK_NUM,:CHECK_DUE_DATE,:CHECK_RECIEV_DATE,'" + User.Name + "',sysdate) ", con);

                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":CHECK_RECIEV_DATE", OracleType.DateTime).Value = CHECK_RECIEV_DATE;
                cmd.Parameters.Add(":CODE", OracleType.VarChar).Value = CODE;
                cmd.Parameters.Add(":CHECK_DUE_DATE", OracleType.DateTime).Value = CHECK_DUE_DATE;
                //cmd.Parameters.Add(":datnow", OracleType.DateTime).Value = datnow;
                cmd.Parameters.Add(":CHECK_VALUE", OracleType.Float).Value = CHECK_VALUE;
                cmd.Parameters.Add(":CHECK_NUM", OracleType.Float).Value = CHECK_NUM;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }
        //jb
        public DataTable sershmorga3(string nombatsh, string clemnom, string nomprov, string typprov, string cardnom, string comnom, DateTime fromdate, DateTime todate, string codeel5sm, string typ_mo, string D_SEQ)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (codeel5sm == "")
                    codeel5sm = "%%";

                if (nombatsh == "")
                    nombatsh = "%%";

                if (nomprov == "")
                    nomprov = "%%";

                if (typprov == "")
                    typprov = "%%";

                cmd = new OracleCommand(@"SELECT DISTINCT  CLAIM_NO ,BATSH_NO ,CARD_ID,EMP_ENAME  ,MANAGER  , MAN_LOC ,MAN_IMP ,DISC_CODE_2  ,DISC_CODE_1  ,DISC_NAME  ,CLAIM_NET ,CLAIM_GROSS ,CLAIM_DATE ,CREATED_DATE ,CREATED_TIME  ,CREATED_BY_CODE ,CREATED_BY  ,GROUP_NAME  ,PRV_NO,PRV_NAME, NOTES,D_SEQ  FROM A00_REP_3 where BATSH_NO like'" + nombatsh + "' and  NVL(D_SEQ,0)  like '" + D_SEQ + "' and DISC_CODE_1 like'" + codeel5sm + "' and   NVL(GROUP_NO_DET,18)  like '" + typ_mo + "'  and CLAIM_NO like'%" + clemnom + "%' and PRV_NAME like '" + nomprov + "'   and GROUP_NAME like '" + typprov + "'  and CARD_ID like'%" + cardnom + "%' and  SUBSTR(CARD_ID,0,INSTR(CARD_ID,'-')-1) like'%" + comnom + "%' and    (to_date(CREATED_DATE) between to_date(:fromdate) and to_date(:todate) )", con);
                cmd.Parameters.Clear();
                // cmd.Parameters.Add(":nomprov", OracleType.VarChar).Value = nomprov;
                //  cmd.Parameters.Add(":typprov", OracleType.VarChar).Value = typprov;
                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public DataTable sershmorga32(string nombatsh, string clemnom, string nomprov, string typprov, string cardnom, string comnom, DateTime fromdate, DateTime todate, string chnum)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (nombatsh == "")
                    nombatsh = "%%";

                cmd = new OracleCommand(@"SELECT  a.CLAIM_NO ,a.BATSH_NO ,a.CARD_ID,a.EMP_ENAME  ,a.MANAGER  , a.MAN_LOC ,a.MAN_IMP ,a.DISC_CODE_2  ,a.DISC_CODE_1  ,a.DISC_NAME  ,a.CLAIM_NET ,a.CLAIM_GROSS ,a.CLAIM_DATE ,a.CREATED_DATE ,a.CREATED_TIME  ,a.CREATED_BY_CODE ,a.CREATED_BY  ,a.GROUP_NAME  ,a.PRV_NO,PRV_NAME, a.NOTES  ,b.CHECK_NO,b.CHECK_DATE FROM A00_REP_2 a , A_REP_CHECK b where a.BATSH_NO =b.BATSH_NO and a.BATSH_NO like'" + nombatsh + "' and a.CLAIM_NO like'%" + clemnom + "%' and a.PRV_NAME like '%' || :nomprov ||'%'   and a.GROUP_NAME like '%' || :typprov ||'%'  and a.CARD_ID like'%" + cardnom + "%' and  SUBSTR(a.CARD_ID,0,INSTR(a.CARD_ID,'-')-1) like'%" + comnom + "%' and b.CHECK_NO like'%" + chnum + "%'  and   (to_date(a.CREATED_DATE) between to_date(:fromdate) and to_date(:todate) )", con);
                // cmd = new OracleCommand(@"SELECT RE_SEQ ,CLAIM_NO  رقم__المطالبة,BATSH_NO  رقم__الباتش,CARD_ID  رقم_الكارت,MANAGER  نوع__الخدمة , MAN_LOC ,MAN_IMP ,DISC_CODE_2  كود__الخصم ,DISC_CODE_1  كود_الخصم__الاول ,DISC_NAME  اسباب__الخصم ,CLAIM_NET  الصافي,CLAIM_GROSS  الاجمالي,CLAIM_DATE  تاريخ__الخدمة,CREATED_DATE  تاريخ__التسجيل,CREATED_TIME  وقت__التسجيل ,CREATED_BY_CODE  كود__المستخدم,CREATED_BY  اسم__المسنخدم ,GROUP_NAME   اسم__الجروب  FROM A00_REP_2 where BATSH_NO like'%" + nombatsh+ "%' and CLAIM_NO like'%" + clemnom + "%' and CARD_ID like'%" + cardnom+ "%' and  SUBSTR(CARD_ID,0,INSTR(CARD_ID,'-')-1) like'%" + comnom+ "%' and  (to_date(CREATED_DATE) between :fromdate and :todate )", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":nomprov", OracleType.VarChar).Value = nomprov;
                cmd.Parameters.Add(":typprov", OracleType.VarChar).Value = typprov;
                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //joba24-9
        public DataTable searchhrproviderv(string codehrprovid, DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                cmd = new OracleCommand(@"SELECT CODE ,STATE_NAME , AREA_NAME,PROVIDER_TYPE ,NEW_PROV_NAME ,NEW_PROV_ADD ,CONTACT_PERSON ,CONTACT_PHONE ,CREATED_BY ,CREATED_DATE ,POPULATION_NUM  FROM HR_PROVIDERS_REQUEST WHERE REPLAYED='W'     and CODE like '%" + codehrprovid + "%' and  (to_date(CREATED_DATE) between :fromdate and :todate)", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //joba24-9
        public DataTable sershta3qtad(string codehrprovid, DateTime fromdate, DateTime todate, string stat)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                //SELECT CODE, BS_ANAME, AREA_NAME, PROVIDER_TYPE, NEW_PROV_NAME, NEW_PROV_ADD, CONTACT_PERSON, CONTACT_PHONE, CREATED_BY, CREATED_DATE, POPULATION_NUM, decode(REPLAYED,'W', 'Wait', 'w', 'Wait', 'F', 'Refuse', 'f', 'Refuse', 'Y', 'Accept', 'y', 'Accept', '') State ,REPLAY ,REASON_REFUSE FROM HR_PROVIDERS_REQUEST,AREA_VIEW WHERE   CODE like '%" +codehrprovid + "%'  and HR_PROVIDERS_REQUEST.STATE_NAME = AREA_VIEW.BS_CODE and  (to_date(CREATED_DATE) between :fromdate and :todate) and REPLAYED like '"+ stat+"'
                cmd = new OracleCommand(@"SELECT CODE  ,STATE_NAME  ,AREA_NAME  ,PROVIDER_TYPE  ,NEW_PROV_NAME  ,NEW_PROV_ADD  ,CONTACT_PERSON ,CONTACT_PHONE ,CREATED_BY ,CREATED_DATE  ,POPULATION_NUM  ,decode( REPLAYED,'W','Wait','w','Wait','F','Refuse','f','Refuse','Y','Accept','y','Accept','')  ,REPLAY ,REASON_REFUSE  FROM HR_PROVIDERS_REQUEST WHERE   CODE like '%" + codehrprovid + "%'  and  (to_date(CREATED_DATE) between :fromdate and :todate) and upper( REPLAYED) like '%" + stat + "%' ", con);
                // cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //   and CODE like '%" + codehrprovid + "%'


                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }
        public DataTable hr3lagmonth(string card_idn, string typed, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {//SELECT ID , TYPE,CARD_ID,CREATED_DATE,CREATED_BY,NOTES,REQUEST_TYP,REPLAY FROM ENUM_REQUESTS WHERE  state='2' and (CARD_ID like'%" + txthrrequqwprint4.Text + "' OR  ID like'%" + txthrrequqwprint4.Text + "') and (REQ_TYPE='c'  or   REQ_TYPE='C' ) and (CREATED_DATE between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' )
                int xconvert;

                if (User.Type == "hr")
                    cmd = new OracleCommand(@"SELECT ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,decode( STATE,'0','',REPLAY ) REPLAY, decode(STATE,'2','pending','1','accept','0','refuse','' ,PR_ENAME,TYPE) STATE  FROM ENUM_REQUESTS  WHERE    (CARD_ID like '%'||:card||'%'  OR  ID = :codeh ) and (REQ_TYPE='c'  or   REQ_TYPE='C' ) and to_date(REQ_DATE) between to_date(:dateFrom) and to_date(:dateto)  and REQUEST_TYP like '%'|| :typeh ||'%' and SUBSTR(CARD_ID,0,INSTR(CARD_ID,'-')-1)='" + User.CompanyID + "' and EMP_ENAME='" + User.Name + "'", con);
                else
                    cmd = new OracleCommand(@"SELECT ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,decode( STATE,'0','',REPLAY ) REPLAY, decode(STATE,'2','pending','1','accept','0','refuse','',PR_ENAME,TYPE) STATE  FROM ENUM_REQUESTS  WHERE    (CARD_ID like '%'||:card||'%'  OR  ID =:codeh ) and (REQ_TYPE='c'  or   REQ_TYPE='C' ) and to_date(REQ_DATE) between to_date(:dateFrom) and to_date(:dateto)  and REQUEST_TYP like '%'|| :typeh ||'%' and EMP_ENAME='" + User.Name + "'", con);
                cmd.Parameters.Clear();
                if (int.TryParse(card_idn, out xconvert))
                {

                    cmd.Parameters.Add(":codeh", OracleType.Number).Value = xconvert;
                }
                else
                {
                    cmd.Parameters.Add(":codeh", OracleType.Number).Value = DBNull.Value;

                }
                cmd.Parameters.Add(":card", OracleType.VarChar).Value = card_idn;
                cmd.Parameters.Add(":typeh", OracleType.VarChar).Value = typed;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                dd.Columns[0].ColumnName = "كود الطلب";
                dd.Columns[1].ColumnName = "نوع الطلب";
                dd.Columns[2].ColumnName = "رقم الكارت";
                dd.Columns[3].ColumnName = "تاريخ الطلب";
                dd.Columns[4].ColumnName = "بواسطة";
                dd.Columns[5].ColumnName = "ملاحظات";
                dd.Columns[6].ColumnName = "عن طريق";
                dd.Columns[7].ColumnName = "الرد";
                dd.Columns[8].ColumnName = "حالة الطلب";
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //joba1t7-9
        public DataTable dms3lagmonth(string card_idn, string typed, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {//SELECT ID , TYPE,CARD_ID,CREATED_DATE,CREATED_BY,NOTES,REQUEST_TYP,REPLAY FROM ENUM_REQUESTS WHERE  state='2' and (CARD_ID like'%" + txthrrequqwprint4.Text + "' OR  ID like'%" + txthrrequqwprint4.Text + "') and (REQ_TYPE='c'  or   REQ_TYPE='C' ) and (CREATED_DATE between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' )
                int xconvert;

                //ID , TYPE,CARD_ID,CREATED_DATE,CREATED_BY,NOTES,REQUEST_TYP,REPLAY
                cmd = new OracleCommand(@"SELECT ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,decode( STATE,'0','',REPLAY ) REPLAY  FROM ENUM_REQUESTS  WHERE  STATE='2' and  (CARD_ID like '%'||:card||'%'  OR  ID  = :codeh ) and (REQ_TYPE='c'  or   REQ_TYPE='C' ) and to_date(REQ_DATE) between to_date(:dateFrom) and to_date(:dateto)  and REQUEST_TYP like '%'|| :typeh ||'%'", con);
                cmd.Parameters.Clear();
                if (int.TryParse(card_idn, out xconvert))
                {

                    cmd.Parameters.Add(":codeh", OracleType.Number).Value = xconvert;
                }
                else
                {
                    cmd.Parameters.Add(":codeh", OracleType.Number).Value = DBNull.Value;

                }
                cmd.Parameters.Add(":card", OracleType.VarChar).Value = card_idn;
                cmd.Parameters.Add(":typeh", OracleType.VarChar).Value = typed;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                dd.Columns[0].ColumnName = "كود الطلب";
                dd.Columns[1].ColumnName = "نوع الطلب";
                dd.Columns[2].ColumnName = "رقم الكارت";
                dd.Columns[3].ColumnName = "تاريخ الطلب";
                dd.Columns[4].ColumnName = "بواسطة";
                dd.Columns[5].ColumnName = "ملاحظات";
                dd.Columns[6].ColumnName = "عن طريق";
                dd.Columns[7].ColumnName = "الرد";

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb30-7   
        public void add_complains(string id, Int32 com_ser, string branch_code, Int32 provider_code, Int32 subject_code, string problem, DateTime com_date, string created_by, string provider_name, string branch_name, string provider_type, string times, int prv_typ_code, string CUST_PHONE, char PROBLEM_TYP)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;


            try
            {



                con.Open();

                if (branch_code == string.Empty)
                {
                    cmd = new OracleCommand(@"INSERT INTO IMS_COMPLAINTS (COMPLAINT_ID, COM_SER,PROVIDER_CODE, SUBJECT_CODE ,PROPLEM,COM_DATE, CREATED_BY,COM_CHECKED,PROVIDER_NAME,BRANCH_NAME,PROVIDER_TYPE,TIME,PROVIDR_TYPE_CODE,CUST_PHONE,PROBLEM_TYP) VALUES 
                                                                  (:id,       :com_ser,:provider_code,:subject_code,:problem,:com_date,:created_by,  'N'  ,:provider_name, :branch_name,:provider_type,:times,:prv_typ_code,'" + CUST_PHONE + "','" + PROBLEM_TYP + "')", con);

                }
                else
                {

                    cmd = new OracleCommand(@"INSERT INTO IMS_COMPLAINTS (COMPLAINT_ID, COM_SER, BRANCH_CODE,PROVIDER_CODE, SUBJECT_CODE ,PROPLEM,COM_DATE, CREATED_BY,COM_CHECKED,PROVIDER_NAME,BRANCH_NAME,PROVIDER_TYPE,TIME,PROVIDR_TYPE_CODE,CUST_PHONE,PROBLEM_TYP ) VALUES 
                                                                  (:id,       :com_ser,:branch_code,:provider_code,:subject_code,:problem,:com_date,:created_by,  'N'  ,:provider_name, :branch_name,:provider_type,:times,:prv_typ_code,'" + CUST_PHONE + "','" + PROBLEM_TYP + "')", con);
                }
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleType.VarChar).Value = id;
                cmd.Parameters.Add(":com_ser", OracleType.Number).Value = com_ser;
                if (branch_code != string.Empty)
                    cmd.Parameters.Add(":branch_code", OracleType.Number).Value = Convert.ToInt64(branch_code);
                cmd.Parameters.Add(":provider_code", OracleType.Number).Value = provider_code;

                cmd.Parameters.Add(":subject_code", OracleType.Number).Value = subject_code;
                cmd.Parameters.Add(":problem", OracleType.VarChar).Value = problem;
                cmd.Parameters.Add(":com_date", OracleType.DateTime).Value = com_date;

                cmd.Parameters.Add(":created_by", OracleType.VarChar).Value = created_by;
                cmd.Parameters.Add(":provider_name", OracleType.VarChar).Value = provider_name;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":provider_type", OracleType.VarChar).Value = provider_type;
                cmd.Parameters.Add(":times", OracleType.VarChar).Value = times;
                cmd.Parameters.Add(":prv_typ_code", OracleType.Number).Value = prv_typ_code;
                // cmd.Parameters.Add(":problem_solution", OracleType.VarChar).Value = problem_solution;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم الحفظ بنجاح");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void add_complains(string id, Int32 com_ser, string branch_code, Int32 provider_code, Int32 subject_code, string problem, DateTime com_date, string created_by, string provider_name, string branch_name, string provider_type, string times, int prv_typ_code)
        {


            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {



                con.Open();

                if (branch_code == string.Empty)
                {
                    cmd = new OracleCommand(@"INSERT INTO IMS_COMPLAINTS (COMPLAINT_ID, COM_SER,PROVIDER_CODE, SUBJECT_CODE ,PROPLEM,COM_DATE, CREATED_BY,COM_CHECKED,PROVIDER_NAME,BRANCH_NAME,PROVIDER_TYPE,TIME,PROVIDR_TYPE_CODE) VALUES 
                                                                  (:id,       :com_ser,:provider_code,:subject_code,:problem,:com_date,:created_by,  'N'  ,:provider_name, :branch_name,:provider_type,:times,:prv_typ_code)", con);

                }
                else
                {

                    cmd = new OracleCommand(@"INSERT INTO IMS_COMPLAINTS (COMPLAINT_ID, COM_SER, BRANCH_CODE,PROVIDER_CODE, SUBJECT_CODE ,PROPLEM,COM_DATE, CREATED_BY,COM_CHECKED,PROVIDER_NAME,BRANCH_NAME,PROVIDER_TYPE,TIME,PROVIDR_TYPE_CODE) VALUES 
                                                                  (:id,       :com_ser,:branch_code,:provider_code,:subject_code,:problem,:com_date,:created_by,  'N'  ,:provider_name, :branch_name,:provider_type,:times,:prv_typ_code)", con);
                }
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleType.VarChar).Value = id;
                cmd.Parameters.Add(":com_ser", OracleType.Number).Value = com_ser;
                if (branch_code != string.Empty)
                    cmd.Parameters.Add(":branch_code", OracleType.Number).Value = Convert.ToInt64(branch_code);
                cmd.Parameters.Add(":provider_code", OracleType.Number).Value = provider_code;

                cmd.Parameters.Add(":subject_code", OracleType.Number).Value = subject_code;
                cmd.Parameters.Add(":problem", OracleType.VarChar).Value = problem;
                cmd.Parameters.Add(":com_date", OracleType.DateTime).Value = com_date;

                cmd.Parameters.Add(":created_by", OracleType.VarChar).Value = created_by;
                cmd.Parameters.Add(":provider_name", OracleType.VarChar).Value = provider_name;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":provider_type", OracleType.VarChar).Value = provider_type;
                cmd.Parameters.Add(":times", OracleType.VarChar).Value = times;
                cmd.Parameters.Add(":prv_typ_code", OracleType.Number).Value = prv_typ_code;
                // cmd.Parameters.Add(":problem_solution", OracleType.VarChar).Value = problem_solution;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم الحفظ بنجاح");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        //torb23-5 Done



        public DataTable select_motlbat_fardya(Int32 indemty, DateTime ff, DateTime tt)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;


            try
            {




                cmd = new OracleCommand(@"SELECT CHECK_NO  FROM IND_DATA where ( COMPNY_ID like '%'|| :indemty ||'%'   ) and (ARCHIVE_RECEIPT_DATE between :ff and :tt) ", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":indemty", OracleType.Number).Value = indemty;
                // cmd.Parameters.Add(":typee", OracleType.VarChar).Value = typee;

                cmd.Parameters.Add(":ff", OracleType.DateTime).Value = ff;
                cmd.Parameters.Add(":tt", OracleType.DateTime).Value = tt;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb30-7
        public void update_register_complains(string prv_type, Int64 provider_code, string prv_name, string bran_code, string bran_name, int subject_code, string solved_by, string escaled_to, string problem, string path, string time, DateTime com_date, string updated_by, string complain_id, string solve_problem, char PROBLEM_TYP, string phone)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {
                con.Open();

                cmd = new OracleCommand(@"UPDATE IMS_COMPLAINTS SET PROVIDER_TYPE=:prv_type,PROVIDER_CODE=:provider_code,PROVIDER_NAME=:prv_name,
                                      BRANCH_CODE =:bran_code,CUST_PHONE='" + phone + @"',BRANCH_NAME=:bran_name,SUBJECT_CODE=:subject_code,SOLVED_BY= :solved_by 
                       ,ESCLATED_TO=:escaled_to ,PROBLEM_TYP='" + PROBLEM_TYP + @"', PROPLEM = :problem, COMM_ATTACH =:path, TIME =:time, COM_DATE =:com_date,UPDATED_BY=:updated_by,PROBLEM_SOLUTION=:solve_problem 
                            WHERE COMPLAINT_ID =  :complain_id ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":prv_type", OracleType.VarChar).Value = prv_type;
                cmd.Parameters.Add(":provider_code", OracleType.Number).Value = provider_code;
                cmd.Parameters.Add(":prv_name", OracleType.VarChar).Value = prv_name;
                if (bran_code != string.Empty)
                {
                    cmd.Parameters.Add(":bran_code", OracleType.Number).Value = Convert.ToInt32(bran_code);
                }
                else
                {
                    cmd.Parameters.Add(":bran_code", OracleType.VarChar).Value = bran_code;
                }
                cmd.Parameters.Add(":bran_name", OracleType.VarChar).Value = bran_name;
                cmd.Parameters.Add(":subject_code", OracleType.Number).Value = subject_code;
                cmd.Parameters.Add(":solved_by", OracleType.VarChar).Value = solved_by;
                if (escaled_to != string.Empty)
                {
                    cmd.Parameters.Add(":escaled_to", OracleType.Number).Value = Convert.ToInt32(escaled_to);
                }
                else
                {
                    cmd.Parameters.Add(":escaled_to", OracleType.VarChar).Value = escaled_to;
                }
                cmd.Parameters.Add(":problem", OracleType.VarChar).Value = problem;
                cmd.Parameters.Add(":path", OracleType.VarChar).Value = path;
                cmd.Parameters.Add(":time", OracleType.VarChar).Value = time;
                cmd.Parameters.Add(":com_date", OracleType.DateTime).Value = com_date;
                cmd.Parameters.Add(":updated_by", OracleType.VarChar).Value = updated_by;
                cmd.Parameters.Add(":complain_id", OracleType.VarChar).Value = complain_id;
                cmd.Parameters.Add(":solve_problem", OracleType.VarChar).Value = solve_problem;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void update_visits(string prv_type, Int32 provider_code, string provider_name, Int32 branch_code, string branch_name, Int32 technical_code, string reason, DateTime visit_date, string updated_by, string visit_id)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                con.Open();

                cmd = new OracleCommand(@"UPDATE IMS_VISITS SET  PRV_TYPE =:prv_type,PR_CODE=:provider_code
,PR_NAME=:provider_name,BRANCH_CODE= :branch_code ,BR_NAME=:branch_name , TECHNICAL_ID = :technical_code, VIS_REASON =:reason,VISIT_DATE=:visit_date, UPDATED_BY =:updated_by WHERE VISIT_ID =  :visit_id ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":prv_type", OracleType.VarChar).Value = prv_type;
                cmd.Parameters.Add(":provider_code", OracleType.Number).Value = provider_code;
                cmd.Parameters.Add(":provider_name", OracleType.VarChar).Value = provider_name;
                cmd.Parameters.Add(":branch_code", OracleType.Number).Value = branch_code;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":technical_code", OracleType.Number).Value = technical_code;
                cmd.Parameters.Add(":reason", OracleType.VarChar).Value = reason;
                cmd.Parameters.Add(":visit_date", OracleType.DateTime).Value = visit_date;
                cmd.Parameters.Add(":updated_by", OracleType.VarChar).Value = updated_by;
                cmd.Parameters.Add(":visit_id", OracleType.VarChar).Value = visit_id;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");
                // da = new OracleDataAdapter(cmd);
                //DataTable dd = new DataTable();

                //  da.Fill(dd);con.Close();
                //   return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        //torb2-7 
        public void update_companies_group(Int32 comp_id, Int32 contract_nom, int area, string industryz, string user1, DateTime starts, DateTime ends, Int32 id)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {

                con.Open();

                cmd = new OracleCommand(@"update COMPANIES_GROUP set COMP_ID=:comp_id,CONTRACT_NUM=:contract_nom
,AREA=:area,INDUSTRY= :industryz ,UPDATED_BY=:user1 , START_DATE = :starts, END_DATE =:ends WHERE ID =  :id ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":contract_nom", OracleType.Number).Value = contract_nom;
                cmd.Parameters.Add(":area", OracleType.Number).Value = area;
                cmd.Parameters.Add(":industryz", OracleType.VarChar).Value = industryz;
                cmd.Parameters.Add(":user1", OracleType.VarChar).Value = user1;
                cmd.Parameters.Add(":starts", OracleType.DateTime).Value = starts;
                cmd.Parameters.Add(":ends", OracleType.DateTime).Value = ends;
                cmd.Parameters.Add(":id", OracleType.Number).Value = id;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        //torb2-7
        public void update_recollect_data(Int32 comp_id, Int32 contract_nom, string value1, string type1, string user1, DateTime starts, DateTime ends, Int32 id)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();

                cmd = new OracleCommand(@"update RECOLLECTION_PREMIUM_DATA set COMP_ID=:comp_id,CONTRACT_CO=:contract_nom
,PREMIUM=:value1,RECOLLECTION= :type1 ,UPDATED_BY=:user1 , START_DATE = :starts, END_DATE =:ends WHERE ID =  :id ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":contract_nom", OracleType.Number).Value = contract_nom;
                cmd.Parameters.Add(":value1", OracleType.VarChar).Value = value1;
                cmd.Parameters.Add(":type1", OracleType.VarChar).Value = type1;
                cmd.Parameters.Add(":user1", OracleType.VarChar).Value = user1;
                cmd.Parameters.Add(":starts", OracleType.DateTime).Value = starts;
                cmd.Parameters.Add(":ends", OracleType.DateTime).Value = ends;
                cmd.Parameters.Add(":id", OracleType.Number).Value = id;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        //torb30-7    
        public void update_complains(string branch_code, Int64 provider_code, Int32 subject_code, string problem, DateTime com_date, string provider_name, string branch_name, string prv_type, string updated_by, string complain_id, char PROBLEM_TYP)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();

                cmd = new OracleCommand(@"UPDATE IMS_COMPLAINTS SET  BRANCH_CODE =:branch_code,PROVIDER_CODE=:provider_code
,SUBJECT_CODE=:subject_code,PROPLEM= :problem ,COM_DATE=:com_date , PROVIDER_NAME = :provider_name, BRANCH_NAME =:branch_name, PROVIDER_TYPE =:prv_type, UPDATED_BY =:updated_by ,PROBLEM_TYP='" + PROBLEM_TYP + "' WHERE COMPLAINT_ID =  :complain_id ", con);

                cmd.Parameters.Clear();
                if (branch_code != string.Empty)
                    cmd.Parameters.Add(":branch_code", OracleType.Number).Value = Convert.ToInt64(branch_code);
                else
                    cmd.Parameters.Add(":branch_code", OracleType.VarChar).Value = branch_code;
                cmd.Parameters.Add(":provider_code", OracleType.Number).Value = provider_code;
                cmd.Parameters.Add(":subject_code", OracleType.Number).Value = subject_code;
                cmd.Parameters.Add(":problem", OracleType.VarChar).Value = problem;
                cmd.Parameters.Add(":com_date", OracleType.DateTime).Value = com_date;
                cmd.Parameters.Add(":provider_name", OracleType.VarChar).Value = provider_name;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":prv_type", OracleType.VarChar).Value = prv_type;
                cmd.Parameters.Add(":updated_by", OracleType.VarChar).Value = updated_by;
                cmd.Parameters.Add(":complain_id", OracleType.VarChar).Value = complain_id;
                //   cmd.Parameters.Add(":solve_problem", OracleType.VarChar).Value = solve_problem;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools(); ;

                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void update_complains(string branch_code, Int64 provider_code, Int32 subject_code, string problem, DateTime com_date, string provider_name, string branch_name, string prv_type, string updated_by, string complain_id, string CUST_PHONE, char PROBLEM_TYP)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {
                con.Open();

                cmd = new OracleCommand(@"UPDATE IMS_COMPLAINTS SET  BRANCH_CODE =:branch_code,PROVIDER_CODE=:provider_code
,SUBJECT_CODE=:subject_code,PROPLEM= :problem ,COM_DATE=:com_date , PROVIDER_NAME = :provider_name, CUST_PHONE='" + CUST_PHONE + "', BRANCH_NAME =:branch_name, PROVIDER_TYPE =:prv_type, UPDATED_BY =:updated_by ,PROBLEM_TYP='" + PROBLEM_TYP + "' WHERE COMPLAINT_ID =  :complain_id ", con);

                cmd.Parameters.Clear();
                if (branch_code != string.Empty)
                    cmd.Parameters.Add(":branch_code", OracleType.Number).Value = Convert.ToInt64(branch_code);
                else
                    cmd.Parameters.Add(":branch_code", OracleType.VarChar).Value = branch_code;
                cmd.Parameters.Add(":provider_code", OracleType.Number).Value = provider_code;
                cmd.Parameters.Add(":subject_code", OracleType.Number).Value = subject_code;
                cmd.Parameters.Add(":problem", OracleType.VarChar).Value = problem;
                cmd.Parameters.Add(":com_date", OracleType.DateTime).Value = com_date;
                cmd.Parameters.Add(":provider_name", OracleType.VarChar).Value = provider_name;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":prv_type", OracleType.VarChar).Value = prv_type;
                cmd.Parameters.Add(":updated_by", OracleType.VarChar).Value = updated_by;
                cmd.Parameters.Add(":complain_id", OracleType.VarChar).Value = complain_id;
                //   cmd.Parameters.Add(":solve_problem", OracleType.VarChar).Value = solve_problem;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        //torb17-7 Done         
        public void update_schedule(string comp_name, string branch, string emp_name, string client, string visit_reason, string timeez, string updated_b, string reason_edit, DateTime visit_date, Int32 visit_id)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"update  SCHUDLE set COMP_NAME=:comp_name,BRANCH_NAME=:branch,EMP_NAME=:emp_name,CLIENT_NAME=:client,VISIT_REASON=:visit_reason,TIME=:timeez,UPDATED_BY=:updated_b,REASON_EDIT=:reason_edit,VISIT_DATE=:visit_date,UPDATED_DATE=sysdate where VISIT_ID=" + visit_id + " ", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":comp_name", OracleType.VarChar).Value = comp_name;
                cmd.Parameters.Add(":branch", OracleType.VarChar).Value = branch;
                cmd.Parameters.Add(":emp_name", OracleType.VarChar).Value = emp_name;
                cmd.Parameters.Add(":client", OracleType.VarChar).Value = client;
                cmd.Parameters.Add(":visit_reason", OracleType.VarChar).Value = visit_reason;
                cmd.Parameters.Add(":timeez", OracleType.VarChar).Value = timeez;
                cmd.Parameters.Add(":updated_b", OracleType.VarChar).Value = updated_b;
                cmd.Parameters.Add(":reason_edit", OracleType.VarChar).Value = reason_edit;
                cmd.Parameters.Add(":visit_date", OracleType.DateTime).Value = visit_date;

                //   cmd.Parameters.Add(":visit_id", OracleType.Number).Value = visit_id;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        //torb17-7 Done
        //torb27-9
        public bool save_schudle(Int32 visit_id, string comp_name, string branch_name, string emp_name, string cont_person, string visit_reason, DateTime visit_date, string timez, string created_by)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"insert into SCHUDLE (VISIT_ID,COMP_NAME,BRANCH_NAME,EMP_NAME,CLIENT_NAME,VISIT_REASON,VISIT_DATE
                                 ,TIME,CREATED_BY)
                                             values(:visit_id,:comp_name, :branch_name,:emp_name , :cont_person,:visit_reason ,:visit_date ,:timez, :created_by) ", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":visit_id", OracleType.Number).Value = visit_id;
                cmd.Parameters.Add(":comp_name", OracleType.VarChar).Value = comp_name;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":emp_name", OracleType.VarChar).Value = emp_name;
                cmd.Parameters.Add(":cont_person", OracleType.VarChar).Value = cont_person;
                cmd.Parameters.Add(":visit_reason", OracleType.VarChar).Value = visit_reason;
                cmd.Parameters.Add(":visit_date", OracleType.DateTime).Value = visit_date;
                cmd.Parameters.Add(":timez", OracleType.VarChar).Value = timez;
                cmd.Parameters.Add(":created_by", OracleType.VarChar).Value = created_by;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }

        public void add_new_visit(string visit_id, Int32 provider_code, DateTime visit_date, Int32 technical_id, Int32 visit_ser, Int32 visit_check, Int32 branch_code, string provider_name, string branch_name, string visit_reason, string provider_type, string created_by)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                //



                con.Open();


                cmd = new OracleCommand(@"insert into IMS_VISITS (VISIT_ID, PR_CODE, VISIT_DATE, TECHNICAL_ID, VIS_SER, VIS_CHECKED, BRANCH_CODE
, PR_NAME, BR_NAME, VIS_REASON,PRV_TYPE,CREATED_BY,CREATED_DATE)
                                values(:visit_id,:provider_code,:visit_date,:technical_id ,:visit_ser,:visit_check ,:branch_code ,:provider_name, :branch_name, :visit_reason, :provider_type, :created_by,sysdate) ", con);


                cmd.Parameters.Clear();
                cmd.Parameters.Add(":visit_id", OracleType.Number).Value = visit_id;
                cmd.Parameters.Add(":provider_code", OracleType.Number).Value = provider_code;
                cmd.Parameters.Add(":visit_date", OracleType.DateTime).Value = visit_date;
                cmd.Parameters.Add(":technical_id", OracleType.Number).Value = technical_id;
                cmd.Parameters.Add(":visit_ser", OracleType.Number).Value = visit_ser;
                cmd.Parameters.Add(":visit_check", OracleType.Number).Value = visit_check;
                cmd.Parameters.Add(":branch_code", OracleType.Number).Value = branch_code;
                cmd.Parameters.Add(":provider_name", OracleType.VarChar).Value = provider_name;
                cmd.Parameters.Add(":branch_name", OracleType.VarChar).Value = branch_name;
                cmd.Parameters.Add(":visit_reason", OracleType.VarChar).Value = visit_reason;
                cmd.Parameters.Add(":provider_type", OracleType.VarChar).Value = provider_type;
                cmd.Parameters.Add(":created_by", OracleType.VarChar).Value = created_by;
                MessageBox.Show("تم الحفظ");
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }
        //torb13-5

        //jb
        public DataTable select_motlbat_fardya(string indemty, string typee, DateTime ff, DateTime tt)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {
                cmd = new OracleCommand(@"SELECT CODE ,BATCH_NO ,COMPANY_NAME ,FROM_CLAIM ,TO_CLAIM ,CHECK_NO ,TRANS_CODE ,CHECK_VALUE , CHECK_NO ,CLAIM_VALUE ,TITLE  ,CHECK_BANK ,ACCOUNTING_NOTES  FROM IND_DATA where ( COMPNY_ID like '%" + indemty + "%' and CHECK_NO like '%'|| :typee ||'%'  ) and (ARCHIVE_RECEIPT_DATE between :ff and :tt) ", con);

                cmd.Parameters.Clear();


                // cmd.Parameters.Add(":indemty", OracleType.Number).Value = indemty;
                cmd.Parameters.Add(":typee", OracleType.VarChar).Value = typee;

                cmd.Parameters.Add(":ff", OracleType.DateTime).Value = ff;
                cmd.Parameters.Add(":tt", OracleType.DateTime).Value = tt;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb18-9 Done
        public DataTable medical_select_search44(string search, Int32 comp_id, string name, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), COMP_EMPLOYEESS.EMP_ENAME_ST || ' ' || COMP_EMPLOYEESS.EMP_ENAME_SC || ' ' || COMP_EMPLOYEESS.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE, COMP_EMPLOYEESS   WHERE EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID AND (EMPLOYEE_REQUEST.REQUEST_CODE like '%' ||:search || '%' or  EMPLOYEE_REQUEST.CARD_ID like'%' ||:search || '%') AND   EMPLOYEE_REQUEST.CREATED_BY = :name  and EMPLOYEE_REQUEST.COMP_ID =:comp_id and(to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'  and EMPLOYEE_REQUEST.COMP_ID =:comp_id and(to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) AND (EMPLOYEE_REQUEST.REQUEST_CODE like '%' ||:search || '%' or  EMPLOYEE_REQUEST.CARD_ID like'%' ||:search || '%') ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":search", OracleType.VarChar).Value = search;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":name", OracleType.VarChar).Value = name;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //torb24-4 Done
        //torb18-9 Move
        public DataTable medical_select_search33(string comp_id, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), COMP_EMPLOYEESS.EMP_ENAME_ST || ' ' || COMP_EMPLOYEESS.EMP_ENAME_SC || ' ' || COMP_EMPLOYEESS.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE, COMP_EMPLOYEESS   WHERE EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID  AND   EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'  and EMPLOYEE_REQUEST.COMP_ID =:comp_id and(to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'  and EMPLOYEE_REQUEST.COMP_ID =:comp_id and(to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto)", con);

                cmd.Parameters.Clear();
                // cmd.Parameters.Add(":search", OracleType.VarChar).Value = search;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":comp_id", OracleType.VarChar).Value = comp_id;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools(); ;
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb10-7 Done
        //torb18-9 Done
        public DataTable medical_select_search888(Int32 typeee, Int32 comp_id, string card, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID ,EMPLOYEE_REQUEST.EMP_CLASS ,EMPLOYEE_REQUEST.EMP_CLASS_REASON ,EMPLOYEE_REQUEST.CREATED_BY,EMPLOYEE_REQUEST.CREATED_DATE ,decode(EMPLOYEE_REQUEST.REGISTER_TYPE,'P','Desktop', 'p','Desktop','Mobile') ,COMP_EMPLOYEES.EMP_ENAME_ST||' '||COMP_EMPLOYEES.EMP_ENAME_SC||' '||COMP_EMPLOYEES.EMP_ENAME_TH,EMPLOYEE_REQUEST_TYPE.TYPE_NAME  ,decode(EMPLOYEE_REQUEST.APPROVE_FLAG ,'y','Accepted','Y','Accepted','n','Pending','N','Pending','w','Under Processing','W','Under Processing','f','Rejected','F','Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST,EMPLOYEE_REQUEST_TYPE,COMP_EMPLOYEES   WHERE EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID AND  EMPLOYEE_REQUEST.CREATED_BY='" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:typeee and (EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:card || '%' or EMPLOYEE_REQUEST.card_id like'%' ||:card || '%') and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) and  EMPLOYEE_REQUEST.COMP_ID =:comp_id union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "' and  (EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:card || '%')   and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) and  EMPLOYEE_REQUEST.COMP_ID =:comp_id  ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":typeee", OracleType.Number).Value = typeee;
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;

                cmd.Parameters.Add(":card", OracleType.VarChar).Value = card;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb10-7 Done
        //torb18-9 Done
        public DataTable medical_select_search102(int search, Int32 typeee, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID ,EMPLOYEE_REQUEST.EMP_CLASS ,EMPLOYEE_REQUEST.EMP_CLASS_REASON ,EMPLOYEE_REQUEST.CREATED_BY,EMPLOYEE_REQUEST.CREATED_DATE ,decode(EMPLOYEE_REQUEST.REGISTER_TYPE,'P','Desktop', 'p','Desktop','Mobile') ,COMP_EMPLOYEES.EMP_ENAME_ST||' '||COMP_EMPLOYEES.EMP_ENAME_SC||' '||COMP_EMPLOYEES.EMP_ENAME_TH,EMPLOYEE_REQUEST_TYPE.TYPE_NAME  ,decode(EMPLOYEE_REQUEST.APPROVE_FLAG ,'y','Accepted','Y','Accepted','n','Pending','N','Pending','w','Under Processing','W','Under Processing','f','Rejected','F','Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST,EMPLOYEE_REQUEST_TYPE,COMP_EMPLOYEES   WHERE EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID AND  EMPLOYEE_REQUEST.CREATED_BY='" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:typeee and (EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:search || '%' or EMPLOYEE_REQUEST.card_id like'%' ||:search || '%') and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "' and  (EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:search || '%' or EMPLOYEE_REQUEST.card_id like'%' ||:search || '%')   and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":typeee", OracleType.Number).Value = typeee;
                cmd.Parameters.Add(":search", OracleType.Number).Value = search;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb18-9 Done
        public DataTable medical_select_Print(string qury, Int32 comp1, Int32 comp2, Int32 typ1, Int32 typ2, DateTime dat1, DateTime dat2, string cod1, string cod2, string crd1, string crd2)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(qury + @"                                         
                                            AND EMPLOYEE_REQUEST.TYPE BETWEEN :typ1 AND :typ2
                                            AND EMPLOYEE_REQUEST.REQUEST_CODE BETWEEN :cod1 AND :cod2
                                            AND NVL(EMPLOYEE_REQUEST.CARD_ID, '0') BETWEEN :crd1 AND :crd2
                                            AND EMPLOYEE_REQUEST.COMP_ID BETWEEN :comp1 AND :comp2
                                            AND to_date(EMPLOYEE_REQUEST.CREATED_DATE) BETWEEN :dat1 AND :dat2", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":comp1", OracleType.Number).Value = comp1;
                cmd.Parameters.Add(":comp2", OracleType.Number).Value = comp2;
                cmd.Parameters.Add(":typ1", OracleType.Number).Value = typ1;
                cmd.Parameters.Add(":typ2", OracleType.Number).Value = typ2;
                cmd.Parameters.Add(":cod1", OracleType.Number).Value = Convert.ToInt32(cod1);
                cmd.Parameters.Add(":cod2", OracleType.Number).Value = Convert.ToInt32(cod2);
                cmd.Parameters.Add(":crd1", OracleType.VarChar).Value = crd1;
                cmd.Parameters.Add(":crd2", OracleType.VarChar).Value = crd2;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return dd; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb10-7 Done
        //torb18-9 Done
        public DataTable datacheck503(Int32 bat1, Int32 bat2, Int32 prv1, Int32 prv2, Int32 larg, Int32 smal, string chk1, string chk2, DateTime dat1, DateTime dat2, DateTime dat3, DateTime dat4, string stat)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (stat == string.Empty)
                    cmd = new OracleCommand(@"SELECT    A_REP_CHECK_03.BATSH_NO, A_REP_CHECK_03.PROV_ID, A_REP_CHECK_03.PROV_NAME, A_REP_CHECK_03.AFTER_REVIEW_AMT, A_REP_CHECK_03.SUB_AMT, A_REP_CHECK_03.ADD_AMT, A_REP_CHECK_03.F_REV_AFT_ADD_SUB, A_REP_CHECK_03.PROV_ADMIN_FEES, 
                                                        A_REP_CHECK_03.F_ADMIN_FEES, A_REP_CHECK_03.F_REV_AFT_ADM, A_REP_CHECK_03.TAX, A_REP_CHECK_03.F_TAX_AMT, A_REP_CHECK_03.F_REV_AFT_TAX_D
                                                FROM    APP.A_REP_CHECK, APP.A_REP_CHECK_03
                                                WHERE   A_REP_CHECK.BATSH_NO = A_REP_CHECK_03.BATSH_NO
                                                    AND A_REP_CHECK.PROV_ID = A_REP_CHECK_03.PROV_ID                                                    
                                                    AND A_REP_CHECK.BATSH_NO BETWEEN :bat1 AND :bat2
                                                    AND A_REP_CHECK.PROV_ID BETWEEN :prv1 AND :prv2
                                                    AND NVL(A_REP_CHECK.CHECK_AMT,0) BETWEEN :smal AND :larg
                                                     AND NVL(CHECK_NO,'0') BETWEEN :chk1 AND :chk2
                                                    AND A_REP_CHECK.BATCH_REC_DATE BETWEEN :dat1 AND :dat2
                                                    AND NVL(A_REP_CHECK.FINANCE_REC_DATE, TO_DATE('02-01-2016', 'dd-MM-yyyy')) BETWEEN :dat3 AND :dat4", con);
                else
                    cmd = new OracleCommand(@"SELECT    A_REP_CHECK_03.BATSH_NO, A_REP_CHECK_03.PROV_ID, A_REP_CHECK_03.PROV_NAME, A_REP_CHECK_03.AFTER_REVIEW_AMT, A_REP_CHECK_03.SUB_AMT, A_REP_CHECK_03.ADD_AMT, A_REP_CHECK_03.F_REV_AFT_ADD_SUB, A_REP_CHECK_03.PROV_ADMIN_FEES, 
                                                        A_REP_CHECK_03.F_ADMIN_FEES, A_REP_CHECK_03.F_REV_AFT_ADM, A_REP_CHECK_03.TAX, A_REP_CHECK_03.F_TAX_AMT, A_REP_CHECK_03.F_REV_AFT_TAX_D
                                                FROM    APP.A_REP_CHECK, APP.A_REP_CHECK_03
                                                WHERE   A_REP_CHECK.BATSH_NO = A_REP_CHECK_03.BATSH_NO
                                                    AND A_REP_CHECK.PROV_ID = A_REP_CHECK_03.PROV_ID                                                    
                                                    AND A_REP_CHECK.BATSH_NO BETWEEN :bat1 AND :bat2
                                                    AND A_REP_CHECK.PROV_ID BETWEEN :prv1 AND :prv2
                                                    AND NVL(A_REP_CHECK.CHECK_AMT,0) BETWEEN :smal AND :larg
                                                     AND NVL(CHECK_NO,'0') BETWEEN :chk1 AND :chk2
                                                    AND A_REP_CHECK.BATCH_REC_DATE BETWEEN :dat1 AND :dat2
                                                    AND NVL(A_REP_CHECK.FINANCE_REC_DATE, TO_DATE('02-01-2016', 'dd-MM-yyyy')) BETWEEN :dat3 AND :dat4
                                                    AND A_REP_CHECK.STAT = :stat", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":bat1", OracleType.Number).Value = bat1;
                cmd.Parameters.Add(":bat2", OracleType.Number).Value = bat2;
                cmd.Parameters.Add(":prv1", OracleType.Number).Value = prv1;
                cmd.Parameters.Add(":prv2", OracleType.Number).Value = prv2;
                cmd.Parameters.Add(":larg", OracleType.Number).Value = larg;
                cmd.Parameters.Add(":smal", OracleType.Number).Value = smal;
                cmd.Parameters.Add(":chk1", OracleType.VarChar).Value = chk1;
                cmd.Parameters.Add(":chk2", OracleType.VarChar).Value = chk2;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dat3", OracleType.DateTime).Value = dat3;
                cmd.Parameters.Add(":dat4", OracleType.DateTime).Value = dat4;

                if (stat != string.Empty)
                    cmd.Parameters.Add(":stat", OracleType.VarChar).Value = stat;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        public DataTable sumarybatch505(Int32 bat1, Int32 bat2, Int32 prv1, Int32 prv2, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@" SELECT   A_REP_CHECK.BATSH_NO, A_REP_CHECK.PROV_ID, A_REP_CHECK.PROV_NAME, A_REP_CHECK.BATCH_REC_DATE, A_REP_CHECK.BATCH_REC_AMT, (TOT_MAN_LOC + TOT_MAN_IMP - PERCENT_MONY_M) TOT_AFTER, (TOT_DIS_IMP + TOT_DIS_LOC) TOT_DIS, ((TOT_MAN_LOC + TOT_MAN_IMP) - (PERCENT_MONY_M + TOT_DIS_IMP + TOT_DIS_LOC)) AFTER_DIS,
                                                    (TOT_DIV_IMP + TOT_DIV_LOC) TOT_DIV, ((TOT_MAN_LOC + TOT_MAN_IMP) - (PERCENT_MONY_M + TOT_DIS_IMP + TOT_DIS_LOC + TOT_DIV_IMP + TOT_DIV_LOC)) AFTER_DIV, /*PERCENT_MONY_M,
                                                    A_REP_CHECK_03.AFTER_REVIEW_AMT,*/ A_REP_CHECK_03.SUB_AMT + (A_REP_CHECK.BATCH_REC_AMT - AMT_MAN_TOT) SUB_AMT, A_REP_CHECK_03.ADD_AMT, A_REP_CHECK_03.F_REV_AFT_ADD_SUB, /*A_REP_CHECK_03.PROV_ADMIN_FEES, */
                                                    A_REP_CHECK_03.F_ADMIN_FEES, A_REP_CHECK_03.F_REV_AFT_ADM, /*A_REP_CHECK_03.TAX,*/ A_REP_CHECK_03.F_TAX_AMT, A_REP_CHECK_03.F_REV_AFT_TAX_D 
                                           FROM     APP.A_REP_CHECK, APP.A_REP_CHECK_03, APP.SUM_MAN
                                           WHERE     A_REP_CHECK.BATSH_NO = A_REP_CHECK_03.BATSH_NO
                                                AND A_REP_CHECK.PROV_ID = A_REP_CHECK_03.PROV_ID
                                                AND A_REP_CHECK.BATSH_NO = SUM_MAN.BATSH_NO           
                                                AND A_REP_CHECK.BATSH_NO BETWEEN :bat1 AND :bat2
                                                AND A_REP_CHECK.PROV_ID BETWEEN :prv1 AND :prv2             
                                                AND BATCH_REC_DATE BETWEEN :dat1 AND :dat2", con);


                cmd.Parameters.Clear();

                cmd.Parameters.Add(":bat1", OracleType.Number).Value = bat1;
                cmd.Parameters.Add(":bat2", OracleType.Number).Value = bat2;
                cmd.Parameters.Add(":prv1", OracleType.Number).Value = prv1;
                cmd.Parameters.Add(":prv2", OracleType.Number).Value = prv2;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        public DataTable medical_select_search777(Int32 search, Int32 comp_id, string card, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                //  AND(EMPLOYEE_REQUEST.REQUEST_CODE like '%' ||:card || '%' or  EMPLOYEE_REQUEST.CARD_ID like'%' ||:card || '%')
                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), COMP_EMPLOYEESS.EMP_ENAME_ST || ' ' || COMP_EMPLOYEESS.EMP_ENAME_SC || ' ' || COMP_EMPLOYEESS.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE, COMP_EMPLOYEESS   WHERE EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID AND (EMPLOYEE_REQUEST.REQUEST_CODE like '%' ||:card || '%' or  EMPLOYEE_REQUEST.CARD_ID like'%' ||:card || '%') AND   EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:search and EMPLOYEE_REQUEST.COMP_ID =:comp_id and(to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":search", OracleType.Number).Value = search;
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                // cmd.Parameters.Add(":code", OracleType.Number).Value = code;
                cmd.Parameters.Add(":card", OracleType.VarChar).Value = card;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }

            }

        }
        //torb18-9 Done
        public DataTable medical_select_search666(Int32 search, Int32 comp_id, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@" select distinct EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID ,EMPLOYEE_REQUEST.EMP_CLASS ,EMPLOYEE_REQUEST.EMP_CLASS_REASON ,EMPLOYEE_REQUEST.CREATED_BY,EMPLOYEE_REQUEST.CREATED_DATE ,decode(EMPLOYEE_REQUEST.REGISTER_TYPE,'P','Desktop', 'p','Desktop','Mobile') ,COMP_EMPLOYEES.EMP_ENAME_ST||' '||COMP_EMPLOYEES.EMP_ENAME_SC||' '||COMP_EMPLOYEES.EMP_ENAME_TH,EMPLOYEE_REQUEST_TYPE.TYPE_NAME  ,decode(EMPLOYEE_REQUEST.APPROVE_FLAG ,'y','Accepted','Y','Accepted','n','Pending','N','Pending','w','Under Processing','W','Under Processing','f','Rejected','F','Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST,EMPLOYEE_REQUEST_TYPE,COMP_EMPLOYEES   WHERE EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID AND  EMPLOYEE_REQUEST.CREATED_BY='" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:search and EMPLOYEE_REQUEST.COMP_ID=:comp_id and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id)  union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected') ,PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'  and EMPLOYEE_REQUEST.TYPE =:search and  EMPLOYEE_REQUEST.COMP_ID =:comp_id and(to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":search", OracleType.Number).Value = search;
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb10-7 Done
        //torb18-9 Done
        public DataTable medical_select_search100(int search, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), COMP_EMPLOYEESS.EMP_ENAME_ST || ' ' || COMP_EMPLOYEESS.EMP_ENAME_SC || ' ' || COMP_EMPLOYEESS.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE, COMP_EMPLOYEESS   WHERE EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID AND (EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:search || '%') AND   EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'  and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "' and EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:search || '%'   and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":search", OracleType.Number).Value = search;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb10-7 Done
        //torb18-9 Done
        public DataTable medical_select_search101(string search, Int32 typeee, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID ,EMPLOYEE_REQUEST.EMP_CLASS ,EMPLOYEE_REQUEST.EMP_CLASS_REASON ,EMPLOYEE_REQUEST.CREATED_BY,EMPLOYEE_REQUEST.CREATED_DATE ,decode(EMPLOYEE_REQUEST.REGISTER_TYPE,'P','Desktop', 'p','Desktop','Mobile') ,COMP_EMPLOYEES.EMP_ENAME_ST||' '||COMP_EMPLOYEES.EMP_ENAME_SC||' '||COMP_EMPLOYEES.EMP_ENAME_TH,EMPLOYEE_REQUEST_TYPE.TYPE_NAME  ,decode(EMPLOYEE_REQUEST.APPROVE_FLAG ,'y','Accepted','Y','Accepted','n','Pending','N','Pending','w','Under Processing','W','Under Processing','f','Rejected','F','Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST,EMPLOYEE_REQUEST_TYPE,COMP_EMPLOYEES   WHERE EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID AND  EMPLOYEE_REQUEST.CREATED_BY='" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:typeee and (EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:search || '%' or EMPLOYEE_REQUEST.card_id like'%' ||:search || '%') and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "' and  (EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:search || '%' or EMPLOYEE_REQUEST.card_id like'%' ||:search || '%')   and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":search", OracleType.VarChar).Value = search;
                cmd.Parameters.Add(":typeee", OracleType.Number).Value = typeee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb24-4 Done
        public void deleteEmployee(string crd, DateTime delvdat, string flg, DateTime tmdat, Int32 comp_id)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"insert into employee_request (card_id,REGISTER_TYPE,type,created_by,created_date,DELIVER_CARD_DATE
                                 ,DELIVER_CARD_FLAG,terminate_date,approve_flag,comp_id)
                        values(:crd, 'P', '3', :usr , SYSDATE,:delvdat , :flg, :tmdat, 'N',:comp_id) ", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":usr", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":delvdat", OracleType.DateTime).Value = delvdat;
                cmd.Parameters.Add(":flg", OracleType.VarChar).Value = flg;
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":tmdat", OracleType.DateTime).Value = tmdat;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //jobs
        public void edetdeleteEmployee(string crd, DateTime delvdat, string flg, DateTime tmdat, Int32 comp_id, string code)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"UPDATE  employee_request set card_id=:crd,DELIVER_CARD_DATE=:delvdat,DELIVER_CARD_FLAG=:flg,terminate_date=:tmdat,comp_id=:comp_id where REQUEST_CODE='" + code + "' ", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                cmd.Parameters.Add(":delvdat", OracleType.DateTime).Value = delvdat;
                cmd.Parameters.Add(":flg", OracleType.VarChar).Value = flg;
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":tmdat", OracleType.DateTime).Value = tmdat;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //torb10-7
        //torb18-9 Done
        public DataTable medical_select_search(string search, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), COMP_EMPLOYEESS.EMP_ENAME_ST || ' ' || COMP_EMPLOYEESS.EMP_ENAME_SC || ' ' || COMP_EMPLOYEESS.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE, COMP_EMPLOYEESS   WHERE EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID AND (EMPLOYEE_REQUEST.REQUEST_CODE like '%' ||:search || '%' or  EMPLOYEE_REQUEST.CARD_ID like'%' ||:search || '%') AND   EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'  and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID  and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'   and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto)  ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":search", OracleType.VarChar).Value = search;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb19-9 Done
        public DataTable search_filter_complains_contract(string nom, string status, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select distinct COMPLAINT_ID,PROVIDER_TYPE, PROVIDER_NAME,BRANCH_NAME,PROPLEM,SUBJECT_CODE ,CREATED_BY,COM_DATE,TIME,COM_REPLAY,MEMBER_NAME from IMS_COMPLAINTS,IMS_ESCLATION_MEMBER where SOLVED_BY = MEMBER_ID(+) and  COM_CHECKED = 'N' and SOLVE_FLAG =:status  and (to_date(COM_DATE) between: dateFrom  and: dateto)  and COMPLAINT_ID like'%' ||:nom || '%'", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":status", OracleType.VarChar).Value = status;
                cmd.Parameters.Add(":nom", OracleType.VarChar).Value = nom;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb19-9 Done
        public DataTable search_complains_contract(string nom, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select distinct COMPLAINT_ID,PROVIDER_TYPE, PROVIDER_NAME,BRANCH_NAME,PROPLEM,SUBJECT_CODE ,CREATED_BY,COM_DATE,TIME,COM_REPLAY,MEMBER_NAME from IMS_COMPLAINTS,IMS_ESCLATION_MEMBER where SOLVED_BY = MEMBER_ID(+) and COM_CHECKED = 'N' and(SOLVE_FLAG = 'Y' or SOLVE_FLAG = 'N') and(to_date(COM_DATE) between: dateFrom  and: dateto)  and COMPLAINT_ID like'%' ||:nom || '%'", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;

                cmd.Parameters.Add(":nom", OracleType.VarChar).Value = nom;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable medical_select_Print333(Int32 type, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID ,EMPLOYEE_REQUEST.EMP_CLASS ,EMPLOYEE_REQUEST.EMP_CLASS_REASON ,EMPLOYEE_REQUEST.CREATED_BY,EMPLOYEE_REQUEST.CREATED_DATE ,decode(EMPLOYEE_REQUEST.REGISTER_TYPE,'P','Desktop', 'p','Desktop','Mobile') ,COMP_EMPLOYEES.EMP_ENAME_ST||' '||COMP_EMPLOYEES.EMP_ENAME_SC||' '||COMP_EMPLOYEES.EMP_ENAME_TH,EMPLOYEE_REQUEST_TYPE.TYPE_NAME  ,decode(EMPLOYEE_REQUEST.APPROVE_FLAG ,'y','Accepted','Y','Accepted','n','Pending','N','Pending','w','Under Processing','W','Under Processing','f','Rejected','F','Rejected'),PRINT_REASON,decode( PRINT_CARD_CONFIRM.DONE_CHECK ,'N','طباعة','Y','Operation','D','Done','Under Processing') Place FROM EMPLOYEE_REQUEST left join PRINT_CARD_CONFIRM on EMPLOYEE_REQUEST.card_id=PRINT_CARD_CONFIRM.CARD_ID,EMPLOYEE_REQUEST_TYPE,COMP_EMPLOYEES   WHERE EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID AND  EMPLOYEE_REQUEST.CREATED_BY='" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:type and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON,decode( PRINT_CARD_CONFIRM.DONE_CHECK ,'N','طباعة','Y','Operation','D','Done','Under Processing') Place FROM EMPLOYEE_REQUEST left join PRINT_CARD_CONFIRM on EMPLOYEE_REQUEST.card_id=PRINT_CARD_CONFIRM.CARD_ID, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.TYPE =:type and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'   and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":type", OracleType.Number).Value = type;


                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb25-9 Done
        public DataTable medical_select_Print444(string search, Int32 typeee, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID ,EMPLOYEE_REQUEST.EMP_CLASS ,EMPLOYEE_REQUEST.EMP_CLASS_REASON ,EMPLOYEE_REQUEST.CREATED_BY,EMPLOYEE_REQUEST.CREATED_DATE ,decode(EMPLOYEE_REQUEST.REGISTER_TYPE,'P','Desktop', 'p','Desktop','Mobile') ,COMP_EMPLOYEES.EMP_ENAME_ST||' '||COMP_EMPLOYEES.EMP_ENAME_SC||' '||COMP_EMPLOYEES.EMP_ENAME_TH,EMPLOYEE_REQUEST_TYPE.TYPE_NAME  ,decode(EMPLOYEE_REQUEST.APPROVE_FLAG ,'y','Accepted','Y','Accepted','n','Pending','N','Pending','w','Under Processing','W','Under Processing','f','Rejected','F','Rejected'),PRINT_REASON ,decode( PRINT_CARD_CONFIRM.DONE_CHECK ,'N','طباعة','Y','Operation','D','Done','Under Processing') Place FROM EMPLOYEE_REQUEST left join PRINT_CARD_CONFIRM on EMPLOYEE_REQUEST.card_id=PRINT_CARD_CONFIRM.CARD_ID,EMPLOYEE_REQUEST_TYPE,COMP_EMPLOYEES   WHERE EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID AND  EMPLOYEE_REQUEST.CREATED_BY='" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:typeee and (EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:search || '%' or EMPLOYEE_REQUEST.card_id like'%' ||:search || '%') and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON,decode( PRINT_CARD_CONFIRM.DONE_CHECK ,'N','طباعة','Y','Operation','D','Done','Under Processing') Place FROM EMPLOYEE_REQUEST left join PRINT_CARD_CONFIRM on EMPLOYEE_REQUEST.card_id=PRINT_CARD_CONFIRM.CARD_ID, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "' and  (EMPLOYEE_REQUEST.REQUEST_CODE like'%' ||:search || '%' or EMPLOYEE_REQUEST.card_id like'%' ||:search || '%')   and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":search", OracleType.VarChar).Value = search;
                cmd.Parameters.Add(":typeee", OracleType.Number).Value = typeee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        //torb25-9 Done
        public DataTable medical_select_Print111(Int32 search, Int32 comp_id, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@" select distinct EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID ,EMPLOYEE_REQUEST.EMP_CLASS ,EMPLOYEE_REQUEST.EMP_CLASS_REASON ,EMPLOYEE_REQUEST.CREATED_BY,EMPLOYEE_REQUEST.CREATED_DATE ,decode(EMPLOYEE_REQUEST.REGISTER_TYPE,'P','Desktop', 'p','Desktop','Mobile') ,COMP_EMPLOYEES.EMP_ENAME_ST||' '||COMP_EMPLOYEES.EMP_ENAME_SC||' '||COMP_EMPLOYEES.EMP_ENAME_TH,EMPLOYEE_REQUEST_TYPE.TYPE_NAME  ,decode(EMPLOYEE_REQUEST.APPROVE_FLAG ,'y','Accepted','Y','Accepted','n','Pending','N','Pending','w','Under Processing','W','Under Processing','f','Rejected','F','Rejected'),PRINT_REASON,decode( PRINT_CARD_CONFIRM.DONE_CHECK ,'N','طباعة','Y','Operation','D','Done','Under Processing') Place FROM EMPLOYEE_REQUEST left join PRINT_CARD_CONFIRM on EMPLOYEE_REQUEST.card_id=PRINT_CARD_CONFIRM.CARD_ID ,EMPLOYEE_REQUEST_TYPE,COMP_EMPLOYEES   WHERE EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID AND  EMPLOYEE_REQUEST.CREATED_BY='" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:search and EMPLOYEE_REQUEST.COMP_ID=:comp_id and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id)  union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected') ,PRINT_REASON,decode( PRINT_CARD_CONFIRM.DONE_CHECK ,'N','طباعة','Y','Operation','D','Done','Under Processing') Place FROM EMPLOYEE_REQUEST left join PRINT_CARD_CONFIRM on EMPLOYEE_REQUEST.card_id=PRINT_CARD_CONFIRM.CARD_ID , EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'  and EMPLOYEE_REQUEST.TYPE =:search and  EMPLOYEE_REQUEST.COMP_ID =:comp_id and(to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":search", OracleType.Number).Value = search;
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        //torb25-9 Done
        public DataTable medical_select_Printz222(Int32 search, Int32 comp_id, string card, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                //  AND(EMPLOYEE_REQUEST.REQUEST_CODE like '%' ||:card || '%' or  EMPLOYEE_REQUEST.CARD_ID like'%' ||:card || '%')
                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), COMP_EMPLOYEESS.EMP_ENAME_ST || ' ' || COMP_EMPLOYEESS.EMP_ENAME_SC || ' ' || COMP_EMPLOYEESS.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON,decode( PRINT_CARD_CONFIRM.DONE_CHECK ,'N','طباعة','Y','Operation','D','Done','Under Processing') Place FROM EMPLOYEE_REQUEST left join PRINT_CARD_CONFIRM on EMPLOYEE_REQUEST.card_id=PRINT_CARD_CONFIRM.CARD_ID, EMPLOYEE_REQUEST_TYPE, COMP_EMPLOYEESS   WHERE EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID AND (EMPLOYEE_REQUEST.REQUEST_CODE like '%' ||:card || '%' or  EMPLOYEE_REQUEST.CARD_ID like'%' ||:card || '%') AND   EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:search and EMPLOYEE_REQUEST.COMP_ID =:comp_id and(to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":search", OracleType.Number).Value = search;
                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                // cmd.Parameters.Add(":code", OracleType.Number).Value = code;
                cmd.Parameters.Add(":card", OracleType.VarChar).Value = card;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }



        //torb24-4 Done
        public DataTable medical_select_search555(Int32 search, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select EMPLOYEE_REQUEST.REQUEST_CODE , EMPLOYEE_REQUEST.CARD_ID,EMPLOYEE_REQUEST_TYPE.TYPE_NAME ,to_char(EMPLOYEE_REQUEST.CREATED_DATE,'DD-MM-YYYY'),EMPLOYEE_REQUEST.APPROVE_FLAG FROM EMPLOYEE_REQUEST,EMPLOYEE_REQUEST_TYPE   WHERE EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID AND  CREATED_BY='" + User.Name + "' and TYPE =:search and (CREATED_DATE between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":search", OracleType.Number).Value = search;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //torb10-7 Done
        //torb18-9 Done
        public DataTable medical_select_search999(string search, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), COMP_EMPLOYEESS.EMP_ENAME_ST || ' ' || COMP_EMPLOYEESS.EMP_ENAME_SC || ' ' || COMP_EMPLOYEESS.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE, COMP_EMPLOYEESS   WHERE EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID AND (EMPLOYEE_REQUEST.CARD_ID like'%' ||:search || '%') AND   EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'  and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "' and EMPLOYEE_REQUEST.card_id like'%' ||:search || '%'   and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":search", OracleType.VarChar).Value = search;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb24-4 Done

        //torb10-7 Done
        //torb18-9 Done
        public DataTable medical_select_search104(Int32 type, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select distinct EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID ,EMPLOYEE_REQUEST.EMP_CLASS ,EMPLOYEE_REQUEST.EMP_CLASS_REASON ,EMPLOYEE_REQUEST.CREATED_BY,EMPLOYEE_REQUEST.CREATED_DATE ,decode(EMPLOYEE_REQUEST.REGISTER_TYPE,'P','Desktop', 'p','Desktop','Mobile') ,COMP_EMPLOYEES.EMP_ENAME_ST||' '||COMP_EMPLOYEES.EMP_ENAME_SC||' '||COMP_EMPLOYEES.EMP_ENAME_TH,EMPLOYEE_REQUEST_TYPE.TYPE_NAME  ,decode(EMPLOYEE_REQUEST.APPROVE_FLAG ,'y','Accepted','Y','Accepted','n','Pending','N','Pending','w','Under Processing','W','Under Processing','f','Rejected','F','Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST,EMPLOYEE_REQUEST_TYPE,COMP_EMPLOYEES   WHERE EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID AND  EMPLOYEE_REQUEST.CREATED_BY='" + User.Name + "' and EMPLOYEE_REQUEST.TYPE =:type and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between :dateFrom  and :dateto) and EMPLOYEE_REQUEST.CARD_ID = COMP_EMPLOYEESS.CARD_ID and COMP_EMPLOYEESS.contract_no = (select max(contract_no) from COMP_EMPLOYEESS where COMP_EMPLOYEESS.card_id = EMPLOYEE_REQUEST.card_id) union select distinct EMPLOYEE_REQUEST.REQUEST_CODE, EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.EMP_CLASS, EMPLOYEE_REQUEST.EMP_CLASS_REASON, EMPLOYEE_REQUEST.CREATED_BY, EMPLOYEE_REQUEST.CREATED_DATE, decode(EMPLOYEE_REQUEST.REGISTER_TYPE, 'P', 'Desktop', 'p', 'Desktop', 'Mobile'), EMPLOYEE_REQUEST.EMP_ENAME_ST || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_SC || ' ' || EMPLOYEE_REQUEST.EMP_ENAME_TH, EMPLOYEE_REQUEST_TYPE.TYPE_NAME, decode(EMPLOYEE_REQUEST.APPROVE_FLAG, 'y', 'Accepted', 'Y', 'Accepted', 'n', 'Pending', 'N', 'Pending', 'w', 'Under Processing', 'W', 'Under Processing', 'f', 'Rejected', 'F', 'Rejected'),PRINT_REASON FROM EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE WHERE EMPLOYEE_REQUEST.card_id='0' and EMPLOYEE_REQUEST.TYPE = EMPLOYEE_REQUEST_TYPE.TYPE_ID and EMPLOYEE_REQUEST.TYPE =:type and EMPLOYEE_REQUEST.CREATED_BY = '" + User.Name + "'   and (to_date(EMPLOYEE_REQUEST.CREATED_DATE) between: dateFrom  and: dateto) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":type", OracleType.Number).Value = type;


                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //joba
        public DataTable sershhrrqustmeangmedecal(string codetext, string x, DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"SELECT ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP FROM ENUM_REQUESTS WHERE (REQ_TYPE='M' or REQ_TYPE='m') AND (CARD_ID like'%" + codetext + "' OR  ID like'%" + codetext + "') and REQUEST_TYP like '%" + x + "' and REQ_DATE BETWEEN  :fromdate  and  :todate ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //joba
        public DataTable sershtele(string codetext, DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (User.Manegar == "Y" || User.Manegar == "y")
                {
                    cmd = new OracleCommand(@"SELECT  TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,NEXT_RECALL_DATE,NEXT_RECALL_TIME,MEETING_DATE,MEETING_TIME,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE , DATE_END_CONTRACT,COM_CONTRACT_NAME FROM TELE_SALES 
           WHERE (TELE_TRANS_NO like '" + codetext + "%' or AGENT_NAME  like '" + codetext + "%'or AGENT_ENAME like '" + codetext + "%') and DATE_OF_CALL BETWEEN :fromdate and :todate  and FINISH_TELE ='N' and ACTIVE='Y'", con);
                }
                else
                {
                    cmd = new OracleCommand(@"SELECT  TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,NEXT_RECALL_DATE,NEXT_RECALL_TIME,MEETING_DATE,MEETING_TIME,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE , DATE_END_CONTRACT,COM_CONTRACT_NAME FROM TELE_SALES 
           WHERE (TELE_TRANS_NO like '" + codetext + "%' or AGENT_NAME  like '" + codetext + "%'or AGENT_ENAME like '" + codetext + "%') and DATE_OF_CALL BETWEEN :fromdate and :todate  and FINISH_TELE ='N' and ACTIVE='Y' and CREATED_BY='" + User.Name + "'", con);

                }
                cmd.Parameters.Clear();





                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //joba 
        public DataTable finddate(string patchnom, string providernom, string cheackstat, DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                if (cheackstat == string.Empty)
                    cmd = new OracleCommand(@"select * from A_BATCH_S where BATCH_NO =  '" + patchnom + "' and PRV_NO = '" + providernom + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                else cmd = new OracleCommand(@"select * from A_BATCH_S where BATCH_NO =  '" + patchnom + "' and PRV_NO = '" + providernom + "'  and CHECK_STATUS = '" + cheackstat + "'  and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //  cmd.Parameters.Add(":patchnom", OracleType.Number).Value = Convert.ToInt32(patchnom);


                // cmd.Parameters.Add(":providernom", OracleType.Number).Value = Convert.ToInt32(providernom);

                //  cmd.Parameters.Add(":cheackstat", OracleType.VarChar).Value = cheackstat;
                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //public DataTable finddate(string providernom, string cheackstat, DateTime fromdate, DateTime todate)
        public DataTable finddate(Int32 bat1, Int32 bat2, Int32 prv1, Int32 prv2, Int32 larg, Int32 smal, string chk1, string chk2, DateTime dat1, DateTime dat2, DateTime dat3, DateTime dat4, string stat)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try


            {
                if (stat == string.Empty)
                    cmd = new OracleCommand(@"SELECT    BATSH_NO, PROV_ID, PROV_NAME, TO_CHAR(BATCH_REC_DATE,'DD-MM-YYYY'), TO_CHAR(CHECK_DATE,'DD-MM-YYYY'), CHECK_NO, BATCH_REC_AMT, AFTER_REVIEW_AMT, CHECK_AMT, 
                                                        TAX_AMT, ADMIN_FEES , DAM5T_BARID , SUB_AMT, ADD_AMT, ID_GROUP, NOTES, SERV_DATE_STETMENT, STATUS, TO_CHAR(FINANCE_REC_DATE,'DD-MM-YYYY') FINANCE_REC_DATE
                                                FROM    APP.A_REP_CHECK
                                                WHERE   BATSH_NO BETWEEN :bat1 AND :bat2
                                                    AND PROV_ID BETWEEN :prv1 AND :prv2
                                                    AND NVL(CHECK_AMT,0) BETWEEN :smal AND :larg
                                                    AND NVL(CHECK_NO,'0') BETWEEN :chk1 AND :chk2
                                                    AND BATCH_REC_DATE BETWEEN :dat1 AND :dat2
                                                    AND NVL(FINANCE_REC_DATE, TO_DATE('02-01-2016', 'dd-MM-yyyy')) BETWEEN :dat3 AND :dat4", con);
                else
                    cmd = new OracleCommand(@"SELECT    BATSH_NO, PROV_ID, PROV_NAME, TO_CHAR(BATCH_REC_DATE,'DD-MM-YYYY'), TO_CHAR(CHECK_DATE,'DD-MM-YYYY'), CHECK_NO, BATCH_REC_AMT, AFTER_REVIEW_AMT, CHECK_AMT, 
                                                        TAX_AMT, ADMIN_FEES , DAM5T_BARID , SUB_AMT, ADD_AMT, ID_GROUP, NOTES, SERV_DATE_STETMENT, STATUS, TO_CHAR(FINANCE_REC_DATE,'DD-MM-YYYY') FINANCE_REC_DATE
                                                FROM    APP.A_REP_CHECK
                                                WHERE   BATSH_NO BETWEEN :bat1 AND :bat2
                                                    AND PROV_ID BETWEEN :prv1 AND :prv2
                                                    AND NVL(CHECK_AMT,0) BETWEEN :smal AND :larg
                                                    AND NVL(CHECK_NO,'0') BETWEEN :chk1 AND :chk2
                                                    AND BATCH_REC_DATE BETWEEN :dat1 AND :dat2
                                                    AND NVL(FINANCE_REC_DATE, TO_DATE('02-01-2016', 'dd-MM-yyyy')) BETWEEN :dat3 AND :dat4
                                                    AND STAT = :stat", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":bat1", OracleType.Number).Value = bat1;
                cmd.Parameters.Add(":bat2", OracleType.Number).Value = bat2;
                cmd.Parameters.Add(":prv1", OracleType.Number).Value = prv1;
                cmd.Parameters.Add(":prv2", OracleType.Number).Value = prv2;
                cmd.Parameters.Add(":larg", OracleType.Number).Value = larg;
                cmd.Parameters.Add(":smal", OracleType.Number).Value = smal;
                cmd.Parameters.Add(":chk1", OracleType.VarChar).Value = chk1;
                cmd.Parameters.Add(":chk2", OracleType.VarChar).Value = chk2;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dat3", OracleType.DateTime).Value = dat3;
                cmd.Parameters.Add(":dat4", OracleType.DateTime).Value = dat4;

                if (stat != string.Empty)
                    cmd.Parameters.Add(":stat", OracleType.VarChar).Value = stat;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        public DataTable finddate(Int32 patchnom, string cheackstat, DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                if (cheackstat == string.Empty)
                    cmd = new OracleCommand(@"select * from A_BATCH_S where PRV_NO = '" + patchnom + "'      and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                else cmd = new OracleCommand(@"select * from A_BATCH_S where PRV_NO = '" + patchnom + "'    and CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();

                //  cmd.Parameters.Add(":patchnom", OracleType.Number).Value = patchnom;
                //  cmd.Parameters.Add(":cheackstat", OracleType.VarChar).Value = cheackstat;
                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        public DataTable finddate(string cheackstat, DateTime fromdate, DateTime todate)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                if (cheackstat == string.Empty)
                    cmd = new OracleCommand(@"select * from A_BATCH_S where    DATE_STETMENT between :fromdate and :todate  order by BATCH_NO", con);
                else cmd = new OracleCommand(@"select * from A_BATCH_S where CHECK_STATUS = '" + cheackstat + "'   and(DATE_STETMENT between :fromdate and :todate)  order by BATCH_NO", con);
                cmd.Parameters.Clear();



                // cmd.Parameters.Add(":cheackstat", OracleType.VarChar).Value = cheackstat;
                cmd.Parameters.Add(":fromdate", OracleType.DateTime).Value = fromdate;
                cmd.Parameters.Add(":todate", OracleType.DateTime).Value = todate;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //end update

        public DataTable getlData(Int32 CHECK_NO, DateTime CHECK_DATE, string CLIENT_NAME, Int32 COMP_ID, DateTime DEL_COMP_DATE, DateTime DEL_REV_DATE, Int32 STATUS_ID, DateTime testtime, DateTime DEL_COMP_DATEto)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                if (CLIENT_NAME != "")
                {

                    cmd = new OracleCommand(@" select IND_CHECKS.CHECK_NO,IND_CHECKS.CHECK_DATE,IND_CHECKS.BANK_ID,V_BANK_ACT.BANK_NAME,IND_CHECKS.CLIENT_NAME,IND_CHECKS.COMP_ID,IND_CHECKS.DEL_COMP_DATE,IND_CHECKS.DEL_REV_DATE,IND_CHECKS_ST.STATUS_ANAME from IND_CHECKS ,IND_CHECKS_ST,V_BANK_ACT
where IND_CHECKS.STATUS_ID=IND_CHECKS_ST.STATUS_ID
and IND_CHECKS.BANK_ID=V_BANK_ACT.BANK_ID
and IND_CHECKS.CHECK_NO BETWEEN :CHECK_NO1 AND :CHECK_NO2
 and IND_CHECKS.CHECK_DATE BETWEEN :CHECK_DATE1 AND :CHECK_DATE2
 and IND_CHECKS.CLIENT_NAME = :CLIENT_NAME
and IND_CHECKS.COMP_ID BETWEEN :COMP_ID1 AND :COMP_ID2
and IND_CHECKS.DEL_COMP_DATE BETWEEN :DEL_COMP_DATE1  AND :DEL_COMP_DATE2
and IND_CHECKS.DEL_REV_DATE BETWEEN :DEL_REV_DATE1 AND :DEL_REV_DATE2
and IND_CHECKS.STATUS_ID BETWEEN :STATUS_ID1 AND :STATUS_ID2 ", con);//AND IRS_CLAIM_REC_H.COMP_ID BETWEEN ( NVL( :P_F_COMP,0)) AND  (NVL(:P_T_COMP,999999999999999999999999999999))


                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(":CLIENT_NAME", OracleType.VarChar).Value = CLIENT_NAME;
                }
                else
                {
                    cmd = new OracleCommand(@"select IND_CHECKS.CHECK_NO,IND_CHECKS.CHECK_DATE,IND_CHECKS.BANK_ID,V_BANK_ACT.BANK_NAME,IND_CHECKS.CLIENT_NAME,IND_CHECKS.COMP_ID,IND_CHECKS.DEL_COMP_DATE,IND_CHECKS.DEL_REV_DATE,IND_CHECKS_ST.STATUS_ANAME from IND_CHECKS ,IND_CHECKS_ST,V_BANK_ACT
where IND_CHECKS.STATUS_ID=IND_CHECKS_ST.STATUS_ID
and IND_CHECKS.BANK_ID=V_BANK_ACT.BANK_ID
and IND_CHECKS.CHECK_NO BETWEEN :CHECK_NO1 AND :CHECK_NO2
 and IND_CHECKS.CHECK_DATE BETWEEN :CHECK_DATE1 AND :CHECK_DATE2
and IND_CHECKS.COMP_ID BETWEEN :COMP_ID1 AND :COMP_ID2
and IND_CHECKS.DEL_COMP_DATE BETWEEN :DEL_COMP_DATE1  AND :DEL_COMP_DATE2
and IND_CHECKS.DEL_REV_DATE BETWEEN :DEL_REV_DATE1 AND :DEL_REV_DATE2
and IND_CHECKS.STATUS_ID BETWEEN :STATUS_ID1 AND :STATUS_ID2 ", con);//AND IRS_CLAIM_REC_H.COMP_ID BETWEEN ( NVL( :P_F_COMP,0)) AND  (NVL(:P_T_COMP,999999999999999999999999999999))


                    cmd.Parameters.Clear();
                }
                if (CHECK_NO == 0)
                {
                    cmd.Parameters.Add(":CHECK_NO1", OracleType.Int32).Value = 0;
                    cmd.Parameters.Add(":CHECK_NO2", OracleType.Int32).Value = 999999999;
                    //   MessageBox.Show("CHECK_NO == 0");
                }
                else
                {
                    cmd.Parameters.Add(":CHECK_NO1", OracleType.Int32).Value = CHECK_NO;
                    cmd.Parameters.Add(":CHECK_NO2", OracleType.Int32).Value = CHECK_NO;
                    //  MessageBox.Show(CHECK_NO.ToString());
                }

                if (CHECK_DATE == testtime)
                {
                    cmd.Parameters.Add(":CHECK_DATE1", OracleType.DateTime).Value = "01-Jan-1990";
                    cmd.Parameters.Add(":CHECK_DATE2", OracleType.DateTime).Value = "01-Jan-2020";
                    // MessageBox.Show("CHECK_DATE.ToString()== 1 / 1 / 0001 12:00:00 AM");
                }
                else
                {
                    cmd.Parameters.Add(":CHECK_DATE1", OracleType.DateTime).Value = CHECK_DATE;
                    cmd.Parameters.Add(":CHECK_DATE2", OracleType.DateTime).Value = CHECK_DATE;
                    // MessageBox.Show(CHECK_DATE.ToString());

                }
                if (COMP_ID == 0)
                {
                    cmd.Parameters.Add(":COMP_ID1", OracleType.Int32).Value = 0;
                    cmd.Parameters.Add(":COMP_ID2", OracleType.Int32).Value = 999999999;
                    //  MessageBox.Show("COMP_ID == 0");
                }
                else
                {
                    cmd.Parameters.Add(":COMP_ID1", OracleType.Int32).Value = COMP_ID;
                    cmd.Parameters.Add(":COMP_ID2", OracleType.Int32).Value = COMP_ID;
                    // MessageBox.Show(COMP_ID.ToString());
                }
                if (DEL_COMP_DATE == testtime)
                    cmd.Parameters.Add(":DEL_COMP_DATE1", OracleType.DateTime).Value = "01-Jan-1990";
                else
                    cmd.Parameters.Add(":DEL_COMP_DATE1", OracleType.DateTime).Value = DEL_COMP_DATE;

                if (DEL_COMP_DATEto == testtime)
                    cmd.Parameters.Add(":DEL_COMP_DATE2", OracleType.DateTime).Value = "01-Jan-2020";
                else
                    cmd.Parameters.Add(":DEL_COMP_DATE2", OracleType.DateTime).Value = DEL_COMP_DATEto;

                if (DEL_REV_DATE == testtime)
                {
                    cmd.Parameters.Add(":DEL_REV_DATE1", OracleType.DateTime).Value = "01-Jan-1990";
                    cmd.Parameters.Add(":DEL_REV_DATE2", OracleType.DateTime).Value = "01-Jan-2020";
                    // MessageBox.Show("DEL_REV_DATE.ToString() == 1 / 1 / 0001 12:00:00 AM");
                }
                else
                {
                    cmd.Parameters.Add(":DEL_REV_DATE1", OracleType.DateTime).Value = DEL_REV_DATE;
                    cmd.Parameters.Add(":DEL_REV_DATE2", OracleType.DateTime).Value = DEL_REV_DATE;
                    //   MessageBox.Show(DEL_REV_DATE.ToString());

                }
                if (STATUS_ID == 0)
                {
                    cmd.Parameters.Add(":STATUS_ID1", OracleType.Int32).Value = 0;
                    cmd.Parameters.Add(":STATUS_ID2", OracleType.Int32).Value = 999999999;
                    //  MessageBox.Show("STATUS_ID == 0");
                }
                else
                {
                    cmd.Parameters.Add(":STATUS_ID1", OracleType.Int32).Value = STATUS_ID;
                    cmd.Parameters.Add(":STATUS_ID2", OracleType.Int32).Value = STATUS_ID;
                    //      MessageBox.Show(STATUS_ID.ToString());
                }


                // MessageBox.Show("CLIENT_NAME =" + CLIENT_NAME);


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }
        public DataTable change_f_search_operation2(string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,EMP_CLASS,EMP_CLASS_REASON,CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY'),REGISTER_TYPE,to_char(DATE_CHANGE_TYP,'DD-MM-YYYY') from EMPLOYEE_REQUEST where TYPE=2 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //jobaupdat24-4
        //torb22-4 lap convert to paremeters format date and replay Move
        public DataTable medical_approval_select2(string type, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,TYP_ANAME,PR_ENAME FROM ENUM_REQUESTS   WHERE (REQ_TYPE='M' or REQ_TYPE='m') AND  REQUEST_TYP like '%'|| :type ||'%' and (REQ_DATE between :dateFrom  and :dateto) and STATE='2' ", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":type", OracleType.VarChar).Value = type;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb26-6
        //torb15-7 Done
        //joba21-7
        public DataTable medical_approval_selecthr(string type, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                if (User.Type == "hr")
                    cmd = new OracleCommand(@"select ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,decode( STATE,'2','','1','',REPLAY ) REPLAY, decode(STATE,'2','pending','1','accept','0','refuse','') STATE,TYP_ANAME,PR_ENAME FROM ENUM_REQUESTS   WHERE (REQ_TYPE='M' or REQ_TYPE='m') AND  CARD_ID like '%'|| '" + User.CompanyID + "' ||'%' and REQUEST_TYP like '%'|| :type ||'%' and (to_date(REQ_DATE) between :dateFrom  and :dateto)  ", con);
                else
                    cmd = new OracleCommand(@"select ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,decode( STATE,'2','','1','',REPLAY ) REPLAY, decode(STATE,'2','pending','1','accept','0','refuse','') STATE,TYP_ANAME,PR_ENAME FROM ENUM_REQUESTS   WHERE (REQ_TYPE='M' or REQ_TYPE='m') AND  REQUEST_TYP like '%'|| :type ||'%' and (to_date(REQ_DATE) between :dateFrom  and :dateto)  ", con);
                cmd.Parameters.Clear();


                cmd.Parameters.Add(":type", OracleType.VarChar).Value = type;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable medical_approval_select12(string card_id, string type, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                if (User.Type == "hr")
                    cmd = new OracleCommand(@"select ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,TYP_ANAME,PR_ENAME FROM ENUM_REQUESTS   WHERE (REQ_TYPE='M' or REQ_TYPE='m') AND CARD_ID like '%'|| '" + User.CompanyID + "' ||'%'  and  (to_char( CARD_ID)  = to_char('" + card_id + "') or to_char( ID = '" + card_id + "')) AND REQUEST_TYP like '%'|| :type ||'%' and (to_date(REQ_DATE) between to_date(:dateFrom)  and to_date(:dateto)) ", con);
                else
                    cmd = new OracleCommand(@"select ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,TYP_ANAME,PR_ENAME FROM ENUM_REQUESTS   WHERE (REQ_TYPE='M' or REQ_TYPE='m') AND (to_char( CARD_ID)  =to_char( '" + card_id + "') or to_char( ID )=to_char( '" + card_id + "')) AND REQUEST_TYP like '%'|| :type ||'%' and (to_date(REQ_DATE) between to_date(:dateFrom)  and to_date(:dateto)) ", con);
                cmd.Parameters.Clear();

                // cmd.Parameters.Add(":card_id", OracleType.VarChar).Value = card_id;
                //  cmd.Parameters.Add(":id", OracleType.Number).Value = id;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = type;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb22-4 lap convert to paremeters format date and column replay Move
        //joba
        //torb15-7 Done
        //joba21-7

        public DataTable medical_approval_select(string card_id, string type, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                if (User.Type == "hr")
                    cmd = new OracleCommand(@"select ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,decode( STATE,'2','','1','',REPLAY ) REPLAY, decode(STATE,'2','pending','1','accept','0','refuse','') STATE,TYP_ANAME,PR_ENAME FROM ENUM_REQUESTS   WHERE (REQ_TYPE='M' or REQ_TYPE='m') AND CARD_ID like '%'|| '" + User.CompanyID + "' ||'%'  and  (to_char( CARD_ID)  = to_char('" + card_id + "') or ID = to_char(  '" + card_id + "')) AND REQUEST_TYP like '%'|| :type ||'%' and (to_date(REQ_DATE) between to_date(:dateFrom)  and to_date(:dateto)) ", con);
                else
                    cmd = new OracleCommand(@"select ID , TYPE,CARD_ID,REQ_DATE,EMP_ENAME,NOTES,REQUEST_TYP,decode( STATE,'2','','1','',REPLAY ) REPLAY, decode(STATE,'2','pending','1','accept','0','refuse','') STATE,TYP_ANAME,PR_ENAME FROM ENUM_REQUESTS   WHERE (REQ_TYPE='M' or REQ_TYPE='m') AND (to_char( CARD_ID)  =to_char( '" + card_id + "') or to_char( ID )=to_char( '" + card_id + "')) AND REQUEST_TYP like '%'|| :type ||'%' and (to_date(REQ_DATE) between to_date(:dateFrom)  and to_date(:dateto)) ", con);
                cmd.Parameters.Clear();

                // cmd.Parameters.Add(":card_id", OracleType.VarChar).Value = card_id;
                //  cmd.Parameters.Add(":id", OracleType.Number).Value = id;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = type;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable change_num_search_operation2(string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {//select REQUEST_CODE  , CARD_ID  , to_char(REOPEN_DATE,'DD-MM-YYYY') , CREATED_BY ,to_char( CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE,UPDATED_BY

                cmd = new OracleCommand(@"select REQUEST_CODE, CARD_ID, NEW_CARD_ID, CREATED_BY, to_char(CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE from EMPLOYEE_REQUEST where TYPE=6 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable refresh_e_search_operation2(string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select REQUEST_CODE  , CARD_ID  ,to_char( REOPEN_DATE,'DD-MM-YYYY') , CREATED_BY , to_char(CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE from EMPLOYEE_REQUEST where TYPE=5 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable get_search_operation2(string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,EMP_ANAME,EMP_ENAME,CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY'),REGISTER_TYPE,CHANG_EMP_NAME from EMPLOYEE_REQUEST where TYPE=7 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable delete_emp_search_operation2(string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,DELIVER_CARD_FLAG,decode(DELIVER_CARD_FLAG,1, to_char(DELIVER_CARD_DATE,'DD-MM-YYYY'),''),to_char(terminate_date,'DD-MM-YYYY'),CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY'),REGISTER_TYPE from EMPLOYEE_REQUEST where TYPE=3 AND approve_flag=:aproveflag  AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;
                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable delete_emp_search_operation(string crd2, string crd3, string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                int crd;
                if (int.TryParse(crd2, out crd))
                    cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,DELIVER_CARD_FLAG,decode(DELIVER_CARD_FLAG,1, to_char(DELIVER_CARD_DATE,'DD-MM-YYYY'),''),to_char(terminate_date,'DD-MM-YYYY'),CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY'),REGISTER_TYPE from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%'|| :hjvjhf ||'%' OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=3 AND approve_flag=:aproveflag  AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);
                else
                    cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,DELIVER_CARD_FLAG,decode(DELIVER_CARD_FLAG,1, to_char(DELIVER_CARD_DATE,'DD-MM-YYYY'),''),to_char(terminate_date,'DD-MM-YYYY'),CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY'),REGISTER_TYPE from EMPLOYEE_REQUEST where ( REQUEST_CODE =:hjvjhf  OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=3 AND approve_flag=:aproveflag  AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();

                if (int.TryParse(crd2, out crd))
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = crd;
                else
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = DBNull.Value;

                cmd.Parameters.Add(":hjv", OracleType.VarChar).Value = crd2;
                cmd.Parameters.Add(":seeer", OracleType.VarChar).Value = crd3;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable complain_search2(DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select * from IMS_VISITS where  (VISIT_DATE between :dateFrom  and :dateto) ", con);


                cmd.Parameters.Clear();


                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat2;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable add_emp_search_operation2(string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"select REQUEST_CODE,EMP_ANAME,EMP_ENAME,NATIONAL_ID,to_char( START_DATE,'DD-MM-YYYY'),to_char(BIRTHDATE,'DD-MM-YYYY'),GENDER,EMP_RELATION,CARD_ID,MOBILE,GLASSES,DISEASE,REGISTER_TYPE,CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY') from EMPLOYEE_REQUEST where TYPE=1 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public DataTable reprint_emp_search_operation2(string typee, DateTime dat1, DateTime dat2, string approveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,PRINT_REASON ,CREATED_BY ,to_char(CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE,REPRINT_EMP_CARD from EMPLOYEE_REQUEST where TYPE=4 AND (approve_flag=:approveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":approveflag", OracleType.VarChar).Value = approveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }


        public DataTable reprint_emp_search_operation(string crd2, string crd3, string typee, DateTime dat1, DateTime dat2, string approveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                int crd;
                if (int.TryParse(crd2, out crd))
                    cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,PRINT_REASON ,CREATED_BY ,to_char(CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE,REPRINT_EMP_CARD from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%'|| :hjvjhf ||'%' OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=4 AND (approve_flag=:approveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);
                else
                    cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,PRINT_REASON ,CREATED_BY ,to_char(CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE,REPRINT_EMP_CARD from EMPLOYEE_REQUEST where ( REQUEST_CODE = :hjvjhf  OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=4 AND (approve_flag=:approveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();
                if (int.TryParse(crd2, out crd))
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = crd;
                else
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = DBNull.Value;
                cmd.Parameters.Add(":hjv", OracleType.VarChar).Value = crd2;
                cmd.Parameters.Add(":seeer", OracleType.VarChar).Value = crd3;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":approveflag", OracleType.VarChar).Value = approveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }


        public DataTable schedule_search(string visit_idz, string company_idz, DateTime dat1, DateTime dat2, string nname)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (User.Manegar == "Y" || User.Manegar == "y")

                    cmd = new OracleCommand(@"select SCHUDLE.VISIT_ID, SCHUDLE.COMP_NAME,V_COMPANIES.C_ANAME, SCHUDLE.BRANCH_NAME, SCHUDLE.EMP_NAME, SCHUDLE.CLIENT_NAME, SCHUDLE.VISIT_REASON, to_char(SCHUDLE.VISIT_DATE,'dd/mm/yyyy') VISIT_DATE, SCHUDLE.TIME,SCHUDLE.CREATED_BY,SCHUDLE.REASON_EDIT,SCHUDLE.FEEDBACK from SCHUDLE,V_COMPANIES where SCHUDLE.COMP_NAME =to_char( C_COMP_ID (+)) and SCHUDLE.DELETE_FLAG = 'Y' 
and ( SCHUDLE.VISIT_ID like '%" + visit_idz + "%'  OR SCHUDLE.COMP_NAME like '%" + company_idz + "%' or UPPER(SCHUDLE.EMP_NAME) like '%" + nname.ToUpper() + "%'  )   and   (SCHUDLE.VISIT_DATE between :dat1  and :dat2) ", con);
                else
                    cmd = new OracleCommand(@"select SCHUDLE.VISIT_ID, SCHUDLE.COMP_NAME,V_COMPANIES.C_ANAME, SCHUDLE.BRANCH_NAME, SCHUDLE.EMP_NAME, SCHUDLE.CLIENT_NAME, SCHUDLE.VISIT_REASON, to_char(SCHUDLE.VISIT_DATE,'dd/mm/yyyy') VISIT_DATE, SCHUDLE.TIME,SCHUDLE.CREATED_BY,SCHUDLE.REASON_EDIT,SCHUDLE.FEEDBACK from SCHUDLE,V_COMPANIES where SCHUDLE.COMP_NAME =to_char( C_COMP_ID (+)) 
and SCHUDLE.DELETE_FLAG = 'Y' and ( SCHUDLE.VISIT_ID like  '%" + visit_idz + "%' OR SCHUDLE.COMP_NAME like '%" + company_idz + "%'   ) and SCHUDLE.EMP_NAME ='" + User.Name + "'  and   (SCHUDLE.VISIT_DATE between :dat1  and :dat2) ", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;



                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public DataTable complain_search(string crd, Int64 crd2, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                cmd = new OracleCommand(@"select * from IMS_VISITS where ( VISIT_ID= :crd OR PR_CODE =:crd2 ) and  (VISIT_DATE between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":crd2", OracleType.Number).Value = crd2;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable add_emp_search_operation(string crd2, string crd3, string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {
                int crd;
                if (int.TryParse(crd2, out crd))
                    cmd = new OracleCommand(@"select REQUEST_CODE,EMP_ANAME,EMP_ENAME,NATIONAL_ID,to_char(START_DATE,'DD-MM-YYYY'),to_char(BIRTHDATE,'DD-MM-YYYY'),GENDER,EMP_RELATION,CARD_ID,MOBILE,GLASSES,DISEASE,REGISTER_TYPE,CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY') from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%'|| :hjvjhf ||'%' OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=1 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);
                else cmd = new OracleCommand(@"select REQUEST_CODE,EMP_ANAME,EMP_ENAME,NATIONAL_ID,to_char(START_DATE,'DD-MM-YYYY'),to_char(BIRTHDATE,'DD-MM-YYYY'),GENDER,EMP_RELATION,CARD_ID,MOBILE,GLASSES,DISEASE,REGISTER_TYPE,CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY') from EMPLOYEE_REQUEST where ( REQUEST_CODE = :hjvjhf OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=1 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();
                if (int.TryParse(crd2, out crd))
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = crd;
                else
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = DBNull.Value;

                cmd.Parameters.Add(":hjv", OracleType.VarChar).Value = crd2;
                cmd.Parameters.Add(":seeer", OracleType.VarChar).Value = crd3;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable change_num_search_operation(string crd2, string crd3, string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                int crd;
                if (int.TryParse(crd2, out crd))
                    cmd = new OracleCommand(@"select REQUEST_CODE  , CARD_ID  ,NEW_CARD_ID , CREATED_BY , to_char(CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%'|| :hjvjhf ||'%' OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=6 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);
                else cmd = new OracleCommand(@"select REQUEST_CODE  , CARD_ID  ,NEW_CARD_ID , CREATED_BY , to_char(CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE from EMPLOYEE_REQUEST where ( REQUEST_CODE = :hjvjhf  OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=6 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);
                cmd.Parameters.Clear();

                if (int.TryParse(crd2, out crd))
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = crd;
                else cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = DBNull.Value;
                cmd.Parameters.Add(":hjv", OracleType.VarChar).Value = crd2;
                cmd.Parameters.Add(":seeer", OracleType.VarChar).Value = crd3;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable refresh_e_search_operation(string crd2, string crd3, string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                int crd;
                if (int.TryParse(crd2, out crd))
                    cmd = new OracleCommand(@"select REQUEST_CODE  , CARD_ID  ,to_char( REOPEN_DATE,'DD-MM-YYYY') , CREATED_BY ,to_char( CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%'|| :hjvjhf ||'%' OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=5 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);
                else cmd = new OracleCommand(@"select REQUEST_CODE  , CARD_ID  ,to_char( REOPEN_DATE,'DD-MM-YYYY') , CREATED_BY ,to_char( CREATED_DATE,'DD-MM-YYYY') ,REGISTER_TYPE from EMPLOYEE_REQUEST where ( REQUEST_CODE =:hjvjhf  OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=5 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);


                cmd.Parameters.Clear();

                if (int.TryParse(crd2, out crd))
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = crd;
                else cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = DBNull.Value;
                cmd.Parameters.Add(":hjv", OracleType.VarChar).Value = crd2;
                cmd.Parameters.Add(":seeer", OracleType.VarChar).Value = crd3;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable change_f_search_operation(string crd2, string crd3, string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                int crd;
                if (int.TryParse(crd2, out crd))
                    cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,EMP_CLASS,EMP_CLASS_REASON,CREATED_BY,to_char( CREATED_DATE,'DD-MM-YYYY'),REGISTER_TYPE,to_char(DATE_CHANGE_TYP,'DD-MM-YYYY') from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%'|| :hjvjhf ||'%' OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=2 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);
                else cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,EMP_CLASS,EMP_CLASS_REASON,CREATED_BY,to_char( CREATED_DATE,'DD-MM-YYYY'),REGISTER_TYPE,to_char(DATE_CHANGE_TYP,'DD-MM-YYYY') from EMPLOYEE_REQUEST where ( REQUEST_CODE =:hjvjhf  OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=2 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);


                cmd.Parameters.Clear();
                if (int.TryParse(crd2, out crd))
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = crd;
                else
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = DBNull.Value;
                cmd.Parameters.Add(":hjv", OracleType.VarChar).Value = crd2;
                cmd.Parameters.Add(":seeer", OracleType.VarChar).Value = crd3;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable get_search_operation(string crd2, string crd3, string typee, DateTime dat1, DateTime dat2, string aproveflag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                int crd;
                if (int.TryParse(crd2, out crd))
                    cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,EMP_ANAME,EMP_ENAME,CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY'),REGISTER_TYPE,CHANG_EMP_NAME from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%'|| :hjvjhf ||'%' OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=7 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);
                else cmd = new OracleCommand(@"select REQUEST_CODE,CARD_ID,EMP_ANAME,EMP_ENAME,CREATED_BY,to_char(CREATED_DATE,'DD-MM-YYYY'),REGISTER_TYPE,CHANG_EMP_NAME from EMPLOYEE_REQUEST where ( REQUEST_CODE  = :hjvjhf  OR CARD_ID like '%'|| :hjv ||'%' OR CREATED_BY like '%'|| :seeer ||'%' )   AND TYPE=7 AND (approve_flag=:aproveflag) AND REGISTER_TYPE like '%'|| :type ||'%' and (created_date between :dateFrom  and :dateto) ", con);

                cmd.Parameters.Clear();
                if (int.TryParse(crd2, out crd))
                    cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = crd;
                else cmd.Parameters.Add(":hjvjhf", OracleType.Number).Value = DBNull.Value;
                cmd.Parameters.Add(":hjv", OracleType.VarChar).Value = crd2;
                cmd.Parameters.Add(":seeer", OracleType.VarChar).Value = crd3;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = typee;
                cmd.Parameters.Add(":dateto", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dateFrom", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":aproveflag", OracleType.VarChar).Value = aproveflag;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable getdetails(string crd, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"SELECT M_INV_SAL.INV_ID , M_INV_SAL.INVT_NAM , M_INV_SAL.INV_DATE , M_INV_SAL.DOSAGE , M_INV_SAL.T_DURATION , M_INV_SAL.AMOUNT , M_INV_SAL.MED_GROUP , M_INV_SAL.LIC_TYPE
                  FROM M_TOT_MED , M_INV_SAL , DMS
                  WHERE M_TOT_MED.CARD_ID = DMS.CARD_ID 
                  AND DMS.D_ID = M_INV_SAL.INV_ID
                  AND M_INV_SAL.INV_DATE = M_INV_SAL.INV_DATE
                  AND DMS.D_DATE BETWEEN :dat1 and :dat2
                  AND M_TOT_MED.CARD_ID = :crd  ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //joba
        public bool inttlemaindata(string code, string typserv, string txt_namear, string txt_nameeg, string txt_fax, string txt_mail, string txt_tele, string txt_0phon, Int64 cbxarea_tele, string txt_name_contract, string txt_phone_contract, string txt_mail_contract, string date_coll_next, string timecollnext, string date_meting, string timemeting, string cbx_anser, string txt_descrabtion_anser, string cbx_resonnotanser, string txt_not_anther, Int64 nom_of_coll, string UserName, Int64 parant, string date_end_contract, string txt_name_com_contract)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();

                if (date_coll_next == string.Empty && date_meting == string.Empty && date_end_contract == string.Empty)
                { cmd = new OracleCommand(@"INSERT INTO TELE_SALES (TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE,COM_CONTRACT_NAME) 
                                   VALUES ( '" + code + "' , '" + typserv + "' , :txt_namear  , :txt_nameeg, '" + txt_fax + "', '" + txt_mail + "', '" + txt_tele + "', '" + txt_0phon + "', '" + cbxarea_tele + "', :txt_name_contract, '" + txt_phone_contract + "', '" + txt_mail_contract + "',sysdate,to_char(CURRENT_TIMESTAMP,'hh:mi:ss AM'), '" + cbx_anser + "', '" + txt_descrabtion_anser + "', '" + cbx_resonnotanser + "', '" + txt_not_anther + "','" + nom_of_coll + "', '" + parant + "' ,'P', '" + User.Name + "' ,sysdate,'" + txt_name_com_contract + "')", con); }

                else if (date_coll_next == string.Empty && date_meting == string.Empty && date_end_contract != string.Empty)
                { cmd = new OracleCommand(@"INSERT INTO TELE_SALES (TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE,DATE_END_CONTRACT,COM_CONTRACT_NAME) 
                                   VALUES ( '" + code + "' , '" + typserv + "' , :txt_namear , :txt_nameeg, '" + txt_fax + "', '" + txt_mail + "', '" + txt_tele + "', '" + txt_0phon + "', '" + cbxarea_tele + "', :txt_name_contract, '" + txt_phone_contract + "', '" + txt_mail_contract + "',sysdate,to_char(CURRENT_TIMESTAMP,'hh:mi:ss AM'), '" + cbx_anser + "', '" + txt_descrabtion_anser + "', '" + cbx_resonnotanser + "', '" + txt_not_anther + "','" + nom_of_coll + "', '" + parant + "' ,'P', '" + User.Name + "' ,sysdate,:date_end_contract,'" + txt_name_com_contract + "')", con); }

                else if (date_coll_next == string.Empty && date_meting != string.Empty && date_end_contract == string.Empty)
                { cmd = new OracleCommand(@"INSERT INTO TELE_SALES (TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,MEETING_DATE,MEETING_TIME,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE,COM_CONTRACT_NAME) 
                                   VALUES ( '" + code + "' , '" + typserv + "' , :txt_namear , :txt_nameeg, '" + txt_fax + "', '" + txt_mail + "', '" + txt_tele + "', '" + txt_0phon + "', '" + cbxarea_tele + "', :txt_name_contract, '" + txt_phone_contract + "', '" + txt_mail_contract + "',sysdate,to_char(CURRENT_TIMESTAMP,'hh:mi:ss AM') , to_date(:date_meting), '" + timemeting + "', '" + cbx_anser + "', '" + txt_descrabtion_anser + "', '" + cbx_resonnotanser + "', '" + txt_not_anther + "','" + nom_of_coll + "', '" + parant + "' ,'P', '" + User.Name + "' ,sysdate,'" + txt_name_com_contract + "')", con); }

                else if (date_coll_next == string.Empty && date_meting != string.Empty && date_end_contract != string.Empty)
                { cmd = new OracleCommand(@"INSERT INTO TELE_SALES (TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,MEETING_DATE,MEETING_TIME,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE,DATE_END_CONTRACT,COM_CONTRACT_NAME) 
                                   VALUES ( '" + code + "' , '" + typserv + "' , :txt_namear , :txt_nameeg, '" + txt_fax + "', '" + txt_mail + "', '" + txt_tele + "', '" + txt_0phon + "', '" + cbxarea_tele + "', :txt_name_contract, '" + txt_phone_contract + "', '" + txt_mail_contract + "',sysdate,to_char(CURRENT_TIMESTAMP,'hh:mi:ss AM'), to_date(:date_meting), '" + timemeting + "', '" + cbx_anser + "', '" + txt_descrabtion_anser + "', '" + cbx_resonnotanser + "', '" + txt_not_anther + "','" + nom_of_coll + "', '" + parant + "' ,'P', '" + User.Name + "' ,sysdate,:date_end_contract,'" + txt_name_com_contract + "')", con); }

                else if (date_coll_next != string.Empty && date_meting == string.Empty && date_end_contract == string.Empty)
                { cmd = new OracleCommand(@"INSERT INTO TELE_SALES (TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,NEXT_RECALL_DATE,NEXT_RECALL_TIME,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE,COM_CONTRACT_NAME) 
                                   VALUES ( '" + code + "' , '" + typserv + "' ,:txt_namear, :txt_nameeg, '" + txt_fax + "', '" + txt_mail + "', '" + txt_tele + "', '" + txt_0phon + "', '" + cbxarea_tele + "', :txt_name_contract, '" + txt_phone_contract + "', '" + txt_mail_contract + "',sysdate,to_char(CURRENT_TIMESTAMP,'hh:mi:ss AM'),to_date(:date_coll_next), '" + timecollnext + "' , '" + cbx_anser + "', '" + txt_descrabtion_anser + "', '" + cbx_resonnotanser + "', '" + txt_not_anther + "','" + nom_of_coll + "', '" + parant + "' ,'P', '" + User.Name + "' ,sysdate,'" + txt_name_com_contract + "')", con); }

                else if (date_coll_next != string.Empty && date_meting == string.Empty && date_end_contract != string.Empty)
                { cmd = new OracleCommand(@"INSERT INTO TELE_SALES (TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,NEXT_RECALL_DATE,NEXT_RECALL_TIME,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE,DATE_END_CONTRACT,COM_CONTRACT_NAME) 
                                    VALUES ( '" + code + "' , '" + typserv + "' ,:txt_namear, :txt_nameeg, '" + txt_fax + "', '" + txt_mail + "', '" + txt_tele + "', '" + txt_0phon + "', '" + cbxarea_tele + "', :txt_name_contract, '" + txt_phone_contract + "', '" + txt_mail_contract + "',sysdate,to_char(CURRENT_TIMESTAMP,'hh:mi:ss AM'),to_date(:date_coll_next), '" + timecollnext + "' , '" + cbx_anser + "', '" + txt_descrabtion_anser + "', '" + cbx_resonnotanser + "', '" + txt_not_anther + "','" + nom_of_coll + "', '" + parant + "' ,'P', '" + User.Name + "' ,sysdate,:date_end_contract,'" + txt_name_com_contract + "')", con); }

                else if (date_coll_next != string.Empty && date_meting != string.Empty && date_end_contract == string.Empty)
                { cmd = new OracleCommand(@"INSERT INTO TELE_SALES (TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,NEXT_RECALL_DATE,NEXT_RECALL_TIME,MEETING_DATE,MEETING_TIME,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE,COM_CONTRACT_NAME) 
                                   VALUES ( '" + code + "' , '" + typserv + "' ,:txt_namear, :txt_nameeg, '" + txt_fax + "', '" + txt_mail + "', '" + txt_tele + "', '" + txt_0phon + "', '" + cbxarea_tele + "', :txt_name_contract, '" + txt_phone_contract + "', '" + txt_mail_contract + "',sysdate,to_char(CURRENT_TIMESTAMP,'hh:mi:ss AM'),to_date(:date_coll_next), '" + timecollnext + "' , to_date(:date_meting), '" + timemeting + "', '" + cbx_anser + "', '" + txt_descrabtion_anser + "', '" + cbx_resonnotanser + "', '" + txt_not_anther + "','" + nom_of_coll + "', '" + parant + "' ,'P', '" + User.Name + "' ,sysdate,'" + txt_name_com_contract + "')", con); }

                else if (date_coll_next != string.Empty && date_meting != string.Empty && date_end_contract != string.Empty)
                { cmd = new OracleCommand(@"INSERT INTO TELE_SALES (TELE_TRANS_NO,  CUST_TYP,AGENT_NAME,AGENT_ENAME,FAX_NO, MAIL, TELE_NO, MOBILE,AREA_CODE,CONTACT_PERSON,CONTACT_TELE,CONTACT_MAIL,DATE_OF_CALL,TIME_OF_CALL,NEXT_RECALL_DATE,NEXT_RECALL_TIME,MEETING_DATE,MEETING_TIME,CUST_REPLY_TYP,CUST_REPLY_DESC,NOT_INTEREST_TYPE,NOTES,TOTAL_OF_CALL,PARENT,DEVICE,CREATED_BY,CREATED_DATE,DATE_END_CONTRACT,COM_CONTRACT_NAME) 
                                   VALUES ( '" + code + "' , '" + typserv + "' ,:txt_namear, :txt_nameeg, '" + txt_fax + "', '" + txt_mail + "', '" + txt_tele + "', '" + txt_0phon + "', '" + cbxarea_tele + "', :txt_name_contract, '" + txt_phone_contract + "', '" + txt_mail_contract + "',sysdate,to_char(CURRENT_TIMESTAMP,'hh:mi:ss AM'),to_date(:date_coll_next), '" + timecollnext + "' , to_date(:date_meting), '" + timemeting + "', '" + cbx_anser + "', '" + txt_descrabtion_anser + "', '" + cbx_resonnotanser + "', '" + txt_not_anther + "','" + nom_of_coll + "', '" + parant + "' ,'P', '" + User.Name + "' ,sysdate,:date_end_contract,'" + txt_name_com_contract + "')", con); }
                //     string , string , string , string , string )


                cmd.Parameters.Clear();
                //cmd.Parameters.Add(":code", OracleType.VarChar).Value = code;
                //cmd.Parameters.Add(":typserv", OracleType.VarChar).Value = typserv;
                cmd.Parameters.Add(":txt_namear", OracleType.VarChar).Value = txt_namear;
                cmd.Parameters.Add(":txt_nameeg", OracleType.VarChar).Value = txt_nameeg;
                //cmd.Parameters.Add(":txt_fax", OracleType.VarChar).Value = txt_fax;
                //cmd.Parameters.Add(":txt_mail", OracleType.VarChar).Value = txt_mail;
                //cmd.Parameters.Add(":txt_tele", OracleType.VarChar).Value = txt_tele;
                //cmd.Parameters.Add(":txt_0phon", OracleType.VarChar).Value = txt_0phon;
                //cmd.Parameters.Add(":cbxarea_tele", OracleType.Number).Value = cbxarea_tele;
                cmd.Parameters.Add(":txt_name_contract", OracleType.VarChar).Value = txt_name_contract;
                //cmd.Parameters.Add(":txt_phone_contract", OracleType.VarChar).Value = txt_phone_contract;
                //cmd.Parameters.Add(":txt_mail_contract", OracleType.VarChar).Value = txt_mail_contract;
                if (date_coll_next != string.Empty)
                    cmd.Parameters.Add(":date_coll_next", OracleType.DateTime).Value = Convert.ToDateTime(date_coll_next);


                //cmd.Parameters.Add(":timecollnext", OracleType.VarChar).Value = timecollnext;
                if (date_meting != string.Empty)
                    cmd.Parameters.Add(":date_meting", OracleType.DateTime).Value = Convert.ToDateTime(date_meting);

                //cmd.Parameters.Add(":timemeting", OracleType.VarChar).Value = timemeting;
                //cmd.Parameters.Add(":cbx_anser", OracleType.VarChar).Value = cbx_anser;

                //cmd.Parameters.Add(":txt_descrabtion_anser", OracleType.VarChar).Value = txt_descrabtion_anser;
                //cmd.Parameters.Add(":cbx_resonnotanser", OracleType.VarChar).Value = cbx_resonnotanser;
                //cmd.Parameters.Add(":txt_not_anther", OracleType.VarChar).Value = txt_not_anther;
                //cmd.Parameters.Add(":UserName", OracleType.VarChar).Value = User.Name;
                //cmd.Parameters.Add(":nom_of_coll", OracleType.Number).Value = nom_of_coll;
                //cmd.Parameters.Add(":parant", OracleType.Number).Value = parant;
                if (date_end_contract != string.Empty)
                    cmd.Parameters.Add(":date_end_contract", OracleType.DateTime).Value = Convert.ToDateTime(date_end_contract);

                //cmd.Parameters.Add(":txt_name_com_contract", OracleType.VarChar).Value = txt_name_com_contract;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //joba
        public void insindmty(string IND_TYP, string CHECK_NO_OLD, Int64 COMPNY_ID, string COMPANY_NAME, string BATCH_NO, DateTime ARCHIVE_RECEIPT_DATE, DateTime REVIEW_RECEIPT_DATE, string RECIPIENT_NAME_FROM_ARCH, String RECIPIENT_NAME_FROM_REVIEW, Int64 FROM_CLAIM, Int64 TO_CLAIM, double CHECK_VALUE, double CLAIM_VALUE, double DISCOUNT_VALUE, double ADDED_VALUE, string CHK_TYP, string CHECK_NO, string TRANS_CODE, string CASH_CODE, string CASH_RESON, string TITLE, string BENEFICIARY_NAME, string NOTS, string CONSULTATIVE_NAME, DateTime CONSUL_DATE, DateTime ACCOUNTS_RECEIPT_DATE, string CREATED_BY)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                //if( BATCH_NO.ToString()==string.Empty)
                cmd = new OracleCommand(@"insert into IND_DATA (IND_TYP,CHECK_NO_OLD ,COMPNY_ID ,COMPANY_NAME ,BATCH_NO,ARCHIVE_RECEIPT_DATE ,REVIEW_RECEIPT_DATE ,RECIPIENT_NAME_FROM_ARCH ,RECIPIENT_NAME_FROM_REVIEW ,FROM_CLAIM ,TO_CLAIM ,CHECK_VALUE,CLAIM_VALUE ,DISCOUNT_VALUE ,ADDED_VALUE ,CHK_TYP,CHECK_NO ,TRANS_CODE,CASH_CODE,CASH_RESON,TITLE,BENEFICIARY_NAME,NOTS ,CONSULTATIVE_NAME ,CONSUL_DATE ,ACCOUNTS_RECEIPT_DATE ,CREATED_BY ,CREATED_DATE) values
                                  (:IND_TYP,:CHECK_NO_OLD,:COMPNY_ID,:COMPANY_NAME,'" + BATCH_NO + "',:ARCHIVE_RECEIPT_DATE,:REVIEW_RECEIPT_DATE,:RECIPIENT_NAME_FROM_ARCH,:RECIPIENT_NAME_FROM_REVIEW,:FROM_CLAIM ,:TO_CLAIM ,:CHECK_VALUE,:CLAIM_VALUE ,:DISCOUNT_VALUE ,:ADDED_VALUE ,:CHK_TYP,:CHECK_NO ,:TRANS_CODE,:CASH_CODE,:CASH_RESON,:TITLE,:BENEFICIARY_NAME,:NOTS ,:CONSULTATIVE_NAME ,:CONSUL_DATE ,:ACCOUNTS_RECEIPT_DATE ,:CREATED_BY ,sysdate)", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":IND_TYP", OracleType.VarChar).Value = IND_TYP;
                cmd.Parameters.Add(":CHECK_NO_OLD", OracleType.VarChar).Value = CHECK_NO_OLD;
                cmd.Parameters.Add(":COMPNY_ID", OracleType.Number).Value = COMPNY_ID;
                cmd.Parameters.Add(":COMPANY_NAME", OracleType.VarChar).Value = COMPANY_NAME;
                //  cmd.Parameters.Add(":BATCH_NO", OracleType.Number).Value = BATCH_NO;
                cmd.Parameters.Add(":ARCHIVE_RECEIPT_DATE", OracleType.DateTime).Value = ARCHIVE_RECEIPT_DATE;
                cmd.Parameters.Add(":REVIEW_RECEIPT_DATE", OracleType.DateTime).Value = REVIEW_RECEIPT_DATE;
                cmd.Parameters.Add(":RECIPIENT_NAME_FROM_ARCH", OracleType.VarChar).Value = RECIPIENT_NAME_FROM_ARCH;
                cmd.Parameters.Add(":RECIPIENT_NAME_FROM_ARCH", OracleType.VarChar).Value = RECIPIENT_NAME_FROM_ARCH;
                cmd.Parameters.Add(":RECIPIENT_NAME_FROM_REVIEW", OracleType.VarChar).Value = RECIPIENT_NAME_FROM_REVIEW;
                cmd.Parameters.Add(":FROM_CLAIM", OracleType.Number).Value = FROM_CLAIM;
                cmd.Parameters.Add(":TO_CLAIM", OracleType.Number).Value = TO_CLAIM;
                cmd.Parameters.Add(":CHECK_VALUE", OracleType.Float).Value = CHECK_VALUE;
                cmd.Parameters.Add(":CLAIM_VALUE", OracleType.Float).Value = CLAIM_VALUE;
                cmd.Parameters.Add(":DISCOUNT_VALUE", OracleType.Float).Value = DISCOUNT_VALUE;
                cmd.Parameters.Add(":ADDED_VALUE", OracleType.Float).Value = ADDED_VALUE;
                cmd.Parameters.Add(":CHK_TYP", OracleType.VarChar).Value = CHK_TYP;

                cmd.Parameters.Add(":CHECK_NO", OracleType.VarChar).Value = CHECK_NO;

                cmd.Parameters.Add(":TRANS_CODE", OracleType.VarChar).Value = TRANS_CODE;
                cmd.Parameters.Add(":CASH_CODE", OracleType.VarChar).Value = CASH_CODE;
                cmd.Parameters.Add(":CASH_RESON", OracleType.VarChar).Value = CASH_RESON;
                cmd.Parameters.Add(":TITLE", OracleType.VarChar).Value = TITLE;

                cmd.Parameters.Add(":BENEFICIARY_NAME", OracleType.VarChar).Value = BENEFICIARY_NAME;
                cmd.Parameters.Add(":NOTS", OracleType.VarChar).Value = NOTS;
                cmd.Parameters.Add(":CONSULTATIVE_NAME", OracleType.VarChar).Value = CONSULTATIVE_NAME;
                cmd.Parameters.Add(":CONSUL_DATE", OracleType.DateTime).Value = CONSUL_DATE;

                cmd.Parameters.Add(":ACCOUNTS_RECEIPT_DATE", OracleType.DateTime).Value = ACCOUNTS_RECEIPT_DATE;
                cmd.Parameters.Add(":CREATED_BY", OracleType.VarChar).Value = CREATED_BY;





                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb22-4 lap paremeters Move
        public void update_add_emp(string enamesttxt, string ndaraaaaaa, string emppppp, string maradaa, string enamesctxt, string enamethtxt, string enamefrtxt, string enametxt, string anamesttxt, string anamesctxt, string anamethtxt, string anamefrtxt, string anametxt, string nationalidtxt, DateTime birthdatetxt, string mobnumtxt, string emailtxt, DateTime startdatetxt, string addrtxt, string elno3, Int32 searchtxt)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();
                // @"UPDATE EMPLOYEE_REQUEST SET  EMP_ENAME_ST =:enamesttxt,GLASSES=:ndaraaaaaa ,TYPE='1',EMP_RELATION= :emppppp ,DISEASE=:maradaa , EMP_ENAME_SC = :enamesctxt, EMP_ENAME_TH =:enamethtxt, EMP_ENAME_FR =:enamefrtxt, EMP_ENAME =:enametxt, EMP_ANAME_ST = :anamesttxt, EMP_ANAME_SC =:anamesctxt, EMP_ANAME_TH =  :anamethtxt, EMP_ANAME_FR =  :anamefrtxt, EMP_ANAME = :anametxt, NATIONAL_ID = :nationalidtxt, BIRTHDATE = :birthdatetxt, MOBILE = :mobnumtxt, EMAIL =:emailtxt, START_DATE = :startdatetxt, ADDRESS = :addrtxt,GENDER = :elno3  WHERE REQUEST_CODE =  :searchtxt ", "تم التعديل بنجاح");
                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"UPDATE EMPLOYEE_REQUEST SET  EMP_ENAME_ST =:enamesttxt,GLASSES=:ndaraaaaaa ,TYPE='1',EMP_RELATION= :emppppp ,DISEASE=:maradaa , EMP_ENAME_SC = :enamesctxt, EMP_ENAME_TH =:enamethtxt, EMP_ENAME_FR =:enamefrtxt, EMP_ENAME =:enametxt, EMP_ANAME_ST = :anamesttxt, EMP_ANAME_SC =:anamesctxt, EMP_ANAME_TH =  :anamethtxt, EMP_ANAME_FR =  :anamefrtxt, EMP_ANAME = :anametxt, NATIONAL_ID = :nationalidtxt, BIRTHDATE = :birthdatetxt, MOBILE = :mobnumtxt, EMAIL =:emailtxt, START_DATE = :startdatetxt, ADDRESS = :addrtxt,GENDER = :elno3  WHERE REQUEST_CODE =  :searchtxt ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":enamesttxt", OracleType.VarChar).Value = enamesttxt;
                cmd.Parameters.Add(":ndaraaaaaa", OracleType.VarChar).Value = ndaraaaaaa;
                cmd.Parameters.Add(":emppppp", OracleType.VarChar).Value = emppppp;
                cmd.Parameters.Add(":maradaa", OracleType.VarChar).Value = maradaa;
                cmd.Parameters.Add(":enamesctxt", OracleType.VarChar).Value = enamesctxt;
                cmd.Parameters.Add(":enamethtxt", OracleType.VarChar).Value = enamethtxt;
                cmd.Parameters.Add(":enamefrtxt", OracleType.VarChar).Value = enamefrtxt;
                cmd.Parameters.Add(":enametxt", OracleType.VarChar).Value = enametxt;
                cmd.Parameters.Add(":anamesttxt", OracleType.VarChar).Value = anamesttxt;
                cmd.Parameters.Add(":anamesctxt", OracleType.VarChar).Value = anamesctxt;
                cmd.Parameters.Add(":anamethtxt", OracleType.VarChar).Value = anamethtxt;
                cmd.Parameters.Add(":anamefrtxt", OracleType.VarChar).Value = anamefrtxt;
                cmd.Parameters.Add(":anamesttxt", OracleType.VarChar).Value = anamesttxt;
                cmd.Parameters.Add(":anametxt", OracleType.VarChar).Value = anametxt;
                cmd.Parameters.Add(":nationalidtxt", OracleType.VarChar).Value = nationalidtxt;
                cmd.Parameters.Add(":birthdatetxt", OracleType.DateTime).Value = birthdatetxt;
                cmd.Parameters.Add(":mobnumtxt", OracleType.VarChar).Value = mobnumtxt;
                cmd.Parameters.Add(":emailtxt", OracleType.VarChar).Value = emailtxt;
                cmd.Parameters.Add(":startdatetxt", OracleType.DateTime).Value = startdatetxt;
                cmd.Parameters.Add(":addrtxt", OracleType.VarChar).Value = addrtxt;
                cmd.Parameters.Add(":elno3", OracleType.VarChar).Value = elno3;
                cmd.Parameters.Add(":searchtxt", OracleType.Int32).Value = searchtxt;






                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //joba
        public void updatsindmty(string IND_TYP, string CHECK_NO_OLD, Int64 COMPNY_ID, string COMPANY_NAME, string BATCH_NO, DateTime ARCHIVE_RECEIPT_DATE, DateTime REVIEW_RECEIPT_DATE, string RECIPIENT_NAME_FROM_ARCH, String RECIPIENT_NAME_FROM_REVIEW, Int64 FROM_CLAIM, Int64 TO_CLAIM, double CHECK_VALUE, double CLAIM_VALUE, double DISCOUNT_VALUE, double ADDED_VALUE, string CHK_TYP, string CHECK_NO, string TRANS_CODE, string CASH_CODE, string CASH_RESON, string TITLE, string BENEFICIARY_NAME, string NOTS, string CONSULTATIVE_NAME, DateTime CONSUL_DATE, DateTime ACCOUNTS_RECEIPT_DATE, string UPDATED_BY, Int64 codei)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@" UPDATE IND_DATA SET IND_TYP ='" + IND_TYP + "',CHECK_NO_OLD ='" + CHECK_NO_OLD + "',COMPNY_ID='" + COMPNY_ID + "',COMPANY_NAME =:COMPANY_NAME ,BATCH_NO='" + BATCH_NO + "',ARCHIVE_RECEIPT_DATE =:ARCHIVE_RECEIPT_DATE,REVIEW_RECEIPT_DATE =:REVIEW_RECEIPT_DATE,RECIPIENT_NAME_FROM_ARCH ='" + RECIPIENT_NAME_FROM_ARCH + "',RECIPIENT_NAME_FROM_REVIEW ='" + RECIPIENT_NAME_FROM_REVIEW + "',FROM_CLAIM='" + FROM_CLAIM + "',TO_CLAIM ='" + TO_CLAIM + "',CHECK_VALUE='" + CHECK_VALUE + "',CLAIM_VALUE='" + CLAIM_VALUE + "', DISCOUNT_VALUE='" + DISCOUNT_VALUE + "',ADDED_VALUE ='" + ADDED_VALUE + "',CHK_TYP='" + CHK_TYP + "',CHECK_NO ='" + CHECK_NO + "',TRANS_CODE='" + TRANS_CODE + "',CASH_CODE='" + CASH_CODE + "',CASH_RESON='" + CASH_RESON + "',TITLE=:TITLE ,BENEFICIARY_NAME =:BENEFICIARY_NAME ,NOTS='" + NOTS + "',CONSULTATIVE_NAME ='" + CONSULTATIVE_NAME + "',CONSUL_DATE=:CONSUL_DATE,ACCOUNTS_RECEIPT_DATE=:ACCOUNTS_RECEIPT_DATE ,UPDATED_BY='" + User.Name + "',UPDATED_DATE=sysdate  WHERE CODE='" + codei + "'", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":ARCHIVE_RECEIPT_DATE", OracleType.DateTime).Value = ARCHIVE_RECEIPT_DATE;
                cmd.Parameters.Add(":REVIEW_RECEIPT_DATE", OracleType.DateTime).Value = REVIEW_RECEIPT_DATE;

                cmd.Parameters.Add(":CONSUL_DATE", OracleType.DateTime).Value = CONSUL_DATE;

                cmd.Parameters.Add(":ACCOUNTS_RECEIPT_DATE", OracleType.DateTime).Value = ACCOUNTS_RECEIPT_DATE;
                cmd.Parameters.Add(":COMPANY_NAME", OracleType.VarChar).Value = COMPANY_NAME;
                cmd.Parameters.Add(":BENEFICIARY_NAME", OracleType.VarChar).Value = BENEFICIARY_NAME;
                cmd.Parameters.Add(":TITLE", OracleType.VarChar).Value = TITLE;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //jb
        public void updatcheckcount(string v_dprecivedatcheck, string v_cbxbankcheck, string v_dprecivedatesun, string v_txtnotcheck, string v_dprecivedatersevcst, string v_txtnemersbcheck, string DELIVERY_METHOD, Int64 code)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            //  db.RunNonQuery("UPDATE IND_DATA SET CHECK_DATE='" + dprecivedatcheck.Text + "',CHECK_BANK='" + cbxbankcheck.Text + "',SIGNATURE_DATE='" + dprecivedatesun.Text + "',ACCOUNTING_NOTES='" + txtnotcheck.Text + "',CUSTOMER_RECEIPT_DATE='" + dprecivedatersevcst.Text + "' ,CHECK_FROM = '" + txtnemersbcheck.Text + "', READY='W' WHERE  CODE  ='" + cbxincode_check.Text + "'  
            try
            {


                //



                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@" UPDATE IND_DATA SET CHECK_DATE=:v_dprecivedatcheck,CHECK_BANK=:v_cbxbankcheck,SIGNATURE_DATE=:v_dprecivedatesun ,ACCOUNTING_NOTES=:v_txtnotcheck ,CUSTOMER_RECEIPT_DATE=:v_dprecivedatersevcst  ,CHECK_FROM = :v_txtnemersbcheck ,DELIVERY_METHOD=:DELIVERY_METHOD, READY='W' WHERE  CODE  =:code   ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":v_dprecivedatcheck", OracleType.DateTime).Value = Convert.ToDateTime(v_dprecivedatcheck);
                cmd.Parameters.Add(":v_cbxbankcheck", OracleType.VarChar).Value = v_cbxbankcheck;

                cmd.Parameters.Add(":v_dprecivedatesun", OracleType.DateTime).Value = Convert.ToDateTime(v_dprecivedatesun);

                cmd.Parameters.Add(":v_txtnotcheck", OracleType.VarChar).Value = v_txtnotcheck;
                cmd.Parameters.Add(":v_dprecivedatersevcst", OracleType.DateTime).Value = Convert.ToDateTime(v_dprecivedatersevcst);

                cmd.Parameters.Add(":v_txtnemersbcheck", OracleType.VarChar).Value = v_txtnemersbcheck;
                cmd.Parameters.Add(":DELIVERY_METHOD", OracleType.VarChar).Value = DELIVERY_METHOD;
                cmd.Parameters.Add(":code", OracleType.Number).Value = code;


                cmd.ExecuteNonQuery();

                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void change_name_emp(string card_id, string name_user, string fename, string sename, string thename, string frename, string fullename, string faname, string saname, string thaname, string franame, string fullaname, Int32 comp_id)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();


                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"INSERT INTO EMPLOYEE_REQUEST (CARD_ID, CREATED_BY, CREATED_DATE,REGISTER_TYPE, TYPE ,emp_ename_st,emp_ename_sc,emp_ename_th,emp_ename_fr,emp_ename,emp_aname_st,emp_aname_sc,emp_aname_th,emp_aname_fr,emp_aname,COMP_ID) VALUES 
                                                                  (:cardnum,:nameuser,sysdate,                 'P'  ,  '7',:fename,        :sename,    :thename,    :frename,   :fullename,:faname,     :saname,     :thaname,   :franame,   :fullaname ,:comp_id)", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":cardnum", OracleType.VarChar).Value = card_id;
                cmd.Parameters.Add(":nameuser", OracleType.VarChar).Value = name_user;
                // cmd.Parameters.Add(":datenw", OracleType.DateTime).Value = clmdat;
                cmd.Parameters.Add(":fename", OracleType.VarChar).Value = fename;
                cmd.Parameters.Add(":sename", OracleType.VarChar).Value = sename;
                cmd.Parameters.Add(":thename", OracleType.VarChar).Value = thename;
                cmd.Parameters.Add(":frename", OracleType.VarChar).Value = frename;
                cmd.Parameters.Add(":fullename", OracleType.VarChar).Value = fullename;
                cmd.Parameters.Add(":faname", OracleType.VarChar).Value = faname;
                cmd.Parameters.Add(":saname", OracleType.VarChar).Value = saname;
                cmd.Parameters.Add(":thaname", OracleType.VarChar).Value = thaname;
                cmd.Parameters.Add(":franame", OracleType.VarChar).Value = franame;
                cmd.Parameters.Add(":fullaname", OracleType.VarChar).Value = fullaname;
                cmd.Parameters.Add(":comp_id", OracleType.Int32).Value = comp_id;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void SAVE_RECOLLECT_PRIM(Int32 company, Int32 contract_no, string type, string value1, string user1, DateTime start1, DateTime end1)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                DB db = new DB();
                int ids = 1;
                System.Data.DataTable idtable = db.RunReader("select max(id) from RECOLLECTION_PREMIUM_DATA").Result;
                if (idtable.Rows[0][0].ToString() != "")
                    ids = Convert.ToInt32(idtable.Rows[0][0].ToString()) + 1;




                //



                con.Open();


                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);


                cmd = new OracleCommand(@"insert into RECOLLECTION_PREMIUM_DATA(id,COMP_ID, CONTRACT_CO, RECOLLECTION, PREMIUM, CREATED_BY
                            , CREATED_DATE, START_DATE, END_DATE)  values
                                                                  (" + ids + ",:company,:contract_no,:type,:value1,:user1,sysdate,:start1,:end1 )", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":company", OracleType.Number).Value = company;
                cmd.Parameters.Add(":contract_no", OracleType.Number).Value = contract_no;
                // cmd.Parameters.Add(":datenw", OracleType.DateTime).Value = clmdat;
                cmd.Parameters.Add(":type", OracleType.NVarChar).Value = type;
                cmd.Parameters.Add(":value1", OracleType.NVarChar).Value = value1;
                cmd.Parameters.Add(":user1", OracleType.NVarChar).Value = user1;
                cmd.Parameters.Add(":start1", OracleType.DateTime).Value = start1;
                cmd.Parameters.Add(":end1", OracleType.DateTime).Value = end1;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }
        //torb2-7
        public void save_companies_group(Int32 company, Int32 contract_no, Int32 area, string industryz, string user1, DateTime start1, DateTime end1)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();
                cmd = new OracleCommand(@"insert into COMPANIES_GROUP(COMP_ID, CONTRACT_NUM, AREA, INDUSTRY, CREATED_BY
                            , CREATED_DATE, START_DATE, END_DATE)  values
                                                                  (:company,:contract_no,:area,:industryz,:user1,sysdate,:start1,:end1 )", con);


                cmd.Parameters.Clear();
                cmd.Parameters.Add(":company", OracleType.Number).Value = company;
                cmd.Parameters.Add(":contract_no", OracleType.Number).Value = contract_no;
                cmd.Parameters.Add(":area", OracleType.Number).Value = area;
                cmd.Parameters.Add(":industryz", OracleType.NVarChar).Value = industryz;
                cmd.Parameters.Add(":user1", OracleType.NVarChar).Value = user1;
                cmd.Parameters.Add(":start1", OracleType.DateTime).Value = start1;
                cmd.Parameters.Add(":end1", OracleType.DateTime).Value = end1;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        //joba
        public void insclm(Int32 clm, string crd, DateTime clmdat, double clmamo, double clmnet, String empname, Int32 prvno, string prvname, Int32 diocode, string dianame, string dianotes, String rescod, string bankacount, string mobnom, string bankname, string branchbank, string cod, DateTime daynow, string usr)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {

                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_NET,EMP_ENAME,PRV_NO,PRV_ENAME,DIA_CODE,DIA_ENAME,DIANOTES,ACC_NUM,EMP_MOBILE,BANK_NAME,BANK_BRANCH,REASON_CODE,CODE,CREATED_DATE,CREATED_BY) VALUES (:clm,:crd,:clmdat,:clmamo,:clmnet,:empname,:prvno,:prvname,:diocode,:dianame,:dianotes,:bankacount,:mobnom,:bankname,:branchbank,:rescod,:cod,:sydate,:usr)", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":clm", OracleType.Number).Value = clm;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":clmdat", OracleType.DateTime).Value = clmdat;
                cmd.Parameters.Add(":clmamo", OracleType.Number).Value = clmamo;
                cmd.Parameters.Add(":clmnet", OracleType.Number).Value = clmnet;
                cmd.Parameters.Add(":empname", OracleType.VarChar).Value = empname;
                cmd.Parameters.Add(":prvno", OracleType.Number).Value = prvno;
                cmd.Parameters.Add(":prvname", OracleType.VarChar).Value = prvname;
                cmd.Parameters.Add(":diocode", OracleType.Number).Value = diocode;
                cmd.Parameters.Add(":dianame", OracleType.VarChar).Value = dianame;
                cmd.Parameters.Add(":dianotes", OracleType.VarChar).Value = dianotes;
                cmd.Parameters.Add(":rescod", OracleType.VarChar).Value = rescod;
                cmd.Parameters.Add(":cod", OracleType.VarChar).Value = cod;
                cmd.Parameters.Add(":bankacount", OracleType.VarChar).Value = bankacount;
                cmd.Parameters.Add(":mobnom", OracleType.VarChar).Value = mobnom;
                cmd.Parameters.Add(":bankname", OracleType.VarChar).Value = bankname;
                cmd.Parameters.Add(":branchbank", OracleType.VarChar).Value = branchbank;

                cmd.Parameters.Add(":sydate", OracleType.DateTime).Value = daynow;
                cmd.Parameters.Add(":usr", OracleType.VarChar).Value = usr;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void insernottely(string code, string datecollnext)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                // db.RunNonQuery(@"INSERT INTO CYCLE_NOTI (CODE_SRVE,TYP_EMP,EMP_MANGER,DPATRT_EMP,TYP_WINDOW,COMP_ID_FROM,COMP_ID_TO,END_DATE,DEVICE,DEVICE_SEEN,CREATED_BY,CREATED_DATE)
                //  VALUES('" + code + "',1,'N','tel Sales','TELESEALES','10000','10000','" + date_coll_next.Text + "','P','0','" + User.Name + "',SYSDATE)");


                con.Open();


                cmd = new OracleCommand(@"INSERT INTO CYCLE_NOTI (CODE_SRVE,TYP_EMP,EMP_MANGER,DPATRT_EMP,TYP_WINDOW,COMP_ID_FROM,COMP_ID_TO,END_DATE,DEVICE,DEVICE_SEEN,CREATED_BY,CREATED_DATE)
                                                   VALUES('" + code + "',1,'N','tel Sales','TELESEALES','10000','10000', :datecollnext ,'P','0','" + User.Name + "',SYSDATE)", con);



                cmd.Parameters.Add(":datecollnext", OracleType.DateTime).Value = Convert.ToDateTime(datecollnext);
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb29-4
        public DataTable selectinfoemployeeEdit(string empid, string username, DateTime datcard)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {

                dd = new DataTable();



                try
                {
                    con.Open();

                    // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                    // cmd = new OracleCommand(@"select REQUEST_CODE from EMPLOYEE_REQUEST where card_id = :empid  and terminate_date = :term_date and DELIVER_CARD_FLAG = :flag and approve_flag ='w' and REGISTER_TYPE ='P'and type=3", con);
                    cmd = new OracleCommand(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID =:empid and CREATED_BY =:username" +
                    " and to_date(CREATED_DATE) = to_date(sysdate) and REOPEN_DATE= :datcard and REGISTER_TYPE ='P' and  TYPE=5", con);
                    //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(":empid", OracleType.VarChar).Value = empid;
                    // cmd.Parameters.Add(":term_date", OracleType.DateTime).Value = term_date;
                    cmd.Parameters.Add(":username", OracleType.VarChar).Value = username;
                    cmd.Parameters.Add(":datcard", OracleType.DateTime).Value = datcard;



                    da = new OracleDataAdapter(cmd);


                    da.Fill(dd);
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();


                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                //finally { return dd; }
                return dd;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }
        public DataTable selectinfoemployeedelete(string empid, DateTime term_date, string flag)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"select REQUEST_CODE from EMPLOYEE_REQUEST where card_id = :empid  and terminate_date = :term_date and DELIVER_CARD_FLAG = :flag  and REGISTER_TYPE ='P'and type=3", con);

                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":empid", OracleType.VarChar).Value = empid;
                cmd.Parameters.Add(":term_date", OracleType.DateTime).Value = term_date;
                cmd.Parameters.Add(":flag", OracleType.VarChar).Value = flag;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd); con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;

               

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        //torb29-4 3adlt fe comp_id
        public void editinfoEmp(string crd, string username, DateTime delvdat, Int32 comp_id)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {


                //



                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"INSERT INTO EMPLOYEE_REQUEST (CARD_ID, CREATED_BY, CREATED_DATE, REOPEN_DATE, REGISTER_TYPE, TYPE ,APPROVE_FLAG,COMP_ID)
                        values(:crd, :username,sysdate,:delvdat,'P','5' , 'N',:comp_id) ", con);

                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":username", OracleType.VarChar).Value = username;
                cmd.Parameters.Add(":delvdat", OracleType.DateTime).Value = delvdat;
                //cmd.Parameters.Add(":datnow", OracleType.DateTime).Value = datnow;
                cmd.Parameters.Add(":comp_id", OracleType.Int32).Value = comp_id;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }
        public void deleteEmployee(string crd, DateTime delvdat, string flg, DateTime tmdat)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();

                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"insert into employee_request (card_id,REGISTER_TYPE,type,created_by,created_date,DELIVER_CARD_DATE
                                 ,DELIVER_CARD_FLAG,terminate_date,approve_flag)
                        values(:crd, 'P', '3', :usr , SYSDATE,:delvdat , :flg, :tmdat, 'N') ", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":usr", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":delvdat", OracleType.DateTime).Value = delvdat;
                cmd.Parameters.Add(":flg", OracleType.VarChar).Value = flg;
                cmd.Parameters.Add(":tmdat", OracleType.DateTime).Value = tmdat;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


        }
        public void insaddta8tya111(string employeeNameSt, string EMP_ANAME_SC, string EMP_ANAME_TH, string EMP_ANAME_FR, string EMP_ENAME_ST, string EMP_ENAME_SC, string EMP_ENAME_TH, string EMP_ENAME_FR, string EMP_ANAME, string EMP_ENAME, string NATIONAL_ID, string BRANCH, string ADDRESS, string MOBILEEMP_RELATION, string emppppp, string DISEASE, string EMAIL, DateTime BIRTHDATE, string GENDER, DateTime START_DATE, Int32 EMP_CODE, string ndaraaaaaa, string CREATED_BY, string EMP_CLASS, string CARD_ID, Int32 COMP_ID)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            con.Open();
            try
            {
                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_NET,EMP_ENAME,PRV_NO,PRV_ENAME,DIA_CODE,DIA_ENAME,DIANOTES,REASON_CODE,CODE,CREATED_DATE,CREATED_BY) VALUES (:clm,:crd,:clmdat,:clmamo,:clmnet,:empname,:prvno,:prvname,:diocode,:dianame,:dianotes,:rescod,:cod,:sydate,:usr)", con);
                cmd = new OracleCommand(@"insert into EMPLOYEE_REQUEST (EMP_ANAME_ST,EMP_ANAME_SC,EMP_ANAME_TH,EMP_ANAME_FR,EMP_ENAME_ST,EMP_ENAME_SC,EMP_ENAME_TH,EMP_ENAME_FR,EMP_ANAME,EMP_ENAME,NATIONAL_ID,BRANCH,ADDRESS,MOBILE,EMP_RELATION,DISEASE,EMAIL,BIRTHDATE,GENDER,START_DATE,EMP_CODE,GLASSES,REGISTER_TYPE,CREATED_BY,CREATED_DATE,APPROVE_FLAG,TYPE,EMP_CLASS,CARD_ID,COMP_ID) values
(:employeeNameSt,:EMP_ANAME_SC,:EMP_ANAME_TH,:EMP_ANAME_FR,:EMP_ENAME_ST,:EMP_ENAME_SC,:EMP_ENAME_TH,:EMP_ENAME_FR,:EMP_ANAME,
:EMP_ENAME,:NATIONAL_ID,:BRANCH,:ADDRESS,:MOBILEEMP_RELATION,:emppppp,:DISEASE,:EMAIL,:BIRTHDATE,:GENDER,:START_DATE,:EMP_CODE,
:GLASSES,:REGISTER_TYPE,:CREATED_BY,sysdate,:APPROVE_FLAG,:TYPE,:EMP_CLASS,:CARD_ID,:COMP_ID)", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":employeeNameSt", OracleType.VarChar).Value = employeeNameSt;
                cmd.Parameters.Add(":EMP_ANAME_SC", OracleType.VarChar).Value = EMP_ANAME_SC;
                cmd.Parameters.Add(":EMP_ANAME_TH", OracleType.VarChar).Value = EMP_ANAME_TH;
                cmd.Parameters.Add(":emppppp", OracleType.VarChar).Value = emppppp;
                cmd.Parameters.Add(":EMP_ANAME_FR", OracleType.VarChar).Value = EMP_ANAME_FR;
                cmd.Parameters.Add(":EMP_ENAME_ST", OracleType.VarChar).Value = EMP_ENAME_ST;
                cmd.Parameters.Add(":EMP_ENAME_SC", OracleType.VarChar).Value = EMP_ENAME_SC;
                cmd.Parameters.Add(":EMP_ENAME_TH", OracleType.VarChar).Value = EMP_ENAME_TH;
                cmd.Parameters.Add(":EMP_ENAME_FR", OracleType.VarChar).Value = EMP_ENAME_FR;
                cmd.Parameters.Add(":EMP_ANAME", OracleType.VarChar).Value = EMP_ANAME;
                cmd.Parameters.Add(":EMP_ENAME", OracleType.VarChar).Value = EMP_ENAME;
                cmd.Parameters.Add(":NATIONAL_ID", OracleType.VarChar).Value = NATIONAL_ID;
                cmd.Parameters.Add(":BRANCH", OracleType.VarChar).Value = BRANCH;
                cmd.Parameters.Add(":ADDRESS", OracleType.VarChar).Value = ADDRESS;
                cmd.Parameters.Add(":MOBILEEMP_RELATION", OracleType.VarChar).Value = MOBILEEMP_RELATION;
                cmd.Parameters.Add(":DISEASE", OracleType.VarChar).Value = DISEASE;
                cmd.Parameters.Add(":EMAIL", OracleType.VarChar).Value = EMAIL;
                cmd.Parameters.Add(":BIRTHDATE", OracleType.DateTime).Value = BIRTHDATE;
                cmd.Parameters.Add(":GENDER", OracleType.VarChar).Value = GENDER;
                cmd.Parameters.Add(":START_DATE", OracleType.DateTime).Value = START_DATE;
                cmd.Parameters.Add(":EMP_CODE", OracleType.Number).Value = EMP_CODE;
                cmd.Parameters.Add(":GLASSES", OracleType.VarChar).Value = ndaraaaaaa;
                cmd.Parameters.Add(":REGISTER_TYPE", OracleType.VarChar).Value = 'P';
                cmd.Parameters.Add(":CREATED_BY", OracleType.VarChar).Value = CREATED_BY;
                //   cmd.Parameters.Add(":CREATED_DATE", OracleType.DateTime).Value = "sysdate";
                cmd.Parameters.Add(":APPROVE_FLAG", OracleType.VarChar).Value = 'N';
                cmd.Parameters.Add(":TYPE", OracleType.VarChar).Value = '1';
                cmd.Parameters.Add(":EMP_CLASS", OracleType.VarChar).Value = EMP_CLASS;
                cmd.Parameters.Add(":CARD_ID", OracleType.VarChar).Value = CARD_ID;
                cmd.Parameters.Add(":COMP_ID", OracleType.Number).Value = COMP_ID;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


            // da = new OracleDataAdapter(cmd);
            //DataTable dd = new DataTable();

            //  da.Fill(dd);con.Close();
            //   return dd;


        }

        public void insaddta8tya(string employeeNameSt, string EMP_ANAME_SC, string EMP_ANAME_TH, string EMP_ANAME_FR, string EMP_ENAME_ST, string EMP_ENAME_SC, string EMP_ENAME_TH, string EMP_ENAME_FR, string EMP_ANAME, string EMP_ENAME, string NATIONAL_ID, string BRANCH, string ADDRESS, string MOBILEEMP_RELATION, string emppppp, string DISEASE, string EMAIL, DateTime BIRTHDATE, string GENDER, DateTime START_DATE, Int32 EMP_CODE, string ndaraaaaaa, string CREATED_BY, string EMP_CLASS, string CARD_ID, Int32 COMP_ID, string relation)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            con.Open();
            try
            {
                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_NET,EMP_ENAME,PRV_NO,PRV_ENAME,DIA_CODE,DIA_ENAME,DIANOTES,REASON_CODE,CODE,CREATED_DATE,CREATED_BY) VALUES (:clm,:crd,:clmdat,:clmamo,:clmnet,:empname,:prvno,:prvname,:diocode,:dianame,:dianotes,:rescod,:cod,:sydate,:usr)", con);
                cmd = new OracleCommand(@"insert into EMPLOYEE_REQUEST (EMP_ANAME_ST    ,EMP_ANAME_SC,EMP_ANAME_TH,EMP_ANAME_FR  ,EMP_ENAME_ST  ,EMP_ENAME_SC,EMP_ENAME_TH,EMP_ENAME_FR,EMP_ANAME,EMP_ENAME,NATIONAL_ID,BRANCH,ADDRESS,MOBILE,EMP_RELATION,DISEASE,EMAIL,BIRTHDATE,GENDER,START_DATE,EMP_CODE,GLASSES,REGISTER_TYPE,CREATED_BY,CREATED_DATE,APPROVE_FLAG,TYPE,EMP_CLASS,CARD_ID,COMP_ID,RELATION) values
                                                                       (:P_employeeNameSt,:P_EMP_ANAME_SC,:P_EMP_ANAME_TH,:P_EMP_ANAME_FR,:P_EMP_ENAME_ST,:P_EMP_ENAME_SC,
:P_EMP_ENAME_TH,:P_EMP_ENAME_FR,:P_EMP_ANAME,:P_EMP_ENAME,:P_NATIONAL_ID,:P_BRANCH,:P_ADDRESS,:P_MOBILEEMP_RELATION,:P_emppppp,:P_DISEASE,:P_EMAIL,:P_BIRTHDATE,:P_GENDER,
:P_START_DATE,:P_EMP_CODE,:P_GLASSES,:P_REGISTER_TYPE,:P_CREATED_BY,sysdate,:P_APPROVE_FLAG,:P_TYPE,:P_EMP_CLASS,:P_CARD_ID,:P_COMP_ID,'" + relation + "')", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":P_employeeNameSt", OracleType.VarChar).Value = employeeNameSt;
                cmd.Parameters.Add(":P_EMP_ANAME_SC", OracleType.VarChar).Value = EMP_ANAME_SC;
                cmd.Parameters.Add(":P_EMP_ANAME_TH", OracleType.VarChar).Value = EMP_ANAME_TH;
                cmd.Parameters.Add(":P_emppppp", OracleType.VarChar).Value = emppppp;
                cmd.Parameters.Add(":P_EMP_ANAME_FR", OracleType.VarChar).Value = EMP_ANAME_FR;
                cmd.Parameters.Add(":P_EMP_ENAME_ST", OracleType.VarChar).Value = EMP_ENAME_ST;
                cmd.Parameters.Add(":P_EMP_ENAME_SC", OracleType.VarChar).Value = EMP_ENAME_SC;
                cmd.Parameters.Add(":P_EMP_ENAME_TH", OracleType.VarChar).Value = EMP_ENAME_TH;
                cmd.Parameters.Add(":P_EMP_ENAME_FR", OracleType.VarChar).Value = EMP_ENAME_FR;
                cmd.Parameters.Add(":P_EMP_ANAME", OracleType.VarChar).Value = EMP_ANAME;
                cmd.Parameters.Add(":P_EMP_ENAME", OracleType.VarChar).Value = EMP_ENAME;
                cmd.Parameters.Add(":P_NATIONAL_ID", OracleType.VarChar).Value = NATIONAL_ID;
                cmd.Parameters.Add(":P_BRANCH", OracleType.VarChar).Value = BRANCH;
                cmd.Parameters.Add(":P_ADDRESS", OracleType.VarChar).Value = ADDRESS;
                cmd.Parameters.Add(":P_MOBILEEMP_RELATION", OracleType.VarChar).Value = MOBILEEMP_RELATION;
                cmd.Parameters.Add(":P_DISEASE", OracleType.VarChar).Value = DISEASE;
                cmd.Parameters.Add(":P_EMAIL", OracleType.VarChar).Value = EMAIL;
                cmd.Parameters.Add(":P_BIRTHDATE", OracleType.DateTime).Value = BIRTHDATE;
                cmd.Parameters.Add(":P_GENDER", OracleType.VarChar).Value = GENDER;
                cmd.Parameters.Add(":P_START_DATE", OracleType.DateTime).Value = START_DATE;
                cmd.Parameters.Add(":P_EMP_CODE", OracleType.Number).Value = EMP_CODE;
                cmd.Parameters.Add(":P_GLASSES", OracleType.VarChar).Value = ndaraaaaaa;
                cmd.Parameters.Add(":P_REGISTER_TYPE", OracleType.VarChar).Value = 'P';
                cmd.Parameters.Add(":P_CREATED_BY", OracleType.VarChar).Value = CREATED_BY;
                //   cmd.Parameters.Add(":CREATED_DATE", OracleType.DateTime).Value = "sysdate";
                cmd.Parameters.Add(":P_APPROVE_FLAG", OracleType.VarChar).Value = 'N';
                cmd.Parameters.Add(":P_TYPE", OracleType.VarChar).Value = '1';
                cmd.Parameters.Add(":P_EMP_CLASS", OracleType.VarChar).Value = EMP_CLASS;
                cmd.Parameters.Add(":P_CARD_ID", OracleType.VarChar).Value = CARD_ID;
                cmd.Parameters.Add(":P_COMP_ID", OracleType.Number).Value = COMP_ID;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }


            // da = new OracleDataAdapter(cmd);
            //DataTable dd = new DataTable();

            //  da.Fill(dd);con.Close();
            //   return dd;


        }
        public void insaddta8tya(string employeeNameSt, string EMP_ANAME_SC, string EMP_ANAME_TH, string EMP_ANAME_FR, string EMP_ENAME_ST, string EMP_ENAME_SC, string EMP_ENAME_TH, string EMP_ENAME_FR, string EMP_ANAME, string EMP_ENAME, string NATIONAL_ID, string BRANCH, string ADDRESS, string MOBILEEMP_RELATION, string emppppp, string DISEASE, string EMAIL, DateTime BIRTHDATE, string GENDER, DateTime START_DATE, Int32 EMP_CODE, string ndaraaaaaa, string CREATED_BY, string EMP_CLASS, string CARD_ID, Int32 COMP_ID)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            con.Open();
            try
            {
                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_NET,EMP_ENAME,PRV_NO,PRV_ENAME,DIA_CODE,DIA_ENAME,DIANOTES,REASON_CODE,CODE,CREATED_DATE,CREATED_BY) VALUES (:clm,:crd,:clmdat,:clmamo,:clmnet,:empname,:prvno,:prvname,:diocode,:dianame,:dianotes,:rescod,:cod,:sydate,:usr)", con);
                cmd = new OracleCommand(@"insert into EMPLOYEE_REQUEST (EMP_ANAME_ST,EMP_ANAME_SC,EMP_ANAME_TH,EMP_ANAME_FR,EMP_ENAME_ST,EMP_ENAME_SC,EMP_ENAME_TH,EMP_ENAME_FR,EMP_ANAME,EMP_ENAME,NATIONAL_ID,BRANCH,ADDRESS,MOBILE,EMP_RELATION,DISEASE,EMAIL,BIRTHDATE,GENDER,START_DATE,EMP_CODE,GLASSES,REGISTER_TYPE,CREATED_BY,CREATED_DATE,APPROVE_FLAG,TYPE,EMP_CLASS,CARD_ID,COMP_ID) values
(:employeeNameSt,:EMP_ANAME_SC,:EMP_ANAME_TH,:EMP_ANAME_FR,:EMP_ENAME_ST,:EMP_ENAME_SC,:EMP_ENAME_TH,:EMP_ENAME_FR,:EMP_ANAME,
:EMP_ENAME,:NATIONAL_ID,:BRANCH,:ADDRESS,:MOBILEEMP_RELATION,:emppppp,:DISEASE,:EMAIL,:BIRTHDATE,:GENDER,:START_DATE,:EMP_CODE,
:GLASSES,:REGISTER_TYPE,:CREATED_BY,sysdate,:APPROVE_FLAG,:TYPE,:EMP_CLASS,:CARD_ID,:COMP_ID)", con);
                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":employeeNameSt", OracleType.VarChar).Value = employeeNameSt;
                cmd.Parameters.Add(":EMP_ANAME_SC", OracleType.VarChar).Value = EMP_ANAME_SC;
                cmd.Parameters.Add(":EMP_ANAME_TH", OracleType.VarChar).Value = EMP_ANAME_TH;
                cmd.Parameters.Add(":emppppp", OracleType.VarChar).Value = emppppp;
                cmd.Parameters.Add(":EMP_ANAME_FR", OracleType.VarChar).Value = EMP_ANAME_FR;
                cmd.Parameters.Add(":EMP_ENAME_ST", OracleType.VarChar).Value = EMP_ENAME_ST;
                cmd.Parameters.Add(":EMP_ENAME_SC", OracleType.VarChar).Value = EMP_ENAME_SC;
                cmd.Parameters.Add(":EMP_ENAME_TH", OracleType.VarChar).Value = EMP_ENAME_TH;
                cmd.Parameters.Add(":EMP_ENAME_FR", OracleType.VarChar).Value = EMP_ENAME_FR;
                cmd.Parameters.Add(":EMP_ANAME", OracleType.VarChar).Value = EMP_ANAME;
                cmd.Parameters.Add(":EMP_ENAME", OracleType.VarChar).Value = EMP_ENAME;
                cmd.Parameters.Add(":NATIONAL_ID", OracleType.VarChar).Value = NATIONAL_ID;
                cmd.Parameters.Add(":BRANCH", OracleType.VarChar).Value = BRANCH;
                cmd.Parameters.Add(":ADDRESS", OracleType.VarChar).Value = ADDRESS;
                cmd.Parameters.Add(":MOBILEEMP_RELATION", OracleType.VarChar).Value = MOBILEEMP_RELATION;
                cmd.Parameters.Add(":DISEASE", OracleType.VarChar).Value = DISEASE;
                cmd.Parameters.Add(":EMAIL", OracleType.VarChar).Value = EMAIL;
                cmd.Parameters.Add(":BIRTHDATE", OracleType.DateTime).Value = BIRTHDATE;
                cmd.Parameters.Add(":GENDER", OracleType.VarChar).Value = GENDER;
                cmd.Parameters.Add(":START_DATE", OracleType.DateTime).Value = START_DATE;
                cmd.Parameters.Add(":EMP_CODE", OracleType.Number).Value = EMP_CODE;
                cmd.Parameters.Add(":GLASSES", OracleType.VarChar).Value = ndaraaaaaa;
                cmd.Parameters.Add(":REGISTER_TYPE", OracleType.VarChar).Value = 'P';
                cmd.Parameters.Add(":CREATED_BY", OracleType.VarChar).Value = CREATED_BY;
                //   cmd.Parameters.Add(":CREATED_DATE", OracleType.DateTime).Value = "sysdate";
                cmd.Parameters.Add(":APPROVE_FLAG", OracleType.VarChar).Value = 'N';
                cmd.Parameters.Add(":TYPE", OracleType.VarChar).Value = '1';
                cmd.Parameters.Add(":EMP_CLASS", OracleType.VarChar).Value = EMP_CLASS;
                cmd.Parameters.Add(":CARD_ID", OracleType.VarChar).Value = CARD_ID;
                cmd.Parameters.Add(":COMP_ID", OracleType.Number).Value = COMP_ID;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        public DataTable getinfodat(string state, string remo, string state2, string remo2, string srch, DateTime dfrom, DateTime dto, string typ)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            dd = new DataTable();
            try
            {






                con.Open();


                // cmd = new OracleCommand(@"INSERT INTO REASON_DISCONTS (CLAIM_NO,CARD_NO,CLAIM_DATE,CLAIM_AMOUNT,CLAIM_ITEM_DED,CLAIM_NET,REASON_CODE,CODE) VALUES (:clm,:crd,:clmdat,:clmamo,:clmded,clmnet,:rescod,:cod)", con);
                cmd = new OracleCommand(@"select EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID, EMPLOYEE_REQUEST.COMP_ID, EMPLOYEE_REQUEST.EMP_CODE, EMPLOYEE_REQUEST_TYPE.TYPE_NAME , to_char(EMPLOYEE_REQUEST.CREATED_DATE,'DD-MM-YYYY') CREATED_DATE,EMPLOYEE_REQUEST.CREATED_BY AS, EMPLOYEE_REQUEST.UPDATED_BY,case 
                                          when upper(EMPLOYEE_REQUEST.APPROVE_FLAG) = 'Y' then 'Accepted'   when upper(EMPLOYEE_REQUEST.APPROVE_FLAG) = 'D' then 'Done'   when upper(EMPLOYEE_REQUEST.APPROVE_FLAG) = 'F' then case 
                                          when upper(EMPLOYEE_REQUEST.FLAG_REMOVE) = 'F' then 'Rejected' when upper(EMPLOYEE_REQUEST.FLAG_REMOVE) = 'N' then 'Under Processing' else 'f' end  when upper(EMPLOYEE_REQUEST.APPROVE_FLAG) = 'N' then case 
                                          when upper(EMPLOYEE_REQUEST.FLAG_REMOVE) = 'N' then 'Pending' when upper(EMPLOYEE_REQUEST.FLAG_REMOVE) = 'D' then 'Under Processing' else 'd' end   else'g' end status, to_char(EMPLOYEE_REQUEST.DATE_CHANGE_TYP,'DD-MM-YYYY') DATE_CHANGE_TYP, EMPLOYEE_REQUEST.RESON from EMPLOYEE_REQUEST, EMPLOYEE_REQUEST_TYPE 
                                          where EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID  and ((upper(EMPLOYEE_REQUEST.APPROVE_FLAG) like '%' || :state || '%' and upper(EMPLOYEE_REQUEST.FLAG_REMOVE) like '%' || :remo || '%') or (upper(EMPLOYEE_REQUEST.APPROVE_FLAG) like '%' || :state2 || '%' 
                                          and upper(EMPLOYEE_REQUEST.FLAG_REMOVE) like '%' || :remo2 || '%') ) and  ( EMPLOYEE_REQUEST.REQUEST_CODE like '%' || :srch || '%' or EMPLOYEE_REQUEST.CARD_ID like '%' || :srch || '%' or EMPLOYEE_REQUEST.CREATED_BY like '%' || :srch || '%' ) 
                                          and (created_date between :dfrom and :dto ) AND TYPE like '%' || :typ || '%' ", con);

                //      (    Convert.ToInt32(Row[4].ToString()), Row[5].ToString(), Convert.ToInt32(Row[6]), Row[7].ToString(), dia, Row[9].ToString(), Row[10].ToString(), "", txtindcode.Text);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":state", OracleType.VarChar).Value = state;
                cmd.Parameters.Add(":remo", OracleType.VarChar).Value = remo;
                cmd.Parameters.Add(":state2", OracleType.VarChar).Value = state2;
                cmd.Parameters.Add(":remo2", OracleType.VarChar).Value = remo2;
                cmd.Parameters.Add(":srch", OracleType.VarChar).Value = srch;
                cmd.Parameters.Add(":typ", OracleType.VarChar).Value = typ;
                cmd.Parameters.Add(":dfrom", OracleType.DateTime).Value = dfrom;
                cmd.Parameters.Add(":dto", OracleType.DateTime).Value = dto;


                //   cmd.ExecuteNonQuery();
                da = new OracleDataAdapter(cmd);
                // dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
               
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }



        }

        public DataTable getoperanoti(string typ, DateTime dfrom, DateTime dto, int dy)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            dd = new DataTable();
            try
            {
                con.Open();
                if (DateTime.Now.Date == dto.Date || DateTime.Now.Date == dto.AddDays(1).Date)
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (typ == "F")
                            cmd = new OracleCommand(@"SELECT  CREATED_BY ,REQUEST_CODE  ,UPDATED_BY   ,APPROVE_FLAG    ,REGISTER_TYPE     ,RESON  FROM EMPLOYEE_REQUEST WHERE upper(APPROVE_FLAG)=:typ AND CREATED_DATE BETWEEN :dfrom AND :dto AND CREATED_DATE < (sysdate - 4)", con);
                        else
                            cmd = new OracleCommand(@"SELECT CREATED_BY ,REQUEST_CODE  ,UPDATED_BY   ,APPROVE_FLAG    ,REGISTER_TYPE     ,RESON  FROM EMPLOYEE_REQUEST WHERE (upper(FLAG_REMOVE) = :typ AND (upper(APPROVE_FLAG)= :typ or upper(APPROVE_FLAG)=:typ)) AND CREATED_DATE BETWEEN :dfrom AND :dto AND CREATED_DATE < (sysdate - 4)", con);
                    }
                    else
                    {
                        if (typ == "F")
                            cmd = new OracleCommand(@"SELECT CREATED_BY ,REQUEST_CODE  ,UPDATED_BY   ,APPROVE_FLAG    ,REGISTER_TYPE     ,RESON  FROM EMPLOYEE_REQUEST WHERE upper(APPROVE_FLAG)=:typ AND CREATED_DATE BETWEEN :dfrom AND :dto AND CREATED_DATE < (sysdate - 2)", con);
                        else
                            cmd = new OracleCommand(@"SELECT CREATED_BY ,REQUEST_CODE  ,UPDATED_BY   ,APPROVE_FLAG    ,REGISTER_TYPE     ,RESON  FROM EMPLOYEE_REQUEST WHERE (upper(FLAG_REMOVE) = :typ AND (upper(APPROVE_FLAG)= :typ or upper(APPROVE_FLAG)=:typ)) AND CREATED_DATE BETWEEN :dfrom AND :dto AND CREATED_DATE < (sysdate - 4)", con);
                    }
                }
                else
                {
                    if (typ == "F")
                        cmd = new OracleCommand(@"SELECT CREATED_BY ,REQUEST_CODE  ,UPDATED_BY   ,APPROVE_FLAG    ,REGISTER_TYPE     ,RESON  FROM EMPLOYEE_REQUEST WHERE upper(APPROVE_FLAG)=:typ AND CREATED_DATE BETWEEN :dfrom AND :dto", con);
                    else
                        cmd = new OracleCommand(@"SELECT CREATED_BY ,REQUEST_CODE  ,UPDATED_BY   ,APPROVE_FLAG    ,REGISTER_TYPE     ,RESON  FROM EMPLOYEE_REQUEST WHERE (upper(FLAG_REMOVE) = :typ AND (upper(APPROVE_FLAG)= :typ or upper(APPROVE_FLAG)=:typ)) AND CREATED_DATE BETWEEN :dfrom AND :dto", con);
                }
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":typ", OracleType.VarChar).Value = typ;
                //   cmd.Parameters.Add(":dy", OracleType.Number).Value = dy;
                cmd.Parameters.Add(":dfrom", OracleType.DateTime).Value = dfrom;
                cmd.Parameters.Add(":dto", OracleType.DateTime).Value = dto;


                da = new OracleDataAdapter(cmd);


                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }



        }
        public int insapprov(string cod, Int32 comp, string crd, string fx, string emil, Int64 mob, DateTime rcdat, DateTime sedat, string apptyp, Int64 pvdnum,
                  string rply, string medrply, string nts, double appval, double valaft, string srvtyp, string pvd, string rato, string mxamun, byte[] appimg, string contr,
                  string mxamutcontr, string clss, string cretby, DateTime cretdat, string anam, string enam, DateTime birth, DateTime strtdat, DateTime enddat, double totcon,
                  string pvdnam, Int32 sbcod, string cmpnam, Int64 digcod, string dignam, string flg, string flg2, string chpcent, string rsnrecol, string vist, string nampat, string idpat, string rcv, string snd, Int32 prvdid)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            if (con.State != ConnectionState.Open)
                con.Open();
            try
            {
                cmd = new OracleCommand(@"  INSERT INTO MEDICAL_APPROVALS (CODE, COMPANY_ID, CARD_NO, FAX, EMAIL, MOBILE_NUMBER, RECIV_DATE, SEND_DATE, APROVAL_TYP, PROVIDER_NUM, REPLAY, 
                                            MEDICAL_REPLAY, NOTS, APROVAL_VALUE, VALUE_AFTER, SERVECE_TYP, PROVIDER_TYP, ENDURANCE_RATIO, MAX_AMOUNT, APROVAL_IMAG, COMP_CONTRACT_NO, 
                                            MAX_AMOUNT_CONTRACT, CLASS_CODE, CREATED_BY, CREATED_DATE, EMP_ANAME, EMP_ENAME, BIRTHDAY, START_DATE, END_DATE, TOT_CONSUM, PROVIDER_NAME,
                                            SUB_CODE,COMP_NAME, DIAG_CODE, DIAG_NAME, STATUS, STATUS2, CHANGE_PERCENT, RECOLLECTION_REASON, VISIT, NAME_EMP_CHKUP, ID_EMP_CHKUP, RECEIVE_TIME, SEND_TIME, PROVIDER_ID)
                                            VALUES 
                                            (:cod, :comp, :crd, :fx, :emil, :mob, :rcdat, :sedat, :apptyp, :pvdnum, :rply, :medrply, :nts, :appval, :valaft, :srvtyp, :pvd, :rato, :mxamun, 
                                            :appimg, :contr, :mxamutcontr, :clss, :cretby, :cretdat, :anam, :enam, :birth, :strtdat, :enddat, :totcon, :pvdnam,:sbcod,:cmpnam,:digcod,:dignam,
                                            :flg,:flg2, :chpcent, :rsnrecol, :vist, :nampat, :idpat, :rcv, :snd, :prvdid)", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":cod", OracleType.VarChar).Value = cod;
                cmd.Parameters.Add(":comp", OracleType.Number).Value = comp;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":fx", OracleType.VarChar).Value = fx;
                cmd.Parameters.Add(":emil", OracleType.VarChar).Value = emil;
                cmd.Parameters.Add(":mob", OracleType.Number).Value = mob;
                cmd.Parameters.Add(":rcdat", OracleType.DateTime).Value = rcdat;
                cmd.Parameters.Add(":sedat", OracleType.DateTime).Value = sedat;
                cmd.Parameters.Add(":apptyp", OracleType.VarChar).Value = apptyp;
                cmd.Parameters.Add(":pvdnum", OracleType.Number).Value = pvdnum;
                cmd.Parameters.Add(":rply", OracleType.VarChar).Value = rply;
                cmd.Parameters.Add(":medrply", OracleType.VarChar).Value = medrply;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":appval", OracleType.Number).Value = appval;
                cmd.Parameters.Add(":valaft", OracleType.Number).Value = valaft;
                cmd.Parameters.Add(":srvtyp", OracleType.VarChar).Value = srvtyp;
                cmd.Parameters.Add(":pvd", OracleType.VarChar).Value = pvd;
                cmd.Parameters.Add(":rato", OracleType.VarChar).Value = rato;
                cmd.Parameters.Add(":mxamun", OracleType.VarChar).Value = mxamun;
                cmd.Parameters.Add(":appimg", OracleType.Blob).Value = appimg;
                cmd.Parameters.Add(":contr", OracleType.VarChar).Value = contr;
                cmd.Parameters.Add(":mxamutcontr", OracleType.VarChar).Value = mxamutcontr;
                cmd.Parameters.Add(":clss", OracleType.VarChar).Value = clss;
                cmd.Parameters.Add(":cretby", OracleType.VarChar).Value = cretby;
                cmd.Parameters.Add(":cretdat", OracleType.DateTime).Value = cretdat;
                cmd.Parameters.Add(":anam", OracleType.VarChar).Value = anam;
                cmd.Parameters.Add(":enam", OracleType.VarChar).Value = enam;
                cmd.Parameters.Add(":birth", OracleType.DateTime).Value = birth;
                cmd.Parameters.Add(":strtdat", OracleType.DateTime).Value = strtdat;
                cmd.Parameters.Add(":enddat", OracleType.DateTime).Value = enddat;
                cmd.Parameters.Add(":totcon", OracleType.Number).Value = totcon;
                cmd.Parameters.Add(":sbcod", OracleType.Number).Value = sbcod;
                cmd.Parameters.Add(":pvdnam", OracleType.VarChar).Value = pvdnam;
                cmd.Parameters.Add(":cmpnam", OracleType.VarChar).Value = cmpnam;
                cmd.Parameters.Add(":digcod", OracleType.Number).Value = digcod;
                cmd.Parameters.Add(":dignam", OracleType.VarChar).Value = dignam;
                cmd.Parameters.Add(":flg", OracleType.VarChar).Value = flg;
                cmd.Parameters.Add(":flg2", OracleType.VarChar).Value = flg2;
                cmd.Parameters.Add(":chpcent", OracleType.VarChar).Value = chpcent;
                cmd.Parameters.Add(":rsnrecol", OracleType.VarChar).Value = rsnrecol;
                cmd.Parameters.Add(":vist", OracleType.VarChar).Value = vist;
                cmd.Parameters.Add(":nampat", OracleType.VarChar).Value = nampat;
                cmd.Parameters.Add(":idpat", OracleType.VarChar).Value = idpat;
                cmd.Parameters.Add(":rcv", OracleType.VarChar).Value = rcv;
                cmd.Parameters.Add(":snd", OracleType.VarChar).Value = snd;
                cmd.Parameters.Add(":prvdid", OracleType.Number).Value = prvdid;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return 1;
            }
            catch//(Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                //  MessageBox.Show("حدثت مشكلة أثناء الحفظ من فضلك حاول ثانية");
                return 0;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }

            }

        }
        public int updateapprov(string cod, Int32 comp, string crd, string fx, string emil, Int64 mob, DateTime rcdat, DateTime sedat, string apptyp, Int64 pvdnum,
           string rply, string medrply, string nts, double appval, double valaft, string srvtyp, string pvd, string rato, string mxamun, byte[] appimg, string contr,
           string mxamutcontr, string clss, string cretby, DateTime cretdat, string anam, string enam, DateTime birth, DateTime strtdat, DateTime enddat, double totcon,
           string pvdnam, string cmpnam, Int64 digcod, string dignam, string flg, string flg2, string chpcent, string rsnrecol, string vist, string nampat, string idpat, string rcv, string snd, Int32 prvdid)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            //con.Open();
            if (con.State != ConnectionState.Open)
                con.Open();
            try
            {
                cmd = new OracleCommand(@"  UPDATE MEDICAL_APPROVALS SET COMPANY_ID = :comp, CARD_NO = :crd, FAX = :fx, EMAIL = :emil, MOBILE_NUMBER = :mob, RECIV_DATE = :rcdat, SEND_DATE = :sedat, 
                                            APROVAL_TYP = :apptyp, PROVIDER_NUM = :pvdnum, REPLAY = :rply, MEDICAL_REPLAY = :medrply, NOTS = :nts, APROVAL_VALUE = :appval, VALUE_AFTER = :valaft, 
                                            SERVECE_TYP = :srvtyp, PROVIDER_TYP = :pvd, ENDURANCE_RATIO = :rato, MAX_AMOUNT = :mxamun, APROVAL_IMAG = :appimg, COMP_CONTRACT_NO = :contr, 
                                            MAX_AMOUNT_CONTRACT = :mxamutcontr, CLASS_CODE = :clss, UPDATED_BY = :cretby, UPDATED_DATE = :cretdat, EMP_ANAME = :anam, EMP_ENAME = :enam, 
                                            BIRTHDAY = :birth, START_DATE = :strtdat, END_DATE = :enddat, TOT_CONSUM = :totcon, PROVIDER_NAME = :pvdnam, /*SUB_CODE = :sbcod,*/ 
                                            COMP_NAME = :cmpnam, DIAG_CODE = :digcod, DIAG_NAME = :dignam, STATUS = :flg, STATUS2 = :flg2, CHANGE_PERCENT = :chpcent, RECOLLECTION_REASON = :rsnrecoll,
                                            VISIT = :vist, NAME_EMP_CHKUP = :nampat, ID_EMP_CHKUP = :idpat, RECEIVE_TIME = :rcv, SEND_TIME = :snd, PROVIDER_ID = :prvdid WHERE CODE = :cod", con);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":cod", OracleType.VarChar).Value = cod;
                cmd.Parameters.Add(":comp", OracleType.Number).Value = comp;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":fx", OracleType.VarChar).Value = fx;
                cmd.Parameters.Add(":emil", OracleType.VarChar).Value = emil;
                cmd.Parameters.Add(":mob", OracleType.Number).Value = mob;
                cmd.Parameters.Add(":rcdat", OracleType.DateTime).Value = rcdat;
                cmd.Parameters.Add(":sedat", OracleType.DateTime).Value = sedat;
                cmd.Parameters.Add(":apptyp", OracleType.VarChar).Value = apptyp;
                cmd.Parameters.Add(":pvdnum", OracleType.Number).Value = pvdnum;
                cmd.Parameters.Add(":rply", OracleType.VarChar).Value = rply;
                cmd.Parameters.Add(":medrply", OracleType.VarChar).Value = medrply;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":appval", OracleType.Number).Value = appval;
                cmd.Parameters.Add(":valaft", OracleType.Number).Value = valaft;
                cmd.Parameters.Add(":srvtyp", OracleType.VarChar).Value = srvtyp;
                cmd.Parameters.Add(":pvd", OracleType.VarChar).Value = pvd;
                cmd.Parameters.Add(":rato", OracleType.VarChar).Value = rato;
                cmd.Parameters.Add(":mxamun", OracleType.VarChar).Value = mxamun;
                cmd.Parameters.Add(":appimg", OracleType.Blob).Value = appimg;
                cmd.Parameters.Add(":contr", OracleType.VarChar).Value = contr;
                cmd.Parameters.Add(":mxamutcontr", OracleType.VarChar).Value = mxamutcontr;
                cmd.Parameters.Add(":clss", OracleType.VarChar).Value = clss;
                cmd.Parameters.Add(":cretby", OracleType.VarChar).Value = cretby;
                cmd.Parameters.Add(":cretdat", OracleType.DateTime).Value = cretdat;
                cmd.Parameters.Add(":anam", OracleType.VarChar).Value = anam;
                cmd.Parameters.Add(":enam", OracleType.VarChar).Value = enam;
                cmd.Parameters.Add(":birth", OracleType.DateTime).Value = birth;
                cmd.Parameters.Add(":strtdat", OracleType.DateTime).Value = strtdat;
                cmd.Parameters.Add(":enddat", OracleType.DateTime).Value = enddat;
                cmd.Parameters.Add(":totcon", OracleType.Number).Value = totcon;
                //    cmd.Parameters.Add(":sbcod", OracleType.Number).Value = sbcod;
                cmd.Parameters.Add(":pvdnam", OracleType.VarChar).Value = pvdnam;
                cmd.Parameters.Add(":cmpnam", OracleType.VarChar).Value = cmpnam;
                cmd.Parameters.Add(":digcod", OracleType.Number).Value = digcod;
                cmd.Parameters.Add(":dignam", OracleType.VarChar).Value = dignam;
                cmd.Parameters.Add(":flg", OracleType.VarChar).Value = flg;
                cmd.Parameters.Add(":flg2", OracleType.VarChar).Value = flg2;
                cmd.Parameters.Add(":chpcent", OracleType.VarChar).Value = chpcent;
                cmd.Parameters.Add(":rsnrecoll", OracleType.VarChar).Value = rsnrecol;
                cmd.Parameters.Add(":vist", OracleType.VarChar).Value = vist;
                cmd.Parameters.Add(":nampat", OracleType.VarChar).Value = nampat;
                cmd.Parameters.Add(":idpat", OracleType.VarChar).Value = idpat;
                cmd.Parameters.Add(":rcv", OracleType.VarChar).Value = rcv;
                cmd.Parameters.Add(":snd", OracleType.VarChar).Value = snd;
                cmd.Parameters.Add(":prvdid", OracleType.Number).Value = prvdid;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return 1;
            }
            catch
            {
                //  MessageBox.Show("حدثت مشكلة أثناء الحفظ من فضلك حاول ثانية");
                return 0;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }

            }

        }

     
        public DataTable getonlinevalue2(string crd, DateTime dat1, DateTime dat2, int flg)
        {

            //            OracleConnection con;
            //            OracleCommand cmd = new OracleCommand();
            //            OracleDataAdapter da;
            //            con = new OracleConnection(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
            //                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
            //                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******");
            OracleConnection con = new OracleConnection(conction);

            OracleCommand cmd = new OracleCommand();

            OracleDataAdapter da;
            DataTable dd = new DataTable();


            try
            {
                if (flg == 1)
                    cmd = new OracleCommand(@" SELECT NVL(SUM(CLAIM_AMOUNT),0) FROM ONLINE_CONS_01 WHERE card_no =:crd and claim_date BETWEEN :dat1 AND :dat2 AND group_no = 116", con);
                else
                    cmd = new OracleCommand(@" SELECT NVL(SUM(CLAIM_AMOUNT),0) FROM ONLINE_CONS_01 WHERE card_no =:crd and claim_date BETWEEN :dat1 AND :dat2 AND group_no != 116", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch { return dd; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }

            }
        }


        public DataTable getonIRSvalue2(string crd, DateTime dat1, DateTime dat2)
        {

            //            OracleConnection con;
            //            OracleCommand cmd = new OracleCommand();
            //            OracleDataAdapter da;
            //            con = new OracleConnection(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
            //                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
            //                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******");
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            DataTable dd = new DataTable();
            try
            {

                //select sum(claim_net) from CLAIM_REC_H where card_no='10453-1-5057-1' where claim_date 
                cmd = new OracleCommand(@"select  NVL(SUM(claim_net),0) from CLAIM_REC_H where card_no=:crd where claim_date BETWEEN :dat1 AND :dat2 ", con);


                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch { return dd; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }

            }
        }

        public DataTable getonlinevalue(string crd, DateTime dat1, DateTime dat2, int flg)
        {

            //            OracleConnection con;
            //            OracleCommand cmd = new OracleCommand();
            //            OracleDataAdapter da;
            //            con = new OracleConnection(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
            //                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
            //                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******");
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            DataTable dd = new DataTable();
            try
            {
                if (flg == 1)
                    cmd = new OracleCommand(@" SELECT NVL(SUM(CLAIM_AMOUNT),0) FROM ONLINE_CONS_01 WHERE card_no =:crd and claim_date BETWEEN :dat1 AND sysdate AND group_no = 116", con);
                else
                    cmd = new OracleCommand(@" SELECT NVL(SUM(CLAIM_AMOUNT),0) FROM ONLINE_CONS_01 WHERE card_no =:crd and claim_date BETWEEN :dat1 AND sysdate AND group_no != 116", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                //  cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch { return dd; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }

            }
        }
        public DataTable getnewrecolect(string cmp, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            DataTable dd = new DataTable();
            try
            {
                cmd = new OracleCommand(@"SELECT SUM(BILL_AMOUNT), SUM(BILL_PAYMENT) FROM COLLECT_BILL WHERE COMP_CODE = :cmp AND COMP_CONTRACT_START_DATE = :dat1 AND COMP_CONTRACT_END_DATE = :dat2", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":cmp", OracleType.VarChar).Value = cmp;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch { return dd; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }

            }
        }


        public void insimghistapprov(string cod, string res, Int32 updcnt, byte[] oldappimg, byte[] newappimg)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"  INSERT INTO APPROVAL_CHANGE (CODE, ACTION, REASON, UPDATED_BY, UPDATED_DATE, UPDATED_COUNT, NAME_CHANGE, OLD_IMG, NEW_IMG)
                                                             VALUES (:cod, 'EDIT', :res  , :updby    , sysdate   , :updcnt      , 'APROVAL_IMAG', :oldimg , :newimg)", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":cod", OracleType.VarChar).Value = cod;
                cmd.Parameters.Add(":res", OracleType.VarChar).Value = res;
                cmd.Parameters.Add(":updby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":updcnt", OracleType.Number).Value = updcnt;
                cmd.Parameters.Add(":oldimg", OracleType.Blob).Value = oldappimg;
                cmd.Parameters.Add(":newimg", OracleType.Blob).Value = newappimg;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public string getnewaprovavalue(string crd, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {

                cmd = new OracleCommand(@" select sum(APPROV_AMOUNT) from V_APPROVAL where CARD_NO = :crd and CREATED_DATE between (select  max(INS_START_DATE) from COMP_EMPLOYEESS where card_id = :crd) and :dat2 AND APROV_REPLY = 'Y'  ", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                // cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd.Rows[0][0].ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void setreplyreviewapproval(string rply, string notee, string cod, byte[] img)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                
                con.Open();

                cmd = new OracleCommand(@"UPDATE REVIEWAPPROVAL SET REPLY_REVIEW = :rply, FINAL_REVIEW_DATE = sysdate, 
                                          REVIEW_BY = :usr, NOTE = :notee, ACTIVE = 'Y', IMAGE_REV = :img where code = :cod", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":rply", OracleType.VarChar).Value = rply;
                cmd.Parameters.Add(":usr", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":notee", OracleType.VarChar).Value = notee;
                cmd.Parameters.Add(":cod", OracleType.VarChar).Value = cod;
                cmd.Parameters.Add(":img", OracleType.Blob).Value = img;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }




        public void t7seel_byana_tFatora(string com_code, string bill_num, string bill_typ, string billdate, string deleverdate, string editedate, string billfrom, string billto, string billamount, string billpayment, string billdifference, string COMPCONTRACTSTART, string COMPCONTRACTEND, string code)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {
                con.Open();

                cmd = new OracleCommand(
                    @"INSERT INTO COLLECT_BILL (COMP_CODE, BILL_NUMBER, BILL_TYP, BILL_DATE, DELEVER_DATE, EDITE_DATE, BILL_FROM_DATE, BILL_TO_DATE, BILL_AMOUNT, BILL_PAYMENT, BILL_DIFFERENCE, CREATED_BY, CREATED_DATE, COMP_CONTRACT_START_DATE, COMP_CONTRACT_END_DATE,CODE) VALUES ('" +
                    com_code + "', '" + bill_num + "', '" + bill_typ +
                    "',:billdate, :deleverdate, :editedate,:billfrom, :billto, '" + billamount + "', '" + billpayment +
                    "', '" + billdifference + "', '" + User.Name +
                    "', sysdate, :COMPCONTRACTSTART, :COMPCONTRACTEND,'" + code + "')", con);
                string x = (5 > 8) ? "dsf" : "sadsa";
                DateTime dte;
                cmd.Parameters.Clear();

                if (billdate == string.Empty)
                    cmd.Parameters.Add(":billdate", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":billdate", OracleType.DateTime).Value = Convert.ToDateTime(billdate);

                if (deleverdate == string.Empty)
                    cmd.Parameters.Add(":deleverdate", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":deleverdate", OracleType.DateTime).Value = Convert.ToDateTime(deleverdate);

                if (editedate == string.Empty)
                    cmd.Parameters.Add(":editedate", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":editedate", OracleType.DateTime).Value = Convert.ToDateTime(editedate);


                if (billfrom == string.Empty)
                    cmd.Parameters.Add(":billfrom", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":billfrom", OracleType.DateTime).Value = Convert.ToDateTime(billfrom);


                if (billto == string.Empty)
                    cmd.Parameters.Add(":billto", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":billto", OracleType.DateTime).Value = Convert.ToDateTime(billto);

                if (COMPCONTRACTSTART == string.Empty)
                    cmd.Parameters.Add(":COMPCONTRACTSTART", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":COMPCONTRACTSTART", OracleType.DateTime).Value = Convert.ToDateTime(COMPCONTRACTSTART);

                if (COMPCONTRACTEND == string.Empty)
                    cmd.Parameters.Add(":COMPCONTRACTEND", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":COMPCONTRACTEND", OracleType.DateTime).Value = Convert.ToDateTime(COMPCONTRACTEND);




                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void t7seel_byana_tFatora_Edite(string com_code, string bill_num, string bill_typ, string billdate, string deleverdate, string editedate, string billfrom, string billto, string billamount, string billpayment, string billdifference, string COMPCONTRACTSTART, string COMPCONTRACTEND, string code)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            //   UPDATE "APP"."COLLECT_BILL" SET BILL_TYP = 'Exceedig', BILL_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), DELEVER_DATE = TO_DATE('2014-02-10 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), EDITE_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_FROM_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_TO_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_AMOUNT = '67777345', BILL_PAYMENT = '234', BILL_DIFFERENCE = '456', UPDATED_BY = 'a', UPDATED_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS') WHERE ROWID = 'AAAhjiAAHAACgQtAAB' AND ORA_ROWSCN = '34778100'

            try
            {
                con.Open();

                cmd = new OracleCommand(@"UPDATE COLLECT_BILL SET BILL_TYP = '" + bill_typ + "', BILL_DATE = :billdate, DELEVER_DATE = :deleverdate, EDITE_DATE = :editedate, BILL_FROM_DATE = :billfrom, BILL_TO_DATE = :billto, BILL_AMOUNT = '" + billamount + "', BILL_PAYMENT = '" + billpayment + "', BILL_DIFFERENCE = '" + billdifference + "', UPDATED_BY = '" + User.Name + "', UPDATED_DATE = sysdate WHERE CODE ='" + code + "'  ", con);

                DateTime dte;
                cmd.Parameters.Clear();

                if (billdate == string.Empty)
                    cmd.Parameters.Add(":billdate", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":billdate", OracleType.DateTime).Value = Convert.ToDateTime(billdate);

                if (deleverdate == string.Empty)
                    cmd.Parameters.Add(":deleverdate", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":deleverdate", OracleType.DateTime).Value = Convert.ToDateTime(deleverdate);

                if (editedate == string.Empty)
                    cmd.Parameters.Add(":editedate", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":editedate", OracleType.DateTime).Value = Convert.ToDateTime(editedate);


                if (billfrom == string.Empty)
                    cmd.Parameters.Add(":billfrom", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":billfrom", OracleType.DateTime).Value = Convert.ToDateTime(billfrom);


                if (billto == string.Empty)
                    cmd.Parameters.Add(":billto", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":billto", OracleType.DateTime).Value = Convert.ToDateTime(billto);



                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                MessageBox.Show("تم");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void UPDATED_History(string s_tb_byanat_rkm, string S_tb_byanat_tsleem, string s_amountbefor, string s_tb_byanat_kema, string code)
        {

            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();

                cmd = new OracleCommand(@"INSERT INTO COLLECT_BILL_HISTORY (CODE,BILL_CODE, UPDATED_DATE, BIIL_AMOUNT_BEFOR, BIIL_AMOUNT_AFTER) VALUES ('" + code + "',:p_s_tb_byanat_rkm,  :P_UPDATED_DATE, :p_s_amountbefor, :p_s_tb_byanat_kema) ", con);

                DateTime dte;
                cmd.Parameters.Clear();

                cmd.Parameters.Add(":p_s_tb_byanat_rkm", OracleType.VarChar).Value = s_tb_byanat_rkm;
                if (S_tb_byanat_tsleem.Trim() == string.Empty)
                    cmd.Parameters.Add(":P_UPDATED_DATE", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":P_UPDATED_DATE", OracleType.DateTime).Value = Convert.ToDateTime(S_tb_byanat_tsleem);
                cmd.Parameters.Add(":p_s_amountbefor", OracleType.VarChar).Value = s_amountbefor;
                cmd.Parameters.Add(":p_s_tb_byanat_kema", OracleType.VarChar).Value = s_tb_byanat_kema;



                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();



            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void t7seel_byana_check_Edite(string com_code, string bill_num, string chek_code, string chek_typ, string deleverdat, string collectdate, string checkamount, string reason, string code)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            //   UPDATE "APP"."COLLECT_BILL" SET BILL_TYP = 'Exceedig', BILL_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), DELEVER_DATE = TO_DATE('2014-02-10 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), EDITE_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_FROM_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_TO_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_AMOUNT = '67777345', BILL_PAYMENT = '234', BILL_DIFFERENCE = '456', UPDATED_BY = 'a', UPDATED_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS') WHERE ROWID = 'AAAhjiAAHAACgQtAAB' AND ORA_ROWSCN = '34778100'
            //
            try
            {
                con.Open();

                cmd = new OracleCommand(@"INSERT INTO COLLECT_CHECK (COMP_CODE, BILL_CODE, CHECK_CODE, CHECK_TYP, DELEVER_DATE, COLLECT_DATE, CHECK_AMOUNT, CREATED_BY,CREATED_DATE,DISCOUNT_REASON,CODE) VALUES ('" + com_code + "', '" + bill_num + "', '" + chek_code + "', '" + chek_typ + "', :deleverdat, :collectdate, '" + checkamount + "', '" + User.Name + "',sysdate,'" + reason + "','" + code + "' ) ", con);

                DateTime dte;
                cmd.Parameters.Clear();

                if (deleverdat == string.Empty)
                    cmd.Parameters.Add(":deleverdat", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":deleverdat", OracleType.DateTime).Value = Convert.ToDateTime(deleverdat);

                if (collectdate == string.Empty)
                    cmd.Parameters.Add(":collectdate", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":collectdate", OracleType.DateTime).Value = Convert.ToDateTime(collectdate);


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                new DB().RunNonQuery("UPDATE COLLECT_CHECK SET CHECK_TYP = '" + chek_typ + "' WHERE CODE='" + code + "' ");
                MessageBox.Show("تم");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void t7seel_byana_check_Edite_edite(string com_code, string bill_num, string chek_code, string chek_typ, string deleverdat, string collectdate, string checkamount, string reason, string code)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            //   UPDATE "APP"."COLLECT_BILL" SET BILL_TYP = 'Exceedig', BILL_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), DELEVER_DATE = TO_DATE('2014-02-10 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), EDITE_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_FROM_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_TO_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_AMOUNT = '67777345', BILL_PAYMENT = '234', BILL_DIFFERENCE = '456', UPDATED_BY = 'a', UPDATED_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS') WHERE ROWID = 'AAAhjiAAHAACgQtAAB' AND ORA_ROWSCN = '34778100'
            //
            try
            {
                con.Open();

                cmd = new OracleCommand(@"UPDATE COLLECT_CHECK SET UPDATED_BY ='" + User.Name + "' , UPDATED_DATE = sysdate , CHECK_TYP = '" + chek_typ + "' , DELEVER_DATE = :deleverdat , COLLECT_DATE = :collectdate ,DISCOUNT_REASON ='" + reason + "' , CHECK_AMOUNT = '" + checkamount + "'  WHERE CODE='" + code + "' ", con);

                DateTime dte;
                cmd.Parameters.Clear();

                if (deleverdat == string.Empty)
                    cmd.Parameters.Add(":deleverdat", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":deleverdat", OracleType.DateTime).Value = Convert.ToDateTime(deleverdat);

                if (collectdate == string.Empty)
                    cmd.Parameters.Add(":collectdate", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":collectdate", OracleType.DateTime).Value = Convert.ToDateTime(collectdate);


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                new DB().RunNonQuery("UPDATE COLLECT_CHECK SET CHECK_TYP = '" + chek_typ + "' WHERE CODE='" + code + "' ");
                MessageBox.Show("تم");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void UploadBill(DataTable dt)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();

                //OracleBulkCopy bulkCopy = new OracleBulkCopy(conex);

                //bulkCopy.DestinationTableName = "ME_AUB";
                //bulkCopy.WriteToServer(dt);


                cmd = new OracleCommand("Insert Into ME_AUB(CLAIM_NO1 , CREATED_DATE  , CLAIM_DATE, COMP_ID, CARD_NO ,  EMP_ENAME, EMP_ANAME, COMP_DEP_CODE, DIA_ENAME, PRV_NO , PR_ANAME, GROUP_NO, GROUP_ENAME, SERV_CODE, SERV_ENAME,  CLAIM_SERV_AMOUNT, CLAIM_DED, SERV_AMOUNT_APR, CLAIM_AMOUNT_PAYED,  CLAIM_DEDUCTIONS, CLAIM_NET, IMPORTED_DATE, CLASS_CODE, PERS_FLG, EMP_ID) values (:cno1, :creda , :clda, :cmp ,:carno, :emenam, :empanam, :copdc , :diena , :prno, :pranm , :grpno, :grpenm , :serco, :serenm,  :clseamo,:clded, :seramapr, :clamopayed, :cldeduct,  :clmnet, :datnow, :cls, :pres, :empid)", con);
                //cmd = new OracleCommand("Insert Into TEST (ID, NAME, PASSWORD, TYPE) VALUES (:id , :name , :pass, :typ)  ", con);
                // cmdo = new OracleCommand("Insert Into ME_AUB(CLAIM_NO1 , YEAR , CLAIM_DATE , CREATED_DATE, COMP_ID, CONTRACT_NO, CARD_NO, EMP_ENAME, COMP_DEP_CODE, DIA_CODE, DIA_NOTES, DIA_ANAME, DIA_ENAME, PRV_NO , PR_ANAME, PR_ENAME, SERV_CODE, SERV_ENAME, GROUP_NO, GROUP_ENAME, CLAIM_SERV_AMOUNT, CLAIM_DEDUCTIONS, CLAIM_DED, SERV_AMOUNT_APR, CLAIM_AMOUNT_PAYED, CLAIM_NET) values (:cno1, :ye, :clda, :creda , :coid, :conno, :carno, :emenam, :copdc , :dicod, :dinot, :diana, :diena , :prno, :pranm , :prenm, :serco, :serenm, :grpno, :grpenm , :clseamo, :cldeduct, :clded, :seramapr, :clamopayed, :clmnet)", con);

                foreach (DataRow r in dt.Rows)
                {

                    cmd.Parameters.Clear();

                    if (r["رقم المطالبة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":cno1", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":cno1", OracleType.Number).Value = Convert.ToInt64(r["رقم المطالبة"]);
                    // cmd.Parameters.Add(":ye", OracleType.Number).Value = int.Parse(r[1].ToString());

                    if (r["تاريخ التسجيل"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":creda", OracleType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":creda", OracleType.DateTime).Value = Convert.ToDateTime(r["تاريخ التسجيل"]).Date;

                    if (r["تاريخ الخدمة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clda", OracleType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clda", OracleType.DateTime).Value = Convert.ToDateTime(r["تاريخ الخدمة"]).Date;

                    // cmd.Parameters.Add(":coid", OracleType.Number).Value = Convert.ToInt32(r[4]);
                    // cmd.Parameters.Add(":conno", OracleType.Number).Value = int.Parse(r[5].ToString());

                    if (r["كود الشركة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":cmp", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":cmp", OracleType.Number).Value = Convert.ToInt64(r["كود الشركة"]);




                    cmd.Parameters.Add(":carno", OracleType.VarChar).Value = r["الرقم الطبي"].ToString();
                    cmd.Parameters.Add(":emenam", OracleType.VarChar).Value = r["اسم الموظف إنجليزي"].ToString();
                    cmd.Parameters.Add(":empanam", OracleType.VarChar).Value = r["اسم الموظف عربي"].ToString();

                    if (r["CC"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":copdc", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":copdc", OracleType.Number).Value = Convert.ToInt32(r["CC"]);

                    //  cmd.Parameters.Add(":dicod", OracleType.Number).Value = int.Parse(r[9].ToString());
                    //    cmd.Parameters.Add(":dinot", OracleType.VarChar).Value = r[10].ToString();
                    //  cmd.Parameters.Add(":diana", OracleType.VarChar).Value = r[11].ToString();
                    cmd.Parameters.Add(":diena", OracleType.VarChar).Value = r["التشخيص"].ToString();

                    if (r["رقم مقدم الخدمة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":prno", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":prno", OracleType.Number).Value = Convert.ToInt32(r["رقم مقدم الخدمة"]);


                    cmd.Parameters.Add(":pranm", OracleType.VarChar).Value = r["اسم مقدم الخدمة"].ToString();
                    // cmd.Parameters.Add(":prenm", OracleType.VarChar).Value = r[15].ToString();
                    if (r["كود الجروب"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":grpno", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":grpno", OracleType.Number).Value = int.Parse(r["كود الجروب"].ToString());

                    cmd.Parameters.Add(":grpenm", OracleType.VarChar).Value = r["اسم الجروب"].ToString();

                    if (r["كود الخدمة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":serco", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":serco", OracleType.Number).Value = int.Parse(r["كود الخدمة"].ToString());

                    cmd.Parameters.Add(":serenm", OracleType.VarChar).Value = r["اسم الخدمة"].ToString();

                    if (r["الاجمالى"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clseamo", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clseamo", OracleType.Number).Value = double.Parse(r["الاجمالى"].ToString());

                    if (r["بعد الخصم"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clded", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clded", OracleType.Number).Value = double.Parse(r["بعد الخصم"].ToString());

                    if (r["بعد التحمل"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":seramapr", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":seramapr", OracleType.Number).Value = double.Parse(r["بعد التحمل"].ToString());

                    if (r["الاستقطاعات"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clamopayed", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clamopayed", OracleType.Number).Value = double.Parse(r["الاستقطاعات"].ToString());


                    if (r["الخصم"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":cldeduct", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":cldeduct", OracleType.Number).Value = double.Parse(r["الخصم"].ToString());

                    if (r["الصافي"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clmnet", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clmnet", OracleType.Number).Value = double.Parse(r["الصافي"].ToString());

                    if (r["الفئة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":cls", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":cls", OracleType.Number).Value = Int32.Parse(r["الفئة"].ToString());

                    if (r["درجة القرابة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":pres", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":pres", OracleType.Number).Value = Int32.Parse(r["درجة القرابة"].ToString());

                    if (r["كود الموظف"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":empid", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":empid", OracleType.Number).Value = Int32.Parse(r["كود الموظف"].ToString());


                    cmd.Parameters.Add(":datnow", OracleType.DateTime).Value = DateTime.Now.Date;

                    cmd.ExecuteNonQuery();

                }



                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ بنجاح");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        public void UploadBill2(DataTable dt)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                con.Open();

                //OracleBulkCopy bulkCopy = new OracleBulkCopy(conex);

                //bulkCopy.DestinationTableName = "ME_AUB";
                //bulkCopy.WriteToServer(dt);

                cmd = new OracleCommand("Insert Into ME_AUB(CLAIM_NO1 , CREATED_DATE  , CLAIM_DATE, COMP_ID, CARD_NO ,  EMP_ENAME, EMP_ANAME, COMP_DEP_CODE, DIA_ENAME, PRV_NO , PR_ANAME, GROUP_NO, GROUP_ENAME, SERV_CODE, SERV_ENAME, SUB_SERV_AMT, CLAIM_SERV_AMOUNT, CLAIM_DED, SERV_AMOUNT_APR, CLAIM_AMOUNT_PAYED,  CLAIM_DEDUCTIONS, CLAIM_NET, IMPORTED_DATE, CLASS_CODE, PERS_FLG, EMP_ID) values (:cno1, :creda , :clda, :cmp ,:carno, :emenam, :empanam, :copdc , :diena , :prno, :pranm , :grpno, :grpenm , :serco, :serenm, :subserv, :clseamo,:clded, :seramapr, :clamopayed, :cldeduct,  :clmnet, :datnow, :cls, :pres, :empid)", con);

                foreach (DataRow r in dt.Rows)
                {

                    cmd.Parameters.Clear();

                    if (r["رقم المطالبة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":cno1", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":cno1", OracleType.Number).Value = Convert.ToInt64(r["رقم المطالبة"]);
                    // cmd.Parameters.Add(":ye", OracleType.Number).Value = int.Parse(r[1].ToString());

                    if (r["تاريخ التسجيل"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":creda", OracleType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":creda", OracleType.DateTime).Value = Convert.ToDateTime(r["تاريخ التسجيل"]).Date;

                    if (r["تاريخ الخدمة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clda", OracleType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clda", OracleType.DateTime).Value = Convert.ToDateTime(r["تاريخ الخدمة"]).Date;

                    // cmd.Parameters.Add(":coid", OracleType.Number).Value = Convert.ToInt32(r[4]);
                    // cmd.Parameters.Add(":conno", OracleType.Number).Value = int.Parse(r[5].ToString());

                    if (r["كود الشركة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":cmp", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":cmp", OracleType.Number).Value = Convert.ToInt64(r["كود الشركة"]);




                    cmd.Parameters.Add(":carno", OracleType.VarChar).Value = r["الرقم الطبي"].ToString();
                    cmd.Parameters.Add(":emenam", OracleType.VarChar).Value = r["اسم الموظف إنجليزي"].ToString();
                    cmd.Parameters.Add(":empanam", OracleType.VarChar).Value = r["اسم الموظف عربي"].ToString();

                    if (r["CC"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":copdc", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":copdc", OracleType.Number).Value = Convert.ToInt32(r["CC"]);

                    //  cmd.Parameters.Add(":dicod", OracleType.Number).Value = int.Parse(r[9].ToString());
                    //    cmd.Parameters.Add(":dinot", OracleType.VarChar).Value = r[10].ToString();
                    //  cmd.Parameters.Add(":diana", OracleType.VarChar).Value = r[11].ToString();
                    cmd.Parameters.Add(":diena", OracleType.VarChar).Value = r["التشخيص"].ToString();

                    if (r["رقم مقدم الخدمة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":prno", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":prno", OracleType.Number).Value = Convert.ToInt32(r["رقم مقدم الخدمة"]);


                    cmd.Parameters.Add(":pranm", OracleType.VarChar).Value = r["اسم مقدم الخدمة"].ToString();
                    // cmd.Parameters.Add(":prenm", OracleType.VarChar).Value = r[15].ToString();
                    if (r["كود الجروب"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":grpno", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":grpno", OracleType.Number).Value = int.Parse(r["كود الجروب"].ToString());

                    cmd.Parameters.Add(":grpenm", OracleType.VarChar).Value = r["اسم الجروب"].ToString();

                    if (r["كود الخدمة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":serco", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":serco", OracleType.Number).Value = int.Parse(r["كود الخدمة"].ToString());

                    cmd.Parameters.Add(":serenm", OracleType.VarChar).Value = r["اسم الخدمة"].ToString();

                    if (r["قيمة الخدمة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":subserv", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":subserv", OracleType.Number).Value = double.Parse(r["قيمة الخدمة"].ToString());

                    if (r["الاجمالى"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clseamo", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clseamo", OracleType.Number).Value = double.Parse(r["الاجمالى"].ToString());

                    if (r["بعد الخصم"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clded", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clded", OracleType.Number).Value = double.Parse(r["بعد الخصم"].ToString());

                    if (r["بعد التحمل"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":seramapr", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":seramapr", OracleType.Number).Value = double.Parse(r["بعد التحمل"].ToString());

                    if (r["الاستقطاعات"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clamopayed", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clamopayed", OracleType.Number).Value = double.Parse(r["الاستقطاعات"].ToString());

                    if (r["الخصم"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":cldeduct", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":cldeduct", OracleType.Number).Value = double.Parse(r["الخصم"].ToString());

                    if (r["الصافي"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":clmnet", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":clmnet", OracleType.Number).Value = double.Parse(r["الصافي"].ToString());

                    if (r["الفئة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":cls", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":cls", OracleType.Number).Value = Int32.Parse(r["الفئة"].ToString());

                    if (r["درجة القرابة"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":pres", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":pres", OracleType.Number).Value = Int32.Parse(r["درجة القرابة"].ToString());

                    if (r["كود الموظف"].Equals(DBNull.Value))
                        cmd.Parameters.Add(":empid", OracleType.Number).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(":empid", OracleType.Number).Value = Int32.Parse(r["كود الموظف"].ToString());


                    cmd.Parameters.Add(":datnow", OracleType.DateTime).Value = DateTime.Now.Date;

                    cmd.ExecuteNonQuery();

                }


                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                MessageBox.Show("تم الحفظ بنجاح");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        public void CalculateBill(Int32 bcomp, Int32 nobill, string typbill, DateTime dat1, DateTime dat2, Int32 adno, double adtotl, Int32 delno, double deltotl, Int32 chngno, double chngtotl, Int32 recolno, double recoltotl, Int32 taxno, double taxtotl, Int32 adfesno, double adfestotl, Int32 isfesno, double isfestotl, Int32 cntbil, double valbil)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@" INSERT INTO CALCULATEBILLS (BILL_NO , COMP_ID, BILL_TYPE, START_DATE, END_DATE, ADDITION_NO, ADDITION_TOTAL, DELETION_NO, DELETION_TOTAL, 
                                                                       CHANGES_NO, CHANGES_TOTAL, RECOLLECTION_NO, RECOLLECTION_TOTAL, TAXES_NO, TAXES_TOTAL, ADMINFEES_NO, ADMINFEES_TOTAL,
                                                                       ISSUEDFEES_NO, ISSUEDFEES_TOTAL, COUNT_BILL, VALUE_BILL, CREATED_BY, CREATED_DATE)
                                                                VALUES (:bcomp, :nobill, :typbill, :dat1, :dat2, :adno, :adtotl, :delno, :deltotl, :chngno, :chngtotl, :recolno, :recoltotl,
                                                                        :taxno, :taxtotl, :adfesno, :adfestotl, :isfesno, :isfestotl, :cntbil, :valbil, :crby, :datnow)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":bcomp", OracleType.Number).Value = bcomp;
                cmd.Parameters.Add(":nobill", OracleType.Number).Value = nobill;
                cmd.Parameters.Add(":typbill", OracleType.VarChar).Value = typbill;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":adno", OracleType.Number).Value = adno;

                cmd.Parameters.Add(":adtotl", OracleType.Number).Value = adtotl;
                cmd.Parameters.Add(":delno", OracleType.Number).Value = delno;
                cmd.Parameters.Add(":deltotl", OracleType.Number).Value = deltotl;
                cmd.Parameters.Add(":chngno", OracleType.Number).Value = chngno;
                cmd.Parameters.Add(":chngtotl", OracleType.Number).Value = chngtotl;
                cmd.Parameters.Add(":recolno", OracleType.Number).Value = recolno;

                cmd.Parameters.Add(":recoltotl", OracleType.Number).Value = recoltotl;
                cmd.Parameters.Add(":taxno", OracleType.Number).Value = taxno;
                cmd.Parameters.Add(":taxtotl", OracleType.Number).Value = taxtotl;
                cmd.Parameters.Add(":adfesno", OracleType.Number).Value = adfesno;
                cmd.Parameters.Add(":adfestotl", OracleType.Number).Value = adfestotl;
                cmd.Parameters.Add(":isfesno", OracleType.Number).Value = isfesno;

                cmd.Parameters.Add(":isfestotl", OracleType.Number).Value = isfestotl;
                cmd.Parameters.Add(":cntbil", OracleType.Number).Value = cntbil;
                cmd.Parameters.Add(":valbil", OracleType.Number).Value = valbil;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":datnow", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable getclaimdata(string crd, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@" SELECT * FROM(
                                                    select CLAIM_NO1, TO_CHAR(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE, TO_NUMBER(BATCH_NO) BATCH_NO, ' ' GROUP_NO, CLAIM_AMOUNT GROSS, CLAIM_NET AMOUNT, ' ' NOTES, 1 f, TO_DATE(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE1
                                                    FROM IRS_CLAIM_REC_H WHERE card_no = :crd AND claim_date BETWEEN :dat1 AND :dat2 
                                                    union all
                                                    select D_ID, TO_CHAR(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE, BATSH_NO, GROUP_NO, GROSS_AMOUNT GROSS, CLAIM_AMOUNT AMOUNT, NOTES, 2 f, TO_DATE(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE1
                                                    FROM ONLINE_CONS_01 WHERE card_no = :crd AND claim_date BETWEEN :dat1 AND :dat2
                                                    ORDER BY CLAIM_DATE1 DESC) WHERE rownum < 6", con);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);
                DataTable dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch { return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        public void t7seel_byana_update_date(string com_code, string CONTRACT_START, string CONTRACT_END)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            //   UPDATE "APP"."COLLECT_BILL" SET BILL_TYP = 'Exceedig', BILL_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), DELEVER_DATE = TO_DATE('2014-02-10 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), EDITE_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_FROM_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_TO_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), BILL_AMOUNT = '67777345', BILL_PAYMENT = '234', BILL_DIFFERENCE = '456', UPDATED_BY = 'a', UPDATED_DATE = TO_DATE('2011-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS') WHERE ROWID = 'AAAhjiAAHAACgQtAAB' AND ORA_ROWSCN = '34778100'
            //
            try
            {
                con.Open();

                cmd = new OracleCommand(@"UPDATE COLLECT_COMPANY SET  CONTRACT_START_DATE = :CONTRACT_START , CONTRACT_END_DATE = :CONTRACT_END  WHERE COMP_ID='" + com_code + "'", con);

                DateTime dte;
                cmd.Parameters.Clear();

                if (CONTRACT_START == string.Empty)
                    cmd.Parameters.Add(":CONTRACT_START", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":CONTRACT_START", OracleType.DateTime).Value = Convert.ToDateTime(CONTRACT_START);

                if (CONTRACT_END == string.Empty)
                    cmd.Parameters.Add(":CONTRACT_END", OracleType.DateTime).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":CONTRACT_END", OracleType.DateTime).Value = Convert.ToDateTime(CONTRACT_END);


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public DataTable moreapproval(string crd, DateTime dat1, DateTime dat2, int cunt)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"SELECT * FROM (
                                                        select    TO_CHAR(APROV_NO) APPROV_NO, COMP_ID COMP_ID, CARD_NO CARD_NO, PATIENT_NAME NAME,TO_CHAR(DATE_RECIVE,'DD-MM-YYYY') RECIV_DATE,
                                                                  TO_CHAR(DATE_SEND,'DD-MM-YYYY') SEND_DATE, SERV_ENAME SERVECE_TYP, APROV_REPLY  REPLY,
                                                                  APPROV_AMOUNT APPROV_AMOUNT, MED_APP MEDICAL_REPLAY,CREATED_BY CREATED_BY, TO_CHAR(CREATED_DATE,'DD-MM-YYYY') CREATED_DATE , TO_DATE(CREATED_DATE,'DD-MM-YYYY') CREATED_DATE1
                                                        FROM      V_APPROVAL LEFT OUTER JOIN IRS_SUPER_GROUP_NEW ON V_APPROVAL.APROV_TYP = IRS_SUPER_GROUP_NEW.IRS_CODE
                                                        WHERE     CARD_NO = :crd AND TO_DATE(CREATED_DATE) BETWEEN :dat1 AND :dat2-- order by  TO_DATE(created_date,'DD-MM-YYYY') desc
                                                            union all
                                                        select    TO_CHAR(APROV_NO) APPROV_NO, COMP_ID COMP_ID, CARD_NO CARD_NO, PATIENT_NAME NAME,TO_CHAR(DATE_RECIVE,'DD-MM-YYYY') RECIV_DATE,
                                                                                                              TO_CHAR(DATE_SEND,'DD-MM-YYYY') SEND_DATE, SERV_ENAME SERVECE_TYP, APROV_REPLY  REPLY,
                                                                  APPROV_AMOUNT APPROV_AMOUNT, MED_APP MEDICAL_REPLAY,CREATED_BY CREATED_BY, TO_CHAR(CREATED_DATE,'DD-MM-YYYY') CREATED_DATE , TO_DATE(CREATED_DATE,'DD-MM-YYYY') CREATED_DATE1
                                                        FROM      IRS_APPROVAL_HIST LEFT OUTER JOIN IRS_SUPER_GROUP_NEW ON IRS_APPROVAL_HIST.APROV_TYP = IRS_SUPER_GROUP_NEW.IRS_CODE
                                                        WHERE     CARD_NO = :crd AND TO_DATE(CREATED_DATE) BETWEEN :dat1 AND :dat2 --order by  TO_DATE(created_date,'DD-MM-YYYY') desc
                                                            union all
                                                        select    CODE APPROV_NO, COMPANY_ID COMP_ID, CARD_NO CARD_NO, EMP_ENAME NAME, TO_CHAR(RECIV_DATE,'DD-MM-YYYY') RECIV_DATE, TO_CHAR(SEND_DATE,'DD-MM-YYYY') SEND_DATE, 
                                                                  SERVECE_TYP SERVECE_TYP, REPLAY REPLY, VALUE_AFTER APPROV_AMOUNT, MEDICAL_REPLAY MEDICAL_REPLAY, 
                                                                  CREATED_BY CREATED_BY, TO_CHAR(CREATED_DATE,'DD-MM-YYYY') CREATED_DATE, TO_DATE(CREATED_DATE,'DD-MM-YYYY') CREATED_DATE1
                                                        FROM      MEDICAL_APPROVALS 
                                                        WHERE     CARD_NO = :crd AND active = 'Y' AND TO_DATE(CREATED_DATE) BETWEEN :dat1 AND :dat2 order by CREATED_DATE1 desc)  where rownum <= :cunt + 7", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":cunt", OracleType.Number).Value = cunt;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        public DataTable moreclaims(string crd, DateTime dat1, DateTime dat2, int cunt)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"SELECT * FROM(
                                                    select CLAIM_NO1, TO_CHAR(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE, TO_NUMBER(BATCH_NO) BATCH_NO, ' ' GROUP_NO, CLAIM_AMOUNT GROSS, CLAIM_NET AMOUNT, ' ' NOTES, 1 f, TO_DATE(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE1
                                                    FROM IRS_CLAIM_REC_H WHERE card_no = :crd AND claim_date BETWEEN :dat1 AND :dat2 AND PRV_NO != 99999
                                                    union all
                                                    select D_ID, TO_CHAR(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE, BATSH_NO, GROUP_NO, GROSS_AMOUNT GROSS, CLAIM_AMOUNT AMOUNT, NOTES, 2 f, TO_DATE(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE1
                                                    FROM ONLINE_CONS_01 WHERE card_no = :crd AND claim_date BETWEEN :dat1 AND :dat2 AND GROUP_NO != 121
                                                    ORDER BY CLAIM_DATE1 DESC) WHERE rownum <= :cunt + 5", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":cunt", OracleType.Number).Value = cunt;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        public DataTable moreonlinelive(string crd, DateTime dat1, DateTime dat2, int cunt)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"SELECT * FROM(                                                
                                                 select D_ID Claim, TO_CHAR(D_DATE,'DD-MM-YYYY') D_DATE, PR_NAME Provid, PR_BRANCH_NAME Branch, decode(SERV_NAME, 'YES', 'Daily', 'MON', 'Chronic', 'MON_PH', 'Monthly') kind,  
                                                        D_VD Total, CARRY Co_Pay, OVER_INSURANCE  OVER, VALUE_CASH Cash, VALUE_CREDIT CREDIT 
                                                 FROM APP.DMS 
                                                 where card_id = :crd AND TO_DATE(D_DATE) BETWEEN :dat1 AND :dat2 ORDER BY TO_DATE(D_DATE,'DD-MM-YYYY') DESC) WHERE rownum <= :cunt + 5", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":cunt", OracleType.Number).Value = cunt;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }


        public DataTable totalonlinelive2(string crd, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"                                                
                                                 select   NVL(sum (VALUE_CREDIT),0) 
                                                 FROM APP.DMS 
                                                 where card_id = :crd AND TO_DATE(D_DATE) BETWEEN :dat1 AND :dat2  ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        public DataTable moreindemity(string crd, DateTime dat1, DateTime dat2, int cunt)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"SELECT * FROM(                                                
                                                 SELECT CLAIM_NO1, TO_CHAR(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE, CLAIM_AMOUNT, CLAIM_NET, TO_DATE(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE1 
                                                 FROM IRS_CLAIM_REC_H 
                                                 WHERE PRV_NO = 99999 AND CARD_NO = :crd AND TO_DATE(CLAIM_DATE) BETWEEN :dat1 AND :dat2 
                                                 UNION ALL
                                                 SELECT D_ID, TO_CHAR(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE, CLAIM_PAID, CLAIM_AMOUNT , TO_DATE(CLAIM_DATE,'DD-MM-YYYY') CLAIM_DATE1
                                                 FROM ONLINE_CONS_01
                                                 WHERE SERV_CODE = 12101 AND CARD_NO = :crd AND TO_DATE(CLAIM_DATE) BETWEEN :dat1 AND :dat2 ORDER BY CLAIM_DATE1 DESC) WHERE rownum <= :cunt + 5", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":cunt", OracleType.Number).Value = cunt;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }
        public DataTable getcount(string qury, string crd, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(qury, con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        public DataTable getconsmvalue(string qury, Int32 comp, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(qury, con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = comp;
                cmd.Parameters.Add(":startzz", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":endzz", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        public DataTable getconsmxlst(string qury, Int32 comp, string mon1, string mon2, string yer1, string yer2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            try
            {
                cmd = new OracleCommand(qury, con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = comp;
                cmd.Parameters.Add(":mon1", OracleType.VarChar).Value = mon1;
                cmd.Parameters.Add(":mon2", OracleType.VarChar).Value = mon2;
                cmd.Parameters.Add(":yer1", OracleType.VarChar).Value = yer1;
                cmd.Parameters.Add(":yer2", OracleType.VarChar).Value = yer2;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();

                return dd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dd = new DataTable();
                return dd;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }
        }

        public int insertnewclass(Int32 cmp, Int32 cont, DateTime dat1, DateTime dat2, string det, string cls, string perm, string celg, Int32 ag1, string agt1, Int32 ag2, string agt2, Int32 meth, string perc, string val, string clsn, Int32 agcal, string nots)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO dms_test.COMP_CONTRACT_CLASS (COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, START_COVER, END_COVER, AREA_CODE, CLASS_CODE, ANNUAL_PREM, MAX_AMOUNT, COVER_AGE_FROM, AGE_FROM_TYP, COVER_AGE_TO, 
                                                                                    AGE_TO_TYP, OVER_AGE_METHOD, OVER_AGE_PERT, OVER_AGE_AMT, NOTES, OVER_AGE_COND, ONLINE_NOTES, CREATED_BY, CREATED_DATE, ACTIVE)
                                                                             VALUES(1, 1, :cmp, :cont, :dat1, :dat2, :det, :cls, :perm, :celg, :ag1, :agt1, :ag2, :agt2, :meth, :perc, :val, :clsn, :agcal, :nots, :crby, :crdat, 'Y')", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                if (det == string.Empty)
                    cmd.Parameters.Add(":det", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":det", OracleType.Number).Value = Convert.ToInt32(det);

                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;

                if (perm == string.Empty)
                    cmd.Parameters.Add(":perm", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":perm", OracleType.Number).Value = Convert.ToInt64(perm);

                if (celg == string.Empty)
                    cmd.Parameters.Add(":celg", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":celg", OracleType.Number).Value = Convert.ToInt64(celg);

                cmd.Parameters.Add(":ag1", OracleType.Number).Value = ag1;
                cmd.Parameters.Add(":agt1", OracleType.VarChar).Value = agt1;
                cmd.Parameters.Add(":ag2", OracleType.Number).Value = ag2;
                cmd.Parameters.Add(":agt2", OracleType.VarChar).Value = agt2;

                cmd.Parameters.Add(":meth", OracleType.Number).Value = meth;

                if (perc == string.Empty)
                    cmd.Parameters.Add(":perc", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":perc", OracleType.Number).Value = Convert.ToInt32(perc);

                if (val == string.Empty)
                    cmd.Parameters.Add(":val", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":val", OracleType.Number).Value = Convert.ToInt32(val);

                cmd.Parameters.Add(":clsn", OracleType.VarChar).Value = clsn;
                cmd.Parameters.Add(":agcal", OracleType.Number).Value = agcal;

                cmd.Parameters.Add(":nots", OracleType.VarChar).Value = nots;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return 0; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public int updatenewclass(Int32 cmp, Int32 cont, string det, string cls, string perm, string celg, Int32 ag1, string agt1, Int32 ag2, string agt2, Int32 meth, string perc, string val, string clsn, Int32 agcal, string nots)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE dms_test.COMP_CONTRACT_CLASS SET  AREA_CODE = :det, ANNUAL_PREM = :perm, MAX_AMOUNT = :celg, COVER_AGE_FROM = :ag1, AGE_FROM_TYP = :agt1, COVER_AGE_TO = :ag2, 
                                                                                   AGE_TO_TYP = :agt2, OVER_AGE_METHOD = :meth, OVER_AGE_PERT = :perc, OVER_AGE_AMT = :val, NOTES = :clsn, OVER_AGE_COND = :agcal, ONLINE_NOTES = :nots, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                //cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                //cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                if (det == string.Empty)
                    cmd.Parameters.Add(":det", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":det", OracleType.Number).Value = Convert.ToInt32(det);

                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;

                if (perm == string.Empty)
                    cmd.Parameters.Add(":perm", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":perm", OracleType.Number).Value = Convert.ToInt64(perm);

                if (celg == string.Empty)
                    cmd.Parameters.Add(":celg", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":celg", OracleType.Number).Value = Convert.ToInt64(celg);

                cmd.Parameters.Add(":ag1", OracleType.Number).Value = ag1;
                cmd.Parameters.Add(":agt1", OracleType.VarChar).Value = agt1;
                cmd.Parameters.Add(":ag2", OracleType.Number).Value = ag2;
                cmd.Parameters.Add(":agt2", OracleType.VarChar).Value = agt2;

                cmd.Parameters.Add(":meth", OracleType.Number).Value = meth;

                if (perc == string.Empty)
                    cmd.Parameters.Add(":perc", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":perc", OracleType.Number).Value = Convert.ToInt32(perc);

                if (val == string.Empty)
                    cmd.Parameters.Add(":val", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":val", OracleType.Number).Value = Convert.ToInt32(val);

                cmd.Parameters.Add(":clsn", OracleType.VarChar).Value = clsn;
                cmd.Parameters.Add(":agcal", OracleType.Number).Value = agcal;

                cmd.Parameters.Add(":nots", OracleType.VarChar).Value = nots;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return 0; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void insertbenefit(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO dms_test.COMP_CUSTOMIZED_D (COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, CLASS_CODE, SERV_CODE, D_SERV_CODE, CEILING_PERT, CEILING_AMT, CARR_AMT, REFUND_FLAG, IND_LIST_PRICE, REQ_APPRPV, REQ_LETTER, REPEAT_NO, REPEAT_TYP, REPEAT_PERIOD, DOC_EXP, MAT_COV_TYP, DOC_EXP_VAL_TYP, NOTES, ACTIVE, CREATED_BY, CREATED_DATE) 
                                          VALUES (1,1, :cmp, :cont, :cls, :srv,:dsrv, :cprt, :mamt, :camt, :reff, :lstp, :aprv, :letr, :rptn, :rptt, :rptp, :ovra, :covr, :prx, :nts, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;


                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;

                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void insertbenefitdd(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string pol, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO dms_test.COMP_CUSTOMIZED_D_D (COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, CLASS_CODE, SERV_CODE, D_SERV_CODE, SER_SERV, CEILING_PERT, CEILING_AMT, CARR_AMT, REFUND_FLAG, IND_LIST_PRICE, REQ_APPRPV, REQ_LETTER, POLL_FLAG, REPEAT_NO, REPEAT_TYP, REPEAT_PERIOD, DOC_EXP, MAT_COV_TYP, DOC_EXP_VAL_TYP, NOTES, ACTIVE, CREATED_BY, CREATED_DATE) 
                                             VALUES (1,1,:cmp,:cont,:cls,:srv,:dsrv, :ssrv, :cprt, :mamt, :camt, :reff, :lstp, :aprv, :letr, :pol ,:rptn, :rptt, :rptp, :ovra, :covr, :prx, :nts, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;
                cmd.Parameters.Add(":pol", OracleType.VarChar).Value = pol;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;
                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void insertbenefitmed(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string damt, string dano, string moamt, string mono, string lbno, string rano, string lbnomo, string ranomo, string dmedamtmon, string dnoromon, string mmedamtmon, string mnoromon, string dmedamtyer, string dnoroyer, string mmedamtyer, string mnoroyer, string vist, string vistmon, string seion, string seionmon)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO APP.COMP_CUSTOMIZED_D_D_MED (COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, CLASS_CODE, SERV_CODE, D_SERV_CODE, SER_SERV, DAY_AMT, DAY_NO, MON_AMT, MON_NO, LAB_NO, RAY_NO, LAB_NO_MON, RAY_NO_MON, DAY_MED_AMT_MON, DAY_NO_ROSHTA_MON, MON_MED_AMT_MON, MON_NO_ROSHTA_MON, DAY_MED_AMT_YEAR, DAY_NO_ROSHTA_YEAR, MON_MED_AMT_YEAR, MON_NO_ROSHTA_YEAR, VISIT_NO, VISIT_NO_MON, SESSION_NO, SESSION_NO_MON, ACTIVE, CREATED_BY, CREATED_DATE) 
                                            VALUES (1,1,:cmp,:cont,:cls,:srv,:dsrv, :ssrv, :damt, :dano, :moamt, :mono, :lbno, :rano, :lbnomo, :ranomo ,:dmedamtmon, :dnoromon, :mmedamtmon, :mnoromon, :dmedamtyer, :dnoroyer, :mmedamtyer, :mnoroyer, :vist, :vistmon, :seion, :seionmon, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;

                if (damt == string.Empty)
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = Convert.ToInt64(damt);

                if (dano == string.Empty)
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = Convert.ToInt64(dano);

                if (moamt == string.Empty)
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = Convert.ToInt64(moamt);

                if (mono == string.Empty)
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = Convert.ToInt64(mono);

                if (lbno == string.Empty)
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = Convert.ToInt64(lbno);
                if (rano == string.Empty)
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = Convert.ToInt64(rano);

                if (lbnomo == string.Empty)
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = Convert.ToInt64(lbnomo);

                if (ranomo == string.Empty)
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = Convert.ToInt64(ranomo);

                if (dmedamtmon == string.Empty)
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = Convert.ToInt64(dmedamtmon);

                if (dnoromon == string.Empty)
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = Convert.ToInt64(dnoromon);

                if (mmedamtmon == string.Empty)
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = Convert.ToInt64(mmedamtmon);

                if (mnoromon == string.Empty)
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = Convert.ToInt64(mnoromon);

                if (dmedamtyer == string.Empty)
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = Convert.ToInt64(dmedamtyer);

                if (dnoroyer == string.Empty)
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = Convert.ToInt64(dnoroyer);

                if (mmedamtyer == string.Empty)
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = Convert.ToInt64(mmedamtyer);

                if (mnoroyer == string.Empty)
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = Convert.ToInt64(mnoroyer);

                if (vist == string.Empty)
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = Convert.ToInt64(vist);

                if (vistmon == string.Empty)
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = Convert.ToInt64(vistmon);

                if (seion == string.Empty)
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = Convert.ToInt64(seion);

                if (seionmon == string.Empty)
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = Convert.ToInt64(seionmon);



                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updatebenefit(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE dms_test.COMP_CUSTOMIZED_D SET CEILING_PERT = :cprt, CEILING_AMT = :mamt, CARR_AMT = :camt, REFUND_FLAG = :reff, IND_LIST_PRICE = :lstp, REQ_APPRPV = :aprv, REQ_LETTER = :letr, REPEAT_NO = :rptn, REPEAT_TYP = :rptt, REPEAT_PERIOD = :rptp, DOC_EXP = :ovra, MAT_COV_TYP = :covr, DOC_EXP_VAL_TYP = :prx, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                             WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;


                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;

                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updatebenefitdd(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string pol, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE dms_test.COMP_CUSTOMIZED_D_D SET CEILING_PERT = :cprt, CEILING_AMT = :mamt, CARR_AMT = :camt, REFUND_FLAG = :reff, IND_LIST_PRICE = :lstp, REQ_APPRPV = :aprv, REQ_LETTER = :letr, POLL_FLAG = :pol, REPEAT_NO = :rptn, REPEAT_TYP = :rptt, REPEAT_PERIOD = :rptp, DOC_EXP = :ovra, MAT_COV_TYP = :covr, DOC_EXP_VAL_TYP = :prx, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND SER_SERV = :ssrv", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;
                cmd.Parameters.Add(":pol", OracleType.VarChar).Value = pol;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;
                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updatebenefitdd222(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string pol, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE dms_test.COMP_CUSTOMIZED_D_D SET CEILING_PERT = :cprt, CEILING_AMT = :mamt, CARR_AMT = :camt, REFUND_FLAG = :reff, IND_LIST_PRICE = :lstp, REQ_APPRPV = :aprv, REQ_LETTER = :letr, POLL_FLAG = :pol, REPEAT_NO = :rptn, REPEAT_TYP = :rptt, REPEAT_PERIOD = :rptp, DOC_EXP = :ovra, MAT_COV_TYP = :covr, DOC_EXP_VAL_TYP = :prx, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                // cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;
                cmd.Parameters.Add(":pol", OracleType.VarChar).Value = pol;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;
                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void updatebenefitmed(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string damt, string dano, string moamt, string mono, string lbno, string rano, string lbnomo, string ranomo, string dmedamtmon, string dnoromon, string mmedamtmon, string mnoromon, string dmedamtyer, string dnoroyer, string mmedamtyer, string mnoroyer, string vist, string vistmon, string seion, string seionmon)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE APP.COMP_CUSTOMIZED_D_D_MED SET DAY_AMT = :damt, DAY_NO = :dano, MON_AMT = :moamt, MON_NO = :mono, LAB_NO = :lbno, RAY_NO = :rano, LAB_NO_MON = :lbnomo, RAY_NO_MON = :ranomo, DAY_MED_AMT_MON = :dmedamtmon, DAY_NO_ROSHTA_MON = :dnoromon, MON_MED_AMT_MON = :mmedamtmon, MON_NO_ROSHTA_MON = :mnoromon, DAY_MED_AMT_YEAR = :dmedamtyer, DAY_NO_ROSHTA_YEAR = :dnoroyer, MON_MED_AMT_YEAR = :mmedamtyer, MON_NO_ROSHTA_YEAR = :mnoroyer, CREATED_BY = :crby, CREATED_DATE = :crdat, VISIT_NO = :vist, VISIT_NO_MON = :vistmon, SESSION_NO = :seion, SESSION_NO_MON = :seionmon 
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND SER_SERV = :ssrv", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;

                if (damt == string.Empty)
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = Convert.ToInt64(damt);

                if (dano == string.Empty)
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = Convert.ToInt64(dano);

                if (moamt == string.Empty)
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = Convert.ToInt64(moamt);

                if (mono == string.Empty)
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = Convert.ToInt64(mono);

                if (lbno == string.Empty)
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = Convert.ToInt64(lbno);
                if (rano == string.Empty)
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = Convert.ToInt64(rano);

                if (lbnomo == string.Empty)
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = Convert.ToInt64(lbnomo);

                if (ranomo == string.Empty)
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = Convert.ToInt64(ranomo);

                if (dmedamtmon == string.Empty)
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = Convert.ToInt64(dmedamtmon);

                if (dnoromon == string.Empty)
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = Convert.ToInt64(dnoromon);

                if (mmedamtmon == string.Empty)
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = Convert.ToInt64(mmedamtmon);

                if (mnoromon == string.Empty)
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = Convert.ToInt64(mnoromon);

                if (dmedamtyer == string.Empty)
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = Convert.ToInt64(dmedamtyer);

                if (dnoroyer == string.Empty)
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = Convert.ToInt64(dnoroyer);

                if (mmedamtyer == string.Empty)
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = Convert.ToInt64(mmedamtyer);

                if (mnoroyer == string.Empty)
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = Convert.ToInt64(mnoroyer);

                if (vist == string.Empty)
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = Convert.ToInt64(vist);

                if (vistmon == string.Empty)
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = Convert.ToInt64(vistmon);

                if (seion == string.Empty)
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = Convert.ToInt64(seion);

                if (seionmon == string.Empty)
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = Convert.ToInt64(seionmon);

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updatebenefitmed222(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string damt, string dano, string moamt, string mono, string lbno, string rano, string lbnomo, string ranomo, string dmedamtmon, string dnoromon, string mmedamtmon, string mnoromon, string dmedamtyer, string dnoroyer, string mmedamtyer, string mnoroyer, string vist, string vistmon, string seion, string seionmon)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE APP.COMP_CUSTOMIZED_D_D_MED SET DAY_AMT = :damt, DAY_NO = :dano, MON_AMT = :moamt, MON_NO = :mono, LAB_NO = :lbno, RAY_NO = :rano, LAB_NO_MON = :lbnomo, RAY_NO_MON = :ranomo, DAY_MED_AMT_MON = :dmedamtmon, DAY_NO_ROSHTA_MON = :dnoromon, MON_MED_AMT_MON = :mmedamtmon, MON_NO_ROSHTA_MON = :mnoromon, DAY_MED_AMT_YEAR = :dmedamtyer, DAY_NO_ROSHTA_YEAR = :dnoroyer, MON_MED_AMT_YEAR = :mmedamtyer, MON_NO_ROSHTA_YEAR = :mnoroyer, CREATED_BY = :crby, CREATED_DATE = :crdat, VISIT_NO = :vist, VISIT_NO_MON = :vistmon, SESSION_NO = :seion, SESSION_NO_MON = :seionmon 
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                // cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;

                if (damt == string.Empty)
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = Convert.ToInt64(damt);

                if (dano == string.Empty)
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = Convert.ToInt64(dano);

                if (moamt == string.Empty)
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = Convert.ToInt64(moamt);

                if (mono == string.Empty)
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = Convert.ToInt64(mono);

                if (lbno == string.Empty)
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = Convert.ToInt64(lbno);
                if (rano == string.Empty)
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = Convert.ToInt64(rano);

                if (lbnomo == string.Empty)
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = Convert.ToInt64(lbnomo);

                if (ranomo == string.Empty)
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = Convert.ToInt64(ranomo);

                if (dmedamtmon == string.Empty)
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = Convert.ToInt64(dmedamtmon);

                if (dnoromon == string.Empty)
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = Convert.ToInt64(dnoromon);

                if (mmedamtmon == string.Empty)
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = Convert.ToInt64(mmedamtmon);

                if (mnoromon == string.Empty)
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = Convert.ToInt64(mnoromon);

                if (dmedamtyer == string.Empty)
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = Convert.ToInt64(dmedamtyer);

                if (dnoroyer == string.Empty)
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = Convert.ToInt64(dnoroyer);

                if (mmedamtyer == string.Empty)
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = Convert.ToInt64(mmedamtyer);

                if (mnoroyer == string.Empty)
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = Convert.ToInt64(mnoroyer);

                if (vist == string.Empty)
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = Convert.ToInt64(vist);

                if (vistmon == string.Empty)
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = Convert.ToInt64(vistmon);

                if (seion == string.Empty)
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = Convert.ToInt64(seion);

                if (seionmon == string.Empty)
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = Convert.ToInt64(seionmon);

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void insertbenefitemp(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO dms_test.COMP_CUSTOMIZED_D_EMP (CARD_ID, COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, CLASS_CODE, SERV_CODE, D_SERV_CODE, CEILING_PERT, CEILING_AMT, CARR_AMT, REFUND_FLAG, IND_LIST_PRICE, REQ_APPRPV, REQ_LETTER, REPEAT_NO, REPEAT_TYP, REPEAT_PERIOD, DOC_EXP, MAT_COV_TYP, DOC_EXP_VAL_TYP, NOTES, ACTIVE, CREATED_BY, CREATED_DATE) 
                                          VALUES (:crd, 1,1, :cmp, :cont, :cls, :srv,:dsrv, :cprt, :mamt, :camt, :reff, :lstp, :aprv, :letr, :rptn, :rptt, :rptp, :ovra, :covr, :prx, :nts, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;

                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void insertbenefitddemp(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string pol, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO dms_test.COMP_CUSTOMIZED_D_D_EMP (CARD_ID, COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, CLASS_CODE, SERV_CODE, D_SERV_CODE, SER_SERV, CEILING_PERT, CEILING_AMT, CARR_AMT, REFUND_FLAG, IND_LIST_PRICE, REQ_APPRPV, REQ_LETTER, POLL_FLAG, REPEAT_NO, REPEAT_TYP, REPEAT_PERIOD, DOC_EXP, MAT_COV_TYP, DOC_EXP_VAL_TYP, NOTES, ACTIVE, CREATED_BY, CREATED_DATE) 
                                             VALUES (:crd, 1,1,:cmp,:cont,:cls,:srv,:dsrv, :ssrv, :cprt, :mamt, :camt, :reff, :lstp, :aprv, :letr, :pol ,:rptn, :rptt, :rptp, :ovra, :covr, :prx, :nts, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;
                cmd.Parameters.Add(":pol", OracleType.VarChar).Value = pol;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;
                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void insertbenefitmedemp(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string damt, string dano, string moamt, string mono, string lbno, string rano, string lbnomo, string ranomo, string dmedamtmon, string dnoromon, string mmedamtmon, string mnoromon, string dmedamtyer, string dnoroyer, string mmedamtyer, string mnoroyer, string crd, string vist, string vistmon, string seion, string seionmon)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO APP.COMP_CUSTOMIZED_D_D_MED_EMP (CARD_ID, COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, CLASS_CODE, SERV_CODE, D_SERV_CODE, SER_SERV, DAY_AMT, DAY_NO, MON_AMT, MON_NO, LAB_NO, RAY_NO, LAB_NO_MON, RAY_NO_MON, DAY_MED_AMT_MON, DAY_NO_ROSHTA_MON, MON_MED_AMT_MON, MON_NO_ROSHTA_MON, DAY_MED_AMT_YEAR, DAY_NO_ROSHTA_YEAR, MON_MED_AMT_YEAR, MON_NO_ROSHTA_YEAR, VISIT_NO, VISIT_NO_MON, SESSION_NO, SESSION_NO_MON, ACTIVE, CREATED_BY, CREATED_DATE) 
                                            VALUES (:crd, 1,1,:cmp,:cont,:cls,:srv,:dsrv, :ssrv, :damt, :dano, :moamt, :mono, :lbno, :rano, :lbnomo, :ranomo ,:dmedamtmon, :dnoromon, :mmedamtmon, :mnoromon, :dmedamtyer, :dnoroyer, :mmedamtyer, :mnoroyer, :vist, :vistmon, :seion, :seionmon, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (damt == string.Empty)
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = Convert.ToInt64(damt);

                if (dano == string.Empty)
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = Convert.ToInt64(dano);

                if (moamt == string.Empty)
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = Convert.ToInt64(moamt);

                if (mono == string.Empty)
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = Convert.ToInt64(mono);

                if (lbno == string.Empty)
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = Convert.ToInt64(lbno);
                if (rano == string.Empty)
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = Convert.ToInt64(rano);

                if (lbnomo == string.Empty)
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = Convert.ToInt64(lbnomo);

                if (ranomo == string.Empty)
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = Convert.ToInt64(ranomo);

                if (dmedamtmon == string.Empty)
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = Convert.ToInt64(dmedamtmon);

                if (dnoromon == string.Empty)
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = Convert.ToInt64(dnoromon);

                if (mmedamtmon == string.Empty)
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = Convert.ToInt64(mmedamtmon);

                if (mnoromon == string.Empty)
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = Convert.ToInt64(mnoromon);

                if (dmedamtyer == string.Empty)
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = Convert.ToInt64(dmedamtyer);

                if (dnoroyer == string.Empty)
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = Convert.ToInt64(dnoroyer);

                if (mmedamtyer == string.Empty)
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = Convert.ToInt64(mmedamtyer);

                if (mnoroyer == string.Empty)
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = Convert.ToInt64(mnoroyer);

                if (vist == string.Empty)
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = Convert.ToInt64(vist);

                if (vistmon == string.Empty)
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = Convert.ToInt64(vistmon);

                if (seion == string.Empty)
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = Convert.ToInt64(seion);

                if (seionmon == string.Empty)
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = Convert.ToInt64(seionmon);

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void updatebenefitemp(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE dms_test.COMP_CUSTOMIZED_D_EMP SET CEILING_PERT = :cprt, CEILING_AMT = :mamt, CARR_AMT = :camt, REFUND_FLAG = :reff, IND_LIST_PRICE = :lstp, REQ_APPRPV = :aprv, REQ_LETTER = :letr, REPEAT_NO = :rptn, REPEAT_TYP = :rptt, REPEAT_PERIOD = :rptp, DOC_EXP = :ovra, MAT_COV_TYP = :covr, DOC_EXP_VAL_TYP = :prx, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                             WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND CARD_ID = :crd", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;


                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;

                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updatebenefitddemp(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string pol, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE dms_test.COMP_CUSTOMIZED_D_D_EMP SET CEILING_PERT = :cprt, CEILING_AMT = :mamt, CARR_AMT = :camt, REFUND_FLAG = :reff, IND_LIST_PRICE = :lstp, REQ_APPRPV = :aprv, REQ_LETTER = :letr, POLL_FLAG = :pol, REPEAT_NO = :rptn, REPEAT_TYP = :rptt, REPEAT_PERIOD = :rptp, DOC_EXP = :ovra, MAT_COV_TYP = :covr, DOC_EXP_VAL_TYP = :prx, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND SER_SERV = :ssrv AND CARD_ID = :crd", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;
                cmd.Parameters.Add(":pol", OracleType.VarChar).Value = pol;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;
                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updatebenefitddemp222(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string pol, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE dms_test.COMP_CUSTOMIZED_D_D_EMP SET CEILING_PERT = :cprt, CEILING_AMT = :mamt, CARR_AMT = :camt, REFUND_FLAG = :reff, IND_LIST_PRICE = :lstp, REQ_APPRPV = :aprv, REQ_LETTER = :letr, POLL_FLAG = :pol, REPEAT_NO = :rptn, REPEAT_TYP = :rptt, REPEAT_PERIOD = :rptp, DOC_EXP = :ovra, MAT_COV_TYP = :covr, DOC_EXP_VAL_TYP = :prx, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND CARD_ID = :crd", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;
                cmd.Parameters.Add(":pol", OracleType.VarChar).Value = pol;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;
                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void updatebenefitmedemp(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string damt, string dano, string moamt, string mono, string lbno, string rano, string lbnomo, string ranomo, string dmedamtmon, string dnoromon, string mmedamtmon, string mnoromon, string dmedamtyer, string dnoroyer, string mmedamtyer, string mnoroyer, string crd, string vist, string vistmon, string seion, string seionmon)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE APP.COMP_CUSTOMIZED_D_D_MED_EMP SET DAY_AMT = :damt, DAY_NO = :dano, MON_AMT = :moamt, MON_NO = :mono, LAB_NO = :lbno, RAY_NO = :rano, LAB_NO_MON = :lbnomo, RAY_NO_MON = :ranomo, DAY_MED_AMT_MON = :dmedamtmon, DAY_NO_ROSHTA_MON = :dnoromon, MON_MED_AMT_MON = :mmedamtmon, MON_NO_ROSHTA_MON = :mnoromon, DAY_MED_AMT_YEAR = :dmedamtyer, DAY_NO_ROSHTA_YEAR = :dnoroyer, MON_MED_AMT_YEAR = :mmedamtyer, MON_NO_ROSHTA_YEAR = :mnoroyer, CREATED_BY = :crby, CREATED_DATE = :crdat, VISIT_NO = :vist, VISIT_NO_MON = :vistmon, SESSION_NO = :seion, SESSION_NO_MON = :seionmon 
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND SER_SERV = :ssrv AND CARD_ID = :crd", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (damt == string.Empty)
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = Convert.ToInt64(damt);

                if (dano == string.Empty)
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = Convert.ToInt64(dano);

                if (moamt == string.Empty)
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = Convert.ToInt64(moamt);

                if (mono == string.Empty)
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = Convert.ToInt64(mono);

                if (lbno == string.Empty)
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = Convert.ToInt64(lbno);
                if (rano == string.Empty)
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = Convert.ToInt64(rano);

                if (lbnomo == string.Empty)
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = Convert.ToInt64(lbnomo);

                if (ranomo == string.Empty)
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = Convert.ToInt64(ranomo);

                if (dmedamtmon == string.Empty)
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = Convert.ToInt64(dmedamtmon);

                if (dnoromon == string.Empty)
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = Convert.ToInt64(dnoromon);

                if (mmedamtmon == string.Empty)
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = Convert.ToInt64(mmedamtmon);

                if (mnoromon == string.Empty)
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = Convert.ToInt64(mnoromon);

                if (dmedamtyer == string.Empty)
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = Convert.ToInt64(dmedamtyer);

                if (dnoroyer == string.Empty)
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = Convert.ToInt64(dnoroyer);

                if (mmedamtyer == string.Empty)
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = Convert.ToInt64(mmedamtyer);

                if (mnoroyer == string.Empty)
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = Convert.ToInt64(mnoroyer);

                if (vist == string.Empty)
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = Convert.ToInt64(vist);

                if (vistmon == string.Empty)
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = Convert.ToInt64(vistmon);

                if (seion == string.Empty)
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = Convert.ToInt64(seion);

                if (seionmon == string.Empty)
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = Convert.ToInt64(seionmon);

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void insertbenefitcc(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO APP.COMP_CUSTOMIZED_D_COST_CENTER (COST_CODE, COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, CLASS_CODE, SERV_CODE, D_SERV_CODE, CEILING_PERT, CEILING_AMT, CARR_AMT, REFUND_FLAG, IND_LIST_PRICE, REQ_APPRPV, REQ_LETTER, REPEAT_NO, REPEAT_TYP, REPEAT_PERIOD, DOC_EXP, MAT_COV_TYP, DOC_EXP_VAL_TYP, NOTES, ACTIVE, CREATED_BY, CREATED_DATE) 
                                          VALUES (:crd, 1,1, :cmp, :cont, :cls, :srv,:dsrv, :cprt, :mamt, :camt, :reff, :lstp, :aprv, :letr, :rptn, :rptt, :rptp, :ovra, :covr, :prx, :nts, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;

                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void insertbenefitddcc(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string pol, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO APP.COMP_CUSTOMIZED_D_D_COST_CNTR (COST_CODE, COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, CLASS_CODE, SERV_CODE, D_SERV_CODE, SER_SERV, CEILING_PERT, CEILING_AMT, CARR_AMT, REFUND_FLAG, IND_LIST_PRICE, REQ_APPRPV, REQ_LETTER, POLL_FLAG, REPEAT_NO, REPEAT_TYP, REPEAT_PERIOD, DOC_EXP, MAT_COV_TYP, DOC_EXP_VAL_TYP, NOTES, ACTIVE, CREATED_BY, CREATED_DATE) 
                                             VALUES (:crd, 1,1,:cmp,:cont,:cls,:srv,:dsrv, :ssrv, :cprt, :mamt, :camt, :reff, :lstp, :aprv, :letr, :pol ,:rptn, :rptt, :rptp, :ovra, :covr, :prx, :nts, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;
                cmd.Parameters.Add(":pol", OracleType.VarChar).Value = pol;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;
                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void insertbenefitmedcc(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string damt, string dano, string moamt, string mono, string lbno, string rano, string lbnomo, string ranomo, string dmedamtmon, string dnoromon, string mmedamtmon, string mnoromon, string dmedamtyer, string dnoroyer, string mmedamtyer, string mnoroyer, string crd, string vist, string vistmon, string seion, string seionmon)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO APP.COMP_CUSTOMIZED_D_MD_CST_CNTR (COST_CODE, COMP_ID, BRANCH_CODE, C_COMP_ID, CONTRACT_NO, CLASS_CODE, SERV_CODE, D_SERV_CODE, SER_SERV, DAY_AMT, DAY_NO, MON_AMT, MON_NO, LAB_NO, RAY_NO, LAB_NO_MON, RAY_NO_MON, DAY_MED_AMT_MON, DAY_NO_ROSHTA_MON, MON_MED_AMT_MON, MON_NO_ROSHTA_MON, DAY_MED_AMT_YEAR, DAY_NO_ROSHTA_YEAR, MON_MED_AMT_YEAR, MON_NO_ROSHTA_YEAR, VISIT_NO, VISIT_NO_MON, SESSION_NO, SESSION_NO_MON, ACTIVE, CREATED_BY, CREATED_DATE) 
                                            VALUES (:crd, 1,1,:cmp,:cont,:cls,:srv,:dsrv, :ssrv, :damt, :dano, :moamt, :mono, :lbno, :rano, :lbnomo, :ranomo ,:dmedamtmon, :dnoromon, :mmedamtmon, :mnoromon, :dmedamtyer, :dnoroyer, :mmedamtyer, :mnoroyer, :vist, :vistmon, :seion, :seionmon, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (damt == string.Empty)
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = Convert.ToInt64(damt);

                if (dano == string.Empty)
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = Convert.ToInt64(dano);

                if (moamt == string.Empty)
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = Convert.ToInt64(moamt);

                if (mono == string.Empty)
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = Convert.ToInt64(mono);

                if (lbno == string.Empty)
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = Convert.ToInt64(lbno);
                if (rano == string.Empty)
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = Convert.ToInt64(rano);

                if (lbnomo == string.Empty)
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = Convert.ToInt64(lbnomo);

                if (ranomo == string.Empty)
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = Convert.ToInt64(ranomo);

                if (dmedamtmon == string.Empty)
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = Convert.ToInt64(dmedamtmon);

                if (dnoromon == string.Empty)
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = Convert.ToInt64(dnoromon);

                if (mmedamtmon == string.Empty)
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = Convert.ToInt64(mmedamtmon);

                if (mnoromon == string.Empty)
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = Convert.ToInt64(mnoromon);

                if (dmedamtyer == string.Empty)
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = Convert.ToInt64(dmedamtyer);

                if (dnoroyer == string.Empty)
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = Convert.ToInt64(dnoroyer);

                if (mmedamtyer == string.Empty)
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = Convert.ToInt64(mmedamtyer);

                if (mnoroyer == string.Empty)
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = Convert.ToInt64(mnoroyer);

                if (vist == string.Empty)
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = Convert.ToInt64(vist);

                if (vistmon == string.Empty)
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = Convert.ToInt64(vistmon);

                if (seion == string.Empty)
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = Convert.ToInt64(seion);

                if (seionmon == string.Empty)
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = Convert.ToInt64(seionmon);

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void updatebenefitcc(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE APP.COMP_CUSTOMIZED_D_COST_CENTER SET CEILING_PERT = :cprt, CEILING_AMT = :mamt, CARR_AMT = :camt, REFUND_FLAG = :reff, IND_LIST_PRICE = :lstp, REQ_APPRPV = :aprv, REQ_LETTER = :letr, REPEAT_NO = :rptn, REPEAT_TYP = :rptt, REPEAT_PERIOD = :rptp, DOC_EXP = :ovra, MAT_COV_TYP = :covr, DOC_EXP_VAL_TYP = :prx, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                             WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND COST_CODE = :crd", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;


                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;

                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updatebenefitddcc(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string pol, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE APP.COMP_CUSTOMIZED_D_D_COST_CNTR SET CEILING_PERT = :cprt, CEILING_AMT = :mamt, CARR_AMT = :camt, REFUND_FLAG = :reff, IND_LIST_PRICE = :lstp, REQ_APPRPV = :aprv, REQ_LETTER = :letr, POLL_FLAG = :pol, REPEAT_NO = :rptn, REPEAT_TYP = :rptt, REPEAT_PERIOD = :rptp, DOC_EXP = :ovra, MAT_COV_TYP = :covr, DOC_EXP_VAL_TYP = :prx, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND SER_SERV = :ssrv AND COST_CODE = :crd", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;
                cmd.Parameters.Add(":pol", OracleType.VarChar).Value = pol;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;
                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updatebenefitddcc222(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string cprt, string mamt, string camt, string reff, string lstp, string aprv, string letr, string pol, string rptn, string rptt, string rptp, string ovra, string covr, string prx, string nts, string crd)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE APP.COMP_CUSTOMIZED_D_D_COST_CNTR SET CEILING_PERT = :cprt, CEILING_AMT = :mamt, CARR_AMT = :camt, REFUND_FLAG = :reff, IND_LIST_PRICE = :lstp, REQ_APPRPV = :aprv, REQ_LETTER = :letr, POLL_FLAG = :pol, REPEAT_NO = :rptn, REPEAT_TYP = :rptt, REPEAT_PERIOD = :rptp, DOC_EXP = :ovra, MAT_COV_TYP = :covr, DOC_EXP_VAL_TYP = :prx, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND COST_CODE = :crd", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (cprt == string.Empty)
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cprt", OracleType.Number).Value = Convert.ToInt64(cprt);

                if (mamt == string.Empty)
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mamt", OracleType.Number).Value = Convert.ToInt64(mamt);

                if (camt == string.Empty)
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":camt", OracleType.Number).Value = Convert.ToInt64(camt);


                cmd.Parameters.Add(":reff", OracleType.VarChar).Value = reff;

                cmd.Parameters.Add(":lstp", OracleType.VarChar).Value = lstp;
                cmd.Parameters.Add(":aprv", OracleType.VarChar).Value = aprv;
                cmd.Parameters.Add(":letr", OracleType.VarChar).Value = letr;
                cmd.Parameters.Add(":pol", OracleType.VarChar).Value = pol;

                if (rptn == string.Empty)
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptn", OracleType.Number).Value = Convert.ToInt32(rptn);

                cmd.Parameters.Add(":rptt", OracleType.VarChar).Value = rptt;

                if (rptp == string.Empty)
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rptp", OracleType.Number).Value = Convert.ToInt32(rptp);

                cmd.Parameters.Add(":ovra", OracleType.VarChar).Value = ovra;
                cmd.Parameters.Add(":covr", OracleType.VarChar).Value = covr;
                cmd.Parameters.Add(":prx", OracleType.VarChar).Value = prx;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void updatebenefitmedcc(Int32 cmp, Int32 cont, string cls, string srv, string dsrv, string ssrv, string damt, string dano, string moamt, string mono, string lbno, string rano, string lbnomo, string ranomo, string dmedamtmon, string dnoromon, string mmedamtmon, string mnoromon, string dmedamtyer, string dnoroyer, string mmedamtyer, string mnoroyer, string crd, string vist, string vistmon, string seion, string seionmon)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE APP.COMP_CUSTOMIZED_D_MD_CST_CNTR SET DAY_AMT = :damt, DAY_NO = :dano, MON_AMT = :moamt, MON_NO = :mono, LAB_NO = :lbno, RAY_NO = :rano, LAB_NO_MON = :lbnomo, RAY_NO_MON = :ranomo, DAY_MED_AMT_MON = :dmedamtmon, DAY_NO_ROSHTA_MON = :dnoromon, MON_MED_AMT_MON = :mmedamtmon, MON_NO_ROSHTA_MON = :mnoromon, DAY_MED_AMT_YEAR = :dmedamtyer, DAY_NO_ROSHTA_YEAR = :dnoroyer, MON_MED_AMT_YEAR = :mmedamtyer, MON_NO_ROSHTA_YEAR = :mnoroyer, CREATED_BY = :crby, CREATED_DATE = :crdat, VISIT_NO = :vist, VISIT_NO_MON = :vistmon, SESSION_NO = :seion, SESSION_NO_MON = :seionmon  
                                            WHERE  C_COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND SERV_CODE = :srv AND D_SERV_CODE = :dsrv AND SER_SERV = :ssrv AND COST_CODE = :crd", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":srv", OracleType.VarChar).Value = srv;
                cmd.Parameters.Add(":dsrv", OracleType.VarChar).Value = dsrv;
                cmd.Parameters.Add(":ssrv", OracleType.VarChar).Value = ssrv;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (damt == string.Empty)
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":damt", OracleType.Number).Value = Convert.ToInt64(damt);

                if (dano == string.Empty)
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dano", OracleType.Number).Value = Convert.ToInt64(dano);

                if (moamt == string.Empty)
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":moamt", OracleType.Number).Value = Convert.ToInt64(moamt);

                if (mono == string.Empty)
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mono", OracleType.Number).Value = Convert.ToInt64(mono);

                if (lbno == string.Empty)
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbno", OracleType.Number).Value = Convert.ToInt64(lbno);
                if (rano == string.Empty)
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rano", OracleType.Number).Value = Convert.ToInt64(rano);

                if (lbnomo == string.Empty)
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":lbnomo", OracleType.Number).Value = Convert.ToInt64(lbnomo);

                if (ranomo == string.Empty)
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":ranomo", OracleType.Number).Value = Convert.ToInt64(ranomo);

                if (dmedamtmon == string.Empty)
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtmon", OracleType.Number).Value = Convert.ToInt64(dmedamtmon);

                if (dnoromon == string.Empty)
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoromon", OracleType.Number).Value = Convert.ToInt64(dnoromon);

                if (mmedamtmon == string.Empty)
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtmon", OracleType.Number).Value = Convert.ToInt64(mmedamtmon);

                if (mnoromon == string.Empty)
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoromon", OracleType.Number).Value = Convert.ToInt64(mnoromon);

                if (dmedamtyer == string.Empty)
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dmedamtyer", OracleType.Number).Value = Convert.ToInt64(dmedamtyer);

                if (dnoroyer == string.Empty)
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":dnoroyer", OracleType.Number).Value = Convert.ToInt64(dnoroyer);

                if (mmedamtyer == string.Empty)
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mmedamtyer", OracleType.Number).Value = Convert.ToInt64(mmedamtyer);

                if (mnoroyer == string.Empty)
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mnoroyer", OracleType.Number).Value = Convert.ToInt64(mnoroyer);

                if (vist == string.Empty)
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vist", OracleType.Number).Value = Convert.ToInt64(vist);

                if (vistmon == string.Empty)
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":vistmon", OracleType.Number).Value = Convert.ToInt64(vistmon);

                if (seion == string.Empty)
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seion", OracleType.Number).Value = Convert.ToInt64(seion);

                if (seionmon == string.Empty)
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":seionmon", OracleType.Number).Value = Convert.ToInt64(seionmon);

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void insertpolldata(string pol, string cmp, string cont, string cls, string cc, string rel, string fag, string tfag, string tag, string ttag, string crd, string amt, string amtf, string amtt, string pert, string cutemp, string nte, string nemp, string nam, string alcrdpol, string aply)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO APP.POLL_DATA (POLL_CODE, COMP_ID, CONTRACT_NO, CLASS_CODE, COST_CODE, RELATION_CODE, FROM_AGE, FAGE_CODE, TO_AGE, TAGE_CODE, CARD_ID, AMOUNT, AMOUNT_FROM, AMOUNT_TYPE, PERCENT_TYPE, COUNT_EMP, NOTES, ACTIVE, CREATED_BY, CREATED_DATE, NUMBER_EMP, NAME_POOL, SUB_CODE, ALL_CARDS, APPLY_TO)
                                                             VALUES (:pol, :cmp, :cont, :cls, :cc, :rel, :fag, :tfag, :tag, :ttag, :crd, :amt, :amtf, :amtt, :pert, :cutemp, :nte, 'Y', :crby, :crdat, :nemp, :nam, (SELECT (NVL(MAX(SUB_CODE),0)+1) FROM APP.POLL_DATA WHERE POLL_CODE = :pol), :alcrd, :aply)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":pol", OracleType.Number).Value = Convert.ToInt64(pol);
                cmd.Parameters.Add(":cmp", OracleType.Number).Value = Convert.ToInt64(cmp);
                cmd.Parameters.Add(":cont", OracleType.Number).Value = Convert.ToInt64(cont);
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":nam", OracleType.VarChar).Value = nam;

                if (cc == string.Empty)
                    cmd.Parameters.Add(":cc", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cc", OracleType.Number).Value = Convert.ToInt64(cc);

                if (rel == string.Empty)
                    cmd.Parameters.Add(":rel", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rel", OracleType.Number).Value = Convert.ToInt64(rel);

                if (fag == string.Empty)
                    cmd.Parameters.Add(":fag", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":fag", OracleType.Number).Value = Convert.ToInt64(fag);

                if (tfag == string.Empty)
                    cmd.Parameters.Add(":tfag", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":tfag", OracleType.Number).Value = Convert.ToInt64(tfag);

                if (tag == string.Empty)
                    cmd.Parameters.Add(":tag", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":tag", OracleType.Number).Value = Convert.ToInt64(tag);

                if (ttag == string.Empty)
                    cmd.Parameters.Add(":ttag", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":ttag", OracleType.Number).Value = Convert.ToInt64(ttag);


                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;

                if (amt == string.Empty)
                    cmd.Parameters.Add(":amt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":amt", OracleType.Number).Value = Convert.ToInt64(amt);

                if (amtf == string.Empty)
                    cmd.Parameters.Add(":amtf", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":amtf", OracleType.Number).Value = Convert.ToInt64(amtf);

                if (amtt == string.Empty)
                    cmd.Parameters.Add(":amtt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":amtt", OracleType.Number).Value = Convert.ToInt64(amtt);

                if (pert == string.Empty)
                    cmd.Parameters.Add(":pert", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":pert", OracleType.Number).Value = Convert.ToInt64(pert);

                if (cutemp == string.Empty)
                    cmd.Parameters.Add(":cutemp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cutemp", OracleType.Number).Value = Convert.ToInt64(cutemp);

                if (nemp == string.Empty)
                    cmd.Parameters.Add(":nemp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":nemp", OracleType.Number).Value = Convert.ToInt64(nemp);

                cmd.Parameters.Add(":nte", OracleType.VarChar).Value = nte;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add(":alcrd", OracleType.VarChar).Value = alcrdpol;
                cmd.Parameters.Add(":aply", OracleType.Number).Value = Convert.ToInt32(aply);

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable getpoolserv(string pol)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"SELECT DISTINCT 'CHRONIC' NAME FROM POLL_DATA_CHRONIC, POLL_DATA
                                        WHERE POLL_DATA_CHRONIC.POLL_CODE = POLL_DATA.POLL_CODE AND POLL_DATA.POLL_CODE = :pol AND POLL_DATA_CHRONIC.ACTIVE != 'N'
                                        UNION ALL
                                        SELECT DISTINCT 'CRITICAL' NAME FROM POLL_DATA_CRITICAL, POLL_DATA
                                        WHERE POLL_DATA_CRITICAL.POLL_CODE = POLL_DATA.POLL_CODE AND POLL_DATA.POLL_CODE = :pol AND POLL_DATA_CRITICAL.ACTIVE != 'N'
                                        UNION ALL
                                        SELECT DISTINCT 'DIAGNOSIS' NAME FROM POLL_DATA_DIAG, POLL_DATA
                                        WHERE POLL_DATA_DIAG.POLL_CODE = POLL_DATA.POLL_CODE AND POLL_DATA.POLL_CODE = :pol AND POLL_DATA_DIAG.ACTIVE != 'N'
                                        UNION ALL
                                        SELECT DISTINCT 'MEDICATION DATE' NAME FROM POLL_DATA_MED_DATA, POLL_DATA
                                        WHERE POLL_DATA_MED_DATA.POLL_CODE = POLL_DATA.POLL_CODE AND POLL_DATA.POLL_CODE = :pol AND POLL_DATA_MED_DATA.ACTIVE != 'N'
                                        UNION ALL
                                        SELECT DISTINCT 'MEDICATION GROUP' NAME FROM POLL_DATA_MED_GROUP, POLL_DATA
                                        WHERE POLL_DATA_MED_GROUP.POLL_CODE = POLL_DATA.POLL_CODE AND POLL_DATA.POLL_CODE = :pol AND POLL_DATA_MED_GROUP.ACTIVE != 'N'
                                        UNION ALL
                                        SELECT DISTINCT 'PREX' NAME FROM POLL_DATA_PREX, POLL_DATA
                                        WHERE POLL_DATA_PREX.POLL_CODE = POLL_DATA.POLL_CODE AND POLL_DATA.POLL_CODE = :pol AND POLL_DATA_PREX.ACTIVE != 'N'
                                        UNION ALL
                                        SELECT DISTINCT 'SERVICE' NAME FROM POLL_DATA_SERVICE, POLL_DATA
                                        WHERE POLL_DATA_SERVICE.POLL_CODE = POLL_DATA.POLL_CODE AND POLL_DATA.POLL_CODE = :pol AND POLL_DATA_SERVICE.ACTIVE != 'N'", con);

                cmd.Parameters.Clear();



                cmd.Parameters.Add(":pol", OracleType.Number).Value = Convert.ToInt64(pol);


                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return dd; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable getpoolservcomp(string cmp, string cont, string cls)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new OracleCommand(@"SELECT DISTINCT POLL_DATA.POLL_CODE, POLL_DATA.NAME_POOL, decode(AMOUNT_TYPE, 1 , 'Amount', 2, 'Percent', 3, 'Number Emp') Type, decode(PERCENT_TYPE, 1 , 'Premium', 2, 'Consumption', 3, 'Number Emp', 4, 'Max Amount', 5, 'Expectation Consumption') Type_Percent, AMOUNT, NUMBER_EMP FROM POLL_DATA
                                          WHERE POLL_DATA.COMP_ID = :cmp AND POLL_DATA.CONTRACT_NO = :cont AND POLL_DATA.CLASS_CODE = :cls", con);


                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = Convert.ToInt64(cmp);
                cmd.Parameters.Add(":cont", OracleType.Number).Value = Convert.ToInt64(cont);
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return dd; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }

        public void updatepolldata(string pol, string amt, string amtf, string amtt, string pert, string cutemp, string nte, string nemp)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE POLL_DATA SET AMOUNT = :amt, AMOUNT_FROM = :amtf, AMOUNT_TYPE = :amtt, PERCENT_TYPE = :pert, COUNT_EMP = :cutemp, NOTES = :nte, UPDATED_BY = :crby, UPDATED_DATE = :crdat, NUMBER_EMP = :nemp
                                                 WHERE POLL_CODE = :pol", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":pol", OracleType.Number).Value = Convert.ToInt64(pol);

                if (amt == string.Empty)
                    cmd.Parameters.Add(":amt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":amt", OracleType.Number).Value = Convert.ToInt64(amt);

                if (amtf == string.Empty)
                    cmd.Parameters.Add(":amtf", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":amtf", OracleType.Number).Value = Convert.ToInt64(amtf);

                if (amtt == string.Empty)
                    cmd.Parameters.Add(":amtt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":amtt", OracleType.Number).Value = Convert.ToInt64(amtt);

                if (pert == string.Empty)
                    cmd.Parameters.Add(":pert", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":pert", OracleType.Number).Value = Convert.ToInt64(pert);

                if (cutemp == string.Empty)
                    cmd.Parameters.Add(":cutemp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cutemp", OracleType.Number).Value = Convert.ToInt64(cutemp);

                if (nemp == string.Empty)
                    cmd.Parameters.Add(":nemp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":nemp", OracleType.Number).Value = Convert.ToInt64(nemp);

                cmd.Parameters.Add(":nte", OracleType.VarChar).Value = nte;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updatepolldata2(string pol, string amt, string amtf, string amtt, string pert, string cutemp, string nte, string nemp, string alcrdpol, string aply)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE POLL_DATA SET AMOUNT = :amt, AMOUNT_TYPE = :amtt, AMOUNT_FROM = :amtf, PERCENT_TYPE = :pert, COUNT_EMP = :cutemp, NOTES = :nte, UPDATED_BY = :crby, UPDATED_DATE = :crdat, NUMBER_EMP = :nemp, ALL_CARDS = :alcrdpol, APPLY_TO = :aply
                                                 WHERE POLL_CODE = :pol ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":pol", OracleType.Number).Value = Convert.ToInt64(pol);

                if (amt == string.Empty)
                    cmd.Parameters.Add(":amt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":amt", OracleType.Number).Value = Convert.ToInt64(amt);

                if (amtf == string.Empty)
                    cmd.Parameters.Add(":amtf", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":amtf", OracleType.Number).Value = Convert.ToInt64(amtf);

                if (amtt == string.Empty)
                    cmd.Parameters.Add(":amtt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":amtt", OracleType.Number).Value = Convert.ToInt64(amtt);

                if (pert == string.Empty)
                    cmd.Parameters.Add(":pert", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":pert", OracleType.Number).Value = Convert.ToInt64(pert);

                if (cutemp == string.Empty)
                    cmd.Parameters.Add(":cutemp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":cutemp", OracleType.Number).Value = Convert.ToInt64(cutemp);

                if (nemp == string.Empty)
                    cmd.Parameters.Add(":nemp", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":nemp", OracleType.Number).Value = Convert.ToInt64(nemp);

                cmd.Parameters.Add(":nte", OracleType.VarChar).Value = nte;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add(":alcrdpol", OracleType.VarChar).Value = alcrdpol;
                cmd.Parameters.Add(":aply", OracleType.Number).Value = Convert.ToInt32(aply);

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void insertproviderbenfit(Int32 cmp, Int32 cont, string cls, string prvdg, string prvtyp, string prvcod, string srv, string coamt, string coperc, string mxamt, string nts, string typ, string prnam)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO APP.COMP_CONTRACT_CLASS_PROVIDER (COMP_ID, CONTRACT_NO, CLASS_CODE, PROV_DEGREE, PRV_TYP, PR_CODE, SERV_CODE, COPAY_AMT, COPAY_PERC, MAX_AMOUNT, NOTES, TYPE, PR_NAME, ACTIVE, CREATED_BY, CREATED_DATE)
                                                                                VALUES (:cmp, :cont, :cls, :prvdg, :prvtyp, :prvcod, :srv, :coamt, :coperc, :mxamt, :nts, :typ, :prnam, 'Y', :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":prvdg", OracleType.Number).Value = Convert.ToInt64(prvdg);
                cmd.Parameters.Add(":prvtyp", OracleType.Number).Value = Convert.ToInt64(prvtyp);
                cmd.Parameters.Add(":prvcod", OracleType.Number).Value = Convert.ToInt64(prvcod);

                if (srv == string.Empty)
                    cmd.Parameters.Add(":srv", OracleType.Number).Value = 0;//DBNull.Value;
                else
                    cmd.Parameters.Add(":srv", OracleType.Number).Value = Convert.ToInt64(srv);

                if (coamt == string.Empty)
                    cmd.Parameters.Add(":coamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":coamt", OracleType.Number).Value = Convert.ToInt64(coamt);

                if (coperc == string.Empty)
                    cmd.Parameters.Add(":coperc", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":coperc", OracleType.Number).Value = Convert.ToInt64(coperc);

                if (mxamt == string.Empty)
                    cmd.Parameters.Add(":mxamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mxamt", OracleType.Number).Value = Convert.ToInt64(mxamt);

                cmd.Parameters.Add(":typ", OracleType.VarChar).Value = typ;
                cmd.Parameters.Add(":prnam", OracleType.VarChar).Value = prnam;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public void updateproviderbenfit(Int32 cmp, Int32 cont, string cls, string prvdg, string prvtyp, string prvcod, string srv, string coamt, string coperc, string mxamt, string nts)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE APP.COMP_CONTRACT_CLASS_PROVIDER SET SERV_CODE = :srv, COPAY_AMT = :coamt, COPAY_PERC = :coperc, MAX_AMOUNT = :mxamt, NOTES = :nts, UPDATE_BY = :crby, UPDATE_DATE = :crdat
                                          WHERE COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls AND PROV_DEGREE = :prvdg AND PRV_TYP = :prvtyp AND PR_CODE = :prvcod", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":prvdg", OracleType.Number).Value = Convert.ToInt64(prvdg);
                cmd.Parameters.Add(":prvtyp", OracleType.Number).Value = Convert.ToInt64(prvtyp);
                cmd.Parameters.Add(":prvcod", OracleType.Number).Value = Convert.ToInt64(prvcod);

                if (srv == string.Empty)
                    cmd.Parameters.Add(":srv", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":srv", OracleType.Number).Value = Convert.ToInt64(srv);

                if (coamt == string.Empty)
                    cmd.Parameters.Add(":coamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":coamt", OracleType.Number).Value = Convert.ToInt64(coamt);

                if (coperc == string.Empty)
                    cmd.Parameters.Add(":coperc", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":coperc", OracleType.Number).Value = Convert.ToInt64(coperc);

                if (mxamt == string.Empty)
                    cmd.Parameters.Add(":mxamt", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":mxamt", OracleType.Number).Value = Convert.ToInt64(mxamt);

                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;

                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public int insertdataprintcard(Int32 cmp, Int32 cont, string cls, string costcod, string crd, string hodeg, string mednet, int langn, int finam, int snam, int tinam, int foinam, int ffinam, string nts, string vipflg, string crdcolr, DateTime datt, string viwcc, string typ)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO APP.PRINT_CARD (CODE, COMP_ID, CONTRACT_NO, CLASS_CODE, COST_CODE, CARD_ID, HOSPITAL_DEGREE, MEDICAL_NETWORK, LANGUAGE_PRINT, FIRST_NAME_PRINT, SECOND_NAME_PRINT, THIRD_NAME_PRINT, FOURTH_NAME_PRINT, FIFTH_NAME_PRINT, NOTES, VIP, CARD_COLOR, VALID_TO, CREATED_BY, CREATED_DATE, VIEW_CC, TYPE_CARD)
                                                                                VALUES ((SELECT NVL(MAX(CODE),0) + 1 FROM PRINT_CARD),:cmp, :cont, :cls, :costcod, :crd, :hodeg, :mednet, :langn, :finam, :snam, :tinam, :foinam, :ffinam, :nts,  :vipflg, :crdcolr, :datt, :crby, :crdat, :viwcc, :typ)", con);

                cmd.Parameters.Clear();
                //   (SELECT NVL(MAX(CODE),0) + 1 FROM PRINT_CARD,:cmp, :cont, :cls, :clscod, :crd, :hodeg, :mednet, :langn, :finam, :snam, :tinam, :foinam, :ffinam, :nts, :crby, :crdat)

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":costcod", OracleType.VarChar).Value = costcod;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":hodeg", OracleType.VarChar).Value = hodeg;
                cmd.Parameters.Add(":mednet", OracleType.VarChar).Value = mednet;
                cmd.Parameters.Add(":langn", OracleType.Number).Value = langn;
                cmd.Parameters.Add(":finam", OracleType.Number).Value = finam;
                cmd.Parameters.Add(":snam", OracleType.Number).Value = snam;
                cmd.Parameters.Add(":tinam", OracleType.Number).Value = tinam;
                cmd.Parameters.Add(":foinam", OracleType.Number).Value = foinam;
                cmd.Parameters.Add(":ffinam", OracleType.Number).Value = ffinam;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":vipflg", OracleType.VarChar).Value = vipflg;
                cmd.Parameters.Add(":crdcolr", OracleType.Number).Value = Int32.Parse(crdcolr);
                cmd.Parameters.Add(":datt", OracleType.DateTime).Value = datt;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add(":viwcc", OracleType.VarChar).Value = viwcc;
                cmd.Parameters.Add(":typ", OracleType.VarChar).Value = typ;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return 0; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public int updatedataprintcard(Int32 cod, Int32 cmp, Int32 cont, string cls, string costcod, string crd, string hodeg, string mednet, int langn, int finam, int snam, int tinam, int foinam, int ffinam, string nts, string vipflg, string crdcolr, DateTime datt, string viwcc, string typ)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"UPDATE APP.PRINT_CARD SET COST_CODE = :costcod, CARD_ID = :crd, HOSPITAL_DEGREE = :hodeg, MEDICAL_NETWORK = :mednet, LANGUAGE_PRINT = :langn, FIRST_NAME_PRINT = :finam, SECOND_NAME_PRINT = :snam, THIRD_NAME_PRINT = :tinam, FOURTH_NAME_PRINT = :foinam, FIFTH_NAME_PRINT = :ffinam, NOTES = :nts, VIP = :vipflg, CARD_COLOR = :crdcolr, VALID_TO = :datt, UPDATE_BY = :crby, UPDATE_DATE = :crdat, VIEW_CC = :viwcc, TYPE_CARD = :typ
                                          WHERE CODE = :cod AND COMP_ID = :cmp AND CONTRACT_NO = :cont AND CLASS_CODE = :cls", con);

                cmd.Parameters.Clear();
                //   (SELECT NVL(MAX(CODE),0) + 1 FROM PRINT_CARD,:cmp, :cont, :cls, :clscod, :crd, :hodeg, :mednet, :langn, :finam, :snam, :tinam, :foinam, :ffinam, :nts, :crby, :crdat)

                cmd.Parameters.Add(":cod", OracleType.Number).Value = cod;
                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":costcod", OracleType.VarChar).Value = costcod;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":hodeg", OracleType.VarChar).Value = hodeg;
                cmd.Parameters.Add(":mednet", OracleType.VarChar).Value = mednet;
                cmd.Parameters.Add(":langn", OracleType.Number).Value = langn;
                cmd.Parameters.Add(":finam", OracleType.Number).Value = finam;
                cmd.Parameters.Add(":snam", OracleType.Number).Value = snam;
                cmd.Parameters.Add(":tinam", OracleType.Number).Value = tinam;
                cmd.Parameters.Add(":foinam", OracleType.Number).Value = foinam;
                cmd.Parameters.Add(":ffinam", OracleType.Number).Value = ffinam;
                cmd.Parameters.Add(":nts", OracleType.VarChar).Value = nts;
                cmd.Parameters.Add(":vipflg", OracleType.VarChar).Value = vipflg;
                cmd.Parameters.Add(":crdcolr", OracleType.Number).Value = Int32.Parse(crdcolr);
                cmd.Parameters.Add(":datt", OracleType.DateTime).Value = datt;
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add(":viwcc", OracleType.VarChar).Value = viwcc;
                cmd.Parameters.Add(":typ", OracleType.VarChar).Value = typ;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return 0; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable getdataempprntcrd(string query, Int32 cmp, int cont, string cls, string crd, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(query, con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable getdataempprntcc(string query, Int32 cmp, int cont, string cls, string cc, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(query, con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":cc", OracleType.VarChar).Value = cc;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public DataTable getdataempprnt(string query, Int32 cmp, int cont, string cls, DateTime dat1, DateTime dat2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(query, con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":dat1", OracleType.DateTime).Value = dat1;
                cmd.Parameters.Add(":dat2", OracleType.DateTime).Value = dat2;

                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
        public int insertprintcrdtop(Int32 cod, Int32 cmp, Int32 cont, string cls, string costcod, string crd, int actn, string rson, Int32 cod2)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new OracleCommand(@"INSERT INTO APP.PRINT_CARD_TOP (CODE, COMP_ID, CONTRACT_NO, CLASS_CODE, COST_CODE, CARD_ID, ACTION, REASON, CODE_DESIGN, CREATED_BY, CREATED_DATE)
                                                                  VALUES (:cod, :cmp, :cont, :cls, :costcod, :crd, :actn, :rson, :cod2, :crby, :crdat)", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":cod", OracleType.Number).Value = cod;
                cmd.Parameters.Add(":cod2", OracleType.Number).Value = cod2;
                cmd.Parameters.Add(":cmp", OracleType.Number).Value = cmp;
                cmd.Parameters.Add(":cont", OracleType.Number).Value = cont;
                cmd.Parameters.Add(":cls", OracleType.VarChar).Value = cls;
                cmd.Parameters.Add(":costcod", OracleType.VarChar).Value = costcod;
                cmd.Parameters.Add(":crd", OracleType.VarChar).Value = crd;
                cmd.Parameters.Add(":actn", OracleType.Number).Value = actn;
                if (rson == string.Empty)
                    cmd.Parameters.Add(":rson", OracleType.Number).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":rson", OracleType.Number).Value = int.Parse(rson);
                cmd.Parameters.Add(":crby", OracleType.VarChar).Value = User.Name;
                cmd.Parameters.Add(":crdat", OracleType.DateTime).Value = DateTime.Now;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                OracleConnection.ClearAllPools();
                return 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return 0; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                    OracleConnection.ClearAllPools();
                }
            }

        }
    }
}
