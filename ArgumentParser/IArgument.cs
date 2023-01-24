namespace ArgumentParser;

// % ***** CMD Argument interface ***** % //
public interface IArgument
{
    public string Name          { get; set; }
    public string Description   { get; set; }
    public string Value         { get; set; }
    public bool   IsDefault     { get; set; }
    public bool   IsImplicit    { get; set; }
    public bool   ValueImplicit { get; set; }

    public IArgument SetDescription(string _text) {
        this.Description = _text;
        return this;
    }

    public IArgument SetDefault(bool _default) {
        this.IsDefault = _default;
        return this;
    }

    public IArgument SetImplicit(bool _cond) {
        this.IsImplicit = _cond;
        return this;
    }

    public IArgument SetValueImplicit(bool _val) {
        this.ValueImplicit = _val;
        return this;
    }
}