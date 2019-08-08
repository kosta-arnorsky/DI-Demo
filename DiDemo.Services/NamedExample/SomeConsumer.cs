using DiDemo.Abstractions;
using System;

namespace DiDemo.Services.NamedExample
{
    public class SomeConsumer
    {
        private readonly IService _service;
        private readonly Guid _id;

        public SomeConsumer(IService service, ILogger logger)
        {
            _service = service;
            _id = Guid.NewGuid();
            logger.Log($"{nameof(SomeConsumer)} is created.");
        }

        public string GetSomething()
        {
            var value = _service.GetValue();
            return $"Consumer {_id}, some result: {value}";
        }
    }
}
