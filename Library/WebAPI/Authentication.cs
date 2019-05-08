using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace TYS.Library.Library.WebAPI
{
    /// <summary>
    /// 認証クラス
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Azure AD アクセス認証用ヘッダー付加情報取得
        /// </summary>
        /// <param name="authenticationData">アクセス認証用設定値</param>
        public static string GetAuthenticationHeader(AuthenticationStruct authenticationData)
        {
            /* Azure AD テナントの設定 */
            // トークン発行元
            string authority = "https://login.microsoftonline.com/" + authenticationData.AdId;

            /* トークン生成 */
            // ADAL の AuthenticationContext オブジェクト
            var authContext = new AuthenticationContext(authority);

            // クライアント資格情報
            var clientCredential = new ClientCredential(authenticationData.ClientApplicationId, authenticationData.SecretKey);

            // トークン要求
            var authResult = authContext.AcquireTokenAsync(authenticationData.ResourceApplicationId, clientCredential);

            // Bearer トークン
            string bearerToken = authResult.Result.CreateAuthorizationHeader();

            return bearerToken;
        }
    }
}
