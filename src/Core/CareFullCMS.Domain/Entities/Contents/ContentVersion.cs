using System;
using System.Collections.Generic;

namespace CareFullCMS.Domain.Entities.Contents
{
    public class ContentVersion
    {
        public DateTime VersionDate { get; set; }
        public string VersionedBy { get; set; } // User ID who created the version
        public List<ContentField> Fields { get; set; } = new(); // Fields snapshot
    }
}
