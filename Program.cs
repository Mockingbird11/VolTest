using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;

namespace VOLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IMemoryCache memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
            memoryCache.Set("Xux", "徐熊");

            memoryCache.TryGetValue("Xux", out var value);
            Console.WriteLine(value);
        }
    }
}
