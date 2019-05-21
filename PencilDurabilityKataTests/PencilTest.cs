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
            var pencil = new Pencil(null, 0);
            Assert.Throws<NoPaperException>(() =>
            {
                pencil.Write(string.Empty);
            });
        }

        [Test]
        public void When_WriteWithPaper_ExpectNoException()
        {
            var paperMock = new Mock<IPaper>();
            var pencil = new Pencil(paperMock.Object, 0);
            pencil.Write(string.Empty);
            Assert.Pass();
        }

        [Test]
        public void When_WriteWithText_ExpectPaperToHaveSameText()
        {
            var textToWrite = "test";
            var paper = new Paper();
            var pencil = new Pencil(paper, 0);
            pencil.Write(textToWrite);
            Assert.AreEqual(paper.Text, textToWrite);
        }

        [Test]
        public void When_WriteWithTextOnPaperWithExistingText_Expect_NewTextToBeAppendedToExistingText()
        {
            var existingText = "existing text";
            var paper = new Paper();
            paper.AddText(existingText);

            var newText = "new text";
            var pencil = new Pencil(paper, 0);
            pencil.Write(newText);

            Assert.AreEqual(paper.Text, $"{existingText}{newText}");
        }

        [Test]
        public void When_NewPencil_Expect_DurabilityToMatchValueInConstructor()
        {
            var durability = 40000;
            var paperMock = new Mock<IPaper>();
            var pencil = new Pencil(paperMock.Object, durability);
            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_Write_Expect_DurabilityToDegrade()
        {
            var durability = 4000;
            var paperMock = new Mock<IPaper>();
            var pencil = new Pencil(paperMock.Object, durability);
            pencil.Write(string.Empty);
            Assert.Less(pencil.Durability, durability);
        }
    }
}
