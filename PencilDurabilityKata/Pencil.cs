using PencilDurabilityKata.Exceptions;

namespace PencilDurabilityKata
{
    public class Pencil
    {
        public void Write()
        {
            throw new NoPaperException();
        }
    }
}
