using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class LifeSimulatorNEW
    {

        private const double MAXDEATHPERC = .3;

        public GDate AGE18 = new GDate(18);

        public GDate AGE5 = new GDate(5);

        public GDate AGE40 = new GDate(40);

        ///<summary>
        /// The random number generator
        ///</summary>
        ///<remarks>
        /// All Praise RNGESUS!!!
        ///</remarks>
        private Random rngesus = new Random(Guid.NewGuid().GetHashCode());

        public GDate Date { get; set; }

        public int TotalCharacters { get; set; }

        //public int AliveCharacters { get; set; }

        private FamilyTreeDB FamTree { get; set; }

        private CharacterDB CDB { get; set; }

        private NameDB NDB { get; set; }

        public LifeSimulator(Database db)
        {

            this.CDB = new CharacterDB(db);
            this.FamTree = new FamilyTreeDB(db);
            this.NDB = new NameDB(db);

        }

        public LifeSimulatorNEW(Database database, int startpop, int maxyears) : this(db)
        {

            //this.Date = new GDate();

            GDate date = new GDate();
            this.TotalCharacters = 0;

            List<CharTemp> characters = new List<CharTemp>();

            for (int i = 0; i < startpop; i++)
            {

                if (i % 2 == 1)
                {

                    this.TotalCharacters++;
                    CharTemp chara = new CharTemp(this.TotalCharacters,new DNAStatic(false),false,this.Date.ToDouble(),new GDate(true),-1,false);
                    characters.Add(chara);

                }

                else
                {

                    this.TotalCharacters++;
                    CharTemp chara = new CharTemp(this.TotalCharacters,new DNAStatic(true),true,this.Date.ToDouble(),new GDate(true),-1,false);
                    characters.Add(chara);

                }

            }

            date = new GDate(AGE18);
            int tempyear = 18;

            do
            {

                if (date.Year > tempyear)
                {

                    tempyear = date.Year;

                }

                Marriage(characters);

                for (int i = 0; i < characters.Count; i++)
                {

                    GetPregnant(date,characters[i]);
                    HaveChild(date,characters[i]);
                    //Death(characters[i]);

                }

                date++;

            } while (date.Year <= maxyears && !AllDead(characters));

            CreateCharactersFromStruct(characters,this.NDB);

            Console.Write("{0} characters have been created",this.TotalCharacters);

        }

        public void Marriage(List<CharTemp> chars)
        {

            for (int i = 0; i < chars.Count-1; i++)
            {

                for (int j = 1; j < chars.Count; j++)
                {

                    if (chars[i].SpouseID >= 0 && chars[j].SpouseID >= 0)
                    {

                        if (chars[j].Gender != chars[i].Gender)
                        {

                            this.FamTree.SetSpouses(chars[i],chars[j]);
                            chars[i].SpouseID = chars[j].CID;
                            chars[j].SpouseID = chars[i].CID;
                            return;

                        }

                    }

                }

            }

        }

        /*public void Death()
        {

            int maxcharsdie = Convert.ToInt32(Math.Floor(this.CDB.GetNumberOfAliveCharacters() * MAXDEATHPERC));

            int numtodie = rngesus.Next(0, maxcharsdie);

            List<int> ids = new List<int>();

            for (int i = 0; i < numtodie; i++)
            {

                ids.Add(rngesus.Next(0, this.TotalCharacters-1));

            }

            foreach(int i in ids)
            {

                int unlucky = rngesus.Next(1,100);

                if (rngesus.Next(1,100) == unlucky)
                {

                    Character chara = new Character(this.CDB, i);

                    if (!chara.Dead)
                    {

                        Console.WriteLine("{0}: {1} has died", this.Time.ToString(), chara.Fname);
                        chara.Dead = true;
                        this.CDB.SaveCharacter(chara);

                    }

                }

            }

        } */

        public void GetPregnent(GDate date, CharTemp chara, List<CharTemp> chars, Random rngesus)
        {

            if (!chara.Gender && !Character.IsPregnent())
            {

                if (chara.SpouseID >= 0)
                {

                    CharTemp spouse = GetCharTempByID(chara.SpouseID,chars);

                    if (!spouse.Dead)
                    {

                        GDate duedate = GDate.AddDays(new GDate(date).ToDouble(),rngesus.Next(270,281));
                        chara.DueDate = duedate.ToDouble();

                    }

                }

            }

        }

        public void HaveChild(GDate date, CharTemp chara, List<CharTemp> chars, Random rngesus)
        {

            if (chara.IsPregnent())
            {

                if (chara.DueDate.Equals(date))
                {

                    CharTemp spouse = GetCharTempByID(chara.SpouseID,chars);
                    this.TotalCharacters++;
                    string cdna = DNAStatic.CreateChildsDNA(DNAStatic.Miosis(chara.Dna,rngesus),DNAStatic.Miosis(spouse.Dna,rngesus));
                    CharTemp child = new CharTemp(this.TotalCharacters,cdna,Convert.ToBoolean(DNAStatic.GetGenePairValue(cdna,0)),date,
                        new GDate(true),-1,false);
                    this.FamTree.SetChild(chara,child);
                    this.FamTree.SetChild(spouse,child);

                    int chararef = GetCharTempByIDPostion(chara.CID,chars);

                    chars[chararef].DueDate = new GDate(true);
                    chars.Add(child);

                }

            }

        }

        public void CreateCharactersFromStruct(List<CharTemp> chars, NameDB NDB)
        {

            string lname;

            List<Character> characters = new List<Character>();

            foreach (CharTemp chara in chars)
            {

                if (chara.Gender)
                {

                    lname = NDB.GenLName();

                }

                string fname = NDB.GenFName(chara.Gender);

                characters.Add(new Character(chara,fname,lname));

            }

            chars.Clear();

            foreach (Character chara in characters)
            {

                lname = chara.Lname;

                if (this.FamTree.HasParent(chara.CID))
                {

                    List<int> parents = this.FamTree.GetParents(chara.CID);
                    Character parent1 = GetCharsByID(parents[0],chars);
                    Character parent2 = GetCharsByID(parents[1],chars);

                    if (parent1.Gender)
                    {

                        lname = parent1.Lname;

                    }

                    else
                    {

                        lname = parent2.Lname;

                    }

                }

                if (!chara.gender)
                {

                    if (chara.SpouseID > 0)
                    {

                        int sref = this.FamTree.GetSpouse(chara.CID);
                        Character spouse = GetCharsByID(sref,chars);
                        lname = spouse.Lname;

                    }

                }

                int charref = GetCharByIDPostion(chara.CID,chars);
                characters[charref].Lname = lname;
                chara.Lname = lname;
                
                Console.Write("{0} was born",chara.ToString());

                this.CDB.SaveListOfCharacters(characters);

            }

        }

        public CharTemp GetCharTempByID(int id, List<CharTemp> chars)
        {

            foreach (CharTemp chara in chars)
            {

                if (chara.CID == id)
                {

                    return chara;

                }

            }

            return null;

        }

        public CharTemp GetCharTempByID(int id, List<Character> chars)
        {

            foreach (Character chara in chars)
            {

                if (chara.CID == id)
                {

                    return new CharTemp(chara);

                }

            }

            return null;

        }

        public int GetCharTempByIDPostion(int id, List<CharTemp> chars)
        {

            int counter = 0;

            foreach (CharTemp chara in chars)
            {

                if (chara.CID == id)
                {

                    return counter;

                }

                counter++;

            }

            return -1;

        }

        public Character GetCharsByID(int id, List<CharTemp> chars)
        {

            foreach (CharTemp chara in chars)
            {

                if (chara.CID == id)
                {

                    return new Character(chara,"","");

                }

            }

            return null;

        }

        public CharTemp GetCharByID(int id, List<Character> chars)
        {

            foreach (Character chara in chars)
            {

                if (chara.CID == id)
                {

                    return chara;

                }

            }

            return null;

        }

        public int GetCharByIDPostion(int id, List<Character> chars)
        {

            int counter = 0;

            foreach (Character chara in chars)
            {

                if (chara.CID == id)
                {

                    return counter;

                }

                counter++;

            }

            return -1;

        }

        public bool AllDead(List<Character> chars)
        {

            int numdead = this.CDB.GetNumberOfDeadCharacters();

            if (numdead == this.TotalCharacters)
            {

                return true;

            }

            return false;

        }

        public bool OnlyOneGenderAndSingle(List<Character> chars)
        {

            if (chars.Count == 0)
            {

                return true;

            }

            bool gender = chars[0].Gender;

            for (int i = 0; i < chars.Count; i++)
            {

                if (chars[i].Gender != gender)
                {

                    return false;

                }

                if (!chars[i].SpouseID)
                {

                    return false;

                }

            }

            return true;

        }

        public void ShowStats()
        {

            int aliveC = this.CDB.GetNumberOfAliveCharacters();

            int singleC = this.CDB.GetNumberOfSingleCharacters();

            int deadC = this.CDB.GetNumberOfDeadCharacters();

            Console.WriteLine();
            Console.WriteLine("|---------------------------|");
            Console.WriteLine("|     END OF YEAR STATS     |");
            Console.WriteLine("|---------------------------|");
            Console.WriteLine("|  Total Characters = {0}   |",this.TotalCharacters);
            Console.WriteLine("|        Alive = {0}        |", aliveC);
            Console.WriteLine("|       Single = {0}        |",singleC);
            Console.WriteLine("|         Dead = {0}        |",deadC);
            Console.WriteLine("|---------------------------|");
            Console.WriteLine();

        }

    }

}
