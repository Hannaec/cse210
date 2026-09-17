using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        
        int guess;
        string again = "";

        do
        {
            int magicNumber = randomGenerator.Next(1, 100);
            int tries = 0;
            
            do
            {
                tries += 1;

                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else if (guess == magicNumber)
                {
                    Console.WriteLine($"You guessed it! It took {tries} tries.");
                }
            } while (guess != magicNumber);

            Console.Write("Would you like to play again? ");
            again = Console.ReadLine();
        } while (again == "yes");
    }
}