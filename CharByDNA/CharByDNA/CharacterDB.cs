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

        // Desktop Conn
        private string sqlcconn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        private SQLiteConnection sqlConn;

        public CharacterDB()
        {

            this.sqlConn = new SQLiteConnection(sqlcconn);

        }

        public void SaveCharacter(Character character)
        {

            string query = string.Format("INSERT INTO Characters VALUES ()",);

        }

        public void SaveAllCharacters(List<Character> characters)
        {



        }

    }

}
