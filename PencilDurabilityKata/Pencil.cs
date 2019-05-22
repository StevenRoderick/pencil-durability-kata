using PencilDurabilityKata.Exceptions;
using PencilDurabilityKata.Interfaces;
using System;
using System.Text;

namespace PencilDurabilityKata
{
    public class Pencil
    {
        private IPaper paper;
        public int Durability { get; private set; }
        public int Length { get; private set; }

        private const char dullPencilCharacter = ' ';
        private readonly int startingDurability;

        public Pencil(IPaper paper, int durability, int length)
        {
            this.paper = paper;
            startingDurability = durability;
            Durability = durability;
            Length = length;
        }

        public void Write(string text)
        {
            if (paper == null)
            {
                throw new NoPaperException();
            }

            var printedText = Print(text);

            paper.AddText(printedText);
        }

        public void Sharpen()
        {
            if (Length > 0)
            {
                Durability = startingDurability;
                Length--;
            }
        }

        private string Print(string text)
        {
            var stringBuilder = new StringBuilder();

            foreach (var character in text)
            {
                var nextCharacter = Durability == 0 ? dullPencilCharacter : character;
                stringBuilder.Append(nextCharacter);
                DegradeDurability(character);
            }

            return stringBuilder.ToString();
        }

        private void DegradeDurability(char character)
        {
            if (char.IsUpper(character))
            {
                Durability -= 2;
            }
            else if (char.IsLower(character))
            {
                Durability--;
            }

            Durability = Math.Clamp(Durability, 0, int.MaxValue);
        }
    }
}
