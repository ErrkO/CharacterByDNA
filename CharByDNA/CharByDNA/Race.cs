using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    public class Race
    {

        // Desktop Conn
        //private string sqlrconn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        private SQLiteConnection SqlConn;

        private Database DB { get; set; }

        public int RID { get; set; }

        ///<summary>
        /// The property that contains the name of the race
        ///</summary>
        public string Racename { get; set; }

        ///<summary>
        /// The property that contains the base height for males
        ///</summary>
        public int MHeightBase { get; set; }

        ///<summary>
        /// The property that contains the base height for females
        ///</summary>
        public int FHeightBase { get; set; }

        public int Strength { get; set; }

        public int Intelligence { get; set; }

        public int Dexterity { get; set; }

        public int Constitution { get; set; }

        public int Wisdom { get; set; }

        public int Luck { get; set; }

        public int Charisma { get; set; }

        public List<string> HairColors { get; set; }

        public List<string> EyeColors { get; set; }

        public List<string> SkinColors { get; set; }

        public Race(Database db)
        {

            this.DB = db;
            this.SqlConn = db.SQLCONN;

        }

        public Race(Database db, int id, string name, int mhieght, int fhieght, int hcid, int ecid, int scid, int str, int inte, int dex, int con, int wis, int luk, int cha) : this(db)
        {

            this.RID = id;
            this.Racename = name;
            this.MHeightBase = mhieght;
            this.FHeightBase = fhieght;
            this.Strength = str;
            this.Intelligence = inte;
            this.Dexterity = dex;
            this.Constitution = con;
            this.Wisdom = wis;
            this.Luck = luk;
            this.Charisma = cha;
            this.HairColors = GetProperties("HairColor", hcid);
            this.EyeColors = GetProperties("EyeColor", ecid);
            this.SkinColors = GetProperties("SkinColor", scid);

        }

        public Race(Database db, int rid) : this(db)
        {

            Race tempRace = GetRaceByID(rid);

            this.RID = tempRace.RID;
            this.Racename = tempRace.Racename;
            this.MHeightBase = tempRace.MHeightBase;
            this.FHeightBase = tempRace.FHeightBase;
            this.Strength = tempRace.Strength;
            this.Intelligence = tempRace.Intelligence;
            this.Dexterity = tempRace.Dexterity;
            this.Constitution = tempRace.Constitution;
            this.Wisdom = tempRace.Wisdom;
            this.Luck = tempRace.Luck;
            this.Charisma = tempRace.Charisma;
            this.HairColors = tempRace.HairColors;
            this.EyeColors = tempRace.EyeColors;
            this.SkinColors = tempRace.SkinColors;

        }

        private List<Race> Query(string query)
        {

            bool conopen = false;

            if (this.SqlConn != null && this.SqlConn.State == System.Data.ConnectionState.Open)
            {

                conopen = true;

            }

            else
            {

                this.SqlConn.Open();

            }

            List<Race> races = new List<Race>();

            SQLiteCommand command = new SQLiteCommand(query, this.SqlConn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int rid = reader.GetInt32(0);
                string name = reader.GetString(1);
                int mh = reader.GetInt32(2);
                int fh = reader.GetInt32(3);
                int hc = reader.GetInt32(4);
                int ec = reader.GetInt32(5);
                int sc = reader.GetInt32(6);
                int s = reader.GetInt32(7);
                int i = reader.GetInt32(8);
                int d = reader.GetInt32(9);
                int c = reader.GetInt32(10);
                int w = reader.GetInt32(11);
                int l = reader.GetInt32(12);
                int ch = reader.GetInt32(13);

                races.Add(new Race(this.DB,rid,name,mh,fh,hc,ec,sc,s,i,d,c,w,l,ch));

            }

            if (!conopen)
            {

                this.SqlConn.Close();

            }

            return races;

        }

        public Race GetRaceByID(int id)
        {

            if (id < 1 || id > 9)
            {

                id = 9;

            }

            string query = string.Format("SELECT * FROM Race WHERE rID = {0}",id);

            return Query(query)[0];

        }

        public List<Race> GetAllRaces()
        {

            string query = "SELECT * FROM Race";

            return Query(query);

        }

        private List<string> GetProperties(string propname, int ID)
        {

            List<string> property = new List<string>();

            bool conopen = false;

            if (this.SqlConn != null && this.SqlConn.State == System.Data.ConnectionState.Open)
            {

                conopen = true;

            }

            else
            {

                this.SqlConn.Open();

            }

            string id = "";

            if (propname == "HairColor")
            {

                id = "hcID";

            }

            else if (propname == "EyeColor")
            {

                id = "ecID";

            }

            else if (propname == "SkinColor")
            {

                id = "scID";

            }

            string query = "SELECT * FROM " + propname + " WHERE " + id + " = " + ID;

            SQLiteCommand command = new SQLiteCommand(query, this.SqlConn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                property.Add(reader.GetString(1));
                property.Add(reader.GetString(2));
                property.Add(reader.GetString(3));
                property.Add(reader.GetString(4));
                property.Add(reader.GetString(5));
                property.Add(reader.GetString(6));

            }

            if (!conopen)
            {

                this.SqlConn.Close();

            }

            return property;

        }

        ///<summary>
        /// The Overridden ToString method to display a specific race
        ///</summary>
        /*public override string ToString()
        {

            string str = string.Format("{0} Male Base Height: {1} Female Base Height: {2} str: {3} int: {4} agi: {5} con: {6} wis: {7} luk: {8} cha: {9}", this.Racename, this.MHeightBase, this.FHeightBase, this.Attrib.Str_mod, this.Attrib.Int_mod, this.Attrib.Dex_mod, this.Attrib.Con_mod, this.Attrib.Wis_mod, this.Attrib.Luk_mod, this.Attrib.Cha_mod);

            return str;

        }*/

    }

}
