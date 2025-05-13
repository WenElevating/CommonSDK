using CommonSDK.Interface;

namespace TestSDK;

/// <summary>
/// 通知消息
/// </summary>
public class NotifyMessageEvent : BaseEventType
{
    public NotifyMessageEvent(string name) : base(name)
    {
        
    }
}