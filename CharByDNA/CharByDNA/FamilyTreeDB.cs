using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/FTree/*'/>
    struct FTree
    {
        
        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/Person/*'/>
        public int Person { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/Relationship/*'/>
        public int Relationship { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/Relation/*'/>
        public int Relation { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/FTree/*'/>
        public FTree(int p, int r, int rid)
        {

            this.Person = p;
            this.Relationship = r;
            this.Relation = rid;

        }

    }
    
    /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/FamilyTreeDB/*'/>
    class FamilyTreeDB : Database
    {
        
        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/DB/*'/>
        private Database DB { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/FamilyTreeDBC/*'/>
        public FamilyTreeDB(Database db)
        {
            
            this.SQLCONN = db.SQLCONN;
            this.DB = db;

        }
        
        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/Query/*'/>
        private List<FTree> Query(string query)
        {

            List<FTree> relations = new List<FTree>();

            this.SQLCONN.Open();

            SQLiteCommand command = new SQLiteCommand(query, this.SQLCONN);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int person = reader.GetInt32(0);
                int rtype = reader.GetInt32(1);
                int relation = reader.GetInt32(2);

                relations.Add(new FTree(person, rtype, relation));

            }

            this.SQLCONN.Close();

            return relations;

        }

        /*
        private void NonQuery(string nonquery)
        {

            this.SQLCONN.Open();

            SQLiteCommand command = new SQLiteCommand(nonquery, this.SQLCONN);
            command.ExecuteNonQuery();

            this.SQLCONN.Close();

        }
        /* */

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/GetAllRelationsToID/*'/>
        public List<FTree> GetAllRelationsToID(int id)
        {

            string query = string.Format("SELECT * FROM FamilyTree WHERE Person_ID = {0}",id);
            return Query(query);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/HasSpouse1/*'/>
        public bool HasSpouse(Character character)
        {

            return HasSpouse(character.CID);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/HasSpouse2/*'/>
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

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/SetChild1/*'/>
        public void SetChild(Character parent, Character child)
        {
            
            SetChild(parent.CID,child.CID);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/SetChild2/*'/>
        public void SetChild(CharTemp parent, CharTemp child)
        {
            
            SetChild(parent.CID,child.CID);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/SetChild3/*'/>
        public void SetChild(int parentid, int childid)
        {
            
            string nonquery = string.Format("INSERT INTO FamilyTree VALUES ({0},3,{1})",parentid,childid);
            NonQuery(nonquery);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/SetSpouses1/*'/>
        public void SetSpouses(Character spouse1, Character spouse2)
        {

            SetSpouses(spouse1.CID,spouse2.CID);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/SetSpouses2/*'/>
        public void SetSpouses(CharTemp spouse1, CharTemp spouse2)
        {

            SetSpouses(spouse1.CID,spouse2.CID);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/SetSpouses3/*'/>
        public void SetSpouses(int spouse1id, int spouse2id)
        {

            string nonquery1 = string.Format("INSERT INTO FamilyTree VALUES ({0},1,{1})",spouse1id,spouse2id);
            string nonquery2 = string.Format("INSERT INTO FamilyTree VALUES ({0},1,{1})", spouse2id, spouse1id);

            NonQuery(nonquery1);
            NonQuery(nonquery2);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/GetSpouses1/*'/>
        public int GetSpouse(Character character)
        {

            GetSpouse(character.CID);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/GetSpouses2/*'/>
        public int GetSpouse(CharTemp character)
        {

            GetSpouse(character.CID);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/GetSpouses3/*'/>
        public int GetSpouse(int id)
        {

            string query = string.Format("SELECT * FROM FamilyTree WHERE Person_ID = {0} AND Rt_ID = 1",id);

            return Query(query)[0].Relation;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/GetParents/*'/>
        public List<int> GetParents(int id)
        {

            string query = string.Format("SELECT * FROM FamilyTree WHERE Relation_ID = {0} AND Rt_ID = 2",id);

            List<int> parentids = new List<int>();
            List<FTree> qresults = Query(query);

            parentids.Add(qresults[0].Person);
            parentids.Add(qresults[1].Person);

            return parentids;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/HasParent1/*'/>
        public bool HasParent(Character character)
        {

            HasParent(character.CID);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/HasParent2/*'/>
        public bool HasParent(CharTemp character)
        {

            HasParent(character.CID);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/HasParent3/*'/>
        public bool HasParent(int id)
        {

            string query = string.Format("SELECT * FROM FamilyTree WHERE Relation_ID = {0} AND Rt_ID = 2",id);

            if (Query(query).Count >= 1)
            {

                return true;

            }

            return false;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="familytreedb"]/GetListOfSingleCharacters/*'/>
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
