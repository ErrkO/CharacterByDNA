using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    ///<summary>
    /// Struct that holds a pair of nucleotides
    ///</summary>
    public struct Tuple
    {
        
        ///<summary>
        /// the left nucleotide
        ///</summary>
        public int left;
        
        ///<summary>
        /// the right nucleotide
        ///</summary>
        public int right;

        ///<summary>
        /// the struct constructor
        ///</summary>
        ///<value>
        /// Accepts an integer for the left and right strands
        ///</value>
        public Tuple(int one, int two)
        {

            this.left = one;
            this.right = two;

        }

    }

    ///<summary>
    /// The class that contains the information for the blocks of nucleotides
    ///</summary>
    public class Allele
    {

        ///<summary>
        /// The random number generator
        ///</summary>
        ///<remarks>
        /// All Praise RNGESUS!!!
        ///</remarks>
        private Random rngesus = new Random();
        
        ///<summary>
        /// This is the number of nucleotides in an allele
        ///</summary>
        ///<value>
        /// 4
        ///</value>
        public const int NUMINALES = 4;

        ///<summary>
        /// The values of the alleles
        ///</summary>
        public int One { get; set; }

        ///<summary>
        /// The values of the alleles
        ///</summary>
        public int Two { get; set; }

        ///<summary>
        /// The values of the alleles
        ///</summary>
        public int Three { get; set; }

        ///<summary>
        /// The values of the alleles
        ///</summary>
        public int Four { get; set; }

        public Nucleotide Nucleo { get; set; }

        ///
        ///
        ///
        public Allele()
        {

            ;

        }

        ///<summary>
        /// The basic Constructor that creates an allele object
        ///</summary>
        ///<param name="one"> int: value in the first position </param>
        ///<param name="two"> int: value in the second position </param>
        ///<param name="three"> int: value in the third position </param>
        ///<param name="four"> int: value in the fourth position </param>
        public Allele(int one, int two, int three, int four)
        {

            this.One = one;
            this.Two = two;
            this.Three = three;
            this.Four = four;

            this.Nucleo = new Nucleotide();

        }

        ///
        ///
        ///
        public Allele GenerateGenderAllele(bool gender)
        {

            int one, two, three, four;
            
            bool correctstring = false;

            if (gender)
            {

                do
                {

                    one = rngesus.Next(0,2);
                    two = rngesus.Next(0,2);
                    three = rngesus.Next(0,3);
                    four = rngesus.Next(0,4);

                    int sum = one + two + three + four;

                    if (sum < 20)
                    {

                        correctstring = true;

                    }
                    
                } while(!correctstring);

                return new Allele(Nucleo.TranslateComboNum(one),Nucleo.TranslateComboNum(two),Nucleo.TranslateComboNum(three),Nucleo.TranslateComboNum(four));

            }

            else
            {

                do
                {

                    one = rngesus.Next(0,2);
                    two = rngesus.Next(0,2);
                    three = rngesus.Next(0,3);
                    four = rngesus.Next(0,4);

                    int sum = one + two + three + four;

                    if (sum > 20)
                    {

                        correctstring = true;

                    }
                    
                } while(!correctstring);

                return new Allele(Nucleo.TranslateComboNum(one),Nucleo.TranslateComboNum(two),Nucleo.TranslateComboNum(three),Nucleo.TranslateComboNum(four));

            }

        }

        ///
        ///
        ///
        public Allele GenerateRandomAllele()
        {

            int one, two, three, four;

            one = rngesus.Next(0,4);
            two = rngesus.Next(0,4);
            three = rngesus.Next(0,4);
            four = rngesus.Next(0,4);

            return new Allele(Nucleo.TranslateComboNum(one),Nucleo.TranslateComboNum(two),Nucleo.TranslateComboNum(three),Nucleo.TranslateComboNum(four));          

        }

        ///<summary>
        /// The getter method for the constant number nucleotides in alleles
        ///</summary>
        ///<returns>
        /// Returns an integer of the constant NUMINALES
        ///</returns>
        public int GetNumberOfNucleoInAlleles()
        {

            return NUMINALES;

        }

        ///<summary>
        /// This method returns the allele in the form of a list
        ///</summary>
        ///<returns>
        /// Returns a list of the alleles in their positions
        ///</returns>
        public List<int> ToList()
        {

            return new List<int>() {this.One, this.Two, this.Three, this.Four};

        }

    }

}
