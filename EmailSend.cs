using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace ConsoleAppContacts
{
    internal class EmailSend
    {
        public static async Task SendEmail(string myName, string recepientEmail, string recepientName, string subject, string body)
        {
            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("ApiKey"), Environment.GetEnvironmentVariable("ApiSecret"));
            
            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
               .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", Environment.GetEnvironmentVariable("MyEmail")},
                  {"Name", myName}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", recepientEmail},
                   {"Name", recepientName}
                   }
                  }},
                 {"Subject", subject},
                 {"TextPart", body},
                 }
                   });
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Email Send Successfully");
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }
    }
}
