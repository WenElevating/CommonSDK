using System.Reflection;
using CommonSDK.EventBus.Interface;
using CommonSDK.EventBus.Model;

namespace TestSDK;

public class CustomNotifyEventHandler : BaseFileWriter, IEventHandler<string> 
{
    private int count;
    
    public bool Equals(IEventHandler<string>? other)
    {
        return GetHashCode() == other?.GetHashCode();
    }

    public void Handle(BaseEventSource<string> source)
    {
        Console.WriteLine(
            $"CustomNotifyEventHandler：{Interlocked.Increment(ref count)}");
    }
}