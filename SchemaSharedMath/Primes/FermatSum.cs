// <copyright file="FermatSum.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Primes
{
    using System.Text;

    /// <summary>
    /// Fermat Sum.
    /// </summary>
    public class FermatSum {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FermatSum"/> class.
        /// </summary>
        public FermatSum() {
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the index r.
        /// </summary>
        /// <value>
        /// The index r.
        /// </value>
        public int IndexR { get; set; }

        /// <summary>
        /// Gets or sets the index s.
        /// </summary>
        /// <value>
        /// The index s.
        /// </value>
        public int IndexS { get; set; }

        /// <summary>
        /// Gets or sets the index t.
        /// </summary>
        /// <value>
        /// The index t.
        /// </value>
        public int IndexT { get; set; }

        /// <summary>
        /// Gets or sets the value r.
        /// </summary>
        /// <value>
        /// The value r.
        /// </value>
        public long ValueR { get; set; }

        /// <summary>
        /// Gets or sets the value s.
        /// </summary>
        /// <value>
        /// The value s.
        /// </value>
        public long ValueS { get; set; }

        /// <summary>
        /// Gets or sets the value t.
        /// </summary>
        /// <value>
        /// The value t.
        /// </value>
        public long ValueT { get; set; }

        /// <summary>
        /// Gets the key indexes.
        /// </summary>
        /// <value>
        /// The key indexes.
        /// </value>
        public string KeyIndexes
        {
            get
            {
                var s = string.Format("{0,5}-{1,5}-{2,5}", this.IndexR, this.IndexS, this.IndexT);
                return s;
            }
        }

        /// <summary>
        /// Gets the indexes to string.
        /// </summary>
        /// <value>
        /// The indexes to string.
        /// </value>
        public string IndexesToString
        {
            get
            {
                var s = string.Format("({0},{1},{2})", this.IndexR, this.IndexS, this.IndexT);
                return s;
            }
        }

        /// <summary>
        /// Gets the values to string.
        /// </summary>
        /// <value>
        /// The values to string.
        /// </value>
        public string ValuesToString
        {
            get
            {
                var s = string.Format("{0}+{1}={2}", this.ValueR, this.ValueS, this.ValueT);
                return s;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder(string.Empty);
            var istr = this.IndexesToString;
            var vstr = this.ValuesToString;
            sb.Append(istr.PadRight(16));
            sb.Append("  ");
            sb.Append(vstr.PadRight(16));
            return sb.ToString();
        }
        #endregion
    }
}
