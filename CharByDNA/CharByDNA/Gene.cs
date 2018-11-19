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
    public class Gene
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
        public const int NUMINGENE = 3;

        ///<summary>
        /// The property that holds the first value
        ///</summary>
        public int One { get; set; }

        ///<summary>
        /// The property that holds the second value
        ///</summary>
        public int Two { get; set; }

        ///<summary>
        /// The property that holds the third value
        ///</summary>
        public int Three { get; set; }

        ///<summary>
        /// The property that holds the nucleotide information
        ///</summary>
        public Nucleotide Nucleo { get; set; }

        public Codon Cdn { get; set; }

        ///<summary>
        /// The default constructor that just instantiates a new object
        ///</summary>
        public Gene()
        {

            this.Nucleo = new Nucleotide();
            this.Cdn = new Codon();

        }

        ///<summary>
        /// The basic Constructor that creates an allele object
        ///</summary>
        ///<param name="one"> int: value in the first position </param>
        ///<param name="two"> int: value in the second position </param>
        ///<param name="three"> int: value in the third position </param>
        ///<param name="four"> int: value in the fourth position </param>
        public Gene(int one, int two, int three) : this()
        {

            this.One = one;
            this.Two = two;
            this.Three = three;

        }

        ///<summary>
        /// The method that takes a gender value and generates 4 random codes that fit that gender
        ///</summary>
        ///<param name="gender"> bool: True for male </param>
        ///<returns>
        /// Returns an allele object
        ///</returns>
        public Gene GenerateGenderGene(bool gender)
        {

            int one, two, three;
            
            bool correctstring = false;

            if (gender)
            {

                do
                {

                    one = rngesus.Next(1, 4);
                    two = rngesus.Next(1, 4);
                    three = rngesus.Next(1, 4);

                    int num = (one*100) + (two*10) + three;

                    if (num % 2 == 0)
                    {

                        if (num != 111 || num != 444)
                        {

                            correctstring = true;

                        }

                    }
                    
                } while(!correctstring);

            }

            else
            {

                do
                {

                    one = rngesus.Next(1, 4);
                    two = rngesus.Next(1, 4);
                    three = rngesus.Next(1, 4);

                    int num = (one * 100) + (two * 10) + three;

                    if (num % 2 == 1)
                    {

                        if (num != 111 || num != 444)
                        {

                            correctstring = true;

                        }

                    }

                } while (!correctstring);

            }

            return new Gene(one, two, three);

        }

        ///<summary>
        /// The method that generates a random allele
        ///</summary>
        ///<returns>
        /// Returns an gene object
        ///</returns>
        public Gene GenerateRandomGene()
        {

            int one, two, three;

            bool correctstring = false;

            do
            {

                one = rngesus.Next(1, 5);
                two = rngesus.Next(1, 5);
                three = rngesus.Next(1, 5);

                if ((one != 1 && two != 1 && three != 1) || (one != 4 && two != 4 && three != 4))
                {

                    correctstring = true;

                }

            } while (!correctstring);

            return new Gene(one,two,three);          

        }

        public override string ToString()
        {

            return ((this.One * 100) + (this.Two * 10) + this.Three).ToString();

        }

        public int ToInt()
        {

            return (this.One * 100) +(this.Two * 10) + this.Three;

        }

        public int ToValue()
        {

            return Cdn.TranslateCodon(this);

        }

        ///<summary>
        /// The getter method for the constant number nucleotides in alleles
        ///</summary>
        ///<returns>
        /// Returns an integer of the constant NUMINALES
        ///</returns>
        public int GetNumberOfNucleoInGenes()
        {

            return NUMINGENE;

        }

        ///<summary>
        /// This method returns the allele in the form of a list
        ///</summary>
        ///<returns>
        /// Returns a list of the alleles in their positions
        ///</returns>
        public List<int> ToList()
        {

            return new List<int>() {this.One, this.Two, this.Three};

        }

    }

}
