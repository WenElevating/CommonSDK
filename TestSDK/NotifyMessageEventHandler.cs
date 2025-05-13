using CommonSDK.Interface;
using CommonSDK.Model;

namespace TestSDK;

public class NotifyMessageEventHandler : BaseFileWriter, IEventHandler<string>
{
    private int count;
    private readonly string id = Guid.NewGuid().ToString();

    public bool Equals(IEventHandler<string>? other)
    {
        return GetHashCode() == other?.GetHashCode();
    }

    public void Handle(BaseEventSource<string> source)
    {
        Console.WriteLine($"NotifyMessageEventHandler: {Interlocked.Increment(ref count)}");
    }
}