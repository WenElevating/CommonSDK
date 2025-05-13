using CommonSDK.EventBus.Model;

namespace CommonSDK.EventBus.Interface;

/// <summary>
/// 事件总线基础接口
/// </summary>
public interface IEventBus<T>
{
    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <param name="handler"></param>
    /// <typeparam name="EventType"></typeparam>
    /// <returns></returns>
    bool Subscribe<BaseEventType>(IEventHandler<T> handler);

    /// <summary>
    /// 单次订阅
    /// </summary>
    /// <param name="handler"></param>
    /// <typeparam name="EventType"></typeparam>
    /// <returns></returns>
    bool SubscribeOneTime<BaseEventType>(IEventHandler<T> handler);
    
    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="handler"></param>
    /// <typeparam name="EventType"></typeparam>
    /// <returns></returns>
    bool Unsubscribe<BaseEventType>(IEventHandler<T> handler);
    
    /// <summary>
    /// 发布通知
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="EventType"></typeparam>
    /// <returns></returns>
    bool Publish<BaseEventType>(BaseEventSource<T> source);
    
    /// <summary>
    /// 异步推送
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="BaseEventType"></typeparam>
    /// <returns></returns>
    Task<bool> PublishAsync<BaseEventType>(BaseEventSource<T> source);
    
    /// <summary>
    /// 通知单个处理器
    /// </summary>
    /// <param name="source"></param>
    /// <param name="handlerType"></param>
    /// <typeparam name="EventType"></typeparam>
    /// <returns></returns>
    bool PublishOneTarget<BaseEventType>(BaseEventSource<T> source, Type handlerType);
    
    int GetSubscriberCount<BaseEventType>();
}