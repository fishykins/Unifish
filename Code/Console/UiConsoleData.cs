using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

}
