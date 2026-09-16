using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percent? ");
        float gradePercent = float.Parse(Console.ReadLine());

        string letter = "";

        if (gradePercent >=90)
        {
            letter = "A";
        }
        else if (gradePercent >=80)
        {
            letter = "B";
        }
        else if (gradePercent >=70)
        {
            letter = "C";
        }
        else if (gradePercent >=60)
        {
            letter = "D";
        }
        else if (gradePercent >= 0)
        {
            letter = "F";
        }
        else if (gradePercent < 0)
        {
            letter = "negative. Please try again";
        }

        string sign = "";

        if (!(letter == "A" || letter == "F" || gradePercent < 0) && gradePercent % 10 >= 7)
        {
            sign = "+";
        }
        else if (gradePercent >= 60 && gradePercent % 10 < 3)
        {
            sign = "-";
        }

        Console.WriteLine($"Your grade is {letter}{sign}.");

        if (gradePercent >= 70)
        {
            Console.WriteLine("You passed! :)");
        }
        else if (gradePercent >= 0)
        {
            Console.WriteLine("Better luck next time. :(");
        }
    }
}