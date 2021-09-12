// <copyright file="PermutedFigure.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Combinatorics {
    using JetBrains.Annotations;
    using System;
    using System.Text;

    /// <summary>
    /// Permuted Figure.
    /// </summary>
    /// <remarks>
    /// Figure is planned to be abstract prototype for figures
    /// of any kind. It is used as superclass for melodic figures.
    /// </remarks>
    [Serializable]
    public class PermutedFigure {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PermutedFigure" /> class.
        /// </summary>
        public PermutedFigure() { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermutedFigure" /> class.
        /// </summary>
        /// <param name="givenDegree">A degree.</param>
        /// <param name="givenOrder">An order.</param>
        [UsedImplicitly]
        public PermutedFigure(byte givenDegree, byte givenOrder) {
            this.Degree = givenDegree;
            this.Order = givenOrder;
            this.Elements = new byte[this.Order];
            for (short e = 0; e < this.Order; e++) {
                this.Elements[e] = 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermutedFigure" /> class.
        /// </summary>
        /// <param name="figure">The figure.</param>
        public PermutedFigure(PermutedFigure figure) {
            this.Degree = figure.Degree;
            this.Order = figure.Order;
            this.INumber = figure.INumber;
            this.Level = figure.Level;
            this.Elements = new byte[this.Order];
            for (short e = 0; e < this.Order; e++) {
                this.Elements[e] = figure.Elements[e];
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the degree. - degree=2 for binary systems.
        /// </summary>
        /// <value>
        /// The degree.
        /// </value>
        public byte Degree { get; set; }

        /// <summary>
        /// Gets or sets the order. - total number of bits.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public byte Order { get; set; }

        //// sum of digits

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public byte Level { get; set; }

        //// number of placed Hats

        /// <summary>
        /// Gets or sets the hats.
        /// </summary>
        /// <value>
        /// The hats.
        /// </value>
        public byte Hats { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        public byte[] Elements { get; set; }

        /// <summary>
        /// Gets or sets the sums.
        /// </summary>
        /// <value>
        /// The sums.
        /// </value>
        public byte[] Sums { get; set; }

        //// instance number  

        /// <summary>
        /// Gets or sets the I number.
        /// </summary>
        /// <value>
        /// The I number.
        /// </value>
        public ulong INumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [correct sum].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [correct sum]; otherwise, <c>false</c>.
        /// </value>
        private bool CorrectSum { get;  set; }
        #endregion

        /// <summary>
        /// Sets number of structure and re-compute characteristics.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetINumber(ulong value) {
            this.INumber = value;
            this.MakeElements();
            this.DetermineLevel(); // ?!
            // Kvùli všeintervalovým øadám
            this.CorrectSum = this.MakeSumOfElements();
        }

        /// <summary>
        /// New item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="level">The level.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public bool AddItem(byte item, byte level) {
            if (Array.IndexOf(this.Elements, item) < 0) {
                this.Elements[level] = item;
                return true;
            }

            return false;
        }
        #region Validation
        /// <summary>
        /// Test of validity.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        [UsedImplicitly]
        public bool IsValid() {
            bool ok = true;
            //// condition for distinction of numbers
            for (byte e = 0; e < this.Order; e++) {
                for (byte f = (byte)(e + 1); f < this.Order; f++) {
                    if (f == e || (this.Elements[e] != this.Elements[f]))
                    {
                        continue;
                    }

                    ok = false;
                    break;
                }
            }

            return ok;
        }

        /// <summary>
        /// Tests validity.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has valid sums]; otherwise, <c>false</c>.
        /// </returns>
        [UsedImplicitly]
        public bool HasValidSums() {
            bool ok = true;
            for (byte e = 0; e < this.Order; e++) {
                if (this.Sums[e] != 0) {
                    continue; //// % (degree+1)
                }

                ok = false;
                break;
            }

            if (ok) {
                //// podmínka na rùznost souètù èísel
                for (byte e = 0; e < this.Order; e++) {
                    for (byte f = (byte)(e + 1); f < this.Order; f++) {
                        if (f == e || (this.Sums[e] != this.Sums[f])) {
                            continue; ////  % (degree+1)
                        }              
 
                        ok = false;
                        break;
                    }
                }
            }

            return ok;
        }
        #endregion

        /// <summary>
        /// Makes the sum of elements.
        /// </summary>
        /// <returns> Returns value. </returns>
        public bool MakeSumOfElements() {
            int sum = 0;
            this.Sums = new byte[this.Order];
            for (byte e = 0; e < this.Order; e++) {
                sum = sum + this.Elements[e];
                this.Sums[e] = (byte)(sum % (this.Degree + 1));
                if (this.Sums[e] == 0) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determine and sets the level property.
        /// </summary>
        [UsedImplicitly]
        public void DetermineHats() {
            this.Hats = 0;
            for (byte e = 0; e < this.Order; e++) {
                if (this.Elements[e] == e + 1) {
                    this.Hats += 1;
                }
            }
        }

        /// <summary> Invert motive vertically. </summary>
        [UsedImplicitly]
        public void Invert() {
            ulong limitNumber = (ulong)Math.Pow(this.Degree, this.Order) - 1;
            ////this.INumber=(limitNumber-this.INumber);
            this.SetINumber(limitNumber - this.INumber);
        }

        #region String representation
        /// <summary>
        /// List of figure elements.
        /// </summary>
        /// <returns> Returns value. </returns>
        public string ElementString() {
            StringBuilder s = new StringBuilder();
            for (byte e = 0; e < this.Order; e++) {
                s.Append(this.Elements[e]);
                if (e < this.Order - 1) {
                    s.Append(",");
                }
            }

            return s.ToString();
        }

        /// <summary>
        /// List of figure elements.
        /// </summary>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public string SumString() {
            StringBuilder s = new StringBuilder("0,");
            for (byte e = 0; e < this.Order; e++) {
                s.Append(this.Sums[e] % (this.Degree + 1));
                if (e < this.Order - 1) {
                    s.Append(",");
                }
            }

            /*
            bool printtones = false;
            if (printtones) {
                string[] sym12 = { "c", "c#", "d", "d#", "e", "f", "f#", "g", "g#", "a", "a#", "h" };
                s.Append("  c,");
                for (byte e = 0; e < this.Order; e++) {
                    byte n = (byte)((byte)this.Sums[e] % (this.Degree + 1));
                    s.Append(sym12[n]);
                    if (e < this.Order - 1) {
                        s.Append(",");
                    }
                }
            } */

            //// s.Append(" <= 0,");
            //// for (byte e = 0; e < Order; e++) {
            ////    s.Append(Sums[e]);
            ////    if (e< Order-1) s.Append(",");
            //// }
            return s.ToString();
        }

        /// <summary>
        /// String representation of the object.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString() {
            StringBuilder s = new StringBuilder();
            s.Append("(");
            s.Append(this.ElementString());
            s.Append(") ");
            //// Kvùli všeintervalovým øadám
            //// s.Append(": "+this.SumString());
            //// Kvùli kloboukùm
            //// s.Append(": "+this.Hats);
            return s.ToString();
        }
        #endregion

        /// <summary>
        /// Determine and sets the level property.
        /// </summary>
        private void MakeElements() {
            ulong num = this.INumber;
            this.Elements = new byte[this.Order];
            for (short e = (short)(this.Order - 1); e >= 0; e--) {
                this.Elements[e] = (byte)((num % this.Degree) + 1);
                num = num / this.Degree;
            }
        }

        /// <summary>
        /// Determine and sets the level property.
        /// </summary>
        private void DetermineLevel() {
            this.Level = 0;
            for (byte e = 0; e < this.Order; e++) {
                this.Level += this.Elements[e];
            }
        }
    }
}
