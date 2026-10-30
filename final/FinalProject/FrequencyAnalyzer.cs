using System;
using System.Collections.Generic;

// Counts letters in a ciphertext and compares them to typical English frequencies.
public class FrequencyAnalyzer
{
    private Dictionary<char, double> _englishFrequencies;

    public FrequencyAnalyzer()
    {
        // Typical English letter frequencies, as percentages
        _englishFrequencies = new Dictionary<char, double>
        {
            { 'A', 8.167 }, { 'B', 1.492 }, { 'C', 2.782 }, { 'D', 4.253 },
            { 'E', 12.702 }, { 'F', 2.228 }, { 'G', 2.015 }, { 'H', 6.094 },
            { 'I', 6.966 }, { 'J', 0.153 }, { 'K', 0.772 }, { 'L', 4.025 },
            { 'M', 2.406 }, { 'N', 6.749 }, { 'O', 7.507 }, { 'P', 1.929 },
            { 'Q', 0.095 }, { 'R', 5.987 }, { 'S', 6.327 }, { 'T', 9.056 },
            { 'U', 2.758 }, { 'V', 0.978 }, { 'W', 2.360 }, { 'X', 0.150 },
            { 'Y', 1.974 }, { 'Z', 0.074 }
        };
    }

    // Counts each letter A-Z (case-insensitive). Only letters that appear are included.
    public Dictionary<char, int> CountLetters(string text)
    {
        Dictionary<char, int> counts = new Dictionary<char, int>();

        foreach (char c in text)
        {
            char upper = char.ToUpper(c);

            if (upper >= 'A' && upper <= 'Z')
            {
                if (counts.ContainsKey(upper))
                {
                    counts[upper]++;
                }
                else
                {
                    counts[upper] = 1;
                }
            }
        }

        return counts;
    }

    // Each letter's share of the text's letters, as a percentage.
    public Dictionary<char, double> GetFrequencies(string text)
    {
        Dictionary<char, int> counts = CountLetters(text);
        int total = 0;

        foreach (int count in counts.Values)
        {
            total += count;
        }

        Dictionary<char, double> frequencies = new Dictionary<char, double>();

        foreach (KeyValuePair<char, int> pair in counts)
        {
            frequencies[pair.Key] = pair.Value * 100.0 / total;
        }

        return frequencies;
    }

    // Chi-squared score: LOWER means the text looks more like English.
    public double ScoreAgainstEnglish(string text)
    {
        Dictionary<char, int> counts = CountLetters(text);
        int total = 0;

        foreach (int count in counts.Values)
        {
            total += count;
        }

        if (total == 0)
        {
            return double.MaxValue;
        }

        double score = 0;

        foreach (KeyValuePair<char, double> pair in _englishFrequencies)
        {
            double expected = pair.Value / 100.0 * total;
            int observed = counts.ContainsKey(pair.Key) ? counts[pair.Key] : 0;

            score += (observed - expected) * (observed - expected) / expected;
        }

        return score;
    }
}