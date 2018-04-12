using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    ///<summary>
    ///
    ///</summary>
    public class Character
    {
        
        ///<summary>
        ///
        ///</summary>
        Names charname = new Names();
        
        ///<summary>
        ///
        ///</summary>
        Race race;

        ///<summary>
        ///
        ///</summary>
        public string FirstName { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string LastName { get; set; }
        
        ///<summary>
        ///
        ///</summary>
        public Race Racee { get; set; }

        ///<summary>
        ///
        ///</summary>
        public bool Gender { get; set; }

        public Attributes Attrib { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Baseattk { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Hptotal { get; private set; }

        ///<summary>
        ///
        ///</summary>
        public int HpCurrent { get; set; }

        ///<summary>
        ///
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

        /*
        ///<summary>
        ///
        ///</summary>
        public Character(string nam, int hp, Race rac, string gen, int s, int i, int a, int c, int w, int l, int ch, int battk)
        {

            FirstName = nam;
            Racee = rac;
            Gender = gen;
            Hptotal = hp;
            HpCurrent = Hptotal;
            Str = s;
            Int = i;
            Dex = a;
            Con = c;
            Wis = w;
            Luk = l;
            Cha = ch;
            Baseattk = battk;

        }
        */

        ///<summary>
        ///
        ///</summary>
        public Character(DNA dna, Character dad = null)
        {

            List<int> allelevalues = dna.GetAllelesvalues();
            List<Allele> alleles = dna.GetAlleles();

            List<string> haircolors = new List<string>() {"Red","Auburn","Brown","Blonde","Black","Grey"};
            List<string> eyecolors = new List<string>() {"Amber","Blue","Brown","Grey","Green","Hazel"};
            List<string> skincolors = new List<string>() {"Albino","Pale","White","Tan","Brown","Black"};

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
            this.HairColor = GetProperty(allelevalues[3],haircolors);

            // allele 5
            this.EyeColor = GetProperty(allelevalues[4],eyecolors);

            // allele 6
            this.SkinColor = GetProperty(allelevalues[5],skincolors);

            // allele 7
            int str = Attrib.GetAbiScore(alleles[0]) + GetScoreMod(allelevalues[6]);

            // allele 8
            int inte = Attrib.GetAbiScore(alleles[1]) + GetScoreMod(allelevalues[7]);

            // allele 9
            int dex = Attrib.GetAbiScore(alleles[3]) + GetScoreMod(allelevalues[8]);

            // allele 10
            int con = Attrib.GetAbiScore(alleles[4]) + GetScoreMod(allelevalues[9]);

            // allele 11
            int wis = Attrib.GetAbiScore(alleles[5]) + GetScoreMod(allelevalues[10]);

            // allele 12
            int luk = Attrib.GetAbiScore(alleles[6]) + GetScoreMod(allelevalues[11]);

            // allele 13
            int cha = Attrib.GetAbiScore(alleles[7]) + GetScoreMod(allelevalues[12]);

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
