using PencilDurabilityKata.Interfaces;

namespace PencilDurabilityKata
{
    public class Paper: IPaper
    {
        public string Text { get; private set; }

        private int indexOfLastRemovedText;
        private int lengthOfLastRemovedText;

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
            var lastIndex = Text.LastIndexOf(text);
            Text = Text.Remove(lastIndex, text.Length);
            Text = Text.Insert(lastIndex, new string(' ', text.Length));

            indexOfLastRemovedText = lastIndex;
            lengthOfLastRemovedText = text.Length;
        }

        public void EditText(string text)
        {
            Text = Text.Remove(indexOfLastRemovedText, lengthOfLastRemovedText);
            Text = Text.Insert(indexOfLastRemovedText, text);
        }
    }
}
