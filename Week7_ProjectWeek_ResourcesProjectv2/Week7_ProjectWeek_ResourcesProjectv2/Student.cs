using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Week7_ProjectWeek_ResourcesProjectv2  
{
    //Student is a class of student objects
    class Student   
    {
        //Constructor
        public Student(string name, string id, string textFile)
        {
            this.Name = name;
            this.ID = id;
            this.TextFile = textFile;
        }

        //Fields
        private string name;
        private string id;
        private string textFile;

        //Properties
        public string Name { get; set; }
        public string ID { get; set; }
        public string TextFile { get; set; }

        //UpdateStudentAcctTextFile() updates the student account text file
        //UpdateStudentAcctTextFile() has a parameter with List of type Resource called "resourceList" that holds all Resource objects
        //UpdateStudentAcctTextFile() has no return value
        public void UpdateStudentAcctTextFile(List<Resource> resourceList)
        {
            StringBuilder studentFileName = new StringBuilder();    //StringBuilder builds the file name from the student's...
            studentFileName.Append(this.TextFile);                  //...text file name
            studentFileName.Append(".txt");                         //...and .txt extension

            StringBuilder nameLine = new StringBuilder();           //StringBuilder builds the student name line for the text file
            nameLine.Append("Student Name: ");
            nameLine.Append(this.Name);
            nameLine.ToString();

            StringBuilder idLine = new StringBuilder();             //StringBuilder builds the student ID line for the text file
            idLine.Append("Student ID: ");
            idLine.Append(this.ID);
            idLine.ToString();

            //StreamWriter is created to write to the student file
            StreamWriter writeStudentAcct = new StreamWriter(studentFileName.ToString());
            //Header information is added to the text file       
            writeStudentAcct.WriteLine("BOOTCAMP RESOURCE LIBRARY STUDENT ACCOUNT INFORMATION");
            writeStudentAcct.WriteLine();
            writeStudentAcct.WriteLine(nameLine);                   //...including the students name
            writeStudentAcct.WriteLine(idLine);                     //...and their student ID
            writeStudentAcct.WriteLine();
            writeStudentAcct.WriteLine("Resources Checked Out: ");
            bool noResources = true;
            foreach (Resource item in resourceList)
            {
                if (item.CheckedOut == this.Name)       //The CheckedOut property holds the name of the student who checked it out
                {
                    //All the student's resources are written to the file
                    writeStudentAcct.WriteLine("{0} ({1})", item.Title, item.Type);                
                    noResources = false;
                }
            }
            if (noResources)
            {
                writeStudentAcct.WriteLine("(No resources checked out)");
            }
            writeStudentAcct.Close();
        } //UpdateStudentAcctTextFile()

        //PrintStudentAcctTextFile() prints the student account text file to the screen
        //PrintStudentAcctTextFile() has no parameters
        //PrintStudentAcctTextFile() has no return values
        public void PrintStudentAcctTextFile()
        {
            //A StringBuilder builds the file name consisting of the student's...
            StringBuilder studentFileName = new StringBuilder();            
            studentFileName.Append(this.TextFile);                      //...text file name
            studentFileName.Append(".txt");                             //...and .txt extension

            //StreamReader is declared to read from the file
            StreamReader readStudentAccount = new StreamReader(studentFileName.ToString());     
            string line = "";
            do
            {
                line = readStudentAccount.ReadLine();
                Console.WriteLine(line);
            } while (line != null);

            //Close the StreamReader
            readStudentAccount.Close();
        } //PrintStudentAcctTextFile()
    }
}
