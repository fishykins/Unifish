using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNIFISH_NEWINPUT
using UnityEngine.InputSystem;
#endif

namespace Unifish
{
    public class UiConsoleData : MonoBehaviour
    {
        // Start is called before the first frame update
        [Header("UI Componenets")]
        public Canvas consoleCanvas;
        public ScrollRect scrollRect;
        public Text consoletext;
        public Text inputText;
        public InputField consoleInput;

#if UNIFISH_NEWINPUT
        public InputAction toggleConsole;
        public InputAction enterKey;

        private void OnEnable()
        {
            enterKey.Enable();
            toggleConsole.Enable();
        }

        private void OnDisable()
        {
            enterKey.Disable();
            toggleConsole.Disable();
        }
#endif
    }

}
