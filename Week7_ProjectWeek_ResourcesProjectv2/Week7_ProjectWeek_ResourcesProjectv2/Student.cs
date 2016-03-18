using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Week7_ProjectWeek_ResourcesProjectv2  
{
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
        //UpdateStudentAcctTextFile() has parameters of type string called "studentName" that holds the student's name, and type Dictionary called "studentIDDictionary" that holds student names and IDs and "resourceDictionary" that holds resource names and who checked them out
        //UpdateStudentAcctTextFile() has no return value
        public void UpdateStudentAcctTextFile(List<Resource> resourceList)
        {
            StringBuilder studentFileName = new StringBuilder();    //The StringBuilder builds the file name consisting of the student's...
            studentFileName.Append(this.TextFile);                      //...text file name
            studentFileName.Append(".txt");                             //...and .txt extension

            StringBuilder nameLine = new StringBuilder();           //The StringBuilder builds the student name line for the text file
            nameLine.Append("Student Name: ");
            nameLine.Append(this.Name);
            nameLine.ToString();

            StringBuilder idLine = new StringBuilder();             //The StringBuilder builds the student ID line for the text file
            idLine.Append("Student ID: ");
            idLine.Append(this.ID);
            idLine.ToString();

            StreamWriter writeStudentAcct = new StreamWriter(studentFileName.ToString());       //StreamWriter is created to write to the student file
            writeStudentAcct.WriteLine("BOOTCAMP RESOURCE LIBRARY STUDENT ACCOUNT INFORMATION");//Header information is added to the text file
            writeStudentAcct.WriteLine();
            writeStudentAcct.WriteLine(nameLine);                                               //includeing the students name
            writeStudentAcct.WriteLine(idLine);                                                 //...and their student ID
            writeStudentAcct.WriteLine();
            writeStudentAcct.WriteLine("Resources Checked Out: ");
            bool noResources = true;
            foreach (Resource item in resourceList)
            {
                if (item.CheckedOut == this.Name)
                {
                    writeStudentAcct.WriteLine(item.Title);                //All the student's resources are written to the file
                    noResources = false;
                }
            }
            if (noResources)
            {
                writeStudentAcct.WriteLine("(No resources checked out)");
            }
            writeStudentAcct.Close();
        }

        //PrintStudentAcctTextFile() prints the student account text file to the screen
        //PrintStudentAcctTextFile() has parameters of type string called "studentName" that holds the student's name and type Dictionary called "studentIDDictionary" that holds all student names and IDs
        //PrintStudentAcctTextFile() has no return values
        public void PrintStudentAcctTextFile()
        {
            //string[] name = studentName.Split();                //The student's name is split into a string array of student's first and last name

            StringBuilder studentFileName = new StringBuilder();            //A StringBuilder builds the file name consisting of the student's...
            studentFileName.Append(this.TextFile);                      //...text file name
            studentFileName.Append(".txt");                             //...and .txt extension

            //studentFileName.Append(name[0].ToUpper());                          //...first name
            //studentFileName.Append(name[1].ToUpper());                          //...last name
            //studentFileName.Append(studentIDDictionary[studentName]);           //...and student ID
            //studentFileName.Append(".txt");

            StreamReader readStudentAccount = new StreamReader(studentFileName.ToString());     //StreamReader is declared to read from the file
            string line = "";
            do
            {
                line = readStudentAccount.ReadLine();
                Console.WriteLine(line);

            } while (line != null);

            readStudentAccount.Close();
        }

    }
}
