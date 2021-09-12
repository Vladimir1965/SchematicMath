// <copyright file="RiemannMaster.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Primes
{
    using JetBrains.Annotations;
    using SchemaSharedMath.Numbers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Riemann Master.
    /// </summary>
    public static class RiemannMaster {
        /*
        static void Main(string[] args) {
            foreach (var partialSum in GetEulerPartialSum(1000000)) {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(partialSum);
            }
            Console.ReadLine();
        }*/

        /// <summary>
        /// Calculates the converged point for a Dirichlet series expansion.
        /// </summary>
        /// <param name="t">Imaginary part of s. The first zero is at 14.134725.</param>
        /// <param name="numberOfTerms">Use a higher number to find more accurate convergence.</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        [UsedImplicitly]
        public static Complex CalcZetaZero(double t, int numberOfTerms) {
            var range = Enumerable.Range(1, numberOfTerms);
            var zetaZero = Complex.Zero;

            foreach (int n in range) {
                var direction = n % 2 == 0 ? Math.PI : 0;
                var newTerm = Complex.Exp(new Complex(-Math.Log(n) * .5, (-Math.Log(n) * t) + direction));
                zetaZero += newTerm;
            }

            return zetaZero;
        }

        /// <summary>
        /// Riemann value.
        /// </summary>
        /// <param name="u">The u.</param>
        /// <param name="numberOfIterations">The number of iterations.</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        public static Complex ZetaValueBySummation(Complex u, int numberOfIterations) {
            var one = new ComplexNumber(1, 0);
            var sumer = one;
            var exponent = new ComplexNumber(u);

            for (int n = 2; n < numberOfIterations; n++) {
                var bn = new ComplexNumber(n, 0);
                var eu = bn.ComplexPower(exponent);
                //// Complex eu = Complex.IntegerPower(n, u);
                var ev = one.Divide(eu); //// Complex.IntegerExponential(n, v);
                //// Complex er = eu.Add(ev);
                sumer = sumer.Add(ev);
                ////this.AddPoint(er, Brushes.Blue);
                //// ResultText += "ev=" + ev.ToString() + " sum=" + sumer.ToString() + "\r";
            }

            return sumer.Complex;
        }

        /// <summary>
        /// Riemann value.
        /// </summary>
        /// <param name="u">The u.</param>
        /// <param name="numberOfPrimes">The number of primes.</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        public static Complex ZetaValueByMultiplication(Complex u, int numberOfPrimes) {
            PrimeNumbers primes = new PrimeNumbers(numberOfPrimes);
            //// Complex zero = new Complex(0, 0);
            var one = new ComplexNumber(1, 0);
            var exponent = new ComplexNumber(u);
            var sumer = one;
            foreach (int p in primes.Primes) {
                if (p == 0) {
                    break;
                }

                var bp = new ComplexNumber(p, 0);
                var eu = bp.ComplexPower(exponent);
                var er = one.Divide(eu);
                var ed = one.Subtract(er);
                var ev = one.Divide(ed);
                sumer = sumer.Multiply(ev);
            }

            return sumer.Complex;
        }

        /// <summary>
        /// Zetas the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="numberOfIterations">The number of iterations.</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        public static double FunctionZeta(double s, int numberOfIterations) {
            /*  s ζ(s) λ(s)
               1 undefined undefined       2 1.644934067 0.452247420  3 1.202056903 0.174762639  4 1.082323234 0.076993140
               5 1.036927755 0.035755017   6 1.017343062 0.017070087  7 1.008349277 0.008283833  8 1.004077356 0.004061405
               9 1.002008393 0.002004468  10 1.000994575 0.000993604  */
            double sum = 0;
            int k = numberOfIterations + 1;
            for (int i = 0; i < numberOfIterations; i++) {
                sum += Math.Pow(i + 1, -s);
            }

            //// Use Euler-Maclaurin (EM) summation
            sum += (Math.Pow(k, 1 - s) / (s - 1)) + //// Trapezoidal approx.
                   (Math.Pow(k, -s) / 2) + //// Half-term
                   (Math.Pow(k, -s - 1) * s / 12) - //// First EM iteration
                   (Math.Pow(k, -s - 3) * s * (s + 1) * (s + 2) / 720); //// Second iteration
            return sum;
        }

        /// <summary>
        /// Calculates the converged point for a Dirichlet series expansion.
        /// </summary>
        /// <param name="t">Imaginary part of s. The first zero is at 14.134725.</param>
        /// <param name="numberOfTerms">Use a higher number to find more accurate convergence.</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        [UsedImplicitly]
        public static Complex CalcZetaZeroDirichlet(double t, int numberOfTerms) {
            var range = Enumerable.Range(1, numberOfTerms);
            var zetaZero = ComplexNumber.Zero;

            foreach (int n in range) {
                var direction = n % 2 == 0 ? Math.PI : 0;
                var newTerm = ComplexNumber.Exponential(new ComplexNumber(-Math.Log(n) * .5, (-Math.Log(n) * t) + direction));
                zetaZero.Add(newTerm);
            }

            return zetaZero.Complex;
        }

        /// <summary>
        /// Complexes the synodic.
        /// </summary>
        /// <param name="givenBase">The given base.</param>
        /// <param name="givenValue">The given value.</param>
        /// <param name="givenPower">The given power.</param>
        /// <returns>Returns value.</returns>
        public static Complex ComplexSynodic(Complex givenBase, int givenValue, double givenPower) {
            var basePower = Complex.Pow(givenBase, givenPower);
            return 1 / ((1 / basePower) - (1 / Math.Pow(givenValue, givenPower)));
        }

        /// <summary>
        /// Synodic of the specified given base.
        /// </summary>
        /// <param name="givenBase">The given base.</param>
        /// <param name="givenValue">The given value.</param>
        /// <param name="givenPower">The given power.</param>
        /// <returns> Returns value. </returns>
        public static double Synodic(double givenBase, double givenValue, double givenPower) {
            var basePower = Math.Pow(givenBase, givenPower);
            return 1 / ((1 / basePower) - (1 / Math.Pow(givenValue, givenPower)));
        }

        /// <summary>
        /// Synodic - variant.
        /// </summary>
        /// <param name="givenBase">The given base.</param>
        /// <param name="givenValue">The given value.</param>
        /// <param name="givenNumber">The given number.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static double SynodicVariant(double givenBase, double givenValue, double givenNumber) {
            return 1 / ((1 / givenBase) - (1 / Math.Pow(givenValue + givenNumber, 2)));
        }

        /// <summary>
        /// Gets the euler partial sum.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>Returns value.</returns>
        [UsedImplicitly]
        private static IEnumerable<double> GetEulerPartialSum(int n)
        {
            double sum = 0.0;
            for (int i = 1; i <= n; i++) {
                sum += 1.0 / Math.Pow(i, 2);
                yield return sum;
            }
        }
    }
}
