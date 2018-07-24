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
        /// This is the number of Genes in DNA
        ///</summary>
        ///<value>
        /// 13
        ///</value>
        public const int NUMGENES = 13;

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

        public Gene Gene { get; set; }

        public List<Gene> Genes { get; set; }

        ///<summary>
        /// The default constructor. It just sets the strands to null
        ///</summary>
        public DNA()
        {

            this.Left = null;
            this.Right = null;
            this.Gene = new Gene();
            this.Genes = new List<Gene>();

        }

        ///<summary>
        /// This Constructor makes random DNA string based on the given gender
        ///</summary>
        ///<param name="gender"> bool: True for male </param>
        public DNA(bool gender)
        {

            this.Left = new List<int>();
            this.Right = new List<int>();
            this.Gene = new Gene();
            this.Genes = new List<Gene>();

            this.Genes.Add(this.Gene.GenerateGenderGene(gender));

            for (int i = 0; i < NUMGENES; i++)
            {

                if (i != 0)
                {
                    
                    this.Genes.Add(this.Gene.GenerateRandomGene());

                }

                Gene a = Genes[i];
                List<int> codes = a.ToList();

                for (int j = 0; j < Gene.GetNumberOfNucleoInGenes(); j++)
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
            this.Gene = new Gene();
            this.Genes = new List<Gene>();

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

            int allpraise = rngesus.Next(1, 3);

            if (allpraise == 1)
            {

                this.Left = dad;
                this.Right = mom;

            }

            else
            {

                this.Left = mom;
                this.Right = dad;

            }
            
            this.Gene = new Gene();
            this.Genes = new List<Gene>();

            FixDNA(allpraise);

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

            return DecodeDNA(Convert.ToInt32(code));

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

                    codeddna += Gene.Nucleo.EncodePair(dna.Left[i],dna.Right[i]);

                }

                else
                {

                    codeddna += Gene.Nucleo.EncodePair(dna.Left[i],dna.Right[i]) + ",";

                }

            }

            return codeddna;

        }

        ///<summary>
        /// This method checks the given DNA object and corrects any errors
        ///</summary>
        ///<param name="dna"> DNA: object that represents the DNA </param>
        public void FixDNA(DNA dna, int c)
        {

            for (int i = 0; i < LENGTH; i++)
            {

                if (i >= 4 && i <= 7)
                {

                    if (c == 1)
                    {

                        dna.Right[i] = this.Gene.Nucleo.Opposite(dna.Left[i]);

                    }

                    else
                    {

                        dna.Left[i] = this.Gene.Nucleo.Opposite(dna.Right[i]);

                    }

                }

                else if (!this.Gene.Nucleo.CheckPair(dna.Left[i],dna.Right[i]))
                {

                    int choice = rngesus.Next(1,3);

                    if (choice == 1)
                    {

                        dna.Right[i] = Gene.Nucleo.Opposite(dna.Left[i]);

                    }

                    else
                    {

                        dna.Left[i] = Gene.Nucleo.Opposite(dna.Right[i]);

                    }

                }

            }

        }

        ///<summary>
        /// This method checks the current DNA object and corrects any errors
        ///</summary>
        public void FixDNA(int c)
        {

            FixDNA(this,c);

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

                    this.Left.Add(Gene.Nucleo.Opposite(strand[i]));

                }

            }

            else
            {

                for (int i = 0; i < LENGTH; i++)
                {

                    this.Right.Add(Gene.Nucleo.Opposite(strand[i]));

                }

            }

        }

        ///<summary>
        /// This method returns all of the Gene values. It should have a length of NUMGeneS
        ///</summary>
        ///<example>
        /// 3333 -> 12
        ///</example>
        ///<returns>
        /// Returns a List containing all of the Genes condensed into a number
        ///</returns>
        public List<int> GetGenesvalues()
        {

            List<int> Genevalues = new List<int>();

            int sum = 0;

            for (int i = 0; i < NUMGENES; i++)
            {

                sum = 0;

                for (int j = Gene.GetNumberOfNucleoInGenes() * i; j < Gene.GetNumberOfNucleoInGenes()  * (i+1); j++)
                {

                    sum += Gene.Nucleo.EncodePairValue(this.Left[j],this.Right[j]);                    

                }

                Genevalues.Add(sum);

            }

            return Genevalues;

        }

        ///<summary>
        /// This method returns a list of all the Genes in the strand of DNA
        ///</summary>
        ///<returns>
        /// Returns a list of all the Genes
        ///</returns>
        public List<Gene> GetGenes()
        {

            List<Gene> Genes = new List<Gene>();
            List<int> ales = new List<int>();

            for (int i = 0; i < NUMGENES; i++)
            {

                ales.Clear();

                for (int j = Gene.GetNumberOfNucleoInGenes() * i; j < Gene.GetNumberOfNucleoInGenes() * (i + 1); j++)
                {

                    ales.Add(Gene.Nucleo.EncodePairValue(this.Left[j],this.Right[j]));

                }

                Genes.Add(new Gene(ales[0],ales[1],ales[2],ales[3]));

            }

            return Genes;

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
        /// The getter method for the constant number of Genes
        ///</summary>
        ///<returns>
        /// Returns an integer of the constant NUMGeneS
        ///</returns>
        public int GetNumberOfGenes()
        {

            return NUMGENES;

        }

    }

}