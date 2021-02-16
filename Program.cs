using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;

namespace dotnetcore_send_email
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25,
            });

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            StringBuilder template = new();
            template.AppendLine("Sayın @Model.Fullname,");
            template.AppendLine("<p>Satın aldığınız ürün: @Model.Product. Bizi tercih ettiğiniz için teşekkür ederiz. </p>");
            template.AppendLine("X Company");

            var email = await Email
                .From("info@xxx.com")
                .To("ali.veli@aaa.com", "Ali Veli")
                .Subject("Tebrikler!")
                .UsingTemplate(template.ToString(), new { Fullname = "Ali Veli", Product = "Apple Macbook 13"}, true)
                //.Body("This is a test, beach!")
                .SendAsync();

            if (email.Successful == false)
                Console.WriteLine("Eposta gönderilemedi, bir hata oluştu.");
            else
                Console.WriteLine("Eposta gönderimi başarılı.");
        }
    }
}
