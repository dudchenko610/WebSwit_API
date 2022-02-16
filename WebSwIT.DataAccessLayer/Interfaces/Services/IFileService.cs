using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebSwIT.Shared.Models.File;

namespace WebSwIT.DataAccessLayer.Interfaces.Services
{
    public interface IFileService
    {
        void Delete(string path, string name);
        DownloadFileModel Get(string path, string name);
        Task CreateAsync(IFormFile file, string path, string name);
    }
}
