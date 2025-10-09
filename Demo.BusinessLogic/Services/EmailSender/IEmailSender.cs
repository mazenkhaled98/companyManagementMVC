using Demo.DataAccess.Models.Shared;

namespace Demo.BusinessLogic.Services.EmailSender
{
    public interface IEmailSender
    {

        //signature of method
        void SendEmail(Email email);

    }
}
