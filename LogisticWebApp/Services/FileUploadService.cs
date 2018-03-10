using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Web.Services
{
    public class FileUploadService
    {
        private readonly IHostingEnvironment _appEnvironment;

        public FileUploadService(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task<string> AddFile(IFormFile thefile)
        {
            //Path.Combine("Files", Guid.NewGuid().ToString()  +Path.GetExtension(thefile.FileName));
            // путь к папке Files
            string path = "/Files/" + Guid.NewGuid().ToString() + Path.GetExtension(thefile.FileName);
            // сохраняем файл в папку Files в каталоге wwwroot
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {

                await thefile.CopyToAsync(fileStream);
                return path;

            }
        }
    }
}
