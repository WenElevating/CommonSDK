using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using CommonSDK.AI.ChatClient;
using CommonSDK.AI.Ollama;
using CommonSDK.Console.Test;
using CommonSDK.Util;
using Microsoft.Win32;
using TestSDK.Test;

namespace TestSDK;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello, World!");
        // EventBusStressTester eventBusStressTester = new(1, 10000);
        // eventBusStressTester.RepeatRun();
        // eventBusStressTester.Run();
        // eventBusStressTester.UnsubscribeNull();
        // HttpListener listener = new HttpListener();
        // listener.Prefixes.Add("http://localhost:8080/");
        // listener.Prefixes.Add("http://localhost:8080/user/");
        // listener.Start();
        //
        // var context = listener.GetContext();
        // var request = context.Request;
        // Console.WriteLine(request.Url.AbsolutePath);
        // var response = context.Response;
        // // Construct a response.
        // string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
        // byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        // // Get a response stream and write the response to it.
        // response.ContentLength64 = buffer.Length;
        // System.IO.Stream output = response.OutputStream;
        // output.Write(buffer,0,buffer.Length);
        // // You must close the output stream.
        // output.Close();
        // listener.Stop();
        // var attributeList= ReflectionHelper.GetAttributeList("Get");
        // for (var i = 0; i < attributeList.Count; i++)
        // {
        //     var className = attributeList[i].classType.Name;
        //     var getData = (Get)attributeList[i].attribute;
        //     Console.WriteLine(className);
        //     Console.WriteLine(getData.Prefix);
        // }

        // HttpClient client = new HttpClient();
        // HttpRequestMessage requestMessage = new HttpRequestMessage();
        // requestMessage.Method = HttpMethod.Post;
        // requestMessage.RequestUri = new Uri("http://192.168.4.94:8000/api/chat");
        // requestMessage.Content = new StringContent("{\n  \"model\": \"llama3.2\",\n  \"messages\": [\n    {\n      \"role\": \"user\",\n      \"content\": \"why is the sky blue?\"\n    }\n  ]\n}", Encoding.UTF8,
        //     "application/json");
        // var httpResponseMessage = client.Send(requestMessage);
        // Console.WriteLine(httpResponseMessage.Content.ReadAsStringAsync().Result);

        //OllamaService service = new OllamaService("http://192.168.4.94:8000", "llama3.2");
        //var response = service.ChatAsync("为什么人需要氧气？");
        //response.Wait();
        //Console.WriteLine(response.Result);

        //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        //{
        //    string? displayName;
        //    string? installLocation;

        //    using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
        //    if (key == null)
        //    {
        //        return;
        //    }

        //    foreach (String keyName in key.GetSubKeyNames())
        //    {
        //        RegistryKey? subkey = key.OpenSubKey(keyName);
        //        displayName = subkey?.GetValue("DisplayName") as string;
        //        installLocation = subkey?.GetValue("InstallLocation") as string;
        //        if (string.IsNullOrEmpty(displayName))
        //            continue;

        //        if (displayName.ToLower().Contains("ollama"))
        //        {
        //            string result = SystemUtil.GetFileIfExist(Path.Combine(installLocation), "ollama.exe");
        //            if (!string.IsNullOrEmpty(result))
        //            {
        //                Console.WriteLine("已找到文件,路径为：" + result);
        //            }
        //            else
        //            {
        //                Console.WriteLine("文件不存在");
        //            }
        //        }

        //        Console.WriteLine(displayName.ToLower() + "  " + installLocation?.ToLower());
        //    }
        //}

        // Task.Run(async () =>
        // {
        //     var endpoint = "http://localhost:8000/";
        //     var modelId = "llama3.2";
        //
        //     IChatClient client = new OllamaChatClient(endpoint, modelId: modelId);
        //
        //     List<ChatMessage> conversation =
        //     [
        //         new(ChatRole.System, "You are a helpful AI assistant"),
        //         new(ChatRole.User, "What is AI?"),
        //         new(ChatRole.Assistant, "AI, or Artificial Intelligence, refers to the simulation of human intelligence in machines that are programmed to think like humans and mimic their actions. The term may also be applied to any machine that exhibits traits associated with a human mind such as learning and problem-solving.\r\n\r\nThere are several types of AI, including:\r\n\r\n1. **Narrow or Weak AI**: This type of AI is designed to perform specifically defined tasks, such as facial recognition or language translation, at a level that surpasses human capabilities.\r\n2. **General or Strong AI**: This type of AI would possess the ability to understand, learn, and apply its intelligence broadly across many tasks, much like human beings.\r\n3. **Superintelligence**: A hypothetical AI system significantly more intelligent than the best human minds, potentially capable of solving complex problems that are unsolvable by humans.\r\n\r\nAI can be categorized into two broad types:\r\n\r\n1. **Machine Learning (ML)**: This approach involves training algorithms to learn from data, allowing them to make predictions or decisions without being explicitly programmed.\r\n2. **Deep Learning (DL)**: A subset of machine learning, deep learning uses neural networks with multiple layers to analyze and interpret data.\r\n\r\nAI has numerous applications in areas such as:\r\n\r\n1. **Virtual Assistants**: Siri, Alexa, Google Assistant\r\n2. **Image and Speech Recognition**\r\n3. **Autonomous Vehicles**\r\n4. **Healthcare**: Predictive analytics, disease diagnosis\r\n5. **Cybersecurity**: Threat detection, incident response\r\n\r\nHowever, AI also raises several concerns and challenges, such as:\r\n\r\n1. **Bias and Fairness**: AI systems can perpetuate existing biases if trained on biased data.\r\n2. **Job Displacement**: Automation could lead to job losses in sectors where tasks are repetitive or easily automated.\r\n3. **Data Privacy**: The increasing reliance on data-driven decision-making raises concerns about data protection.\r\n\r\nOverall, AI is a rapidly evolving field with the potential to transform numerous aspects of our lives and society as a whole."),
        //         new(ChatRole.User, "Will AI affect the living space of humans?")
        //     ];
        //
        //     Console.WriteLine(await client.GetResponseAsync(conversation));
        // });

        //var task = Task.Run(async () =>
        //{
        //    Console.WriteLine("do some work!");
        //    await Task.Delay(3000);
        //});

        //var taskAwaiter = task.GetAwaiter();
        //taskAwaiter.OnCompleted(() =>
        //{
        //    Console.WriteLine("after do some work!");
        //});

        //taskAwaiter.GetResult();


        //using HttpClient client = new();
        //using HttpRequestMessage requestMessage = new();
        //requestMessage.Method = HttpMethod.Post;
        //requestMessage.RequestUri = new("http://192.168.4.94:8000/api/chat");
        //requestMessage.Content = new StringContent("{\r\n  \"model\": \"llama3.2\",\r\n  \"messages\": [\r\n    {\r\n      \"role\": \"user\",\r\n      \"content\": \"why is the sky blue?\"\r\n    }\r\n  ]\r\n}", Encoding.UTF8, "application/json");

        //HttpResponseMessage httpResponseMessage = client.Send(requestMessage);

        //string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
        //Console.WriteLine(result);

        //IChatClient client = new OllamaChatClient("http://localhost:8000", "llama3.2");

        //CancellationTokenSource tokenSource = new();

        //Task<ChatResponse> response = client.ChatAsync("为什么人需要氧气？", tokenSource.Token);

        //Task.Run(() =>
        //{
        //    Thread.Sleep(3000);
        //    tokenSource.Cancel();
        //});

        //response.Wait();

        //Console.WriteLine(response.Result.Data.Message.Content);

        YieldTest test = new YieldTest();
        test.Run();


        Console.ReadKey();
    }
}