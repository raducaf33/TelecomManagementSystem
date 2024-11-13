using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace TelecomManagement.Data.Logger
{
    /// <summary>
    /// Logger class.
    /// </summary>
   
    public class Logger : ILogger
    {
        /// <summary>
        /// The log object.
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Logs the error.
        /// </summary>
        
        public void LogError(string message, MethodBase method)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Error($"[{method.DeclaringType.Name}.{method.Name}]:{message}");
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
       
        public void LogError(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// Logs the information.
        /// </summary>
        
        public void LogInfo(string message, MethodBase method)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info($"[{method.DeclaringType.Name}.{method.Name}]:{message}");
            }
        }

        /// <summary>
        /// Logs the information.
        /// </summary>
        
        public void LogInfo(string message)
        {
            Log.Info(message);
        }

        /// <summary>
        /// Logs the warning.
        /// </summary>
        
        public void LogWarning(string message, MethodBase method)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Warn($"[{method.DeclaringType.Name}.{method.Name}]:{message}");
            }
        }

        /// <summary>
        /// Logs the warning.
        /// </summary>
       
        public void LogWarning(string message)
        {
            Log.Warn(message);
        }
    }
}
