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
        public void When_RemoveText_Expect_TextToBeReplacedWithEmptySpaces()
        {
            var text = "Pencil Kata";
            var textToBeErased = "Kata";

            paper.AddText(text);
            Assert.AreEqual(paper.Text, text);
            paper.RemoveText(textToBeErased);
            Assert.AreEqual(paper.Text, "Pencil     ");
        }

        [Test]
        public void When_RemoveText_Expect_OnlyLastOccurenceOfTextToBeReplacedWithEmptySpaces()
        {
            var text = "Kata Pencil Kata";
            var textToBeErased = "Kata";

            paper.AddText(text);
            Assert.AreEqual(paper.Text, text);
            paper.RemoveText(textToBeErased);
            Assert.AreEqual(paper.Text, "Kata Pencil     ");
        }

        [Test]
        public void When_RemoveText_Expect_OnlyLastOccurenceOfTextToBeReplacedWithEmptySpaces_2()
        {
            var text = "Kata Pencil Kata";
            var textToBeErased = "Pencil";

            paper.AddText(text);
            Assert.AreEqual(paper.Text, text);
            paper.RemoveText(textToBeErased);
            Assert.AreEqual(paper.Text, "Kata        Kata");
        }

        [Test]
        public void When_EditText_Expect_TextToGoIntoLastRemovedText()
        {
            var text = "Remove text test";
            var textToRemove = "test";
            var editText = "edit";

            var paper = new Paper();

            paper.AddText(text);
            paper.RemoveText(textToRemove);
            paper.EditText(editText);

            Assert.AreEqual(paper.Text, "Remove text edit");
        }

        [Test]
        public void When_EditText_Expect_TextToGoIntoLastRemovedText_2()
        {
            var text = "Remove text test";
            var textToRemove = "text";
            var editText = "edit";

            var paper = new Paper();

            paper.AddText(text);
            paper.RemoveText(textToRemove);
            paper.EditText(editText);

            Assert.AreEqual(paper.Text, "Remove edit test");
        }

        [Test]
        public void When_EditTextIsLongerThanErasedText_Expect_OverridingTextToBeAnAtSymbol()
        {
            var text = "Remove text test with edit";
            var textToRemove = "text";
            var editText = "edited text";

            var paper = new Paper();

            paper.AddText(text);
            paper.RemoveText(textToRemove);
            paper.EditText(editText);

            Assert.AreEqual(paper.Text, "Remove edite@e@@x@ith edit");

            //"Remove text test with edit";
            //        edited text
        }

    }
}