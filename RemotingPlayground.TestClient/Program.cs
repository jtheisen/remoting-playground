using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingPlayground.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Test().Wait();

            Console.ReadKey();
        }

        async Task Test()
        {
            var client = new Client("https://localhost:44396/execute");

            Console.WriteLine(await client.ExecuteAsync(() => DateTimeOffset.Now));
            Console.WriteLine(await client.ExecuteAsync(() => Task.Delay(1000).ContinueWith(c => DateTimeOffset.Now)));
        }
    }
}
