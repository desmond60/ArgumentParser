using System.Globalization;
using System.Text;
using System.Collections.Specialized;

namespace ArgumentParser;

// % ***** ArgParser class ***** % //
public class ArgParser
{
    //: Fields & Properties
    private List<Positional> positionals { get; set; }

    public int NumRequiredArg => positionals.Where(n => n.IsDefault).Count();

    //: Constructor
    public ArgParser() {
        positionals = new List<Positional>();
    }

    //: Adding argument
    public IArgument Add(string name) {

        // Adding positional argument
        if (!name.StartsWith('-')) {
            positionals.Add(new Positional(name));
            return positionals[^1];
        }
        return new Positional(name);
    }

    // public IArgument Add(string short_name, string long_name) {

    // }

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
            
            if (!strEnum.Current!.StartsWith('-') ||
                int.TryParse(strEnum.Current!, out _)) {
                if (IdPositionalArg == positionals.Count) throw new ParsingException($"Invalid number of positional arguments! Must be: {positionals.Count}");
                if (positionals[IdPositionalArg].IsDefault) CountRequiredArg++;
                positionals[IdPositionalArg++].Value = strEnum.Current;
                continue;
            }
        }

        if (CountRequiredArg != NumRequiredArg) throw new ParsingException($"Invalid number of required arguments! Must be: {NumRequiredArg}");
    }

    //: Returning the value of an argument (By Name)
    public dynamic GetArgument(string name, string type = "string") {

        //: Getting positional arguments
        foreach (var item in positionals)
            if (name.Equals(item.Name))
                return GetValueByType(item, type);

        return 0;
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
