using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Behaviours
{
    public class WatchBehavior<TRequest, TResponse>(ILogger<WatchBehavior<TRequest, TResponse>> logger)
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<WatchBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("\n\n🚀 🚀 🚀  - [{commandName}][START] - 🚀 🚀 🚀", typeof(TRequest).Name );
            Stopwatch stopWatcher = Stopwatch.StartNew();

            var result = await next();

            stopWatcher.Stop();
            long elapsed = stopWatcher.ElapsedMilliseconds;
            _logger.LogInformation($"\n\n🎉 🎉 🎉 - [{{commandName}}][END]: Executed in {elapsed} ms", typeof(TRequest).Name);

            return result;
        }

    }
}
