using System.Globalization;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace ArgumentParser;

// % ***** ArgParser class ***** % //
public class ArgParser
{
    //: Fields & Properties
    private List<Positional> Positionals { get; set; }
    private List<Optional>   Optionals   { get; set; }
    private List<Optional>   Flags => Optionals.Where(n => n.IsImplicit).ToList();

    public int NumRequiredArg => Positionals.Where(n => n.IsDefault).Count() + 
                                 Optionals.Where(n => n.IsDefault).Count();

    //: Constructor
    public ArgParser() {
        Positionals = new List<Positional>();
        Optionals   = new List<Optional>();
    }

    //: Indexer for flags
    public bool this[string name] {
        get {
            for (int i = 0; i < Flags.Count; i++)
                if (name.Equals(Flags[i].Name) || 
                    name.Equals(Flags[i].ShortName))
                    return Flags[i].ValueImplicit;
            return false;
        }
    }

    //: Adding argument
    public IArgument Add(string name) {

        // Adding positional argument
        if (Regex.IsMatch(name, @"^[a-zA-z]")) {
            Positionals.Add(new Positional(name));
            return Positionals[^1];
        }

        // Adding optional argument
        if (name.StartsWith("--") || name.StartsWith("-")) {
            Optionals.Add(new Optional(name));
            return Optionals[^1];
        }
   
        throw new InvalidDataException("""Invalid argument name! Example: ("-f", "--file", "square")""");
    }

    public IArgument Add(string long_name, string short_name) {
        // Adding optional argument
        if (long_name.StartsWith("--") && short_name.StartsWith("-")) {
            Optionals.Add(new Optional(long_name, short_name));
            return Optionals[^1];
        }
        throw new InvalidDataException("""Invalid argument name! Example: ("--file", "-f")""");
    }

    //: Parsing arguments
    public void Parse(string[] args) {

        // Create Enumerator Argument`s
        StringCollection argCol = new StringCollection(); 
        argCol.AddRange(args);
        StringEnumerator strEnum = argCol.GetEnumerator();

        // Variable
        int IdPositionalArg = 0;
        int CountRequiredArg = 0;

        while(strEnum.MoveNext()) {
            
            // positional argument
            if (!strEnum.Current!.StartsWith('-') ||
                int.TryParse(strEnum.Current!, out _)) {
                if (IdPositionalArg == Positionals.Count) throw new ParsingException($"Invalid number of positional arguments! Must be: {Positionals.Count}");
                if (Positionals[IdPositionalArg].IsDefault) CountRequiredArg++;
                Positionals[IdPositionalArg++].Value = strEnum.Current;
                continue;
            }

            // optional argument long_name
            if (strEnum.Current!.StartsWith("--")) {
                for (int i = 0; i < Optionals.Count; i++) {
                    if (Optionals[i].Name.Equals(strEnum.Current)) {
                        if (Optionals[i].IsImplicit) {
                            Optionals[i].ValueImplicit = true;
                            break;
                        } else {
                            strEnum.MoveNext();
                            Optionals[i].Value = strEnum.Current;
                            break;
                        }
                    }

                    if (i == Optionals.Count - 1)
                        throw new ParsingException($"\"{strEnum.Current}\" - invalid argument!");
                }
                continue;
            }

            // optional argument short_name
            if (strEnum.Current!.StartsWith("-")) {
                for (int i = 0; i < Optionals.Count; i++) {
                    if (Optionals[i].ShortName.Equals(strEnum.Current)) {
                        if (Optionals[i].IsImplicit) {
                            Optionals[i].ValueImplicit = true;
                            break;
                        } else {
                            strEnum.MoveNext();
                            Optionals[i].Value = strEnum.Current;
                            break;
                        }
                    }

                    if (i == Optionals.Count - 1)
                        throw new ParsingException($"\"{strEnum.Current}\" - invalid argument!");
                }
                continue;
            }
        }

        if (CountRequiredArg != NumRequiredArg) throw new ParsingException($"Invalid number of required arguments! Must be: {NumRequiredArg}");
    }

    //: Returning the value of an argument (By Name)
    public dynamic GetArgument(string name, string type = "string") {

        //: Getting positional arguments
        foreach (var item in Positionals)
            if (name.Equals(item.Name))
                return GetValueByType(item, type);
        
        //: Getting optional argument
        foreach (var item in Optionals)
            if (name.Equals(item.Name) || name.Equals(item.ShortName))
                return GetValueByType(item, type);

        throw new InvalidDataException($"");
    }

    //: Convert the value to the specified type
    private object GetValueByType(IArgument item, string type) {
        return type switch {
            "bool"    => Boolean.Parse(item.Value.ToString()!),
            "sbyte"   => SByte.Parse(item.Value.ToString()!),
            "byte"    => Byte.Parse(item.Value.ToString()!),
            "short"   => Int16.Parse(item.Value.ToString()!),
            "ushort"  => UInt16.Parse(item.Value.ToString()!),
            "int"     => Int32.Parse(item.Value.ToString()!),
            "uint"    => UInt32.Parse(item.Value.ToString()!),
            "long"    => Int64.Parse(item.Value.ToString()!),
            "ulong"   => UInt64.Parse(item.Value.ToString()!),
            "int128"  => Int128.Parse(item.Value.ToString()!),
            "uint128" => UInt128.Parse(item.Value.ToString()!),
            "float"   => Single.Parse(item.Value.ToString()!),
            "double"  => Double.Parse(item.Value.ToString()!),
            "decimal" => Decimal.Parse(item.Value.ToString()!),
            "char"    => Char.Parse(item.Value.ToString()!),
            "string"  => item.Value.ToString()!,
            _         => item.Value.ToString()!
        };
    }
}
