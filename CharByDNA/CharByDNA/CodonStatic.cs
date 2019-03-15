using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    /// <include file='Documentation.xml' path='Documentation/members[@name="codonstatic"]/CodonStatic/*'/>
    public static class CodonStatic
    {
        
        /// <include file='Documentation.xml' path='Documentation/members[@name="codonstatic"]/TranslateCodon/*'/>
        public static int TranslateCodon(int codon)
        {

            if (codon == 111 || codon == 444)
            {

                return 0;

            }

            else if (codon == 112 || codon == 211 || codon == 344 || codon == 443)
            {

                return 1;

            }

            else if (codon == 113 || codon == 212 || codon == 343 || codon == 442)
            {

                return 2;

            }

            else if (codon == 114 || codon == 213 || codon == 342 || codon == 441)
            {

                return 3;

            }

            else if (codon == 121 || codon == 214 || codon == 341 || codon == 434)
            {

                return 4;

            }

            else if (codon == 122 || codon == 221 || codon == 334 || codon == 433)
            {

                return 5;

            }

            else if (codon == 123 || codon == 222 || codon == 333 || codon == 432)
            {

                return 6;

            }

            else if (codon == 124 || codon == 223 || codon == 332 || codon == 431)
            {

                return 7;

            }

            else if (codon == 131 || codon == 224 || codon == 331 || codon == 424)
            {

                return 8;

            }

            else if (codon == 132 || codon == 231 || codon == 324 || codon == 423)
            {

                return 9;

            }

            else if (codon == 133 || codon == 232 || codon == 323 || codon == 422)
            {

                return 10;

            }

            else if (codon == 134 || codon == 233 || codon == 322 || codon == 421)
            {

                return 11;

            }

            else if (codon == 141 || codon == 234 || codon == 321 || codon == 414)
            {

                return 12;

            }

            else if (codon == 142 || codon == 241 || codon == 314 || codon == 413)
            {

                return 13;

            }

            else if (codon == 143 || codon == 242 || codon == 313 || codon == 412)
            {

                return 14;

            }

            else if (codon == 144 || codon == 243 || codon == 312 || codon == 411)
            {

                return 15;

            }

            else if (codon == 244 || codon == 311)
            {

                return 16;

            }

            else
            {

                return -1;

            }

        }

        /// <include file='Documentation.xml' path='Documentation/members[@name="codonstatic"]/ToList/*'/>
        public static List<int> ToList(string gene)
        {

            return new List<int>() {gene[0], gene[1], gene[2]};

        }

    }
}
