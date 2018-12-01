using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace CharByDNA
{

    class Database
    {

        private char[] badchars = {' ','/','>','<','?','"',':','\\','|','*'};

        private string extension = ".db";

        private string version = "Version=3";

        private string UncompleteConn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\";

        private string RecreateDbsql = "D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\RecreateDB.sql";

        private string InitDbsql = "D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\InitializeDB.sql";

        // Desktop Conn
        private string sqlconn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        public SQLiteConnection SQLCONN { get; set; }

        public string DbName { get; set; }

        public Database()
        {

            this.SQLCONN = new SQLiteConnection(sqlconn);
            this.DbName = "Game.db";

            ReInitDB();

        }

        public Database(string filename, bool isCreated)
        {

            if (!isCreated)
            {

                if (!CheckFilename(filename))
                {

                    Console.WriteLine("The filename is not valid");

                    return;

                }

                else
                {

                    if (!CheckExtension(filename))
                    {

                        filename += extension;

                    }

                    SQLiteConnection.CreateFile(filename);

                    this.SQLCONN = new SQLiteConnection(UncompleteConn + filename + ";" + version);

                    this.DbName = filename;

                    InitDB();

                }

            }

            else
            {

                if (!CheckExtension(filename))
                {

                    filename += extension;

                }

                if (!File.Exists(filename))
                {

                    Console.WriteLine("The database does not exist");

                    return;

                }

                this.SQLCONN = new SQLiteConnection(UncompleteConn + filename + ";" + version);

                this.DbName = filename;

            }

        }

        public virtual int NumOfRowsInTable()
        {

            return -1;

        }

        private bool CheckExtension(string str)
        {

            if (str.Contains(extension))
            {

                return true;

            }

            return false;

        }

        private bool CheckFilename(string str)
        {

            for (int i = 0; i < badchars.Length; i++)
            {

                for (int j = 0; j < str.Length; j++)
                {

                    if (str[j] == badchars[i])
                    {

                        return false;

                    }

                }

            }

            return true;

        }

        private void InitDB()
        {

            this.SQLCONN.Open();

            string script = File.ReadAllText(InitDbsql);

            SQLiteCommand cmd = new SQLiteCommand(script, this.SQLCONN);

            cmd.ExecuteNonQuery();

            this.SQLCONN.Close();

        }

        private void ReInitDB()
        {

            this.SQLCONN.Open();

            string script = File.ReadAllText(RecreateDbsql);

            SQLiteCommand cmd = new SQLiteCommand(script,this.SQLCONN);

            cmd.ExecuteNonQuery();

            this.SQLCONN.Close();

        }

        protected int CountQuery(string query)
        {

            int count = 0;

            bool conopen = false;

            if (this.SQLCONN != null && this.SQLCONN.State == System.Data.ConnectionState.Open)
            {

                conopen = true;

            }

            else
            {

                this.SQLCONN.Open();

            }

            SQLiteCommand command = new SQLiteCommand(query, this.SQLCONN);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                count = reader.GetInt32(0);

            }

            if (!conopen)
            {

                this.SQLCONN.Close();

            }

            return count;

        }

        protected void NonQuery(string nonquery)
        {

            bool conopen = false;

            if (this.SQLCONN != null && this.SQLCONN.State == System.Data.ConnectionState.Open)
            {

                conopen = true;

            }

            else
            {

                this.SQLCONN.Open();

            }

            SQLiteCommand command = new SQLiteCommand(nonquery, this.SQLCONN);
            command.ExecuteNonQuery();

            if (!conopen)
            {

                this.SQLCONN.Close();

            }

        }

        protected virtual bool InTable(int id)
        {

            return false;

        }

    }

}
