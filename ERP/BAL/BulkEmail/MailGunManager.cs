using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.BulkEmail
{
    public class MailGunManager
    {
        public static RestSharp.IRestResponse SendSimpleMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               "key-92aa7c64fa2bcf2bc563eda1ebdfa45c");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 "sandbox90952ac641ab46a8a83bc50f23d98215.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "abhishekkhemariya29@gmail.com");
            request.AddParameter("to", "abhishek.k@cruzata.com");
            request.AddParameter("to", "abhishekkhemariya29@gmail.com");
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
