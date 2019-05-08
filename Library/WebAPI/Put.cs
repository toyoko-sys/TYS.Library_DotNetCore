using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TYS.Library.Library.WebAPI
{
    /// <summary>
    /// PUT
    /// </summary>
    public class Put
    {
        // 認証設定値
        protected AuthenticationStruct? authenticationData = null;
        // リトライ回数
        protected const int MAX_RETRY_COUNT = 3;
        protected int retryCount = 0;
        protected readonly TimeSpan delay = TimeSpan.FromSeconds(5);

        private readonly HttpClient client;

        public Put(HttpClient client, AuthenticationStruct? authenticationData = null)
        {
            this.client = client;
            this.authenticationData = authenticationData;

            // 認証情報が無ければ設定する
            if (client.DefaultRequestHeaders.Authorization == null)
            {
                if (this.authenticationData.HasValue)
                {
                    SetAuthenticationHeader();
                }
            }
        }

        public async Task<(bool Result, int StatusCode, byte[] ResponseByteArray)> Call(string url, HttpContent content)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    if (authenticationData.HasValue)
                    {
                        // トークンエラーの場合情報更新
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            SetAuthenticationHeader();
                            await Task.Delay(delay);
                        }

                        // リトライ
                        if (retryCount < MAX_RETRY_COUNT)
                        {
                            retryCount++;
                            return await Call(url, content);
                        }
                    }
                }
                var responseByteArray = await response.Content.ReadAsByteArrayAsync();
                return (response.IsSuccessStatusCode, (int)response.StatusCode, responseByteArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 認証設定を再設定
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        protected void SetAuthenticationHeader()
        {
            var authHeader = Authentication.GetAuthenticationHeader(authenticationData.Value);
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", authHeader);
        }
    }
}
