using ArgumentParser;

ArgParser parser = new ArgParser();

parser.Add("file")
      .SetDescription("file name")
      .SetDefault(true);

try {
    parser.Parse(args);
}
catch (ParsingException ex) {
    Console.WriteLine(ex.Message);
    return;
}

string abs = parser.GetArgument("file");

Console.WriteLine(abs);
