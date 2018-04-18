using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{
    public class FamilyTree
    {

        public Character Dad { get; private set; }

        public Character Mom { get; private set; }

        public Character Me { get; private set; }

        public List<Character> Siblings { get; private set; }

        public List<Character> Children { get; private set; }

        public FamilyTree() : this(null,null,null)
        {



        }

        public FamilyTree(Character me) : this(null,null,me)
        {



        }

        public FamilyTree(Character dad, Character mom, Character Me)
        {

            this.Dad = dad;
            this.Mom = mom;
            this.Me = Me;
            this.Siblings = new List<Character>();
            this.Children = new List<Character>();

        }

        public void AddChildren(Character child)
        {

            this.Children.Add(child);

        }

        public void AddSiblings(Character sibling)
        {

            this.Siblings.Add(sibling);

        }

        public void SetDad(Character dad)
        {

            this.Dad = dad;

        }

        public void SetMom(Character mom)
        {

            this.Mom = mom;

        }

        public void FillSiblings()
        {

            List<Character> dadkids = this.Dad.Family.Children;
            List<Character> momkids = this.Mom.Family.Children;

            int dsize = dadkids.Count;
            int msize = momkids.Count;

            if (dsize == msize)
            {

                for ()

            }

        }

    }
}
