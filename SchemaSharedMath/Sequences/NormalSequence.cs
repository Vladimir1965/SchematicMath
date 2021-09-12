// <copyright file="NormalSequence.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Sequences
{
    using System.Collections.Generic;
    using System.Text;
    using SchemaSharedMath.Primes;

    /// <summary>
    /// Normal Sequence.
    /// </summary>
    public class NormalSequence {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NormalSequence"/> class.
        /// </summary>
        public NormalSequence() {
            this.Elements = new List<long>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalSequence"/> class.
        /// </summary>
        /// <param name="givenLength">Length of the given.</param>
        /// <param name="givenConstant">The given constant.</param>
        public NormalSequence(int givenLength, long givenConstant) {
            this.Elements = new List<long>();
            for (int i = 0; i < givenLength; i++) {
                this.Elements.Add(givenConstant);
            }
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        public List<long> Elements { get; set; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size => this.Elements.Count;

        /// <summary>
        /// Gets the differential sequence.
        /// </summary>
        /// <value>
        /// The differential sequence.
        /// </value>
        public NormalSequence DifferentialSequence
        {
            get
            {
                var ds = new NormalSequence();
                for (var i = 1; i < this.Size; i++) {
                    ds.Elements.Add(this.Elements[i] - this.Elements[i - 1]);
                }

                return ds;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Summaries the sequence.
        /// </summary>
        /// <param name="givenFirstValue">The given first value.</param>
        /// <returns> Returns value. </returns>
        public NormalSequence SummarySequence(long givenFirstValue) {
            var ns = new NormalSequence();
            ns.Elements.Add(givenFirstValue);
            for (var i = 0; i < this.Size; i++) {
                ns.Elements.Add(ns.Elements[i] + this.Elements[i]);
            }

            return ns;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder("{");
            foreach (var c in this.Elements) {
                sb.Append(c);
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// Looks for fermat sum.
        /// </summary>
        /// <param name="givenRange">The given range.</param>
        /// <returns> Returns value. </returns>
        public FermatSum LookForFermatSum(int givenRange) {
            for (int r = 0; r < givenRange; r++) {
                var vr = this.Elements[r];
                for (int s = r + 1; s < givenRange; s++) {
                    var vs = this.Elements[s];
                    if (vs <= vr) {
                        continue;
                    }

                    var vt = vr + vs;
                    //// var value = (from p in this.Progression where p == vt select p).FirstOrDefault();
                    var t = this.Elements.IndexOf(vt);
                    if (t > s) {
                        var fs = new FermatSum() {
                            IndexR = r,
                            IndexS = s,
                            IndexT = t,
                            ValueR = vr,
                            ValueS = vs,
                            ValueT = vt
                        };

                        return fs;
                    }
                }
            }

            return null;
        }
        #endregion
    }
}
