using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Services.EntitiesServices.FileServices
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string AddFile(IFormFile file)
        {
            if (file != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return fileName;
            }
            return null;
        }

        public string DeleteFile(string fileName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return "Deleted file";
            }
            return "No deleted file";
        }

        public string UpdateFile(IFormFile file)
        {
            string fileName, path;

            if (!file.Equals(null))
            {
                //return new Exception("В коде изменение фото продукта есть ").ToString();
                var fullPath = _webHostEnvironment.WebRootPath + file;
                if (!File.Exists(fullPath))
                {
                    fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                    path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return fileName;
                }
                else if (File.Exists(fullPath))
                    return fullPath;

            }
            return null;
        }
    }
}
