using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //var folderPath = @"D:\Route-diploma\Youssef\MVC\Session 3\Demo\Company.Web\Company.Web\wwwroot\Images\";
            // 1. Get Folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", folderName);

            //2. Get Filename 
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";

            //3. Combine FolderPath with fileName
            var FilePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(fileStream);

            return FilePath;
        }
    }
}
