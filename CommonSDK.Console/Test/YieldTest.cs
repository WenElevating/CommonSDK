using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSDK.Console.Test
{
    internal class YieldTest
    {
        public async void Run()
        {
            await foreach (int n in GenerateNumbersAsync(5))
            {
                System.Console.Write(n);
                System.Console.Write(" ");
            }
            // Output: 0 2 4 6 8
        }

        async IAsyncEnumerable<int> GenerateNumbersAsync(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return await ProduceNumberAsync(i);
            }
        }

        async Task<int> ProduceNumberAsync(int seed)
        {
            await Task.Delay(1000);
            return 2 * seed;
        }
    }
}
