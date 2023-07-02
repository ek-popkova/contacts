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

        [HttpPost("/contact")]
        public async Task<ActionResult<Contact>> AddContactAsync(Contact contact)
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<Contact, Contact>(HttpMethod.Post, "ContactsAccessor", "/Contact/addNewContact", contact);

                if (result is null)
                {
                    _logger.LogInformation("An error occured while adding contact to DB");
                    return BadRequest("An error occured while adding contact to DB");
                }
                else
                {
                    _logger.LogInformation("New contact successfully added to DB");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost("/contact-rabbitMQ")]
        public async Task<ActionResult<Contact>> AddPhoneToQueueAsync(Contact contact)
        {
            try
            {
                await _daprClient.InvokeBindingAsync("contactqueue", "create", contact);

                _logger.LogInformation("Contact sucessfully added to the queue");
                return Ok("Contact sucessfully added to the queue");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                return Problem(ex.InnerException.Message);
            }
        }


    }
}