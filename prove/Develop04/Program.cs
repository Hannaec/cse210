// EXCEEDING REQUIREMENTS:
// TODO: describe your extras here

class Program
{
    static void Main(string[] args)
    {
        int choice;
        do
        {
            // Display Choices and get user's choice
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start breathing activity");
            Console.WriteLine("2. Start reflection activity");
            Console.WriteLine("3. Start listing activity");
            Console.WriteLine("4. Quit");
            Console.Write("Select a choice from the menu: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                choice = 0;
            }

            if (choice == 1) //1. Breathing Activity
            {
                BreathingActivity breathingActivity = new BreathingActivity();
                breathingActivity.Run();
            }

            else if (choice == 2) //2. Reflection Activity
            {
                ReflectionActivity reflectionActivity = new ReflectionActivity();
                reflectionActivity.Run();
            }

            else if (choice == 3) //3. Listing Activity
            {
                ListingActivity listingActivity = new ListingActivity();
                listingActivity.Run();
            }

            else if (choice == 4) //4. Quit
            {
                Console.WriteLine("Thank you for using the program. Goodbye!");
            }
            
            else
            {
                Console.WriteLine("Invalid choice. Please select a valid option.");
            }
        } while (choice != 4);
    }
}