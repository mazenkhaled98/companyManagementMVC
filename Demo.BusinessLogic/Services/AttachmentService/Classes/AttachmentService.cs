using Demo.BusinessLogic.Services.AttachmentService.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttachmentService.Classes
{
    public class AttachmentService : IAttachmentService
    {
        public bool Delete(string path)
        {
            //get file path and check if exists
            if(File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;

        }

        public string? upload(IFormFile file, string folderName)
        {
            List<string> allowedExtensions = [".png", ".jpg", ".jpeg"];

            //2MB
           const int maxSize = 22_097_152; //2*1024*1024=2,097,152 bytes



            //1- Check Extension
            var Extension =Path.GetExtension(file.FileName).ToLower();//mazen.png => .png
            if (!allowedExtensions.Contains(Extension))
            {
                return null;
            }

            //2- Check Size
            if (file.Length > maxSize)
            {
                return null;
            }

            //3- Get Located Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);


            //4- Make attachement name unique
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";


            //5- Get file path
            var filePath = Path.Combine(folderPath, fileName);
            //6- Create file stream
            using FileStream fs = new FileStream(filePath, FileMode.Create);
            //7- Use stram to copy the file
            file.CopyTo(fs);
            //8- Return filename to store in db
            return fileName;
        }
    }
}
