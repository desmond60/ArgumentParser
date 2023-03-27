using ArgumentParser;

ArgParser parser = new ArgParser();

// Позиционный аргумент
parser.Add("file")
      .SetDescription("file name")
      .SetDefault(false);

// Опциональный флаговый аргумент
parser.Add("--test", "-t")
      .SetDescription("optional argument!")
      .SetImplicit(true) // Это флаг
      .SetValueImplicit(false); // Если нет аргумента то false

// Опциональный аргумент с значением
parser.Add("--var", "-v")
      .SetDescription("optional argument!")
      .SetDefault(false);

try {
    parser.Parse(args);
}
catch (ParsingException ex) {
    Console.WriteLine(ex.Message);
    return;
}

if (parser["-t"])
    Console.WriteLine("опачки");

if (parser.IsContain("file")) {
    string pos = parser.GetArgument("file", "string");
    Console.WriteLine(pos);
}

if (parser.IsContain("-v")) {
    string opt = parser.GetArgument("-va");
    Console.WriteLine(opt);
}