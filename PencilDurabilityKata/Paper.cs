using PencilDurabilityKata.Interfaces;

namespace PencilDurabilityKata
{
    public class Paper: IPaper
    {
        public string Text = string.Empty;

        public void AddText(string text)
        {
            Text += text;
        }
    }
}
