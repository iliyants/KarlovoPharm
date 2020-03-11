namespace KarlovoPharm.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile file, string fileName, string folder);
    }
}
