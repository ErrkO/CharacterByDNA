using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class LifeSimulator
    {

        private const double MAXDEATHPERC = .3;

        public GTime AGE18 = new GTime(18);

        public GTime AGE5 = new GTime(5);

        public GTime AGE40 = new GTime(40);

        ///<summary>
        /// The random number generator
        ///</summary>
        ///<remarks>
        /// All Praise RNGESUS!!!
        ///</remarks>
        private Random rngesus = new Random(Guid.NewGuid().GetHashCode());

        public GTime Time { get; set; }

        public int TotalCharacters { get; set; }

        //public int AliveCharacters { get; set; }

        public List<Character> Men { get; set; }

        public List<Character> Women { get; set; }

        private FamilyTreeDB FamTree { get; set; }

        private CharacterDB CDB { get; set; }

        public LifeSimulator(Database db)
        {

            this.CDB = new CharacterDB(db);
            this.FamTree = new FamilyTreeDB(db);

        }

        public LifeSimulator(Database db, int startpop, int maxyears) : this(db)
        {

            this.Time = new GTime();

            List<Character> characters = new List<Character>();

            for (int i = 0; i < startpop; i++)
            {

                if (i % 2 == 0)
                {

                    Character chara = new Character(db, new DNA(false), Time, TotalCharacters);
                    TotalCharacters++;
                    characters.Add(chara);
                    Console.WriteLine("{0}: {1},{2} was created.", this.Time.ToString(), chara.Fname,chara.Gender);

                }

                else
                {

                    Character chara = new Character(db, new DNA(true), Time, TotalCharacters);
                    TotalCharacters++;
                    characters.Add(chara);
                    Console.WriteLine("{0}: {1},{2} was created.", this.Time.ToString(), chara.Fname,chara.Gender);

                }

            }

            this.CDB.SaveListOfCharacters(characters);

            characters.Clear();

            this.Time = AGE18;

            int tempyear = 18;

            do
            {

                //Console.WriteLine(this.Time.ToString() + " New Day");

                if (this.Time.Year > tempyear)
                {

                    ShowStats();

                    Console.WriteLine(this.Time.ToString() + " New Year!");

                    tempyear = this.Time.Year;

                }

                characters.AddRange(this.CDB.FillListWithViableCharacters(this.Time));

                if (!OnlyOneGenderAndSingle(characters))
                {

                    Marriage(characters);

                    GetPregnant(this.Time.ToString(), characters);

                    HaveChild(characters);

                }

                //Death();

                this.Time = GTime.IncrementByDays(this.Time);

                //this.Time = this.Time++;

                //CleanLists();

                this.CDB.SaveListOfCharacters(characters);

                characters.Clear();

            } while (this.Time.Year != maxyears+1 && !AllDead(characters));

            Console.WriteLine("{0} characters have been created", this.TotalCharacters);

        }

        public void Marriage(List<Character> chars)
        {

            if (ContainsMenAndWomen(chars))
            {

                for (int i = 0; i < chars.Count; i++)
                {

                    for (int j = 0; j < chars.Count(); j++)
                    {

                        if (!this.FamTree.HasSpouse(chars[i]))
                        {

                            if (i != j)
                            {

                                if (chars[j].Gender != chars[i].Gender)
                                {

                                    if (!this.FamTree.HasSpouse(chars[j]))
                                    {

                                        if (chars[i].Gender)
                                        {

                                            chars[j].Lname = chars[i].Lname;

                                            chars[j].IsSingle = false;
                                            chars[i].IsSingle = false;

                                            this.FamTree.SetSpouses(chars[i], chars[j]);

                                            Console.WriteLine("{0}: {1} and {2} got married.", this.Time.ToString(), chars[i].Fname, chars[j].Fname);

                                            return;

                                        }

                                        else
                                        {

                                            chars[i].Lname = chars[j].Lname;

                                            chars[j].IsSingle = false;
                                            chars[i].IsSingle = false;

                                            this.FamTree.SetSpouses(chars[i], chars[j]);

                                            Console.WriteLine("{0}: {1} and {2} got married.", this.Time.ToString(), chars[j].Fname, chars[i].Fname);

                                            return;

                                        }

                                    }

                                }

                            }

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

        public void GetPregnant(string timecode, List<Character> chars)
        {

            if (ContainsWomen(chars))
            {

                for (int i = 0; i < chars.Count; i++)
                {

                    if (!chars[i].Gender && !chars[i].IsPregnent())
                    {

                        if (this.FamTree.HasSpouse(chars[i]))
                        {

                            Character spouse = this.CDB.GetCharacter(this.FamTree.GetSpouse(chars[i]));

                            if (!spouse.Dead)
                            {

                                string duecode = GTime.AddDays(timecode, rngesus.Next(270, 281));

                                chars[i].DueDate = new GTime(duecode);

                                Console.WriteLine("{0}: {1} and {2} are pregnant, due date is {3}", this.Time.ToString(), spouse.Fname, chars[i].Fname, chars[i].DueDate.ToString());

                            }

                        }

                    }

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

                        if (chars[i].DueDate.Year == this.Time.Year && chars[i].DueDate.Month == this.Time.Month && chars[i].DueDate.Day == this.Time.Day)
                        {

                            Character spouse = this.CDB.GetCharacter(this.FamTree.GetSpouse(chars[i]));

                            Character child = new Character(this.CDB, spouse, chars[i], this.Time, this.TotalCharacters);

                            Console.WriteLine("{0}: {1} and {2} have had a child, {3}",this.Time,spouse.Fname,chars[i].Fname,child.Fname);

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

        public bool ContainsGender(List<Character> characters, bool gender)
        {

            for (int i = 0; i < chars.Count; i++)
            {

                if (chars[i].Gender == gender)
                {

                    return true;

                }

            }

            return false;

        }

        public bool ContainsMenAndWomen(List<Character> chars)
        {

            if (ContainsGender(chars,true) && ContainsGender(chars,false))
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

                if (!chars[i].IsSingle)
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
