using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CharByDNA
{

    public class Name
    {

        List<string> ffnames,mfnames,lnames;
        string fname, lname,fullname;
        Random rngesus = new Random();
        string ffcon = "FFNames.txt";
        string mfcon = "MFNames.txt";
        string lcon = "LNames.txt";

        public Name()
        {

            this.ffnames = File.ReadLines(ffcon).ToList();
            this.mfnames = File.ReadLines(mfcon).ToList();
            this.lnames = File.ReadLines(lcon).ToList();

        }

        public string GenName(string gender)
        {

            if (gender == "Female")
            {

                fname = ffnames[ rngesus.Next(0, ffnames.Count)];

            }

            else
            {

                fname = mfnames[rngesus.Next(0, mfnames.Count)];

            }

            lname = lnames[rngesus.Next(0, ffnames.Count)];

            fullname = fname + " " + lname;

            return fullname;

        }

        public string GenFname(string gender)
        {

            string fname;

            if (gender == "Female")
            {

                fname = ffnames[ rngesus.Next(0, ffnames.Count)];

            }

            else
            {

                fname = mfnames[rngesus.Next(0, mfnames.Count)];

            }

            return fname;

        }

        public string GenLname()
        {

            return lnames[rngesus.Next(0, ffnames.Count)];

        }

    }

}
