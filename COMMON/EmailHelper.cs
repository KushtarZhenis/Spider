using System;
using System.Reflection;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace COMMON;
public class EmailHelper
{
    readonly static string sender = "jengisqushtar@gmail.com";

    private static string GetTemplateContent(string fileName)
    {
        var assembly = typeof(EmailHelper).GetTypeInfo().Assembly;
        string path = $"COMMON.templates.{fileName}";
        var resourceStream = assembly.GetManifestResourceStream(path);
        using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }
    public static bool SendHtmlEmail(string email, string link, out string message)
    {
        try
        {
            message = string.Empty;
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("film.com", $"verify@film.com"));
            mimeMessage.To.Add(new MailboxAddress("", email));
            mimeMessage.Subject = "Email анықтау";
            string htmlContet = GetTemplateContent("verificatonemail.html");
            htmlContet = htmlContet.Replace("{{link}}", link);
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlContet
            };

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect("smtp.gmail.com", 465, true); //if port is 587 false
            client.Authenticate(sender, "jeqs yuzb kdvl ixbd");
            client.Send(mimeMessage);
            client.Disconnect(true);
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }
}