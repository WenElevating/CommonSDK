namespace CommonSDK.Interface;

public abstract class BaseEventType
{
    public string Id { get; protected set; }
    
    public string Name { get; set; }

    protected BaseEventType(string name)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
    }
}