// <copyright file="NumberProperties.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Numbers
{
    using JetBrains.Annotations;
    using SchemaSharedMath.Primes;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Integer number and its characteristics.
    /// </summary>
    public class NumberProperties {
        /// <summary>
        /// Max Del.
        /// </summary>
        private const int MaxDel = 20;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberProperties"/> class.
        /// </summary>
        /// <param name="givenNumber">A number.</param>
        /// <param name="givenPrimes">A primes.</param>
        public NumberProperties(long givenNumber, PrimeNumbers givenPrimes) {
            this.Number = givenNumber;
            this.Primes = givenPrimes;
            this.Bases = new long[MaxDel];
            this.Exponents = new int[MaxDel];
            this.Divisors = new long[MaxDel];
            this.Partition();
        }

        #region Properties
        /// <summary>
        /// Gets or sets the primes.
        /// </summary>
        /// <value>
        /// The primes.
        /// </value>
        private PrimeNumbers Primes { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        private long Number { get; set; }
        //// private int CountBases, CountDivisors;

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        private int Count { get; set; }

        /// <summary>
        /// Gets or sets the bases.
        /// </summary>
        /// <value>
        /// The bases.
        /// </value>
        private long[] Bases { get; set; }

        /// <summary>
        /// Gets or sets the exponents.
        /// </summary>
        /// <value>
        /// The exponents.
        /// </value>
        private int[] Exponents { get; set; }

        /// <summary>
        /// Gets or sets the divisors.
        /// </summary>
        /// <value>
        /// The divisors.
        /// </value>
        private long[] Divisors { get; set; }
        #endregion

        /// <summary>
        /// Primes at.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns> Returns value. </returns>
        public int PrimeAt(int i) {
            return this.Primes.PrimeAt(i);
        }

        /// <summary>
        /// Partitions this instance. - Partition of the number to prime factors.
        /// </summary>
        public void Partition() {
            int j;
            long n = this.Number;
            this.Count = 0;
            for (j = 0; j < MaxDel; j++) {
                this.Exponents[j] = 1;
                this.Bases[j] = 0;
            }

            if (n == 0) {
                return;
            }

            int i = 0;
            j = 0;
            do {
                long rest = n % this.PrimeAt(i);
                if (rest == 0) {
                    n = n / this.PrimeAt(i);
                    if (this.PrimeAt(i) == this.Bases[j]) {
                        this.Exponents[j]++;
                    }
                    else {
                        this.Bases[++j] = this.PrimeAt(i);
                    }
                }
                else {
                    i++;
                }
            } while (n != 1 && i < this.Primes.Count);
            this.Count = j;
        }

        /// <summary>
        /// Phis this instance.
        /// </summary>
        /// <returns> Returns value. </returns>
        public long Phi() {
            int j;
            long fiR = this.Number;
            int count = this.Count;
            for (j = 1; j <= count; j++) {
                if (this.Bases[j] != 1) {
                    fiR = fiR * (this.Bases[j] - 1) / this.Bases[j];
                }
            }

            return fiR;
        }

        //// All divisors of the given number

        /// <summary>
        /// Determines the divisors.
        /// </summary>
        [UsedImplicitly]
        public void DetermineDivisors() {
            long i;
            int j = 0;
            for (i = 0; i < this.Number - 1; i++) {
                long rest = this.Number % i;
                if (rest == 0) {
                    this.Divisors[++j] = i;
                }
            }
            ////this.CountDivisors = j;
        }

        #region String representation
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString() {
            StringBuilder s = new StringBuilder();
            s.Append(this.Number);
            s.Append(" = ");
            int count = this.Count;
            for (int j = 1; j <= count; j++) {
                long p = this.Bases[j];
                int t = this.Exponents[j];
                if (p != 0) {
                    if (j > 1) {
                        s.Append(" . ");
                    }

                    if (t > 1) {
                        s.Append(string.Format(
                            CultureInfo.CurrentCulture, 
                            "{0}^{1}",
                            p.ToString("D", System.Globalization.CultureInfo.CurrentCulture.NumberFormat),
                            t.ToString("D", System.Globalization.CultureInfo.CurrentCulture.NumberFormat)));
                    }
                    else {
                        s.Append(string.Format(
                            CultureInfo.CurrentCulture, 
                            "{0}",
                            p.ToString("D", System.Globalization.CultureInfo.CurrentCulture.NumberFormat)));
                    }
                }
            }

            s.Append("(phi=" + this.Phi().ToString(System.Globalization.CultureInfo.CurrentCulture.NumberFormat) + ") \r");
            return s.ToString();
        }
        #endregion
    }
}
