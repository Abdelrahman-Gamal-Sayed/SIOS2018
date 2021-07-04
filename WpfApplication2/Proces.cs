using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace WpfApplication2
{
    class Proces
    {
        OracleConnection con;
        OracleCommand cmd = new OracleCommand();
        OracleDataAdapter da;


        public DataTable getdata()
        {
            DataTable dt = new DataTable();
            con = new OracleConnection(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******");

            cmd = new OracleCommand("select * from ME_AUB", con);
            da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            con.Dispose();
            con.Close();
             
            return dt;

        }



    }
}
