using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Interfaces.Services;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.File;

namespace WebSwIT.DataAccessLayer.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        public DownloadFileModel Get(string path, string name)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Constants.File.FILE_FOLDER, path, name);

            var fileModel = new DownloadFileModel
            {
                Name = name,
                Type = Path.GetExtension(fullPath),
                File = File.ReadAllBytes(fullPath),
            };

            return fileModel;
        }

        public void Delete(string path, string name)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Constants.File.FILE_FOLDER, path, name);
    
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task CreateAsync(IFormFile file, string path, string name)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Constants.File.FILE_FOLDER, path);
            string fullPath = Path.Combine(folderPath, name);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
