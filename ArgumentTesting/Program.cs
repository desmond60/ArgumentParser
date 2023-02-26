using ArgumentParser;

ArgParser parser = new ArgParser();

// Позиционный аргумент
parser.Add("file")
      .SetDescription("file name")
      .SetDefault(true);

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

string pos = parser.GetArgument("file");
Console.WriteLine(pos);

string opt = parser.GetArgument("-v");
Console.WriteLine(opt);

Сделать исключение при обращение к аргументу который не задан
Справку сделать
