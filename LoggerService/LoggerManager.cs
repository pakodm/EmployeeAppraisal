using System;
using NLog;

namespace LoggerService
{

    public interface ILoggerManager
    {
        void LogDebug(string Message);
        void LogInfo(string Message);
        void LogWarn(string Message);
        void LogError(string Message);
        void LogException(Exception ex);
    }

    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        private void GetErrorFromException(Exception ex)
        {
            logger.Warn(ex.Message);
            if (ex.InnerException != null)
                GetErrorFromException(ex.InnerException);
        }

        public void LogDebug(string Message)
        {
            logger.Debug(Message);
        }

        public void LogInfo(string Message)
        {
            logger.Info(Message);
        }

        public void LogWarn(string Message)
        {
            logger.Warn(Message);
        }

        public void LogError(string Message)
        {
            logger.Error(Message);
        }

        public void LogException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                GetErrorFromException(ex.InnerException);
            }
            else
            {
                logger.Warn(ex.Message);
            }
        }
    }
}
