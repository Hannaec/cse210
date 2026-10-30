using System;
using System.IO;

//To exceed requirements, my program keeps track of the file so that the user does not have to be prompted more than once. It loads the file before saving to prevent losing previous entries. The user can also choose whether they want to use the randomly selected prompt or a different one.
class Program
{
    static void Main(string[] args)
    {
        //Loads and saves files from ...\cse210\prove\Develop02\
        Journal myJournal = new Journal();
        Console.WriteLine("Welcome to the Journal Program!");
        int choice;
        string filename = "";
        do
        {
            // Display Choices and get user's choice
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("> ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                choice = 0;
            }

            if (choice == 1) //1. Write
            {
                myJournal.Write();
            }

            else if (choice == 2) //2. Display
            {
                // 2. Displays the journal entries 
                // (only displays the day's entries unless the saved journal has been loaded)
                myJournal.Display();
            }

            else if (choice == 3) //3. Load
            {
                //Loads the journal from the filename
                if (filename == "")
                {
                    Console.WriteLine("What is the file name?");
                    filename = Console.ReadLine();
                }
                myJournal.Load(filename);
            }

            else if (choice == 4) //4. Save
            {
                if (filename == "")
                {
                    Console.WriteLine("What is the file name?");
                    filename = Console.ReadLine();
                }
                myJournal.Save(filename);
            }

            else if (choice == 5) //5. Quit
            {
                Console.WriteLine("Thank you for using the program. Goodbye!");
            }

            else
            {
                Console.WriteLine("Invalid choice. Please select a valid option.");
            }
        } while (choice != 5);
    }
}