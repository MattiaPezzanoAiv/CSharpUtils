using MP.CSharpUtilities.Extensions;
using NUnit.Framework;

namespace CSharpUtilitiesTest
{
    [TestFixture]
    public class ExtensionsTest
    {
        [Test]
        public void AlmostEqualTest()
        {
            float num = 5f;
            Assert.That(num.AlmostEqual(5.0001f), Is.True);
            Assert.That(num.AlmostEqual(5.1f), Is.False);
        }
    }
}
