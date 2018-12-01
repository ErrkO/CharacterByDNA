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

            Database db = new Database();

            CharMenu(db);

        }

        public static void CharMenu(Database db)
        {

            int choice;

            CharacterDB cdb = new CharacterDB(db);

            //Console.Clear();
            Console.WriteLine("Welcome to the character builder");
            Console.WriteLine("");
            Console.WriteLine("1. Generate Character by DNA");
            Console.WriteLine("2. Generate Child from Parent DNA.");
            Console.WriteLine("3. Generate random character from gender.");
            Console.WriteLine("4. Generate child from random parents.");
            Console.WriteLine("5. Generate Random Parents and then Child");
            Console.WriteLine("6. Show all races and mods");
            Console.WriteLine("7. Run Life Sim");
            Console.WriteLine("8. Fill DB with Characters");
            Console.WriteLine("9. Test EmptyDB");
            Console.WriteLine("10. Create CSV files");
            Console.WriteLine("11. Exit");
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

                Character chara = new Character(db,dna,new GTime(),0);

                Console.WriteLine(chara.ToString());

                Console.ReadKey();

                CharMenu(db);


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

                Character dad = new Character(db,new DNA(cddna),new GTime(),0);
                Character mom = new Character(db,new DNA(cmdna),new GTime(),1);

                Console.WriteLine("\n" + dad.ToString());
                Console.WriteLine("\n" + mom.ToString());

                Character child = new Character(db,dad,mom,new GTime(),2);

                Console.WriteLine("\n" + child.ToString());

                Console.WriteLine(child.Dna.ToString());

                Console.ReadKey();

                CharMenu(db);

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

                Character a = new Character(db,new DNA(gender),new GTime(),0);

                Console.WriteLine("\n" + a.ToString());

                Console.ReadKey();
                CharMenu(db);

            }

            else if (choice == 4)
            {

                bool end = false;

                int numchars = 0;

                //Console.WriteLine("Generating Dad...");
                Character dad = new Character(db,new DNA(true),new GTime(),numchars);
                numchars++;

                //Console.WriteLine("\nGenerating Mom...");
                Character mom = new Character(db,new DNA(false),new GTime(),numchars);
                numchars++;

                while(!end)
                {

                    Console.WriteLine("\nDad:\n" + dad.ToString());
                    Console.WriteLine("\nMom:\n" + mom.ToString());

                    Console.WriteLine("\nCreating Child");
                    Character child = new Character(db,new DNA(dad.Dna.Miosis(), mom.Dna.Miosis()),new GTime(), numchars);
                    numchars++;
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

                CharMenu(db);

            }

            else if (choice == 5)
            {

                bool end = false;
                int numchars = 0;

                while (!end)
                {

                    Character dad = new Character(db,new DNA(true), new GTime(),numchars);
                    numchars++;
                    Console.WriteLine("\nDad:\n" + dad.ToString());
                    Character mom = new Character(db,new DNA(false),new GTime(),numchars);
                    numchars++;
                    Console.WriteLine("\nMom:\n" + mom.ToString());

                    Console.WriteLine("\nCreating Child");
                    Character child = new Character(db,new DNA(dad.Dna.Miosis(), mom.Dna.Miosis()),new GTime(),numchars);
                    numchars++;
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

                CharMenu(db);

            }

            else if (choice == 6)
            {

                Console.WriteLine("");

                Race r = new Race(db);

                List<Race> races = r.GetAllRaces();

                foreach (Race ra in races)
                {

                    Console.WriteLine(ra.ToString());

                }

                Console.ReadKey();
                CharMenu(db);

            }

            else if (choice == 7)
            {

                Console.Write("Please enter a starting population: ");
                int startpop = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please enter the max years: ");
                int maxyears = Convert.ToInt32(Console.ReadLine());

                LifeSimulator sim = new LifeSimulator(db,startpop, maxyears);

                Console.ReadKey();
                CharMenu(db);

            }

            else if (choice == 8)
            {

                Console.Write("Enter the number of characters to genereate: ");
                int numchars = Convert.ToInt32(Console.ReadLine());

                List<Character> chars = new List<Character>();

                GTime time = new GTime();

                for (int i = 0; i < numchars; i++)
                {

                    Character chara = new Character(db,new DNA(),time,i);

                    chars.Add(chara);

                }

                cdb.SaveListOfCharacters(chars);

                CharMenu(db);

            }

            else if(choice == 9)
            {

                List<Character> chars = new List<Character>();

                chars.AddRange(cdb.GetAllCharacters());

                Console.WriteLine(chars);

                Console.ReadKey();

            }

            else if (choice == 10)
            {

                NameDB nm = new NameDB(db);
                //nm.NumberedFile();
                CharMenu(db);

            }

            else if (choice == 11)
            {

                Console.WriteLine("Exiting....");
                Console.ReadKey();

            }

            else
            {

                Console.WriteLine("Please enter a valid choice");
                Console.ReadKey();

                CharMenu(db);

            }

        }

    }

}
