using LibContent.Entities;
using MongoDB.Driver;

namespace ContentCacheApi.Services
{
    public class ContentCacheService : IContentCacheService
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        private const string DATABASE = "ContentCache";
        private const string COLLECTION = "Pages";

        public ContentCacheService(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = _client.GetDatabase(DATABASE);
        }

        public async Task<Page> SetPageCache(Page page)
        {
            var collection = _database.GetCollection<Page>(COLLECTION);
            var filter = Builders<Page>.Filter.Eq(p => p.Id, page.Id);
            var options = new FindOneAndReplaceOptions<Page, Page> { IsUpsert = true };
            await collection.FindOneAndReplaceAsync(filter, page, options);
            return page;
        }

        public async Task<Page?> GetPageCache(Guid id)
        {
            var collection = _database.GetCollection<Page>(COLLECTION);
            var filter = Builders<Page>.Filter.Eq(p => p.Id, id);
            return (await collection.FindAsync<Page>(filter)).FirstOrDefault();
        }
    }

}
