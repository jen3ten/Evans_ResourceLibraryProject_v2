using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week7_ProjectWeek_ResourcesProjectv2
{
    //DVD is an inherited class of Resource
    class DVD : Resource
    {
        //DVD constructor
        public DVD(string title, string isbn, int length)   //Parameters of title, isbn, and length set the DVD properties
        {
            this.Title = title;
            this.ISBN = isbn;
            this.Length = length;
            this.CheckedOut = "";       //The CheckedOut property defaults to "not checked out"
            this.Type = "DVD";          //The Type property is "DVD"
        }

        //ViewTitle() prints title, ISBN and length of DVD in minutes
        //It has no parameters and no return value
        //This method overrides the Resource method of the same name
        public override void ViewTitle()
        {
            Console.WriteLine("Title:\t\t{0}", this.Title);
            Console.WriteLine("ISBN:\t\t{0}", this.ISBN);
            Console.WriteLine("# Minutes:\t{0}", this.Length);
        } //ViewTitle()

        //EditResourceProperties() allows the user to edit the properties of a DVD resource
        //It has no parameters and no return value
        //This method overrides the Resource method of the same name
        public override void EditResourceProperties()
        {
            Console.Write("What is the name of this DVD? ");
            this.Title = Console.ReadLine();
            Console.Write("What is the ISBN of this DVD? ");
            this.ISBN = Console.ReadLine();
            Console.Write("What is the length of this DVD in minutes? ");
            this.Length = int.Parse(Console.ReadLine());
        } //EditResourceProperties()
    }
}
