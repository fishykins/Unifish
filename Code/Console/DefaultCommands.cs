using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unifish
{
    public class DefaultCommands : MonoBehaviour
    {
        void Start()
        {
            Console.AddCommand("Goon", Goon);
            Console.AddCommand("Exit", Exit);
            Console.AddCommand("Help", Console.instance.ShowHelp);
        }

        private void Goon(string arg)
        {
            if (arg == "") arg = "You";
            Console.Log(arg + " is a goon");
        }

        private void Exit(string arg)
        {
            Application.Quit();
        }
    }
}

