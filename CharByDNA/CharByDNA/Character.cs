﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    ///<summary>
    /// The class that contains the information for a character
    ///</summary>
    public class Character
    {
        
        ///<summary>
        /// The Names class object
        ///</summary>
        Names charname = new Names();
        
        ///<summary>
        /// The Race class object
        ///</summary>
        Race race;

        ///<summary>
        /// The property that holds the first name
        ///</summary>
        public string FirstName { get; set; }

        ///<summary>
        /// The property that holds the last name
        ///</summary>
        public string LastName { get; set; }
        
        ///<summary>
        /// The property that holds the Race class object
        ///</summary>
        public Race Racee { get; set; }

        ///<summary>
        /// The property that holds the gender value
        ///</summary>
        ///<example>
        /// True = male
        ///</example>
        public bool Gender { get; set; }

        ///<summary>
        /// The property that holds the Attributes object
        ///</summary>
        public Attributes Attrib { get; set; }

        ///<summary>
        /// The property that holds the base attack value
        ///</summary>
        public int Baseattk { get; set; }

        ///<summary>
        /// The property that holds the health point total of the character
        ///</summary>
        public int Hptotal { get; private set; }

        ///<summary>
        /// The Property that holds the current amount of health points
        ///</summary>
        public int HpCurrent { get; set; }

        ///<summary>
        /// The property that holds the DNA class object
        ///</summary>
        public DNA Dna { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public int Height { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string HairColor { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string EyeColor { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string SkinColor { get; set; }

        ///<summary>
        ///
        ///</summary>
        public Character()
        {

            ;

        }

        ///<summary>
        ///
        ///</summary>
        public Character(DNA dna, Character dad = null)
        {

            List<int> allelevalues = dna.GetAllelesvalues();
            List<Allele> alleles = dna.GetAlleles();

            this.Dna = dna;

            race = new Race();
            Attrib = new Attributes();

            // allele 1
            if (allelevalues[0] > 20)
            {

                this.Gender = false;

            }

            else
            {

                this.Gender = true;

            }

            // allele 2
            this.Racee = race.Races[0];

            // allele 3
            if (this.Gender)
            {

                this.Height = this.Racee.MHeightBase + allelevalues[2];

            }

            else
            {

                this.Height = this.Racee.FHeightBase + allelevalues[2];

            }

            // allele 4
            this.HairColor = GetProperty(allelevalues[3], race.HairColors);

            // allele 5
            this.EyeColor = GetProperty(allelevalues[4],race.EyeColors);

            // allele 6
            this.SkinColor = GetProperty(allelevalues[5],race.SkinColors);

            // allele 7
            int str = Attrib.GetAbiScore(alleles[0]) + GetScoreMod(allelevalues[6]) + race.Attrib.Str_mod;

            // allele 8
            int inte = Attrib.GetAbiScore(alleles[1]) + GetScoreMod(allelevalues[7]) + race.Attrib.Int_mod;

            // allele 9
            int dex = Attrib.GetAbiScore(alleles[3]) + GetScoreMod(allelevalues[8]) + race.Attrib.Dex_mod;

            // allele 10
            int con = Attrib.GetAbiScore(alleles[4]) + GetScoreMod(allelevalues[9]) + race.Attrib.Con_mod;

            // allele 11
            int wis = Attrib.GetAbiScore(alleles[5]) + GetScoreMod(allelevalues[10]) + race.Attrib.Wis_mod;

            // allele 12
            int luk = Attrib.GetAbiScore(alleles[6]) + GetScoreMod(allelevalues[11]) + race.Attrib.Luk_mod;

            // allele 13
            int cha = Attrib.GetAbiScore(alleles[7]) + GetScoreMod(allelevalues[12]) + race.Attrib.Cha_mod;

            Attrib.SetStats(str,inte,dex,con,wis,luk,cha);

            this.FirstName = charname.GenFname(this.Gender);

            if (dad != null)
            {

                this.LastName = dad.LastName;

            }

            else
            {

                this.LastName = charname.GenLname();

            }  

        }

        ///<summary>
        /// This constructor generates a new Character based on the given gender
        ///</summary>
        ///<param name="gender"> bool: True is male </param>
        public Character(bool gender)
        {

            this.Dna = new DNA(gender);
            race = new Race();
            this.Attrib = new Attributes();

            List<int> allelevalues = this.Dna.GetAllelesvalues();
            List<Allele> alleles = this.Dna.GetAlleles();

            // allele 1
            if (allelevalues[0] > 20)
            {

                this.Gender = false;

            }

            else
            {

                this.Gender = true;

            }

            if (this.Gender != gender)
            {

                Console.WriteLine("I should never happen");

            }

            // allele 2
            this.Racee = race.Races[0];

            // allele 3
            if (this.Gender)
            {

                this.Height = this.Racee.MHeightBase + allelevalues[2];

            }

            else
            {

                this.Height = this.Racee.FHeightBase + allelevalues[2];

            }

            // allele 4
            this.HairColor = GetProperty(allelevalues[3],race.HairColors);

            // allele 5
            this.EyeColor = GetProperty(allelevalues[4],race.EyeColors);

            // allele 6
            this.SkinColor = GetProperty(allelevalues[5],race.SkinColors);

            // allele 7
            int str = Attrib.GetAbiScore(alleles[0]) + GetScoreMod(allelevalues[6]) + race.Attrib.Str_mod;

            // allele 8
            int inte = Attrib.GetAbiScore(alleles[1]) + GetScoreMod(allelevalues[7]) + race.Attrib.Int_mod;

            // allele 9
            int dex = Attrib.GetAbiScore(alleles[3]) + GetScoreMod(allelevalues[8]) + race.Attrib.Dex_mod;

            // allele 10
            int con = Attrib.GetAbiScore(alleles[4]) + GetScoreMod(allelevalues[9]) + race.Attrib.Con_mod;

            // allele 11
            int wis = Attrib.GetAbiScore(alleles[5]) + GetScoreMod(allelevalues[10]) + race.Attrib.Wis_mod;

            // allele 12
            int luk = Attrib.GetAbiScore(alleles[6]) + GetScoreMod(allelevalues[11]) + race.Attrib.Luk_mod;

            // allele 13
            int cha = Attrib.GetAbiScore(alleles[7]) + GetScoreMod(allelevalues[12]) + race.Attrib.Cha_mod;

            Attrib.SetStats(str,inte,dex,con,wis,luk,cha);

            this.FirstName = charname.GenFname(this.Gender);

            this.LastName = charname.GenLname();


        }

        ///<summary>
        ///
        ///</summary>
        public string GetProperty(int value, List<string> properties)
        {

            if (value <= 14)
            {

                return properties[0];

            }

            else if (value <= 17)
            {

                return properties[1];

            }

            else if (value <= 21)
            {

                return properties[2];
                
            }

            else if (value <= 24)
            {

                return properties[3];
                
            }

            else if (value <= 27)
            {

                return properties[4];
                
            }

            else if (value <= 32)
            {

                return properties[5];
                
            }

            return null;

        }

        ///<summary>
        ///
        ///</summary>
        public int GetScoreMod(int value)
        {

            if (value % 7 == 3)
            {

                return -3;

            }

            else if (value % 7 == 2)
            {

                return -2;

            }

            else if (value % 7 == 1)
            {

                return -1;

            }

            else if (value % 7 == 0)
            {

                return 0;

            }

            else if (value % 7 == 6)
            {

                return 1;

            }

            else if (value % 7 == 5)
            {

                return 2;

            }

            else if (value % 7 == 4)
            {

                return 3;

            }

            return -100;

        }

        ///<summary>
        ///
        ///</summary>
        public string HeightToString()
        {

            int ft; 
            int inch;

            ft = Convert.ToInt32(Math.Floor(Convert.ToDouble(this.Height/12)));

            inch = this.Height % 12;

            string str = string.Format("{0}\' {1}\"",ft,inch);

            return str;

        }

        ///<summary>
        ///
        ///</summary>
        public override string ToString()
        {

            string gen = "";

            if (this.Gender)
            {

                gen = "male";

            }

            else
            {

                gen = "female";

            }

            string ret = string.Format("{0} {1} is a {2} of the {3} race with a height of {4}, {5} hair, {6} eyes, {7} skin color,str: {8}, int: {9}, agi: {10}, con: {11}, wis: {12}, luk: {13}, cha: {14}", this.FirstName, this.LastName, gen, this.Racee.Racename, HeightToString(), this.HairColor, this.EyeColor, this.SkinColor, this.Attrib.Str, this.Attrib.Int, this.Attrib.Dex, this.Attrib.Con, this.Attrib.Wis, this.Attrib.Luk, this.Attrib.Cha);
            return ret;

        }

    }

}
