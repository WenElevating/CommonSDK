namespace CommonSDK.AI.Interface;

public interface IChatService<T> : IAsyncDisposable
{
    public Task<bool> RunAsync();

    public Task<bool> StopAsync();

    public Task<T> ChatAsync(string message);

    public Task ChatStreamAsync(string message, Action<string> streamCallback);
}