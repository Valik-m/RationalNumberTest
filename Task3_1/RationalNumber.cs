using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_1

{
    public class RationalNumber : IEquatable<RationalNumber>, IComparable, IComparable<RationalNumber>

    {
        public long Numerator { get; }

        public long Denominator { get; }

        public RationalNumber(long numerator, long denominator = 1)
        {
            if (denominator == 0) throw new DivideByZeroException();
            GetReduceFraction(ref numerator, ref denominator);
            if (denominator < 0)
            {
                Numerator = 0 - numerator;
                Denominator = Math.Abs(denominator);
            }
            else
            {
                Numerator = numerator;
                Denominator = denominator;
            }
        }

        public int CompareTo(object obj)
        {
            if (!(obj is RationalNumber))
                throw new ArgumentException("Object is not a RationalNumber");
            var other = (RationalNumber) obj;
            return CompareTo(other);
        }

        public int CompareTo(RationalNumber other)
        {
            var lcm = MathHelper.GetLcm(Denominator, other.Denominator);
            return Equals(other) ? 0 : (Numerator*lcm/Denominator).CompareTo(other.Numerator*lcm/other.Denominator);
        }

        public bool Equals(RationalNumber other)
        {
            return !ReferenceEquals(other, null) && (Numerator == other.Numerator && Denominator == other.Denominator);
        }

        public override string ToString()
        {
            return ToString("D");
        }

        public string ToString(string format)
        {
            switch (format.ToUpper())
            {
                case "D":
                    if (Numerator == 0)
                        return "0";
                    return Denominator == 1 ? $"{Numerator}" : $"{Numerator}/{Denominator}";
                case "F":
                    return $"{GetPeriod(this)}";
                default:
                    throw new FormatException("Wrong format");
            }
        }

        public static RationalNumber Parse(string value)
        {
            var splitValue = value.Split('/');
            if (splitValue.Length == 1)
                return new RationalNumber(Convert.ToInt64(splitValue[0]));
            if (splitValue.Length > 2)
                throw new FormatException();
            if (splitValue[1].Contains("-"))
                throw new FormatException("Denominator can't be negative");
            if (Convert.ToInt64(splitValue[1]) == 0)
                throw new DivideByZeroException();
            return new RationalNumber(Convert.ToInt64(splitValue[0]), Convert.ToInt64(splitValue[1]));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RationalNumber))
                throw new ArgumentException("Object is not a RationalNumber");
            var other = (RationalNumber)obj;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Numerator.GetHashCode() ^ Denominator.GetHashCode();
        }

        private static RationalNumber GetReduceFraction(RationalNumber rationalNumber)
        {
            var gcd = MathHelper.GetGsd(rationalNumber.Numerator, rationalNumber.Denominator);
            return new RationalNumber(rationalNumber.Numerator/gcd, rationalNumber.Denominator/gcd);
        }

        private static void GetReduceFraction(ref long numerator, ref long denominator)
        {
            var gcd = MathHelper.GetGsd(numerator, denominator);
            if (numerator == 0) return;
            numerator /= gcd;
            denominator /= gcd;
        }

        public static bool operator ==(RationalNumber leftNumber, RationalNumber rightNumber)
        {
            return  !ReferenceEquals(leftNumber, null) && leftNumber.Equals(rightNumber);
        }

        public static bool operator !=(RationalNumber leftNumber, RationalNumber rightNumber)
        {
            return !(leftNumber == rightNumber);
        }

        public static bool operator <(RationalNumber leftNumber, RationalNumber rightNumber)
        {
            return !(leftNumber.CompareTo(rightNumber) >= 0);
        }

        public static bool operator >(RationalNumber leftNumber, RationalNumber rightNumber)
        {
            return !(leftNumber.CompareTo(rightNumber) <= 0);
        }

        public static RationalNumber operator +(RationalNumber leftNumber, RationalNumber rightNumber)
        {
            var lcm = MathHelper.GetLcm(leftNumber.Denominator, rightNumber.Denominator);
            return
                GetReduceFraction(
                    new RationalNumber(
                        leftNumber.Numerator*lcm/leftNumber.Denominator +
                        rightNumber.Numerator*lcm/rightNumber.Denominator, lcm));
        }

        public static RationalNumber operator -(RationalNumber leftNumber, RationalNumber rightNumber)
        {
            var lcm = MathHelper.GetLcm(leftNumber.Denominator, rightNumber.Denominator);
            return
                GetReduceFraction(
                    new RationalNumber(
                        leftNumber.Numerator*lcm/leftNumber.Denominator -
                        rightNumber.Numerator*lcm/rightNumber.Denominator, lcm));
        }

        public static RationalNumber operator *(RationalNumber leftNumber, RationalNumber rightNumber)
        {
            return
                GetReduceFraction(new RationalNumber(leftNumber.Numerator*rightNumber.Numerator,
                    leftNumber.Denominator*rightNumber.Denominator));
        }

        public static RationalNumber operator /(RationalNumber leftNumber, RationalNumber rightNumber)
        {
            return
                GetReduceFraction(new RationalNumber(leftNumber.Numerator*rightNumber.Denominator,
                    leftNumber.Denominator*rightNumber.Numerator));
        }

        public static implicit operator long(RationalNumber rationalNumber)
        {
            return (long) ((double) rationalNumber.Numerator/rationalNumber.Denominator);
        }

        public static implicit operator int(RationalNumber rationalNumber)
        {
            return (int) ((double) rationalNumber.Numerator/rationalNumber.Denominator);
        }

        private static string GetPeriod(RationalNumber rationalNumber)
        {
            var sb = new StringBuilder();
            if (rationalNumber.Numerator < 0)
                sb.Append('-');
            if (rationalNumber.Numerator%rationalNumber.Denominator == 0)
                return (rationalNumber.Numerator/rationalNumber.Denominator).ToString();
            var numerator = Math.Abs(rationalNumber.Numerator);
            var denominator = rationalNumber.Denominator;
            var integerPart = numerator/denominator;
            sb.Append($"{integerPart}" + ",");
            numerator -= integerPart*denominator;
            var periodDictionary = new Dictionary<long, long>();
            var period = new StringBuilder();
            var n = numerator;
            var div = false;
            n *= 10;
            while (n < denominator)
            {
                n *= 10;
                sb.Append('0');
            }
            do
            {
                if (periodDictionary.Count != 0)
                {
                    if (periodDictionary.ContainsKey(n%denominator))
                    {
                        break;
                    }
                }
                periodDictionary.Add(n%denominator, n/denominator);
                period.Append(n/denominator);
                n %= denominator;
                n *= 10;
                if (n == 0)
                    div = true;
            } while (true);
            if (div)
            {
                sb.Append(period);
            }
            else
            {
                sb.Append(period.ToString()
                    .Substring(0, period.ToString().IndexOf(periodDictionary[n%denominator].ToString(), StringComparison.Ordinal)));
                sb.Append('(');
                sb.Append(
                    period.ToString().Substring(period.ToString().IndexOf(periodDictionary[n%denominator].ToString(), StringComparison.Ordinal)) +
                    ")");
            }
            return sb.ToString();
        }
    }
}