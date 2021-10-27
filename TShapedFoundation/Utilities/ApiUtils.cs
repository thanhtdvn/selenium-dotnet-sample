using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;

namespace TShapedFoundation.Utilities
{
    public class ApiUtils
    {
        public bool AddBookToUserCollection(string userId, string username, string password, string isbn)
        {
            RestClient client = new RestClient("https://demoqa.com");
            var obj = new
            {
                userId = userId,
                collectionOfIsbns = new List<object> { new { isbn = isbn } }
            };
            var request = new RestRequest("BookStore/v1/Books")
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(obj);
            client.Authenticator = new HttpBasicAuthenticator(username, password);
            var rs = client.Post(request);
            return rs.IsSuccessful;
        }
    }
}
