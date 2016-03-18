using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week7_ProjectWeek_ResourcesProjectv2
{
    class DVD : Resource
    {
        //DVD constructor
        public DVD(string title, string isbn, int length)
        {
            this.Title = title;
            this.ISBN = isbn;
            this.Length = length;
            this.CheckedOut = "";
            this.Type = "DVD";
        }

        //ViewTitle() prints title, ISBN and length of DVD in minutes
        public override void ViewTitle()
        {
            Console.WriteLine("Title:\t\t{0}", this.Title);
            Console.WriteLine("ISBN:\t\t{0}", this.ISBN);
            Console.WriteLine("# Minutes:\t{0}", this.Length);
        }

        //EditResourceProperties()
        public override void EditResourceProperties()
        {
            Console.Write("What is the name of this DVD? ");
            this.Title = Console.ReadLine();
            Console.Write("What is the ISBN of this DVD? ");
            this.ISBN = Console.ReadLine();
            Console.Write("What is the length of this DVD in minutes? ");
            this.Length = int.Parse(Console.ReadLine());
        }
    }
}
