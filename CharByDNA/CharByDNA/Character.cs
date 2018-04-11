using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    public class Character
    {

        Random rngesus = new Random();
        Name charname = new Name();
        Race race;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Race Racee { get; set; }

        public string Gender { get; set; }

        public int Str { get; set; }

        public int Int { get; set; }

        public int Agi { get; set; }

        public int Con { get; set; }

        public int Wis {get; set;}

        public int Luk { get; set; }

        public int Cha { get; set; }

        public int Baseattk { get; set; }

        public int Hptotal { get; private set; }

        public int HpCurrent { get; set; }

        public DNA Dna { get; set; }

        public int Height { get; set; }

        public string HairColor { get; set; }

        public string EyeColor { get; set; }

        public string SkinColor { get; set; }

        public Character()
        {

            string tempname,g;
            int s, i, a, c, w, l, ch;
            int temp = rngesus.Next(0, 2);

            if (temp == 0)
            {

                g = "Female";
                tempname = charname.GenName(g);

            }

            else
            {

                g = "Male";
                tempname = charname.GenName(g);

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

        public Character(DNA dna, Character dad = null)
        {

            List<int> allele = new List<int>();
            int sum = 0;

            List<string> haircolors = new List<string>() {"Red","Auburn","Brown","Blonde","Black","Grey"};
            List<string> eyecolors = new List<string>() {"Amber","Blue","Brown","Grey","Green","Hazel"};
            List<string> skincolors = new List<string>() {"Albino","Pale","White","Tan","Brown","Black"};

            this.Dna = dna;

            race = new Race();

            for (int i = 0; i < dna.GetLength(); i++)
            {

                if (i == 4 * 1)
                {

                    if (sum > 20)
                    {

                        Gender = "Female";

                    }

                    else
                    {

                        Gender = "Male";

                    }

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 2)
                {

                    Racee = race.Races[0];

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 3)
                {

                    if (Gender == "Male")
                    {

                        Height = 50 + sum;

                    }

                    else
                    {

                        Height = 47 + sum;

                    }

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 4)
                {

                    HairColor = GetProperty(sum,haircolors);

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 5)
                {

                    EyeColor = GetProperty(sum,eyecolors);

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 6)
                {

                    SkinColor = GetProperty(sum,skincolors);

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 7)
                {

                    int score = GetAbiScore(allele);

                    score += GetScoreMod(sum);

                    Str = score;

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 8)
                {

                    int score = GetAbiScore(allele);

                    score += GetScoreMod(sum);

                    Int = score;

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 9)
                {

                    int score = GetAbiScore(allele);

                    score += GetScoreMod(sum);

                    Agi = score;

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 10)
                {

                    int score = GetAbiScore(allele);

                    score += GetScoreMod(sum);

                    Con = score;

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 11)
                {

                    int score = GetAbiScore(allele);

                    score += GetScoreMod(sum);

                    Wis = score;

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == 4 * 12)
                {

                    int score = GetAbiScore(allele);

                    score += GetScoreMod(sum);

                    Luk = score;

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

                else if (i == (4 * 13) - 1)
                {

                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                    int score = GetAbiScore(allele);

                    score += GetScoreMod(sum);

                    Cha = score;

                    sum = 0;
                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Clear();

                }

                else
                {

                    sum += dna.EncodePairValue(dna.Left[i], dna.Right[i]);
                    allele.Add(dna.EncodePairValue(dna.Left[i], dna.Right[i]));

                }

            }

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

        public int GetMod(int score)
        {

            double temp = (score/2);

            int temp2 = Convert.ToInt32(Math.Floor(temp));

            return temp2 - 5;

        }

        public Character CharGen()
        {

            string tempname,g;
            int s, i, a, c, w, l, ch;
            int temp = rngesus.Next(0, 2);

            if (temp == 0)
            {

                g = "Female";
                tempname = charname.GenName(g);

            }

            else
            {

                g = "Male";
                tempname = charname.GenName(g);

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

        public string HeightToString()
        {

            int ft; 
            int inch;

            ft = Convert.ToInt32(Math.Floor(Convert.ToDouble(this.Height/12)));

            inch = this.Height % 12;

            string str = string.Format("{0}\' {1}\"",ft,inch);

            return str;

        }

        public override string ToString()
        {

            string ret = string.Format("{0} {1} is a {2} of the {3} race with a height of {4}, {5} hair, {6} eyes, {7} skin color,str: {8}, int: {9}, agi: {10}, con: {11}, wis: {12}, luk: {13}, cha: {14}", this.FirstName, this.LastName, this.Gender, this.Racee.Racename, HeightToString(), this.HairColor, this.EyeColor, this.SkinColor, this.Str, this.Int, this.Agi, this.Con, this.Wis, this.Luk, this.Cha);
            return ret;

        }

    }

}
