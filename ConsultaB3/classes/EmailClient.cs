using ConsultaB3.models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAPTraining.Classes
{
    public class EmailClient
    {

        public void sendEmail(Email email, ComunicacaoConfig comunicacao)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(email.toEmail, email.fromEmail));
            message.To.Add(new MailboxAddress(email.toName, email.toEmail));
            message.Subject = email.subject;

            message.Body = new TextPart("plain")
            {
                Text = email.body
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(comunicacao.SMTP, comunicacao.Port);
                client.Authenticate(comunicacao.Email, comunicacao.Password);
                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}
