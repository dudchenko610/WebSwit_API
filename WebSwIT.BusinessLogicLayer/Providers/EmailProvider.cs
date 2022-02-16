using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Options;

namespace WebSwIT.BusinessLogicLayer.Providers
{
    public class EmailProvider
    {
        private readonly IOptions<EmailConnectionOptions> _connectionOptions;
        private readonly string _mailAddress;
        private readonly string _port;
        private readonly string _password;

        public EmailProvider(IOptions<EmailConnectionOptions> connectionOptions)
        {
            _connectionOptions = connectionOptions;
            _mailAddress = _connectionOptions.Value.MailAddress;
            _port = _connectionOptions.Value.Port;
            _password = _connectionOptions.Value.Password;
        }

        public async Task SendEmailAsync(string mailTo, string caption, string textMessage)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(_mailAddress);
                mail.To.Add(new MailAddress(mailTo));
                mail.Subject = caption;
                mail.Body = textMessage;

                var client = new SmtpClient();

                client.Host = "smtp.gmail.com";
                client.Port = Convert.ToInt32(_port);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_mailAddress, _password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                await client.SendMailAsync(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {
                throw new ServerException("Message was not sending!", HttpStatusCode.InternalServerError);
            }
        }
    }
}

