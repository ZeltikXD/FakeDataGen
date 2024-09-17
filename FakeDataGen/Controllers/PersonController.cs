using FakeDataGen.Code;
using Microsoft.AspNetCore.Mvc;

namespace FakeDataGen.Controllers
{
    [ApiController]
    [Route("api/persons")]
    [Produces("application/json")]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPersons(int seed, double errorProb, Locale locale = Locale.EN, int page = 1, int size = 10)
        {
            if (!Enum.IsDefined(locale))
                return BadRequest(new { Message = "The locale is not defined." });

            var dataGen = FakeDataGenerator.Create(seed, locale);
            return Ok(dataGen.Generate(page, size, errorProb));
        }
    }
}
