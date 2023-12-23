using MediatR;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace PoC.Api
{
    public class RequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : class
    {
        private ILogger<RequestLoggingBehavior<TRequest, TResponse>> _logger;

        public RequestLoggingBehavior(ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var timer = new Stopwatch();

            timer.Start();  

            var log = new StringBuilder();
            var requestModel = string.Empty;
            var responseModel = string.Empty;

            Exception exception = null;

            try
            {
                requestModel = JsonSerializer.Serialize(request);
             

                log.Append("Request Handled: {Name}");

                log.AppendLine("Request: {RequestModel}");

                var response = await next();

                responseModel = JsonSerializer.Serialize(response);

                log.AppendLine("Response: {ResponseModel}");

                return response;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                timer.Stop();

                log.AppendLine("Elapsed Time: {ElapsedMilliseconds} ms");

                if (exception != null)
                {
                    log.AppendLine("Exception: {Exception}");
                }

                _logger.LogInformation(log.ToString(), typeof(TRequest).Name, requestModel, responseModel, timer.ElapsedMilliseconds, exception);
            }
        }
    }
}