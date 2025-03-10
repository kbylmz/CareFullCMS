using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using CareFullCMS.Domain.Entities.Contents;
using CareFullCMS.Domain.Interfaces;
using CareFullCMS.Infrastructure.Persistence.Mongo.Models;
using System.Linq;

namespace CareFullCMS.Infrastructure.Persistence.Mongo.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly IMongoCollection<Content> _contentCollection;

        public ContentRepository(IMongoDatabase database)
        {
            _contentCollection = database.GetCollection<Content>("Contents");
        }

        public async Task<Content> GetByIdAsync(string id) =>
            await _contentCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Content>> GetAllAsync() =>
            await _contentCollection.Find(_ => true).ToListAsync();

        public async Task AddAsync(Content content) =>
            await _contentCollection.InsertOneAsync(content);

        public async Task UpdateAsync(Content content)
        {
            // Save the current version before updating
            var existingContent = await GetByIdAsync(content.Id);
            if (existingContent != null)
            {
                var version = new ContentVersion
                {
                    VersionDate = System.DateTime.UtcNow,
                    VersionedBy = "System", // Ideally, this should be the current user
                    Fields = new List<ContentField>(existingContent.Fields)
                };

                existingContent.Versions.Add(version);
            }

            await _contentCollection.ReplaceOneAsync(c => c.Id == content.Id, content);
        }

        public async Task DeleteAsync(string id) =>
            await _contentCollection.DeleteOneAsync(c => c.Id == id);

        public async Task<IEnumerable<Content>> GetByStatusAsync(ContentStatus status) =>
            await _contentCollection.Find(c => c.Status == status).ToListAsync();

        public async Task<List<ContentVersion>> GetVersionsAsync(string contentId)
        {
            var content = await GetByIdAsync(contentId);
            return content?.Versions ?? new List<ContentVersion>();
        }

        public async Task RestoreVersionAsync(string contentId, ContentVersion version)
        {
            var content = await GetByIdAsync(contentId);
            if (content == null) return;

            content.Fields = new List<ContentField>(version.Fields);
            await UpdateAsync(content);
        }
    }
}
