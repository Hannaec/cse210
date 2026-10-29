using System;

//To exceed requirements, my program pulls a random scripture from a library of scriptures stored in a text file. I also added the possibility of having two verse ranges in a scripture reference so that all of the scripture masteries can be stored correctly. I did not all the scripture masteries because I could not find a complete list of the references with the text in one place, but I did add a few of the scripture masteries for demonstration purposes.
class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = new List<Scripture>();
        
        string[] lines = System.IO.File.ReadAllLines("library.txt");

        foreach (string line in lines)
        {
            string[] parts = line.Split("`");
            scriptures.Add(new Scripture(new Reference(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5])), parts[6]));
        }

        Random rand = new Random();
        int index = rand.Next(scriptures.Count);
        Scripture scripture = scriptures[index];
        string input = "";
        scripture.Display();
        do
        {
            Console.WriteLine("\nPress enter to continue or type 'quit' to finish.");
            input = Console.ReadLine();
            if (input.ToLower() != "quit")
            {
                scripture.HideRandomWords(3);
                scripture.Display();
            }
        } while (!scripture.IsCompletelyHidden() && input.ToLower() != "quit");
    }
}