
namespace DiDemo.Services.NamedExample
{
    public class AnotherConsumer
    {
        private readonly SomeService _service;

        public AnotherConsumer(SomeService service)
        {
            _service = service;
        }

        public string GetAnotherThing()
        {
            var value = _service.GetValue();
            return $"Another result: {value}";
        }
    }
}
