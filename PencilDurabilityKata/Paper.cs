using System;
using System.Collections.Generic;
using System.Text;

namespace PencilDurabilityKata
{
    public class Paper
    {
        public string Text = string.Empty;

        public void AddText(string text)
        {
            Text = text;
        }
    }
}
