using Accessor.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Accessor.Accessors;

namespace Accessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {

        private readonly ILogger<ContactController> _logger;
        private readonly ContactService _contactService;

        public ContactController(ILogger<ContactController> logger, ContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }


        
        //public async Task<ActionResult<IEnumerable<Contact>>> Get()
        //{
        //    var all = await _dbCollection.FindAsync(Builders<Contact>.Filter.Empty);
        //    if (all is null) { return NotFound(); }
        //    return Ok(all.ToList());
        //}

        [HttpGet]
        [Route("getAllContacts")]
        public async Task<ActionResult<List<Contact>>> GetAllContactsAsync()
        {
            try
            {
                _logger.LogInformation("Entered GetAllContactAsync method");

                var result = await _contactService.GetAllAsync();

                if (result is null)
                {
                    _logger.LogInformation("Request from DB returned an empty list of contacts");
                    return NotFound("no contacts found");
                }
                else
                {
                    _logger.LogInformation("List of contacts successfully retrieved from DB");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }



        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}