public class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        StartTimer();
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now <= endTime)
        {
            Console.Write("\nBreathe in...");
            ShowCountDown(4);
            Console.Write("\nHold...");
            ShowCountDown(7);
            Console.Write("\nNow breathe out...");
            ShowCountDown(8);
            Console.WriteLine();
        }
        DisplayEndingMessage();
    }
}