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
        Random rngesus = new Random();
        
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
        public string Gender { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Str { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Int { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Agi { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Con { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Wis {get; set;}

        ///<summary>
        ///
        ///</summary>
        public int Luk { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Cha { get; set; }

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

            string tempname,g;
            int s, i, a, c, w, l, ch;
            int temp = rngesus.Next(0, 2);

            if (temp == 0)
            {

                g = "Female";
                tempname = charname.GenFname(g);

            }

            else
            {

                g = "Male";
                tempname = charname.GenFname(g);

            }

            Race rac = new Race();
            //List<Race> races = rac.GetRaces();

            int rnum = rngesus.Next(1, rac.Races.Count);

            s = GetAbiScore() + rac.Races[rnum].Str_mod;
            i = GetAbiScore() + rac.Races[rnum].Int_mod;
            a = GetAbiScore() + rac.Races[rnum].Agi_mod;
            c = GetAbiScore() + rac.Races[rnum].Con_mod;
            w = GetAbiScore() + rac.Races[rnum].Wis_mod;
            l = GetAbiScore() + rac.Races[rnum].Luk_mod;
            ch = GetAbiScore() + rac.Races[rnum].Cha_mod;

            int hp = 10 + GetMod(c);



        }

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
            Agi = a;
            Con = c;
            Wis = w;
            Luk = l;
            Cha = ch;
            Baseattk = battk;

        }

        ///<summary>
        ///
        ///</summary>
        public Character(DNA dna, Character dad = null)
        {

            List<int> allelevalues = dna.GetAllelesvalues();
            List<Allele> alleles = dna.GetAlleles();
            int sum = 0;

            List<string> haircolors = new List<string>() {"Red","Auburn","Brown","Blonde","Black","Grey"};
            List<string> eyecolors = new List<string>() {"Amber","Blue","Brown","Grey","Green","Hazel"};
            List<string> skincolors = new List<string>() {"Albino","Pale","White","Tan","Brown","Black"};

            this.Dna = dna;

            race = new Race();

            // allele 1
            if (allelevalues[0] > 20)
            {

                this.Gender = "Female";

            }

            else
            {

                this.Gender = "Male";

            }

            // allele 2
            this.Racee = race.Races[0];

            // allele 3
            if (this.Gender == "Male")
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
            this.Str = GetAbiScore(alleles[0]) + GetScoreMod(allelevalues[6]);

            // allele 8
            this.Int = GetAbiScore(alleles[1]) + GetScoreMod(allelevalues[7]);

            // allele 9
            this.Agi = GetAbiScore(alleles[3]) + GetScoreMod(allelevalues[8]);

            // allele 10
            this.Con = GetAbiScore(alleles[4]) + GetScoreMod(allelevalues[9]);

            // allele 11
            this.Wis = GetAbiScore(alleles[5]) + GetScoreMod(allelevalues[10]);

            // allele 12
            this.Luk = GetAbiScore(alleles[6]) + GetScoreMod(allelevalues[11]);

            // allele 13
            this.Cha = GetAbiScore(alleles[7]) + GetScoreMod(allelevalues[12]);

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
        public int GetAbiScore()
        {

            List<int> dicerolls = new List<int>();

            int minus = 0;
            bool found = false;
            int total = 0;

            for (int i = 0; i < 4; i++)
            {

                dicerolls.Add(rngesus.Next(1, 7));
                total = total + dicerolls[i];

            }

            while(found != true)
            {

                minus = dicerolls[0];

                for (int i = 1; i < 4; i++)
                {

                    if (minus > dicerolls[i])
                    {

                        minus = dicerolls[i];

                    }

                }

                found = true;

            }

            return total - minus;

        }

        ///<summary>
        ///
        ///</summary>
        public int GetAbiScore(List<int> rolls)
        {

            int minus = 0;
            bool found = false;
            int total = 0;

            for (int i = 0; i < 4; i++)
            {

                if (rolls[i] > 6)
                {

                    rolls[i] = 6;

                    total += 6;

                }

                else
                {

                    total += rolls[i];

                }

            }

            while(found != true)
            {

                minus = rolls[0];

                for (int i = 1; i < 4; i++)
                {

                    if (minus > rolls[i])
                    {

                        minus = rolls[i];

                    }

                }

                found = true;

            }

            return total - minus;


        }

        public int GetAbiScore(Allele allele)
        {

            int minus = 0;
            bool found = false;
            int total = 0;
            List<int> aleval = allele.ToList();

            for (int i = 0; i < 4; i++)
            {

                if (aleval[i] > 6)
                {

                    aleval[i] = 6;

                }

                if (minus > aleval[i])
                {

                    minus = aleval[i];

                }

                total += aleval[i];

            }

            return total - minus;


        }

        ///<summary>
        ///
        ///</summary>
        public int GetMod(int score)
        {

            double temp = (score/2);

            int temp2 = Convert.ToInt32(Math.Floor(temp));

            return temp2 - 5;

        }

        ///<summary>
        ///
        ///</summary>
        public Character CharGen()
        {

            string tempname,g;
            int s, i, a, c, w, l, ch;
            int temp = rngesus.Next(0, 2);

            if (temp == 0)
            {

                g = "Female";
                tempname = charname.GenFname(g);

            }

            else
            {

                g = "Male";
                tempname = charname.GenFname(g);

            }

            Race rac = new Race();
            //List<Race> races = rac.GetRaces();

            int rnum = rngesus.Next(1, rac.Races.Count);

            s = GetAbiScore() + rac.Races[rnum].Str_mod;
            i = GetAbiScore() + rac.Races[rnum].Int_mod;
            a = GetAbiScore() + rac.Races[rnum].Agi_mod;
            c = GetAbiScore() + rac.Races[rnum].Con_mod;
            w = GetAbiScore() + rac.Races[rnum].Wis_mod;
            l = GetAbiScore() + rac.Races[rnum].Luk_mod;
            ch = GetAbiScore() + rac.Races[rnum].Cha_mod;

            int hp = 10 + GetMod(c);

            return new Character(tempname, hp, rac.Races[rnum], g, s, i, a, c,w,l,ch,1);

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

            string ret = string.Format("{0} {1} is a {2} of the {3} race with a height of {4}, {5} hair, {6} eyes, {7} skin color,str: {8}, int: {9}, agi: {10}, con: {11}, wis: {12}, luk: {13}, cha: {14}", this.FirstName, this.LastName, this.Gender, this.Racee.Racename, HeightToString(), this.HairColor, this.EyeColor, this.SkinColor, this.Str, this.Int, this.Agi, this.Con, this.Wis, this.Luk, this.Cha);
            return ret;

        }

    }

}
