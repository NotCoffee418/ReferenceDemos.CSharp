using Grpc.Core;
using Grpc.Net.Client;
using GrpcReferenceDemo.Shared;
using System;
using System.Threading.Tasks;

namespace GrpcReferenceDemo.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Prepare connection
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");


            // Prepare stream client
            TestStreamer.TestStreamerClient streamClient = 
                new TestStreamer.TestStreamerClient(channel);

            // Get reply stream
            AsyncServerStreamingCall<ThingReply> stream = 
                streamClient.DoAThing(new ThingRequest { Amount = 100 });

            // Print data while stream is alive.
            await foreach (var response in stream.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"Latest: {response.Latest} - Sqrt: {response.Sqrt}");
            }

            // Prevent close
            Console.WriteLine("Server is done sending. Goodbye.");
            Console.ReadLine();
        }
    }
}
