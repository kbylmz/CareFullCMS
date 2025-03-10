using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CareFullCMS.Domain.Entities.Contents;

namespace CareFullCMS.Infrastructure.Persistence.Mongo.Models
{
    public class ContentMongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public ContentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public List<ContentField> Fields { get; set; } = new();
        public List<ContentVersion> Versions { get; set; } = new();
        public string Language { get; set; }

        // Converts to Domain Model
        public Content ToDomain()
        {
            return new Content
            {
                Id = Id,
                Title = Title,
                Slug = Slug,
                Status = Status,
                CreatedAt = CreatedAt,
                PublishedAt = PublishedAt,
                Fields = Fields,
                Versions = Versions,
                Language = Language
            };
        }

        // Converts from Domain Model
        public static ContentMongoModel FromDomain(Content content)
        {
            return new ContentMongoModel
            {
                Id = content.Id,
                Title = content.Title,
                Slug = content.Slug,
                Status = content.Status,
                CreatedAt = content.CreatedAt,
                PublishedAt = content.PublishedAt,
                Fields = content.Fields,
                Versions = content.Versions,
                Language = content.Language
            };
        }
    }
}
