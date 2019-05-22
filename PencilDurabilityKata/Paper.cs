using PencilDurabilityKata.Interfaces;
using System.Text;
using System.Linq;

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
            var editText = Text.Substring(indexOfLastRemovedText, text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                var editTextChar = editText[i];
                var newTextChar = text[i];

                if (!char.IsWhiteSpace(newTextChar))
                {
                    editText = editText.Remove(i, 1);
                    var newCharacter = char.IsWhiteSpace(editTextChar) ? text[i] : '@';
                    editText = editText.Insert(i, newCharacter.ToString());
                }
            }

            Text = Text.Remove(indexOfLastRemovedText, text.Length);
            Text = Text.Insert(indexOfLastRemovedText, editText);
        }
    }
}
