namespace CommonSDK.HttpServer;

public class TestController
{
    [Get("/data")]
    public string GetData()
    {
        return "Test";
    }
}