using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{
    public class Nucleotide
    {

        ///<summary>
        /// This method checks to see if the nucleotides are matching pairs
        ///</summary>
        ///<example>
        /// 1-4 | 3-2 | 2-3 | 4-1 are all matching pairs
        ///</example>
        ///<param name="left"> int: The nucleotide from the left strand </param>
        ///<param name="right"> int: The nucleotide from the right strand </param>
        ///<returns>
        /// Returns true if the pair is a matching pair
        ///</returns>
        public bool CheckPair(int left, int right)
        {

            if (left == 1 && right == 4)
            {

                return true;

            }

            else if (left == 2 &&  right == 3)
            {

                return true;

            }

            else if (left == 3 &&  right == 2)
            {

                return true;

            }

            else if (left == 4 && right == 1)
            {

                return true;

            }

            return false;

        }

        ///<summary>
        /// This method takes a nucleotide and flips it
        ///</summary>
        ///<example>
        /// EX: in: 1 out: 3 | in: 2 out: 4 and the reverse
        ///</example>
        ///<param name="nucleotide"> int: Numerical value of the nucleotide </param>
        ///<returns>
        /// Returns the opposite of the nucleotide as an integer
        ///</returns>
        public int Opposite(int nucleotide)
        {

            if (nucleotide == 1)
            {

                return 4;

            }

            else if (nucleotide == 2)
            {

                return 3;

            }

            else if (nucleotide == 3)
            {

                return 2;

            }

            return 1;

        }

        // To Change

        ///
        ///
        ///
        public string EncodePair(int left, int right)
        {

            if (CheckPair(left,right))
            {

                if (left > right)
                {

                    int code = left*right;

                    return code.ToString();

                }

                else
                {

                    int code = left+right;

                    return code.ToString();

                }

            }

            return null;

        }

        // To Change

        ///<summary>
        /// This method turns a nucleotide pair into a coded pair (int)
        ///</summary>
        ///<example>
        /// EX: 1-3 -> 4 | 3-1 -> 3 | 2-4 -> 6 | 4-2 -> 8
        ///</example>
        ///<param name="left"> int: Value of the left strand </param>
        ///<param name="right"> int: Value of the right strand </param>
        ///<returns>
        /// Returns the encoded pair as an integer
        ///</returns>
        public int EncodePairValue(int left, int right)
        {

            if (CheckPair(left,right))
            {

                if (left > right)
                {

                    return left*right;

                }

                else
                {

                    return left+right;

                }

            }

            return 0;


        }

        // IDK what this is

        ///
        ///
        ///
        public int TranslateNumber(int num)
        {

            if (num == 0)
            {

                return 1;

            }

            else if (num == 1)
            {

                return 2;

            }

            else if (num == 2)
            {

                return 3;

            }

            else if (num == 3)
            {

                return 4;

            }

            else
            {

                return 100;

            }

        }

        // IDK what this is either

        ///
        ///
        ///
        public int TranslateComboNum(int num)
        {

            if (num == 0)
            {

                return 3;

            }

            else if (num == 1)
            {

                return 4;

            }

            else if (num == 2)
            {

                return 6;

            }

            else if (num == 3)
            {

                return 8;

            }

            else
            {

                return 100;

            }

        }

    }

}
