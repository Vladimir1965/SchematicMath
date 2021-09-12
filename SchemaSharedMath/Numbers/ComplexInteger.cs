// <copyright file="ComplexInteger.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Numbers
{
    using System;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary>
    /// Complex number with integer coefficients.
    /// </summary>
    public class ComplexInteger {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexInteger"/> class.
        /// </summary>
        /// <param name="a">The number A.</param>
        /// <param name="b">The number B.</param>
        public ComplexInteger(int a, int b) {
            this.A = a;
            this.B = b;
        }

        #region Properties
        /// <summary>
        /// Gets or sets the A.
        /// </summary>
        /// <value>
        /// The A.
        /// </value>
        public int A { get; set; }

        /// <summary>
        /// Gets or sets the B.
        /// </summary>
        /// <value>
        /// The B.
        /// </value>
        public int B { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value> Property description. </value>
        public double Value => Math.Sqrt((this.A * this.A) + (this.B * this.B));

        /// <summary>
        /// Gets the norm.
        /// </summary>
        /// <value> Property description. </value>
        public int Norm => (this.A * this.A) + (this.B * this.B);

        #endregion

        #region Complements
        /// <summary>
        /// Reals the complement.
        /// </summary>
        [UsedImplicitly]
        public void RealComplement() {
            this.A = -this.A;
        }

        /// <summary>
        /// Imaginary complement.
        /// </summary>
        public void ImagComplement() {
            this.B = -this.B;
        }

        /// <summary>
        /// Complements this instance.
        /// </summary>
        [UsedImplicitly]
        public void Complement() {
            this.A = -this.A; 
            this.B = -this.B;
        }

        /// <summary>
        /// Swaps this instance.
        /// </summary>
        [UsedImplicitly]
        public void Swap() {
            int c = this.A;
            this.A = this.B;
            this.B = c;
        }
        #endregion

        #region Adding and Substracting
        /// <summary>
        /// Adds the number.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        [UsedImplicitly]
        public void AddNumber(int x, int y) {
            this.A = this.A + x;
            this.B = this.B + y;
        }

        /// <summary>
        /// Adds the specified cn.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        [UsedImplicitly]
        public void Add(ComplexInteger cn) {
            this.A = this.A + cn.A;
            this.B = this.B + cn.B;
        }

        /// <summary>
        /// Subtracts the specified cn.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        [UsedImplicitly]
        public void Subtract(ComplexInteger cn) {
            this.A = this.A - cn.A;
            this.B = this.B - cn.B;
        }
        #endregion

        #region Multiplication and Division
        /// <summary>
        /// Reals the multiply.
        /// </summary>
        /// <param name="r">The r.</param>
        [UsedImplicitly]
        public void RealMultiply(int r) {
            this.A = this.A * r;
            this.B = this.B * r;
        }

        /// <summary>
        /// Multiplies the specified cn.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        public void Multiply(ComplexInteger cn) {
            int poma = (this.A * cn.A) - (this.B * cn.B);
            int pomb = (this.A * cn.B) + (this.B * cn.A);
            this.A = poma;
            this.B = pomb;
        }

        /// <summary>
        /// Reals the divide.
        /// </summary>
        /// <param name="r">The r.</param>
        public void RealDivide(int r) {
            this.A = this.A / r;
            this.B = this.B / r;
        }

        /// <summary>
        /// Divides the specified cn.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        [UsedImplicitly]
        public void Divide(ComplexInteger cn) {
            int n = cn.Norm;
            cn.ImagComplement();
            this.Multiply(cn);
            this.RealDivide(n);
        }
        #endregion

        #region Congruences
        /// <summary>
        /// Primaries the form.
        /// </summary>
        [UsedImplicitly]
        public void PrimaryForm() {
            int j = 0;
            bool f2 = ((this.A - 1) % 2 == 0) && (this.B % 2 == 0);
            bool f4 = ((this.A - 1) % 4 == 0) && (this.B % 4 == 0);
            bool f4x = ((this.A - 1) % 4 == 0) || (this.B % 4 == 0);
            while (!(f4 || (f2 && !f4x)) && (j < 4)) {   /* nasobeno i */
                int c = this.B;
                this.B = -this.A;
                this.A = c;
                f2 = ((this.A - 1) % 2 == 0) && (this.B % 2 == 0);
                f4 = ((this.A - 1) % 4 == 0) && (this.B % 4 == 0);
                f4x = ((this.A - 1) % 4 == 0) || (this.B % 4 == 0);
                j = j + 1;
            }
        }

        /// <summary>
        /// Module of the specified cn.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        [UsedImplicitly]
        public void Module(ComplexInteger cn) {
            int n = cn.Norm;
            this.Multiply(cn);
            this.RealDivide(n);
        }

        /// <summary>
        /// Min module.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        [UsedImplicitly]
        public void MinModule(ComplexInteger cn) {
            int n = cn.Norm;
            cn.ImagComplement();
            this.Multiply(cn);
            int f = this.A % n;
            int g = this.B % n;
            if (f > n / 2) {
                f = f - n;
            }

            if (g > n / 2) {
                g = g - n;
            }

            if (f < -n / 2) {
                f = f + n;
            }

            if (g < -n / 2) {
                g = g + n;
            }

            this.A = (cn.A * f) + (cn.B * g);
            this.B = (cn.A * g) - (cn.B * f);
            this.RealDivide(n);
        }
        #endregion

        #region Roots and Powers
        /// <summary>
        /// Powers the specified k.
        /// </summary>
        /// <param name="k">The k.</param>
        [UsedImplicitly]
        public void Power(int k) {
            int i;
            if (k == 0) {
                this.A = 1;
                this.B = 0;
            }

            ComplexInteger cn = new ComplexInteger(this.A, this.B);
            for (i = 1; i <= k - 1; i++) {
                this.Multiply(cn);
            }
        }
        #endregion 

        #region String representation
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString() {
            StringBuilder s = new StringBuilder();
            return s.ToString();
        }
        #endregion
    }
}