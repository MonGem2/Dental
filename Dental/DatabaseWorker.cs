using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental
{
    public static class DatabaseWorker
    {
        static string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
        static SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
        static DatabaseWorker()
        {
            con.Open();
        }
        public static DataTable FindTransactions(string pattern)
        {

            string query = "select * from [Transactions]";
            if (pattern != "") // Note: txt_Search is the TextBox..
            {
                query += $" where Description Like '@{pattern}@' or where id_Patient Like '%{pattern}%' or Suma Like '%{pattern}%' or Type Like '%{pattern}%'";
            }
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();

            SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
            DataTable _dt = new DataTable();
            _adp.Fill(_dt);
            _adp.Update(_dt);
            return _dt;




        }
        public static DataSet SelectTransactions()
        {
            string text = "Select * From [Transactions]";
            try
            {

                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void NewCard(string name, string surname, string fathername,string gender, string mobphone, string homephone, string workphone, string birth, string descr)
        {
            string query = $"insert into [Patients] (Name,Surname,FatherName,Gender,Mobile_Phone,Home_Phone,Work_Phone,Date_Birth,Description,Date) values ('{name}','{surname}','{fathername}','{gender}','{mobphone}','{homephone}','{workphone}','{birth}','{descr}','{DateTime.Today.ToShortDateString()}')";
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();
        }
        public static void Close()
        {
            con.Close();
        }
    }
}
