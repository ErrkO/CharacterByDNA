using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private List<CharacterDB> CQuery(string query)
        {

            List<CharacterDB> characters = new List<CharacterDB>();

            this.SQLCONN.Open();

            SQLiteCommand command = new SQLiteCommand(query, this.SQLCONN);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int cid = reader.GetInt32(0);
                string fname = reader.GetString(1);
                string lname = reader.GetString(2);
                string dna = reader.GetString(3);
                bool gender = reader.GetBoolean(4);
                double btime = reader.GetDouble(5);
                double dtime = reader.GetDouble(6);
                bool dead = reader.GetBoolean(7);

                characters.Add(new CharacterDB(this,cid, fname, lname, dna, gender, btime, dtime, dead));

            }

            this.SQLCONN.Close();

            return characters;

        }

        public List<CharacterDB> FillListWithViableCharacters(GTime time)
        {

            string bquery = "SELECT * FROM CharacterDB WHERE Dead = false";

            string query = string.Format(bquery + " AND DueDate = {0}",time.ToDouble());
            string query2 = string.Format(bquery + " AND BirthTime + 180000 >= {0} AND Gender = true",time.ToDouble());

            List<CharacterDB> chars = new List<CharacterDB>();

            chars = CQuery(query);
            chars.AddRange(CQuery(query2));

            return chars;

        }

        public void SaveListOfCharacters(List<CharacterDB> chars)
        {

            this.SQLCONN.Open();

            foreach (CharacterDB c in chars)
            {

                string query = string.Format("INSERT INTO CharacterDB VALUES ({0},\'{1}\',\'{2}\',\'{3}\',{4},{5},{6},{7})", c.CID, c.Fname, c.Lname, c.Dna.ToString(),
                    Convert.ToInt32(c.Gender), c.BirthTime.ToDouble(), c.DueDate.ToDouble(), Convert.ToInt32(c.Dead));

                SQLiteCommand command = new SQLiteCommand(query, this.SQLCONN);
                command.ExecuteNonQuery();

            }

            this.SQLCONN.Close();

        }

    }

}
