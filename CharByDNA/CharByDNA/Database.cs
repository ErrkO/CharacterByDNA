using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace CharByDNA
{

    /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/Database/*'/>
    class Database
    {

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/BadChars/*'/>
        private char[] badchars = {' ','/','>','<','?','"',':','\\','|','*'};

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/Extension/*'/>
        private string extension = ".db";

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/Version/*'/>
        private string version = "Version=3";

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/UncompleteConn/*'/>
        private string UncompleteConn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\";

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/RecreateDbSql/*'/>
        private string RecreateDbsql = "D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\RecreateDB.sql";

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/InitDbSql/*'/>
        private string InitDbsql = "D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\InitializeDB.sql";

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/SqlConn/*'/>
        private string sqlconn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/SQLCONN/*'/>
        public SQLiteConnection SQLCONN { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/DbName/*'/>
        public string DbName { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/DatabaseC1/*'/>
        public Database()
        {

            this.SQLCONN = new SQLiteConnection(sqlconn);
            this.DbName = "Game.db";

            ReInitDB();

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/DatabaseC2/*'/>
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

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/NumOfRowsInTable/*'/>
        public virtual int NumOfRowsInTable()
        {

            return -1;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/CheckExtension/*'/>
        private bool CheckExtension(string str)
        {

            if (str.Contains(extension))
            {

                return true;

            }

            return false;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/CheckFilename/*'/>
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

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/InitDB/*'/>
        private void InitDB()
        {

            this.SQLCONN.Open();

            string script = File.ReadAllText(InitDbsql);

            SQLiteCommand cmd = new SQLiteCommand(script, this.SQLCONN);

            cmd.ExecuteNonQuery();

            this.SQLCONN.Close();

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/ReInitDB/*'/>
        private void ReInitDB()
        {

            this.SQLCONN.Open();

            string script = File.ReadAllText(RecreateDbsql);

            SQLiteCommand cmd = new SQLiteCommand(script,this.SQLCONN);

            cmd.ExecuteNonQuery();

            this.SQLCONN.Close();

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/CountQuery/*'/>
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

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/NonQuery/*'/>
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

        /// <include file='Documentation.xml' path='Documentation/members[@name="database"]/InTable/*'/>
        protected virtual bool InTable(int id)
        {

            return false;

        }

    }

}
