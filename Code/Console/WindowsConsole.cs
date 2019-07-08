using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Unifish
{
    public class WindowsConsole : IConsole
    {
        private WindowsConsoleInput windowsInput;

        public bool IsActive { get { return true; } }

        public void Initialize()
        {
            windowsInput = new WindowsConsoleInput();
        }

        public void Log(string message) => HandleLog(message, "", LogType.Log);
        public void LogWarning(string message) => HandleLog(message, "", LogType.Warning);
        public void LogError(string message) => HandleLog(message, "", LogType.Error);

        public void HandleLog(string message, string stackTrace, LogType type)
        {
            if (type == LogType.Warning)
                System.Console.ForegroundColor = ConsoleColor.Yellow;
            else if (type == LogType.Error)
                System.Console.ForegroundColor = ConsoleColor.Red;
            else
                System.Console.ForegroundColor = ConsoleColor.Blue;

            // If we were typing something re-add it.
            windowsInput.RedrawInputLine();

            System.Console.WriteLine(message);

            //reset colour
            System.Console.ForegroundColor = ConsoleColor.White;
        }

        public void Update()
        {
            windowsInput.Update();
        }
    }

}
