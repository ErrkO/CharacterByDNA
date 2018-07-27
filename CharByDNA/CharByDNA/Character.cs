using System;
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

        public FamilyTree Family { get; private set; }

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
        public Character(DNA dna)
        {

            List<int> genevals = dna.GetGeneValues();
            List<Gene> genes = dna.Genes;

            this.Family = new FamilyTree(this);

            this.Dna = dna;

            race = new Race();
            Attrib = new Attributes();

            // Gene Pair 1
            int gone = genevals[0] % 2;
            int gtwo = genevals[1] % 2;

            if (gone+gtwo == 1)
            {

                this.Gender = true;

            }

            else if (gone+gtwo == 2)
            {

                this.Gender = false;

            }

            else
            {

                Console.WriteLine("This is an error");

            }

            // Gene Pair 2
            this.Racee = race.Races[0];

            //this.Racee = GetRace(race.Races,allelevalues[1]);

            // Gene Pair 3
            if (this.Gender)
            {

                this.Height = this.Racee.MHeightBase + genevals[4] + genevals[5];

            }

            else
            {

                this.Height = this.Racee.FHeightBase + genevals[4] + genevals[5];

            }

            // Gene Pair 4
            this.HairColor = GetProperty(genevals[6], genevals[7], this.Racee.HairColors);

            // Gene Pair 5
            this.EyeColor = GetProperty(genevals[8], genevals[9], this.Racee.EyeColors);

            // Gene Pair 6
            this.SkinColor = GetProperty(genevals[10], genevals[11], this.Racee.SkinColors);

            // Gene Pair 7
            int strtemp = (int)Math.Floor((double)(genevals[12] + genevals[13]) / 2);

            int str = strtemp + this.Racee.Attrib.Str_mod;

            // Gene Pair 8
            int inttemp = (int)Math.Floor((double)(genevals[14] + genevals[15]) / 2);

            int inte = inttemp + this.Racee.Attrib.Int_mod;

            // Gene Pair 9
            int dextemp = (int)Math.Floor((double)(genevals[16] + genevals[17]) / 2);

            int dex = dextemp + this.Racee.Attrib.Dex_mod;

            // Gene Pair 10
            int contemp = (int)Math.Floor((double)(genevals[18] + genevals[19]) / 2);

            int con = contemp + this.Racee.Attrib.Con_mod;

            // Gene Pair 11
            int wistemp = (int)Math.Floor((double)(genevals[20] + genevals[21]) / 2);

            int wis = wistemp + this.Racee.Attrib.Wis_mod;

            // Gene Pair 12
            int luktemp = (int)Math.Floor((double)(genevals[22] + genevals[23]) / 2);

            int luk = luktemp + this.Racee.Attrib.Luk_mod;

            // Gene Pair 13
            int chatemp = (int)Math.Floor((double)(genevals[24] + genevals[25]) / 2);

            int cha = chatemp + this.Racee.Attrib.Cha_mod;

            Attrib.SetStats(str,inte,dex,con,wis,luk,cha);

            this.FirstName = charname.GenFname(this.Gender);

            this.LastName = charname.GenLname();

        }

        public Character(Character dad, Character mom) : this(new DNA(dad.Dna.Miosis(),mom.Dna.Miosis()))
        {

            this.FirstName = charname.GenFname(this.Gender);
            this.LastName = dad.LastName;
            this.Family.SetDad(dad);
            this.Family.SetMom(mom);
            dad.Family.AddChild(this);
            mom.Family.AddChild(this);

        }

        ///<summary>
        /// This constructor generates a new Character based on the given gender
        ///</summary>
        ///<param name="gender"> bool: True is male </param>
        public Character(bool gender) : this(new DNA(gender))
        {

            this.FirstName = charname.GenFname(this.Gender);
            this.LastName = charname.GenLname();

        }

        ///<summary>
        ///
        ///</summary>
        public string GetProperty(int value, int valuetwo, List<string> properties)
        {

            int vused;

            if (value >= valuetwo)
            {

                vused = value;

            }

            else
            {

                vused = valuetwo;

            }

            if (vused >= 1 && vused <= 3)
            {

                return properties[1];

            }

            else if (vused >= 4 && vused <= 6)
            {

                return properties[2];

            }

            else if (vused >= 5 && vused <= 9)
            {

                return properties[3];
                
            }

            else if (vused >= 10 && vused <= 12)
            {

                return properties[4];
                
            }

            else if (vused >= 13 && vused <= 15)
            {

                return properties[5];
                
            }

            else if (vused == 16)
            {

                return properties[0];
                
            }

            return null;

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
