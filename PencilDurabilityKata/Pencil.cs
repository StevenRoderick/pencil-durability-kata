using PencilDurabilityKata.Exceptions;
using PencilDurabilityKata.Interfaces;

namespace PencilDurabilityKata
{
    public class Pencil
    {
        private IPaper paper;
        public int Durability { get; }

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
        }
    }
}
