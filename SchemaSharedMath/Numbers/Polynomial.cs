// <copyright file="Polynomial.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Numbers
{
    using JetBrains.Annotations;
    using System.Collections;
    using System.Text;

    /// <summary> Polynomial with integer quotients. </summary>
    public class Polynomial
    {
        /// <summary>
        /// Poly Hight.
        /// </summary>
        private const int PolyHight = 20;

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        public Polynomial()
        {
            this.A = new ArrayList(); // int[_this.Length];
            for (int i = 0; i < 2 * PolyHight; i++) {
                this.A.Add(0);
            }

            this.Length = PolyHight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        [UsedImplicitly]
        public Polynomial(int p0) : this()
        {
            this.A[0] = p0;
            this.Length = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        public Polynomial(int p0, int p1) : this()
        {
            this.A[0] = p0;
            this.A[1] = p1;
            this.Length = 2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        public Polynomial(int p0, int p1, int p2) : this()
        {
            this.A[0] = p0;
            this.A[1] = p1;
            this.A[2] = p2;
            this.Length = 3;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        public Polynomial(int p0, int p1, int p2, int p3) : this()
        {
            this.A[0] = p0;
            this.A[1] = p1;
            this.A[2] = p2;
            this.A[3] = p3;
            this.Length = 4;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        public Polynomial(int p0, int p1, int p2, int p3, int p4) : this()
        {
            this.A[0] = p0;
            this.A[1] = p1;
            this.A[2] = p2;
            this.A[3] = p3;
            this.A[4] = p4;
            this.Length = 5;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        /// <param name="p5">The p5.</param>
        public Polynomial(int p0, int p1, int p2, int p3, int p4, int p5) : this()
        {
            this.A[0] = p0;
            this.A[1] = p1;
            this.A[2] = p2;
            this.A[3] = p3;
            this.A[4] = p4;
            this.A[5] = p5;
            this.Length = 6;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        /// <param name="p5">The p5.</param>
        /// <param name="p6">The p6.</param>
        public Polynomial(int p0, int p1, int p2, int p3, int p4, int p5, int p6) : this()
        {
            this.A[0] = p0;
            this.A[1] = p1;
            this.A[2] = p2;
            this.A[3] = p3;
            this.A[4] = p4;
            this.A[5] = p5;
            this.A[6] = p6;
            this.Length = 7;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        /// <param name="p5">The p5.</param>
        /// <param name="p6">The p6.</param>
        /// <param name="p7">The p7.</param>
        public Polynomial(int p0, int p1, int p2, int p3, int p4, int p5, int p6, int p7) : this()
        {
            this.A[0] = p0;
            this.A[1] = p1;
            this.A[2] = p2;
            this.A[3] = p3;
            this.A[4] = p4;
            this.A[5] = p5;
            this.A[6] = p6;
            this.A[7] = p7;
            this.Length = 8;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        /// <param name="p5">The p5.</param>
        /// <param name="p6">The p6.</param>
        /// <param name="p7">The p7.</param>
        /// <param name="p8">The p8.</param>
        public Polynomial(int p0, int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8) : this()
        {
            this.A[0] = p0;
            this.A[1] = p1;
            this.A[2] = p2;
            this.A[3] = p3;
            this.A[4] = p4;
            this.A[5] = p5;
            this.A[6] = p6;
            this.A[7] = p7;
            this.A[8] = p8;
            this.Length = 9;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        /// <param name="p5">The p5.</param>
        /// <param name="p6">The p6.</param>
        /// <param name="p7">The p7.</param>
        /// <param name="p8">The p8.</param>
        /// <param name="p9">The p9.</param>
        public Polynomial(int p0, int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8, int p9) : this()
        {
            this.A[0] = p0;
            this.A[1] = p1;
            this.A[2] = p2;
            this.A[3] = p3;
            this.A[4] = p4;
            this.A[5] = p5;
            this.A[6] = p6;
            this.A[7] = p7;
            this.A[8] = p8;
            this.A[9] = p9;
            this.Length = 10;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial" /> class.
        /// </summary>
        /// <param name="p">The p.</param>
        public Polynomial(Polynomial p) : this()
        {
            for (int i = 0; i < this.Length; i++) {
                var a = p.A[i];
                this.A[i] = a;
            }

            //// for (int i=0; i< _this.Length; i++) this.A[i]=(int)anA[i];
            //// this.A = anA;
            this.Length = p.Length;
        }

        /// <summary>
        /// Gets or sets the A.
        /// </summary>
        /// <value>
        /// The A.
        /// </value>
        public ArrayList A { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; set; }

        /// <summary>
        /// Nullifies this instance.
        /// </summary>
        [UsedImplicitly]
        public void Nullify()
        {
            for (int i = 0; i < this.Length; i++) {
                this.A[i] = 0;
            }
        }

        /// <summary>
        /// Integer multiply.
        /// </summary>
        /// <param name="q">The q.</param>
        [UsedImplicitly]
        public void IntMultiply(int q)
        {
            for (int i = 0; i < this.Length; i++) {
                this.A[i] = (int)this.A[i] * q;
            }
        }

        /// <summary>
        /// Ints the divide.
        /// </summary>
        /// <param name="q">The q.</param>
        public void IntDivide(int q)
        {
            for (int i = 0; i < this.Length; i++) {
                this.A[i] = (int)this.A[i] / q;
            }
        }

        /// <summary>
        /// Ints the modulo.
        /// </summary>
        /// <param name="m">The m.</param>
        public void IntModulo(int m)
        {
            for (int i = 0; i < this.Length; i++) {
                this.A[i] = (int)this.A[i] % m;
                if ((int)this.A[i] < 0) {
                    this.A[i] = (int)this.A[i] + m;
                }
            }
        }

        /// <summary>
        /// Adds the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        [UsedImplicitly]
        public void Add(Polynomial b)
        {
            for (int i = 0; i < this.Length; i++) {
                this.A[i] = (int)b.A[i] + (int)this.A[i];
            }
        }

        /// <summary>
        /// Minuses the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        public void Minus(Polynomial b)
        {
            for (int i = 0; i < this.Length; i++) {
                this.A[i] = (int)this.A[i] - (int)b.A[i];
            }
        }

        /// <summary>
        /// Multiplies the with.
        /// </summary>
        /// <param name="b">The b.</param>
        public void MultiplyWith(Polynomial b)
        {
            this.Length += b.Length - 1; //// 2021/01
            Polynomial c = new Polynomial();  //// c.Nullify();
            for (int i = 0; i < PolyHight; i++) {
                for (int j = 0; j < PolyHight; j++) {
                    c.A[i + j] = (int)c.A[i + j] + ((int)this.A[i] * (int)b.A[j]);
                }
            }

            this.A = c.A;
        }

        /// <summary>
        /// Powers the specified k.
        /// </summary>
        /// <param name="k">The k.</param>
        [UsedImplicitly]
        public void Power(int k)
        {
            Polynomial b = new Polynomial(this);
            for (int i = 2; i <= k; i++) {
                this.MultiplyWith(b);
            }
        }

        /// <summary>
        /// Divides the by.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns>Returns value.</returns>
        public Polynomial DivideBy(Polynomial b)
        {
            Polynomial a = new Polynomial(this);  ////  c.Nullify();
            Polynomial c = new Polynomial();  ////  c.Nullify();
            for (int i = 0; i < PolyHight; i++) {
                c.A[i] = (int)a.A[i] / (int)b.A[0];
                for (int j = 0; j < PolyHight; j++) {
                    a.A[i + j] = (int)a.A[i + j] - ((int)c.A[i] * (int)b.A[j]);
                }
            }

            return c;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < this.Length; i++) {
                var a = this.A[i];
                sb.AppendFormat("{0,2}", a);
                sb.Append(", ");
            }

            return sb.ToString();
        }
    }
}