namespace CommonSDK.EventBus.Model;

public abstract class BaseFileWriter
{
    protected static StreamWriter streamWriter = new StreamWriter(new FileStream("TestEventBusStressTester.txt", FileMode.Create));

    public BaseFileWriter()
    {
        
    }
}