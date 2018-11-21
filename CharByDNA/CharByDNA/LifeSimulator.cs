using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class LifeSimulator
    {

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

        public List<Character> Men { get; set; }

        public List<Character> Women { get; set; }

        private FamilyTreeDB FamTree { get; set; }

        private Database DB { get; set; }

        public LifeSimulator(Database db)
        {

            this.DB = db;
            this.FamTree = new FamilyTreeDB(db);

        }

        public LifeSimulator(Database db, int startpop, int maxyears) : this(db)
        {

            this.Time = new GTime();

            List<CharacterDB> characters = new List<CharacterDB>();

            for (int i = 0; i < startpop; i++)
            {

                CharacterDB chara = new CharacterDB(db,new DNA(),Time,TotalCharacters);
                TotalCharacters++;
                characters.Add(chara);
                Console.WriteLine("{0}: {1} was created.", this.Time.ToString(), chara.Fname);

            }

            this.DB.SaveListOfCharacters(characters);

            characters.Clear();

            this.Time = AGE18;

            do
            {

                characters.AddRange(this.DB.FillListWithViableCharacters(this.Time));

                Marriage(characters);

                GetPregnant(this.Time.ToString(),characters);

                HaveChild(characters);

                this.Time = this.Time++;

                //CleanLists();

                this.DB.UpdateDB(characters);

                characters.Clear();

            } while (this.Time.Year != maxyears);

            Console.WriteLine("{0} characters have been created", this.TotalCharacters);

        }

        public void Marriage(List<CharacterDB> chars)
        {

            if (ContainsMen(chars) || ContainsWomen(chars))
            {

                for (int i = 0; i < chars.Count; i++)
                {

                    if (!this.FamTree.HasSpouse(chars[i]))
                    {

                        for (int j = 0; j < chars.Count; j++)
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

                                            this.FamTree.SetSpouses(chars[i], chars[j]);

                                            Console.WriteLine("{0}: {1} and {2} got married.", this.Time.ToString(), chars[i].Fname, chars[j].Fname);

                                            return;

                                        }

                                        else
                                        {

                                            chars[i].Lname = chars[j].Lname;

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

        public void Death(Character chara, int pos)
        {

            if (chara.GetAge(this.Time) <= AGE5)
            {

                int death = rngesus.Next(0, 10000);

                if (death <= 10)
                {

                    Console.WriteLine("{0}: {1} has Died", this.Time.ToString(), chara.FirstName);

                    if (chara.Gender)
                    {

                        this.Men[pos].Dead = true;

                    }

                    else
                    {

                        this.Women[pos].Dead = true;

                    }

                }

            }

            else if (chara.GetAge(this.Time) <= AGE40)
            {

                int death = rngesus.Next(0, 10000);

                if (death <= 1)
                {

                    Console.WriteLine("{0}: {1} has Died", this.Time.ToString(), chara.FirstName);

                    if (chara.Gender)
                    {

                        this.Men[pos].Dead = true;

                    }

                    else
                    {

                        this.Women[pos].Dead = true;

                    }

                }

            }

            else if (chara.GetAge(this.Time) > AGE40 )
            {

                int death = rngesus.Next(0, 10000);

                if (death <= 50)
                {

                    Console.WriteLine("{0}: {1} has Died", this.Time.ToString(), chara.FirstName);

                    if (chara.Gender)
                    {

                        this.Men[pos].Dead = true;

                    }

                    else
                    {

                        this.Women[pos].Dead = true;

                    }

                }

            }

        }

        public void GetPregnant(string timecode, List<CharacterDB> chars)
        {

            if (ContainsWomen(chars))
            {

                for (int i = 0; i < chars.Count; i++)
                {

                    if (!chars[i].Gender && !chars[i].IsPregnent())
                    {

                        if (this.FamTree.HasSpouse(chars[i]))
                        {

                            CharacterDB spouse = new CharacterDB(this.DB, this.FamTree.GetSpouse(chars[i]));

                            string duecode = GTime.AddDays(timecode, rngesus.Next(270, 281));

                            chars[i].DueDate = new GTime(duecode);

                            Console.WriteLine("{0}: {1} and {2} are pregnant, due date is {3}", this.Time.ToString(), spouse.Fname, chars[i].Fname, chars[i].DueDate.ToString());

                        }

                    }

                }

            }

        }

        public void HaveChild(List<CharacterDB> chars)
        {

            if (ContainsWomen(chars))
            {

                for (int i = 0; i < chars.Count; i++)
                {

                    if (chars[i].IsPregnent())
                    {

                        if (chars[i].DueDate == this.Time)
                        {

                            CharacterDB spouse = new CharacterDB(this.DB, this.FamTree.GetSpouse(chars[i]));

                            CharacterDB child = new CharacterDB(this.DB, spouse, chars[i], this.Time, this.TotalCharacters);

                            Console.WriteLine("{0}: {1} and {2} have had a child, {3}",this.Time,spouse.Fname,chars[i].Fname,child.Fname);

                            this.FamTree.SetChild(chars[i], child);
                            this.FamTree.SetChild(spouse, child);

                        }

                    }

                }

            }

        }

        public void HaveChild(Character mom)
        {

            Character chld;

            chld = new Character(mom.Family.Spouse, mom);

            //CharacterDB tmpchild = new CharacterDB(chld, this.Time);
            //tmpchild.SaveCharacter(tmpchild);

            Console.WriteLine("{0}: {1} and {2} have had a child named {3}", this.Time.ToString(), mom.Family.Spouse.FirstName, mom.FirstName, chld.FirstName);

            if (chld.Gender)
            {

                Men.Add(chld);

            }

            else
            {

                Women.Add(chld);

            }

            this.TotalCharacters++;

            return;

        }

        public void CleanLists()
        {

            int msize = this.Men.Count;
            int wsize = this.Women.Count;

            int iter = 0;

            while (iter < msize)
            {

                if (this.Men[iter].Dead)
                {

                    this.Men.RemoveAt(iter);

                    msize--;

                }

                else
                {

                    iter++;

                }

            }

            iter = 0;

            while (iter < wsize)
            {

                if (this.Women[iter].Dead)
                {

                    this.Women.RemoveAt(iter);

                    wsize--;

                }

                else
                {

                    iter++;

                }

            }

        }

        public bool ContainsMen(List<CharacterDB> chars)
        {

            int mencount = 0;

            for (int i = 0; i < chars.Count; i++)
            {

                if (chars[i].Gender)
                {

                    mencount++;

                }

            }

            if (mencount > 0)
            {
                return true;

            }

            return false;

        }

        public bool ContainsWomen(List<CharacterDB> chars)
        {

            int womencount = 0;

            for (int i = 0; i < chars.Count; i++)
            {

                if (!chars[i].Gender)
                {

                    womencount++;

                }

            }

            if (womencount > 0)
            {
                return true;

            }

            return false;

        }

    }

}
