
namespace DiDemo.Services.NamedExample
{
    public class AnotherConsumer
    {
        private readonly IService _service;

        public AnotherConsumer(IService service)
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
