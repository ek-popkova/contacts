using MongoDB.Driver;

namespace ContactsDapr.Models
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
