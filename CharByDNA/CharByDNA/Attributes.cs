using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{
    public class Attributes
    {

        #region Base stats
        ///<summary>
        /// The random number generator
        ///</summary>
        ///<remarks>
        /// All Praise RNGESUS!!!
        ///</remarks>
        Random rngesus = new Random();

        ///<summary>
        ///
        ///</summary>
        public int Str { get; private set; }

        ///<summary>
        ///
        ///</summary>
        public int Int { get; private set; }

        ///<summary>
        ///
        ///</summary>
        public int Dex { get; private set; }

        ///<summary>
        ///
        ///</summary>
        public int Con { get; private set; }

        ///<summary>
        ///
        ///</summary>
        public int Wis {get; private set;}

        ///<summary>
        ///
        ///</summary>
        public int Luk { get; private set; }

        ///<summary>
        ///
        ///</summary>
        public int Cha { get; private set; }
        #endregion

        #region Stat Modifiers
        ///<summary>
        /// The property that contains the intelligence modifier
        ///</summary>
        public int Int_mod { get; private set; }

        ///<summary>
        /// The property that contains the strength modifier
        ///</summary>
        public int Str_mod { get; private set; }

        ///<summary>
        /// The property that contains the agility modifier
        ///</summary>
        public int Dex_mod { get; private set; }

        ///<summary>
        /// The property that contains the constitution modifier
        ///</summary>
        public int Con_mod { get; private set; }

        ///<summary>
        /// The property that contains the wisdon modifier
        ///</summary>
        public int Wis_mod { get; private set; }

        ///<summary>
        /// The property that contains the luck modifier
        ///</summary>
        public int Luk_mod { get; private set; }

        ///<summary>
        /// The property that contains the charisma modifier
        ///</summary>
        public int Cha_mod { get; private set; }
        #endregion

        ///<summary>
        ///
        ///</summary>
        public Attributes()
        {

            ;

        }

        ///<summary>
        ///
        ///</summary>
        public void SetStats(int s, int i, int d, int c, int w, int l, int ch)
        {

            this.Str = s;
            this.Int = i;
            this.Dex = d;
            this.Con = c;
            this.Wis = w;
            this.Luk = l;
            this.Cha = ch;
            this.Str_mod = GetMod(this.Str);
            this.Int_mod = GetMod(this.Int);
            this.Dex_mod = GetMod(this.Dex);
            this.Con_mod = GetMod(this.Con);
            this.Wis_mod = GetMod(this.Wis);
            this.Luk_mod = GetMod(this.Luk);
            this.Cha_mod = GetMod(this.Cha);

        }

        ///<summary>
        ///
        ///</summary>
        public void SetMods(int sm, int im, int dm, int cm, int wm, int lm, int chm)
        {

            this.Str_mod = sm;
            this.Int_mod = im;
            this.Dex_mod = dm;
            this.Con_mod = cm;
            this.Wis_mod = wm;
            this.Luk_mod = lm;
            this.Cha_mod = chm;
            
        }

        ///<summary>
        ///
        ///</summary>
        public int GetAbiScore()
        {

            List<int> dicerolls = new List<int>();

            int minus = 0;
            int total = 0;

            for (int i = 0; i < 4; i++)
            {

                dicerolls.Add(rngesus.Next(1, 7));

                if (i == 0)
                {

                    minus = dicerolls[i];

                }

                if (minus > dicerolls[i])
                {

                    minus = dicerolls[i];

                }

                total = total + dicerolls[i];

            }

            return total - minus;

        }

        ///<summary>
        ///
        ///</summary>
        public int GetAbiScore(List<int> rolls)
        {

            int minus = rolls[0];
            int total = 0;

            for (int i = 0; i < 4; i++)
            {

                if (rolls[i] > 6)
                {

                    rolls[i] = 6;

                }

                if (minus > rolls[i])
                {

                    minus = rolls[i];

                }

                total += rolls[i];

            }

            return total - minus;

        }

        public int GetAbiScore(Allele allele)
        {

            int minus = allele.One;
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

    }

}
