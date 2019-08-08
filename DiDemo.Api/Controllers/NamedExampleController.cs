using DiDemo.Services.NamedExample;
using Microsoft.AspNetCore.Mvc;

namespace DiDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamedExampleController : ControllerBase
    {
        private readonly SomeConsumer _some1;
        private readonly SomeConsumer _some2;
        private readonly AnotherConsumer _another;

        public NamedExampleController(
            SomeConsumer some1,
            SomeConsumer some2,
            AnotherConsumer another)
        {
            _some1 = some1;
            _some2 = some2;
            _another = another;
        }

        [HttpGet("some")]
        public ActionResult<string> GetSome()
        {
            var something1 = _some1.GetSomething();
            var something2 = _some2.GetSomething();
            return something1 + "; " + something2;
        }

        [HttpGet("another")]
        public ActionResult<string> GetAnother()
        {
            return _another.GetAnotherThing();
        }
    }
}
