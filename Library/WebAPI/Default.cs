using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TYS.Library.WebAPI
{
    public abstract class Default
    {
        // 認証設定値
        protected AuthenticationStruct? authenticationData = null;
        // リトライ回数
        protected const int MAX_RETRY_COUNT = 3;
        protected int retryCount = 0;
        protected readonly TimeSpan delay = TimeSpan.FromSeconds(5);

        protected readonly HttpClient client;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authenticationData"></param>
        protected Default(HttpClient client, AuthenticationStruct? authenticationData = null)
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

        /// <summary>
        /// 呼び出し
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task<(bool Result, int StatusCode, byte[] ResponseByteArray)> Call(string url, dynamic data = null)
        {
            try
            {
                HttpResponseMessage response = await Request(url, data);
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
                            return await Call(url, data);
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
        /// send request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected abstract Task<HttpResponseMessage> Request(string url, dynamic data);

        /// <summary>
        /// 認証設定を再設定
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        protected virtual void SetAuthenticationHeader()
        {
            var authHeader = Authentication.GetAuthenticationHeader(authenticationData.Value);
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", authHeader);
        }

        /// <summary>
        /// HttpContentに変換
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual HttpContent ConvertToHttpContent(dynamic data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
