using System;
using NUnit.Framework;
using Task3_1;

namespace TaskTest
{
    [TestFixture]
    public class MathHelperTest
    {
        [TestCase(1, 7, ExpectedResult = 1)]
        [TestCase(0, 7, ExpectedResult = 7)]
        [TestCase(2, 6, ExpectedResult = 2)]
        public long GetGsdTest(long a, long b)
        {
            return MathHelper.GetGsd(a, b);
        }

        [TestCase(1, 7, ExpectedResult = 7)]
        [TestCase(2, 3, ExpectedResult = 6)]
        [TestCase(2, 6, ExpectedResult = 6)]
        public long GetLcmTest(long a, long b)
        {
            return MathHelper.GetLcm(a, b);
        }
    }
}
