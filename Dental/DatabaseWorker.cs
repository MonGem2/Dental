﻿using System;
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
                query += $" where Description Like '@{pattern}@' or where id_Patient Like '%{pattern}%' or where Suma Like '%{pattern}%' or where Type Like '%{pattern}%'";
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
        public static void InsertTransaction(string Price, string Descr, string Id_Pat, string Date)
        {

            string query1 = $"insert into [Transactions] (Suma,Description,id_Patient,Date,Type) values ('{Price}','{Descr}','{Id_Pat}','{Date}','Добавлен долг')";
            SQLiteCommand _cmd1 = new SQLiteCommand(query1, con);
            _cmd1.ExecuteNonQuery();

        }
        public static void InsertDepth(string Price, string Descr, string Id_Pat, string Date)
        {

            string query = $"insert into [Depth] (Suma,Description,id_Patient,Date) values ('{Price}','{Descr}','{Id_Pat}','{Date}')";
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();

        }
        public static void InsertPered(string Price, string Descr, string Id_Pat, string Date)
        {

            string query = $"insert into [Pered] (Suma,Description,id_Patient,Date) values ('{Price}','{Descr}','{Id_Pat}','{Date}')";
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();

        }

        public static void InsertTreatment(string Price, string Descr, string Id_Pat, string Date)
        {

            string query = $"insert into [Treatment] (Date,Description,id_Patient,Price) values ('{Date}','{Descr}','{Id_Pat}','{Price}')";
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();

        }
        public static Patient getPatient(string Id)
        {
            Patient patient = new Patient();
            string text = $"Select [Date],[Date_Birth], [Mobile_Phone], [Home_Phone], [Work_Phone], [Description] From [Patients] where Id='{Id}'";
            SQLiteCommand comand = new SQLiteCommand(text, con);
            SQLiteDataReader dataReader = comand.ExecuteReader();
            while (dataReader.Read())
            {
                patient.Date = dataReader.GetString(0);
                patient.Date_Birth = dataReader.GetString(1);
                patient.Mobile_Phone = dataReader.GetString(2);

                patient.Home_Phone = dataReader.GetString(3);
                patient.Work_Phone = dataReader.GetString(4);
                patient.Description = dataReader.GetString(5);
            }

            return patient;
        }

        public static string GetTreatmentString(string id)
        {
            string Rez = string.Empty;
            try
            {
                string text = $"Select [Date] From [Treatment] where id_Patient='{id}'";
                string tmp = string.Empty;
                string tmp1 = string.Empty;
                
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.GetString(0) != tmp)
                    {
                        
                        Rez += "Дата: " + dataReader.GetString(0) + "\n";
                        text = $"Select [Description] From [Treatment] where id_Patient='{id}' and Date='{dataReader.GetString(0)}'";

                        try
                        {
                         
                            SQLiteCommand comand1 = new SQLiteCommand(text, con);
                            SQLiteDataReader dataReader1 = comand1.ExecuteReader();
                            while (dataReader1.Read())
                            {
                               
                                Rez += "Описание: " + dataReader1.GetString(0) + "\n";
                                text = $"Select [Price] From [Treatment] where id_Patient='{id}' and Date='{dataReader.GetString(0)}' and Description='{dataReader1.GetString(0)}'";
                                try
                                {                                                                       
                                    SQLiteCommand comand2 = new SQLiteCommand(text, con);
                                    SQLiteDataReader dataReader2 = comand2.ExecuteReader();
                                    while (dataReader2.Read())
                                    {
                                        Rez += "Цена: " + dataReader2.GetDouble(0).ToString() + "\n";
                                    }
                                    Rez += "\n";
                                }
                                catch (Exception e)
                                {
                                }
                            }
                            

                        }
                        catch (Exception e)
                        {

                        }
                    }
                    tmp = dataReader.GetString(0);
                    
                }
                
            }
            catch (Exception e)
            {

            }
            return Rez;
        }


    }
}