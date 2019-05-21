using PencilDurabilityKata.Exceptions;
using PencilDurabilityKata.Interfaces;
using System.Linq;

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

            paper.AddText(text);
            if (text.Any(char.IsUpper))
            {
                Durability -= 2;
            }
            else
            {
                Durability--;
            }
        }
    }
}
