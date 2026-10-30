using System;
using System.Collections.Generic;
using System.Linq;

// Handles all user interaction.
public class Menu
{
    private const string DefaultSubstitutionKey = "QWERTYUIOPASDFGHJKLZXCVBNM";

    private FrequencyAnalyzer _analyzer;
    private CodeBreaker _codeBreaker;

    public Menu()
    {
        _analyzer = new FrequencyAnalyzer();
        _codeBreaker = new CodeBreaker(_analyzer);
    }

    public void Run()
    {
        bool running = true;

        string choice;
        do
        {
            Console.WriteLine();
            Console.WriteLine("=== Cipher Toolkit ===");
            Console.WriteLine("1. Encrypt a message");
            Console.WriteLine("2. Decrypt a message");
            Console.WriteLine("3. Analyze letter frequencies");
            Console.WriteLine("4. Try to break a cipher");
            Console.WriteLine("5. Quit");

            choice = ReadText("Choose an option: ");

            if (choice == "1")
            {
                EncryptMessage();
            }
            else if (choice == "2")
            {
                DecryptMessage();
            }
            else if (choice == "3")
            {
                AnalyzeMessage();
            }
            else if (choice == "4")
            {
                BreakCipher();
            }
            else if (choice == "5")
            {
                Console.WriteLine("Thank you for using the program. Goodbye!");
            }
            else
            {
                Console.WriteLine("Please choose 1-5.");
            }
        } while (choice != "5");
    }

    private string ReadText(string prompt)
    {
        Console.Write(prompt);
        string line = Console.ReadLine();
        return line ?? "";
    }

    // Keeps asking until the text contains at least one letter.
    private string ReadTextWithLetters(string prompt)
    {
        string text = ReadText(prompt);

        while (_analyzer.CountLetters(text).Count == 0)
        {
            Console.WriteLine("Please enter some text that contains letters.");
            text = ReadText(prompt);
        }

        return text;
    }

    private int ReadShift()
    {
        int shift;
        Console.Write("Enter the shift (a whole number): ");

        while (!int.TryParse(Console.ReadLine(), out shift))
        {
            Console.Write("That isn't a whole number. Try again: ");
        }

        return shift;
    }

    private string ReadKeyword()
    {
        string keyword = ReadText("Enter the keyword: ");

        while (!VigenereCipher.IsValidKeyword(keyword))
        {
            Console.WriteLine("The keyword needs at least one letter.");
            keyword = ReadText("Enter the keyword: ");
        }

        return keyword;
    }

    private string ReadSubstitutionKey()
    {
        string prompt = "Enter a 26-letter key, each letter once (press Enter for the default): ";
        string key = ReadText(prompt).Trim();

        while (key != "" && !SubstitutionCipher.IsValidKey(key))
        {
            Console.WriteLine("The key must use each letter A-Z exactly once.");
            key = ReadText(prompt).Trim();
        }

        if (key == "")
        {
            key = DefaultSubstitutionKey;
        }

        return key;
    }

    // Asks which cipher and which key, then returns it as a Cipher.
    private Cipher ChooseCipher()
    {
        string choice = "";

        while (choice != "1" && choice != "2" && choice != "3")
        {
            Console.WriteLine("Which cipher?");
            Console.WriteLine("1. Caesar");
            Console.WriteLine("2. Vigenere");
            Console.WriteLine("3. Substitution");

            choice = ReadText("Choose a cipher: ");

            if (choice != "1" && choice != "2" && choice != "3")
            {
                Console.WriteLine("Please choose 1-3.");
            }
        }

        if (choice == "1")
        {
            return new CaesarCipher(ReadShift());
        }
        else if (choice == "2")
        {
            return new VigenereCipher(ReadKeyword());
        }
        else
        {
            return new SubstitutionCipher(ReadSubstitutionKey());
        }
    }

    private void EncryptMessage()
    {
        Cipher cipher = ChooseCipher();
        Message message = new Message(ReadText("Enter your message: "), false);

        Message result = new Message(cipher.Encrypt(message.GetText()), true);
        Console.WriteLine(result.GetDisplayString());
    }

    private void DecryptMessage()
    {
        Cipher cipher = ChooseCipher();
        Message message = new Message(ReadText("Enter the ciphertext: "), true);

        Message result = new Message(cipher.Decrypt(message.GetText()), false);
        Console.WriteLine(result.GetDisplayString());
    }

    private void AnalyzeMessage()
    {
        string text = ReadTextWithLetters("Enter the text to analyze: ");

        Dictionary<char, double> frequencies = _analyzer.GetFrequencies(text);

        foreach (KeyValuePair<char, double> pair in frequencies.OrderByDescending(p => p.Value))
        {
            Console.WriteLine($"{pair.Key}: {pair.Value:F1}%");
        }
    }

    private void BreakCipher()
    {
        Console.WriteLine("Which cipher do you want to break?");
        Console.WriteLine("1. Caesar");
        Console.WriteLine("2. Vigenere");

        string choice = ReadText("Choose a cipher: ");

        if (choice == "1")
        {
            BreakCaesar();
        }
        else if (choice == "2")
        {
            BreakVigenere();
        }
        else
        {
            Console.WriteLine("Please choose 1 or 2.");
        }
    }

    private void BreakCaesar()
    {
        string text = ReadTextWithLetters("Enter the ciphertext: ");

        int shift = _codeBreaker.GuessCaesarShift(text);
        Cipher cipher = new CaesarCipher(shift);

        Console.WriteLine($"Best guess for the shift: {shift}");
        Console.WriteLine(new Message(cipher.Decrypt(text), false).GetDisplayString());
    }

    private void BreakVigenere()
    {
        string text = ReadTextWithLetters("Enter the ciphertext: ");

        if (_analyzer.CountLetters(text).Values.Sum() < 100)
        {
            Console.WriteLine("Note: short messages make this guess unreliable. A few hundred letters works best.");
        }

        int keyLength = _codeBreaker.GuessVigenereKeyLength(text);
        string keyword = _codeBreaker.GuessVigenereKeyword(text, keyLength);
        Cipher cipher = new VigenereCipher(keyword);

        Console.WriteLine($"Best guess for the key length: {keyLength}");
        Console.WriteLine($"Best guess for the keyword: {keyword}");
        Console.WriteLine(new Message(cipher.Decrypt(text), false).GetDisplayString());
    }
}