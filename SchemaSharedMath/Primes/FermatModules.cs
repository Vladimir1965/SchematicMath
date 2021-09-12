// <copyright file="FermatModules.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Primes
{
    using System;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary>
    /// Fermat Modules.
    /// </summary>
    public class FermatModules {
        /// <summary>
        /// Range constant.
        /// </summary>
        private const uint Range = 30;

        /// <summary>
        /// Order k.
        /// </summary>
        private readonly uint k;

        /// <summary>
        /// Module m.
        /// </summary>
        private uint m;

        /// <summary>
        /// Array for modules.
        /// </summary>
        private uint[] arr = new uint[Range];

        /// <summary>
        /// Array size.
        /// </summary>
        private uint arrsize;

        /// <summary>
        /// Initializes a new instance of the <see cref="FermatModules"/> class.
        /// </summary>
        /// <param name="order">The order.</param>
        public FermatModules(uint order) {
            this.k = order;
        }

        /// <summary>
        /// Determines whether this instance contains sum.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance contains sum; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsSum() {
            uint i;
            bool found = false;
            for (i = 0; i < this.arrsize; i++) {
                if (found)
                {
                    continue;
                }

                uint j;
                for (j = i; j < this.arrsize; j++)
                {
                    uint l = 0;
                    while ((l < this.arrsize) && !found) {
                        found = (this.arr[i] + this.arr[j]) % this.m == this.arr[l++] % this.m;
                    }
                }
            }

            return found;
        }

        /// <summary>
        /// Tests the module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns> Returns value. </returns>
        public string TestModule(uint module) {
            StringBuilder s = new StringBuilder(string.Empty);
            uint iz = 0;
            this.m = module;
            this.arr = new uint[Range];
            this.arrsize = 0;
            for (uint n = 1; n < Range; n++) {
                ulong v = (ulong)Math.Pow(n, this.k);
                uint z = (uint)v % module;
                if ((z != 0) && (Array.IndexOf(this.arr, z) < 0) && iz < Range) {
                    this.arr[iz++] = z;
                }
            }

            this.arrsize = iz;
            if (this.arrsize > 1 && this.arrsize < this.m && !this.ContainsSum()) {
                s.Append(this.m); 
                s.Append(": (");
                for (uint i = 0; i < this.arrsize - 1; i++) {
                    s.Append(this.arr[i] + ",");
                }

                s.Append(this.arr[this.arrsize - 1] + ")\n");
            }

            return s.ToString();
        }

        /// <summary>
        /// Tests the modules.
        /// </summary>
        /// <param name="maxModule">The max module.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public string TestModules(uint maxModule) {
            StringBuilder s = new StringBuilder(string.Empty);
            for (uint im = 3; im <= maxModule; im++) {
                string sm = this.TestModule(im);
                s.Append(sm);
            }

            return s.ToString();
        }
    }
}
