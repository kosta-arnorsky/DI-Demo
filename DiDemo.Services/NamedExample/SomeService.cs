using DiDemo.Abstractions;
using System;

namespace DiDemo.Services.NamedExample
{
    public class SomeService : IService
    {
        private readonly Random _random;

        public SomeService(ILogger logger)
        {
            _random = new Random();
            logger.Log($"{nameof(SomeService)} is created.");
        }

        public int GetValue()
        {
            return _random.Next(1, 10);
        }
    }
}
