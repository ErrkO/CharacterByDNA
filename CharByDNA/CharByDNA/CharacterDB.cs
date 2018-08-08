using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class CharacterDB
    {

        public int CID { get; private set; }

        public string Fname { get; private set; }

        public string Lname { get; private set; }

        public DNA Dna { get; private set; }

        public GTime BirthTime { get; private set; }

        // Desktop Conn
        private string sqlcconn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        private SQLiteConnection sqlConn;

        public CharacterDB()
        {

            this.sqlConn = new SQLiteConnection(sqlcconn);

        }

        public CharacterDB(Character character, GTime time)
        {

            this.sqlConn = new SQLiteConnection(sqlcconn);
            this.CID = character.ID;
            this.Fname = character.FirstName;
            this.Lname = character.LastName;
            this.Dna = character.Dna;
            this.BirthTime = time;

        }

        public void SaveCharacter(CharacterDB character)
        {

            this.sqlConn.Open();

            string query = string.Format("INSERT INTO Characters VALUES ({0},{1},{2},{3},{4})",character.CID,character.Fname,character.Lname,character.Dna.ToString(),character.BirthTime.ToString());

            SQLiteCommand command = new SQLiteCommand(query, sqlConn);
            command.ExecuteNonQuery();

        }

        public void SaveAllCharacters(List<CharacterDB> characters)
        {

            foreach(CharacterDB c in characters)
            {

                SaveCharacter(c);

            }

        }

    }

}
