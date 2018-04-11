using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CharByDNA
{

    ///<summary>
    /// A class that contains all of the names that will be used for now
    ///</summary>
    public class Names
    {

        ///<summary>
        /// The random number generator
        ///</summary>
        ///<remarks>
        /// All Praise RNGESUS!!!
        ///</remarks>
        Random rngesus = new Random();

        ///<summary>
        /// The filename that contains the female first names
        ///</summary>
        string ffcon = "FFNames.txt";

        ///<summary>
        /// The filename that contains the male first names
        ///</summary>
        string mfcon = "MFNames.txt";

        ///<summary>
        /// The filename that contains the last names
        ///</summary>
        string lcon = "LNames.txt";

        ///<summary>
        /// The List that contains all of the female first names
        ///</summary>
        private List<string> Ffnames { get; set; }

        ///<summary>
        /// the list that contains all of the male first names
        ///</summary>
        private List<string> Mfnames { get; set; }

        ///<summary>
        /// The list that contains all of the last names
        ///</summary>
        private List<string> Lnames { get; set; }

        ///<summary>
        /// The default constructor. It fills the lists with their respective values
        ///</summary>
        public Names()
        {

            this.Ffnames = File.ReadLines(ffcon).ToList();
            this.Mfnames = File.ReadLines(mfcon).ToList();
            this.Lnames = File.ReadLines(lcon).ToList();

        }

        ///<summary>
        /// This Method generates a first name based on a given gender
        ///</summary>
        ///<value>
        /// accepts a value for the gender
        ///</value>
        ///<returns>
        /// returns a string contianing the first name
        ///</returns>
        ///<remarks>
        /// debating on whether I should change gender to a boolean
        ///</remarks>
        public string GenFname(string gender)
        {

            string fname;

            if (gender == "Female")
            {

                fname = this.Ffnames[ rngesus.Next(0, this.Ffnames.Count)];

            }

            else
            {

                fname = this.Mfnames[rngesus.Next(0, this.Mfnames.Count)];

            }

            return fname;

        }

        ///<summary>
        /// This method generates a last name
        ///</summary>
        ///<returns>
        /// returns a string containing the last name
        ///</returns>
        public string GenLname()
        {

            return this.Lnames[rngesus.Next(0, this.Lnames.Count)];

        }

    }

}
