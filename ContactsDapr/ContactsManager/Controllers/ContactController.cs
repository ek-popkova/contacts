using ContactsManager.Models;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {

        private readonly ILogger<ContactController> _logger;
        private readonly DaprClient _daprClient;
        public ContactController(ILogger<ContactController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet("/contacts")]
        public async Task<ActionResult<List<Contact>>> GetAllPhonesAsync()
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<List<Contact>>(HttpMethod.Get, "ContactsAccessor", "/Contact/getAllContacts");

                if (result is null)
                {
                    _logger.LogInformation("Request from acessor service returned an empty list of contacts");
                    return NotFound("No contacts found");
                }
                else
                {
                    _logger.LogInformation("List of contacts successfully retrieved from accessor service");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                return Problem(ex.Message);
            }
        }


    }
}