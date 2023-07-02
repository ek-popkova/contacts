using Accessor.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Runtime.InteropServices;

namespace Accessor.Accessors
{
    public class ContactService : IContactService
    {
        private readonly IMongoDBContext _context;
        protected IMongoCollection<ContactDTO> _dbCollection;
        private readonly string collectionName = "Contact";

        public ContactService(IMongoDBContext context)
        { 
            _context = context;
            _dbCollection = _context.GetCollection<ContactDTO>(collectionName);
        }

        public async Task<List<Contact>?> GetAllAsync()
        {
            try
            {
                var result = await _dbCollection.FindAsync(Builders<ContactDTO>.Filter.Empty);
                return result.ToList().Select(FromDTO).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Contact?> AddContactAsync(Contact contact)
        {
            try
            {
                await _dbCollection.InsertOneAsync(ToDTO(contact));

                return contact;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Contact>?> GetContactByPhoneAsync(string phone)
        {
            try
            {
                var result = await _dbCollection.Find(contact => contact.phone == phone).ToListAsync();

                return result.Select(FromDTO).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> DeleteContactByPhoneAsync(string phone)
        {
            try
            {
                var result = await _dbCollection.DeleteManyAsync(contact => contact.phone == phone);
                return result.DeletedCount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Contact FromDTO(ContactDTO contact)
        {
            return new Contact()
            {
                Name = contact.name,
                Phone = contact.phone
            };
        }

        private ContactDTO ToDTO(Contact contact)
        {
            return new ContactDTO()
            {
                name = contact.Name,
                phone = contact.Phone
            };
        }
    }
}
