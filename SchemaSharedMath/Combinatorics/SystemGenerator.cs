// <copyright file="SystemGenerator.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Combinatorics
{
    using JetBrains.Annotations;

    /// <summary>
    /// System Generator.
    /// </summary>
    public static class SystemGenerator {
        /// <summary>
        /// Gets or sets the result text.
        /// </summary>
        /// <value>
        /// The result text.
        /// </value>
        public static string ResultText { get; set; }

        /// <summary>
        /// Wieferich systems.
        /// </summary>
        [UsedImplicitly]
        public static void WieferichSystems() {
            for (int t = 3; t < 4000; t += 2) {
                int rsize = t * t;
                RestrictedSystem rsys = new RestrictedSystem(2, rsize);
                if (rsys.Order <= t) {
                    ResultText += "t: " + t + " \r";
                }
            }
        }

        /// <summary>
        /// Low-order systems.
        /// </summary>
        [UsedImplicitly]
        public static void LowOrderSystems() {
            for (int rsize = 3; rsize < 1100; rsize += 2) {
                RestrictedSystem rsys = new RestrictedSystem(2, rsize);
                if (rsys.Order <= rsize && (rsize - 1) % rsys.Order == 0) {
                    int ratio = (rsize - 1) / rsys.Order;
                    if (ratio > 0 && ratio <= 1) {
                        ResultText += "rsize: " + rsize + "    order: " + rsys.Order + "    x=" + ratio + " \r";
                    }
                }
            }
        }

        /// <summary>
        /// Specials the level systems.
        /// </summary>
        [UsedImplicitly]
        public static void SpecialLevelSystems() {
            const int h = 7;
            for (int rsize = 3; rsize < 5000; rsize += 2) {
                if (rsize % h == 1) {
                    RestrictedSystem rsys = new RestrictedSystem(2, rsize);
                    if (rsize == (h * rsys.Order) + 1) {
                        string s = rsys.GenerateClasses(true, true);
                        ResultText += s;
                        ResultText += "Modul8 =" + (rsize % 8);
                        ResultText += " \r\r";
                    }
                }
            }
        }

        /// <summary>
        /// Wieferich like systems.
        /// </summary>
        [UsedImplicitly]
        public static void WieferichLikeSystems() {
            for (int rsize = 3; rsize < 10000; rsize += 1) { //// 1194650
                RestrictedSystem rsys = new RestrictedSystem(2, rsize);
                if ((rsize - 1) % rsys.Order == 0 && rsys.Order * rsys.Order < rsize / 4) {
                    string s = rsys.GenerateClasses(true, true);
                    if (rsize - 1 == rsys.Order * (rsys.TotalClasses - 2)) {
                        ResultText += s;
                        ResultText += " \r\r";
                    }
                }
            }
        }
    }
}
