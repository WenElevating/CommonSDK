using System.Collections.Concurrent;
using System.Diagnostics;
using CommonSDK.EventBus.Interface;
using CommonSDK.EventBus.Model;

namespace CommonSDK.EventBus.Service;

public class EventBus<T> : IEventBus<T>
{
    private static readonly Lazy<EventBus<T>> _instance = new(() => new EventBus<T>());
    public static EventBus<T> Instance => _instance.Value;

    
    /// <summary>
    /// 事件--->处理器集合
    /// </summary>
    private readonly ConcurrentDictionary<Type, List<IEventHandler<T>>> handlers = new();
    private readonly object handlerLocker = new object();

    private EventBus()
    {
        
    }

    /// <summary>
    /// 订阅
    /// </summary>
    /// <param name="handler"></param>
    /// <typeparam name="EventType"></typeparam>
    /// <typeparam name="BaseEventType"></typeparam>
    /// <returns></returns>
    public bool Subscribe<BaseEventType>(IEventHandler<T> handler)
    {
        ArgumentNullException.ThrowIfNull(handler, nameof(handler));
        try
        {
            lock (handlerLocker)
            {
                var eventType = typeof(BaseEventType);
                if (!handlers.ContainsKey(eventType))
                {
                    handlers.TryAdd(eventType, []);
                }
                handlers[eventType].Add(handler);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return false;
    }

    public bool SubscribeOneTime<BaseEventType>(IEventHandler<T> handler)
    {
        throw new NotImplementedException();
    }

    public bool Unsubscribe<BaseEventType>(IEventHandler<T> handler)
    {
        ArgumentNullException.ThrowIfNull(handler, nameof(handler));

        try
        {
            lock (handlerLocker)
            {
                var eventType = typeof(BaseEventType);
                if (!handlers.TryGetValue(eventType, out var handlerList))
                {
                    return false;
                }
                return handlerList.Remove(handler);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
        return false;
    }

    public bool Publish<BaseEventType>(BaseEventSource<T> source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        List<IEventHandler<T>> handlerList = new();
        lock (handlerLocker)
        {
            if (!handlers.TryGetValue(typeof(BaseEventType), out handlerList))
            {
                return false;
            }
        }
        
        var list = handlerList.ToList();
        foreach (var eventHandler in list)
        {
            try
            {
                Task.Run(() => eventHandler.Handle(source));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        return false;
    }

    public Task<bool> PublishAsync<BaseEventType>(BaseEventSource<T> source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return Task.FromResult(Publish<BaseEventType>(source));
    }

    public bool PublishOneTarget<BaseEventType>(BaseEventSource<T> source, Type handlerType)
    {
        throw new NotImplementedException();
    }

    public int GetSubscriberCount<BaseEventType>()
    {
        if (!handlers.ContainsKey(typeof(BaseEventType)))
        {
            return -1;
        }
        return handlers[typeof(BaseEventType)].Count;
    }
}