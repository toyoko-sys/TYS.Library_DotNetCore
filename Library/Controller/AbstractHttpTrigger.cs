using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TYS.Library.Controller
{
    public abstract class AbstractHttpTrigger
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
                ret = new OkObjectResult(args.Model);
            }
            else
            {
                string message = args.Model.Message;
                int errorCode = args.Model.ErrorCode != null ? args.Model.ErrorCode : args.Model.GetType() == typeof(int) ? args.Model : StatusCodes.Status500InternalServerError;

                if (!string.IsNullOrEmpty(message))
                {
                    ret = new ObjectResult(new { message = message })
                    {
                        StatusCode = errorCode
                    };
                }
                else
                {
                    ret = new StatusCodeResult(errorCode);
                }
            }

            return ret;
        }

        /// <summary>
        /// 返却用のActionResultを作成します
        /// </summary>
        /// <param name="args">返却用データ</param>
        /// <returns>IHttpActionResult</returns>
        protected ActionResult CreateNullResult(ResponseArgs args)
        {
            ActionResult ret;

            if (args.Result)
            {
                ret = new OkResult();
            }
            else
            {
                string message = args.Model.Message;
                int errorCode = args.Model.ErrorCode != null ? args.Model.ErrorCode : args.Model.GetType() == typeof(int) ? args.Model : StatusCodes.Status500InternalServerError;

                if (!string.IsNullOrEmpty(message))
                {
                    ret = new ObjectResult(new { message = message })
                    {
                        StatusCode = errorCode
                    };
                }
                else
                {
                    ret = new StatusCodeResult(errorCode);
                }
            }

            return ret;
        }
    }
}
