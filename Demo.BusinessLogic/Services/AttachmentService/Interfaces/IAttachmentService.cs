using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttachmentService.Interfaces
{
    public interface IAttachmentService
    {
        //upload
        public string? upload(IFormFile file, string folderName);

        //delete
        public bool Delete(string path);

    }
}
