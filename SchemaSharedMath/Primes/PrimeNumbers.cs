// <copyright file="PrimeNumbers.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Primes
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Prime Numbers.
    /// </summary>
    public class PrimeNumbers {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimeNumbers"/> class.
        /// </summary>
        /// <param name="givenLimit">A limit.</param>
        public PrimeNumbers(int givenLimit) {
            this.Limit = givenLimit;
            this.Primes = new int[this.Limit];
            this.EratosthenesSieve();
        }

        /// <summary>
        /// Gets or sets the primes.
        /// </summary>
        /// <value>
        /// The primes.
        /// </value>
        public int[] Primes { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>
        /// The limit.
        /// </value>
        private int Limit { get; set; }

        /// <summary>
        /// Primes at.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns> Returns value. </returns>
        public int PrimeAt(int i) {
            return this.Primes[i];
        }

        /// <summary>
        /// Analytical pi.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        public static float AnalyticalPi(long number) {
            float api = number == 1 ? 1 : (float)(1.0 * number / Math.Log(number));
            return api;
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
            int n = 0;
            foreach (int p in this.Primes.Where(p => p != 0))
            {
                s.Append(p.ToString("D", System.Globalization.CultureInfo.CurrentCulture.NumberFormat) + ",");
                if (n++ > 10) {
                    n = 0;
                    s.Append("\r");
                }
            }

            s.Append(string.Format(
                                CultureInfo.CurrentCulture, 
                                "\r***{0}(z {1}) ({2})***", 
                                this.Count.ToString("D", CultureInfo.CurrentCulture.NumberFormat),
                                this.Limit.ToString("D", CultureInfo.CurrentCulture.NumberFormat), 
                                AnalyticalPi(this.Limit).ToString(CultureInfo.InvariantCulture)));
            return s.ToString();
        }
        #endregion

        #region Eratosthenes
        /// <summary>
        /// Eratosthenes the sieve.
        /// </summary>
        private void EratosthenesSieve() {
            // Eratosthenes sieve k tvorbe vsech prvocisel p < Limit
            BitArray marked = new BitArray(this.Limit, false);
            int k = 0, i;
            int median = (int)Math.Sqrt(this.Limit) + 1;
            for (i = 2; i <= median; i++) {
                if (marked.Get(i))
                {
                    continue;
                }

                this.Primes[k++] = i;
                int inext = 0;
                do {
                    inext += i;
                    if (inext < this.Limit) {
                        marked.Set(inext, true);
                    }
                } while (inext < this.Limit);
            }

            for (i = median; i < this.Limit; i++) {
                if (!marked.Get(i)) {
                    this.Primes[k++] = i;
                }
            }

            this.Count = k;
        }
        #endregion
    }
}