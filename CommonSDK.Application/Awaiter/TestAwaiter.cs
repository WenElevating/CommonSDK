using System.Runtime.CompilerServices;

namespace CommonSDK.Application.Awaiter;

public class TestAwaiter : INotifyCompletion
{
    private readonly TestOperation _operation;
    
    // ReSharper disable once MemberCanBePrivate.Global
    public bool IsCompleted => _operation.IsCompleted;
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public TestAwaiter(TestOperation operation)
    {
        _operation = operation;
    }

    public void OnCompleted(Action continuation)
    {
        if (IsCompleted)
        {
            continuation?.Invoke();
        }
        else
        {
            _operation.Continuation = continuation;
        }
    }

    public void GetResult()
    {
        _operation.CurrentTask.GetAwaiter().GetResult();
    }
}