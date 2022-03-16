using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFileManagementService
    {
            /// <summary>
            /// Upload a file to the wwwroot directory
            /// </summary>
            /// <param name="file">the targetfile stream</param>
            /// <param name="filepath">the targetfile path in the wwwroot</param>
            /// <returns>
            /// 1004 : when the passed file is larger than the max size
            /// 1005 : when the passed file is empty
            /// 1002 : when the passed file is uploaded successfully
            /// </returns>
            Task<int> UploadFile(IFormFile file, string filepath, int maxsize = 10001000);

            string GetFileDownloadLink(HttpRequest request, string filepath);
      

    }
}
