using System;
using Microsoft.VisualBasic;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();

        string name = PromptUserName();
        int number = PromptUserNumber();
        int squared = SquareNumber(number);

        int year;
        PromtUserBirthYear(out year);
        DisplayResult(name, squared, year);
    }

    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());
        return number;
    }

    static void PromtUserBirthYear(out int year)
    {
        Console.Write("Please enter the year you were born: ");
        year = int.Parse(Console.ReadLine());
    }

    static int SquareNumber(int number)
    {
        int squared = number*number;
        return squared;
    }

    static void DisplayResult(string name, int squared, int year)
    {
        Console.WriteLine($"{name}, the square of your number is {squared}.");
        Console.WriteLine($"{name}, you will turn {2026 - year} this year.");
    }
}