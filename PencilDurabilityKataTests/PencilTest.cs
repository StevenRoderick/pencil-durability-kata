using NUnit.Framework;
using PencilDurabilityKata;
using PencilDurabilityKata.Exceptions;
using Moq;
using PencilDurabilityKata.Interfaces;

namespace PencilDurabilityKataTests
{
    [TestFixture]
    class PencilTest
    {
        [Test]
        public void When_WriteWithoutPaper_ExpectNoPaperException()
        {
            var pencil = new Pencil(null);
            Assert.Throws<NoPaperException>(() =>
            {
                pencil.Write();
            });
        }

        [Test]
        public void When_WriteWithPaper_ExpectNoException()
        {
            var paperMock = new Mock<IPaper>();
            var pencil = new Pencil(paperMock.Object);
            pencil.Write();
            Assert.Pass();
        }
    }
}
