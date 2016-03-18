﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Week7_ProjectWeek_ResourcesProjectv2
{
    class Program
    {
        //ResetScreen() clears the screen and prints the title
        //ResetScreen() has no parameters and no returns
        static void ResetScreen()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("\tBOOTCAMP RESOURCE LIBRARY");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();
        }

        //PressKey() asks the user to press a key to return to the Main Menu
        //PressKey() has no parameters and no returns
        static void PressKey()
        {
            Console.Write("Press any key to return to the Main Menu...");
            Console.ReadKey();
        }

        //Menu() presents the main menu to the user
        //Menu() has no parameters
        //Menu() returns the user's menu selection as string data type
        static string Menu()
        {
            //Print a menu of user options, including letter codes used to access the options
            Console.WriteLine("*****************************************");
            Console.WriteLine("*\tMENU\t\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*\tS    View <S>tudent List\t*");            
            Console.WriteLine("*\tR    View All <R>esources\t*");             
            Console.WriteLine("*\tA    View <A>vailable Resources\t*");         
            Console.WriteLine("*\tE    <E>dit Resources\t\t*");               
            Console.WriteLine("*\tC    View Student A<c>counts\t*");        
            Console.WriteLine("*\tO    Check <O>ut Resource\t*");              
            Console.WriteLine("*\tI    Check <I>n Resource\t*");                
            Console.WriteLine("*\tX    E<x>it\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
            Console.Write("How may I help you? (Please enter letter code) ");
            string menuOption = Console.ReadLine();
            Console.WriteLine();
            return menuOption.ToUpper();       //User can enter a lower case or upper case menu option
        }

        //UpdateStudentListTextFile()
        static void UpdateStudentListTextFile(List<Student> studentList)
        {
            StreamWriter writeStudentList = new StreamWriter("BootcampStudentList.txt");
            foreach (Student student in studentList)
            {
                writeStudentList.WriteLine(student.Name);
            }
            writeStudentList.Close();
        }

        //PrintstudentList() presents the student list to the user
        //PrintStudentList() has a parameter of List of type Student called "studentList", which holds a list of Student objects
        //PrintStudentList() has no return value
        static void PrintStudentList(List<Student> studentList)
        {
            int num = 1;
            string line = "";
            StreamReader readStudentList = new StreamReader("BootcampStudentList.txt");

            Console.WriteLine("*****************************************");
            Console.WriteLine("*\tSTUDENT LIST\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            line = readStudentList.ReadLine();
            do
            {
                Console.WriteLine($"*{num,9}.  {line,-27}*");        //the line numbers are right justified; names are left justified
                line = readStudentList.ReadLine();
                num++;
            } while (line != null);
            readStudentList.Close();
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
        }

        //PrintResourceList() presents the resource list to the user
        //PrintResourceList() has a parameter of List of type Resource called "resourceList", which holds a list of resource ojbects
        //PrintResourceList() has no return value
        static void PrintResourceList(List<Resource> resourceList)
        {
            int num = 1;
            string line = "";
            StreamReader readResourceList = new StreamReader("BootcampResourceList.txt");

            Console.WriteLine("*****************************************");
            Console.WriteLine("*\tRESOURCES LIST\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            line = readResourceList.ReadLine();
            do
            {
                Console.WriteLine($"*{num,5}.  {line,-31}*");        //the line numbers are right justified; names are left justified
                line = readResourceList.ReadLine();
                num++;
            } while (line != null);
            readResourceList.Close();
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
        }

        //EditResource() allows the use to choose a resource and edit it
        //EditResource() has a parameter List of type Resource called "resourceList"
        //EditResource() has no return value
        static bool EditResource(List<Resource> resourceList)
        {
            bool validNum = false;
            int resourceNum = -1;
            bool edited = false;
            Console.WriteLine(edited);
            do
            {
                Console.Write("Please enter the number of the resource to edit (or 0 to exit without editing): ");
                validNum = int.TryParse(Console.ReadLine(), out resourceNum);       //The input is checked for validity
                if (validNum == false)
                    continue;
                if (resourceNum == 0)
                    break;
                else if (resourceNum < 0 || resourceNum > resourceList.Count())
                {
                    validNum = false;
                }
                else
                {
                    //The name of the resources is extracted from the list by index number
                    string resource = resourceList[resourceNum - 1].Title; 
                    foreach (Resource item in resourceList)
                    {
                        if (item.Title == resource)
                        {
                            //Call the ViewTitle() method from Resource class to print resource parameters to console
                            item.ViewTitle();
                            Console.WriteLine();
                            Console.Write("Would you like to edit this {0}? (Please enter y/n) ", item.Type);  
                            string response = Console.ReadLine().ToUpper();
                            if (response == "Y" || response == "YES")
                            {
                                //Call the EditResourceProperties() method from Resource class to all editing of object parameters
                                edited = true;
                                item.EditResourceProperties();
                                Console.WriteLine();
                                Console.WriteLine("This {0} has been edited.",item.Type);
                                item.ViewTitle();
                                Console.WriteLine();
                                PressKey();
                            }
                        }
                    }
                }
            } while (!validNum);
            Console.WriteLine(edited);
            return edited;
        }

        //PrintAvailableResources() presents the available items to the user
        //PrintAvailableResources() has a paramater of list of type Resource called "resourceList", which is a list of resource objects
        //PrintAvailableResources() returns List of type string called "availableResources", which is a list of only the available resources
        static List<string> PrintAvailableResources(List<Resource> resourceList)
        {
            List<string> availableResources = new List<string>();

            Console.WriteLine("*****************************************");
            Console.WriteLine("*\tAVAILABLE RESOURCES\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            int num = 0;
            bool allCheckedOut = true;
            foreach (Resource item in resourceList)  //go through all pairs and print only available resources
            {
                if (item.CheckedOut == "")   //if the value is blank, it is available
                {
                    num++;
                    Console.WriteLine($"*{num,5}.  {item.Title+ " (" + item.Type + ")",-31}*");   //the line numbers are right justified; resources are left justified
                    availableResources.Add(item.Title);
                    allCheckedOut = false;                              //If there is a blank, all resources are not checked out
                }
            }
            if (allCheckedOut)  //if no values are blank, all resources are checked out
            {
                Console.WriteLine("*\t(All resources checked out)\t*");
            }
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
            return availableResources;
        }

        //PrintStudentAccount() 
        //PrintStudentAccount()
        //PrintStudentAccount()
        static void PrintStudentAccount(List<Student> studentList, List<Resource> resourceList)
        {
            bool validNum = false;
            int studentNum = 0;
            int numStudents = studentList.Count();
            //First the PrintStudentList() method must be called to present a list of students to the user
            //Then PrintStudentAccount() allows the user to choose a student from the list by number
            do
            {
                Console.Write("Please enter the number of a student to view their account: ");
                validNum = int.TryParse(Console.ReadLine(), out studentNum);    //the input must be an integer...
                if (studentNum < 1 || studentNum > numStudents)                 //..that falls between 1 and the number of students
                    validNum = false;
            } while (!validNum);
            string studentName = studentList[studentNum - 1].Name;       //the student's name is extracted from the list by index number
            int num = 1;
            bool noResources = true;
            Console.WriteLine();
            Console.WriteLine("*****************************************");
            StringBuilder output = new StringBuilder();
            output.Append("*\t");
            output.Append(studentName.ToUpper());
            output.Append("'S ACCOUNT");
            output.ToString();
            Console.WriteLine("{0,-34}*", output);
            Console.WriteLine("*\t\t\t\t\t*");
            foreach (Resource item in resourceList)   //Loop through all key value pairs in resourceDictionary
            {
                if (item.CheckedOut == studentName)                             //When the value equals the student name...
                {
                    Console.WriteLine($"*{num, 5}.  {item.Title + " (" + item.Type + ")",-31}*");           //...print the name of the resource they checked out
                    num++;
                    noResources = false;
                }
            }
            if (noResources)                                           //...or let the user know that they didn't check out any resources
            {
                Console.WriteLine("*\t(No resources checked out)\t*");
            }
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
            Console.Write("Would you like to print the student account? (Please enter y/n) ");  //The user may print the student account from text file
            string response = Console.ReadLine().ToUpper();
            if (response == "Y" || response == "YES")
            {
                ResetScreen();
                //Fix these methods....
                //studentList[studentNum - 1].UpdateStudentAcctTextFile(resourceList);    //Do I need to update here??? Update the text file
                studentList[studentNum - 1].PrintStudentAcctTextFile();                         //Print the text file to the screen
            }
        }

        //CheckoutResource() allows user to checkout an item
        //CheckoutResource() has parameters of List of type Student called "studentList" which has a list of student objects, and List of type Resources called "resourceList" which holds a list of resources objects
        //CheckoutResource() does not return a value
        static void CheckoutResource(List<Student> studentList, List<Resource> resourceList)
        {
            bool validNum = false;
            int studentNum = 0;
            int numStudents = studentList.Count();
            //First the PrintStudentList() method must be called to present a list of students to the user
            //Then CheckoutResources() allows the user to choose a student from the list by number
            do
            {
                Console.Write("Please enter the number of the student who wishes to check out a resource: ");
                validNum = int.TryParse(Console.ReadLine(), out studentNum);            //The input must be an integer...
                if (studentNum < 1 || studentNum > numStudents)                         //..that falls between 1 and the number of students
                    validNum = false;
            } while (!validNum);
            Console.WriteLine();
            string studentName = studentList[studentNum - 1].Name;            //The students name is extracted from the list by index number
            bool maxResources = MaxResources(studentName, resourceList);      //Call maxResources() method to see if maximum resources checked out
            if (maxResources)
            {
                Console.WriteLine($"{studentName} has checked out a maximum number of 3 resources.");
                Console.WriteLine();
            }
            else
            {
                List<string> availableResources = PrintAvailableResources(resourceList);    //Print list of available resources
                if (availableResources.Count() > 0)
                {
                    int resourceNum = -1;
                    do
                    {
                        Console.Write("Please enter the number of the resource (or 0 to exit without checking out): ");
                        validNum = int.TryParse(Console.ReadLine(), out resourceNum);       //The input is checked for validity
                        if (validNum == false)
                            continue;
                        if (resourceNum == 0)
                            break;
                        else if (resourceNum < 0 || resourceNum > availableResources.Count())
                        {
                            validNum = false;
                        }
                        else
                        {
                            string resource = availableResources[resourceNum - 1]; //The name of the resources is extracted from the list by index number
                            foreach(Resource item in resourceList)
                            {
                                if (item.Title == resource)
                                {
                                    item.CheckedOut = studentName;          //The CheckedOut property is assigned to the student's name
                                    Console.WriteLine();
                                    item.CheckOut(studentName);
                                    studentList[studentNum - 1].UpdateStudentAcctTextFile(resourceList);    //Update the text file
                                    studentList[studentNum - 1].PrintStudentAcctTextFile();                //For testing....
                                    UpdateCheckedOutTextFile(resourceList);
                                }
                            }
                            Console.WriteLine();
                            //Console.WriteLine($"{studentName} has checked out \"{resource}\"");
                        }
                    } while (!validNum);
                }
            }
        }

        //MaxResources() checks to see if the student already checked out the maximum of 3 resources
        //MaxResources() has parameters of type string called "studentName" that holds the students name and type Dictionary called "resourceDictionary" that holds the names of the resources and who checked them out
        //MaxResources() has a return value of type bool, which is set to true if the maximum number of resources have been checked out
        static bool MaxResources(string studentName, List<Resource> resourceList)
        {
            bool maxResources = false;
            int count = 0;
            foreach (Resource item in resourceList)       //Loop through all key value pairs in resourceDictionary
            {
                if (item.CheckedOut == studentName)                                      //Count each time the student's name is found
                {
                    count++;
                }
            }
            if (count >= 3)                                                         //If the count is 3 or more, maximum resources have been checked out
                maxResources = true;
            return maxResources;                                                    //The method returns a true or false value
        }

        //ReturnResources() allows user to return a resource
        //ReturnResources() has parameters of type List called "studentList" that holds a list of student names and type Dictionary called "resourceDictionary" that holds a list of resources and who checked them out
        //ReturnResources() does not return a value
        static void ReturnResource(List<Student> studentList, List<Resource> resourceList)
        {
            bool validNum = false;
            int studentNum = 0;
            int numStudents = studentList.Count();
            //First the PrintStudentList() method must be called to present a list of students to the user
            //Then ReturnResources() allows the user to choose a student from the list by number
            do
            {
                Console.Write("Please enter the number of the student who wishes to return a resource: ");
                validNum = int.TryParse(Console.ReadLine(), out studentNum);            //The user input is checked for validity
                if (studentNum < 1 || studentNum > numStudents)
                    validNum = false;
            } while (!validNum);
            Console.WriteLine();
            string studentName = studentList[studentNum - 1].Name;       //The student's name is extracted from the list by index number

            int num = 0;
            bool noResources = true;
            int resourceNum;
            Console.WriteLine();
            Console.WriteLine("*****************************************");
            StringBuilder output = new StringBuilder();
            output.Append("*\t");
            output.Append(studentName.ToUpper());
            output.Append("'S ACCOUNT");
            output.ToString();
            Console.WriteLine("{0,-34}*", output);
            Console.WriteLine("*\t\t\t\t\t*");

            List<string> studentResourceList = new List<string>();                  //A new list is declared to temporarily hold the student's resources

            foreach (Resource item in resourceList)       //Check each key value pair in resourceDictionary
            {
                if (item.CheckedOut == studentName)                                      //If the student's name is found...
                {
                    num++;
                    Console.WriteLine($"*\t{num}.  {item.Title,-28}*");               //...the name of the resource is printed to the screen
                    studentResourceList.Add(item.Title);                              //and the name of the resource is added to the list
                    noResources = false;
                }
            }
            if (noResources)
            {
                Console.WriteLine("*\t(No resources checked out)\t*");
            }
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
            if (!noResources)
            {
                do
                {
                    Console.Write("Please enter the number of the resource (or 0 to exit without returning an item): ");
                    validNum = int.TryParse(Console.ReadLine(), out resourceNum);           //Check user input for validity
                    if (validNum == false)
                        continue;
                    if (resourceNum == 0)
                        break;
                    else if (resourceNum < 0 || resourceNum > num)
                    {
                        validNum = false;
                    }
                    else
                    {
                        string resource = studentResourceList[resourceNum - 1];         //Look up the name of the resource from the list by index
                        foreach (Resource item in resourceList)
                        {
                            if (item.Title == resource)
                            {
                                item.CheckedOut = "";
                                studentList[studentNum - 1].UpdateStudentAcctTextFile(resourceList);    //Update the text file
                                studentList[studentNum - 1].PrintStudentAcctTextFile();                //For testing....
                                UpdateCheckedOutTextFile(resourceList);
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine($"{studentName} has returned \"{resource}\"");
                        Console.WriteLine();
                    }
                } while (!validNum);
            }
        }

        ////UpdateStudentAcctTextFile() updates the student account text file
        ////UpdateStudentAcctTextFile() has parameters of type string called "studentName" that holds the student's name, and type Dictionary called "studentIDDictionary" that holds student names and IDs and "resourceDictionary" that holds resource names and who checked them out
        ////UpdateStudentAcctTextFile() has no return value
        //static void UpdateStudentAcctTextFile(string studentName, Dictionary<string, int> studentIDDictionary, Dictionary<string, string> resourceDictionary )
        //{
        //    string[] name = studentName.Split();                    //The students name is split into a string array of first name and last name

        //    StringBuilder studentFileName = new StringBuilder();    //The StringBuilder builds the file name consisting of the student's...
        //    studentFileName.Append(name[0].ToUpper());                  //...first name
        //    studentFileName.Append(name[1].ToUpper());                  //...last name
        //    studentFileName.Append(studentIDDictionary[studentName]);   //...and student ID
        //    studentFileName.Append(".txt");

        //    StringBuilder nameLine = new StringBuilder();           //The StringBuilder builds the student name line for the text file
        //    nameLine.Append("Student Name: ");
        //    nameLine.Append(studentName);
        //    nameLine.ToString();

        //    StringBuilder idLine = new StringBuilder();             //The StringBuilder builds the student ID line for the text file
        //    idLine.Append("Student ID: ");
        //    idLine.Append(studentIDDictionary[studentName]);
        //    idLine.ToString();

        //    StreamWriter writeStudentAcct = new StreamWriter(studentFileName.ToString());       //StreamWriter is created to write to the student file
        //    writeStudentAcct.WriteLine("BOOTCAMP RESOURCE LIBRARY STUDENT ACCOUNT INFORMATION");//Header information is added to the text file
        //    writeStudentAcct.WriteLine();
        //    writeStudentAcct.WriteLine(nameLine);                                               //includeing the students name
        //    writeStudentAcct.WriteLine(idLine);                                                 //...and their student ID
        //    writeStudentAcct.WriteLine();
        //    writeStudentAcct.WriteLine("Resources Checked Out: ");
        //    bool noResources = true;
        //    foreach (KeyValuePair<string, string> pair in resourceDictionary)
        //    {
        //        if (pair.Value == studentName)
        //        {
        //            writeStudentAcct.WriteLine(pair.Key);                                     //All the student's resources are written to the file
        //            noResources = false;
        //        }
        //    }
        //    if (noResources)
        //    {
        //        writeStudentAcct.WriteLine("(No resources checked out)");
        //    }
        //    writeStudentAcct.Close();
        //}

        ////UpdateStudentTextFile() updates the student account text file
        ////UpdateStudentTextFile() has parameters of type string called "studentName" that holds the student's name, and type Dictionary called "studentIDDictionary" that holds student names and IDs and "resourceDictionary" that holds resource names and who checked them out
        ////UpdateStudentTextFile() has no return value
        //static void UpdateStudentTextFile(string studentName, List<Student> studentList, List<Resource> resourceList)
        //{
        //    //string[] name = studentName.Split();                    //The students name is split into a string array of first name and last name
        //    string fileName = "";
        //    string studentID = "";

        //    foreach(Student student in studentList)
        //    {
        //        if (student.Name == studentName )
        //        {
        //            fileName = student.TextFile;
        //            studentID = student.ID;
        //        }
        //    }
        //    StringBuilder studentFileName = new StringBuilder();    //The StringBuilder builds the file name consisting of the student's...
        //    //studentFileName.Append(name[0].ToUpper());                  //...first name
        //    //studentFileName.Append(name[1].ToUpper());                  //...last name
        //    //studentFileName.Append(studentIDDictionary[studentName]);   //...and student ID
        //    studentFileName.Append(fileName);
        //    studentFileName.Append(".txt");


        //    StringBuilder nameLine = new StringBuilder();           //The StringBuilder builds the student name line for the text file
        //    nameLine.Append("Student Name: ");
        //    nameLine.Append(studentName);
        //    nameLine.ToString();

        //    StringBuilder idLine = new StringBuilder();             //The StringBuilder builds the student ID line for the text file
        //    idLine.Append("Student ID: ");
        //    idLine.Append(studentID);
        //    idLine.ToString();

        //    StreamWriter writeStudentAcct = new StreamWriter(studentFileName.ToString());       //StreamWriter is created to write to the student file
        //    writeStudentAcct.WriteLine("BOOTCAMP RESOURCE LIBRARY STUDENT ACCOUNT INFORMATION");//Header information is added to the text file
        //    writeStudentAcct.WriteLine();
        //    writeStudentAcct.WriteLine(nameLine);                                               //includeing the students name
        //    writeStudentAcct.WriteLine(idLine);                                                 //...and their student ID
        //    writeStudentAcct.WriteLine();
        //    writeStudentAcct.WriteLine("Resources Checked Out: ");
        //    bool noResources = true;
        //    foreach (Resource item in resourceList)
        //    {
        //        if (item.CheckedOut == studentName)
        //        {
        //            writeStudentAcct.WriteLine(item.Title);                //All the student's resources are written to the file
        //            noResources = false;
        //        }
        //    }
        //    if (noResources)
        //    {
        //        writeStudentAcct.WriteLine("(No resources checked out)");
        //    }
        //    writeStudentAcct.Close();
        //}


        //UpdateCheckedOutTextFile() updates the text file of checked out resources
        //UpdateCheckedOutTextFile() has a parameter of type Dictionary called "resourceDictionary" that holds resource names and the student who has checked them out
        //UpdateCheckedOutTextFile() has no return values
        static void UpdateCheckedOutTextFile(List<Resource> resourceList)
        {
            StreamWriter writeCheckedOut = new StreamWriter("BootcampResourcesCheckedOut.txt");     //StreamWriter is created to write to the text file
            writeCheckedOut.WriteLine("BOOTCAMP RESOURCE LIBRARY CHECKED OUT RESOURCES");           //Header information is written to the text file
            writeCheckedOut.WriteLine();
            writeCheckedOut.WriteLine("Resources Checked Out: ");
            bool noResources = true;
            foreach (Resource item in resourceList)
            {
                if (item.CheckedOut != "")
                {
                    StringBuilder checkedOutLine = new StringBuilder();
                    checkedOutLine.Append("\"");
                    checkedOutLine.Append(item.Title);
                    checkedOutLine.Append("\"");
                    checkedOutLine.Append(" has been checked out by ");
                    checkedOutLine.Append(item.CheckedOut);
                    checkedOutLine.ToString();
                    writeCheckedOut.WriteLine(checkedOutLine);                    //The resource name and who checked it out is written to the text file
                    noResources = false;
                }
            }
            if (noResources)
            {
                writeCheckedOut.WriteLine("(No resources checked out)");
            }
            writeCheckedOut.Close();
        }


        ////PrintStudentTextFile() prints the student account text file to the screen
        ////PrintStudentTextFile() has parameters of type string called "studentName" that holds the student's name and type Dictionary called "studentIDDictionary" that holds all student names and IDs
        ////PrintStudentTextFile() has no return values
        //static void PrintStudentTextFile(string studentName, Dictionary<string, int> studentIDDictionary)
        //{
        //    string[] name = studentName.Split();                //The student's name is split into a string array of student's first and last name

        //    StringBuilder studentFileName = new StringBuilder();            //A StringBuilder builds the file name consisting of the student's...
        //    studentFileName.Append(name[0].ToUpper());                          //...first name
        //    studentFileName.Append(name[1].ToUpper());                          //...last name
        //    studentFileName.Append(studentIDDictionary[studentName]);           //...and student ID
        //    studentFileName.Append(".txt");

        //    StreamReader readStudentAccount = new StreamReader(studentFileName.ToString());     //StreamReader is declared to read from the file
        //    string line = "";
        //    do
        //    {
        //        line = readStudentAccount.ReadLine();
        //        Console.WriteLine(line);

        //    } while (line != null);

        //    readStudentAccount.Close();
        //}

        //PrintCheckedOutTextFile() prints the text file containing all checked out resources and who checked them out
        //PrintCheckedOutTextFile() has a parameter of type Dictionary called "resourceDictionary" that holds a list of resources and which student checked them out
        //PrintCheckedOutTextFile() has no return values
        static void PrintCheckedOutTextFile (List<Resource> resourceList)
        {
            Console.Write("Would you like to print a list of checked out resources? (Please enter y/n) ");
            string response = Console.ReadLine().ToUpper();
            if (response == "Y" || response == "YES" )
            {
                ResetScreen();
                //UpdateCheckedOutTextFile(resourceList);                   //Do I need this?? First the text file is updated
                StreamReader readCheckedOut = new StreamReader("BootcampResourcesCheckedOut.txt");  //Then the file is read
                string line = "";
                do
                {
                    line = readCheckedOut.ReadLine();
                    Console.WriteLine(line);
                } while (line != null);
                readCheckedOut.Close();
            }
        }
        
        //Main() is the main method of this program
        //Main() declares all lists and dictionaries and calls all methods based on the user's main menu selection
        static void Main(string[] args)
        {
            ResetScreen();

            //boolean variable that keeps program running until exit key is pressed
            bool runProgram = true;

            //Instantiate the student objects
            Student student1 = new Student("Jennifer Evans", "0312", "Evans0312");
            Student student2 = new Student("Mary Winkleman", "1256", "Winkleman1256");
            Student student3 = new Student("Kim Vargas", "5401", "Vargas5401");
            Student student4 = new Student("Imari Childress", "9981", "Childress9881");
            Student student5 = new Student("Quinn Bennett", "5102", "Bennett5102");
            Student student6 = new Student("Richard Raponi", "2288", "Raponi2288");
            Student student7 = new Student("Cameron Robinson", "8192", "Robinson8192");
            Student student8 = new Student("Krista Scholdberg", "1171", "Scholdberg1171");
            Student student9 = new Student("Ashley Stewart", "3456", "Stewart3456");
            Student student10 = new Student("Cadale Thomas", "4094", "Thomas4094");
            Student student11 = new Student("Margaret Landefeld", "6363", "Landefeld6363");
            Student student12 = new Student("Lawrence Hudson", "7343", "Hudson7343");
            Student student13 = new Student("Jacob Lockyer", "1961", "Lockyer1961");


            //Add student objects to a list, called "studentList"
            List<Student> studentList = new List<Student>();
            studentList.Add(student1);
            studentList.Add(student2);
            studentList.Add(student3);
            studentList.Add(student4);
            studentList.Add(student5);
            studentList.Add(student6);
            studentList.Add(student7);
            studentList.Add(student8);
            studentList.Add(student9);
            studentList.Add(student10);
            studentList.Add(student11);
            studentList.Add(student12);
            studentList.Add(student13);
            studentList = studentList.OrderBy(o => o.TextFile).ToList();

            //Instantiate the DVD objects
            DVD dvd1 = new DVD("C# for Dummies", "09-23423-5434", 67);
            DVD dvd2 = new DVD("Getting Along with Git", "09-34322-123", 124);
            DVD dvd3 = new DVD("JavaScript", "09-6335-123", 81);
            
            ////Add DVD objects to a list
            //List<DVD> DVDList = new List<DVD>();
            //DVDList.Add(dvd1);
            //DVDList.Add(dvd2);
            //DVDList.Add(dvd3);

            //For testing 'All checked out'
            dvd1.CheckedOut = "Jennifer Evans";
            dvd2.CheckedOut = "Mary Winkleman";
            //dvd3.CheckedOut = "Kim Vargas";

            //Instantiate the Book objects
            Book book1 = new Book("Database Design", "08-541324-1234", 320);
            Book book2 = new Book("SQL Queries", "08-1243-212", 197);
            Book book3 = new Book("C# Player's Guide", "08-52342-23", 94);

            //Instantiate the Magazine object
            Magazine magazine1 = new Magazine("HTML & CSS", "07-4322-232", 38);
            Magazine magazine2 = new Magazine("Agile Methodology", "07-34122-6324", 52);
            Magazine magazine3 = new Magazine("Scrum 101", "07-1234-5423", 40);
            Magazine magazine4 = new Magazine("Hooray for Arrays!", "07-63208-45", 23);

            //A List of type Resource holds resource objects, which can be from the DVD, Book, or Magazine inherited class
            List<Resource> resourceList = new List<Resource>();
            resourceList.Add(dvd1);
            resourceList.Add(dvd2);
            resourceList.Add(dvd3);
            resourceList.Add(book1);
            resourceList.Add(book2);
            resourceList.Add(book3);
            resourceList.Add(magazine1);
            resourceList.Add(magazine2);
            resourceList.Add(magazine3);
            resourceList.Add(magazine4);
            resourceList = resourceList.OrderBy(o=>o.Title).ToList();

            Resource.UpdateResourceListTextFile(resourceList);

            for (int i = 0; i < studentList.Count(); i++)
            {
                studentList[i].UpdateStudentAcctTextFile(resourceList);    //Update the text file
                studentList[i].PrintStudentAcctTextFile();                //For testing....

            }

            UpdateStudentListTextFile(studentList);

            do
            {
                string menuOption = Menu();                                                 //Present the main menu to the user
                switch (menuOption)                                                         //Run code based on the menu option chosen by the user
                {
                    case "S":               //"View Student List" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        PrintStudentList(studentList);                                      //Print the student list
                        PressKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "R":               //"View All Resources" menu option
                        ResetScreen();
                        PrintResourceList(resourceList);
                        PressKey();
                        ResetScreen();
                        break;
                    case "A":               //"View Available Resources" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        List<string> unused = PrintAvailableResources(resourceList);        //Print the available resources
                        PrintCheckedOutTextFile(resourceList);                        //Allow user to print checked out resources text file
                        PressKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "E":               //"Edit Resources" menu option
                        ResetScreen();
                        PrintResourceList(resourceList);
                        bool edited = EditResource(resourceList);
                        if (edited)
                        {
                            //Console.WriteLine("update the student text files and all Resource files");
                            Resource.UpdateResourceListTextFile(resourceList);
                            UpdateCheckedOutTextFile(resourceList);
                            for (int i = 0; i < studentList.Count(); i++)
                            {
                                studentList[i].UpdateStudentAcctTextFile(resourceList);    //Update the text file
                                //studentList[i].PrintStudentAcctTextFile();                //For testing....
                            }
                        }
                        Console.ReadLine();
                        ResetScreen();
                        break;
                    case "C":               //"View Student Account" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        PrintStudentList(studentList);                                      //Print the student list
                        PrintStudentAccount(studentList, resourceList);  //Print the student account
                        PressKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "O":               //"Checkout Resource" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        PrintStudentList(studentList);                                      //Print the student list
                        CheckoutResource(studentList, resourceList);                        //Allow the user to checkout a resource
                        PressKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "I":               //"CheckIn Resource" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        PrintStudentList(studentList);                                      //Print the student list
                        ReturnResource(studentList, resourceList);                          //Allow the user to return a resource
                        PressKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "X":               //"Exit" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        Console.WriteLine("Goodbye!");                                      //Gives the user a "Goodbye" message
                        Console.WriteLine();
                        runProgram = false;                                                 //Breaks out of do-while loop
                        break;
                    default:
                        ResetScreen();                                                      //Clear screen; print title
                        Console.WriteLine("The code you entered was not found.  Please enter a valid code."); //User is warned that invalid entry was made
                        Console.WriteLine();
                        break;                                                              //The do-while loop continues; Menu() is called
                }
            } while (runProgram);                                                           //The loop continues while runProgram is true
        }
    }
}
