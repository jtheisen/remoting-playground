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

            var delay = 500;

            Console.WriteLine(await client.ExecuteAsync(() => delay));
            Console.WriteLine(await client.ExecuteAsync(() => Task.Delay(delay).ContinueWith(c => DateTimeOffset.Now)));
        }
    }
}
