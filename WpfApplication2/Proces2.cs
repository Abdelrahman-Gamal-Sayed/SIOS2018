using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace WpfApplication2
{
    class Proces2
    {
        OracleConnection con;
        OracleCommand cmd = new OracleCommand();
        OracleDataAdapter da;
        public Proces2()
        {
            con = new OracleConnection(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******");

        }

        public void open()
        {
            if (con.State != ConnectionState.Open)
                con.Open();
        }
        public void close()
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Dispose();
                con.Close();
                 
            }
        }

        public DataTable testprovider(Int32 id, int tst)
        {
            if (tst == 1)
                cmd = new OracleCommand(@"select PR_CODE from PROVIDERS_IRS where PR_CODE = :id1", con);
            else
                cmd = new OracleCommand(@"select * from IMGAUB where ID = :id1", con);

            cmd.Parameters.Clear();
            cmd.Parameters.Add(":id1", OracleType.Number).Value = id;

            da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.Dispose();
            con.Close();
             
            return dt;

        }

        public void IorUimg(Int32 prvd, byte[] img, int tst)
        {
            if (tst == 1)
                cmd = new OracleCommand("Insert Into IMGAUB (ID, IMG) VALUES (:id1,:image1)", con);
            else if (tst == 2)
                cmd = new OracleCommand("Insert Into IMGAUB (ID, IMG2) VALUES (:id1,:image1)", con);
            else if (tst == 3)
                cmd = new OracleCommand("Update IMGAUB SET IMG = :image1 where ID = :id1", con);
            else if (tst == 4)
                cmd = new OracleCommand("Update IMGAUB SET IMG2 = :image1 where ID = :id1", con);

            open();
            cmd.Parameters.Clear();

            cmd.Parameters.Add(":id1", OracleType.Number).Value = prvd;
            cmd.Parameters.Add(":image1", OracleType.Blob).Value = img;

            cmd.ExecuteNonQuery();
            close();
        }
        public void IorUimgandstmp(Int32 prvd, byte[] img, byte[] stmp, int tst)
        {
            if (tst == 1)
                cmd = new OracleCommand("Insert Into IMGAUB (ID, IMG, IMG2) VALUES (:id1,:image1,:image2)", con);
            else if (tst == 2)
                cmd = new OracleCommand("Update IMGAUB SET IMG = :image1, IMG2 = :image2 where ID = :id1", con);

            open();
            cmd.Parameters.Clear();

            cmd.Parameters.Add(":id1", OracleType.Number).Value = prvd;
            cmd.Parameters.Add(":image1", OracleType.Blob).Value = img;
            cmd.Parameters.Add(":image2", OracleType.Blob).Value = stmp;

            cmd.ExecuteNonQuery();
            close();
        }
        public void Delimg(Int32 prvd, int tst)
        {
            if (tst == 1)
                cmd = new OracleCommand("Update IMGAUB SET IMG = null where ID = :id1", con);
            else if (tst == 2)
                cmd = new OracleCommand("Update IMGAUB SET IMG2 = null where ID = :id1", con);
            else if (tst == 3)
                cmd = new OracleCommand("DELETE FROM IMGAUB WHERE ID = :id1", con);

            open();
            cmd.Parameters.Clear();

            cmd.Parameters.Add(":id1", OracleType.Number).Value = prvd;

            cmd.ExecuteNonQuery();
            close();
        }

    }
}
