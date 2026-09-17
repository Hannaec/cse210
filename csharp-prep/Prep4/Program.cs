using System;

class Program
{
    static void Main(string[] args)
    {
        List<float> numbersList = new List<float>();
        float userNumber;
        
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        do
        {
            Console.Write("Enter number: ");
            userNumber = float.Parse(Console.ReadLine());
            if (userNumber != 0)
            {
                numbersList.Add(userNumber);
            }
        } while (userNumber != 0);
        numbersList.Sort();

        float sum = 0;
        float greatest = numbersList[^1];
        foreach (float number in numbersList)
        {
            sum += number;
        }

        string message;
        float leastPositive;
        if (greatest > 0)
        {
            leastPositive = greatest;
            foreach (float number in numbersList)
            {
                if (number > 0 && number < leastPositive)
                {
                    leastPositive = number;
                }
            }
            message = $"{leastPositive}";
        }
        else
        {
            message = "N/A. No positive numbers were entered.";
        }
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {sum/numbersList.Count}");
        Console.WriteLine($"The largest number is: {greatest}");
        Console.WriteLine($"The smallest positive number is: {message}");

        foreach (float number in numbersList)
            {
                Console.WriteLine(number);
            }
    }
}