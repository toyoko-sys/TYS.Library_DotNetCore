using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EventLog
{
    public class EventLogWriter
    {
        /// <summary>
        /// イベントログ出力
        /// </summary>
        /// <param name="eventLogSourceName"></param>
        /// <param name="ex"></param>
        /// <param name="type"></param>
        /// <param name="args">ExpandoObjectで作られたインプット情報</param>
        /// <param name="classId"></param>
        public static void Error(ILogger logger, Exception ex, string message, int moduleId, Const.ClassId classId)
        {
            var eventId = GetEventId(moduleId, classId);
            logger.LogError(eventId, ex, message);
        }

        /// <summary>
        /// イベントログ出力
        /// </summary>
        /// <param name="eventLogSourceName"></param>
        /// <param name="ex"></param>
        /// <param name="type"></param>
        /// <param name="eventId"></param>
        public static void Info(ILogger logger, string message, int moduleId, Const.ClassId classId)
        {
            var eventId = GetEventId(moduleId, classId);
            logger.LogInformation(eventId, message);
        }

        /// <summary>
        /// イベントID取得
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public static EventId GetEventId(int moduleId, Const.ClassId classId)
        {
            return new EventId(moduleId + (int)classId);
        }
    }
}