namespace CommonSDK.TaskExecutor;
/// <summary>
/// 执行器，接收任务并执行具体的功能
/// </summary>
public interface ITaskExecutor
{
    /// <summary>
    /// 执行任务
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public bool Execute(CustomTask task);

    public bool Retry(CustomTask task);
}