using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class FileManagementService : IFileManagementService
    {
        public string GetFileDownloadLink(HttpRequest request, string filepath)
        {
            return $"{request.Scheme}://{request.Host}{request.PathBase}/{filepath}";
        }

        public async Task<int> UploadFile(IFormFile file, string filepath, int maxsize = 10001000)
        {
            if (file.Length >= 10 && file != null)
            {
                //check file size
                if (file.Length > maxsize)
                {
                    return 1004;//larg file error code
                }

                var filesfolder = Path.GetDirectoryName(filepath);

                if (!Directory.Exists(filesfolder))
                {
                    Directory.CreateDirectory(filesfolder);
                }

                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                using (FileStream stream = File.Create(filepath))
                {
                    await file.CopyToAsync(stream);
                    await stream.FlushAsync();
                }
                return 1002;//for success uploading
            }
            else
                return 1005;//for small length error
        }
    }
}
