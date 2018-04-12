using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    ///<summary>
    /// Class that contains the information for DNA
    ///</summary>
    public class DNA
    {

        ///<summary>
        /// This is the number of nucleotides in a strand of DNA
        ///</summary>
        ///<value>
        /// 52
        ///</value>
        public const int LENGTH = 52;

        ///<summary>
        /// This is the number of alleles in DNA
        ///</summary>
        ///<value>
        /// 13
        ///</value>
        public const int NUMALLELES = 13;

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

        public Allele Alle { get; set; }

        public List<Allele> Alleles { get; set; }

        ///<summary>
        /// The default constructor. It just sets the strands to null
        ///</summary>
        public DNA()
        {

            this.Left = null;
            this.Right = null;

        }

        ///<summary>
        /// This Constructor makes random DNA string based on the given gender
        ///</summary>
        ///<param name="gender"> bool: True for male </param>
        public DNA(bool gender)
        {

            this.Left = new List<int>();
            this.Right = new List<int>();
            this.Alle = new Allele();
            this.Alleles = new List<Allele>();

            this.Alleles.Add(this.Alle.GenerateGenderAllele(gender));

            for (int i = 0; i < NUMALLELES - 1; i++)
            {

                this.Alleles.Add(this.Alle.GenerateRandomAllele());

            }

            for (int i = 0; i < NUMALLELES; i++)
            {

                Allele a = Alleles[i];
                List<int> codes = a.ToList();

                for (int j = 0; j < Alle.GetNumberOfNucleoInAlleles(); j++)
                {

                    Tuple tup = DecodeDNA(codes[j]);
                    this.Left.Add(tup.left);
                    this.Right.Add(tup.right);

                }

            }

        }

        ///<summary>
        /// This constructor builds the dna object by decoding a string and turning it into the strands
        ///</summary>
        ///<param name="codedna">
        /// string: represents coded dna
        ///</param>
        public DNA(string codeddna)
        {

            this.Left = new List<int>();
            this.Right = new List<int>();
            this.Alle = new Allele();

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
       ///<param name="dad"> List of int: the strand of nucleotides from the father </param>
       ///<param name="mom"> List of int: the strand of nucleotides from the mother </param>
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
        /// This method converts a coded pair into a tuple object
        ///</summary>
        ///<param name="code"> string: Encoded version of the DNA  </param>
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

        ///
        ///
        ///
        private Tuple DecodeDNA(int code)
        {

            Tuple tup = new Tuple(0,0);

            if (code == 3)
            {

                tup = new Tuple(3,1);

            }

            else if (code == 4)
            {

                tup = new Tuple(1,3);

            }

            else if (code == 6)
            {

                tup = new Tuple(2,4);
                
            }

            else if (code == 8)
            {

                tup = new Tuple(4,2);
                
            }

            return tup;

        }

        ///<summary>
        /// This method turns a nucleotide pair into a coded pair (string)
        ///</summary>
        ///<example>
        /// EX: 1-3 -> 4 | 3-1 -> 3 | 2-4 -> 6 | 4-2 -> 8
        ///</example>
        ///<param name="left"> int: Value of the left strand </param>
        ///<param name="right"> int: Value of the right strand </param>
        ///<returns>
        /// Returns the encoded pair as a string
        ///</returns>

        ///<summary>
        /// This method turns the DNA object into a coded string
        ///</summary>
        ///<param name="dna"> DNA: object that represents the DNA </param>
        ///<returns>
        /// Returns a string of the coded DNA
        ///</returns>
        public String EncodeDNA(DNA dna)
        {

            string codeddna = "";

            for (int i = 0; i < LENGTH; i++)
            {

                if (i == LENGTH - 1)
                {

                    codeddna += Alle.Nucleo.EncodePair(dna.Left[i],dna.Right[i]);

                }

                else
                {

                    codeddna += Alle.Nucleo.EncodePair(dna.Left[i],dna.Right[i]) + ",";

                }

            }

            return codeddna;

        }

        ///<summary>
        /// This method checks the given DNA object and corrects any errors
        ///</summary>
        ///<param name="dna"> DNA: object that represents the DNA </param>
        public void FixDNA(DNA dna)
        {

            for (int i = 0; i < LENGTH; i++)
            {

                if (!Alle.Nucleo.CheckPair(dna.Left[i],dna.Right[i]))
                {

                    int choice = rngesus.Next(1,3);

                    if (choice == 1)
                    {

                        dna.Right[i] = Alle.Nucleo.Opposite(dna.Left[i]);

                    }

                    else
                    {

                        dna.Left[i] = Alle.Nucleo.Opposite(dna.Right[i]);

                    }

                }

            }

        }

        ///<summary>
        /// This method checks the current DNA object and corrects any errors
        ///</summary>
        public void FixDNA()
        {

            for (int i = 0; i < LENGTH; i++)
            {

                if (!Alle.Nucleo.CheckPair(this.Left[i],this.Right[i]))
                {

                    int choice = rngesus.Next(1,3);

                    if (choice == 1)
                    {

                        this.Right[i] = Alle.Nucleo.Opposite(this.Left[i]);

                    }

                    else
                    {

                        this.Left[i] = Alle.Nucleo.Opposite(this.Right[i]);

                    }

                }

            }

        }

        ///<summary>
        /// This method takes one strand of DNA and completes the other half
        ///</summary>
        ///<param name="strand"> List of int: One side of the DNA values </param>
        public void CompleteDNA(List<int> strand)
        {

            if (this.Left == null)
            {

                for (int i = 0; i < LENGTH; i++)
                {

                    this.Left.Add(Alle.Nucleo.Opposite(strand[i]));

                }

            }

            else
            {

                for (int i = 0; i < LENGTH; i++)
                {

                    this.Right.Add(Alle.Nucleo.Opposite(strand[i]));

                }

            }

        }

        ///<summary>
        /// This method returns all of the allele values. It should have a length of NUMALLELES
        ///</summary>
        ///<example>
        /// 3333 -> 12
        ///</example>
        ///<returns>
        /// Returns a List containing all of the alleles condensed into a number
        ///</returns>
        public List<int> GetAllelesvalues()
        {

            List<int> allelevalues = new List<int>();

            int sum = 0;

            for (int i = 0; i < NUMALLELES; i++)
            {

                sum = 0;

                for (int j = Alle.GetNumberOfNucleoInAlleles() * i; j < Alle.GetNumberOfNucleoInAlleles()  * (i+1); j++)
                {

                    sum += Alle.Nucleo.EncodePairValue(this.Left[j],this.Right[j]);                    

                }

                allelevalues.Add(sum);

            }

            return allelevalues;

        }

        ///<summary>
        /// This method returns a list of all the alleles in the strand of DNA
        ///</summary>
        ///<returns>
        /// Returns a list of all the alleles
        ///</returns>
        public List<Allele> GetAlleles()
        {

            List<Allele> alleles = new List<Allele>();
            List<int> ales = new List<int>();

            for (int i = 0; i < NUMALLELES; i++)
            {

                ales.Clear();

                for (int j = Alle.GetNumberOfNucleoInAlleles() * i; j < Alle.GetNumberOfNucleoInAlleles() * (i + 1); j++)
                {

                    ales.Add(Alle.Nucleo.EncodePairValue(this.Left[j],this.Right[j]));

                }

                alleles.Add(new Allele(ales[0],ales[1],ales[2],ales[3]));

            }

            return alleles;

        }

        ///<summary>
        /// This is the override method to display the DNA object
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
        /// The getter method for constant length
        ///</summary>
        ///<returns>
        /// Returns an integer of the constant LENGTH
        ///</returns>
        public int GetLength()
        {

            return LENGTH;

        }

        ///<summary>
        /// The getter method for the constant number of alleles
        ///</summary>
        ///<returns>
        /// Returns an integer of the constant NUMALLELES
        ///</returns>
        public int GetNumberOfAlleles()
        {

            return NUMALLELES;

        }

    }

}