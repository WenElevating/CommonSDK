namespace CommonSDK.EventBus.Model;

public abstract class BaseEventSource<T>
{
    /// <summary>
    /// 确定事件的唯一标识（查找、日志等作用）
    /// </summary>
    public string Id { get; private set; }

    /// <summary>
    /// 事件的名字
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 事件的参数
    /// </summary>
    public T Param { get; set; }

    protected BaseEventSource(string name, T param)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Param = param;
    }
}