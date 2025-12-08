using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void AddGoal(Goal g)
    {
        _goals.Add(g);
    }

    public void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals defined yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void RecordEvent(int index)
    {
        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("Invalid goal index.");
            return;
        }

        int awarded = _goals[index].RecordEvent();
        _score += awarded;
    }

    public int GetScore() => _score;

    public void SaveToFile(string filePath)
    {
        List<string> lines = new List<string>();
        lines.Add(_score.ToString());
        foreach (var g in _goals)
        {
            lines.Add(g.GetSaveString());
        }
        File.WriteAllLines(filePath, lines);
        Console.WriteLine($"Saved {_goals.Count} goals and score {_score} to {filePath}");
    }

    public void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length == 0)
        {
            Console.WriteLine("File is empty.");
            return;
        }

        _goals.Clear();
        
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
           
            string[] tokens = SplitPreserveEscapes(line);
            if (tokens.Length == 0) continue;
            string type = tokens[0];
            switch (type)
            {
                case "SimpleGoal":
                    _goals.Add(SimpleGoal.LoadFromTokens(tokens));
                    break;
                case "EternalGoal":
                    _goals.Add(EternalGoal.LoadFromTokens(tokens));
                    break;
                case "ChecklistGoal":
                    _goals.Add(ChecklistGoal.LoadFromTokens(tokens));
                    break;
                default:
                    Console.WriteLine($"Unknown goal type '{type}' in file. Skipping line.");
                    break;
            }
        }

        Console.WriteLine($"Loaded {_goals.Count} goals and score {_score} from {filePath}");
    }

    // Very small parser that splits on '|' but respects escaped '\|'
    private static string[] SplitPreserveEscapes(string line)
    {
        List<string> tokens = new List<string>();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        bool escape = false;
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '\\' && !escape)
            {
               
                escape = true;
                continue;
            }
            if (c == '|' && !escape)
            {
                tokens.Add(sb.ToString());
                sb.Clear();
            }
            else
            {
                sb.Append(c);
            }
            escape = false;
        }
        tokens.Add(sb.ToString());
        
        for (int i = 0; i < tokens.Count; i++) tokens[i] = tokens[i].Trim();
        return tokens.ToArray();
    }
}
