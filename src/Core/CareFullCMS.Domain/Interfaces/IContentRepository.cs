using System.Collections.Generic;
using System.Threading.Tasks;
using CareFullCMS.Domain.Entities.Contents;

namespace CareFullCMS.Domain.Interfaces
{
    public interface IContentRepository
    {
        Task<Content> GetByIdAsync(string id);
        Task<IEnumerable<Content>> GetAllAsync();
        Task AddAsync(Content content);
        Task UpdateAsync(Content content);
        Task DeleteAsync(string id);
        Task<IEnumerable<Content>> GetByStatusAsync(ContentStatus status);

        // New Methods for Versioning
        Task<List<ContentVersion>> GetVersionsAsync(string contentId);
        Task RestoreVersionAsync(string contentId, ContentVersion version);
    }
}
