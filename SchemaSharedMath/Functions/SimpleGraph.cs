// <copyright file="SimpleGraph.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Functions
{
    using Microsoft.VisualBasic.FileIO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Simple Graph.
    /// </summary>
    public class SimpleGraph
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleGraph"/> class.
        /// </summary>
        /// <param name="givenCsvPath">The given CSV path.</param>
        public SimpleGraph(string givenCsvPath) {
            this.CsvPath = givenCsvPath;
            if (this.LoadCSV()) {
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleGraph"/> class.
        /// </summary>
        public SimpleGraph()
        {
            this.Points = new List<Complex>();
        }

        #region Properties
        /// <summary>
        /// Gets or sets the CSV path.
        /// </summary>
        /// <value>
        /// The CSV path.
        /// </value>
        public string CsvPath { get; set; }

        /// <summary>
        /// Gets or sets the first point.
        /// </summary>
        /// <value>
        /// The first point.
        /// </value>
        public Complex FirstPoint { get; set; }

        /// <summary>
        /// Gets or sets the last point.
        /// </summary>
        /// <value>
        /// The last point.
        /// </value>
        public Complex LastPoint { get; set; }

        /// <summary>
        /// Gets or sets the extent.
        /// </summary>
        /// <value>
        /// The extent.
        /// </value>
        public int Extent { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public double Duration { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public List<Complex> Points { get; set; }
        #endregion

        /// <summary>
        /// Simples the integral.
        /// </summary>
        /// <returns>Returns value.</returns>
        public double SimpleIntegral()
        {
            double value = 0;
            for (int i = 1; i < this.Points.Count; i++)
            {
                value += (this.Points[i].Imaginary + this.Points[i - 1].Imaginary) / 2 * (this.Points[i].Real - this.Points[i - 1].Real);
            }

            return value;
        }

        /// <summary>
        /// Finds the value.
        /// </summary>
        /// <param name="givenX">The given x.</param>
        /// <returns>Returns value.</returns>
        public double FindValue(double givenX)
        {
            var rpoints = this.Points.Reverse<Complex>();
            var point1 = (from p in rpoints where p.Real <= givenX select p).FirstOrDefault();
            var point2 = (from p in this.Points where p.Real > givenX select p).FirstOrDefault();
            var deltax = point2.Real - point1.Real;
            var deltay = point2.Imaginary - point1.Imaginary;
            var value = point1.Imaginary + (((givenX - point1.Real) * deltay) / deltax);
            return value;
        }

        /// <summary>
        /// Adds the sinus.
        /// </summary>
        /// <param name="givenAmplitude">The given amplitude.</param>
        /// <param name="givenPeriod">The given period.</param>
        /// <param name="givenPhase">The given phase.</param>
        /// <returns>Returns value.</returns>
        public SimpleGraph AddSinus(double givenAmplitude, double givenPeriod, double givenPhase)
        {
            var ngraph = new SimpleGraph();
            foreach (var point in this.Points)
            {
                var fvalue = givenAmplitude * Math.Sin(2 * Math.PI * ((point.Real - givenPhase) / givenPeriod));
                var npoint = new Complex(point.Real, point.Imaginary + fvalue);
                ngraph.Points.Add(npoint);
            }

            ngraph.CompleteInit();
            return ngraph;
        }

        /// <summary>
        /// Completes the initialize.
        /// </summary>
        public void CompleteInit()
        {
            this.FirstPoint = this.Points.FirstOrDefault();
            this.LastPoint = this.Points.LastOrDefault();
            this.Extent = this.Points.Count;
            this.Duration = this.LastPoint.Real - this.FirstPoint.Real;
        }

        /// <summary>
        /// Loads the CSV.
        /// </summary>
        /// <returns>Returns value.</returns>
        private bool LoadCSV()
        {
            this.Points = new List<Complex>();
            double d;

            using (TextFieldParser parser = new TextFieldParser(this.CsvPath)) {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData) {
                    //// Processing row
                    string[] fields = parser.ReadFields();
                    //// foreach (string field in fields)   {   }
                    var s1 = fields[0].Replace("\"", string.Empty);
                    var s2 = fields[1].Replace("\"", string.Empty);

                    if (!double.TryParse(s1, out d) || !double.TryParse(s2, out d)) {
                        continue;
                    }

                    var x = double.Parse(s1);
                    var y = double.Parse(s2);
                    var c = new Complex(x, y);
                    this.Points.Add(c);
                }
            }

            if (this.Points.Count < 2) {
                return false;
            }

            this.CompleteInit();
            return true;
        }
    }
}
