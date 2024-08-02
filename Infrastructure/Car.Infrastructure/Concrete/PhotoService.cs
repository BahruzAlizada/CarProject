using Car.Infrastructure.Abstract;
using Car.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;

namespace Car.Infrastructure.Concrete;

public class PhotoService : IPhotoService
{
    public void DeletePhoto(string path)
    {
        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);
    }

    public async Task<(bool, string)> PhotoChechkValidatorAsync(IFormFile photo, bool IsAllowNull, bool IsEqual256Kb)
    {
        if (!IsAllowNull)
        {
            if (photo is null)
                return (false, "Bu xana boş ola bilməz");
        }

        if (!photo.IsImage())
            return (false, "Sadəcə jpeg yaxud jpg tipli fayllar");

        if (IsEqual256Kb)
        {
            if (photo.IsOlder256Kb())
                return (false, "Maksimum 256 Kb");
        }
        else
        {
            if (photo.IsOlder512Kb())
                return (false, "Maksimum 512 Kb");
        }

        return (true, "");
    }

    public async Task<string> SavePhotoAsync(IFormFile Photo, string folder)
    {
        return await Photo.SaveFileAsync(folder);
    }
}
