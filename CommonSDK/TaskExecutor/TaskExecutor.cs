namespace CommonSDK.TaskExecutor;

public class TaskExecutor : ITaskExecutor
{
    private readonly object _lock = new object();
    
    public bool Execute(CustomTask task)
    {
        ArgumentNullException.ThrowIfNull(task, nameof(task));
        lock (_lock)
        {
            task.executeTime = DateTime.Now;
            bool isFailed = false;
        
            // 执行任务
            if (!Work(task))
            {
                // 检查结果，失败重试
                isFailed = Retry(task);
            }
        
            if (isFailed)
            {
                Console.Write("execute task failed!");
                task.OnFailed();
                return false;
            }

            // 成功调用回调
            Console.Write("execute task succeeded!");
            task.OnCompleted();
        }
        return true;
    }

    public bool Retry(CustomTask task)
    {
        int count = task.RetryCount;
        for (int i = 0; i < count; i++)
        {
            try
            {
                Console.Write($"The {i} time retry task!");
                if (Work(task))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        Console.Write("retry task failed!");
        return false;
    }

    private bool Work(CustomTask task)
    {
        ArgumentNullException.ThrowIfNull(task, nameof(task));
        if (task?.Content == null || string.IsNullOrWhiteSpace(task.Content))
        {
            throw new ArgumentNullException(nameof(task.Content));
        }
        
        try
        {
            Console.Write("Execute task: " + task.Content);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
}