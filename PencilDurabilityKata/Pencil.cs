using PencilDurabilityKata.Exceptions;
using PencilDurabilityKata.Interfaces;

namespace PencilDurabilityKata
{
    public class Pencil
    {
        private IPaper paper;

        public Pencil(IPaper paper)
        {
            this.paper = paper;
        }

        public void Write()
        {
            if (paper == null)
            {
                throw new NoPaperException();
            }
        }
    }
}
