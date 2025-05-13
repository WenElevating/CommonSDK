namespace CommonSDK.TaskExecutor;

public class CustomTask
{
    public string Id { get; set; }

    public DateTime executeTime { get; set; }
    
    public string Content { get; set; }
    
    public int RetryCount { get; set; }
    
    public Action OnCompleted;
    
    public Action OnFailed;
}