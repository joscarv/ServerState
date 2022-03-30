using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServerStateEmail
{
    internal class Email
    {
        public void sendEmail(String text)
        {
            SmtpClient client = new SmtpClient("10.128.10.32", 25);
            client.Credentials = new NetworkCredential("juarezo@sanborns.com.mx", "Cogo7454");
                       
            MailAddress from = new MailAddress("juarezo@sanborns.com.mx", "Oscar Juarez");
            MailAddress to = new MailAddress("SappsiSoporte@sanborns.net");
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Tiendas con XpressServer Abierto";
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            String imagePath = @"img/cam.png";
            LinkedResource img = new LinkedResource(imagePath);
            img.ContentId = "camilo";

            string htmlBody = "<html><body>" + text + "</body></html>";
            AlternateView htmlVeiw = AlternateView.CreateAlternateViewFromString(htmlBody, System.Text.Encoding.UTF8, "text/html");
            htmlVeiw.LinkedResources.Add(img);
            message.AlternateViews.Add(htmlVeiw);

            try
            {
                Console.WriteLine("Sending email...");
                Log.writeLog("Sending email...");
                client.Send(message);
                Console.WriteLine("Mail send successfull!");
                Log.writeLog("Mail send successfull!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Log.writeLog($"Error: {ex.Message}");
            }

            message.Dispose();
            client.Dispose();
            Console.WriteLine("Goodbye.");
        }

        public void sendEmailOutlook(String text)
        {
            MailAddress from = new MailAddress("oscar.jv@outlook.com", "Oscar Juarez (outlook)");
            MailAddress to = new MailAddress("juarezo@sanborns.com.mx");
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Tiendas con XpressServer Abierto";
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            string htmlBody = "<html><body>" + text + "</body></html>";
            AlternateView htmlVeiw = AlternateView.CreateAlternateViewFromString(htmlBody, System.Text.Encoding.UTF8, "text/html");

            String imagePath = @"img/cam.png";
            try
            {
                Log.writeLog("Loading parameters...");
                LinkedResource img = new LinkedResource(imagePath);
                img.ContentId = "camilo";
                htmlVeiw.LinkedResources.Add(img);
            }
            catch (Exception ex)
            {
                Log.writeLog("Error loading parameters: " + ex.Message);
            }

            message.AlternateViews.Add(htmlVeiw);
            

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587);
            client.Credentials = new NetworkCredential("oscar.jv@outlook.com", "tese_0073");
            client.EnableSsl = true;
            
            try
            {
                Console.WriteLine("Sending email...");
                Log.writeLog("Sending email...");
                client.Send(message);
                Console.WriteLine("Mail send successfull!");
                Log.writeLog("Mail send successfull!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Log.writeLog($"Error: {ex.Message}");
            }

            message.Dispose();
            client.Dispose();
            Console.WriteLine("Goodbye.");
        }
    }
}
