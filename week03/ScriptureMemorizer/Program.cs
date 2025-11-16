using System;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("John", 3, 16);
        Scripture scripture = new Scripture(reference, "For God so loved the world that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");

        while (true)
        {
            Console.Clear();
            scripture.DisplayScripture();

            if (scripture.AllWordsHidden())
                break;

            Console.WriteLine("Press Enter to hide some words, or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3); 
        }

        Console.WriteLine("All words are hidden. Program finished.");
    }
}
