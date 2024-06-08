using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.LoggerManager
{
    public interface ILoggerManager
    {
        void LogInformation(string message);
        void LogDebug(string message);        void LogWarning(string message);
        void LogError(object message, Exception exception);
        void LogError(string message, Exception exception);
        void LogError(string message);
    }
}
