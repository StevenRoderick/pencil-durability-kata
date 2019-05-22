﻿using NUnit.Framework;
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
            var pencil = new Pencil(null, 0, 0);
            Assert.Throws<NoPaperException>(() =>
            {
                pencil.Write(string.Empty);
            });
        }

        [Test]
        public void When_WriteWithPaper_ExpectNoException()
        {
            var pencil = new Pencil(paperMock.Object, 0, 0);

            pencil.Write(string.Empty);

            Assert.Pass();
        }

        [Test]
        public void When_WriteWithText_ExpectPaperToHaveSameText()
        {
            var textToWrite = "test";
            var paper = new Paper();
            var pencil = new Pencil(paper, 4000, 0);

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
            var pencil = new Pencil(paper, 4000, 0);

            pencil.Write(newText);

            Assert.AreEqual(paper.Text, $"{existingText}{newText}");
        }

        [Test]
        public void When_NewPencil_Expect_DurabilityToMatchValueInConstructor()
        {
            var durability = 40000;
            var pencil = new Pencil(paperMock.Object, durability, 0);

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_WriteEmpty_Expect_DurabilityToStayTheSame()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0);

            pencil.Write(string.Empty);

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_WriteOneLowerCaseCharacter_Expect_DurabilityToDegradeByOne()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0);

            pencil.Write("a");

            Assert.AreEqual(pencil.Durability, 3999);
        }

        [Test]
        public void When_WriteOneUpperCaseCharacter_Expect_DurabilityToDegradeByTwo()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0);

            pencil.Write("A");

            Assert.AreEqual(pencil.Durability, 3998);
        }

        [Test]
        public void When_WriteOneUpperCaseCharacterAndOneLowerCaseCharacter_Expect_DurabilityToDegradeByThree()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0);

            pencil.Write("Aa");

            Assert.AreEqual(pencil.Durability, 3997);
        }

        [Test]
        public void When_WriteSpace_Expect_DurabilityToStayTheSame()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0);

            pencil.Write(" ");

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_WriteNewLine_Expect_DurabilityToStayTheSame()
        {
            var durability = 4000;
            var pencil = new Pencil(paperMock.Object, durability, 0);

            pencil.Write(Environment.NewLine);

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_PencilHas0Durability_Expect_PaperTextToBeSpaceCharacters()
        {
            var durability = 0;
            var paper = new Paper();
            var pencil = new Pencil(paper, durability, 0);

            pencil.Write("Test");

            Assert.AreEqual(paper.Text, "    ");
        }

        [Test]
        public void When_PencilHas0DurabilityAndTextToWriteStill_Expect_DurabilityToNotGoBelow0()
        {
            var durability = 0;
            var pencil = new Pencil(paperMock.Object, durability, 0);

            pencil.Write("Test");

            Assert.AreEqual(pencil.Durability, durability);
        }

        [Test]
        public void When_PencilReaches0DurabilityAndTextToWriteStill_Expect_RemainingTextToWriteToBeSpaces()
        {
            var durability = 2;
            var paper = new Paper();
            var pencil = new Pencil(paper, durability, 0);

            pencil.Write("test");

            Assert.AreEqual(paper.Text, "te  ");
        }

        [Test]
        public void When_PencilIsSharpened_Expect_DurabilityToReturnToStartingValue()
        {
            var startingDurability = 2;
            var pencil = new Pencil(paperMock.Object, startingDurability, 1);
            pencil.Write("a");
            Assert.Less(pencil.Durability, startingDurability);
            pencil.Sharpen();
            Assert.AreEqual(pencil.Durability, startingDurability);
        }

        [Test]
        public void When_PencilIsSharpened_Expect_LengthIsReduced()
        {
            var durability = 3;
            var startingLength = 3;
            var pencil = new Pencil(paperMock.Object, durability, startingLength);
            pencil.Sharpen();
            Assert.Less(pencil.Length, startingLength);
        }

        [Test]
        public void When_PencilIsSharpened_Expect_LengthToBeReducedByOne()
        {
            var startingLength = 3;
            var pencil = new Pencil(paperMock.Object, 0, startingLength);
            pencil.Sharpen();
            Assert.AreEqual(pencil.Length, 2);
        }

        [Test]
        public void When_PencilIsSharpenedTwice_Expect_LengthToBeReducedByTwo()
        {
            var startingLength = 3;
            var pencil = new Pencil(paperMock.Object, 0, startingLength);
            pencil.Sharpen();
            pencil.Sharpen();
            Assert.AreEqual(pencil.Length, 1);
        }

        [Test]
        public void When_PencilIsNotSharpened_Expect_LengthToBeOriginal()
        {
            var startingLength = 3;
            var pencil = new Pencil(paperMock.Object, 0, startingLength);
            Assert.AreEqual(pencil.Length, startingLength);
        }

        [Test]
        public void When_PencilHas0Length_Expect_DurabilityToNotRestore()
        {
            var startingDurability = 5;
            var startingLength = 0;
            var pencil = new Pencil(paperMock.Object, startingDurability, startingLength);
            pencil.Write("a");
            Assert.AreEqual(pencil.Durability, 4);
            pencil.Sharpen();
            Assert.AreEqual(pencil.Durability, 4);
        }
    }
}
