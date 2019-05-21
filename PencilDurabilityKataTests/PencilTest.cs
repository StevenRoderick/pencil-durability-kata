using NUnit.Framework;
using PencilDurabilityKata;
using PencilDurabilityKata.Exceptions;

namespace PencilDurabilityKataTests
{
    [TestFixture]
    class PencilTest
    {
        [Test]
        public void When_WriteWithoutPaper_ExpectNoPaperException()
        {
            var pencil = new Pencil();
            Assert.Throws<NoPaperException>(() =>
            {
                pencil.Write();
            });
        }
    }
}
