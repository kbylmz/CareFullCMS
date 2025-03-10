namespace CareFullCMS.Domain.Entities.Contents
{
    public class ContentField
    {
        public string Name { get; set; } // Field Name
        public string Type { get; set; } // e.g., "Text", "Markdown", "RichText", "Image", etc.
        public string Value { get; set; } // Field Value
    }
}
