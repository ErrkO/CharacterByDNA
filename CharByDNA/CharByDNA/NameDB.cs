using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class NameDB
    {

        Random rngesus = new Random(Guid.NewGuid().GetHashCode());

        // Desktop Conn
        private string sqlrconn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        private SQLiteConnection SqlConn;

        public NameDB(Database db)
        {

            this.SqlConn = db.SQLCONN;

        }

        private List<string> Query(string query)
        {

            this.SqlConn = new SQLiteConnection(sqlrconn);

            List<string> names = new List<string>();

            this.SqlConn.Open();

            SQLiteCommand command = new SQLiteCommand(query, this.SqlConn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                //int id = reader.GetInt32(0);
                string name = reader.GetString(0);

                names.Add(name);

            }

            this.SqlConn.Close();

            return names;

        }

        private string GetFName(int id)
        {

            string query = string.Format("SELECT Fname FROM FNames WHERE FNID = {0}",id);

            return Query(query)[0];

        }

        private string GetMName(int id)
        {

            string query = string.Format("SELECT Mname FROM MNames WHERE MNID = {0}", id);

            return Query(query)[0];

        }

        private string GetLName(int id)
        {

            string query = string.Format("SELECT Lname FROM LNames WHERE LNID = {0}", id);

            return Query(query)[0];

        }

        public string GenFname(bool gender)
        {

            int id;

            if (gender)
            {

                id = rngesus.Next(1, 6289);

                return GetMName(id);

            }

            else
            {

                id = rngesus.Next(1, 4392);

                return GetFName(id);

            }

        }

        public string GenLname()
        {

            int id = rngesus.Next(1,10446);

            return GetLName(id);

        }

    }

}
