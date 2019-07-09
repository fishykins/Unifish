using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.Scripting.Hosting;
using Exodrifter.UnityPython;
using System.Collections;
using IronPython.Hosting;

namespace Unifish
{
    public class Console : MonoBehaviour
    {
        public static Console instance;
        private IConsoleGui console;

        public static bool IsOpen { get { return instance.console.IsActive; } }

        public delegate void ConsoleCommand(string message);
        private Dictionary<string, ConsoleCommand> consoleCommands = new Dictionary<string, ConsoleCommand>();

        [Header("Python Commands")]
        [SerializeField]
        private bool usePython = true;
        [SerializeField]
        private String[] assemblies = { "Unifish" };
        [SerializeField]
        private string pythonTag = "py";

        private ScriptEngine pythonEngine;
        private ScriptScope pythonScope;

        private void Awake()
        {
            if (instance != null) return;

            instance = this;
            pythonEngine = UnityPython.CreateEngine(assemblies);
            pythonScope = pythonEngine.CreateScope();


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

            string code = "import UnityEngine\nUnityEngine.Debug.Log('IronPython initiated')";

            var source = pythonEngine.CreateScriptSourceFromString(code);
            source.Execute(pythonScope);
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
            string[] stringArray = command.Split(null);
            if (stringArray.Length == 0 || stringArray == null)
            {
                LogError("Command '" + command + "' is a bit broken");
                return;
            }

            string type = stringArray[0].ToLower();
            string arg = (stringArray.Length >= 2) ? command.Substring(type.Length + 1) : "";

            if (type == pythonTag && usePython && pythonEngine != null)
            {
                Log("Parsing '" + arg + "' as Python...");
                var source = pythonEngine.CreateScriptSourceFromString(arg);
                source.Execute(pythonScope);
            }
            else
            {
                if (consoleCommands.TryGetValue(type, out ConsoleCommand comm))
                {
                    comm.Invoke(arg);
                }
                else
                {
                    Log("No command for '" + type + "' was found");
                }
            }
        }

        public static void AddCommand(string name, ConsoleCommand command)
        {
            name = name.ToLower();
            if (!instance.consoleCommands.ContainsKey(name))
                instance.consoleCommands.Add(name, command);
        }

        public void ShowHelp(string arg)
        {
            foreach (var command in Console.instance.consoleCommands)
            {
                Console.Log("--" + command.Key);
            }
        }
    }
}

