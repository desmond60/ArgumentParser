namespace ArgumentParser;

// % ***** Positional Argument class ***** % //
internal class Positional : IArgument
{
    //: Fields & Properties 
    public string Name          { get; set; }
    public string Description   { get; set; }
    public string Value         { get; set; }
    public bool   IsDefault     { get; set; }
    public bool   IsImplicit    { get; set; }
    public bool   ValueImplicit { get; set; }

    //: Constructor
    public Positional(string _name) {
        this.Name        = _name;
        this.Description = String.Empty;
        this.IsDefault   = false;
    }
}