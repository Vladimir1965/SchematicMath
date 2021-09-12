// <copyright file="ComplexNumber.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Numbers
{
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;

    /// <summary>
    /// Complex number with double coefficients.
    /// </summary>
    public class ComplexNumber {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexNumber"/> class.
        /// </summary>
        /// <param name="a">The number A.</param>
        /// <param name="b">The number B.</param>
        public ComplexNumber(double a, double b) {
            this.A = a;
            this.B = b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexNumber" /> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public ComplexNumber(ComplexNumber c) {
            this.A = c.A;
            this.B = c.B;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexNumber"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public ComplexNumber(Complex c)
        {
            this.A = c.Real;
            this.B = c.Imaginary;
        }

        #region Properties
            /// <summary>
            /// Gets or sets the A.
            /// </summary>
            /// <value>
            /// The A.
            /// </value>
        public double A { get; set; }

        /// <summary>
        /// Gets the real.
        /// </summary>
        /// <value>
        /// The real.
        /// </value>
        public double Real { 
            get {
                return this.A;
            }
        }

        /// <summary>
        /// Gets or sets the B.
        /// </summary>
        /// <value>
        /// The B.
        /// </value>
        public double B { get; set; }

        /// <summary>
        /// Gets the imaginary.
        /// </summary>
        /// <value>
        /// The imaginary.
        /// </value>
        public double Imaginary {
            get {
                return this.B;
            }
        }

        /// <summary>
        /// Gets the complex.
        /// </summary>
        /// <value>
        /// The complex.
        /// </value>
        public Complex Complex {
            get {
                return new Complex(this.A, this.B);
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value> Property description. </value>
        public double Value => Math.Sqrt((this.A * this.A) + (this.B * this.B));

        /// <summary>
        /// Gets the norm.
        /// </summary>
        /// <value> Property description. </value>
        public double Size => (this.A * this.A) + (this.B * this.B);

        /// <summary>
        /// Gets the zero.
        /// </summary>
        /// <value> Property description. </value>
        public static ComplexNumber Zero => new ComplexNumber(0, 0);

        /// <summary>
        /// Gets the norm.
        /// </summary>
        /// <value> Property description. </value>
        public double Norm => Math.Sqrt(this.Size);

        /*
        /// <summary>
        /// Gets the tangent.
        /// </summary>
        public double Tangent {
            get {
                if (Math.Abs(this.A)> Double.Epsilon) {
                    return  this.B / this.A;
                }

                if (Math.Sign(this.A) == Math.Sign(this.B)) {
                    return Double.PositiveInfinity;
                }

                return Double.NegativeInfinity;
            }
        }*/

        /// <summary>
        /// Gets the argument.
        /// </summary>
        /// <value> Property description. </value>
        public double Argument {
            get {
                double arg = Math.Atan2(this.B, this.A);
                return arg;
            }
        }

        #endregion

        #region Public static
        /// <summary>
        /// Exponentials the specified exponent.
        /// </summary>
        /// <param name="exponent">The exponent.</param>
        /// <returns> Returns value. </returns>
        public static ComplexNumber Exponential(ComplexNumber exponent) {
            double value = Math.Exp(exponent.A);
            double vcos = Math.Cos(exponent.B);
            double vsin = Math.Sin(exponent.B);
            ComplexNumber n = new ComplexNumber(value * vcos, value * vsin);
            return n;
        }

        /// <summary>
        /// Exponentials the specified exponent.
        /// </summary>
        /// <param name="basenumber">The base number.</param>
        /// <param name="exponent">The exponent.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static ComplexNumber IntegerPower(int basenumber, ComplexNumber exponent) {
            if (basenumber == 0) {
                return new ComplexNumber(0, 0);
            }

            double lg = Math.Log(Math.Abs(basenumber));
            ComplexNumber z = exponent.RealMultiply(lg);
            ComplexNumber w = Exponential(z);
            return w;
        }

        /// <summary>
        /// Exponentials the specified exponent.
        /// </summary>
        /// <param name="basenumber">The base number.</param>
        /// <param name="exponent">The exponent.</param>
        /// <param name="count">The count.</param>
        /// <returns> Returns value. </returns>
        public static List<ComplexNumber> IntegerToPower(int basenumber, ComplexNumber exponent, int count) {
            List<ComplexNumber> list = new List<ComplexNumber>();
            if (basenumber == 0) {
                list.Add(new ComplexNumber(0, 0));
                return list;
            }

            double lg = Math.Log(Math.Abs(basenumber));
            for (int k = -count; k <= count; k++) {
                double p = 2 * k * Math.PI;
                ComplexNumber clg = new ComplexNumber(lg, p);
                ComplexNumber z = exponent.Multiply(clg);
                ComplexNumber w = Exponential(z);
                list.Add(w);
            }

            return list;
        }
        #endregion

        #region Derived Numbbers

        /// <summary>
        /// Real complement.
        /// </summary>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public ComplexNumber RealComplement() {
            return new ComplexNumber(-this.A, this.B);
        }

        /// <summary>
        /// Imaginary complement.
        /// </summary>
        /// <returns> Returns value. </returns>
        public ComplexNumber ImagComplement() {
            return new ComplexNumber(this.A, -this.B);
        }

        /// <summary>
        /// Complements this instance.
        /// </summary>
        /// <returns> Returns value. </returns>
        public ComplexNumber Complement() {
            return new ComplexNumber(-this.A, -this.B);
        }

        /// <summary>
        /// Swaps this instance.
        /// </summary>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public ComplexNumber Swap() {
            return new ComplexNumber(this.B, this.A);
        }
        #endregion

        #region Adding and Substracting
        /// <summary>
        /// Adds the number.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public ComplexNumber AddNumber(double x, double y) {
            return new ComplexNumber(this.A + x, this.B + y);
        }

        /// <summary>
        /// Adds the specified cn.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        /// <returns> Returns value. </returns>
        public ComplexNumber Add(ComplexNumber cn) {
            return new ComplexNumber(this.A + cn.A, this.B + cn.B);
        }

        /// <summary>
        /// Subtracts the specified cn.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        /// <returns> Returns value. </returns>
        public ComplexNumber Subtract(ComplexNumber cn) {
            return new ComplexNumber(this.A - cn.A, this.B - cn.B);
        }
        #endregion

        #region Multiplication and Division
        /// <summary>
        /// Reals the multiply.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns> Returns value. </returns>
        public ComplexNumber RealMultiply(double r) {
            return new ComplexNumber(this.A * r, this.B * r);
        }

        /// <summary>
        /// Multiplies the specified cn.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        /// <returns> Returns value. </returns>
        public ComplexNumber Multiply(ComplexNumber cn) {
            double poma = (this.A * cn.A) - (this.B * cn.B);
            double pomb = (this.A * cn.B) + (this.B * cn.A);
            return new ComplexNumber(poma, pomb);
        }

        /// <summary>
        /// Reals the divide.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns> Returns value. </returns>
        public ComplexNumber RealDivide(double r) {
            return new ComplexNumber(this.A / r, this.B / r);
        }

        /// <summary>
        /// Divides the specified cn.
        /// </summary>
        /// <param name="cn">The complex number.</param>
        /// <returns> Returns value. </returns>
        public ComplexNumber Divide(ComplexNumber cn) {
            double n = cn.Size;
            ComplexNumber ic = cn.ImagComplement();
            ComplexNumber m = this.Multiply(ic);
            ComplexNumber d = m.RealDivide(n);
            return d;
        }
        #endregion

        #region Roots and Powers
        /// <summary>
        /// Squares the root.
        /// </summary>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public ComplexNumber SquareRoot() {
            double v = this.Value;
            double poma = Math.Sqrt((v + this.A) / 2);
            double pomb = Math.Sqrt((v - this.A) / 2);
            double pb = (this.B > 0) ? pomb : -pomb;

            return new ComplexNumber(poma, pb);
        }

        /// <summary>
        /// Powers the specified k.
        /// </summary>
        /// <param name="k">The k.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public ComplexNumber Power(int k) {
            int i;
            if (k == 0) {
                return new ComplexNumber(1, 0);
            }

            ComplexNumber cn = new ComplexNumber(this.A, this.B);
            for (i = 1; i <= k - 1; i++) {
                cn = cn.Multiply(this);
            }

            return cn;
        }
        #endregion

        #region Exponential and logarithmic functions
        /// <summary>
        /// Reals the complement.
        /// </summary>
        /// <returns> Returns value. </returns>
        public ComplexNumber Logarithm() {
            double r = Math.Log(this.Norm);
            return new ComplexNumber(r, this.Argument);
        }

        /// <summary>
        /// Reals the complement.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns> Returns value. </returns>
        public List<ComplexNumber> Logarithms(int count) {
            double r = Math.Log(this.Norm);
            double arg = this.Argument;
            List<ComplexNumber> list = new List<ComplexNumber>();
            for (int k = -count; k <= count; k++) {
                double imag = arg + (2 * Math.PI * k);
                ComplexNumber z = new ComplexNumber(r, imag);
                list.Add(z);
            }

            return list;
        }

        /*
         * function PowRC(b: real; a:complex): complex;
            var
               c: complex;
               x,y: real;
            begin 
               x:=log(b);
               y:=x*a[1];
               x:=exp(a[0]*x);
               c[0]:=x*cos(y);
               c[1]:=x*sin(y); 
               return(c);
            end.
        */

        /// <summary>
        /// Toes the exponential.
        /// </summary>
        /// <returns> Returns value. </returns>
        public ComplexNumber Exponential() {
            return Exponential(this);
        }

        /// <summary>
        /// Complexes the power.
        /// </summary>
        /// <param name="exponent">The exponent.</param>
        /// <returns> Returns value. </returns>
        public ComplexNumber ComplexPower(ComplexNumber exponent) {
            if (this.Norm == 0) {
                return new ComplexNumber(0, 0);
            }

            ComplexNumber lge = this.Logarithm();
            ComplexNumber z = lge.Multiply(exponent);
            ComplexNumber r = z.Exponential();
            return r;
        }

        /// <summary>
        /// Exponentials the specified exponent.
        /// </summary>
        /// <param name="exponent">The exponent.</param>
        /// <param name="count">The count.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public List<ComplexNumber> ComplexPowers(ComplexNumber exponent, int count) {
            List<ComplexNumber> list = new List<ComplexNumber>();
            if (this.Norm == 0) {
                list.Add(new ComplexNumber(0, 0));
                return list;
            }

            List<ComplexNumber> logs = this.Logarithms(count);
            foreach (ComplexNumber log in logs) {
                ComplexNumber z = Exponential(log.Multiply(exponent));
                list.Add(z);
            }

            return list;
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
            s.AppendFormat("({0,12:F6},{1,12:F6})", this.A, this.B);
            return s.ToString();
        }
        #endregion
    }
}
