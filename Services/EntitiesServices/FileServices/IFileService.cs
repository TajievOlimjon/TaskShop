using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.FileServices
{
    public interface IFileService
    {
        string AddFile(IFormFile file);
        string UpdateFile(IFormFile file);
        string DeleteFile(string file);
    }
}
