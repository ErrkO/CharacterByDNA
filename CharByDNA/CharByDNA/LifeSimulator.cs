using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    public class LifeSimulator
    {

        ///<summary>
        /// The random number generator
        ///</summary>
        ///<remarks>
        /// All Praise RNGESUS!!!
        ///</remarks>
        private Random rngesus = new Random();

        public int CurrDay { get; set; }

        public int CurrYear { get; set; }

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

            this.CurrDay = 0;
            this.CurrYear = 0;

            for (int i = 0; i < startpop; i++)
            {

                Character male = new Character(true);

                Character female = new Character(false);

                this.Men.Add(male);
                this.TotalCharacters++;

                this.Women.Add(female);
                this.TotalCharacters++;

            }

            do
            {

                for (int i = 0; i < this.Men.Count; i++)
                {

                    if (this.CurrDay == 365)
                    {

                        this.Men[i].Age += 1;

                    }

                    Marriage(this.Men[i]);

                    GetPregnent(this.Men[i]);

                    if (this.CurrYear >= 18)
                    {

                        Death(this.Men[i], i);

                    }

                }

                for (int i = 0; i < this.Women.Count; i++)
                {
                    
                    if (this.Women[i].Pregnent == 1)
                    {

                        HaveChild(this.Women[i]);

                    }

                    if (this.Women[i].Pregnent > 0)
                    {

                        this.Women[i].Pregnent--;

                    }

                    if (this.CurrDay == 365)
                    {

                        this.Women[i].Age += 1;

                    }

                    if (this.CurrYear >= 18)
                    {

                        Death(this.Women[i], i);

                    }

                }

                CleanLists();

                if (this.CurrDay == 365)
                {

                    this.CurrDay = 0;

                    this.CurrYear++;

                }

                this.CurrDay++;

            } while (this.CurrYear != maxyears);

            Console.WriteLine("{0} characters have been created", this.TotalCharacters);

        }

        public void Marriage(Character husband)
        {

            for (int i = 0; i < this.Women.Count; i++)
            {

                if (husband.Age > 18 && this.Women[i].Age > 18)
                {

                    if (husband.Family.Spouse == null && this.Women[i].Family.Spouse == null)
                    {

                        husband.Family.Marriage(this.Women[i]);
                        this.Women[i].Family.Marriage(husband);

                        Console.WriteLine("Year {0} Day {1}: {2} and {3} got married!", this.CurrYear + 1, this.CurrDay + 1, husband.FirstName, this.Women[i].FirstName);

                        return;

                    }

                }

            }

        }

        public void Death(Character chara, int pos)
        {

            if (chara.Age <= 5)
            {

                int death = rngesus.Next(0, 10000);

                if (death <= 10)
                {

                    Console.WriteLine("Year {0} Day {1}: {2} has Died", this.CurrYear + 1, this.CurrDay + 1, chara.FirstName);

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

            else if (chara.Age <= 40)
            {

                int death = rngesus.Next(0, 10000);

                if (death <= 1)
                {

                    Console.WriteLine("Year {0} Day {1}: {2} has Died", this.CurrYear + 1, this.CurrDay + 1, chara.FirstName);

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

            else if (chara.Age > 40 )
            {

                int death = rngesus.Next(0, 10000);

                if (death <= 50)
                {

                    Console.WriteLine("Year {0} Day {1}: {2} has Died", this.CurrYear + 1, this.CurrDay + 1, chara.FirstName);

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

        public void GetPregnent(Character husband)
        {

            for (int i = 0; i < this.Women.Count; i++)
            {

                if (husband.Age > 18 && this.Women[i].Age > 18)
                {

                    if (husband.Family.Spouse == this.Women[i])
                    {

                        if (this.Women[i].Pregnent > 0)
                        {

                            return;

                        }

                        this.Women[i].Pregnent = rngesus.Next(270, 281);

                        Console.WriteLine("Year {0} Day {1}: {2} and {3} are pregnant",this.CurrYear + 1, this.CurrDay + 1, husband.FirstName, this.Women[i].FirstName);

                        return;

                    }

                }

            }

        }

        public void HaveChild(Character mom)
        {

            Character chld;

            chld = new Character(mom.Family.Spouse, mom);

            Console.WriteLine("Year {0} Day {1}: {2} and {3} have had a child named {4}", this.CurrYear + 1, this.CurrDay + 1, mom.Family.Spouse.FirstName, mom.FirstName, chld.FirstName);

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
