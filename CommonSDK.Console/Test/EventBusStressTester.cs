using System.Diagnostics;

namespace TestSDK.Test;

public class EventBusStressTester
{
    //private readonly int _threadCount;
    //private readonly int _publishPerThread;
    
    //public EventBusStressTester(int threadCount, int publishPerThread)
    //{
    //    _threadCount = threadCount;
    //    _publishPerThread = publishPerThread;
    //}

    //public void RepeatRun()
    //{
    //    Console.WriteLine($"RepeatRun: 重复订阅、取消订阅测试");
    //    var handler = new NotifyMessageEventHandler();
    //    var customNotifyEventHandler = new CustomNotifyEventHandler();
    //    // 一组
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(customNotifyEventHandler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(customNotifyEventHandler);
        
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(customNotifyEventHandler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(customNotifyEventHandler);
        
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(customNotifyEventHandler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(customNotifyEventHandler);
        
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(customNotifyEventHandler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(customNotifyEventHandler);
        
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(customNotifyEventHandler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(handler);
    //    //EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(customNotifyEventHandler);
        
    //    Console.WriteLine("最终订阅结果：" + EventBus<string>.Instance.GetSubscriberCount<NotifyMessageEvent>());
    //}

    //public void UnsubscribeNull()
    //{
    //    var handler = new NotifyMessageEventHandler();
    //    var customNotifyEventHandler = new CustomNotifyEventHandler();

    //    var res1 = EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(handler);
    //    var res2 = EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(customNotifyEventHandler);
    //    Console.WriteLine($"取消注册结果1：{res1}，结果2：{res2}");
    //}

    //public void Run()
    //{
    //    var tasks = new List<Task>();
    //    var watch = new Stopwatch();
    //    watch.Start();
    //    for (int i = 0; i < _threadCount; i++)
    //    {
    //        tasks.Add(Task.Run(() => 
    //        {
    //            var handler = new NotifyMessageEventHandler();
    //            var customNotifyEventHandler = new CustomNotifyEventHandler();
    //            EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(handler);
    //            EventBus<string>.Instance.Subscribe<NotifyMessageEvent>(customNotifyEventHandler);

    //            for (int j = 0; j < _publishPerThread; j++)
    //            {
    //                try
    //                {
    //                    EventBus<string>.Instance.Publish<NotifyMessageEvent>(
    //                        null
    //                    );
    //                }
    //                catch (Exception ex)
    //                {
    //                    Console.WriteLine($"Publish异常: {ex.Message}");
    //                }
    //            }

    //            EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(handler);
    //            EventBus<string>.Instance.Unsubscribe<NotifyMessageEvent>(customNotifyEventHandler);
    //        }));
    //    }

    //    Task.WaitAll(tasks.ToArray());

    //    Console.WriteLine("并发测试完成！");
    //    Console.WriteLine("发布耗时：" + watch.ElapsedMilliseconds + "ms");
    //}
}
