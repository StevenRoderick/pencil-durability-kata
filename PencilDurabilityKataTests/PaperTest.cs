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
    }
}