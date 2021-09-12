// <copyright file="FermatMaster.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Primes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using JetBrains.Annotations;
    using LargoBaseAbstract;
    using SchemaSharedClasses;
    using SchemaSharedMath.Sequences;

    /// <summary>
    /// Fermat Master.
    /// </summary>
    public static class FermatMaster {
        /// <summary>
        /// Generates the sequences.
        /// </summary>
        public static void GenerateSequences() {
            List<ArithmeticSequence> list = new List<ArithmeticSequence>();
            for (long c0 = 0; c0 < 10000; c0++) {
                //// var nc = new NormalCharacteristic(1, 1, 1, c0);
                //// var nc = new NormalCharacteristic(6, 6, 1, c0);
                //// var nc = new NormalCharacteristic(6, 6, c0, 0);
                var nc = new NormalCharacteristic(6, 0, 1, c0);
                var seq = new ArithmeticSequence(nc, 100);
                list.Add(seq);
            }

            var sortedlist = list.OrderBy(seq => seq.FermatSumKeyIndexes).ToList();

            var sb = new StringBuilder();
            foreach (var seq in sortedlist) { 
                sb.AppendLine(seq.ToString());
            }

            var s = sb.ToString();
            SupportFiles.StringToFile(s, @"d:\Temp\Fermat\List 6-0-1-c.txt");
            //// SupportFiles.StringToFile(s, @"d:\Temp\Fermat\List 6-6-c-0.txt");
            //// SupportFiles.StringToFile(s, @"d:\Temp\Fermat\List 1-1-1-c.txt");
            //// SupportFiles.StringToFile(s, @"d:\Temp\Fermat\List 6-6-1-c.txt");
        }

        /// <summary>
        /// Lasts the fermat variants. (from Last Fermat Generator).
        /// </summary>
        [UsedImplicitly]
        public static void LastFermatVariants() {
            const long limit = 10000;
            string str = string.Empty;
            for (long r = 4; r < limit; r++) {
                long r3 = r * r * r;

                for (long s = r + 1; s < limit; s++) {
                    long c = (long)MathSupport.GreatestCommonDivisor(r, s);
                    if (c > 1) {
                        continue;
                    }

                    for (long d = 2; d < r; d++) {
                        if (r3 % d != 0) {
                            continue;
                        }

                        long d3 = d * d * d;
                        long t = r3 - d3;
                        long u = r - d;
                        long v = t / u;

                        long f = s * (s + d); ////3*d*
                        //// long m1 = f % u;   long m2 = f % v;   long m3 = u % f;  long m4 = v % f;
                        //// if (q % t == 0) {
                        //// long x = q / t;
                        //// m1={6,4} m2={7,4} m3={8,4} m4={9,4}
                        long w = t % f;
                        if (t >= f && w == 0) {
                            str = str + string.Format("a={0,4} b={1,4} c={2,4} d={3,3} u={4,8} v={5,8} x={6,8}  \r", r, s, s + d, d, u, v, w);
                        }
                        //// }
                    }
                }
            }

            //// MessageBox.Show(str);
            SupportFiles.StringToFile(str, "TestFermat.txt");
        }
    }
}
