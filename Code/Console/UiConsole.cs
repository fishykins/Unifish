using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unifish
{
    public class UiConsole : IConsole
    {
        private UiConsoleData data;

        public bool IsActive { get { return data.consoleCanvas.gameObject.activeInHierarchy; } }

        public void Initialize()
        {
            data = Console.instance.GetComponent<UiConsoleData>();
            
            if (data == null) Debug.LogError("UiConsoleData not found!");

            data.consoleCanvas.gameObject.SetActive(false);
        }

        public void Log(string message) => AddTextToGameConsole(message);
        public void LogWarning(string message) => AddTextToGameConsole(message);
        public void LogError(string message) => AddTextToGameConsole(message);

        public void HandleLog(string message, string stackTrace, LogType type)
        {
            AddTextToGameConsole(message);
        }

        public void AddTextToGameConsole(string message)
        {
            data.consoletext.text += message + "\n";
            data.scrollRect.verticalNormalizedPosition = 0f;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                data.consoleCanvas.gameObject.SetActive(!IsActive);
            }

            if (IsActive)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Call input
                    Log(data.inputText.text);
                    data.inputText.text = "";
                }
            }
        }
    }

}
