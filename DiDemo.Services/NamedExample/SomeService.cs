using DiDemo.Abstractions;
using Microsoft.Extensions.Options;
using System;

namespace DiDemo.Services.NamedExample
{
    public class SomeService
    {
        private readonly SomeServiceOptions _options;
        private readonly Random _random;

        public SomeService(IOptions<SomeServiceOptions> options, ILogger logger)
        {
            _options = options.Value;
            _random = new Random();
            logger.Log($"{nameof(SomeService)} is created.");
        }

        public int GetValue()
        {
            return _random.Next(_options.MinValue, _options.MaxValue);
        }
    }
}
