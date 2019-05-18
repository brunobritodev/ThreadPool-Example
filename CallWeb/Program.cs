using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CallWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                MainAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }

        static async Task MainAsync()
        {
            int times = Environment.ProcessorCount;
            var syncUrl = "http://localhost:5000/api/values/";
            var asyncUrl = "http://localhost:5000/api/values/13";

            Console.WriteLine($" Synchronous time for {times} connections: {await RunTest(syncUrl, times)}");

            times = Environment.ProcessorCount + 1;
            Console.WriteLine($" Synchronous time for {times} connections: {await RunTest(syncUrl, times)}");

            times = Environment.ProcessorCount;
            Console.WriteLine($"Asynchronous time for {times} connections: {await RunTest(asyncUrl, times)}");

            times = 200;
            Console.WriteLine($"Asynchronous time for {times} connections: {await RunTest(asyncUrl, times)}");
        }

        static async Task<TimeSpan> RunTest(string url, int concurrentConnections)
        {
            var sw = new Stopwatch();
            var client = new HttpClient();

            await client.GetStringAsync(url); // warmup
            sw.Start();
            await Task.WhenAll(Enumerable.Range(0, concurrentConnections).Select(i => client.GetStringAsync(url)));
            sw.Stop();

            return sw.Elapsed;
        }
    }
}
