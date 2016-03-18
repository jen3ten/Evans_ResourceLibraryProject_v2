using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Week7_ProjectWeek_ResourcesProjectv2
{
    abstract class Resource
    {
        //Fields
        private string title;
        private string isbn;
        private int length;
        private bool statusAvailable;         //Is this used?
        private string checkedOut;
        private string type;
        protected string returnDate;

        //Properties
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Length { get; set; }
        public bool StatusAvailable { get; set; }     //Is this used?
        public string CheckedOut { get; set; }
        public string Type { get; set; }

        //Constructors
        //Parameterless constructor
        public Resource()
        {
            this.Title = "No title listed";
            this.ISBN = "978-99-99999-99-9";
            this.Length = 0;
            this.StatusAvailable = true;
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
        }

        //EditResourceProperties() allows the user to edit the properties of a resource
        //It has no parameters and no return value
        //This method is virtual and can be overrideen by an inherited class
        public virtual void EditResourceProperties()
        {
            Console.Write("What is the name of this resource? ");
            this.Title = Console.ReadLine();
            Console.Write("What is the ISBN of this resource? ");
            this.ISBN = Console.ReadLine();
            Console.Write("How many pages does this resource have? ");
            this.Length = int.Parse(Console.ReadLine());
        }

        //CheckOut()
        public virtual void CheckOut(string studentName)
        {
            Console.WriteLine($"{studentName} has checked out \"{this.Title}\"");
            returnDate = DateTime.Now.AddDays(3).ToString("D");
            Console.WriteLine("{0} is due back on {1}.", this.Title, returnDate);
        }

        //UpdateResourceTextFile()
        public static void UpdateResourceListTextFile(List<Resource> resourceList)     //make virtual??
        {
            StreamWriter writeResourceList = new StreamWriter("BootcampResourceList.txt");
            StreamWriter writeDVDList = new StreamWriter("BootcampDVDResourceList.txt");
            StreamWriter writeBookList = new StreamWriter("BootcampBookResourceList.txt");
            StreamWriter writeMagazineList = new StreamWriter("BootcampMagazineResourceList.txt");
            foreach(Resource item in resourceList)
            {
                StringBuilder stringItem = new StringBuilder();
                stringItem.Append(item.Title);
                stringItem.Append(" (");
                stringItem.Append(item.Type);
                stringItem.Append(")");
                stringItem.ToString();
                writeResourceList.WriteLine(stringItem);
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
            writeResourceList.Close();
            writeDVDList.Close();
            writeBookList.Close();
            writeMagazineList.Close();
        }
    }
}
