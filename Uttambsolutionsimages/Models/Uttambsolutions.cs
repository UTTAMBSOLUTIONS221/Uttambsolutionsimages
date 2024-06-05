namespace Uttambsolutionsimages.Models
{
    public class Uttambsolutions
    {
        public string? Foldername { get; set; }
        public string? OldFileImagePath { get; set; }
        public List<string>? OldFileImagePaths { get; set; }
        public IList<IFormFile>? Fileimages { get; set; }
    }
}
