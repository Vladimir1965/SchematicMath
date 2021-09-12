// <copyright file="NormalCharacteristic.cs" company="Traced-Ideas, Czech republic">
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
    using JetBrains.Annotations;

    /// <summary>
    /// Normal Characteristic.
    /// </summary>
    public class NormalCharacteristic {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NormalCharacteristic"/> class.
        /// </summary>
        public NormalCharacteristic() {
            this.Elements = new List<long>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalCharacteristic"/> class.
        /// </summary>
        /// <param name="c0">The c0.</param>
        /// <param name="c1">The c1.</param>
        [UsedImplicitly]
        public NormalCharacteristic(long c0, long c1) {
            this.Elements = new List<long>();
            this.Elements.Add(c0);
            this.Elements.Add(c1);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalCharacteristic"/> class.
        /// </summary>
        /// <param name="c0">The c0.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="c2">The c2.</param>
        public NormalCharacteristic(long c0, long c1, long c2) {
            this.Elements = new List<long>();
            this.Elements.Add(c0);
            this.Elements.Add(c1);
            this.Elements.Add(c2);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalCharacteristic"/> class.
        /// </summary>
        /// <param name="c0">The c0.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="c3">The c3.</param>
        public NormalCharacteristic(long c0, long c1, long c2, long c3) {
            this.Elements = new List<long>();
            this.Elements.Add(c0);
            this.Elements.Add(c1);
            this.Elements.Add(c2);
            this.Elements.Add(c3);
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order => this.Elements.Count;

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        public List<long> Elements { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder("[");
            foreach (var c in this.Elements) {
                sb.AppendFormat("0,4", c);
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString().PadRight(12);
        }
        #endregion
    }
}
