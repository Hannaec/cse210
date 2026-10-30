using System.Text;

public class CaesarCipher : Cipher
{
    private int _shift;

    public CaesarCipher(int shift) : base("Caesar")
    {
        _shift = shift;
    }

    public override string Encrypt(string plaintext)
    {
        return ShiftText(plaintext, _shift);
    }

    public override string Decrypt(string ciphertext)
    {
        return ShiftText(ciphertext, -_shift);
    }

    private string ShiftText(string text, int amount)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in text)
        {
            if (c >= 'A' && c <= 'Z')
            {
                result.Append(Rotate(c, 'A', amount));
            }
            else if (c >= 'a' && c <= 'z')
            {
                result.Append(Rotate(c, 'a', amount));
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }
}