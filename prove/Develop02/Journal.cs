public class Journal
{
    
    public List<Entry> _entryList = new List<Entry>();
    public List<string> _prompts = new List<string>{
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is one thing I did well today?",
        "Who did I help today, or who helped me?",
        "What do I need more of in my life right now? What do I need less of?"
    };
    private bool _hasLoaded = false;
    Random rand = new Random();

    public void Write()
    {
        Entry entry = new Entry();
        DateTime theCurrentTime = DateTime.Now;
        entry._date = theCurrentTime.ToShortDateString();

        string prompt = Prompt();
        entry._prompt = prompt;

        Console.WriteLine(prompt);
        entry._response = Console.ReadLine();

        _entryList.Add(entry);
    }

    public void Display()
    {
        foreach (Entry entry in _entryList)
        {
            Console.WriteLine();
            entry.Display();
        }
    }

    public void Load(string filename)
    {
        if (_hasLoaded)
        {
            Console.WriteLine($"A journal has already been loaded or saved this session. Current file: {filename}");
            return;
        }

        string[] lines = System.IO.File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split("`");

            Entry entry = new Entry();
            entry._date = parts[0];
            entry._prompt = parts[1];
            entry._response = parts[2];

            _entryList.Add(entry);
        }

        _hasLoaded = true;
    }

    public void Save(string filename)
    {
        //If nothing has been loaded yet, load the file first so it isn't overwritten
        if (_hasLoaded == false)
        {
            Load(filename);
        }

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entryList)
            {
                outputFile.WriteLine($"{entry._date}`{entry._prompt}`{entry._response}");
            }
        }

        _hasLoaded = true;
    }

    public string Prompt()
    {
        string prompt = _prompts[rand.Next(_prompts.Count)];

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"Prompt: {prompt}");
            Console.WriteLine("1. Use this prompt");
            Console.WriteLine("2. Get a different random prompt");
            Console.WriteLine("3. Choose from the list");
            Console.Write("> ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                return prompt;
            }
            else if (input == "2")
            {
                prompt = _prompts[rand.Next(_prompts.Count)];
            }
            else if (input == "3")
            {
                for (int i = 0; i < _prompts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_prompts[i]}");
                }
                Console.Write("> ");

                if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= _prompts.Count)
                {
                    return _prompts[number - 1];
                }
                Console.WriteLine("That isn't a number on the list. Random prompt selected.");
            }
            else
            {
                Console.WriteLine("Please enter 1, 2, or 3.");
            }
        }
    }
}