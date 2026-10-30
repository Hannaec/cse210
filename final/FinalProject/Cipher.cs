using System;

// Abstract base class: defines what every cipher must do, not how.
public abstract class Cipher
{
    private string _name;

    public Cipher(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }

    public abstract string Encrypt(string plaintext);
    public abstract string Decrypt(string ciphertext);

    // Shared by the ciphers: shifts one letter within its alphabet, wrapping around.
    protected char Rotate(char c, char start, int amount)
    {
        int position = c - start;
        int shifted = ((position + amount) % 26 + 26) % 26;
        return (char)(start + shifted);
    }

    // Shared by the ciphers: true only for A-Z and a-z.
    protected static bool IsAsciiLetter(char c)
    {
        return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
    }
}