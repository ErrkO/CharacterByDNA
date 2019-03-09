using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    ///<summary>
    /// The class that contains the information for the blocks of nucleotides
    ///</summary>
    public static class GeneStatic
    {
        
        ///<summary>
        /// This is the number of nucleotides in an allele
        ///</summary>
        ///<value>
        /// 3
        ///</value>
        private const int NUMINGENE = 3;

        ///<summary>
        /// The method that takes a gender value and generates 4 random codes that fit that gender
        ///</summary>
        ///<param name="gender"> bool: True for male </param>
        ///<returns>
        /// Returns an allele object
        ///</returns>
        public static string GenerateGenderGene(bool gender, Random rngesus)
        {

            int one, two, three, num;
            
            bool correctstring = false;

            if (gender)
            {

                do
                {

                    one = rngesus.Next(1, 4);
                    two = rngesus.Next(1, 4);
                    three = rngesus.Next(1, 4);

                    num = (one*100) + (two*10) + three;

                    if (num % 2 == 1)
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

                    num = (one * 100) + (two * 10) + three;

                    if (num % 2 == 0)
                    {

                        if (num != 111 || num != 444)
                        {

                            correctstring = true;

                        }

                    }

                } while (!correctstring);

            }

            return num.ToString();

        }

        ///<summary>
        /// The method that generates a random allele
        ///</summary>
        ///<returns>
        /// Returns an gene object
        ///</returns>
        public static string GenerateRandomGene(Random rngesus)
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

            int num = (one*100) + (two*10) + three;

            return num.ToString();          

        }

        public static int ToInt(string gene)
        {

            return (Convert.ToInt32(gene[0]) * 100) +(Convert.ToInt32(gene[1]) * 10) + Convert.ToInt32(gene[2]);

        }

        public static int ToValue(string gene)
        {

            return CodonStatic.TranslateCodon(gene);

        }

        ///<summary>
        /// The getter method for the constant number nucleotides in alleles
        ///</summary>
        ///<returns>
        /// Returns an integer of the constant NUMINALES
        ///</returns>
        public static int GetNumberOfNucleoInGenes()
        {

            return NUMINGENE;

        }

        public static List<string> ToList(string Dna)
        {

            List<string> Genes = new List<string>();
            string tempgene;

            for (int i = 0; i < Dna.Length/3; i++)
            {

                tempgene = "";
                tempgene += Dna[(i*3)+0];
                tempgene += Dna[(i*3)+1];
                tempgene += Dna[(i*3)+2];

                Genes.Add(tempgene); 

            }

            return Genes;

        }

    }

}
