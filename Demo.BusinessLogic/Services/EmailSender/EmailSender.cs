using Demo.DataAccess.Models.Shared;
using System.Net;
using System.Net.Mail;

namespace Demo.BusinessLogic.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            //protocol SMTP
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl=true;
            //sender , reciver , body
            //email sender and pass sender
            client.Credentials = new NetworkCredential("Mazenkhaled8@gmail.com", "thbidyfowxruiwnc");
            client.Send("Mazenkhaled8@gmail.com",
                email.To, email.Subject, email.Body);

        }
    }
}
