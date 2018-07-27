﻿using System;
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
        /// 78
        ///</value>
        public const int LENGTH = 78;

        ///<summary>
        /// This is the number of Genes in DNA
        ///</summary>
        ///<value>
        /// 36
        ///</value>
        public const int NUMGENES = 26;

        /// <summary>
        /// 
        /// </summary>
        public string DNAStrand;

        ///<summary>
        /// The random number generator
        ///</summary>
        ///<remarks>
        /// All Praise RNGESUS!!!
        ///</remarks>
        private Random rngesus = new Random();

        /// <summary>
        /// 
        /// </summary>
        public Gene Gene { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Gene> Genes { get; set; }

        // To be Changed
        ///<summary>
        /// The default constructor. It just sets the strands to null
        ///</summary>
        public DNA()
        {

            this.DNAStrand = "";
            this.Gene = new Gene();
            this.Genes = new List<Gene>();

            for (int i = 0; i < NUMGENES; i++)
            {

                this.DNAStrand += this.Gene.GenerateRandomGene().ToString();

            }

        }

        ///<summary>
        /// This Constructor makes random DNA string based on the given gender
        ///</summary>
        ///<param name="gender"> bool: True for male </param>
        public DNA(bool gender)
        {

            this.DNAStrand = "";
            this.Gene = new Gene();
            this.Genes = new List<Gene>();

            this.Genes.Add(this.Gene.GenerateGenderGene(gender));
            
            if (gender)
            {

                this.Genes.Add(this.Gene.GenerateGenderGene(!gender));

            }

            else
            {

                this.Genes.Add(this.Gene.GenerateGenderGene(gender));

            }

            for (int i = 0; i < NUMGENES; i++)
            {

                if (i != 0)
                {
                    
                    this.Genes.Add(this.Gene.GenerateRandomGene());

                }

                this.DNAStrand += this.Genes[i].ToString();

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dnastrand"></param>
        public DNA(string dnastrand)
        {

            this.Gene = new Gene();
            this.Genes = new List<Gene>();

            this.DNAStrand = dnastrand;

            List<char> nucleotides = dnastrand.ToCharArray().ToList<char>();

            for (int i = 0; i < NUMGENES; i++)
            {

                Gene temp = new Gene(nucleotides[(i * 3) + 0], nucleotides[(i * 3) + 1], nucleotides[(i * 3) + 2]);
                this.Genes.Add(temp);

            }

        }

        ///<summary>
        /// This constructor builds the dna object from the DNA of the dad and mom strands
        ///</summary>
        ///<param name="dad"> List of int: the strand of nucleotides from the father </param>
        ///<param name="mom"> List of int: the strand of nucleotides from the mother </param>
        public DNA(List<Gene> dad, List<Gene> mom)
        {

            this.Genes = new List<Gene>();

            for (int i = 0; i < NUMGENES; i++)
            {

                if (i % 2 == 0)
                {

                    this.Genes.Add(dad[i]);

                }

                else
                {

                    this.Genes.Add(mom[i]);

                }

                this.DNAStrand += this.Genes[i].ToString();

            }

            Mutate();

        }

        ///<summary>
        /// This method randomly selects a strand of dna to give for reproduction
        ///</summary>
        ///<returns>
        /// Returns a list of integers representing the strand of DNA
        ///</returns>
        public List<Gene> Miosis()
        {

            List<Gene> traitstopass = new List<Gene>();

            int choice;

            for (int i = 0; i < NUMGENES/2; i++)
            {

                choice = rngesus.Next(1, 3);

                if (choice == 1)
                {

                    traitstopass.Add(this.Genes[i * 2]);

                }

                else
                {

                    traitstopass.Add(this.Genes[(i * 2) + 1]);

                }

            }

            return traitstopass;

        }

        // To Change

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

        // To Change

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

        private void Mutate()
        {

            int allpraise = rngesus.Next(1, 10000);
            int pos = rngesus.Next(0, LENGTH);
            int newbase = rngesus.Next(1, 4);

            List<char> tempstrand = this.DNAStrand.ToCharArray().ToList<char>();

            if (allpraise == 500)
            {

                string bse = newbase.ToString();

                tempstrand[pos] = Char.Parse(bse);

                this.DNAStrand = tempstrand.ToString();

            }

        }

        public List<int> GetGeneValues()
        {

            List<int> genevals = new List<int>();

            for (int i = 0; i < NUMGENES; i++)
            {

                genevals.Add(this.Gene.Cdn.TranslateCodon(this.Genes[i]));

            }

            return genevals;

        }

        ///<summary>
        /// This is the override method to display the DNA object
        ///</summary>
        public override string ToString()
        {

            return this.DNAStrand;

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