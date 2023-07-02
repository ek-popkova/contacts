using ContactsManager.Models;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System;

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

                if (result == null)
                {
                    _logger.LogInformation("An error occured while getting contacts from accessor service");
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
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException.Message);
                return Problem(ex.Message);
            }
        }

        [HttpGet("/contact/{phone}")]
        public async Task<ActionResult<List<Contact>>> GetPhoneByNameAsync(string phone)
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<List<Contact>>(HttpMethod.Get, "ContactsAccessor", $"/Contact/getContactByPhone/{phone}");

                if (result is null)
                {
                    _logger.LogInformation("An error occured while getting contacts with phone number {phone} from accessor service", phone);
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
                _logger.LogError(ex.Message);
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
                _logger.LogError(ex.InnerException.Message);
                return Problem(ex.Message);
            }
        }

        [HttpDelete("/contact/{phone}")]
        public async Task<ActionResult<List<Contact>>> DeleteContactByPhoneAsync(string phone)
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<long>(HttpMethod.Delete, "ContactsAccessor", $"/Contact/deleteContactByPhone/{phone}");

                if (result == 0)
                {
                    _logger.LogInformation("No contact with phone number: {phone} found in DB", phone);
                    return Ok($"No contact with phone number: {phone} found in DB");
                }
                else
                {
                    _logger.LogInformation("Deleted {result} phone with name: {name}", result, phone);
                    return Ok($"Successfully deleted contact with phone number: {phone}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException.Message);
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
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException.Message);
                return Problem(ex.Message);
            }
        }


    }
}