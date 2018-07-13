using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace CharByDNA
{

    ///<summary>
    /// The Race class contains all the information for the different races
    ///</summary>
    public class Race
    {

        ///<summary>
        /// Contains the filename for the race information
        ///</sumamry>
        private string rconn = "Races.txt";

        // laptop Conn
        //private string sqlrconn = "URI=file:C:\\Users\\erico\\Documents\\Github\\CharacterByDNA\\Database\\Game.db;Version=3";

        // Desktop Conn
        private string sqlrconn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        private SQLiteConnection sqlConn;

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

        public Attributes Attrib { get; set; }

        ///<summary>
        /// The property that contains the list of all races
        ///</summary>
        public List<Race> Races { get; set; }

        public List<string> HairColors { get; set; }


        public List<string> EyeColors { get; set; }


        public List<string> SkinColors { get; set; }

        ///<summary>
        /// The default constructor for the race class. It creates a list of all the races and stores that information
        ///</summary>
        public Race()
        {

            this.sqlConn = new SQLiteConnection(sqlrconn);
            
            this.Races = GetRacesByDB();

        }

        ///<summary>
        /// The Constructor that takes the different parameters and creates a race object
        ///</summary>
        ///<param name="name"> string: the race name </param>
        ///<param name="mh"> int: the male base height </param>
        ///<param name="fh"> int: the female base height </param>
        ///<param name="s"> int: strength modifier </param>
        ///<param name="i"> int: intelligence modifier </param>
        ///<param name="d"> int: dexterity modifier </param>
        ///<param name="c"> int: constitution modifier </param>
        ///<param name="w"> int: wisdom modifier </param>
        ///<param name="l"> int: luck modifier </param>
        ///<param name="ch"> int: charisma modifier </param>
        ///<remarks>
        /// This method should only be used by the class to fill the list of races
        ///</remarks>
        private Race(string name,int mh, int fh, int hc, int ec, int sc, int s, int i, int d, int c, int w, int l, int ch)
        {

            this.Attrib = new Attributes();
            this.sqlConn = new SQLiteConnection(sqlrconn);

            this.Racename = name;
            this.MHeightBase = mh;
            this.FHeightBase = fh;
            this.Attrib.SetMods(s,i,d,c,w,l,ch);
            this.HairColors = GetProperties("HairColor",hc);
            this.EyeColors = GetProperties("EyeColor",ec);
            this.SkinColors = GetProperties("SkinColor",sc);

        }

        ///<summary>
        /// This method reads in the filename and creates a list of all the race information
        ///</summary>
        ///<returns>
        /// returns a list containing all of the races
        ///</returns>
        private List<Race> GetRaces()
        {

            List<Race> races = new List<Race>();

            string line = "";

            StreamReader file = new StreamReader(rconn);

            while ((line = file.ReadLine()) != null)
            {

                string name;
                int mh, fh, s, i, d, w, c, l, ch;
                string[] parts = line.Split(',');
                name = parts[0];
                mh = Convert.ToInt32(parts[1]);
                fh = Convert.ToInt32(parts[2]);
                s = Convert.ToInt32(parts[3]);
                i = Convert.ToInt32(parts[4]);
                d = Convert.ToInt32(parts[5]);
                c = Convert.ToInt32(parts[6]);
                w = Convert.ToInt32(parts[7]);
                l = Convert.ToInt32(parts[8]);
                ch = Convert.ToInt32(parts[9]);

                races.Add(new Race(name, mh, fh, 0, 0, 0,s, i, d, c, w, l, ch));

            }

            return races;

        }

        private List<Race> GetRacesByDB()
        {

            List<Race> races = new List<Race>();

            this.sqlConn.Open();
            string query = "SELECT * FROM Race";

            SQLiteCommand command = new SQLiteCommand(query,sqlConn);
            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                
                string name;
                int id, mh, fh, s, i, d, w, c, l, ch, hc, ec, sc;

                id = reader.GetInt32(0);
                name = reader.GetString(1);
                mh = reader.GetInt32(2);
                fh = reader.GetInt32(3);
                hc = reader.GetInt32(4);
                ec = reader.GetInt32(5);
                sc = reader.GetInt32(6);
                s = reader.GetInt32(7);
                i = reader.GetInt32(8);
                d = reader.GetInt32(9);
                c = reader.GetInt32(10);
                w = reader.GetInt32(11);
                l = reader.GetInt32(12);
                ch = reader.GetInt32(13);

                races.Add(new Race(name, mh, fh, hc, ec, sc, s, i, d, c, w, l, ch));

            }

            sqlConn.Close();

            return races;

        }

        private List<string> GetProperties(string propname, int ID)
        {

            List<string> property = new List<string>();

            this.sqlConn.Open();

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

            SQLiteCommand command = new SQLiteCommand(query,sqlConn);
            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                
                property.Add(reader.GetString(1));
                property.Add(reader.GetString(2));
                property.Add(reader.GetString(3));
                property.Add(reader.GetString(4));
                property.Add(reader.GetString(5));
                property.Add(reader.GetString(6));

            }

            sqlConn.Close();

            return property;

        }

        ///<summary>
        /// The Overridden ToString method to display a specific race
        ///</summary>
        public override string ToString()
        {

            string str = string.Format("{0} Male Base Height: {1} Female Base Height: {2} str: {3} int: {4} agi: {5} con: {6} wis: {7} luk: {8} cha: {9}",this.Racename,this.MHeightBase,this.FHeightBase,this.Attrib.Str_mod,this.Attrib.Int_mod,this.Attrib.Dex_mod,this.Attrib.Con_mod,this.Attrib.Wis_mod,this.Attrib.Luk_mod,this.Attrib.Cha_mod );

            return str;

        }

    }

}
