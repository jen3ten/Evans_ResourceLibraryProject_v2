using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week7_ProjectWeek_ResourcesProjectv2
{
    //Magazine is an inherited class of Resource
    class Magazine : Resource
    {
        //Magazine constructor
        public Magazine(string title, string isbn, int length)  //Parameters of title, isbn, and length set the Magazine properties
        {
            this.Title = title;
            this.ISBN = isbn;
            this.Length = length;
            this.CheckedOut = "";       //The CheckedOut property defaults to "not checked out"
            this.Type = "Magazine";     //The Type property is "Magazine"
        }

        //EditResourceProperties() allows the user to edit the properties of a Magazine resource
        //It has no parameters and no return value
        //This method overrides the Resource method of the same name
        public override void EditResourceProperties()
        {
            Console.Write("What is the name of this magazine? ");
            this.Title = Console.ReadLine();
            Console.Write("What is the ISBN of this magazine? ");
            this.ISBN = Console.ReadLine();
            Console.Write("How many pages does this magazine have? ");
            this.Length = int.Parse(Console.ReadLine());
        } //EditResourceProperties()

        //CheckOut() prints a message to the screen describing which student checked out the resource and when it is due for return
        //It has one parameter of type string called "studentName".  It has no return value.
        //This method overrides the Resource method of the same name
        public override void CheckOut(string studentName)
        {
            Console.WriteLine($"{studentName} has checked out \"{this.Title}\"");
            returnDate = DateTime.Now.AddDays(2).ToString("D");
            Console.WriteLine("\"{0}\" is due back on {1}.", this.Title, returnDate);
        } //CheckOut()
    }
}
