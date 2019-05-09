namespace TYS.Library.WebAPI
{
    public struct AuthenticationStruct
    {
        /// <summary>
        /// Azure AD テナントのディレクトリ ID
        /// </summary>
        public string AdId;
        /// <summary>
        /// 認証対象のクライアントID
        /// </summary>
        public string ResourceApplicationId;
        /// <summary>
        /// アクセス元 AD アプリのアプリケーションID
        /// </summary>
        public string ClientApplicationId;
        /// <summary>
        /// アクセス元 AD アプリで発行したキー
        /// </summary>
        public string SecretKey;
    }
}
