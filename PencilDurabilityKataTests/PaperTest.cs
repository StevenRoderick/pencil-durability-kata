using NUnit.Framework;
using PencilDurabilityKata;

namespace Tests
{
    [TestFixture]
    public class PaperTests
    {
        private Paper paper;

        [SetUp]
        public void Setup()
        {
            paper = new Paper();
        }

        [Test]
        public void When_NewPaper_Expect_TextIsEmpty()
        {
            Assert.IsEmpty(paper.Text);
        }

        [Test]
        public void When_AddText_Expect_PaperToHaveSameText()
        {
            var textToAdd = "new text";
            paper.AddText(textToAdd);

            Assert.AreEqual(paper.Text, textToAdd);
        }

        [Test]
        public void When_AddTextTwiceWithDifferentText_Expect_PaperToHaveAllText()
        {
            var firstText = "first text";
            var secondText = "second text";

            paper.AddText(firstText);
            paper.AddText(secondText);

            Assert.AreEqual(paper.Text, $"{firstText}{secondText}");
        }

        [Test]
        public void When_RemoveText_Expect_TextToBeEmptySpaces()
        {
            var text = "Pencil Kata";
            var textToBeErased = "Kata";

            paper.AddText(text);
            Assert.AreEqual(paper.Text, text);
            paper.RemoveText(textToBeErased);
            Assert.AreEqual(paper.Text, "Pencil     ");
        }

        [Test]
        public void When_RemoveText_Expect_OnlyLastOccurenceOfTextToBeRemoved()
        {
            var text = "Kata Pencil Kata";
            var textToBeErased = "Kata";

            paper.AddText(text);
            Assert.AreEqual(paper.Text, text);
            paper.RemoveText(textToBeErased);
            Assert.AreEqual(paper.Text, "Kata Pencil     ");
        }
    }
}