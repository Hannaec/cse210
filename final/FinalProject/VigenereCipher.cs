using System.Text;

public class VigenereCipher : Cipher
{
    private string _keyword;

    public VigenereCipher(string keyword) : base("Vigenere")
    {
        _keyword = CleanKeyword(keyword);

        // Safety net: callers should check IsValidKeyword first,
        // but the class never ends up with an empty keyword.
        if (_keyword.Length == 0)
        {
            _keyword = "A";
        }
    }

    // True if the keyword contains at least one letter A-Z.
    public static bool IsValidKeyword(string keyword)
    {
        foreach (char c in keyword)
        {
            if (IsAsciiLetter(c))
            {
                return true;
            }
        }

        return false;
    }

    public override string Encrypt(string plaintext)
    {
        return Transform(plaintext, 1);
    }

    public override string Decrypt(string ciphertext)
    {
        return Transform(ciphertext, -1);
    }

    private static string CleanKeyword(string keyword)
    {
        StringBuilder cleaned = new StringBuilder();

        foreach (char c in keyword)
        {
            if (IsAsciiLetter(c))
            {
                cleaned.Append(char.ToUpper(c));
            }
        }

        return cleaned.ToString();
    }

    private string Transform(string text, int direction)
    {
        StringBuilder result = new StringBuilder();
        int keyIndex = 0;

        foreach (char c in text)
        {
            if (IsAsciiLetter(c))
            {
                int shift = (_keyword[keyIndex % _keyword.Length] - 'A') * direction;
                char start = char.IsUpper(c) ? 'A' : 'a';
                result.Append(Rotate(c, start, shift));
                keyIndex++;
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }
}