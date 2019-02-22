using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TYS.Library.Controller
{
    /// <summary>
    /// APIControllerクラスの規程クラス
    /// </summary>
    public abstract class AbstractController : ControllerBase
    {
        /// <summary>
        /// 返却用のActionResultを作成します
        /// </summary>
        /// <param name="args">返却用データ</param>
        /// <returns>IHttpActionResult</returns>
        protected ActionResult CreateDefaultResult(ResponseArgs args)
        {
            ActionResult ret;

            if (args.Result)
            {
                ret = Ok(args.Model);
            }
            else
            {
                if (args.Model is HttpStatusCode code)
                {
                    ret = StatusCode((int)code);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            
            return ret;
        }
    }
}