using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    public struct Tuple
    {

        public int left;
        public int right;

        public Tuple(int one, int two)
        {

            this.left = one;
            this.right = two;

        }

    }
    public class DNA
    {

        public const int LENGTH = 52;

        private Random rngesus = new Random();

        public List<int> Left {get;set;}
        public List<int> Right {get;set;}

        public DNA()
        {

            this.Left = null;
            this.Right = null;

        }

        public DNA(string codeddna)
        {

            this.Left = new List<int>();
            this.Right = new List<int>();

            List<string> codelist = codeddna.Split(',').ToList();

            for (int i = 0; i < LENGTH; i++)
            {

                Tuple tup = DecodeDNA(codelist[i]);

                this.Left.Add(tup.left);

                this.Right.Add(tup.right);

            }

        }

        public DNA(List<int> dad, List<int> mom)
        {

            this.Left = dad;
            this.Right = mom;

            FixDNA();

        }

        public List<int> Mieosis()
        {

            int choice = rngesus.Next(1,3);

            if (choice == 1)
            {

                return this.Left;

            }

            else
            {

                return this.Right;

            }

        }

        public bool CheckPair(int pos)
        {

            if (this.Left[pos] == 1 && this.Right[pos] == 3)
            {

                return true;

            }

            else if (this.Left[pos] == 2 &&  this.Right[pos] == 4)
            {

                return true;

            }

            else if (this.Left[pos] == 3 &&  this.Right[pos] == 1)
            {

                return true;

            }

            else if (this.Left[pos] == 4 &&  this.Right[pos] == 2)
            {

                return true;

            }

            return false;

        }

        public bool CheckPair(int left, int right)
        {

            if (left == 1 && right == 3)
            {

                return true;

            }

            else if (left == 2 &&  right == 4)
            {

                return true;

            }

            else if (left == 3 &&  right == 1)
            {

                return true;

            }

            else if (left == 4 && right == 2)
            {

                return true;

            }

            return false;

        }

        public int Opposite(int half)
        {

            if (half == 1)
            {

                return 3;

            }

            else if (half == 2)
            {

                return 4;

            }

            else if (half == 3)
            {

                return 1;

            }

            return 2;

        }

        public Tuple DecodeDNA(string code)
        {

            Tuple tup = new Tuple(0,0);

            if (code == "3")
            {

                tup = new Tuple(3,1);

            }

            else if (code == "4")
            {

                tup = new Tuple(1,3);

            }

            else if (code == "6")
            {

                tup = new Tuple(2,4);
                
            }

            else if (code == "8")
            {

                tup = new Tuple(4,2);
                
            }

            return tup;

        }

        public String EncodePair(int left, int right)
        {

            if (CheckPair(left,right))
            {

                if (left > right)
                {

                    int code = left*right;

                    return code.ToString();

                }

                else
                {

                    int code = left+right;

                    return code.ToString();

                }

            }

            return null;

        }

        public int EncodePairValue(int left, int right)
        {

            if (CheckPair(left,right))
            {

                if (left > right)
                {

                    return left*right;

                }

                else
                {

                    return left+right;

                }

            }

            return 0;


        }

        public String EncodeDNA(DNA dna)
        {

            string codeddna = "";

            for (int i = 0; i < LENGTH; i++)
            {

                if (i == LENGTH - 1)
                {

                    codeddna += EncodePair(dna.Left[i],dna.Right[i]);

                }

                else
                {

                    codeddna += EncodePair(dna.Left[i],dna.Right[i]) + ",";

                }

            }

            return codeddna;

        }

        public void FixDNA(DNA dna)
        {

            for (int i = 0; i < LENGTH; i++)
            {

                if (!CheckPair(i))
                {

                    int choice = rngesus.Next(1,3);

                    if (choice == 1)
                    {

                        dna.Right[i] = Opposite(dna.Left[i]);

                    }

                    else
                    {

                        dna.Left[i] = Opposite(dna.Right[i]);

                    }

                }

            }

        }

        public void FixDNA()
        {

            for (int i = 0; i < LENGTH; i++)
            {

                if (!CheckPair(i))
                {

                    int choice = rngesus.Next(1,3);

                    if (choice == 1)
                    {

                        this.Right[i] = Opposite(this.Left[i]);

                    }

                    else
                    {

                        this.Left[i] = Opposite(this.Right[i]);

                    }

                }

            }

        }

        public void CompleteDNA(List<int> strand)
        {

            if (this.Left == null)
            {

                this.Left = strand;

            }

            else
            {

                this.Right = strand;

            }

        }

        public override string ToString()
        {

            string dna = "";

            for (int i = 0; i < LENGTH; i++)
            {

                dna += this.Left[i] + " - " + this.Right[i] + "\n";

            }

            return dna;

        }

        public int GetLength()
        {

            return LENGTH;

        }

    }

}