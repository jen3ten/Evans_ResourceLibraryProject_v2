using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Week7_ProjectWeek_ResourcesProjectv2
{
    class Program
    {
        //Clears the screen and prints the title
        static void ResetScreen()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("\tBOOTCAMP RESOURCE LIBRARY");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();
        }

        //Presents the main menu to the user
        static string Menu()
        {
            //Print a menu of user options, including letter codes used to access the options
            Console.WriteLine("*****************************************");
            Console.WriteLine("*\tMENU\t\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*\tS    View <S>tudent List\t*");
            Console.WriteLine("*\tI    View Available <I>tems\t*");
            Console.WriteLine("*\tA    View Student <A>ccounts\t*");
            Console.WriteLine("*\tC    <C>heckout Item\t\t*");
            Console.WriteLine("*\tR    <R>eturn Item\t\t*");
            Console.WriteLine("*\tX    E<x>it\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
            Console.Write("How may I help you? (Please enter letter code) ");
            string menuOption = Console.ReadLine();
            Console.WriteLine();
            return menuOption.ToUpper();       //User can enter a lower case or upper case menu option
        }

        //Presents the student list to the user
        //uses the List "studentList" which is sent as a parameter
        static void PrintStudentList(List<string> studentList)
        {
            int num = 1;
            Console.WriteLine("*****************************************");
            Console.WriteLine("*\tSTUDENT LIST\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            foreach (string student in studentList)
            {
                Console.WriteLine($"*\t{num}.  {student,-28}*");
                num++;
            }
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
        }

        //Presents the available items to the user
        //uses the Dictionary "resourceDictionary", sent as a parameter, which is a dictionary of all resources
        //returns a List "availableResources", which is a list of only the available resources, not all
        static List<string> PrintAvailableResources(Dictionary<string, string> resourceDictionary) 
        {
            List<string> availableResources = new List<string>();

            Console.WriteLine("*****************************************");
            Console.WriteLine("*\tRESOURCES LIST\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            int num = 0;
            bool allCheckedOut = true;
            foreach(KeyValuePair<string, string> pair in resourceDictionary)  //go through all pairs and print only available resources
            {
                if (pair.Value == "")   //if the value is blank, it is available
                {
                    num++;
                    StringBuilder output = new StringBuilder();
                    output.Append("*\t");
                    output.Append(num);
                    output.Append(".  ");
                    output.Append(pair.Key);
                    output.ToString();
                    Console.WriteLine("{0,-34}*", output);
                    availableResources.Add(pair.Key);
                    allCheckedOut = false;
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

        //Presents the student account to the user
        //uses List "studentList", Dictionary "studentIDDictionary", Dictionary "resourceDictionary", sent as parameters
        static void PrintStudentAccount(List<string> studentList, Dictionary<string, int> studentIDDictionary, Dictionary<string, string> resourceDictionary)
        {
            bool validNum = false;
            int studentNum = 0;
            int numStudents = studentList.Count();
            do
            {
                Console.Write("Please enter the number of a student to view their account: ");
                validNum = int.TryParse(Console.ReadLine(), out studentNum);
                if (studentNum < 1 || studentNum > numStudents)
                    validNum = false;
            } while (!validNum);
            string studentName = studentList[studentNum - 1];
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
            foreach (KeyValuePair<string, string> pair in resourceDictionary)
            {
                if (pair.Value == studentName)
                {
                    Console.WriteLine($"*\t{num}.  {pair.Key,-28}*");
                    num++;
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
            Console.Write("Would you like to print the student account? (Please enter y/n) ");
            string response = Console.ReadLine().ToUpper();
            if (response == "Y" || response == "YES")
            {
                ResetScreen();
                UpdateStudentTextFile(studentName, studentIDDictionary, resourceDictionary);
                PrintStudentTextFile(studentName, studentIDDictionary);
            }
        }

        //Allows user to checkout an item
        static void CheckoutResource(List<string> studentList, Dictionary<string, string> resourceDictionary)
        {
            bool validNum = false;
            int studentNum = 0;
            int numStudents = studentList.Count();
            do
            {
                Console.Write("Please enter the number of the student who wishes to check out a resource: ");
                validNum = int.TryParse(Console.ReadLine(), out studentNum);
                if (studentNum < 1 || studentNum > numStudents)
                    validNum = false;
            } while (!validNum);
            Console.WriteLine();
            string studentName = studentList[studentNum - 1];
            bool maxResources = MaxResources(studentName, resourceDictionary);
            if (maxResources)
            {
                Console.WriteLine($"{studentName} has checked out a maximum number of 3 resources.");
                Console.WriteLine();
            }
            else
            { 
                List<string> numResources = PrintAvailableResources(resourceDictionary);
                if (numResources.Count() > 0)
                {
                    int resourceNum = -1;
                    do
                    {
                        Console.Write("Please enter the number of the resource (or 0 to exit without checking out): ");
                        validNum = int.TryParse(Console.ReadLine(), out resourceNum);
                        if (validNum == false)
                            continue;
                        if (resourceNum == 0)
                            break;
                        else if (resourceNum < 0 || resourceNum > numResources.Count())
                        {
                            validNum = false;
                        }
                        else
                        {
                            string resource = numResources[resourceNum - 1];
                            resourceDictionary[resource] = studentName;
                            Console.WriteLine();
                            Console.WriteLine($"{studentName} has checked out \"{resource}\"");
                        }
                    } while (!validNum);
                }
            }
        }
        
        //Checks to see if user already checked out the maximum of 3 resources
        static bool MaxResources(string studentName, Dictionary<string, string> resourceDictionary)
        {
            bool maxResources = false;
            int count = 0;
            foreach (KeyValuePair<string, string> pair in resourceDictionary)
            {
                if (pair.Value == studentName)
                {
                    count++;
                }
            }
            if (count >= 3)
                maxResources = true;
            return maxResources;
        }

        //Allows user to return a resource
        static void ReturnResource(List<string> studentList, Dictionary<string, string> resourceDictionary)
        {
            bool validNum = false;
            int studentNum = 0;
            int numStudents = studentList.Count();
            do
            {
                Console.Write("Please enter the number of the student who wishes to return a resource: ");
                validNum = int.TryParse(Console.ReadLine(), out studentNum);
                if (studentNum < 1 || studentNum > numStudents)
                    validNum = false;
            } while (!validNum);
            Console.WriteLine();
            string studentName = studentList[studentNum - 1];

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

            List<string> studentResourceList = new List<string>();

            foreach (KeyValuePair<string, string> pair in resourceDictionary)
            {
                if (pair.Value == studentName)
                {
                    num++;
                    Console.WriteLine($"*\t{num}.  {pair.Key,-28}*");
                    studentResourceList.Add(pair.Key);
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
                    validNum = int.TryParse(Console.ReadLine(), out resourceNum);
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
                        string resource = studentResourceList[resourceNum - 1];
                        resourceDictionary[resource] = "";
                        Console.WriteLine();
                        Console.WriteLine($"{studentName} has returned \"{resource}\"");
                    }
                } while (!validNum);
            }
        }

        //Update student account text file
        static void UpdateStudentTextFile(string studentName, Dictionary<string, int> studentIDDictionary, Dictionary<string, string> resourceDictionary )
        {
            string[] name = studentName.Split();

            StringBuilder studentFileName = new StringBuilder();
            studentFileName.Append(name[0].ToUpper());
            studentFileName.Append(name[1].ToUpper());
            studentFileName.Append(studentIDDictionary[studentName]);
            studentFileName.Append(".txt");

            StringBuilder nameLine = new StringBuilder();
            nameLine.Append("Student Name: ");
            nameLine.Append(studentName);
            nameLine.ToString();

            StringBuilder idLine = new StringBuilder();
            idLine.Append("Student ID: ");
            idLine.Append(studentIDDictionary[studentName]);
            idLine.ToString();

            StreamWriter writeStudentAcct = new StreamWriter(studentFileName.ToString());
            writeStudentAcct.WriteLine("BOOTCAMP RESOURCE LIBRARY STUDENT ACCOUNT INFORMATION");
            writeStudentAcct.WriteLine();
            writeStudentAcct.WriteLine(nameLine);
            writeStudentAcct.WriteLine(idLine);
            writeStudentAcct.WriteLine();
            writeStudentAcct.WriteLine("Resources Checked Out: ");
            bool noResources = true;
            foreach (KeyValuePair<string, string> pair in resourceDictionary)
            {
                if (pair.Value == studentName)
                {
                    writeStudentAcct.WriteLine(pair.Key);
                    noResources = false;
                }
            }
            if (noResources)
            {
                writeStudentAcct.WriteLine("(No resources checked out)");
            }
            writeStudentAcct.Close();
        }
        
        static void UpdateCheckedOutTextFile (Dictionary<string, string> resourceDictionary)
        {
            StreamWriter writeCheckedOut = new StreamWriter("BootcampResourcesCheckedOut.txt");
            writeCheckedOut.WriteLine("BOOTCAMP RESOURCE LIBRARY CHECKED OUT RESOURCES");
            writeCheckedOut.WriteLine();
            writeCheckedOut.WriteLine("Resources Checked Out: ");
            bool noResources = true;
            foreach (KeyValuePair<string, string> pair in resourceDictionary)
            {
                if (pair.Value != "")
                {
                    StringBuilder checkedOutLine = new StringBuilder();
                    checkedOutLine.Append("\"");
                    checkedOutLine.Append(pair.Key);
                    checkedOutLine.Append("\"");
                    checkedOutLine.Append(" has been checked out by ");
                    checkedOutLine.Append(pair.Value);
                    checkedOutLine.ToString();

                    writeCheckedOut.WriteLine(checkedOutLine);
                    noResources = false;
                }
            }
            if (noResources)
            {
                writeCheckedOut.WriteLine("(No resources checked out)");
            }
            writeCheckedOut.Close();
        }

        static void PrintStudentTextFile(string studentName, Dictionary<string, int> studentIDDictionary)
        {
            string[] name = studentName.Split();

            StringBuilder studentFileName = new StringBuilder();
            studentFileName.Append(name[0].ToUpper());
            studentFileName.Append(name[1].ToUpper());
            studentFileName.Append(studentIDDictionary[studentName]);
            studentFileName.Append(".txt");

            StreamReader readStudentAccount = new StreamReader(studentFileName.ToString());
            string line = "";
            do
            {
                line = readStudentAccount.ReadLine();
                Console.WriteLine(line);

            } while (line != null);

            readStudentAccount.Close();
        }

        static void PrintCheckedOutTextFile (Dictionary<string, string> resourceDictionary)
        {
            Console.Write("Would you like to print a list of checked out resources? (Please enter y/n) ");
            string response = Console.ReadLine().ToUpper();
            if (response == "Y" || response == "YES" )
            {
                ResetScreen();
                UpdateCheckedOutTextFile(resourceDictionary);
                StreamReader readCheckedOut = new StreamReader("BootcampResourcesCheckedOut.txt");
                string line = "";
                do
                {
                    line = readCheckedOut.ReadLine();
                    Console.WriteLine(line);
                } while (line != null);
                readCheckedOut.Close();
            }
        }

        static void Main(string[] args)
        {
            ResetScreen();

            //boolean variable that keeps program running until exit key is pressed
            bool runProgram = true;
            
            //studentList list holds the name of all students
            List<string> studentList = new List<string>();
            studentList.Add("Jen Evans");
            studentList.Add("Mary Winkleman");
            studentList.Add("Kim Vargas");
            studentList.Add("Imari Childress");
            studentList.Sort();

            //Resources list holds the name of all resources
            List<string> resourceList = new List<string>();
            resourceList.Add("C# for Dummies");
            resourceList.Add("Getting Along with Git");
            resourceList.Add("JavaScript");
            resourceList.Add("Database Design");
            resourceList.Add("SQL Queries");
            resourceList.Add("C# Player's Guide");
            resourceList.Add("HTML & CSS");
            resourceList.Add("Agile Methodology");
            resourceList.Add("Scrum 101");
            resourceList.Add("Hooray for Arrays!");
            resourceList.Sort();

            //Resource dictionary holds keys with the resource names and values equal to name of student who checked out item, or blank
            //if not checked out
            //The dictionary is initialized with resource names from the resourceList
            Dictionary<string, string> resourceDictionary = new Dictionary<string, string>();
            foreach (string resource in resourceList)
            {
                resourceDictionary.Add(resource, "");
            }

            //The following is for testing purposes
            //resourceDictionary["C# for Dummies"] = "Jen";
            //resourceDictionary["JavaScript"] = "Jen";
            //resourceDictionary["Getting Along with Git"] = "Jen";

            //StudentID dictionary holds keys with the student names and values equal to the student ID
            //The dictionary is initialized with student names from the studentList and a 3 digit ID number
            Dictionary<string, int> studentIDDictionary = new Dictionary<string, int>();
            int studentID = 310;
            foreach (string student in studentList)
            {
                studentIDDictionary.Add(student, studentID);
                UpdateStudentTextFile(student, studentIDDictionary, resourceDictionary);
                studentID = studentID / 2 + 50;
            }

            do
            {
                string menuOption = Menu();
                switch (menuOption)
                {
                    case "S":
                        ResetScreen();
                        PrintStudentList(studentList);
                        Console.Write("Press any key to return to the Menu...");
                        Console.ReadKey();
                        ResetScreen();
                        break;
                    case "I":
                        ResetScreen();
                        List<string> unused = PrintAvailableResources(resourceDictionary);
                        PrintCheckedOutTextFile(resourceDictionary);
                        Console.Write("Press any key to return to the Menu...");
                        Console.ReadKey();
                        ResetScreen();
                        break;
                    case "A":
                        ResetScreen();
                        PrintStudentList(studentList);
                        PrintStudentAccount(studentList, studentIDDictionary, resourceDictionary);
                        Console.Write("Press any key to return to the Menu...");
                        Console.ReadKey();
                        ResetScreen();
                        break;
                    case "C":
                        ResetScreen();
                        PrintStudentList(studentList);
                        CheckoutResource(studentList, resourceDictionary);
                        Console.Write("Press any key to return to the Menu...");
                        Console.ReadKey();
                        ResetScreen();
                        break;
                    case "R":
                        ResetScreen();
                        PrintStudentList(studentList);
                        ReturnResource(studentList, resourceDictionary);
                        Console.Write("Press any key to return to the Menu...");
                        Console.ReadKey();
                        ResetScreen();
                        break;
                    case "X":
                        ResetScreen();
                        Console.WriteLine("Goodbye!");
                        Console.WriteLine();
                        runProgram = false;
                        break;
                    default:
                        ResetScreen();
                        Console.WriteLine("The code you entered was not found.  Please enter a valid code.");
                        Console.WriteLine();
                        break;
                }
            } while (runProgram);
        }
    }
}
