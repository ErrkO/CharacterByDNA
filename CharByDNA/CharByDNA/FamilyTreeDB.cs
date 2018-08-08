using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class FamilyTreeDB
    {

        // Desktop Conn
        private string sqlftconn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        private SQLiteConnection sqlConn;

        public FamilyTreeDB()
        {

            this.sqlConn = new SQLiteConnection(sqlftconn);

        }

        public void SetSpouse(int c1id, int c2id)
        {

            string query1 = string.Format("INSERT INTO FamilyTree VALUES ({0},1,{1})",c1id,c2id);
            string query2 = string.Format("INSERT INTO FamilyTree VALUES ({0},1,{1})",c2id,c1id);

            SQLiteCommand command1 = new SQLiteCommand(query1, this.sqlConn);
            SQLiteCommand command2 = new SQLiteCommand(query2, this.sqlConn);
            SQLiteDataReader reader = command1.ExecuteReader();
            reader = command2.ExecuteReader();

        }

        public void SetParent(int p1id, int p2id, int cid)
        {

            string query1 = string.Format("INSERT INTO FamilyTree VALUES ({0},2,{1})", p1id, cid);
            string query2 = string.Format("INSERT INTO FamilyTree VALUES ({0},2,{1})", p2id, cid);

            SQLiteCommand command1 = new SQLiteCommand(query1, this.sqlConn);
            SQLiteCommand command2 = new SQLiteCommand(query2, this.sqlConn);
            SQLiteDataReader reader = command1.ExecuteReader();
            reader = command2.ExecuteReader();

        }

        public List<int> GetChildren(int id)
        {

            List<int> ids = new List<int>();

            string query = string.Format("SELECT * FROM FamilyTree WHERE PID = {0} AND rtID = 2",id);

            SQLiteCommand command = new SQLiteCommand(query, this.sqlConn);
            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {

                ids.Add(reader.GetInt32(2));

            }

            return ids;

        }

        public List<int> GetAllChildren(int id)
        {

            List<int> ids = new List<int>();
            List<int> tempids = GetChildren(id);

            ids.AddRange(tempids);

            while (tempids != null)
            {

                List<int> grandchilds = new List<int>();

                for (int i = 0; i < tempids.Count; i++)
                {

                    if (GetChildren(i) != null)
                    {

                        grandchilds.AddRange(GetChildren(i));

                    }

                }

                if (grandchilds == null)
                {

                    tempids = null;

                }

                else
                {

                    tempids = grandchilds;

                }

                ids.AddRange(tempids);

            }

            return ids;

        }

    }

}
