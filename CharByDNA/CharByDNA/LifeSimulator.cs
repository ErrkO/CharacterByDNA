using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    public class LifeSimulator
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
        private Random rngesus = new Random();

        public GTime Time { get; set; }

        public int TotalCharacters { get; set; }

        public List<Character> Men { get; set; }

        public List<Character> Women { get; set; }

        public LifeSimulator()
        {

            ;

        }

        public LifeSimulator(int startpop, int maxyears)
        {

            this.Men = new List<Character>();
            this.Women = new List<Character>();

            Time = new GTime();

            for (int i = 0; i < startpop; i++)
            {

                Character male = new Character(true);

                Character female = new Character(false);

                this.Men.Add(male);
                this.TotalCharacters++;

                CharacterDB tmpmale = new CharacterDB(male,Time);

                this.Women.Add(female);
                this.TotalCharacters++;

                CharacterDB tmpfmale = new CharacterDB(female, Time);

                Console.WriteLine("{0}: {1} and {2} were created", this.Time.ToString(), male.FirstName, female.FirstName);

            }

            do
            {

                this.Time = AGE18;

                for (int i = 0; i < this.Men.Count; i++)
                {

                    Marriage(this.Men[i]);

                    GetPregnent(this.Time.ToString(), this.Men[i]);

                }

                for (int i = 0; i < this.Women.Count; i++)
                {

                    if (this.Time.Month == 10)
                    {

                        if (this.Time.Hour == this.Women[i].DueDate.Hour)
                        {

                            if (this.Time.Minute == this.Women[i].DueDate.Minute)
                            {

                                ;

                            }

                        }

                    }

                    if (this.Women[i].DueDate != new GTime(true))
                    {

                        if (this.Time == this.Women[i].DueDate)
                        {

                            HaveChild(this.Women[i]);

                            this.Women[i].DueDate = new GTime(true);

                        }

                    }
                    
                }

                this.Time = this.Time++;

                CleanLists();

            } while (this.Time.Year != maxyears);

            Console.WriteLine("{0} characters have been created", this.TotalCharacters);

        }

        public void Marriage(Character husband)
        {

            for (int i = 0; i < this.Women.Count; i++)
            {

                if (husband.GetAge(this.Time) >= AGE18 && this.Women[i].GetAge(this.Time) >= AGE18)
                {

                    if (husband.Family.Spouse == null && this.Women[i].Family.Spouse == null)
                    {

                        husband.Family.Marriage(this.Women[i]);
                        this.Women[i].Family.Marriage(husband);

                        Console.WriteLine("{0}: {1} and {2} got married!", this.Time.ToString(), husband.FirstName, this.Women[i].FirstName);

                        return;

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

        public void GetPregnent(string timecode, Character husband)
        {

            if (husband.Family.Spouse != null)
            {

                if (husband.Family.Spouse.DueDate != new GTime(true))
                {

                    return;

                }

                string duecode = GTime.AddDays(timecode, rngesus.Next(270, 281));

                husband.Family.Spouse.DueDate = new GTime(duecode);

                Console.WriteLine("{0}: {1} and {2} are pregnant, due date is {3}", this.Time.ToString(), husband.FirstName, husband.Family.Spouse.FirstName, husband.Family.Spouse.DueDate.ToString());

                return;

            } 

        }

        public void HaveChild(Character mom)
        {

            Character chld;

            chld = new Character(mom.Family.Spouse, mom);

            CharacterDB tmpchild = new CharacterDB(chld, this.Time);
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

    }

}
