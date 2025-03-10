using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CareFullCMS.Domain.Entities.Contents;
using CareFullCMS.Domain.Interfaces;

namespace CareFullCMS.Application.CQRS.Contents.Commands
{
    public class CreateContentCommand : IRequest<string>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public List<ContentField> Fields { get; set; }
        public string Language { get; set; }
        public DateTime? PublishedAt { get; set; } // NEW FIELD
    }

    public class CreateContentHandler : IRequestHandler<CreateContentCommand, string>
    {
        private readonly IContentRepository _contentRepository;

        public CreateContentHandler(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<string> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            
            var content = new Content
            {
                Title = request.Title,
                Slug = request.Slug,
                Fields = request.Fields,
                Language = request.Language,
                Status = request.PublishedAt > DateTime.UtcNow ? ContentStatus.Draft : ContentStatus.Published,
                PublishedAt = request.PublishedAt
            };

            await _contentRepository.AddAsync(content);
            return content.Id;
        }
    }
}
