namespace ArgumentParser;

// % ***** Optional Argument class ***** % //
internal class Optional : IArgument
{
    //: Fields & Properties 
    public string Name          { get; set; }
    public string ShortName     { get; set; }
    public string Description   { get; set; }
    public string Value         { get; set; }
    public bool   IsDefault     { get; set; }
    public bool   IsImplicit    { get; set; }
    public bool   ValueImplicit { get; set; }

    //: Constructor`s
    public Optional(string _name) {
        this.Name        = _name;
        this.ShortName   = String.Empty;
        this.Description = String.Empty;
        this.IsDefault   = false;
        this.IsImplicit  = false;
    }

    public Optional(string _name, string _shortName) {
        this.Name        = _name;
        this.ShortName   = _shortName;
        this.Description = String.Empty;
        this.IsDefault   = false;
        this.IsImplicit  = false;
    }
}