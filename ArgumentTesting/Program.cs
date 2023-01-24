using ArgumentParser;

ArgParser parser = new ArgParser();

parser.Add("file")
      .SetDescription("file name")
      .SetDefault(false);

parser.Add("--test", "-t")
      .SetDescription("optional argument!")
      .SetImplicit(true)
      .SetValueImplicit(false);

try {
    parser.Parse(args);
}
catch (ParsingException ex) {
    Console.WriteLine(ex.Message);
    return;
}

if (parser["-t"])
    Console.WriteLine("опачки");

// string abs = parser.GetArgument("file");

// Console.WriteLine(abs);
