using NUnit.Framework;
using PencilDurabilityKata;
using PencilDurabilityKata.Exceptions;
using Moq;
using PencilDurabilityKata.Interfaces;
using System;

namespace PencilDurabilityKataTests
{
    [TestFixture]
    class PencilTest
    {
        Mock<IPaper> paperMock;

        [SetUp]
        public void Setup()
        {
            paperMock = new Mock<IPaper>();
        }

        [Test]
        public void When_WriteWithoutPaper_ExpectNoPaperException()
        {
            var pencil = new Pencil(null, 0, 0, 0);
            Assert.Throws<NoPaperException>(() =>
            {
                pencil.Write(string.Empty);
            });
        }

        [Test]
        public void When_WriteWithPaper_ExpectNoException()
        {
            var pencil = new Pencil(paperMock.Object, 0, 0, 0);

            pencil.Write(string.Empty);

            Assert.Pass();
        }

        [Test]
        public void When_Write_ExpectAddTextToBeCalledWithTextToWrite()
        {
            var textToWrite = "test";
            var pencil = new Pencil(paperMock.Object, 4000, 0, 0);

            pencil.Write(textToWrite);

            paperMock.Verify(_paper => _paper.AddText(It.Is<string>(p => p == textToWrite)), Times.Once);
        }

        [Test]
        public void When_NewPencil_Expect_DurabilityToMatchValueInConstructor()
        {
            var durability = 40000;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_WriteEmpty_Expect_DurabilityToStayTheSame()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);

            pencil.Write(string.Empty);

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_WriteOneLowerCaseCharacter_Expect_DurabilityToDegradeByOne()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);

            pencil.Write("a");

            Assert.AreEqual(pencil.Durability, 3999);
        }

        [Test]
        public void When_WriteOneUpperCaseCharacter_Expect_DurabilityToDegradeByTwo()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);

            pencil.Write("A");

            Assert.AreEqual(pencil.Durability, 3998);
        }

        [Test]
        public void When_WriteOneUpperCaseCharacterAndOneLowerCaseCharacter_Expect_DurabilityToDegradeByThree()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);

            pencil.Write("Aa");

            Assert.AreEqual(pencil.Durability, 3997);
        }

        [Test]
        public void When_WriteSpace_Expect_DurabilityToStayTheSame()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);

            pencil.Write(" ");

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_WriteNewLine_Expect_DurabilityToStayTheSame()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);

            pencil.Write(Environment.NewLine);

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_WriteAndPencilHas0Durability_Expect_AddTextToBeCalledWithEmptySpacesSameLengthAsTextToWrite()
        {
            var textToWrite = "Test";

            var durability = 0;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);

            pencil.Write(textToWrite);

            paperMock.Verify(_paper => _paper.AddText(It.Is<string>(p => p == "    ")), Times.Once);
        }

        [Test]
        public void When_WriteAndPencilHas0DurabilityAndTextToWriteStill_Expect_DurabilityToNotGoBelow0()
        {
            var durability = 0;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);

            pencil.Write("Test");

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_WriteAndPencilReaches0DurabilityAndTextToWriteStill_Expect_PaperAddTextToBeCalledWithLast2CharactersInTextToBeEmpty()
        {
            var durability = 2;
            var pencil = new Pencil(paperMock.Object, durability, 0, 0);
            pencil.Write("test");

            paperMock.Verify(_paper => _paper.AddText(It.Is<string>(p => p == "te  ")), Times.Once);
        }

        [Test]
        public void When_Sharpen_Expect_DurabilityToReturnToStartingValue()
        {
            var startingDurability = 2;
            var pencil = new Pencil(paperMock.Object, startingDurability, 1, 0);
            pencil.Write("a");
            Assert.Less(pencil.Durability, startingDurability);
            pencil.Sharpen();
            Assert.AreEqual(pencil.Durability, startingDurability);
        }

        [Test]
        public void When_Sharpen_Expect_LengthIsReduced()
        {
            var durability = 3;
            var startingLength = 3;
            var pencil = new Pencil(paperMock.Object, durability, startingLength, 0);
            pencil.Sharpen();
            Assert.Less(pencil.Length, startingLength);
        }

        [Test]
        public void When_Sharpen_Expect_LengthToBeReducedByOne()
        {
            var startingLength = 3;
            var pencil = new Pencil(paperMock.Object, 0, startingLength, 0);
            pencil.Sharpen();
            Assert.AreEqual(pencil.Length, 2);
        }

        [Test]
        public void When_SharpenIsUsedTwice_Expect_LengthToBeReducedByTwo()
        {
            var startingLength = 3;
            var pencil = new Pencil(paperMock.Object, 0, startingLength, 0);
            pencil.Sharpen();
            pencil.Sharpen();
            Assert.AreEqual(pencil.Length, 1);
        }

        [Test]
        public void When_NoSharpen_Expect_LengthToBeOriginal()
        {
            var startingLength = 3;
            var pencil = new Pencil(paperMock.Object, 0, startingLength, 0);
            Assert.AreEqual(pencil.Length, startingLength);
        }

        [Test]
        public void When_SharpenAndPencilHas0Length_Expect_DurabilityToNotRestore()
        {
            var startingDurability = 5;
            var startingLength = 0;
            var pencil = new Pencil(paperMock.Object, startingDurability, startingLength, 0);
            pencil.Write("a");
            Assert.AreEqual(pencil.Durability, 4);
            pencil.Sharpen();
            Assert.AreEqual(pencil.Durability, 4);
        }

        [Test]
        public void When_Erase_Expect_PaperRemoveTextToBeCalledWithTextToBeErased()
        {
            var textToBeErased = "Pencil";

            var pencil = new Pencil(paperMock.Object, 100, 5, 100);
            pencil.Erase(textToBeErased);

            paperMock.Verify(_paper => _paper.RemoveText(It.Is<string>(p => p == textToBeErased)), Times.Once);
        }

        [Test]
        public void When_Erase_Expect_EraserDurabilityToDecrease()
        {
            var text = "Kata Pencil";
            var textToBeErased = "Pencil";

            var pencil = new Pencil(paperMock.Object, 100, 5, 100);
            pencil.Write(text);
            pencil.Erase(textToBeErased);
            Assert.AreEqual(pencil.EraserDurability, 94);
        }

        [Test]
        public void When_Erase_Expect_EraserDurabilityToDecreaseOnlyOnNonWhiteSpaceCharacters()
        {
            var text = "Erasing pencil test";
            var textToBeErased = " pencil test";

            var pencil = new Pencil(paperMock.Object, 100, 5, 100);
            pencil.Write(text);
            pencil.Erase(textToBeErased);
            Assert.AreEqual(pencil.EraserDurability, 90);
        }

        [Test]
        public void When_EraseAnd0Durability_Expect_RemoveTextToBeCalledWithAnEmptyString()
        {
            var textToErase = "fun";
            var pencil = new Pencil(paperMock.Object, 100, 5, 0);
            pencil.Erase(textToErase);

            paperMock.Verify(_paper => _paper.RemoveText(It.Is<string>(p => p == "")), Times.Once);
        }

        [Test]
        public void When_EraseAnd2Durability_Expect_RemoveTextToBeCalledWithLast2CharactersOfTextToErase()
        {
            var eraserDurability = 2;
            var textToErase = "test";

            var pencil = new Pencil(paperMock.Object, 100, 5, eraserDurability);
            pencil.Erase(textToErase);

            paperMock.Verify(_paper => _paper.RemoveText(It.Is<string>(p => p == "st")), Times.Once);
        }

        [Test]
        public void When_Edit_Expect_PaperToHaveEditTextCalledWithGivenEditText()
        {
            var editText = "edit";

            var pencil = new Pencil(paperMock.Object, 100, 5, 100);
            pencil.Edit(editText);

            paperMock.Verify(_paper => _paper.EditText(It.Is<string>(p => p == editText)), Times.Once);
        }
    }
}
