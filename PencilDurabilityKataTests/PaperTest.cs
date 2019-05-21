using NUnit.Framework;
using PencilDurabilityKata;

namespace Tests
{
    public class PaperTests
    {
        [Test]
        public void When_NewPaper_Expect_TextIsEmpty()
        {
            var paper = new Paper();
            Assert.IsEmpty(paper.Text);
        }

        [Test]
        public void When_AddText_Expect_PaperToHaveSameText()
        {
            var textToAdd = "new text";
            var paper = new Paper();
            paper.AddText(textToAdd);

            Assert.AreEqual(paper.Text, textToAdd);
        }

        [Test]
        public void When_AddTextTwiceWithDifferentText_Expect_PaperToHaveAllText()
        {
            var firstText = "first text";
            var secondText = "second text";

            var paper = new Paper();
            paper.AddText(firstText);
            paper.AddText(secondText);

            Assert.AreEqual(paper.Text, $"{firstText}{secondText}");
        }
    }
}