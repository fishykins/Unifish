using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unifish
{
    public static class StringFormatter
    {
        private const string lineDivider = "\n============================================\n";

        public static string LineDivider => lineDivider;

        public static string GetLine(char character, int count = 50) {
            string text = "";
            for (int i = 0; i < count; i++)
            {
                text += character;
            }
            return text;
        }

        public static string WrapHeader(string text) {
            return LineDivider + text + LineDivider;
        }
    }
}

