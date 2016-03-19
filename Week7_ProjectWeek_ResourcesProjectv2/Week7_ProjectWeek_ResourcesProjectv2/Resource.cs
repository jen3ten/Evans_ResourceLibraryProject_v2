using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Week7_ProjectWeek_ResourcesProjectv2
{
    //Resource is an abstract class with inherited classes of DVD, Book, and Magazine
    abstract class Resource
    {
        //Fields
        private string title;
        private string isbn;
        private int length;
        private string checkedOut;
        private string type;
        protected string returnDate;

        //Properties
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Length { get; set; }
        public string CheckedOut { get; set; }
        public string Type { get; set; }

        //Constructors
        //Parameterless constructor
        public Resource()
        {
            this.Title = "No title listed";
            this.ISBN = "978-99-99999-99-9";
            this.Length = 0;
            this.CheckedOut = "";
        }

        //Methods
        //ViewTitle() prints out title, ISBN, and length
        //It has no parameters and no return value
        //This method is virtual and can be overridden by inherited classes
        public virtual void ViewTitle()
        {
            Console.WriteLine("Title:\t\t{0}", this.Title);
            Console.WriteLine("ISBN:\t\t{0}", this.ISBN);
            Console.WriteLine("# Pages:\t{0}", this.Length);
        } //ViewTitle()

        //EditResourceProperties() allows the user to edit the properties of a resource
        //It has no parameters and no return value
        //This method is virtual and can be overridden by an inherited class
        public virtual void EditResourceProperties()
        {
            Console.Write("What is the name of this resource? ");
            this.Title = Console.ReadLine();
            Console.Write("What is the ISBN of this resource? ");
            this.ISBN = Console.ReadLine();
            Console.Write("How many pages does this resource have? ");
            this.Length = int.Parse(Console.ReadLine());
        } //EditResourceProperties()

        //CheckOut() prints a message to the screen describing which student checked out the resource and when it is due for return
        //It has one parameter of type string called "studentName".  It has no return value.
        //This method is virtual and can be overridden by an inherited class.
        public virtual void CheckOut(string studentName)
        {
            Console.WriteLine($"{studentName} has checked out \"{this.Title}\"");
            returnDate = DateTime.Now.AddDays(3).ToString("D");
            Console.WriteLine("\"{0}\" is due back on {1}.", this.Title, returnDate);
        } //CheckOut()

        //UpdateResourceTextFile() uses StreamWriters to write to the Resource, DVD, Book, and Magazine text files with the names of the resources and their type.  It is run at the start of the program and whenever resources are edited.
        //It has a parameter of List of type Resource named "resourceList".  It has no return values
        //This method is static (shared) with all objects of the class, and does not belong to a particular object
        public static void UpdateResourceListTextFile(List<Resource> resourceList)     
        {
            //StreamWriter is created for each text file
            StreamWriter writeResourceList = new StreamWriter("BootcampResourceList.txt");
            StreamWriter writeDVDList = new StreamWriter("BootcampDVDResourceList.txt");
            StreamWriter writeBookList = new StreamWriter("BootcampBookResourceList.txt");
            StreamWriter writeMagazineList = new StreamWriter("BootcampMagazineResourceList.txt");

            //For each Resource object create a StringBuilder of the resource title and type
            foreach (Resource item in resourceList)
            {
                StringBuilder stringItem = new StringBuilder();
                stringItem.Append(item.Title);
                stringItem.Append(" (");
                stringItem.Append(item.Type);
                stringItem.Append(")");
                stringItem.ToString();
                //Add all of the Resource names and type to the Resource text file
                writeResourceList.WriteLine(stringItem);
                //And add Resource names and type to the text file that matches the type of the Resource
                switch(item.Type)
                {
                    case "DVD":
                        writeDVDList.WriteLine(stringItem);
                        break;
                    case "Book":
                        writeBookList.WriteLine(stringItem);
                        break;
                    case "Magazine":
                        writeMagazineList.WriteLine(stringItem);
                        break;
                    default:
                        break;
                }
            }
            //Close all the StreamWriters
            writeResourceList.Close();
            writeDVDList.Close();
            writeBookList.Close();
            writeMagazineList.Close();
        } //UpdateResourceTextFile()
    }
}
