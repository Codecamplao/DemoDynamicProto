using Grpc.Core;
using GRPCServer;

namespace GRPCServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloRequest> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(request);
        }
    }
}