using Accessor.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Accessor.Accessors;
using static System.Runtime.InteropServices.JavaScript.JSType;

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


        [HttpGet("getAllContacts")]
        public async Task<ActionResult<List<Contact>>> GetAllContactsAsync()
        {
            try
            {
                _logger.LogInformation("Entered GetAllContactAsync method");

                var result = await _contactService.GetAllAsync();

                if (result is null)
                {
                    _logger.LogInformation("An error occured while getting contacts from DB");
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

        [HttpGet("getContactByPhone/{phone}")]
        public async Task<ActionResult<List<Contact>>> GetContactByPhoneAsync(string phone)
        {
            try
            {
                _logger.LogInformation("Entered GetContactByPhoneAsync method");

                var result = await _contactService.GetContactByPhoneAsync(phone);

                if (result is null)
                {
                    _logger.LogInformation("An error occured while getting contacts with phone number {phone} from DB", phone);
                    return NotFound("No contacts found");
                }
                else
                {
                    _logger.LogInformation("List of contacts with phone number {phone} successfilly retrieved from DB", phone);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost("addNewContact")]
        public async Task<ActionResult<Contact>> AddContactAsync(Contact contact)
        {
            try
            {
                _logger.LogInformation("Entered AddContactAsync method");

                var result = await _contactService.AddContactAsync(contact);

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

        [HttpDelete("deleteContactByPhone/{phone}")]
        public async Task<ActionResult<long>> DeleteContactByPhoneAsync(string phone)
        {
            try
            {
                _logger.LogInformation("Entered DeleteContactByPhoneAsync method");

                var result = await _contactService.DeleteContactByPhoneAsync(phone);
                
                _logger.LogInformation("Successfully deleted from DB {result} contact(-s) with phone number: {phone}", result, phone);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost("/contactqueue")]
        public async Task<ActionResult<Contact>> AddContactFromQueueAsync(Contact contact)
        {
            try
            {
                _logger.LogInformation("Entered AddContactFromQueueAsync method");

                var result = await _contactService.AddContactAsync(contact);

                if (result is null)
                {
                    _logger.LogInformation("An error occured while adding contact through queue to DB");
                    return BadRequest("An error occured while adding contact through queue to DB");
                }
                else
                {
                    _logger.LogInformation("New contact successfully added through queue to DB");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}