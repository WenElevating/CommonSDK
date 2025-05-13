namespace CommonSDK.HttpServer;

public interface IHttpServer
{
    /// <summary>
    /// 增加请求监听
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public bool Add(string prefix);
    
    /// <summary>
    /// 启动服务
    /// </summary>
    public void Run();
}