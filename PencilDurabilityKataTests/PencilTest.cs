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
                pencil.Write(string.Empty);
            });
        }

        [Test]
        public void When_WriteWithPaper_ExpectNoException()
        {
            var paperMock = new Mock<IPaper>();
            var pencil = new Pencil(paperMock.Object);
            pencil.Write(string.Empty);
            Assert.Pass();
        }

        [Test]
        public void When_WriteWithText_ExpectPaperToHaveSameText()
        {
            var textToWrite = "test";
            var paper = new Paper();
            var pencil = new Pencil(paper);
            pencil.Write(textToWrite);
            Assert.AreEqual(paper.Text, textToWrite);
        }
    }
}
