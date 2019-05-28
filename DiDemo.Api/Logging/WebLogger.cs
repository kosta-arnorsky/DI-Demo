using DiDemo.Abstractions;
using DiDemo.Logging;
using Microsoft.AspNetCore.Http;
using System;

namespace DiDemo.Api.Logging
{
    public class WebLogger : ConsoleLogger, ILogger
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public WebLogger(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public override void Log(string message)
        {
            HttpContext context = _contextAccessor.HttpContext;
            if (context != null)
            {
                message += Environment.NewLine
                    + "Trace ID: " + context.TraceIdentifier;
            }

            base.Log(message);
        }
    }
}
