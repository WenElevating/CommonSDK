namespace CommonSDK.HttpServer;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class Get : Attribute
{
    private string prefix;

    public string Prefix
    {
        get { return prefix; }
        set { prefix = value; }
    }

    public Get(string prefix)
    {
        this.Prefix = prefix;
    }
}