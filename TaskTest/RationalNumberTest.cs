using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task3_1;

namespace TaskTest
{
    [TestFixture]
    public class RationalNumberTest
    {
        [Test]
        public void CreateNotReduceRationalNumber()
        {
            var number = new RationalNumber(4,8);
            Assert.That(number.Denominator, Is.Positive);
            Assert.That(number.Numerator, Is.EqualTo(1));
            Assert.That(number.Denominator, Is.EqualTo(2));
        }

        [Test]
        public void CreateNaturalNumber()
        {
            var number = new RationalNumber(1);
            Assert.That(number.Denominator, Is.Positive);
            Assert.That(number.Numerator, Is.EqualTo(1));
            Assert.That(number.Denominator, Is.EqualTo(1));
        }

        [Test]
        public void CreateReduceRationalNumber()
        {
            var number = new RationalNumber(3, 7);
            Assert.That(number.Denominator, Is.Positive);
            Assert.That(number.Numerator, Is.EqualTo(3));
            Assert.That(number.Denominator, Is.EqualTo(7));
        }

        [Test]
        public void CreateRationalNumberWithNegativeDenominator()
        {
            var number = new RationalNumber(3, -7);
            Assert.That(number.Denominator, Is.Positive);
            Assert.That(number.Numerator, Is.EqualTo(-3));
            Assert.That(number.Denominator, Is.EqualTo(7));
        }

        [Test]
        public void CompareRationalWithAnotherObject()
        {

            Assert.Throws(typeof (ArgumentException), () =>
            {
                var number = new RationalNumber(3, 7);
                var obj = new DateTime();
                number.CompareTo(obj);
            });
        }

        [Test]
        public void CompareTwoRational()
        {
            var numberA = new RationalNumber(3, 7);
            var numberB = new RationalNumber(1, 4);
            Assert.That(numberA.CompareTo(numberB), Is.Positive);
            Assert.That(numberB.CompareTo(numberA), Is.Negative);
            Assert.That(numberB.CompareTo(numberB), Is.EqualTo(0));
        }

        [Test]
        public void EqualsTwoRational()
        {
            var numberA = new RationalNumber(3, 7);
            var numberB = new RationalNumber(1, 4);
            var numberC = new RationalNumber(3, 12);
            Assert.That(numberA.Equals(numberB), Is.False);
            Assert.That(numberB.Equals(numberB), Is.True);
            Assert.That(numberB.Equals(numberC), Is.True);
            Assert.That(numberB.Equals(null), Is.False);
        }

        [Test]
        public void EqualsRationalWithAnotherObject()
        {
            Assert.Throws(typeof(ArgumentException), () =>
            {
                var number = new RationalNumber(3, 7);
                var obj = new DateTime();
                number.Equals(obj);
            });
        }

        [Test]
        public void ParseReducePositiveRational()
        {
            var number = RationalNumber.Parse("1/8");
            Assert.That(number.Numerator, Is.EqualTo(1));
            Assert.That(number.Denominator, Is.EqualTo(8));
        }

        [Test]
        public void ParseReduceNegativeRational()
        {
            var number = RationalNumber.Parse("-1/8");
            Assert.That(number.Numerator, Is.EqualTo(-1));
            Assert.That(number.Denominator, Is.EqualTo(8));
        }

        [Test]
        public void ParseReduceRationalWithNegativeDenominator()
        {
            Assert.Throws(typeof (FormatException), () =>
            {
                var number = RationalNumber.Parse("1/-8");
            });
        }

        [Test]
        public void ParseReduceRationalWithZeroDenominator()
        {
            Assert.Throws(typeof(DivideByZeroException), () =>
            {
                var number = RationalNumber.Parse("1/0");
            });
        }
    }
}
