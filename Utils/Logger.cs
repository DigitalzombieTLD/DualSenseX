using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace DualSenseX.Utils.Logging
{
    public static class ZombieLog
    {
        private static MelonLogger.Instance _loggerInstance;

        public static void SetLoggerInstance(MelonLogger.Instance loggerInstance)
        {
            _loggerInstance = loggerInstance;
        }

        public static void Log(string logText) 
        {
            _loggerInstance.Msg(logText);
        }
        public static void LogError(string logText)
        {
            _loggerInstance.Msg("[ERROR] " + logText);
        }

        public static void LogWarning(string logText)
        {
            _loggerInstance.Msg("[WARNING] " + logText);
        }

        public static void LogInfo(string logText)
        {
            _loggerInstance.Msg("[INFO] " + logText);
        }
    }
}
