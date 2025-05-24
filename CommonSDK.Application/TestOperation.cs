namespace CommonSDK.Application;

public class TestOperation 
{
    public TestOperation(Action action)
    {
        CurrentTask = new Task(action);
        CurrentTask.Start();
    }
    public bool IsCompleted { get; set; }
    
    public Action Continuation { get; set; }
    
    public Task CurrentTask { get; set; }
}