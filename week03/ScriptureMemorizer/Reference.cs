public class Reference
{
    private string _book;
    private int _startVerse;
    private int _endVerse;

    
    public Reference(string book, int verse)
    {
        _book = book;
        _startVerse = verse;
        _endVerse = verse;
    }

        public Reference(string book, int startVerse, int endVerse)
    {
        _book = book;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public string GetReference()
    {
        if (_startVerse == _endVerse)
            return $"{_book} {_startVerse}";
        else
            return $"{_book} {_startVerse}-{_endVerse}";
    }
}
