using System;
using System.Collections.Generic;

namespace CareFullCMS.Domain.Entities.Contents
{
    public class Content
    {
        public string Id { get; set; }  // No Mongo-specific attributes
        public string Title { get; set; }
        public string Slug { get; set; }
        public ContentStatus Status { get; set; } = ContentStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; set; }
        public List<ContentField> Fields { get; set; } = new();
        public List<ContentVersion> Versions { get; set; } = new();
        public string Language { get; set; }
    }
}
