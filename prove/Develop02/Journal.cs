public class Journal
{
    
    public List<Entry> _entryList = new List<Entry>();
    public List<string> _prompts = new List<string>{
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };
    Random rand = new Random();

    // public string GetPrompt()
    // {

    // }
    public void Display()
    {
        foreach (Entry entry in _entryList)
        {
            Console.WriteLine();
            entry.Display();
        }
    }

    public void Save(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entryList)
            {
                outputFile.WriteLine($"{entry._date}`{entry._prompt}`{entry._response}");
            }
        }
    }

    public string Prompt()
    {
        return _prompts[rand.Next(_prompts.Count)];
    }
}