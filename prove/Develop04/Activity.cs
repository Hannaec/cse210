public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    protected int GetDuration() => _duration;

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    private DateTime _startTime;

    protected void StartTimer()
    {
        _startTime = DateTime.Now;
    }

    public void DisplayEndingMessage()
    {
        int elapsed = (int)Math.Round((DateTime.Now - _startTime).TotalSeconds);
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine($"You have completed {elapsed} seconds of the {_name}.");
        ShowSpinner(3);
    }

    public void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            foreach (string frame in new[] { "|", "/", "-", "\\" })
            {
                Console.Write(frame);
                Thread.Sleep(250);
                Console.Write("\b \b");
            }
        }
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            int digits = i.ToString().Length;

            Console.Write(i);
            Thread.Sleep(1000);

            Console.Write(new string('\b', digits));
            Console.Write(new string(' ', digits));
            Console.Write(new string('\b', digits));
        }
    }

    private Random _random = new Random();

    protected string GetRandomItem(List<string> items)
    {
        return items[_random.Next(items.Count)];
    }

    public abstract void Run();
}