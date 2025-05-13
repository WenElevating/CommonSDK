using System.Reflection;
using CommonSDK.Interface;
using CommonSDK.Model;

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