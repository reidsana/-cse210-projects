using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

/*
  Mindfulness Program - Program.cs
  Week 5 Project (Full implementation)

  Exceeds requirements (documented here):
  - Session logging: each completed activity appends a timestamped record to "mindfulness_log.txt".
  - Prompts/questions aren't repeated within a single activity session until all have been used.
  - ReadLine with timeout for ListingActivity to avoid blocking after time expires.
  - Clean animations: spinner and countdown used consistently.
  - Encapsulation: base Activity holds shared behavior; derived activities override Run().
*/

namespace MindfulnessProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Mindfulness Program - Week 5 Project";
            var app = new MindfulnessApp();
            app.Run();
        }
    }

    public class MindfulnessApp
    {
        private bool _running = true;

        public void Run()
        {
            while (_running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Mindfulness Program\n");
                Console.WriteLine("Please select an activity:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice (1-4): ");

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        RunActivity(new BreathingActivity());
                        break;
                    case "2":
                        RunActivity(new ReflectionActivity());
                        break;
                    case "3":
                        RunActivity(new ListingActivity());
                        break;
                    case "4":
                        _running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }

            Console.WriteLine("Thank you for using the Mindfulness Program. Take care!");
        }

        private void RunActivity(Activity activity)
        {
            Console.Clear();
            activity.Start();
            activity.Run();
            activity.Finish();
            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }
    }

    // Base class for activities
    public abstract class Activity
    {
        private string _name;
        private string _description;
        private int _durationSeconds;
        private static readonly string LogFilePath = "mindfulness_log.txt";
        private static readonly Random _rand = new Random();

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
            _durationSeconds = 0;
        }

        // Accessors for derived classes
        protected int DurationSeconds => _durationSeconds;
        protected string Name => _name;
        protected string Description => _description;

        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"=== {Name} ===\n");
            Console.WriteLine(Description + "\n");

            // Prompt for duration
            int seconds = PromptForDuration();
            _durationSeconds = seconds;

            Console.WriteLine("\nPrepare to begin...");
            PauseWithSpinner(3);
            Console.WriteLine();
        }

        public abstract void Run();

        public void Finish()
        {
            Console.WriteLine();
            Console.WriteLine("Good job! You've completed the activity.");
            Console.WriteLine($"Activity: {Name}");
            Console.WriteLine($"Duration: {DurationSeconds} seconds");
            PauseWithSpinner(3);

            // Log the session (exceeds base requirements)
            try
            {
                LogSession();
            }
            catch
            {
                // Do not crash if logging fails
            }
        }

        private int PromptForDuration()
        {
            while (true)
            {
                Console.Write("Enter duration in seconds (e.g., 30): ");
                var input = Console.ReadLine()?.Trim();
                if (int.TryParse(input, out int seconds) && seconds > 0)
                {
                    return seconds;
                }
                Console.WriteLine("Please enter a positive integer value for seconds.");
            }
        }

        // Small spinner animation for given seconds
        protected void PauseWithSpinner(int seconds)
        {
            var spinner = new[] { '|', '/', '-', '\\' };
            var sw = Stopwatch.StartNew();
            int idx = 0;
            while (sw.Elapsed.TotalSeconds < seconds)
            {
                Console.Write(spinner[idx % spinner.Length]);
                Thread.Sleep(250);
                Console.Write('\r');
                idx++;
            }
            // Clear spinner character
            Console.Write(' ');
            Console.Write('\r');
        }

        // Countdown display (whole seconds) - shows numbers from n down to 1
        protected void Countdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        // Write a small log entry to a file
        private void LogSession()
        {
            var line = $"{DateTime.UtcNow:o}\t{Name}\t{DurationSeconds}s{Environment.NewLine}";
            File.AppendAllText(LogFilePath, line);
        }

        // Utility: get a shuffled sequence without repeating until all used
        protected IEnumerable<T> GetShuffledSequence<T>(IEnumerable<T> items)
        {
            return items.OrderBy(x => _rand.Next()).ToList();
        }
    }

    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        public override void Run()
        {
            int totalSeconds = DurationSeconds;
            var sw = Stopwatch.StartNew();

            // Set breath durations (seconds). Reasonable defaults.
            int inhale = 4;
            int exhale = 6;

            if (totalSeconds < 10)
            {
                inhale = Math.Max(1, totalSeconds / 4);
                exhale = Math.Max(1, totalSeconds / 4);
            }

            bool inhaleTurn = true;
            while (sw.Elapsed.TotalSeconds < totalSeconds)
            {
                int remaining = Math.Max(0, totalSeconds - (int)sw.Elapsed.TotalSeconds);
                if (remaining <= 0) break;

                if (inhaleTurn)
                {
                    Console.WriteLine("Breathe in...");
                    int wait = Math.Min(inhale, remaining);
                    Countdown(wait);
                }
                else
                {
                    Console.WriteLine("Breathe out...");
                    int wait = Math.Min(exhale, remaining);
                    Countdown(wait);
                }

                inhaleTurn = !inhaleTurn;
            }
        }
    }

    public class ReflectionActivity : Activity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity() : base("Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
        }

        public override void Run()
        {
            var promptsShuffled = GetShuffledSequence(_prompts).ToList();
            // Pick first prompt and remove it from list of available prompts for this session
            string chosenPrompt = promptsShuffled.First();
            Console.WriteLine(chosenPrompt + "\n");
            Console.WriteLine("You will be given a series of questions to reflect on. Take your time with each one.");
            PauseWithSpinner(2);

            var questionsPool = new Queue<string>(GetShuffledSequence(_questions));
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < DurationSeconds)
            {
                if (questionsPool.Count == 0)
                {
                    // Reshuffle once we've exhausted all questions to avoid immediate repeats
                    questionsPool = new Queue<string>(GetShuffledSequence(_questions));
                }

                var q = questionsPool.Dequeue();
                Console.WriteLine("\n" + q);

                // Give user time to reflect - spinner for up to 10 seconds or remaining time
                int wait = (int)Math.Min(10, DurationSeconds - sw.Elapsed.TotalSeconds);
                if (wait <= 0) break;
                PauseWithSpinner(wait);
            }
        }
    }

    public class ListingActivity : Activity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base("Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        public override void Run()
        {
            var promptsShuffled = GetShuffledSequence(_prompts).ToList();
            var chosenPrompt = promptsShuffled.First();

            Console.WriteLine(chosenPrompt + "\n");
            Console.WriteLine("You will have a few seconds to think, then start listing items. Press Enter after each item.");
            Console.WriteLine("Get ready...");
            Countdown(5);

            var sw = Stopwatch.StartNew();
            var entries = new List<string>();

           
            while (sw.Elapsed.TotalSeconds < DurationSeconds)
            {
                int remaining = Math.Max(0, DurationSeconds - (int)sw.Elapsed.TotalSeconds);
                if (remaining <= 0) break;

                Console.Write($"[{remaining}s left] - Item: ");
                string input = ReadLineWithTimeout(remaining);
                if (input == null)
                {
                    // Time ran out
                    break;
                }
                input = input.Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    entries.Add(input);
                }
            }

            Console.WriteLine($"\nYou listed {entries.Count} item(s):");
            foreach (var e in entries)
            {
                Console.WriteLine("- " + e);
            }
        }

        
        private string ReadLineWithTimeout(int secondsTimeout)
        {
            if (secondsTimeout <= 0) return null;
            var sb = new StringBuilder();
            var sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < secondsTimeout)
            {
                while (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        return sb.ToString();
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Length--;
                           
                            Console.Write("\b \b");
                        }
                    }
                    else
                    {
                        sb.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                }
                Thread.Sleep(50);
            }
            Console.WriteLine();
            return null;
        }
    }
}
