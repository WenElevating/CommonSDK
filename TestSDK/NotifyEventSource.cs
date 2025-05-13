using CommonSDK.Model;

namespace TestSDK;

public class NotifyEventSource<T> : BaseEventSource<T>
{
    public NotifyEventSource(string name, T param) : base(name, param)
    {
        
    }
}