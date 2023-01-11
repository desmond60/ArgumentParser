namespace ArgumentParser;

// % ***** ParsingException class ***** % //
public class ParsingException : Exception
{
    public ParsingException(string message)
                           : base(message) { }
}