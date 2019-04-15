using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Denta
{
    static class DbContext
    {
         const string filename = @"C:\Users\Andriy\source\repos\Denta\Denta\DBFold\db.db";
      
         public static DataView selectFirtst()
         {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
            const string sql = "select * from userss;";
            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(sql, conn);
                da.Fill(ds);
                return ds.Tables[0].DefaultView;
              
                //grid.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
                throw;
            }
         }
        public static void Delete(string ID)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
              string sql = $"Delete from userss where ID={ID}";
            using (var dbCommand = conn.CreateCommand())
            {
                conn.Open();

                dbCommand.CommandText = $"Delete from userss where ID={ID}";

                dbCommand.ExecuteNonQuery();
                conn.Dispose();
            }
        }
        public static string GetLastID()
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
        const string sql = @"SELECT * FROM userss Where ID=(SELECT MAX(ID) FROM userss)";
            try
            {
                conn.Open();
                DataSet ds = new DataSet();
        var da = new SQLiteDataAdapter(sql, conn);
        da.Fill(ds);

                return ds.Tables[0].Rows[0][0].ToString();

                //grid.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void Add(string Surname, string Name, string FatherName, string Gender, string DomTel, string RabTel, string MobTel, string Zametki, DateTime date , DateTime date1)
        {

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
            string format = "yyyy-MM-dd";
            string dateb = date.ToString(format);
            string dates = date1.ToString(format);
            dates = dates.Replace(" 12:00:00 AM", "");
            dateb = dateb.Replace(" 12:00:00 AM", "");
           
            using (var dbCommand = conn.CreateCommand())
            {
                conn.Open();

                dbCommand.CommandText = $"Insert Into Userss Values({int.Parse(GetLastID())+1},'{Name}','{Surname}','{FatherName}','{dateb}','{dates}','{Gender}','{RabTel}','{DomTel}','{MobTel}','{Zametki}')";

                dbCommand.ExecuteNonQuery();
                conn.Dispose();
            }
        }
      public static DataView FindFio(string fio)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
            const string sql = "select * from userss where Name LIKE '%or%' or ";
            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(sql, conn);
                da.Fill(ds);
                return ds.Tables[0].DefaultView; ;
                //grid.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
