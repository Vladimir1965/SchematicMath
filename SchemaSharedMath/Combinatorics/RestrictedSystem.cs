// <copyright file="RestrictedSystem.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Combinatorics
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Text;
    using JetBrains.Annotations;
    using SchemaSharedMath.Numbers;

    /// <summary>
    /// System variety.
    /// </summary>
    //// [Serializable]
    public class RestrictedSystem : object {
        /// <summary>
        /// Max Order.
        /// </summary>
        private const int MaxOrder = 10000;

        /// <summary>
        /// Self Instances.
        /// </summary>
        private int[] selfInstances, selfClasses;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictedSystem" /> class.
        /// </summary>
        [UsedImplicitly]
        public RestrictedSystem() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictedSystem" /> class.
        /// </summary>
        /// <param name="degree">The degree.</param>
        /// <param name="size">The size.</param>
        public RestrictedSystem(int degree, int size) {
            this.Degree = degree;
            this.Size = size;
            this.Order = this.GuessOrder();
            this.Valid = this.Order < MaxOrder;
            this.InitData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictedSystem" /> class.
        /// </summary>
        /// <param name="givenDegree">A degree.</param>
        /// <param name="givenOrder">An order.</param>
        /// <param name="givenType">A type.</param>
        public RestrictedSystem(int givenDegree, int givenOrder, char givenType) {
            this.Degree = givenDegree;
            this.Order = givenOrder;
            switch (givenType) {
                case 'G': 
                    this.Size = (int)Math.Pow(this.Degree, this.Order) - 1;
                    break;
                case 'M': 
                    this.Size = (int)((Math.Pow(this.Degree, this.Order) - 1) / (this.Degree - 1));
                    break;
            }

            this.InitData();
        }

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RestrictedSystem"/> is valid.
        /// </summary>
        /// <value>
        /// <c>True</c> if valid; otherwise, <c>false</c>.
        /// </value>
        public bool Valid { get; set; }

        /// <summary>
        /// Gets or sets the degree.
        /// </summary>
        /// <value>
        /// The degree.
        /// </value>
        public int Degree { get; set; }

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
        /// Gets or sets Total Instances.
        /// </summary>
        /// <value> Property description. </value>
        public int TotalInstances { get; set; }

        /// <summary>
        /// Gets or sets Total Classes.
        /// </summary>
        /// <value>
        /// The total classes.
        /// </value>
        public int TotalClasses { get; set; }
        #endregion

        /*
        public int GuessOrder()  {
            int z = this.Degree % this.Size;   int k=1; bool lbreak = false;
            while ((z!=1) && !lbreak) {
                k++; z = (z*this.Degree) % this.Size; if (k>200) { lbreak = true; k=0; }
            }
            return (int)k;
        } */

        /// <summary>
        /// Inits the data.
        /// </summary>
        public void InitData() {
            this.selfInstances = new int[MaxOrder]; 
            this.selfClasses = new int[MaxOrder];
            this.TotalInstances = 0; 
            this.TotalClasses = 0;
        }

        /// <summary>
        /// Guesses the order.
        /// </summary>
        /// <returns> Returns value. </returns>
        public int GuessOrder() {
            BitArray marked = new BitArray(this.Size + 1, false);
            int g = 1;
            int kmax = 0;
            while (g < this.Size) {
                if (!marked.Get(g)) {
                    int u = g;
                    int k = 0;
                    while (u != 0 && u < this.Size && !marked.Get(u)) {
                        k++;
                        marked.Set(u, true);
                        u = (u * this.Degree) % this.Size;
                    }

                    if (k > kmax) {
                        kmax = k;
                    }
                }

                g = g + 1;
            }

            return kmax;
        }

        /// <summary>
        /// Generate all classes.
        /// </summary>
        /// <param name="showClasses">If set to <c>true</c> [show classes].</param>
        /// <param name="showInstance">If set to <c>true</c> [show instance].</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        public string GenerateClasses(bool showClasses, bool showInstance) {
            BitArray marked = new BitArray(this.Size + 1, false);
            StringBuilder s = new StringBuilder(string.Empty);
            s.AppendFormat("n={0,8} k={1,8} r={2,8} \r\r", this.Degree, this.Order, this.Size);
            s.AppendFormat(CultureInfo.CurrentCulture, "{0,8}\r", 0);
            int g = 1; 
            this.TotalClasses++; 
            this.TotalInstances++; 
            this.selfClasses[1]++; 
            this.selfInstances[1]++;
            while (g < this.Size) {
                if (!marked.Get(g)) {
                    int u = g; 
                    this.TotalClasses++;
                    int k = 0;
                    while (u != 0 && u < this.Size && !marked.Get(u)) {
                        this.TotalInstances++;
                        if ((k == 0 && showClasses) || showInstance) {
                            s.AppendFormat(CultureInfo.CurrentCulture, "{0,8}", u); // % 12
                        }

                        k++;
                        marked.Set(u, true);
                        u = (u * this.Degree) % this.Size;
                    }

                    this.selfClasses[k]++; 
                    this.selfInstances[k] += k;
                    if (showClasses) {
                        s.AppendFormat("\r");
                    }
                }

                g = g + 1;
            }

            this.TotalClasses++;
            this.TotalInstances++;
            this.selfClasses[1]++;
            this.selfInstances[1]++;
            
            s.AppendFormat(CultureInfo.CurrentCulture, "{0,8}\r\r", this.Size);
            /*
            s.AppendFormat("Level:\t        ");
            for (int d = 1; d <= this.Order; d++) {
                s.AppendFormat(CultureInfo.CurrentCulture, "{0,8}", d);
            }

            s.AppendFormat("           Total \r");
            s.AppendFormat("------------------------------------------------------------------\r");
            s.AppendFormat("Classes: \t");
            for (int d = 1; d <= this.Order; d++) {
                s.AppendFormat(CultureInfo.CurrentCulture, "{0,8}", this.selfClasses[d]);
            }
            */
            s.AppendFormat(CultureInfo.CurrentCulture, "\t*{0,8}*\r", this.TotalClasses);
            /*
            s.AppendFormat("Instances:\t");
            for (int d = 1; d <= this.Order; d++) {
                s.AppendFormat(CultureInfo.CurrentCulture, "{0,8}", this.selfInstances[d]);
            }
            */
            s.AppendFormat(CultureInfo.CurrentCulture, "\t*{0,8}*\r", this.TotalInstances);
            return s.ToString();
        }

        /// <summary>
        /// Generate all classes.
        /// </summary>
        /// <param name="showClasses">If set to <c>true</c> [show classes].</param>
        /// <param name="showInstance">If set to <c>true</c> [show instance].</param>
        /// <returns>
        /// Returns value.
        /// </returns>
        public string GenerateClassesAndPolynomials(bool showClasses, bool showInstance) {
            BitArray marked = new BitArray(this.Size + 1, false);
            StringBuilder s = new StringBuilder(string.Empty);
            s.AppendFormat("n={0,8} k={1,8} r={2,8} \r\r", this.Degree, this.Order, this.Size);
            s.AppendFormat(CultureInfo.CurrentCulture, "{0,8}\r", 0);
            int g = 1;
            this.TotalClasses++;
            this.TotalInstances++;
            this.selfClasses[1]++;
            this.selfInstances[1]++;
            while (g < this.Size) {
                if (!marked.Get(g)) {
                    int u = g;
                    this.TotalClasses++;
                    int k = 0;
                    var polynom = new Polynomial(1);
                    while (u != 0 && u < this.Size && !marked.Get(u)) {
                        this.TotalInstances++;
                        if ((k == 0 && showClasses) || showInstance) {
                            s.AppendFormat(CultureInfo.CurrentCulture, "{0,8}", u); // % 12
                        }

                        k++;
                        marked.Set(u, true);
                        u = (u * this.Degree) % this.Size;
                        var expression = new Polynomial(1, -u);
                        /* var p = Math.Pow(2, k);
                        var gp = (int)Math.Pow(g, p);
                        var expression = new Polynomial(1, -gp);
                        expression.Power(k);
                        expression.IntModulo(this.Degree); */
                        polynom.MultiplyWith(expression);
                    }

                    this.selfClasses[k]++;
                    this.selfInstances[k] += k;
                    s.AppendFormat(CultureInfo.CurrentCulture, "\t{0}", polynom);
                    polynom.IntModulo(this.Degree);
                    s.AppendFormat(CultureInfo.CurrentCulture, "\t{0}", polynom);
                    if (showClasses) {
                        s.AppendFormat("\r");
                    }
                }

                g = g + 1;
            }

            this.TotalClasses++;
            this.TotalInstances++;
            this.selfClasses[1]++;
            this.selfInstances[1]++;

            s.AppendFormat(CultureInfo.CurrentCulture, "{0,8}\r\r", this.Size);
            s.AppendFormat(CultureInfo.CurrentCulture, "\t*{0,8}*\r", this.TotalClasses);
            s.AppendFormat(CultureInfo.CurrentCulture, "\t*{0,8}*\r", this.TotalInstances);
            return s.ToString();
        }
    }
}