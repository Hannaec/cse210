using System;
using System.Collections.Generic;
using System.Text;

// Uses the FrequencyAnalyzer to guess keys for simple ciphers.
public class CodeBreaker
{
    private FrequencyAnalyzer _analyzer;

    public CodeBreaker(FrequencyAnalyzer analyzer)
    {
        _analyzer = analyzer;
    }

    // Tries all 26 shifts and returns the one whose result looks most like English.
    public int GuessCaesarShift(string ciphertext)
    {
        int bestShift = 0;
        double bestScore = double.MaxValue;

        for (int shift = 0; shift < 26; shift++)
        {
            CaesarCipher candidate = new CaesarCipher(shift);
            string attempt = candidate.Decrypt(ciphertext);
            double score = _analyzer.ScoreAgainstEnglish(attempt);

            if (score < bestScore)
            {
                bestScore = score;
                bestShift = shift;
            }
        }

        return bestShift;
    }

    // Returns the smallest key length (up to 20) whose columns look like English.
    public int GuessVigenereKeyLength(string ciphertext)
    {
        string letters = GetLetters(ciphertext);
        int maxLength = Math.Min(20, letters.Length / 2);

        int bestLength = 1;
        double bestIC = 0;

        for (int length = 1; length <= maxLength; length++)
        {
            double ic = AverageIndexOfCoincidence(letters, length);

            if (ic >= 0.06)
            {
                return length;
            }

            if (ic > bestIC)
            {
                bestIC = ic;
                bestLength = length;
            }
        }

        return bestLength;
    }

    // Given a key length, breaks each column as its own Caesar cipher to recover the keyword.
    public string GuessVigenereKeyword(string ciphertext, int keyLength)
    {
        string letters = GetLetters(ciphertext);
        StringBuilder keyword = new StringBuilder();

        for (int col = 0; col < keyLength; col++)
        {
            StringBuilder column = new StringBuilder();

            for (int i = col; i < letters.Length; i += keyLength)
            {
                column.Append(letters[i]);
            }

            int shift = GuessCaesarShift(column.ToString());
            keyword.Append((char)('A' + shift));
        }

        return keyword.ToString();
    }

    // Keeps only A-Z, uppercased, so columns line up with how Vigenere counts letters.
    private string GetLetters(string text)
    {
        StringBuilder letters = new StringBuilder();

        foreach (char c in text)
        {
            char upper = char.ToUpper(c);

            if (upper >= 'A' && upper <= 'Z')
            {
                letters.Append(upper);
            }
        }

        return letters.ToString();
    }

    // Splits the letters into columns (every Nth letter) and averages their index of coincidence.
    private double AverageIndexOfCoincidence(string letters, int length)
    {
        StringBuilder[] columns = new StringBuilder[length];
        for (int i = 0; i < length; i++)
        {
            columns[i] = new StringBuilder();
        }

        for (int i = 0; i < letters.Length; i++)
        {
            columns[i % length].Append(letters[i]);
        }

        double total = 0;
        int usedColumns = 0;

        foreach (StringBuilder column in columns)
        {
            int n = column.Length;
            if (n < 2)
            {
                continue;
            }

            Dictionary<char, int> counts = _analyzer.CountLetters(column.ToString());
            double sum = 0;

            foreach (int count in counts.Values)
            {
                sum += count * (count - 1);
            }

            total += sum / (n * (n - 1));
            usedColumns++;
        }

        return usedColumns == 0 ? 0 : total / usedColumns;
    }
}