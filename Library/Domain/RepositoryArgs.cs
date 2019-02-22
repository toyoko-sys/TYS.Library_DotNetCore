using System.Dynamic;
using Microsoft.Extensions.Logging;

namespace TYS.Library.Domain.Repository
{
    /// <summary>
    /// Repository実行時の引数
    /// </summary>
    public class RepositoryArgs
    {
        /// <summary>
        /// モジュール識別番号
        /// </summary>
        public int ModuleId = 0;
        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger = null;
        /// <summary>
        /// 入力引数
        /// </summary>
        public dynamic Input = new ExpandoObject();
        /// <summary>
        /// 出力引数
        /// </summary>
        public dynamic Entity = new ExpandoObject();
    }
}
