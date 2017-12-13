using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    //The token endpoint at IdentityServer implements the OAuth 2.0 protocol, and you could use raw HTTP to access it.
    //However, we have a client library called IdentityModel, that encapsulates the protocol interaction in an 
    //easy to use API.
    //http://localhost:5000/connect/token
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            //var disco = await DiscoveryClient.GetAsync("https://site1.mydomain.com/identityserver/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");


            // request token
            var tokenClientRo = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponseRo = await tokenClientRo.RequestResourceOwnerPasswordAsync("alice", "password", "api1");

            if (tokenResponseRo.IsError)
            {
                Console.WriteLine(tokenResponseRo.Error);
                return;
            }
            //The presence(or absence) of the sub claim lets the API distinguish between calls on behalf of clients and 
            //    calls on behalf of users.

            Console.WriteLine(tokenResponseRo.Json);
            Console.WriteLine("\n\n");


            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            //var response = await client.GetAsync("http://127.0.0.1:5001/api/identity");
            var response = await client.GetAsync("https://site1.mydomain.com/webapi/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }

}
