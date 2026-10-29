public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse1;
    private int _endVerse1;
    private int _startVerse2;
    private int _endVerse2;

    public Reference(string book, int chapter, int startVerse)
        : this(book, chapter, startVerse, startVerse, 0, 0)
    {
    }
    public Reference(string book, int chapter, int startVerse, int endVerse)
        : this(book, chapter, startVerse, endVerse, 0, 0)
    {
    }

    public Reference(string book, int chapter, int startVerse1, int endVerse1, int startVerse2, int endVerse2)
    {
        _book = book;
        _chapter = chapter;
        _startVerse1 = startVerse1;
        _endVerse1 = endVerse1;
        _startVerse2 = startVerse2;
        _endVerse2 = endVerse2;
    }

    private string FormatRange(int start, int end)
    {
        if (start == end)
        {
            return $"{start}";
        }
        else
        {
            return $"{start}–{end}";
        }
    }

    public string GetDisplayText()
    {
        string text = $"{_book} {_chapter}:{FormatRange(_startVerse1, _endVerse1)}";

        if (_startVerse2 != 0)
        {
            text += $", {FormatRange(_startVerse2, _endVerse2)}";
        }

        return text;
    }
}