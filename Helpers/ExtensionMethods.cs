namespace FurniMpa201.Helpers
{
    public static class ExtensionMethods
    {
        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.StartsWith(type);
        }
        public static bool CheckSize(this IFormFile file, int size)
        {
            return (file.Length / 1024) <= size * 1024;
        }
        public static string SaveFile(this IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string fullPath = Path.Combine(folder, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
