using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{
    public class FamilyTree
    {

        public const int MAXGENERATIONS = 5;

        public Character Dad { get; private set; }

        public Character Mom { get; private set; }

        public Character Me { get; private set; }

        public Character Spouse { get; private set; }

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
            this.Children = new List<Character>();

        }

        public void AddChild(Character child)
        {

            this.Children.Add(child);

        }

        public void SetDad(Character dad)
        {

            this.Dad = dad;

        }

        public void SetMom(Character mom)
        {

            this.Mom = mom;

        }

        public void Marriage(Character spouse)
        {

            if (this.Me.Gender == true)
            {

                spouse.LastName = this.Me.LastName;

            }

            else
            {

                this.Me.LastName = spouse.LastName;

            }

            this.Spouse = spouse;

        }

        public bool IsParent(Character child, Character parent)
        {

            if (child.Family.Dad == parent || child.Family.Mom == parent)
            {

                return true;

            }

            return false;

        }

        public bool IsSibling(Character a, Character b)
        {

            if (a.Family.Dad == b.Family.Dad)
            {

                return true;

            }

            return false;

        }

        /* public bool IsAncestor(Character descen, Character ancest, int generation)
        {

            if (IsParent(descen,ancest))
            {

                return true;

            }

            else
            {

                generation++;
                ancest = ancest.Family.

            }

        } */

    }
}
