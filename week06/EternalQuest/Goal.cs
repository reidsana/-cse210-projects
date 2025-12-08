using System;

public abstract class Goal
{
    private string _title;
    private string _description;
    private int _points;

    public Goal(string title, string description, int points)
    {
        _title = title;
        _description = description;
        _points = points;
    }

    public string GetTitle() => _title;
    public string GetDescription() => _description;
    public int GetPoints() => _points;
    public void SetTitle(string title) => _title = title;
    public void SetDescription(string description) => _description = description;
    public void SetPoints(int points) => _points = points;

    // RecordEvent returns the number of points awarded when the user records this goal.
    // Abstract so each derived class must implement its own behavior.
    public abstract int RecordEvent();

    // Return a user-friendly details string (for listing goals)
    public virtual string GetDetailsString()
    {
        // Default implementation used by most goals
        return $"[{GetStatusString()}] {GetTitle()} ({GetDescription()})";
    }

    // For saving to a file: derived classes should extend this string with their own fields.
    public virtual string GetSaveString()
    {
        // Format: GoalType|Title|Description|Points|... (derived classes append more)
        return $"{this.GetType().Name}|{Escape(GetTitle())}|{Escape(GetDescription())}|{GetPoints()}";
    }

    // Helper default status (most goals not completed override if needed)
    protected virtual string GetStatusString()
    {
        // Default: not applicable -> show empty box
        return " ";
    }

    // Unescape/Escape for safe file format (simple)
    protected static string Escape(string s)
    {
        return s.Replace("|", "\\|");
    }

    protected static string Unescape(string s)
    {
        return s.Replace("\\|", "|");
    }

    // Static helper to parse a token that was escaped
    protected static string ParseToken(string token)
    {
        return Unescape(token);
    }
}
