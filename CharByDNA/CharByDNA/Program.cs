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
            Console.WriteLine("Welcome to the character builder");
            Console.WriteLine("");
            Console.WriteLine("1. Generate Character by DNA");
            Console.WriteLine("2. Generate Child from Parent DNA.");
            Console.WriteLine("3. Generate random character from gender.");
            Console.WriteLine("4. Generate child from random parents.");
            Console.WriteLine("5. Generate Random Parents and then Child");
            Console.WriteLine("6. Show all races and mods");
            Console.WriteLine("7. Exit");
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

                Character dad = new Character(new DNA(cddna));
                Character mom = new Character(new DNA(cmdna));

                Console.WriteLine("\n" + dad.ToString());
                Console.WriteLine("\n" + mom.ToString());

                Character child = new Character(dad,mom);

                Console.WriteLine("\n" + child.ToString());

                Console.WriteLine(child.Dna.ToString());

                Console.ReadKey();

                CharMenu();

            }

            else if (choice == 3)
            {

                Console.Write("Please enter a gender: ");
                string gen = Console.ReadLine();
                bool gender;

                List<char> input = gen.ToList<char>();

                if (input[0] == 'm' || input[0] == 'M')
                {

                    gender = true;

                }

                else
                {

                    gender = false;

                }

                Character a = new Character(gender);

                Console.WriteLine("\n" + a.ToString());

                Console.ReadKey();
                CharMenu();

            }

            else if (choice == 4)
            {

                bool end = false;

                //Console.WriteLine("Generating Dad...");
                Character dad = new Character(true);
                

                //Console.WriteLine("\nGenerating Mom...");
                Character mom = new Character(false);

                while(!end)
                {

                    Console.WriteLine("\nDad:\n" + dad.ToString());
                    Console.WriteLine("\nMom:\n" + mom.ToString());

                    Console.WriteLine("\nCreating Child");
                    Character child = new Character(dad, mom);
                    Console.WriteLine("\n" + child.ToString());

                    Console.Write("\nend? (y/n): ");
                    string chc = Console.ReadLine();

                    if (chc.Contains('y') || chc.Contains('Y'))
                    {

                        end = true;

                    }

                    else
                    {

                        Console.Clear();

                    }

                }

                CharMenu();

            }

            else if (choice == 5)
            {

                bool end = false;

                while (!end)
                {

                    Character dad = new Character(true);
                    Console.WriteLine("\nDad:\n" + dad.ToString());
                    Character mom = new Character(false);
                    Console.WriteLine("\nMom:\n" + mom.ToString());

                    Console.WriteLine("\nCreating Child");
                    Character child = new Character(dad, mom);
                    Console.WriteLine("\n" + child.ToString());

                    Console.Write("\nend? (y/n): ");
                    string chc = Console.ReadLine();

                    if (chc.Contains('y') || chc.Contains('Y'))
                    {

                        end = true;

                    }

                    else
                    {

                        Console.Clear();

                    }

                }

                CharMenu();

            }

            else if (choice == 6)
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

            else if (choice == 7)
            {

                Console.WriteLine("Exiting....");
                Console.ReadKey();

            }

            else
            {

                Console.WriteLine("Please enter a valid choice");
                Console.ReadKey();

                CharMenu();

            }

        }

    }

}
