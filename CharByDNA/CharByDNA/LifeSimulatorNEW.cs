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

            this.Date = new GDate();
            this.TotalCharacters = 0;

            List<CharTemp> characters = new List<CharTemp>();

            for (int i = 0; i < startpop; i++)
            {

                if (i % 2 == 0)
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

            this.Date = new GDate(AGE18);
            int tempyear = 18;

            do
            {

                if (this.Date.Year > tempyear)
                {

                    tempyear = this.Date.Year;

                }

                Marriage(characters);

                for (int i = 0; i < characters.Count; i++)
                {

                    GetPregnant(this.Date,characters[i]);
                    HaveChild(this.Date,characters[i]);
                    //Death(characters[i]);

                }

            } while (this.Date.Year <= maxyears && !AllDead(characters));

        }

        public LifeSimulator(Database db, int startpop, int maxyears) : this(db)
        {

            this.Date = new GTime();

            List<Character> characters = new List<Character>();

            for (int i = 0; i < startpop; i++)
            {

                if (i % 2 == 0)
                {

                    Character chara = new Character(db, new DNA(false), Date, TotalCharacters);
                    TotalCharacters++;
                    characters.Add(chara);
                    Console.WriteLine("{0}: {1},{2} was created.", this.Date.ToString(), chara.Fname,chara.Gender);

                }

                else
                {

                    Character chara = new Character(db, new DNA(true), Date, TotalCharacters);
                    TotalCharacters++;
                    characters.Add(chara);
                    Console.WriteLine("{0}: {1},{2} was created.", this.Date.ToString(), chara.Fname,chara.Gender);

                }

            }

            this.CDB.SaveListOfCharacters(characters);

            characters.Clear();

            this.Date = AGE18;

            int tempyear = 18;

            do
            {

                //Console.WriteLine(this.Time.ToString() + " New Day");

                if (this.Date.Year > tempyear)
                {

                    ShowStats();

                    Console.WriteLine(this.Date.ToString() + " New Year!");

                    tempyear = this.Date.Year;

                }

                characters.AddRange(this.CDB.FillListWithViableCharacters(this.Date));

                if (!OnlyOneGenderAndSingle(characters))
                {

                    Marriage(characters);

                    GetPregnant(this.Date.ToString(), characters);

                    HaveChild(characters);

                }

                //Death();

                this.Date = GTime.IncrementByDays(this.Date);

                //this.Time = this.Time++;

                //CleanLists();

                this.CDB.SaveListOfCharacters(characters);

                characters.Clear();

            } while (this.Date.Year != maxyears+1 && !AllDead(characters));

            Console.WriteLine("{0} characters have been created", this.TotalCharacters);

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

                    CharTemp spouse = GetCharByID(chara.SpouseID,chars);

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

                    CharTemp spouse = GetCharByID(chara.SpouseID,chars);
                    this.TotalCharacters++;
                    CharTemp child = new CharTemp(this.TotalCharacters,DNAStatic.CreateChildsDNA(DNAStatic.Miosis(chara.Dna,rngesus),
                                                                                                DNAStatic.Miosis(spouse.Dna,rngesus)),
                                                                                    )

                }

            }

        }

        public void HaveChild(List<Character> chars)
        {

            if (ContainsWomen(chars))
            {

                for (int i = 0; i < chars.Count; i++)
                {

                    if (chars[i].IsPregnent())
                    {

                        if (chars[i].DueDate.Year == this.Date.Year && chars[i].DueDate.Month == this.Date.Month && chars[i].DueDate.Day == this.Date.Day)
                        {

                            Character spouse = this.CDB.GetCharacter(this.FamTree.GetSpouse(chars[i]));

                            Character child = new Character(this.CDB, spouse, chars[i], this.Date, this.TotalCharacters);

                            Console.WriteLine("{0}: {1} and {2} have had a child, {3}",this.Date,spouse.Fname,chars[i].Fname,child.Fname);

                            chars[i].DueDate = new GTime(true);

                            this.FamTree.SetChild(chars[i], child);
                            this.FamTree.SetChild(spouse, child);

                            chars.Add(child);

                            this.TotalCharacters++;

                        }

                    }

                }

            }

        }

        public CharTemp GetCharByID(int id, List<CharTemp> chars)
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

        public int GetCharByIDPostion(int id, List<CharTemp> chars)
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

        public bool ContainsMen(List<Character> chars)
        {

            for (int i = 0; i < chars.Count; i++)
            {

                if (chars[i].Gender)
                {

                    return true;

                }

            }

            return false;

        }

        public bool ContainsWomen(List<Character> chars)
        {

            for (int i = 0; i < chars.Count; i++)
            {

                if (!chars[i].Gender)
                {

                    return true;

                }

            }

            return false;

        }

        public bool ContainsMenAndWomen(List<Character> chars)
        {

            if (ContainsMen(chars) && ContainsWomen(chars))
            {

                return true;

            }

            return false;

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
