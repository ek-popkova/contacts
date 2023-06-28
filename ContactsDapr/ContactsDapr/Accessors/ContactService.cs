using ContactsDapr.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Runtime.InteropServices;

namespace ContactsDapr.Accessors
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

        private Contact FromDTO(ContactDTO contact)
        {
            return new Contact()
            {
                Name = contact.name,
                Phone = contact.phone
            };
        }
    }
}
