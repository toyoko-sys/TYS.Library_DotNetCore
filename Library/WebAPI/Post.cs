using System.Net.Http;
using System.Threading.Tasks;

namespace TYS.Library.WebAPI
{
    /// <summary>
    /// POST
    /// </summary>
    public class Post : Default
    {
        public Post(HttpClient client, AuthenticationStruct? authenticationData = null)
            : base(client, authenticationData)
        {
        }

        /// <summary>
        /// send a post reauest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> Request(string url, dynamic data)
        {
            var content = ConvertToHttpContent(data);
            return await client.PostAsync(url, content);
        }
    }
}
