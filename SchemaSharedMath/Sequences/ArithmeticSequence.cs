// <copyright file="ArithmeticSequence.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Sequences
{
    using JetBrains.Annotations;
    using SchemaSharedMath.Numbers;
    using SchemaSharedMath.Primes;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Arithmetic Sequence.
    /// </summary>
    public class ArithmeticSequence
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticSequence"/> class.
        /// </summary>
        /// <param name="givenCharacteristic">The given characteristic.</param>
        /// <param name="givenSize">Size of the given.</param>
        public ArithmeticSequence(NormalCharacteristic givenCharacteristic, int givenSize)
        {
            this.Characteristic = givenCharacteristic;
            this.Order = this.Characteristic.Order;
            this.Size = givenSize;
            this.BuildSequenceFromCharacteristic();
            this.FermatSum = this.Sequence.LookForFermatSum(this.Size);
            this.Generator = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticSequence"/> class.
        /// </summary>
        /// <param name="givenSequence">The given sequence.</param>
        [UsedImplicitly]
        public ArithmeticSequence(NormalSequence givenSequence)
        {
            this.Sequence = givenSequence;
            this.Size = this.Sequence.Size;
            this.PrepareDifferences();
            //// this.Characteristic = givenCharacteristic;
            //// this.Order = this.Characteristic.Order;
            this.FermatSum = this.Sequence.LookForFermatSum(this.Size);
            this.Generator = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticSequence"/> class.
        /// </summary>
        /// <param name="givenPolynomial">The given polynomial.</param>
        /// <param name="givenGenerator">The given generator.</param>
        public ArithmeticSequence(Polynomial givenPolynomial, string givenGenerator)
        {
            var seq = new NormalSequence();
            for (int i = 0; i < givenPolynomial.Length; i++) {
                int a = (int)givenPolynomial.A[i];
                seq.Elements.Add(a);
            }

            this.Generator = givenGenerator;

            this.Sequence = seq;
            this.Size = this.Sequence.Size;
            this.Order = 9;
            this.PrepareDifferences();
            this.Characteristic = new NormalCharacteristic();
            foreach (var d in this.Differences) {
                this.Characteristic.Elements.Add(d.Elements[0]);
            }
            //// this.Order = this.Characteristic.Order;
            this.FermatSum = this.Sequence.LookForFermatSum(this.Size);
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the generator.
        /// </summary>
        /// <value>
        /// The generator.
        /// </value>
        public string Generator { get; set; }

        /// <summary>
        /// Gets or sets the characteristic.
        /// </summary>
        /// <value>
        /// The characteristic.
        /// </value>
        public NormalCharacteristic Characteristic { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public NormalSequence Sequence { get; set; }

        /// <summary>
        /// Gets or sets the fermat sum.
        /// </summary>
        /// <value>
        /// The fermat sum.
        /// </value>
        public FermatSum FermatSum { get; set; }

        /// <summary>
        /// Gets or sets the differences.
        /// </summary>
        /// <value>
        /// The differences.
        /// </value>
        public List<NormalSequence> Differences { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the fermat sum key indexes.
        /// </summary>
        /// <value>
        /// The fermat sum key indexes.
        /// </value>
        public string FermatSumKeyIndexes
        {
            get
            {
                if (this.FermatSum != null) {
                    return this.FermatSum.KeyIndexes;
                }
                else {
                    return @"xxxxxxxx";
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            sb.Append(this.Generator);
            sb.Append(this.Characteristic);
            sb.Append(FermatSum?.ToString() ?? string.Empty.PadRight(33));
            sb.Append(this.Sequence);
            return sb.ToString();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Builds the sequence from characteristic.
        /// </summary>
        private void BuildSequenceFromCharacteristic()
        {
            var cseq = new NormalSequence(this.Size, this.Characteristic.Elements[0]);
            var diffs = new List<NormalSequence> { cseq };
            for (int level = 1; level < this.Order; level++) {
                var dseq = diffs[level - 1].SummarySequence(this.Characteristic.Elements[level]);
                diffs.Add(dseq);
            }

            this.Sequence = diffs[this.Order - 1];
            this.Differences = diffs;
        }

        /// <summary>
        /// Prepares the differences.
        /// </summary>
        private void PrepareDifferences()
        {
            //// var elems = this.Sequence.Elements;
            var differences = new List<NormalSequence> { this.Sequence };
            for (int level = 1; level < this.Order; level++) {
                var dseq = differences[level - 1].DifferentialSequence;
                differences.Add(dseq);
            }

            this.Differences = differences;
        }
        #endregion
    }
}
