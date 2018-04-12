﻿using System;
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

        public Attributes Attrib { get; set; }

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
        ///<param name="d"> int: dexterity modifier </param>
        ///<param name="c"> int: constitution modifier </param>
        ///<param name="w"> int: wisdom modifier </param>
        ///<param name="l"> int: luck modifier </param>
        ///<param name="ch"> int: charisma modifier </param>
        ///<remarks>
        /// This method should only be used by the class to fill the list of races
        ///</remarks>
        private Race(string name,int mh, int fh, int s, int i, int d, int c, int w, int l, int ch)
        {

            this.Attrib = new Attributes();

            this.Racename = name;
            this.MHeightBase = mh;
            this.FHeightBase = fh;
            this.Attrib.SetMods(s,i,d,c,w,l,ch);

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

            string str = string.Format("{0} Male Base Height: {1} Female Base Height: {2} str: {3} int: {4} agi: {5} con: {6} wis: {7} luk: {8} cha: {9}",this.Racename,this.MHeightBase,this.FHeightBase,this.Attrib.Str_mod,this.Attrib.Int_mod,this.Attrib.Dex_mod,this.Attrib.Con_mod,this.Attrib.Wis_mod,this.Attrib.Luk_mod,this.Attrib.Cha_mod );

            return str;

        }

    }

}
