public class Scripture
{
    private Reference _reference;
    private List<Word> _text = new List<Word>();
    private List<Word> _visibleWords = new List<Word>();
    private Random _rand = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;

        string[] parts = text.Split(' ');
        foreach (string part in parts)
        {
            Word word = new Word(part);
            _text.Add(word);
            _visibleWords.Add(word);
        }
    }
    public void HideRandomWords(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_visibleWords.Count != 0)
            {
                int index = _rand.Next(_visibleWords.Count);
                _visibleWords[index].Hide();
                _visibleWords.RemoveAt(index);
            }
        }
    }

    public bool IsCompletelyHidden()
    {
        if (_visibleWords.Count == 0)
            {
                return true;
            }
        else    
        {
            return false;
        }
    }
    
    public void Display()
    {
        Console.Clear();
        Console.Write(_reference.GetDisplayText());
        foreach (Word word in _text)
        {
            Console.Write($" {word.GetDisplayText()}");
        }
    }
}
