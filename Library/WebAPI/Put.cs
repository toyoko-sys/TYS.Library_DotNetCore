using System.Net.Http;
using System.Threading.Tasks;

namespace TYS.Library.WebAPI
{
    /// <summary>
    /// PUT
    /// </summary>
    public class Put : Default
    {
        public Put(HttpClient client, AuthenticationStruct? authenticationData = null)
            : base(client, authenticationData)
        {
        }

        /// <summary>
        /// send a put request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> Request(string url, dynamic data)
        {
            var content = ConvertToHttpContent(data);
            return await client.PutAsync(url, content);
        }
    }
}
