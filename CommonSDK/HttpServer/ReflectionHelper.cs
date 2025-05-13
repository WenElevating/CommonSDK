using System.Reflection;

namespace CommonSDK.HttpServer;

public struct AttributeInfo()
{
    public Type classType;
    
    public MethodInfo methodInfo;
    
    public object attribute;
}

public class ReflectionHelper
{
    public static List<AttributeInfo> GetAttributeList(string attributeName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(attributeName);
        
        List<AttributeInfo> attributesList = new List<AttributeInfo>();
        
        // 获取程序集信息
        var executingAssembly = Assembly.GetExecutingAssembly();
        
        // 获取程序集下的所有内容
        var types = executingAssembly.GetTypes();
        
        for (var i = 0; i < types.Length; i++)
        {
            // 只检查class
            if (!types[i].IsClass)
            {
                continue;
            }

            var methodInfos = types[i].GetMethods(BindingFlags.Instance | BindingFlags.Public);
            for (var j = 0; j < methodInfos.Length; j++)
            {
                var attributes = methodInfos[j].GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType().Name == attributeName)
                    {
                        Console.WriteLine(methodInfos[j].Name);
                        AttributeInfo info = new AttributeInfo();
                        info.classType = types[i];
                        info.methodInfo = methodInfos[j];
                        info.attribute = attribute;
                        attributesList.Add(info);
                    }
                }
            }
        }
        return attributesList;
    }
}