// Holds a piece of text and whether it is currently plaintext or ciphertext.
public class Message
{
    private string _text;
    private bool _isEncrypted;

    public Message(string text, bool isEncrypted)
    {
        _text = text;
        _isEncrypted = isEncrypted;
    }

    public string GetText() => _text;

    public void SetText(string text)
    {
        _text = text;
    }

    public bool IsEncrypted() => _isEncrypted;

    public void SetEncrypted(bool isEncrypted)
    {
        _isEncrypted = isEncrypted;
    }

    public string GetDisplayString()
    {
        string label = _isEncrypted ? "Ciphertext" : "Plaintext";
        return $"{label}: {_text}";
    }
}