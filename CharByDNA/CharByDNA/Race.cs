using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        ///<summary>
        /// The property that contains the intelligence modifier
        ///</summary>
        public int Int_mod { get; set; }

        ///<summary>
        /// The property that contains the strength modifier
        ///</summary>
        public int Str_mod { get; set; }

        ///<summary>
        /// The property that contains the agility modifier
        ///</summary>
        public int Agi_mod { get; set; }

        ///<summary>
        /// The property that contains the constitution modifier
        ///</summary>
        public int Con_mod { get; set; }

        ///<summary>
        /// The property that contains the wisdon modifier
        ///</summary>
        public int Wis_mod { get; set; }

        ///<summary>
        /// The property that contains the luck modifier
        ///</summary>
        public int Luk_mod { get; set; }

        ///<summary>
        /// The property that contains the charisma modifier
        ///</summary>
        public int Cha_mod { get; set; }

        ///<summary>
        /// The property that contains the list of all races
        ///</summary>
        public List<Race> Races { get; set; }

        ///<summary>
        /// The default constructor for the race class. It creates a list of all the races and stores that information
        ///</summary>
        public Race()
        {
            
            this.Races = GetRaces();

        }

        ///<summary>
        /// The Constructor that takes the different parameters and creates a race object
        ///</summary>
        ///<param name="name"> string: the race name </param>
        ///<param name="mh"> int: the male base height </param>
        ///<param name="fh"> int: the female base height </param>
        ///<param name="s"> int: strength modifier </param>
        ///<param name="i"> int: intelligence modifier </param>
        ///<param name="a"> int: agility modifier </param>
        ///<param name="c"> int: constitution modifier </param>
        ///<param name="w"> int: wisdom modifier </param>
        ///<param name="l"> int: luck modifier </param>
        ///<param name="ch"> int: charisma modifier </param>
        ///<remarks>
        /// This method should only be used by the class to fill the list of races
        ///</remarks>
        private Race(string name,int mh, int fh, int s, int i, int a, int c, int w, int l, int ch)
        {

            this.Racename = name;
            this.MHeightBase = mh;
            this.FHeightBase = fh;
            this.Int_mod = i;
            this.Str_mod = s;
            this.Agi_mod = a;
            this.Con_mod = c;
            this.Wis_mod = w;
            this.Luk_mod = l;
            this.Cha_mod = ch;

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
                int mh, fh, s, i, a, w, c, l, ch;
                string[] parts = line.Split(',');
                name = parts[0];
                mh = Convert.ToInt32(parts[1]);
                fh = Convert.ToInt32(parts[2]);
                s = Convert.ToInt32(parts[3]);
                i = Convert.ToInt32(parts[4]);
                a = Convert.ToInt32(parts[5]);
                c = Convert.ToInt32(parts[6]);
                w = Convert.ToInt32(parts[7]);
                l = Convert.ToInt32(parts[8]);
                ch = Convert.ToInt32(parts[9]);

                races.Add(new Race(name, mh, fh, s, i, a, c, w, l, ch));

            }

            return races;

        }

        ///<summary>
        /// The Overridden ToString method to display a specific race
        ///</summary>
        public override string ToString()
        {

            string str = string.Format("{0} Male Base Height: {1} Female Base Height: {2} str: {3} int: {4} agi: {5} con: {6} wis: {7} luk: {8} cha: {9}",this.Racename,this.MHeightBase,this.FHeightBase,this.Str_mod,this.Int_mod,this.Agi_mod,this.Con_mod,this.Wis_mod,this.Luk_mod,this.Cha_mod );

            return str;

        }

    }

}
