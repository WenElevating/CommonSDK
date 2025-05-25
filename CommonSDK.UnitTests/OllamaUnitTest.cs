using CommonSDK.AI.Ollama;

namespace CommonSDK.UnitTests;

public class OllamaUnitTest
{
    private OllamaService service;

    /// <summary>  
    /// 执行初始化工作  
    /// </summary>  
    [SetUp]
    public void Setup()
    {
        // service = new OllamaService("http://192.168.4.94:8000", "llama3.2");
    }

    [Test]
    public void Test1()
    {
        Task ts = Task.Run(async () =>
        {
            await service.ChatStreamAsync("给我推荐几本程序员必看的书籍", s =>
            {
                Console.Write(s);
            });
        });

        ts.Wait();
    }

    /// <summary>  
    /// 执行清理工作  
    /// </summary>  
    [TearDown]
    public async Task TearDown()
    {
        if (service != null)
        {
            await service.DisposeAsync();
        }
    }
}
