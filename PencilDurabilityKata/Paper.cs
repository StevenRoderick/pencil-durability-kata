using PencilDurabilityKata.Interfaces;
using System.Text;
using System.Linq;
using PencilDurabilityKata.Exceptions;

namespace PencilDurabilityKata
{
    public class Paper: IPaper
    {
        public string Text { get; private set; }

        private int? indexOfLastRemovedText;

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
            ReplaceText(lastIndex, text.Length, new string(' ', text.Length));
            indexOfLastRemovedText = lastIndex;
        }

        public void EditText(string editText)
        {
            if (!indexOfLastRemovedText.HasValue)
            {
                throw new NoErasedTextException();
            }

            var snapShotLength = Text.Length - indexOfLastRemovedText.Value;
            var subStringLength = snapShotLength > editText.Length ? editText.Length : snapShotLength;

            var snapShotOfOriginalText = Text.Substring(indexOfLastRemovedText.Value, subStringLength);

            var editExtensionLength = editText.Length - subStringLength;
            snapShotOfOriginalText += new string(' ', editExtensionLength);

            for (int i = 0; i < editText.Length; i++)
            {
                var originalTextChar = snapShotOfOriginalText[i];
                var newTextChar = editText[i];

                if (!char.IsWhiteSpace(newTextChar))
                {
                    ReplaceCharacterInOriginalText(newTextChar, ref snapShotOfOriginalText, i, originalTextChar);
                }
            }

            ReplaceText(indexOfLastRemovedText.Value, subStringLength, snapShotOfOriginalText);
        }

        private static void ReplaceCharacterInOriginalText(char newTextChar, ref string originalText, int i, char originalTextChar)
        {
            originalText = originalText.Remove(i, 1);
            var newCharacter = char.IsWhiteSpace(originalTextChar) ? newTextChar : '@';
            originalText = originalText.Insert(i, newCharacter.ToString());
        }

        private void ReplaceText(int index, int length, string newText)
        {
            Text = Text.Remove(index, length);
            Text = Text.Insert(index, newText);
        }
    }
}
