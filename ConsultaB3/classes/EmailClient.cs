using ConsultaB3.Classes;
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

namespace ConsultaB3.Classes
{
    public class EmailClient
    {

        public void sendEmail(ComunicacaoConfig comunicacao, Email email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(comunicacao.FromName, comunicacao.FromEmail));
            message.To.Add(new MailboxAddress(comunicacao.ToName, comunicacao.ToEmail));
            message.Subject = email.subject;

            message.Body = new TextPart("plain")
            {
                Text = email.body
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(comunicacao.SMTP, comunicacao.Port);
                client.Authenticate(comunicacao.FromEmail, comunicacao.Password);
                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}
