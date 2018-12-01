using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace CharByDNA
{

    class CharacterDB : Database
    {

        private FamilyTreeDB FamTree { get; set; }

        public CharacterDB(Database db)
        {

            this.SQLCONN = db.SQLCONN;
            this.FamTree = new FamilyTreeDB(db);

        }

        public Character GetCharacter(int id)
        {

            string query = string.Format("SELECT * FROM CharacterDB WHERE CID = {0}", id);

            return Query(query)[0];

        }

        private List<Character> Query(string query)
        {

            List<Character> characters = new List<Character>();

            bool conopen = false;

            if (this.SQLCONN != null && this.SQLCONN.State == System.Data.ConnectionState.Open)
            {

                conopen = true;

            }

            else
            {

                this.SQLCONN.Open();

            }

            SQLiteCommand command = new SQLiteCommand(query, SQLCONN);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int cid = reader.GetInt32(0);
                string fname = reader.GetString(1);
                string lname = reader.GetString(2);
                string dna = reader.GetString(3);
                int gender = reader.GetInt32(4);
                double btime = reader.GetDouble(5);
                double dtime = reader.GetDouble(6);
                int issingle = reader.GetInt32(7);
                int dead = reader.GetInt32(8);

                characters.Add(new Character(this, cid, fname, lname, dna, Convert.ToBoolean(gender), btime, dtime, Convert.ToBoolean(issingle), Convert.ToBoolean(dead)));

            }

            if (!conopen)
            {

                this.SQLCONN.Close();

            }

            return characters;

        }

        public List<Character> GetAllCharacters()
        {

            string query = "SELECT * FROM CharacterDb";

            return Query(query);

        }

        public List<Character> FillListWithViableCharacters(GTime time)
        {

            List<int> ids = this.FamTree.GetListOfSingleCharacters(NumOfRowsInTable());

            string query = string.Format("SELECT * FROM CharacterDb WHERE Dead = 0 AND ({0} - BirthTime) / 10000 >= 18 AND (", time.ToDouble());

            query += "IsSingle = 1 OR ";

            query += string.Format("DueDate = {0} OR (DueDate < 0 AND Gender = 0))", time.ToDouble());

            List<Character> chars = new List<Character>();

            chars = Query(query);

            return chars;

        }

        public void SaveCharacter(Character character)
        {

            if (InTable(character.CID))
            {

                UpdateCharacter(character);

            }

            else
            {

                InsertCharacter(character);

            }

        }

        public void SaveListOfCharacters(List<Character> characters)
        {

            foreach(Character c in characters)
            {

                SaveCharacter(c);

            }

        }

        private void InsertCharacter(Character character)
        {

            string nonquery = string.Format("INSERT INTO CharacterDB VALUES ({0},\'{1}\',\'{2}\',\'{3}\',{4},{5},{6},{7},{8})", character.CID, character.Fname, character.Lname, character.Dna.ToString(),
                Convert.ToInt32(character.Gender), character.BirthTime.ToDouble(), character.DueDate.ToDouble(), Convert.ToInt32(character.IsSingle), Convert.ToInt32(character.Dead));

            NonQuery(nonquery);

        }

        private void UpdateCharacter(Character character)
        {

            string query = string.Format("UPDATE CharacterDB SET Lname = \'{0}\', DueDate = {1}, IsSingle = {2}, Dead = {3} WHERE CID = {4}", character.Lname, character.DueDate.ToDouble(),
                        Convert.ToInt32(character.IsSingle), Convert.ToInt32(character.Dead), character.CID);

            NonQuery(query);

        }

        public int GetNumberOfAliveCharacters()
        {

            string query = "SELECT COUNT(*) FROM CharacterDB WHERE Dead = 0";

            return CountQuery(query);

        }

        public int GetNumberOfDeadCharacters()
        {

            string query = "SELECT COUNT(*) FROM CharacterDB WHERE Dead = 1";

            return CountQuery(query);

        }

        public int GetNumberOfSingleCharacters()
        {

            string query = "SELECT COUNT(*) FROM CharacterDB WHERE IsSingle = 1";

            return CountQuery(query);

        }

        public override int NumOfRowsInTable()
        {

            string query = "SELECT Count(*) FROM CharacterDB";

            return CountQuery(query);

        }

        protected override bool InTable(int id)
        {

            string query = string.Format("SELECT * FROM CharacterDB WHERE CID = {0}", id);

            List<Character> chars = Query(query);

            if (chars.Count > 0)
            {

                return true;

            }

            return false;

        }

    }

}
