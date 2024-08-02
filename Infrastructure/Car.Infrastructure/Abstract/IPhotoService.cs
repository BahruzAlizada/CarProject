using Microsoft.AspNetCore.Http;

namespace Car.Infrastructure.Abstract
{
    public interface IPhotoService
    {
        Task<(bool, string)> PhotoChechkValidatorAsync(IFormFile photo, bool IsAllowNull, bool IsEqual256Kb);
        Task<string> SavePhotoAsync(IFormFile Photo, string folder);
        void DeletePhoto(string path);
    }
}
