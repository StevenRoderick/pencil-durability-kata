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

        public Pencil(IPaper paper, int durability)
        {
            this.paper = paper;
            Durability = durability;
        }

        public void Write(string text)
        {
            if (paper == null)
            {
                throw new NoPaperException();
            }

            var stringBuilder = new StringBuilder();

            foreach (var character in text)
            {
                DegradeDurability(character);

                var nextCharacter = Durability == 0 ? ' ' : character;
                stringBuilder.Append(nextCharacter);
            }

            paper.AddText(stringBuilder.ToString());
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
