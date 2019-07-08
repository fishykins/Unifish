using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unifish
{
    public class Console : MonoBehaviour
    {
        public static Console instance;
        private IConsole console;

        [Header("Python Commands")]
        public bool python = true;
        public String[] assemblies = new string[0];

        private void Awake()
        {
            if (instance != null) return;

            instance = this;

#if UNITY_SERVER
            console = new WindowsConsole();
#else
            console = new UiConsole();

#endif

            Application.logMessageReceived += console.HandleLog;
        }

        private void Start()
        {
            console.Initialize();
            Log(StringFormatter.WrapHeader("Unifish Console Initiated"));
        }

        public void Update()
        {
            console.Update();
        }

        public static void Log(string message) => instance.console.Log(message);
        public static void LogWarning(string message) => instance.console.LogWarning(message);
        public static void LogError(string message) => instance.console.LogError(message);

        /// <summary>
        /// Takes a given string and attempts to do something with it!
        /// </summary>
        /// <param name="command"></param>
        public void ParseCommand(string command)
        {

        }

        public void AddCommand()
        {

        }
    }
}

