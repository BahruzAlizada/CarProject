using Microsoft.AspNetCore.Http;

namespace Car.Infrastructure.Helpers
{
    public static class File
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public static bool IsOlder256Kb(this IFormFile file)
        {
            return file.Length / 1024 > 1024 / 4;
        }

        public static bool IsOlder512Kb(this IFormFile file)
        {
            return file.Length / 1024 > 1024 / 2;
        }

        public static async Task<string> SaveFileAsync(this IFormFile file, string folder)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(folder, filename);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filename;
        }
    }
}
