using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week7_ProjectWeek_ResourcesProjectv2
{
    class Magazine : Resource
    {
        //Magazine constructor
        public Magazine(string title, string isbn, int length)
        {
            this.Title = title;
            this.ISBN = isbn;
            this.Length = length;
            this.CheckedOut = "";
            this.Type = "Magazine";
        }

        //EditResourceProperties()
        public override void EditResourceProperties()
        {
            Console.Write("What is the name of this magazine? ");
            this.Title = Console.ReadLine();
            Console.Write("What is the ISBN of this magazine? ");
            this.ISBN = Console.ReadLine();
            Console.Write("How many pages does this magazine have? ");
            this.Length = int.Parse(Console.ReadLine());
        }

        //CheckOut()
        public override void CheckOut(string studentName)
        {
            Console.WriteLine($"{studentName} has checked out \"{this.Title}\"");
            returnDate = DateTime.Now.AddDays(2).ToString("D");
            Console.WriteLine("{0} is due back on {1}.", this.Title, returnDate);
        }
    }
}
