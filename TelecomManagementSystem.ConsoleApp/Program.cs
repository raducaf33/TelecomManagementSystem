using TelecomManagement.Data.Logger;
using TelecomManagement.Domain;
using TelecomManagement.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using log4net;
using log4net.Config;
using System.Reflection;

namespace TelecomManagementSystem
{

    class Program
    {

        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            ILogger logger = new Logger();
            logger.LogInfo("Aplicația a pornit.");

            // Apelează logurile din Data
            logger.LogInfo("Mesaj de log din Data.");

            // Restul codului tău
            Console.WriteLine("Salut lume!");
        }
    }
}





