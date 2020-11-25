using Grpc.Core;
using GrpcReferenceDemo.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcReferenceDemo.Server
{
    public class TestStreamerService : TestStreamer.TestStreamerBase
    {
        private readonly ILogger<TestStreamerService> _logger;

        public TestStreamerService(ILogger<TestStreamerService> logger)
        {
            _logger = logger;
        }

        public override async Task DoAThing(ThingRequest request, IServerStreamWriter<ThingReply> responseStream, ServerCallContext context)
        {
            for (int current = 0; current < 100; current++)
            {
                // Do stuff and prepare the/a reponse message
                var reply = new ThingReply {
                    Latest = current,
                    Sqrt = Math.Sqrt(current)
                };

                // Send response message & wait
                await responseStream.WriteAsync(reply);
                await Task.Delay(1000);
            }
        }
    }
}
