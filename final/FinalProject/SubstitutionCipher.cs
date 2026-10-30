using System.Text;

public class SubstitutionCipher : Cipher
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string _keyAlphabet;

    public SubstitutionCipher(string keyAlphabet) : base("Substitution")
    {
        // Safety net: callers should check IsValidKey first,
        // but a bad key falls back to the plain alphabet (no change to the text).
        if (IsValidKey(keyAlphabet))
        {
            _keyAlphabet = keyAlphabet.ToUpper();
        }
        else
        {
            _keyAlphabet = Alphabet;
        }
    }

    // True if the key has exactly 26 characters, using each letter A-Z once.
    public static bool IsValidKey(string key)
    {
        if (key.Length != 26)
        {
            return false;
        }

        bool[] seen = new bool[26];

        foreach (char c in key.ToUpper())
        {
            if (c < 'A' || c > 'Z')
            {
                return false;
            }

            if (seen[c - 'A'])
            {
                return false;
            }

            seen[c - 'A'] = true;
        }

        return true;
    }

    public override string Encrypt(string plaintext)
    {
        return Translate(plaintext, Alphabet, _keyAlphabet);
    }

    public override string Decrypt(string ciphertext)
    {
        return Translate(ciphertext, _keyAlphabet, Alphabet);
    }

    private string Translate(string text, string from, string to)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in text)
        {
            if (c >= 'A' && c <= 'Z')
            {
                result.Append(to[from.IndexOf(c)]);
            }
            else if (c >= 'a' && c <= 'z')
            {
                char upper = char.ToUpper(c);
                result.Append(char.ToLower(to[from.IndexOf(upper)]));
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }
}