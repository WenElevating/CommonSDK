using CommonSDK.Service;
using TestSDK.Test;

namespace TestSDK;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        EventBusStressTester eventBusStressTester = new(1, 10000);
        eventBusStressTester.RepeatRun();
        Console.ReadKey();
    }
}