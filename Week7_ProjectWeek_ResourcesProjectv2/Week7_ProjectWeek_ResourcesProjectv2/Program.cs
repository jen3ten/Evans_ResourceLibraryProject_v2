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

        //Menu() presents the main menu to the user
        //Menu() has no parameters
        //Menu() returns the user's menu selection as string data type
        static string Menu()
        {
            //Print a menu of user options, including letter codes used to access the options
            Console.WriteLine("*****************************************");
            Console.WriteLine("*\tMENU\t\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*\tS    View <S>tudent List\t*");            //S for Students
            Console.WriteLine("*\tR    View All <R>esources\t*");             //Change to R for Resources
            Console.WriteLine("*\tA    View <A>vailable Resources\t*");         //Change to A for Available
            Console.WriteLine("*\tE    <E>dit Resources\t\t*");               //E for Edit
            Console.WriteLine("*\tC    View Student A<c>counts\t*");        //Change to C for Accounts
            Console.WriteLine("*\tO    Check <O>ut Resource\t*");              //Change to O for Check out
            Console.WriteLine("*\tI    Check <I>n Resource\t*");                //Change to I for Check In
            Console.WriteLine("*\tX    E<x>it\t\t\t*");
            Console.WriteLine("*\tT    TESTING");
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
            Console.Write("How may I help you? (Please enter letter code) ");
            string menuOption = Console.ReadLine();
            Console.WriteLine();
            return menuOption.ToUpper();       //User can enter a lower case or upper case menu option
        }

        //PrintStudentList() presents the student list to the user
        //PrintStudentList() has a parameter of type List called "studentList", which holds a list of student names
        //PrintStudentList() has no return value
        static void PrintStudentList(List<string> studentList)
        {
            int num = 1;
            Console.WriteLine("*****************************************");
            Console.WriteLine("*\tSTUDENT LIST\t\t\t*");
            Console.WriteLine("*\t\t\t\t\t*");
            foreach (string student in studentList)
            {
                Console.WriteLine($"*{num,9}.  {student,-27}*");        //the line numbers are right justified; names are left justified
                num++;
            }
            Console.WriteLine("*\t\t\t\t\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine();
        }

        //PrintAvailableResources() presents the available items to the user
        //PrintAvailableResources() has a paramater of type Dictionary called "resourceDictionary", which is a dictionary of all resources and who checked them out
        //PrintAvailableResources() returns type List called "availableResources", which is a list of only the available resources
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
                    Console.WriteLine($"*{num,9}.  {pair.Key,-27}*");   //the line numbers are right justified; resources are left justified
                    availableResources.Add(pair.Key);
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

        //PrintStudentAccout() presents the student account to the user
        //PrintStudentAccout() has paramenters of type List called "studentList", and type Dictionary called "studentIDDictionary" and "resourceDictionary"
            //"studentList" is a list of all students
            //"studentIDDictionary" is a dictionary of student names and their student IDs
            //"resourceDictionary" is a dictionary of resources and who checked them out
        //PrintStudentAccout() has no return value  
        static void PrintStudentAccount(List<string> studentList, Dictionary<string, int> studentIDDictionary, Dictionary<string, string> resourceDictionary)
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
            string studentName = studentList[studentNum - 1];                   //the student's name is extracted from the list by index number
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
            foreach (KeyValuePair<string, string> pair in resourceDictionary)   //Loop through all key value pairs in resourceDictionary
            {
                if (pair.Value == studentName)                                  //When the value equals the student name...
                {
                    Console.WriteLine($"*\t{num}.  {pair.Key,-28}*");           //...print the name of the resource they checked out
                    num++;
                    noResources = false;
                }
            }
            if (noResources)                                                    //...or let the user know that they didn't check out any resources
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
                UpdateStudentTextFile(studentName, studentIDDictionary, resourceDictionary);    //Update the text file
                PrintStudentTextFile(studentName, studentIDDictionary);                         //Print the text file to the screen
            }
        }

        //CheckoutResource() allows user to checkout an item
        //CheckoutResource() has parameters of type List called "studentList" which has a list of students, and type Dictionary called "resourceDictionary" which holds a list of resources and who checked them out
        //CheckoutResources() does not return a value
        static void CheckoutResource(List<string> studentList, Dictionary<string, string> resourceDictionary)
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
            string studentName = studentList[studentNum - 1];                           //The students name is extracted from the list by index number
            bool maxResources = MaxResources(studentName, resourceDictionary);      //Call maxResources() method to see if maximum resources checked out
            if (maxResources)
            {
                Console.WriteLine($"{studentName} has checked out a maximum number of 3 resources.");
                Console.WriteLine();
            }
            else
            { 
                List<string> numResources = PrintAvailableResources(resourceDictionary);    //Print list of available resources
                if (numResources.Count() > 0)
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
                        else if (resourceNum < 0 || resourceNum > numResources.Count())
                        {
                            validNum = false;
                        }
                        else
                        {
                            string resource = numResources[resourceNum - 1];        //The name of the resources is extracted from the list by index number
                            resourceDictionary[resource] = studentName;             //The resource key is assigned to the value of the student's name
                            Console.WriteLine();
                            Console.WriteLine($"{studentName} has checked out \"{resource}\"");
                        }
                    } while (!validNum);
                }
            }
        }
        
        //MaxResources() checks to see if the student already checked out the maximum of 3 resources
        //MaxResources() has parameters of type string called "studentName" that holds the students name and type Dictionary called "resourceDictionary" that holds the names of the resources and who checked them out
        //MaxResources() has a return value of type bool, which is set to true if the maximum number of resources have been checked out
        static bool MaxResources(string studentName, Dictionary<string, string> resourceDictionary)
        {
            bool maxResources = false;
            int count = 0;
            foreach (KeyValuePair<string, string> pair in resourceDictionary)       //Loop through all key value pairs in resourceDictionary
            {
                if (pair.Value == studentName)                                      //Count each time the student's name is found
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
        static void ReturnResource(List<string> studentList, Dictionary<string, string> resourceDictionary)
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
            string studentName = studentList[studentNum - 1];                           //The student's name is extracted from the list by index number

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

            foreach (KeyValuePair<string, string> pair in resourceDictionary)       //Check each key value pair in resourceDictionary
            {
                if (pair.Value == studentName)                                      //If the student's name is found...
                {
                    num++;
                    Console.WriteLine($"*\t{num}.  {pair.Key,-28}*");               //...the name of the resource is printed to the screen
                    studentResourceList.Add(pair.Key);                              //and the name of the resource is added to the list
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
                        resourceDictionary[resource] = "";                              //Set the value to "" for the resource name in resourceDictionary
                        Console.WriteLine();
                        Console.WriteLine($"{studentName} has returned \"{resource}\"");
                    }
                } while (!validNum);
            }
        }

        //UpdateStudentTextFile() updates the student account text file
        //UpdateStudentTextFile() has parameters of type string called "studentName" that holds the student's name, and type Dictionary called "studentIDDictionary" that holds student names and IDs and "resourceDictionary" that holds resource names and who checked them out
        //UpdateStudentTextFile() has no return value
        static void UpdateStudentTextFile(string studentName, Dictionary<string, int> studentIDDictionary, Dictionary<string, string> resourceDictionary )
        {
            string[] name = studentName.Split();                    //The students name is split into a string array of first name and last name

            StringBuilder studentFileName = new StringBuilder();    //The StringBuilder builds the file name consisting of the student's...
            studentFileName.Append(name[0].ToUpper());                  //...first name
            studentFileName.Append(name[1].ToUpper());                  //...last name
            studentFileName.Append(studentIDDictionary[studentName]);   //...and student ID
            studentFileName.Append(".txt");

            StringBuilder nameLine = new StringBuilder();           //The StringBuilder builds the student name line for the text file
            nameLine.Append("Student Name: ");
            nameLine.Append(studentName);
            nameLine.ToString();

            StringBuilder idLine = new StringBuilder();             //The StringBuilder builds the student ID line for the text file
            idLine.Append("Student ID: ");
            idLine.Append(studentIDDictionary[studentName]);
            idLine.ToString();

            StreamWriter writeStudentAcct = new StreamWriter(studentFileName.ToString());       //StreamWriter is created to write to the student file
            writeStudentAcct.WriteLine("BOOTCAMP RESOURCE LIBRARY STUDENT ACCOUNT INFORMATION");//Header information is added to the text file
            writeStudentAcct.WriteLine();
            writeStudentAcct.WriteLine(nameLine);                                               //includeing the students name
            writeStudentAcct.WriteLine(idLine);                                                 //...and their student ID
            writeStudentAcct.WriteLine();
            writeStudentAcct.WriteLine("Resources Checked Out: ");
            bool noResources = true;
            foreach (KeyValuePair<string, string> pair in resourceDictionary)
            {
                if (pair.Value == studentName)
                {
                    writeStudentAcct.WriteLine(pair.Key);                                     //All the student's resources are written to the file
                    noResources = false;
                }
            }
            if (noResources)
            {
                writeStudentAcct.WriteLine("(No resources checked out)");
            }
            writeStudentAcct.Close();
        }

        //UpdateCheckedOutTextFile() updates the text file of checked out resources
        //UpdateCheckedOutTextFile() has a parameter of type Dictionary called "resourceDictionary" that holds resource names and the student who has checked them out
        //UpdateCheckedOutTextFile() has no return values
        static void UpdateCheckedOutTextFile (Dictionary<string, string> resourceDictionary)
        {
            StreamWriter writeCheckedOut = new StreamWriter("BootcampResourcesCheckedOut.txt");     //StreamWriter is created to write to the text file
            writeCheckedOut.WriteLine("BOOTCAMP RESOURCE LIBRARY CHECKED OUT RESOURCES");           //Header information is written to the text file
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

        //PrintStudentTextFile() prints the student account text file to the screen
        //PrintStudentTextFile() has parameters of type string called "studentName" that holds the student's name and type Dictionary called "studentIDDictionary" that holds all student names and IDs
        //PrintStudentTextFile() has no return values
        static void PrintStudentTextFile(string studentName, Dictionary<string, int> studentIDDictionary)
        {
            string[] name = studentName.Split();                //The student's name is split into a string array of student's first and last name

            StringBuilder studentFileName = new StringBuilder();            //A StringBuilder builds the file name consisting of the student's...
            studentFileName.Append(name[0].ToUpper());                          //...first name
            studentFileName.Append(name[1].ToUpper());                          //...last name
            studentFileName.Append(studentIDDictionary[studentName]);           //...and student ID
            studentFileName.Append(".txt");

            StreamReader readStudentAccount = new StreamReader(studentFileName.ToString());     //StreamReader is declared to read from the file
            string line = "";
            do
            {
                line = readStudentAccount.ReadLine();
                Console.WriteLine(line);

            } while (line != null);

            readStudentAccount.Close();
        }

        //PrintCheckedOutTextFile() prints the text file containing all checked out resources and who checked them out
        //PrintCheckedOutTextFile() has a parameter of type Dictionary called "resourceDictionary" that holds a list of resources and which student checked them out
        //PrintCheckedOutTextFile() has no return values
        static void PrintCheckedOutTextFile (Dictionary<string, string> resourceDictionary)
        {
            Console.Write("Would you like to print a list of checked out resources? (Please enter y/n) ");
            string response = Console.ReadLine().ToUpper();
            if (response == "Y" || response == "YES" )
            {
                ResetScreen();
                UpdateCheckedOutTextFile(resourceDictionary);                                       //First the text file is updated
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
            
            //studentList list holds the name of all students
            List<string> studentList = new List<string>();
            studentList.Add("Jennifer Evans");
            studentList.Add("Mary Winkleman");
            studentList.Add("Kim Vargas");
            studentList.Add("Imari Childress");
            studentList.Add("Quinn Bennett");
            studentList.Add("Richard Raponi");
            studentList.Add("Cameron Robinson");
            studentList.Add("Krista Scholdberg");
            studentList.Add("Ashley Stewart");
            studentList.Add("Cadale Thomas");
            studentList.Add("Margaret Landefeld");
            studentList.Add("Lawrence Hudson");
            studentList.Add("Jacob Lockyer");
            studentList.Sort();

            //Instantiate the student objects

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

            //Instantiate the DVD objects
            DVD dvd1 = new DVD("C# for Dummies", "09-23423-5434", 67);
            DVD dvd2 = new DVD("Getting Along with Git", "09-34322-123", 124);
            DVD dvd3 = new DVD("JavaScript", "09-6335-123", 81);
            
            //Add DVD objects to a list
            List<DVD> DVDList = new List<DVD>();
            DVDList.Add(dvd1);
            DVDList.Add(dvd2);
            DVDList.Add(dvd3);

            Book book1 = new Book("Database Design", "08-541324-1234", 320);
            Book book2 = new Book("SQL Queries", "08-1243-212", 197);
            Book book3 = new Book("C# Player's Guide", "08-52342-23", 94);

            Magazine magazine1 = new Magazine("HTML & CSS", "07-4322-232", 38);
            Magazine magazine2 = new Magazine("Agile Methodology", "07-34122-6324", 52);
            Magazine magazine3 = new Magazine("Scrum 101", "07-1234-5423", 40);
            Magazine magazine4 = new Magazine("Hooray for Arrays!", "07-63208-45", 23);

            //Resource dictionary holds keys with the resource names and values equal to name of student who checked out item, or blank if not checked out
            //The dictionary is initialized with resource names from the resourceList
            Dictionary<string, string> resourceDictionary = new Dictionary<string, string>();
            foreach (string resource in resourceList)
            {
                resourceDictionary.Add(resource, "");                                       //Resource names and "" are added to the dictionary
            }

            //StudentID dictionary holds keys with the student names and values equal to the student ID
            //The dictionary is initialized with student names from the studentList and unique integer ID number
            Dictionary<string, int> studentIDDictionary = new Dictionary<string, int>();
            int studentID = 33;
            foreach (string student in studentList)
            {
                studentIDDictionary.Add(student, studentID);                                //Student names and IDs are added to the dictionary
                UpdateStudentTextFile(student, studentIDDictionary, resourceDictionary);    //Update the student text file for every student
                studentID = studentID + 57;         
            }

            do
            {
                string menuOption = Menu();                                                 //Present the main menu to the user
                switch (menuOption)                                                         //Run code based on the menu option chosen by the user
                {
                    case "S":               //"View Student List" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        PrintStudentList(studentList);                                      //Print the student list
                        Console.Write("Press any key to return to the Menu...");            //The user presses a key to continue
                        Console.ReadKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "R":               //"View All Resources" menu option
                        break;
                    case "A":               //"View Available Resources" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        List<string> unused = PrintAvailableResources(resourceDictionary);  //Print the available resources
                        PrintCheckedOutTextFile(resourceDictionary);                        //Allow user to print checked out resources text file
                        Console.Write("Press any key to return to the Menu...");            //The user presses a key to continue
                        Console.ReadKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "E":               //"Edit Resources" menu option
                        break;
                    case "C":               //"View Student Account" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        PrintStudentList(studentList);                                      //Print the student list
                        PrintStudentAccount(studentList, studentIDDictionary, resourceDictionary);  //Print the student account
                        Console.Write("Press any key to return to the Menu...");            //The user presses a key to continue
                        Console.ReadKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "O":               //"Checkout Resource" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        PrintStudentList(studentList);                                      //Print the student list
                        CheckoutResource(studentList, resourceDictionary);                  //Allow the user to checkout a resource
                        Console.Write("Press any key to return to the Menu...");            //The user presses a key to continue
                        Console.ReadKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "I":               //"CheckIn Resource" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        PrintStudentList(studentList);                                      //Print the student list
                        ReturnResource(studentList, resourceDictionary);                    //Allow the user to return a resource
                        Console.Write("Press any key to return to the Menu...");            //The user presses a key to continue
                        Console.ReadKey();
                        ResetScreen();                                                      //Clear screen; print title
                        break;                                                              //The do-while loop continues; Menu() is called
                    case "X":               //"Exit" menu option
                        ResetScreen();                                                      //Clear screen; print title
                        Console.WriteLine("Goodbye!");                                      //Gives the user a "Goodbye" message
                        Console.WriteLine();
                        runProgram = false;                                                 //Breaks out of do-while loop
                        break;
                    case "T":               //"Test" menu option
                        foreach(DVD item in DVDList)
                        {
                            Console.WriteLine(item.Title);

                        }
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
