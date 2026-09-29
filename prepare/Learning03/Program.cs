using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction fraction = new Fraction();
        Console.WriteLine(1 == fraction.GetTop() && 1 == fraction.GetBottom());
        Console.WriteLine(fraction.GetFractionString());
        Console.WriteLine(fraction.GetDecimalValue());

        Fraction fraction1 = new Fraction(5);
        Console.WriteLine(5 == fraction1.GetTop() && 1 == fraction1.GetBottom());
        Console.WriteLine(fraction1.GetFractionString());
        Console.WriteLine(fraction1.GetDecimalValue());

        Fraction fraction2 = new Fraction(4,7);
        Console.WriteLine(4 == fraction2.GetTop() && 7 == fraction2.GetBottom());
        Console.WriteLine(fraction2.GetFractionString());
        Console.WriteLine(fraction2.GetDecimalValue());

        Fraction fraction3 = new Fraction(11,2);
        Console.WriteLine(11 == fraction3.GetTop() && 2 == fraction3.GetBottom());
        Console.WriteLine(fraction3.GetFractionString());
        Console.WriteLine(fraction3.GetDecimalValue());
        fraction3.SetTop(12);
        fraction3.SetBottom(9);
        Console.WriteLine(12 == fraction3.GetTop() && 9 == fraction3.GetBottom());
        Console.WriteLine(fraction3.GetFractionString());
        Console.WriteLine(fraction3.GetDecimalValue());
        
        Fraction fraction4 = new Fraction();
        Random rand = new Random();
        for (int i = 0; i < 30; i++)
        {
            int top = rand.Next(1,20);
            int bottom = rand.Next(1,20);
            fraction4.SetTop(top);
            fraction4.SetBottom(bottom);

            Console.WriteLine($"Fraction {i + 1}: string: {fraction4.GetFractionString()} Number: {fraction4.GetDecimalValue()}");
        }
    }
}