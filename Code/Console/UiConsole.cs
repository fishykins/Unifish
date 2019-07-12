using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNIFISH_NEWINPUT
using UnityEngine.InputSystem;
#endif


namespace Unifish
{
    public class UiConsole : IConsoleGui
    {
        private UiConsoleData data;

        public bool IsActive { get { return data.consoleCanvas.gameObject.activeInHierarchy; } }

        public void Initialize()
        {
            data = Console.instance.GetComponent<UiConsoleData>();

            if (data == null) Debug.LogError("UiConsoleData not found!");

            SetActive(false);

#if UNIFISH_NEWINPUT
            data.toggleConsole.performed += ToggleConsole;
#endif
        }

#if UNIFISH_NEWINPUT
        private void HitEnter(InputAction.CallbackContext context)
        {
            Console.instance.ParseCommand(data.inputText.text);
        }

        private void ToggleConsole(InputAction.CallbackContext context)
        {
            SetActive(!IsActive);
        }
#endif

        public void Log(string message) => AddTextToGameConsole(message);
        public void LogWarning(string message) => AddTextToGameConsole(message);
        public void LogError(string message) => AddTextToGameConsole(message);

        public void HandleLog(string message, string stackTrace, LogType type)
        {
            AddTextToGameConsole(message);
        }

        private void AddTextToGameConsole(string message)
        {
            if (data is null) return;
            data.consoletext.text += message + "\n";
            data.scrollRect.verticalNormalizedPosition = 0f;
        }

        public void SetActive(bool active = true)
        {
            data.consoleCanvas.gameObject.SetActive(active);

#if UNIFISH_NEWINPUT
            if (active)
                data.enterKey.performed += HitEnter;
            else
                data.enterKey.performed -= HitEnter;
#endif
        }



        public void Update()
        {

#if UNIFISH_NEWINPUT
            //Use the new input system
#else

            //Old Input system
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SetActive(!IsActive);
            }

            if (IsActive)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //Call input
                    Console.instance.ParseCommand(data.inputText.text);

                }

                if (Input.GetKeyUp(KeyCode.Return))
                    data.inputText.text = "";
            }
#endif
        }
    }

}
