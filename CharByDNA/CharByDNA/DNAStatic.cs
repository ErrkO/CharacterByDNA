using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/DNAStatic/*'/>
    public static class DNAStatic
    {

        /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/Length/*'/>
        public int Length { get { return 78; } }

        /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/Numgenes/*'/>
        public int Numgenes { get { return 26; } }

        /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/CreateChildsDNA/*'/>
        public static string CreateChildsDNA(string parent1, string parent2)
        {

            string tempDNA = "";

            List<string> p1Genes = GeneStatic.ToList(parent1);
            List<string> p2Genes = GeneStatic.ToList(parent2);

            for (int i = 0; i < this.Length/2; i++)
            {

                tempDNA += p1Genes[i];

                tempDNA += p2Genes[i];

            }

            return tempDNA;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/NewDNA1/*'/>
        public static string NewDNA(Random rngesus)
        {

            int gender = rngesus.Next(0, 2);

            return NewDNA(gender,rngesus);

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/NewDNA2/*'/>
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

            for (int i = 0; i < this.Numgenes-2; i++)
            {

                DNAStrand += GeneStatic.GenerateRandomGene(rngesus);

            }

            return DNAstrand;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/Miosis/*'/>
        public static string Miosis(string dna, Random rngesus)
        {

            List<string> genes = GeneStatic.ToList(dna);
            List<string> traitstopass = new List<string>();

            int choice;

            for (int i = 0; i < this.Numgenes/2; i++ )
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
        
        /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/GetGeneValues/*'/>
        public static List<int> GetGeneValues(string dna)
        {

            List<int> genevals = new List<int>();
            List<string> genes = GeneStatic.ToList(dna);

            for (int i = 0; i < this.Numgenes; i++)
            {

                genevals.Add(CodonStatic.TranslateCodon(genes[i]));

            }

            return genevals;

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/GetGenePairValue/*'/>
        public static int GetGenePairValue(string dna, int position)
        {

            int gene1, gene2;

            if (position % 2 == 0)
            {

                gene1 = GeneStatic.IGetGene(dna,position);
                gene2 = GeneStatic.IGetGene(dna,position + 1);

            }

            else
            {

                gene1 = GeneStatic.IGetGene(dna,position - 1);
                gene2 = GeneStatic.IGetGene(dna,position);

            }

            if (position == 1 || position == 2)
            {

                
				if (gene1 % 2 == 0 && gene2 % 2 == 0)
				{

					return 0;

				}

				else if ((gene1 % 2 == 1 && gene2 % 2 == 0) || (gene1 % 2 == 0 && gene2 % 2 == 1))
				{

					return 1;

				}

				else
				{

					return -1;

				}


            }

			return (Math.Floor((gene1 + gene2)/2));

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="dnastatic"]/ToList/*'/>
        public static List<string> ToList(string dna)
        {

            return GeneStatic.ToList(dna);

        }

    }

}