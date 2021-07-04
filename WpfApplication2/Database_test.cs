using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Windows;


namespace WpfApplication2
{
    class Database_test
    {

        public static string conction = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=dms_test;Password=***";

        //OracleConnection con = new OracleConnection(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
        //                                    (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
        //                                    (SERVICE_NAME=ora11g)));User Id=dms_test;Password=***");

        //OracleCommand cmd = new OracleCommand();

        //OracleDataAdapter da;
        DataTable dd;
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

                 
                //DB b = new DB();
                //dd = b.RunReader("select CODE,TYP_ANAME,PR_ANAME,ITEM_NAME,DATE_REQUEST,NUM,STATUS STATUSs,SERIAL_NOM,SERIAL_NOM_FROM,SERIAL_NOM_TO,DELIVERY_METHOD from NOTEBOOKS WHERE STATUS !='F'  and CODE like '%" + CODE + "%' and  TYP_ANAME like '%" + TYP_ANAME + "%' and PR_ANAME like '%" + PR_ANAME + "%' and SERIAL_NOM like '%" + SERIAL_NOM + "%' and SERIAL_NOM_FROM like '%" + SERIAL_NOM_FROM + "%' and SERIAL_NOM_TO like '%" + SERIAL_NOM_TO + "%' and ITEM_NAME like '%" + ITEM_NAME + "%'").Result;

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return dd; }
            finally
            {
                
                    if (con.State != ConnectionState.Closed)
                {    con.Dispose();
                    con.Close();

                     
                }
            }

        }
        //217.139.89..22

        public DataTable selectdataoprationaddem(string CODE, string comid, string bransh, string typ, string telefon, string mobail, string dateparthfrom, string dateparthto, string datestratfrom, string datestratto, string gender, string ageraraba, string mo7fza, string mantaqa)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                cmd = new OracleCommand(@"select  DISTINCT c.CARD_ID, c.C_COMP_ID,c.COST_CODE,c.DEPT_ID,
c.EMP_CODE,c.CONTRACT_NO,c.CLASS_CODE,c.PRINT_TYP,c.PRINT_FST_NAME,c.PRINT_SEC_NAME,c.PRINT_THR_NAME,c.PRINT_FTH_NAME
,c.PRINT_LST_NAME,c.GENDER,c.USR_TYP,c.MATERIAL_STATUS,c.INS_TYP,c.REF_EMP,c.EMP_ANAME_ST,c.EMP_ANAME_SC
,c.EMP_ANAME_TH,c.EMP_ANAME_FR,c.EMP_ANAME_FAM,c.EMP_ANAME,c.EMP_ENAME_ST,c.EMP_ENAME_SC,c.EMP_ENAME_TH,c.EMP_ENAME_FR
,c.EMP_ENAME_FAM,c.EMP_ENAME,c.SPECIFIC_DATE,c.HIRE_DATE,c.INS_START_DATE,c.INS_END_DATE,c.ADDRESS2,EMP_ID,
c.ADDRESS1,c.FAX,c.EMAIL,TEL1,c.TEL2,c.BK_CODE,BK_ACC_NO ,c.W_GLASS/*,PERVIOUS_DISEASE*/,''PERVIOUS_DISEASE,c.EXP_CELLING
,c.EMP_INSURANCE_NO,c.EXP_AGE ,c.COMP_ID,c.BRANCH_CODE ,MAX_AMOUNT,ANNUAL_PREM,OVER_AGE_AMT,SON_COVER_FROM,
SON_COVER_TO,COVER_RELATION_AMT,CARD_FEE_FRST,CARD_FEE_OTH,MAX_CEILING,PERT_CEILING,OVER_AGE_PERT,
TRAVEL_COVER_AMT,STOP_APPROVAL,TRAVEL_COVER_PERT,P_BEF_CANCEL_CONTRACT,CRIT_CASE_AMT,ALLOW_PERIOD,
STOP_INDEM,INTEREST_VAL,DELAY_PERT,CRIT_CASE_DAYS,HOSPITAL_DEGREE,STOP_ISSUE_CARD,AMBULANCE_AMT,CRIT_CASE,AMBULANCE
,C.BIRTH_DATE,c.NOTES typ_emp,c.EMP_INSURANCE_NO NOTES ,TERMINATE_FLAG,TERMINATE_DATE from INSURANCE_CLASS i, COMP_EMPLOYEES c LEFT JOIN COMP_CONTRACT_CLASS_EMP B ON c.C_COMP_ID = B.C_COMP_ID AND 
c.CONTRACT_NO = B.CONTRACT_NO AND c.CONTRACT_NO = B.CONTRACT_NO AND c.CLASS_CODE = B.CLASS_CODE  
where  c.CARD_ID like '%'||:p_codecard||'%' and  c.C_COMP_ID like '%'||:p_comid||'%' and c.COST_CODE like '%'||:p_bransh||'%' and i.CLASS_ENAME like '%'|| :p_typ ||'%' 
 and (to_date(BIRTH_DATE) BETWEEN  :P_dateparthfrom and  :p_dateparthto )and (to_date(SPECIFIC_DATE) BETWEEN 
 :p_datestratfrom and :p_datestratttto) /*and GENDER like '%'||:p_gender||'%' and REF_EMP like '%'||:p_ageraraba||'%'  and INS_TYP like '%'||:p_ageraraba||'%' 
and SUBSTR( ADDRESS2,INSTR(ADDRESS2,'/')+1,INSTR(ADDRESS2,'/') ) like '%'||:p_mantaqa||'%' and    SUBSTR( ADDRESS2,0,INSTR(ADDRESS2,'/')-1 )   like '%'||:p_mo7fza || '%'
*/ and c.CLASS_CODE=i.CLASS_CODE and

c.CONTRACT_NO =(select  MAX(d.CONTRACT_NO) from COMP_EMPLOYEES d where d.CARD_ID=c.CARD_ID) /*and c.TEL1 like '%'||:p_telefon||'%' and c.TEL2 like '%'||:p_mobail||'%'*/", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":p_codecard", OracleType.VarChar).Value = CODE;
                if (comid.Trim() != string.Empty)
                    cmd.Parameters.Add(":p_comid", OracleType.Number).Value = Convert.ToInt32(comid);
                else cmd.Parameters.Add(":p_comid", OracleType.Number).Value = DBNull.Value;
                cmd.Parameters.Add(":p_bransh", OracleType.VarChar).Value = bransh;
                cmd.Parameters.Add(":p_typ", OracleType.VarChar).Value = typ;

                /* cmd.Parameters.Add(":p_telefon", OracleType.VarChar).Value = telefon;

                cmd.Parameters.Add(":p_mobail", OracleType.VarChar).Value = mobail;*/
                if (dateparthfrom.Trim() != string.Empty)
                    cmd.Parameters.Add(":P_dateparthfrom", OracleType.DateTime).Value = Convert.ToDateTime(dateparthfrom);
                else cmd.Parameters.Add(":p_dateparthfrom", OracleType.DateTime).Value = Convert.ToDateTime("01-01-1990");
                if (dateparthto.Trim() != string.Empty)
                    cmd.Parameters.Add(":p_dateparthto", OracleType.DateTime).Value = Convert.ToDateTime(dateparthto);
                else cmd.Parameters.Add(":p_dateparthto", OracleType.DateTime).Value = DateTime.Now;
                if (datestratfrom.Trim() != string.Empty)
                    cmd.Parameters.Add(":p_datestratfrom", OracleType.DateTime).Value = Convert.ToDateTime(datestratfrom);
                else cmd.Parameters.Add(":p_datestratfrom", OracleType.DateTime).Value = Convert.ToDateTime("01-01-1990");
                if (datestratto.Trim() != string.Empty)
                    cmd.Parameters.Add(":p_datestratttto", OracleType.DateTime).Value = Convert.ToDateTime(datestratto);
                else cmd.Parameters.Add(":p_datestratttto", OracleType.DateTime).Value = DateTime.Now;
                /* if (gender.Trim() == string.Empty)
                     cmd.Parameters.Add(":p_gender", OracleType.Number).Value = DBNull.Value;
                 else if (gender.Trim() == "ذكر")
                     cmd.Parameters.Add(":p_gender", OracleType.Number).Value = 1;
                 else if (gender.Trim() == "أنثي")
                     cmd.Parameters.Add(":p_gender", OracleType.Number).Value = 2;
                 if(ageraraba.Trim()!=string.Empty)
                 cmd.Parameters.Add(":p_ageraraba", OracleType.Number).Value =Convert.ToInt32( ageraraba);
                 else cmd.Parameters.Add(":p_ageraraba", OracleType.Number).Value =DBNull.Value;
                 cmd.Parameters.Add(":p_mo7fza", OracleType.VarChar).Value = mo7fza;
                 cmd.Parameters.Add(":p_mantaqa", OracleType.VarChar).Value = mantaqa;*/



                da = new OracleDataAdapter(cmd);
                dd = new DataTable();

                da.Fill(dd);
                con.Dispose();
                con.Close();

                 

                return dd;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return dd; }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                     
                }
            }

        }
        public bool insert_req_Del_operation(string card_id, string comp_id, Int32 max_contra, string class_code, DateTime delete_date, string reason_closs_open, string active, string notez, string created_by, string type, string date_deliver_card, Int32 comp_id_id, Int32 branch_id, string n_codez, string ne_class, string new_card)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {





                con.Open();


                cmd = new OracleCommand(@"  insert into CLOSE_EMP_DATA (CARD_ID,C_COMP_ID,CONTRACT_NO,CLASS_CODE, CLOSE_DATE,     CLOSE_REASON     ,ACTIVE, NOTES, CREATED_BY,CREATED_DATE,TRANS_TYP,WITHDRAW_CARD_DATE,COMP_ID,BRANCH_CODE,N_CODE,N_CLASS,N_CARD,KP_CL,KP_CO)
                                                                 VALUES(:card_id,:comp_id,:max_contra,:class_code,:delete_date,:reason_closs_open,:active, :notez,:created_by,sysdate,        :type,:date_deliver_card,     :comp_id_id,:branch_id,:n_codez,:ne_class,:new_card,'Y','Y') ", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":card_id", OracleType.VarChar).Value = card_id;
                cmd.Parameters.Add(":comp_id", OracleType.VarChar).Value = comp_id;
                cmd.Parameters.Add(":max_contra", OracleType.VarChar).Value = max_contra;
                cmd.Parameters.Add(":class_code", OracleType.VarChar).Value = class_code;
                cmd.Parameters.Add(":delete_date", OracleType.DateTime).Value = delete_date;
                cmd.Parameters.Add(":reason_closs_open", OracleType.VarChar).Value = reason_closs_open;
                cmd.Parameters.Add(":new_card", OracleType.VarChar).Value = new_card;
                cmd.Parameters.Add(":active", OracleType.VarChar).Value = "I";
                cmd.Parameters.Add(":notez", OracleType.VarChar).Value = notez;
                cmd.Parameters.Add(":created_by", OracleType.VarChar).Value = created_by;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = type;
                if (date_deliver_card == string.Empty)
                    cmd.Parameters.Add(":date_deliver_card", OracleType.VarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(":date_deliver_card", OracleType.DateTime).Value = Convert.ToDateTime(date_deliver_card);
                if (n_codez == string.Empty)
                    cmd.Parameters.Add(":n_codez", OracleType.VarChar).Value = n_codez;
                else
                    cmd.Parameters.Add(":n_codez", OracleType.Number).Value = Convert.ToInt32(n_codez);
                cmd.Parameters.Add(":comp_id_id", OracleType.Number).Value = comp_id_id;
                cmd.Parameters.Add(":ne_class", OracleType.VarChar).Value = ne_class;
                cmd.Parameters.Add(":branch_id", OracleType.Number).Value = branch_id;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                 
                return true;
            }
            catch (Exception ex)
            {
                string mess = ex.Message.Split(':')[0];

                if (mess == "ORA-00001")
                    return true;

                else
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                     
                }
            }

        }

        //torb28-8 Done
        public void insert_req_Del_operation(string card_id, string comp_id, Int64 max_contra, Int32 class_code, DateTime delete_date, string reason_closs_open, string active, string notez, string created_by, string type, string date_deliver_card, Int32 comp_id_id, Int32 branch_id, string n_codez, string ne_class, string new_card)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {





                con.Open();


                cmd = new OracleCommand(@"  insert into dms_test.CLOSE_EMP_DATA (CARD_ID,C_COMP_ID,CONTRACT_NO,CLASS_CODE, CLOSE_DATE,     CLOSE_REASON    ,ACTIVE, NOTES, CREATED_BY,CREATED_DATE,TRANS_TYP,WITHDRAW_CARD_DATE,COMP_ID,BRANCH_CODE,N_CODE,N_CLASS,N_CARD)
                                                                 VALUES(:card_id,:comp_id,:max_contra,:class_code,:delete_date,:reason_closs_open,:active,:notez,:created_by,sysdate,:type,:date_deliver_card,     :comp_id_id,:branch_id,:n_codez,:ne_class,:new_card) ", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":card_id", OracleType.VarChar).Value = card_id;
                cmd.Parameters.Add(":comp_id", OracleType.VarChar).Value = comp_id;
                cmd.Parameters.Add(":max_contra", OracleType.VarChar).Value = max_contra;
                cmd.Parameters.Add(":class_code", OracleType.Number).Value = class_code;
                cmd.Parameters.Add(":delete_date", OracleType.DateTime).Value = delete_date;
                cmd.Parameters.Add(":reason_closs_open", OracleType.VarChar).Value = reason_closs_open;
                cmd.Parameters.Add(":new_card", OracleType.VarChar).Value = new_card;
                cmd.Parameters.Add(":active", OracleType.VarChar).Value = "I";
                cmd.Parameters.Add(":notez", OracleType.VarChar).Value = notez;
                cmd.Parameters.Add(":created_by", OracleType.VarChar).Value = created_by;
                cmd.Parameters.Add(":type", OracleType.VarChar).Value = type;
                if (date_deliver_card == string.Empty)
                    cmd.Parameters.Add(":date_deliver_card", OracleType.VarChar).Value = date_deliver_card;
                else
                    cmd.Parameters.Add(":date_deliver_card", OracleType.DateTime).Value = Convert.ToDateTime(date_deliver_card);
                if (n_codez == string.Empty)
                    cmd.Parameters.Add(":n_codez", OracleType.VarChar).Value = n_codez;
                else
                    cmd.Parameters.Add(":n_codez", OracleType.Number).Value = Convert.ToInt32(n_codez);
                cmd.Parameters.Add(":comp_id_id", OracleType.Number).Value = comp_id_id;
                cmd.Parameters.Add(":ne_class", OracleType.VarChar).Value = ne_class;
                cmd.Parameters.Add(":branch_id", OracleType.Number).Value = branch_id;


                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                 
                // MessageBox.Show("تم الحفظ");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                     
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
                string v_active = "";
                System.Data.DataTable dtrr = db_test.RunReader(" select  count (*) from COMP_EMPLOYEES where ACTIVE ='I' and C_COMP_ID='" + comp_id + "' and CARD_ID='" + card_id + "' and contract_no=(select max(CONTRACT_NO) from COMP_EMPLOYEES where C_COMP_ID='" + comp_id + "') ").Result;
                if (dtrr.Rows[0][0].ToString() == "0")
                    v_active = "U";
                else
                    v_active = "I";


                con.Open();

                cmd = new OracleCommand(@"update dms_test.COMP_EMPLOYEES set TERMINATE_FLAG='Y',ACTIVE='" + v_active + "' , TERMINATE_DATE =to_date(:termin_date)-1 where C_COMP_ID=:comp_id and CARD_ID=:card_id and contract_no=(select max(CONTRACT_NO) from COMP_EMPLOYEES where C_COMP_ID=:comp_id) ", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":comp_id", OracleType.Number).Value = comp_id;
                cmd.Parameters.Add(":card_id", OracleType.VarChar).Value = card_id;
                cmd.Parameters.Add(":termin_date", OracleType.DateTime).Value = termin_date;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                 

                //  MessageBox.Show("تم التعديل");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                     
                }
            }

        }
        public bool updecloseopration(string comp_id, string card_id, Int64 contact_nom, string v_TERMINATE_DATE)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                string v_active = "";
                System.Data.DataTable dtrr = db_test.RunReader(" select  count (*) from COMP_EMPLOYEES where ACTIVE ='I' and   C_COMP_ID='" + comp_id + "' and CARD_ID='" + card_id + "' and contract_no='" + contact_nom + "' ").Result;
                if (dtrr.Rows[0][0].ToString() == "0")
                    v_active = "U";
                else
                    v_active = "I";
                con.Open();

                cmd = new OracleCommand(@"update COMP_EMPLOYEES set ACTIVE='" + v_active + "',TERMINATE_FLAG='Y'  , TERMINATE_DATE = :p_v_TERMINATE_DATE ,UPDATE_BY='" + User.Name + "' ,UPDATE_DATE=sysdate where C_COMP_ID=:p_compid and CARD_ID=:p_cardid and contract_no=:p_contactnom", con);

                cmd.Parameters.Clear();

                cmd.Parameters.Add(":p_compid", OracleType.Number).Value = Convert.ToInt64(comp_id);
                cmd.Parameters.Add(":p_contactnom", OracleType.Number).Value = contact_nom;
                cmd.Parameters.Add(":p_cardid", OracleType.VarChar).Value = card_id;
                if (v_TERMINATE_DATE != string.Empty)
                    cmd.Parameters.Add(":p_v_TERMINATE_DATE", OracleType.DateTime).Value = Convert.ToDateTime(v_TERMINATE_DATE);
                else cmd.Parameters.Add(":p_v_TERMINATE_DATE", OracleType.DateTime).Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                 
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

                     
                }
            }

        }

        public bool updetopenopration(string comp_id, string card_id, Int64 contact_nom, string spasfec_date)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            try
            {
                string v_active = "";
                System.Data.DataTable dtrr = db_test.RunReader(" select  count (*) from COMP_EMPLOYEES where ACTIVE ='I' and   C_COMP_ID='" + comp_id + "' and CARD_ID='" + card_id + "' and contract_no='" + contact_nom + "' ").Result;
                if (dtrr.Rows[0][0].ToString() == "0")
                    v_active = "U";
                else
                    v_active = "I";

                con.Open();

                cmd = new OracleCommand(@"update COMP_EMPLOYEES set ACTIVE='" + v_active + "',TERMINATE_FLAG='N' ,SPECIFIC_DATE=:p_stpcifcdate, TERMINATE_DATE = '' ,UPDATE_BY='" + User.Name + "' ,UPDATE_DATE=sysdate where C_COMP_ID=:p_compid and CARD_ID=:p_cardid and contract_no=:p_contactnom", con);

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

                     
                }
            }

        }


        //joba16-10
        public bool insertceatexcaleaddemo(string code, string compid, string bransh, string row1, string connom, string row2, string row9, string a1, string a2, string a3, string a4, string a5, string e1, string e2, string e3, string e4, string e5, string row10, string dates, string datee, string row8, string bthd, string typeempadd, string empref, string relation)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            bool test = false;
            try
            {
                con.Open();
                cmd = new OracleCommand(@"  insert into dms_test.COMP_EMPLOYEES (CARD_ID,C_COMP_ID,COST_CODE,EMP_CODE,CONTRACT_NO,CLASS_CODE,GENDER,EMP_ANAME_ST,EMP_ANAME_SC,EMP_ANAME_TH,EMP_ANAME_FR,EMP_ANAME_FAM,EMP_ANAME,EMP_ENAME_ST,EMP_ENAME_SC,EMP_ENAME_TH,EMP_ENAME_FR,EMP_ENAME_FAM,EMP_ENAME,SPECIFIC_DATE,INS_START_DATE,INS_END_DATE,EMP_ID,COMP_ID,BRANCH_CODE,BIRTH_DATE,TERMINATE_FLAG ,CREATED_BY,CREATED_DATE,EMP_SEQ,REF_EMP,OLD_CLASS_CODE,ACTIVE,INS_TYP,EFFECT_DATE_TYPE ,PRINT_TYP,PRINT_FST_NAME,PRINT_SEC_NAME,PRINT_THR_NAME,PRINT_FTH_NAME,PRINT_LST_NAME,USR_TYP,MATERIAL_STATUS)
                                                                        VALUES ( :code ,:compid,:bransh,      :row1, :connom        , :row2    ,:row9,    :a1,       :a2        ,:a3          ,: a4       ,:a5          ,:row3,     :e1        ,:e2        ,:e3          ,:e4         ,:e5          ,:row4,   :row10,         :dates       ,:datee      ,:row8  ,1         ,1,     :bthd,'N','" + User.Name + "' ,SYSDATE,:p_seq_emp,:empref,:typeempadd,'I',:relation        ,'SD'            ,'E'       ,'Y'             ,'Y',          'Y'              ,'N'          ,'N'         ,'N'     ,'S'                  )", con);
                cmd.Parameters.Clear();
                string row3 = a1 + a2 + a3 + a4 + a5;
                string row4 = e1 + e2 + e3 + e4 + e5;
                //  string a1, a2, a3, a4, a5, e1, e2, e3, e4, e5;
                // string[] arra = row3.Trim().Split(' ');
                // if (arra.Length > 0) a1 = arra[0].ToString() ? a1 = "";
                // a1 = arra.Length >= 1 ? arra[0].ToString() : "";
                cmd.Parameters.Add(":a1", OracleType.VarChar).Value = a1;
                // a2 = arra.Length >= 2 ? arra[1].ToString() : "";
                cmd.Parameters.Add(":a2", OracleType.VarChar).Value = a2;
                //  a3 = arra.Length >= 3 ? arra[2].ToString() : "";
                cmd.Parameters.Add(":a3", OracleType.VarChar).Value = a3;
                //a4 = arra.Length >= 4 ? arra[3].ToString() : "";
                cmd.Parameters.Add(":a4", OracleType.VarChar).Value = a4;
                //  a5 = arra.Length >= 5 ? arra[4].ToString() : "";
                cmd.Parameters.Add(":a5", OracleType.VarChar).Value = a5;

                // string[] arre = row4.Trim().Split(' ');
                //  e1 = arre.Length >= 1 ? arre[0].ToString() : "";
                cmd.Parameters.Add(":e1", OracleType.VarChar).Value = e1;
                //  e2 = arre.Length >= 2 ? arre[1].ToString() : "";
                cmd.Parameters.Add(":e2", OracleType.VarChar).Value = e2;
                //  e3 = arre.Length >= 3 ? arre[2].ToString() : "";
                cmd.Parameters.Add(":e3", OracleType.VarChar).Value = e3;
                //  e4 = arre.Length >= 4 ? arre[3].ToString() : "";
                cmd.Parameters.Add(":e4", OracleType.VarChar).Value = e4;
                //   e5 = arre.Length >= 5 ? arre[4].ToString() : "";
                cmd.Parameters.Add(":e5", OracleType.VarChar).Value = e5;

                cmd.Parameters.Add(":code", OracleType.VarChar).Value = code;
                cmd.Parameters.Add(":relation", OracleType.VarChar).Value = relation;
                cmd.Parameters.Add(":compid", OracleType.Number).Value = Convert.ToInt64(compid);

                if (bransh == string.Empty)
                    cmd.Parameters.Add(":bransh", OracleType.Number).Value = DBNull.Value;
                else cmd.Parameters.Add(":bransh", OracleType.Number).Value = Convert.ToInt64(bransh);
                if (row1 == string.Empty)
                    cmd.Parameters.Add(":row1", OracleType.Number).Value = DBNull.Value;
                else cmd.Parameters.Add(":row1", OracleType.Number).Value = Convert.ToInt64(row1);
                if (connom == string.Empty)
                    cmd.Parameters.Add(":connom", OracleType.Number).Value = DBNull.Value;
                else cmd.Parameters.Add(":connom", OracleType.Number).Value = Convert.ToInt64(connom);

                cmd.Parameters.Add(":row2", OracleType.VarChar).Value = row2;
                if (row9 == string.Empty)
                    cmd.Parameters.Add(":row9", OracleType.Number).Value = DBNull.Value;
                else cmd.Parameters.Add(":row9", OracleType.Number).Value = Convert.ToInt64(row9);

                cmd.Parameters.Add(":row3", OracleType.VarChar).Value = row3;
                cmd.Parameters.Add(":row4", OracleType.VarChar).Value = row4;

                if (row10 == string.Empty)
                    cmd.Parameters.Add(":row10", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":row10", OracleType.DateTime).Value = Convert.ToDateTime(row10);

                if (dates == string.Empty)
                    cmd.Parameters.Add(":dates", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":dates", OracleType.DateTime).Value = Convert.ToDateTime(dates);

                if (datee == string.Empty)
                    cmd.Parameters.Add(":datee", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":datee", OracleType.DateTime).Value = Convert.ToDateTime(datee);
                cmd.Parameters.Add(":row8", OracleType.VarChar).Value = row8;

                if (bthd == string.Empty)
                    cmd.Parameters.Add(":bthd", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":bthd", OracleType.DateTime).Value = Convert.ToDateTime(bthd);
                cmd.Parameters.Add(":p_seq_emp", OracleType.Number).Value = Convert.ToInt32(db_test.RunReader("select nvl( (select MAX(EMP_SEQ)from dms_test.COMP_EMPLOYEES  where EMP_ID='" + row8 + "' ),nvl((select MAX(EMP_SEQ)from dms_test.COMP_EMPLOYEES )+1,1)) EMP_SEQ from dms_test.COMP_EMPLOYEES where ROWNUM=1").Result.Rows[0][0].ToString());
                cmd.Parameters.Add(":typeempadd", OracleType.VarChar).Value = typeempadd;
                cmd.Parameters.Add(":empref", OracleType.VarChar).Value = empref;

                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                 
                test = true;
            }
            catch (Exception ex)
            {
                string mess = ex.Message.Split(':')[0];

                if (mess == "ORA-00001")
                    MessageBox.Show("تم تسجيل هذا الكارت من قبل");
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                     
                }
            }
            return test;

        }
        public bool insertdatacontaract(string code, string contaractnom, string datetstart, string dateend, string active)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            bool test = false;
            try
            {
                con.Open();
                cmd = new OracleCommand(@"  INSERT INTO CONTRACT_DATA (C_COMP_ID,CONTRACT_NO,COMP_ID,BRANCH_CODE,DATE_FROM,DATE_TO,ACTIVE,CREATED_DATE,CREATED_BY) 
                                                                 VALUES(:p_code,:contaractnom,1,1,           :p_datetstart,:p_dateend,:p_active,SYSDATE,'" + User.Name + "')", con);
                cmd.Parameters.Clear();


                cmd.Parameters.Add(":p_code", OracleType.Number).Value = code;

                cmd.Parameters.Add(":contaractnom", OracleType.Number).Value = contaractnom;


                if (datetstart == string.Empty)
                    cmd.Parameters.Add(":p_datetstart", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":p_datetstart", OracleType.DateTime).Value = Convert.ToDateTime(datetstart);
                if (dateend == string.Empty)
                    cmd.Parameters.Add(":p_dateend", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":p_dateend", OracleType.DateTime).Value = Convert.ToDateTime(dateend);

                cmd.Parameters.Add(":p_active", OracleType.VarChar).Value = active;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                 
                test = true;
            }
            catch (Exception ex)
            {
                string mess = ex.Message.Split(':')[0];

                if (mess == "ORA-00001")
                    MessageBox.Show("تم تسجيل هذا الكارت من قبل");
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                     
                }
            }
            return test;

        }
       
        public bool updatadatacontaract(string code, string contaractnom, string datetstart, string dateend)
        {
            OracleConnection con = new OracleConnection(conction);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            bool test = false;
            try
            {
                con.Open();
                cmd = new OracleCommand(@"  UPDATE CONTRACT_DATA SET CONTRACT_NO=:contaractnom,DATE_FROM=:p_datetstart,DATE_TO=:p_dateend,UPDATE_DATE=SYSDATE,UPDATE_BY='" + User.Name + "'  WHERE C_COMP_ID=:p_code ", con);

                cmd.Parameters.Clear();


                cmd.Parameters.Add(":p_code", OracleType.Number).Value = code;

                cmd.Parameters.Add(":contaractnom", OracleType.Number).Value = contaractnom;


                if (datetstart == string.Empty)
                    cmd.Parameters.Add(":p_datetstart", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":p_datetstart", OracleType.DateTime).Value = Convert.ToDateTime(datetstart);
                if (dateend == string.Empty)
                    cmd.Parameters.Add(":p_dateend", OracleType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add(":p_dateend", OracleType.DateTime).Value = Convert.ToDateTime(dateend);

                // cmd.Parameters.Add(":p_active", OracleType.VarChar).Value = active;
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();

                 
                test = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Dispose();
                    con.Close();

                     
                }
            }
            return test;

        }
        db_dms_test db_test = new db_dms_test();

        //internal void insert_req_Del_operation(string text1, string text2, long cONTRACT, int class_code_dms, DateTime dateTime, string text3, string active, string text4, string name, string type, string text5, int comp_id_id, int branch_id, string n_code, string new_class, string new_crd)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

