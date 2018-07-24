using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class DNAMath
    {

        public List<Gene> AllAlleles { get; set; }

        public List<int> AllAlleleSums { get; set; }

        public List<Gene> GenerateAllPossibleUniqueAlleles()
        {

            List<Gene> alleles = new List<Gene>();

            for (int i = 0; i < 4; i++)
            {

                for (int j = i; j < 4; j++)
                {

                    for (int k = j; k < 4; k++)
                    {

                        for (int l = k; l < 4; l++)
                        {

                            int one, two, three, four;

                            one = ConvertiToCDNA(i);
                            two = ConvertiToCDNA(j);
                            three = ConvertiToCDNA(k);
                            four = ConvertiToCDNA(l);

                            Gene a = new Gene(one,two,three,four);

                            alleles.Add(a);
                            
                        }

                    }

                }

            }

            return alleles;

        }

        public List<int> GenerateAllPossibleUniqueAlleleSums(List<Gene> alleles)
        {

            List<int> allsums = new List<int>();

            for (int i = 0; i < alleles.Count; i++)
            {

                int sum = 0;

                Gene allele = alleles[i];

                sum += allele.One + allele.Two + allele.Three + allele.Four;

                allsums.Add(sum);

            }

            return allsums.Distinct().ToList();

        }

        public int ConvertiToCDNA(int val)
        {

            if (val == 0)
            {

                return 3;

            }

            else if (val == 1)
            {

                return 4;

            }

            else if (val == 2)
            {

                return 6;

            }

            else if (val == 3)
            {

                return 8;

            }

            return -10;

        }

        public string AlleleToString(Gene a)
        {

            return Convert.ToString(a.One + "" + a.Two + "" + a.Three + "" + a.Four);

        }

        public string AlleleListToString(List<Gene> a)
        {

            string str = "";

            for (int i = 0; i < a.Count; i++)
            {

                str += AlleleToString(a[i]) + "\n";

            }

            return str;
            
        }

    }

}
