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
    /// Class that contains the information for DNA
    ///</summary>
    public class DNA
    {

        ///<summary>
        /// This is the number of genes in a strand of DNA
        ///</summary>
        public const int LENGTH = 52;

        ///<summary>
        /// The random number generator
        ///</summary>
        ///<remarks>
        /// All Praise RNGESUS!!!
        ///</remarks>
        private Random rngesus = new Random();

        ///<summary>
        /// The property that contains a list of the left strand nucleotides
        ///</summary>
        public List<int> Left {get;set;}
        
        ///<summary>
        /// The property that contains a list of the right strand nucleotides
        ///</summary>
        public List<int> Right {get;set;}

        ///<summary>
        /// The default constructor. It just sets the strands to null
        ///</summary>
        public DNA()
        {

            this.Left = null;
            this.Right = null;

        }

        ///<summary>
        /// This constructor builds the dna object by decoding a string and turning it into the strands
        ///</summary>
        ///<value>
        /// Accepts a string that represents the DNA
        ///</value>
        public DNA(string codeddna)
        {

            this.Left = new List<int>();
            this.Right = new List<int>();

            List<string> codelist = codeddna.Split(',').ToList();

            for (int i = 0; i < LENGTH; i++)
            {

                Tuple tup = DecodeDNA(codelist[i]);

                this.Left.Add(tup.left);

                this.Right.Add(tup.right);

            }

        }

        ///<summary>
        /// This constructor builds the dna object from the DNA of the dad and mom strands
        ///</summary>
        ///<value>
        /// Accepts a strand from the dad and the mom
        ///</value>
        public DNA(List<int> dad, List<int> mom)
        {

            this.Left = dad;
            this.Right = mom;

            FixDNA();

        }

        ///<summary>
        /// This method randomly selects a strand of dna to give for reproduction
        ///</summary>
        ///<returns>
        /// Returns a list of integers representing the strand of DNA
        ///</returns>
        public List<int> Miosis()
        {

            int choice = rngesus.Next(1,3);

            if (choice == 1)
            {

                return this.Left;

            }

            else
            {

                return this.Right;

            }

        }

        ///<summary>
        /// This method checks to see if the nucleotides are matching pairs
        ///</summary>
        ///<example>
        /// 1-3, 3-1, 2-4, 4-2 are all matching pairs
        ///</example>
        ///<value>
        /// Accepts an integer that gives the position on the strands
        ///</value>
        ///<returns>
        /// Returns true if the pair is a matching pair
        ///</returns>
        private bool CheckPair(int pos)
        {

            if (this.Left[pos] == 1 && this.Right[pos] == 3)
            {

                return true;

            }

            else if (this.Left[pos] == 2 &&  this.Right[pos] == 4)
            {

                return true;

            }

            else if (this.Left[pos] == 3 &&  this.Right[pos] == 1)
            {

                return true;

            }

            else if (this.Left[pos] == 4 &&  this.Right[pos] == 2)
            {

                return true;

            }

            return false;

        }

        ///<summary>
        /// This method checks to see if the nucleotides are matching pairs
        ///</summary>
        ///<example>
        /// 1-3, 3-1, 2-4, 4-2 are all matching pairs
        ///</example>
        ///<value>
        /// Accepts two integers that represent the left and right nucleotides
        ///</value>
        ///<returns>
        /// Returns true if the pair is a matching pair
        ///</returns>
        private bool CheckPair(int left, int right)
        {

            if (left == 1 && right == 3)
            {

                return true;

            }

            else if (left == 2 &&  right == 4)
            {

                return true;

            }

            else if (left == 3 &&  right == 1)
            {

                return true;

            }

            else if (left == 4 && right == 2)
            {

                return true;

            }

            return false;

        }

        ///<summary>
        /// This method takes a nucleotide and flips it
        ///</summary>
        ///<example>
        /// EX: in: 1 out: 3, in: 2 out: 4 and the reverse
        ///</example>
        ///<value>
        /// Accepts a nucleotide as an integer
        ///</value>
        ///<returns>
        /// Returns the opposite of the value
        ///</returns>
        private int Opposite(int half)
        {

            if (half == 1)
            {

                return 3;

            }

            else if (half == 2)
            {

                return 4;

            }

            else if (half == 3)
            {

                return 1;

            }

            return 2;

        }

        ///<summary>
        /// This method converts a coded pair into a tuple object
        ///</summary>
        ///<value>
        /// Accepts a string that represents the coded pair
        ///</value>
        ///<returns>
        /// Returns a tuple object that contains the numerical pairs
        ///</returns>
        private Tuple DecodeDNA(string code)
        {

            Tuple tup = new Tuple(0,0);

            if (code == "3")
            {

                tup = new Tuple(3,1);

            }

            else if (code == "4")
            {

                tup = new Tuple(1,3);

            }

            else if (code == "6")
            {

                tup = new Tuple(2,4);
                
            }

            else if (code == "8")
            {

                tup = new Tuple(4,2);
                
            }

            return tup;

        }

        ///<summary>
        /// This method turns a nucleotide pair into a coded pair
        ///</summary>
        ///<example>
        /// EX: 1-3 -> 4, 3-1 -> 3, 2-4 -> 6, 4-2 -> 8
        ///</example>
        ///<value>
        /// Accepts two integers that represent the nucleotide pair
        ///</value>
        ///<returns>
        /// Returns a string that contains the coded pair
        ///</returns>
        private String EncodePair(int left, int right)
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

        ///<summary>
        /// 
        ///</summary>
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

        ///<summary>
        /// 
        ///</summary>
        public String EncodeDNA(DNA dna)
        {

            string codeddna = "";

            for (int i = 0; i < LENGTH; i++)
            {

                if (i == LENGTH - 1)
                {

                    codeddna += EncodePair(dna.Left[i],dna.Right[i]);

                }

                else
                {

                    codeddna += EncodePair(dna.Left[i],dna.Right[i]) + ",";

                }

            }

            return codeddna;

        }

        ///<summary>
        /// 
        ///</summary>
        public void FixDNA(DNA dna)
        {

            for (int i = 0; i < LENGTH; i++)
            {

                if (!CheckPair(i))
                {

                    int choice = rngesus.Next(1,3);

                    if (choice == 1)
                    {

                        dna.Right[i] = Opposite(dna.Left[i]);

                    }

                    else
                    {

                        dna.Left[i] = Opposite(dna.Right[i]);

                    }

                }

            }

        }

        ///<summary>
        /// 
        ///</summary>
        public void FixDNA()
        {

            for (int i = 0; i < LENGTH; i++)
            {

                if (!CheckPair(i))
                {

                    int choice = rngesus.Next(1,3);

                    if (choice == 1)
                    {

                        this.Right[i] = Opposite(this.Left[i]);

                    }

                    else
                    {

                        this.Left[i] = Opposite(this.Right[i]);

                    }

                }

            }

        }

        ///<summary>
        /// 
        ///</summary>
        public void CompleteDNA(List<int> strand)
        {

            if (this.Left == null)
            {

                this.Left = strand;

            }

            else
            {

                this.Right = strand;

            }

        }

        ///<summary>
        /// 
        ///</summary>
        public override string ToString()
        {

            string dna = "";

            for (int i = 0; i < LENGTH; i++)
            {

                dna += this.Left[i] + " - " + this.Right[i] + "\n";

            }

            return dna;

        }

        ///<summary>
        /// 
        ///</summary>
        public int GetLength()
        {

            return LENGTH;

        }

    }

}