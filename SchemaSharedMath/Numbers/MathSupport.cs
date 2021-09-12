// <copyright file="MathSupport.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace LargoBaseAbstract {
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;

    /// <summary> Mathematical utilities. </summary>
    /// was static
    public class MathSupport {
        /// <summary>
        /// Alpha characters.
        /// </summary>
        public const string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Numeric characters.
        /// </summary>
        public const string Numeric = "0123456789";

        /// <summary>
        /// Alphanumeric characters.
        /// </summary>
        [UsedImplicitly] public const string Alphanumeric = Alpha + Numeric;

        #region Random
        /// <summary>
        /// Gets System.GUID. System.GUID has a very low probability of being duplicated 
        /// and provides more unique seed values. 
        /// </summary>
        /// <value> General musical property.</value>
        public static int GuidSeed {
            get {
                const byte seedLength = 5;
                string guid = System.Guid.NewGuid().ToString("N", CultureInfo.CurrentCulture);
                int seed = 0;
                guid = guid.Replace("a", string.Empty).Replace("b", string.Empty).Replace("c", string.Empty)
                            .Replace("d", string.Empty).Replace("e", string.Empty).Replace("f", string.Empty);
                if (guid.Length > seedLength) {
                    seed = int.Parse(guid.Substring(0, seedLength), System.Globalization.CultureInfo.CurrentCulture.NumberFormat);
                }

                return seed;
            }
        }

        /// <summary> Gets or sets object for random numbers. </summary>
        /// <value> Property description. </value>
        public static Random RandObj { get; set; }

        /// <summary>
        /// Returns random correction, aRandomEffect 0-1.
        /// </summary>
        /// <param name="randomEffect">Random Effect.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static float RandomCorrection(float randomEffect) {
            if (RandObj == null) {
                int theSeed = MathSupport.GuidSeed;
                //// int theSeed = (int)DateTime.Now.Ticks; 
                RandObj = new Random(theSeed);
            }

            return (float)(randomEffect * RandObj.NextDouble()); // /100     
        }

        /// <summary>
        /// Returns random correction, aRandomEffect 0-1. 
        /// </summary>
        /// <param name="limit">Upper limit.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static int RandomNatural(int limit) {
            if (RandObj == null) {
                int theSeed = MathSupport.GuidSeed;
                //// int theSeed = (int)DateTime.Now.Ticks; 
                RandObj = new Random(theSeed);
            }

            return (int)Math.Floor(limit * RandObj.NextDouble()); //// Round
        }

        //// System.Threading.Thread.Sleep(10);   
        //// System.Security.Cryptography.RandomNumberGenerator
        //// generators cryptographically strong random values as follow:

        /// <summary>
        /// Creates a random password
        /// E.G. GetRandomNumber(5, Alphanumeric).
        /// </summary>
        /// <param name="length">Length of the string.</param>
        /// <param name="characterSet">Character Set.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static string GetRandomNumber(int length, string characterSet) {
            string randomData = string.Empty;
            if (!string.IsNullOrEmpty(characterSet) && length > 0) {
                int characterSetLength = characterSet.Length;
                byte[] data = new byte[length];
                using (System.Security.Cryptography.RandomNumberGenerator random = System.Security.Cryptography.RandomNumberGenerator.Create()) {
                    if (random != null) {
                        random.GetBytes(data);
                        for (int index = 0; index < length; index++) {
                            int position = data[index];
                            position = position % characterSetLength;
                            randomData = randomData + characterSet.Substring(position, 1);
                        }

                        //// random.Dispose();
                    }
                }
            }

            return randomData;
        }

        /// <summary>
        /// Get Random Number.
        /// </summary>
        /// <param name="lowerBound">Lower Bound.</param>
        /// <param name="upperBound">Upper Bound.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static double GetRandomNumber(int lowerBound, int upperBound) {
            Random r = new Random();
            double number;
            checked {
                number = r.Next(lowerBound, upperBound + 1);
            }

            return number;
        }

        #endregion

        /// <summary>
        /// Mathematical Power.
        /// </summary>
        /// <param name="value">Base value.</param>
        /// <param name="number">Exponent value.</param>
        /// <returns> Returns value. </returns>
        public static int Power(int value, int number) {
            if (number == 0) {
                return 1;
            }

            if (number % 2 == 0) { // (number is even
                return Power(value * value, number / 2);
            }
            //// (number is odd
            return value * Power(value * value, number / 2);
        }

        /// <summary>
        /// Powers the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="number">The number.</param>
        /// <returns> Returns value. </returns>
        public static long Power(long value, long number) {
            if (number == 0) {
                return 1;
            }

            if (number % 2 == 0) { // (number is even
                return Power(value * value, number / 2);
            }
            //// (number is odd
            return value * Power(value * value, number / 2);
        }

        /// <summary>
        /// Binomial quotient.
        /// </summary>
        /// <param name="number">Upper integer.</param>
        /// <param name="order">Lower integer.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static long Binom(int number, int order) {
            long[] b = new long[number + 1];
            b[0] = 1;
            for (int i = 1; i <= number; ++i) {
                b[i] = 1;
                for (int j = i - 1; j > 0; --j) {
                    b[j] += b[j - 1];
                }
            }

            return b[order];
        }

        #region Equality
        /// <summary>
        /// Equality test of the given inaccurate numbers by difference.
        /// </summary>
        /// <param name="number0">Given value 0.</param>
        /// <param name="number1">Given value 1.</param>
        /// <param name="givenDelta">Given Delta.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static bool EqualNumbers(float number0, float number1, float givenDelta) {
            return Math.Abs(number0 - number1) <= givenDelta;
        }

        /// <summary>
        /// Equality test of the given inaccurate numbers by ratio.
        /// </summary>
        /// <param name="number0">Given value 0.</param>
        /// <param name="number1">Given value 1.</param>
        /// <param name="limit">Upper limit.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static bool EqualNumbersRational(float number0, float number1, float limit) {
            //// bool b0 = Math.Abs(aR0) < limit; 
            //// bool b1 = Math.Abs(aR1) < limit;
            if (float.IsNaN(number0) || float.IsNaN(number1)) {
                return false;
            }

            //// int s0 = Math.Sign(number0);
            //// int s1 = Math.Sign(number1);
            //// if (s0 != s1) {  return false;  }

            if (number0 < 0) {
                number0 = -number0;
            }

            if (number1 < 0) {
                number1 = -number1;
            }

            float ratio, min, max;
            if (number0 < number1) {
                min = number0;
                max = number1;
            }
            else {
                min = number1;
                max = number0;
            }
            //// float min = Math.Min(number0, number1);
            //// float max = Math.Max(number0, number1);
            const float afterZero = 0.0001f;
            const float largeNumber = 10000f;
            if (min >= afterZero && min <= largeNumber) {
                ratio = max / min;
            }
            else {
                ratio = 0;
            }

            return ratio <= limit;
        }

        /// <summary>
        /// Absolute Difference.
        /// </summary>
        /// <param name="value1">Given value1.</param>
        /// <param name="value2">Given value2.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static int AbsoluteDifference(int value1, int value2) {
            int difference = value1 - value2;
            difference = difference > int.MinValue ? Math.Abs(difference) : 0;

            return difference;
        }

        #endregion

        #region Algebra
        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        /// <param name="value1">Given value1.</param>
        /// <param name="value2">Given value2.</param>
        /// <returns>Returns value.</returns>
        public static decimal GreatestCommonDivisor(decimal value1, decimal value2) {
            if (value1 == 0 || value2 == 0) {
                return 0;
            }

            decimal n1 = Math.Max(value1, value2);
            decimal n2 = Math.Min(value1, value2);
            decimal rest = -1;
            while (rest != 0) {
                rest = n1 % n2;
                n1 = n2;
                n2 = rest;
            }

            return n1;
        }

        /// <summary> Least common multiple. </summary>
        /// <param name="number1">First number.</param>
        /// <param name="number2">Second number.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static decimal LeastCommonMultiple(decimal number1, decimal number2) {
            decimal result = GreatestCommonDivisor(number1, number2);
            result = (number1 * number2) / result;
            return result;
        }

        /// <summary> Quick unexact logarithm Value (see Math.Log(num)). </summary>
        /// <param name="number">Given number.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static float SimpleLog(float number) {
            if (number <= 1) {
                return 0;
            }

            float f = (number - 1) / (number + 1);
            return 2 * (f + (f * f * f / 3)); // +f*f*f*f*f/5
        }

        /// <summary> Compute factorial. </summary>
        /// <value> Calculates the factorial value for the specified integer passed in. </value>
        /// <param name="number">Given number.</param>
        /// <returns> Returns value. </returns>
        public static int Factorial(int number) {
            return (number <= 1) ? 1 : (number * Factorial(number - 1));
        }

        /// <summary>
        /// Returns only the even numbers.
        /// </summary>
        /// <param name="givenIntegers">Integer numbers.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static IEnumerable<int> GetEven(int[] givenIntegers) {
            Contract.Requires(givenIntegers != null);
            //// Use yield to return the even numbers in the list.
            return givenIntegers.Where(i => i % 2 == 0);
        }

        /// <summary>
        /// Returns only the odd numbers.
        /// </summary>
        /// <param name="givenIntegers">Integer values.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static IEnumerable<int> GetOdd(int[] givenIntegers) {
            Contract.Requires(givenIntegers != null);
            //// Use yield to return only the odd numbers.
            return givenIntegers.Where(i => i % 2 == 1);
        }

        /// <summary>
        /// Integer power.
        /// </summary>
        /// <param name="number">Given number.</param>
        /// <param name="exponent">Exponent value.</param>
        /// <returns> Decimal value.</returns>
        [UsedImplicitly]
        public static decimal Pow(int number, int exponent) {
            decimal p = 1;
            for (int i = 1; i <= exponent; i++) {
                p *= number;
            }

            return p;
        }
        #endregion

        #region Decibels
        /// <summary>
        /// Linear to dB conversion.
        /// </summary>
        /// <param name="linear">Linear value.</param>
        /// <returns>Decibel value.</returns>
        [UsedImplicitly]
        public static double LinearToDecibels(double linear) {
            // 20 / ln( 10 )
            const double log2DB = 8.6858896380650365530225783783321;
            return Math.Log(linear) * log2DB;
        }

        /// <summary>
        /// DB to linear conversion.
        /// </summary>
        /// <param name="decibel">Decibel value.</param>
        /// <returns>Linear value.</returns>
        [UsedImplicitly]
        public static double DecibelsToLinear(double decibel) {
            // ln( 10 ) / 20
            const double quotient = 0.11512925464970228420089957273422;
            return Math.Exp(decibel * quotient);
        }

        #endregion

        /// <summary>
        /// Polynomial prefixes.
        /// </summary>
        /// <param name="sequence">Array of integers.</param>
        /// <param name="number">Given number.</param>
        [UsedImplicitly]
        public static void PrefixSums(int[] sequence, int number) {
            Contract.Requires(sequence != null);
            checked {
                for (int j = number - 1; j >= 0; --j) {
                    int sum = 0;
                    for (int i = 0; i <= j; ++i) {
                        sum += sequence[i];
                    }

                    sequence[j] = sum;
                }
            }
        }

        /// <summary>
        /// Evaluates Horner schema.
        /// </summary>
        /// <param name="sequence">Array of integers.</param>
        /// <param name="number">Given size.</param>
        /// <param name="value">Given value.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static int Horner(int[] sequence, int number, int value) {
            Contract.Requires(sequence != null);
            int result = sequence[number];
            checked {
                for (int i = number - 1; i >= 0; --i) {
                    result = (result * value) + sequence[i];
                }
            }

            return result;
        }

        /// <summary>
        /// Find Maximum.
        /// </summary>
        /// <param name="sequence">Array of integers.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static int FindMaximum(int[] sequence) {
            Contract.Requires(sequence != null);
            int result = sequence[0];
            for (int i = 1; i < sequence.Length; ++i) {
                if (sequence[i] > result) {
                    result = sequence[i];
                }
            }

            return result;
        }

        /// <summary>
        /// Gamma function.
        /// </summary>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static double Gamma() {
            const int testLimit = 500000;
            double result = 0;
            for (int i = 1; i <= testLimit; ++i) {
                result += (1.0 / i) - Math.Log((i + 1.0) / i);
            }

            return result;
        }

        #region Euklid
        /*
        /// <summary>
        /// Greatest common divisor.
        /// </summary>
        /// <param name="value1">Given value1.</param>
        /// <param name="value2">Given value2.</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        /// Eukliduv algoritmus pro vypocet nejvetsiho
        /// spolecneho divisorse a nejmensiho spolecneho
        /// nasobku dvou cisel.
        private static long GreatestCommonDivisor(long value1, long value2) {
            //// long number1 = Math.Max(value1, value2)
            //// long number2 = Math.Min(value1, value2)
            long number1, number2;
            if (value1 > value2) {
                number1 = value1;
                number2 = value2;
            }
            else {
                number1 = value2;
                number2 = value1;
            }

            if (number2 == 0) {
                return number1;
            }

            long rest = 1;
            while (rest != 0) {
                if (number2 != 0) {
                    rest = number1 % number2;
                }

                number1 = number2;
                number2 = rest;
            }

            return number1;
        }

        /// <summary>
        /// Least common multiple.
        /// </summary>
        /// <param name="value1">Given value1.</param>
        /// <param name="value2">Given value2.</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        private static long LeastCommonMultiple(long value1, long value2) {
            long divisors = MathSupport.GreatestCommonDivisor(value1, value2);
            if (divisors <= 0) {
                return 0;
            }

            return (value1 * value2) / divisors;
        }
        */
        #endregion

        /* not used
        /// <summary>
        /// Generates a random string with the given length.
        /// </summary>
        /// <param name="size">Size of the string.</param>
        /// <param name="lowerCase">If true, generate lowercase string.</param>
        /// <returns>Random string.</returns>
        private static string RandomString(int size, bool lowerCase) {
            const byte randomQuotient = 26;
            const byte randomAdder = 65;
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor((randomQuotient * random.NextDouble()) + randomAdder)));
                builder.Append(ch);
            }

            if (lowerCase) {
                return builder.ToString().ToLower(CultureInfo.CurrentCulture);
            }

            return builder.ToString();
        } */
    }
}
