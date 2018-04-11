using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class Program
    {

        static void Main(string[] args)
        {

            CharMenu();

        }

        public static void CharMenu()
        {

            int choice;

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("1. Generate Character by DNA");
            Console.WriteLine("2. Generate Child from Parent DNA.");
            Console.WriteLine("3. Show all races and mods");
            Console.WriteLine("4. Generate and Display all Unique Alleles");
            Console.WriteLine("5. Generate and Display all Unique Sums of Alleles");
            Console.WriteLine("6. Return to the main menu");
            Console.Write("Please enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (choice == 1)
            {

                string cdna;

                Console.WriteLine("");

                Console.Write("Please enter the DNA string: ");

                cdna = Console.ReadLine();

                DNA dna = new DNA(cdna);

                Character chara = new Character(dna);

                Console.WriteLine(chara.ToString());

                Console.ReadKey();

                CharMenu();


            }

            else if (choice == 2)
            {

                Console.WriteLine("");

                //Console.Write("Please enter the number of generations: ");

                //int choice2 = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Dad Dna: ");
                string cddna = Console.ReadLine();

                Console.Write("Enter Mom DNA: ");
                string cmdna = Console.ReadLine();

                DNA ddna = new DNA(cddna);
                DNA mdna = new DNA(cmdna);

                Character dad = new Character(ddna);
                Character mom = new Character(mdna);

                Console.WriteLine("\n" + dad.ToString());
                Console.WriteLine("\n" + mom.ToString());

                List<int> dstrand = dad.Dna.Mieosis();
                List<int> mstrand = mom.Dna.Mieosis();

                DNA cdna = new DNA(dstrand, mstrand);

                Character child = new Character(cdna, dad);

                Console.WriteLine("\n" + child.ToString());

                //Console.WriteLine(child.Dna.ToString());

                Console.ReadKey();

                CharMenu();

            }

            else if (choice == 3)
            {

                Console.WriteLine("");

                Race r = new Race();

                foreach (Race ra in r.Races)
                {

                    Console.WriteLine(ra.ToString());

                }

                Console.ReadKey();
                CharMenu();

            }

            else if (choice == 4)
            {

                Console.WriteLine("\nGenerating...");
                DNAMath dnam = new DNAMath();

                List<Allele> ualle = dnam.GenerateAllPossibleUniqueAlleles();

                Console.WriteLine(dnam.AlleleListToString(ualle));
                Console.WriteLine("\nTotal Alleles: " + ualle.Count);

                Console.ReadKey();
                CharMenu();

            }

            else if (choice == 5)
            {

                Console.WriteLine("\nGenerating...");
                DNAMath dnam = new DNAMath();

                List<Allele> ualle = dnam.GenerateAllPossibleUniqueAlleles();

                List<int> sums = dnam.GenerateAllPossibleUniqueAlleleSums(ualle);

                for (int i = 0; i < sums.Count; i++)
                {

                    Console.WriteLine(sums[i]);

                }

                Console.WriteLine("\nTotal Sums: " + sums.Count);

                Console.ReadKey();
                CharMenu();

            }

            else if (choice == 6)
            {

                Console.WriteLine("Exiting....");
                Console.ReadKey();

            }

            else
            {

                Console.WriteLine("Please enter a valid choice");

                CharMenu();

            }

        }

    }

}
