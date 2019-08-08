using System;

namespace DiDemo.Services.NamedExample
{
    public class AnotherService : IService
    {
        private Random _random;

        public AnotherService()
        {
            _random = new Random();
        }

        public int GetValue()
        {
            return _random.Next(1, 10) * 100;
        }
    }
}
