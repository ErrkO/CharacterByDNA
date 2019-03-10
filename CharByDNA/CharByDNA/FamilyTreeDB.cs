using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    struct FTree
    {

        public int Person { get; set; }

        public int Relationship { get; set; }

        public int Relation { get; set; }

        public FTree(int p, int r, int rid)
        {

            this.Person = p;
            this.Relationship = r;
            this.Relation = rid;

        }

    }

    class FamilyTreeDB
    {

        private SQLiteConnection SqlConn { get; set; }
        
        private Database DB { get; set; }

        public FamilyTreeDB(Database db)
        {

            this.DB = db;
            this.SqlConn = db.SQLCONN;

        }

        private List<FTree> Query(string query)
        {

            List<FTree> relations = new List<FTree>();

            this.SqlConn.Open();

            SQLiteCommand command = new SQLiteCommand(query, this.SqlConn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int person = reader.GetInt32(0);
                int rtype = reader.GetInt32(1);
                int relation = reader.GetInt32(2);

                relations.Add(new FTree(person, rtype, relation));

            }

            this.SqlConn.Close();

            return relations;

        }

        private void NonQuery(string nonquery)
        {

            this.SqlConn.Open();

            SQLiteCommand command = new SQLiteCommand(nonquery, this.SqlConn);
            command.ExecuteNonQuery();

            this.SqlConn.Close();

        }

        public List<FTree> GetAllRelationsToID(int id)
        {

            string query = string.Format("SELECT * FROM FamilyTree WHERE Person_ID = {0}",id);
            return Query(query);

        }

        public bool HasSpouse(Character character)
        {

            return HasSpouse(character.CID);

        }

        public bool HasSpouse(int id)
        {

            string query = string.Format("SELECT * FROM FamilyTree WHERE Person_ID = {0} AND Rt_ID = 1", id);

            List<FTree> tree = Query(query);

            if (tree.Count >= 1)
            {

                return true;

            }

            return false;

        }

        public void SetChild(Character parent, Character child)
        {
            
            string nonquery = string.Format("INSERT INTO FamilyTree VALUES ({0},3,{1})",parent.CID,child.CID);
            NonQuery(nonquery);

        }

        public void SetSpouses(Character s1, Character s2)
        {

            string nonquery1 = string.Format("INSERT INTO FamilyTree VALUES ({0},1,{1})",s1.CID,s2.CID);
            string nonquery2 = string.Format("INSERT INTO FamilyTree VALUES ({0},1,{1})", s2.CID, s1.CID);

            NonQuery(nonquery1);
            NonQuery(nonquery2);

        }

        public int GetSpouse(Character character)
        {

            string query = string.Format("SELECT * FROM FamilyTree WHERE Person_ID = {0} AND Rt_ID = 1",character.CID);

            return Query(query)[0].Relation;

        }

        public List<int> GetParents(int id)
        {

            string query = string.Format("SELECT * FROM FamilyTree WHERE Relation_ID = {0} AND Rt_ID = 2",id);

            List<int> parentids = new List<int>();
            List<FTree> qresults = Query(query);

            parentids.Add(qresults[0].Person);
            parentids.Add(qresults[1].Person);

            return parentids;

        }

        public bool HasParent(int id)
        {

            string query = string.Format("SELECT * FROM FamilyTree WHERE Relation_ID = {0} AND Rt_ID = 2",id);

            if (Query(query).Count >= 1)
            {

                return true;

            }

            return false;

        }

        public List<int> GetListOfSingleCharacters(int size)
        {

            List<int> chars = new List<int>();

            for (int i = 0; i < size; i++)
            {

                if (!HasSpouse(i))
                {

                    chars.Add(i);

                }

            }

            return chars;

        }

    }

}
