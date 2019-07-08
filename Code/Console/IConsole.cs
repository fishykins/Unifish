using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unifish
{
    public interface IConsole
    {
        bool IsActive { get; }

        void Initialize();
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
        void HandleLog(string message, string stackTrace, LogType type);

        void Update();
    }

}
