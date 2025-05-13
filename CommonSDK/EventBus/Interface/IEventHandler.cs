using CommonSDK.Model;

namespace CommonSDK.Interface;

public interface IEventHandler<T> : IEquatable<IEventHandler<T>>
{
    /// <summary>
    /// 提供处理接口
    /// </summary>
    /// <param name="source"></param>
    void Handle(BaseEventSource<T> source);
}