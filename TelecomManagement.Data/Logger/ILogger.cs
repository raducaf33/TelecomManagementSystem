using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TelecomManagement.Data.Logger
{
    

    /// <summary>
    /// ILogger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the error.
        /// </summary>
        
        void LogError(string message, MethodBase method);

        /// <summary>
        /// Logs the error.
        /// </summary>
        
        void LogError(string message);

        /// <summary>
        /// Logs the information.
        /// </summary>
        
        void LogInfo(string message, MethodBase method);

        /// <summary>
        /// Logs the information.
        /// </summary>
        
        void LogInfo(string message);

        /// <summary>
        /// Logs the warning.
        /// </summary>
        
        void LogWarning(string message, MethodBase method);

        /// <summary>
        /// Logs the warning.
        /// </summary>
        
        void LogWarning(string message);
    }
}
