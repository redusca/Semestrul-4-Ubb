using Grpc.Net.Client;
using GrpcClient;
using System;
using System.Threading.Tasks;

namespace GrpClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to exit...");
            
            using var channel = GrpcChannel.ForAddress("http://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}