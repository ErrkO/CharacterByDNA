using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{
    
    /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
    public struct CharTemp
    {

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]/CID/*'/>
        public int CID { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public string Dna { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public bool Gender { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public double BirthTime { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public double DueDate { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int SpouseID { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public bool Dead { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public CharTemp(int cid, string dna, bool gender, double birth, double due, int sid, bool dead)
        {

            this.CID = cid;
            this.Dna = dna;
            this.Gender = gender;
            this.BirthTime = birth;
            this.DueDate = due;
            this.SpouseID = sid;
            this.Dead = dead;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public CharTemp(Character character)
        {

            this.CID = character.cid;
            this.Dna = character.dna;
            this.Gender = character.gender;
            this.BirthTime = character.birth;
            this.DueDate = character.due;
            this.SpouseID = character.sid;
            this.Dead = character.dead;

        }

    }

    /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
    class Character
    {

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int CID { get; private set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public string Fname { get; private set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public string Lname { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public string Dna { get; private set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public bool Gender { get; set; }

        //public GTime BirthTime { get; private set; }
        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public double BirthTime { get; private set; }

        //public GTime DueDate { get; set; }
        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public double DueDate { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int SpouseID { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public bool Dead { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public Race Racee { get; set; }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int Strength
        {
            get
            {

                return (int)Math.Floor((double)(GeneStatic.ToValue(this.Dna,12) + GeneStatic.ToValue(this.Dna,13)) / 2);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int StrMod
        {

            get
            {

                return GetMod(this.Strength);

            }

        }
        
        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int Intelligence
        {

            get
            {

                return (int)Math.Floor((double)(GeneStatic.ToValue(this.Dna,14) + GeneStatic.ToValue(this.Dna,15)) / 2);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int IntMod
        {

            get
            {

                return GetMod(this.Intelligence);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int Dexterity
        {

            get
            {

                return (int)Math.Floor((double)(GeneStatic.ToValue(this.Dna,16) + GeneStatic.ToValue(this.Dna,17)) / 2);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int DexMod
        {

            get
            {

                return GetMod(this.Dexterity);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int Constitution
        {

            get
            {

                return (int)Math.Floor((double)(GeneStatic.ToValue(this.Dna,18) + GeneStatic.ToValue(this.Dna,19)) / 2);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int ConMod
        {

            get
            {

                return GetMod(this.Constitution);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int Wisdom
        {

            get
            {

                return (int)Math.Floor((double)(GeneStatic.ToValue(this.Dna,20) + GeneStatic.ToValue(this.Dna,21)) / 2);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int WisMod
        {

            get
            {

                return GetMod(this.Wisdom);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int Luck
        {

            get
            {

                return (int)Math.Floor((double)(GeneStatic.ToValue(this.Dna,22) + GeneStatic.ToValue(this.Dna,23)) / 2);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int LukMod
        {

            get
            {

                return GetMod(this.Luck);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int Charisma
        {

            get
            {

                return (int)Math.Floor((double)(GeneStatic.ToValue(this.Dna,24) + GeneStatic.ToValue(this.Dna,25)) / 2);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int ChaMod
        {

            get
            {

                return GetMod(this.Charisma);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int Height
        {

            get
            {

                if (this.Gender)
                {

                    return this.Racee.MHeightBase + GeneStatic.ToValue(this.Dna,4) + GeneStatic.ToValue(this.Dna,5);

                }

                else
                {

                    return this.Racee.FHeightBase + GeneStatic.ToValue(this.Dna,4) + GeneStatic.ToValue(this.Dna,5);

                }

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public string HairColor
        {

            get
            {

                return GetProperty(GeneStatic.ToValue(this.Dna,6), GeneStatic.ToValue(this.Dna,7), this.Racee.HairColors);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public string EyeColor
        {

            get
            {

                return GetProperty(GeneStatic.ToValue(this.Dna,8), GeneStatic.ToValue(this.Dna,9), this.Racee.EyeColors);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public string SkinColor
        {

            get
            {

                return GetProperty(GeneStatic.ToValue(this.Dna,10), GeneStatic.ToValue(this.Dna,11), this.Racee.SkinColors);

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public Character(int id, string fname, string lname, string dna, bool gender, double btime, double dtime,
            int sid, bool dead)
        {
            
            this.CID = id;
            this.Fname = fname;
            this.Lname = lname;
            this.Dna = dna;
            this.Gender = gender;
            this.BirthTime = btime;
            this.DueDate = dtime;
            this.SpouseID = sid;
            this.Dead = dead;
            this.Racee = new Race(db,1);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public Character(Database db, string dna, double time, int id, NameDB NDB)
        {

            List<string> genes = DNAStatic.ToList(dna);
            this.CID = id;
            this.BirthTime = time;
            this.Dead = false;
            this.DueDate = 0;
            this.SpouseID = true;
            this.Dna = dna;
            this.Racee = new Race(db,0);

            // Gene Pair 1
            int gone = genes[0].ToInt() % 2;
            int gtwo = genes[1].ToInt() % 2;

            if (gone + gtwo == 1)
            {

                this.Gender = true;

            }

            else if (gone + gtwo == 0)
            {

                this.Gender = false;

            }

            else
            {

                Console.WriteLine("{0} has two y chromosomes",id);

            }

            this.Fname = NDB.GenFname(this.Gender);

            this.Lname = NDB.GenLname();

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public Character(Character dad, Character mom, double time, int id, NameDB NDB) : this(DNAStatic.CreateChildsDNA(dad,mom),time, id,NDB)
        {

            this.Fname = NDB.GenFname(this.Gender);
            this.Lname = dad.Lname;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public Character(CharTemp chart, string fname, string lname)
        {

            this.CID = chart.CID;
            this.BirthTime = chart.BirthTime;
            this.Gender = chart.Gender;
            this.Dead = chart.Dead;
            this.DueDate = chart.DueDate;
            this.SpouseID = chart.SpouseID;
            this.Dna = chart.Dna;
            this.Racee = new Race(db,0);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public string GetProperty(int value, int valuetwo, List<string> properties)
        {

            int vused;

            if (value == 16 || valuetwo == 16)
            {

                if (value < valuetwo)
                {

                    vused = value;

                }

                else if (valuetwo < value)
                {

                    vused = valuetwo;

                }

                else
                {

                    vused = 16;

                }

            }

            else if (value >= valuetwo)
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

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public int GetMod(int score)
        {

            double temp = (score / 2);

            int temp2 = Convert.ToInt32(Math.Floor(temp));

            return temp2 - 5;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public bool IsPregnent()
        {

            return IsPregnent(this);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public bool IsPregnent(Character character)
        {

            if (character.Gender)
            {

                return false;

            }

            else
            {

                if (Convert.ToDouble(character.DueDate) > 0)
                {

                    return true;

                }

            }

            return false;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="character"]//*'/>
        public override string ToString()
        {

            string gender;

            if (this.Gender)
            {

                gender = "Male";

            }

            else
            {

                gender = "Female";

            }

            GDate bdate = new GDate(this.BirthTime);

            return string.Format("{0}: {1}, {2} {3}",bdate.ToString(),gender,this.Fname,this.Lname);

        }

    }

}
