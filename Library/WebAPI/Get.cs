using System.Net.Http;
using System.Threading.Tasks;

namespace TYS.Library.Library.WebAPI
{
    /// <summary>
    /// GET
    /// </summary>
    public class Get : Default
    {
        public Get(HttpClient client, AuthenticationStruct? authenticationData = null)
            : base(client, authenticationData)
        {
        }

        /// <summary>
        /// send a get request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> Request(string url, dynamic data)
        {
            return await client.GetAsync(url);
        }
    }
}
