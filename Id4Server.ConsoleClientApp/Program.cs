using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Id4Server.ConsoleClientApp
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Client Credentials
            /*var tokenClient = new TokenClient("http://localhost:5000/connect/token", "client", "secret");
            var tokenResponse =  tokenClient.RequestClientCredentialsAsync("api1").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
               // return;
            }

            Console.WriteLine(tokenResponse.Json);*/
            #endregion


            #region Resource Owner (userame/password)
            var tokenClient = new TokenClient("http://localhost:5000/connect/token", "ro.client", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("alice","password", "api1").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                // return;
            }

            Console.WriteLine(tokenResponse.Json); 





            #endregion



            #region Calling API
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response =  client.GetAsync("http://localhost:6000/api/values").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content =  response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
            #endregion














            Console.ReadKey();
        }
    }
}
