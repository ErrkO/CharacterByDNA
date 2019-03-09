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
    public static class DNAStatic
    {

        ///<summary>
        /// This is the number of nucleotides in a strand of DNA
        ///</summary>
        ///<value>
        /// 78
        ///</value>
        private const int LENGTH = 78;

        ///<summary>
        /// This is the number of Genes in DNA
        ///</summary>
        ///<value>
        /// 26
        ///</value>
        private const int NUMGENES = 26;

        public static string CreateChildsDNA(string dad, string mom)
        {

            string tempDNA = "";

            List<string> DGenes = GeneStatic.ToList(dad);
            List<string> MGenes = GeneStatic.ToList(mom);

            for (int i = 0; i < this.LENGTH/2; i++)
            {

                tempDNA += DGenes[i];

                tempDNA += MGenes[i];

            }

            return tempDNA;

        }

        public static string NewDNA(Random rngesus)
        {

            int gender = rngesus.Next(0, 2);

            return NewDNA(gender,rngesus);

        }

        public static string NewDNA(bool gender, Random rngesus)
        {

            DNAStrand = "";

            if (gender == 0)
            {

                DNAStrand += GeneStatic.GenerateGenderGene(Convert.ToBoolean(gender));
                DNAStrand += GeneStatic.GenerateGenderGene(Convert.ToBoolean(gender));

                this.DNAStrand += this.Genes[0].ToString() + this.Genes[1].ToString();

            }

            else if (gender == 1)
            {

                DNAStrand += GeneStatic.GenerateGenderGene(Convert.ToBoolean(gender));
                DNAStrand += GeneStatic.GenerateGenderGene(Convert.ToBoolean(0));

            }

            else
            {

                Console.WriteLine("The Dna Constructor is bugged");

            }

            for (int i = 0; i < NUMGENES-2; i++)
            {

                DNAStrand += GeneStatic.GenerateRandomGene(rngesus);

            }

            return DNAstrand;

        }

        public static string Miosis(string dna, Random rngesus)
        {

            List<string> genes = GeneStatic.ToList(dna);
            List<string> traitstopass = new List<string>();

            int choice;

            for (int i = 0; i < NUMGENES/2; i++ )
            {

                choice = rngesus.Next(1,3);

                if (choice == 1)
                {

                    traitstopass.Add(genes[i*2]);

                }
                else
                {

                    traitstopass.Add(genes[i*2]+1);

                }

            }

            return traitstopass;

        }
        
        public static List<int> GetGeneValues(string dna)
        {

            List<int> genevals = new List<int>();
            List<string> genes = GeneStatic.ToList(dna);

            for (int i = 0; i < NUMGENES; i++)
            {

                genevals.Add(CodonStatic.TranslateCodon(genes[i]));

            }

            return genevals;

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