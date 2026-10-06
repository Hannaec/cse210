using System;
using System.IO;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        //Loads and saves files from ...\cse210\prove\Develop02\
        Journal myJournal = new Journal();
        Console.WriteLine("Welcome to the Journal Program!");
        int choice;
        string prompt;
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

            choice = int.Parse(Console.ReadLine());

            if (choice == 1) //1. Write
            {
                Entry entry = new Entry();
                DateTime theCurrentTime = DateTime.Now;
                entry._date = theCurrentTime.ToShortDateString();

                prompt = myJournal.Prompt();
                entry._prompt = prompt;

                Console.WriteLine(prompt);
                entry._response = Console.ReadLine();
         
                myJournal._entryList.Add(entry);
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
                Console.WriteLine("What is the file name?");
                string filename = Console.ReadLine();
                string[] lines = System.IO.File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    string[] parts = line.Split("`");

                    Entry entry = new Entry();
                    entry._date = parts[0];
                    entry._prompt = parts[1];
                    entry._response = parts[2];

                    myJournal._entryList.Add(entry);
                }
            }

            else if (choice == 4) //4. Save
            {
                //First loads the journal from the filename
                Console.WriteLine("What is the file name?");
                string filename = Console.ReadLine();
                string[] lines = System.IO.File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    string[] parts = line.Split("`");

                    Entry entry = new Entry();
                    entry._date = parts[0];
                    entry._prompt = parts[1];
                    entry._response = parts[2];

                    myJournal._entryList.Add(entry);
                }
                
                //Then saves the file to the filename (This prevents overwriting)
                myJournal.Save(filename);
            }

            else if (choice == 5) //5. Quit
            {
                
            }
        } while (choice != 5);
    }
}