using PencilDurabilityKata.Interfaces;

namespace PencilDurabilityKata
{
    public class Paper: IPaper
    {
        public string Text { get; private set; }

        public Paper()
        {
            Text = string.Empty;
        }

        public void AddText(string text)
        {
            Text += text;
        }

        public void RemoveText(string text)
        {
            Text = Text.Replace(text, new string(' ', text.Length));
        }
    }
}
