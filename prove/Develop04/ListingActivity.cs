public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    private List<string> GetListFromUser(int duration)
    {
        List<string> items = new List<string>();
        StartTimer();
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now <= endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                items.Add(input);
            }
        }
        return items;
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("List as many responses as you can to the following prompt:");
        string prompt = GetRandomItem(_prompts);
        Console.WriteLine($"--- {prompt} ---");
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.WriteLine();
        List<string> entries = GetListFromUser(GetDuration());
        Console.WriteLine($"You listed {entries.Count} items!");
        SaveToFile(prompt, entries);
        DisplayEndingMessage();
    }

    private void SaveToFile(string prompt, List<string> entries)
    {
        List<string> lines = new List<string>
    {
        $"{DateTime.Now}",
        prompt
    };

        foreach (string entry in entries)
        {
            lines.Add($"  - {entry}");
        }

        lines.Add("");   // blank line between sessions

        File.AppendAllLines("listing_log.txt", lines);
    }
}