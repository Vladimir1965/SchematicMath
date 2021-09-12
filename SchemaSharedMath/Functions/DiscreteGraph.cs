// <copyright file="DiscreteGraph.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Functions
{
    using SchemaSharedClasses;
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;

    /// <summary>
    /// Discrete Graph.
    /// </summary>
    public class DiscreteGraph
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteGraph"/> class.
        /// </summary>
        /// <param name="givenGraph">The given graph.</param>
        /// <param name="numberOfPoints">The number of points.</param>
        public DiscreteGraph(SimpleGraph givenGraph, int numberOfPoints)
        {
            this.Graph = givenGraph;
            var fileNameDiscrete = givenGraph.CsvPath.Replace(".csv", "Discrete.csv");
            var fileNameResult = givenGraph.CsvPath.Replace(".csv", "Result.csv");

            if (this.Discretization(numberOfPoints))
            {
                this.SaveCSV(fileNameDiscrete);
            }

            var data = this.DiscretePoints.ToArray();
            //// var data = graph.Points.ToArray();

            /*
            //// One dimensional Discrete Fourier Transform
            //// AForge.Math.FourierTransform.DFT(data, FourierTransform.Direction.Forward);
            var data2 = new Complex[100,100];

            //// Two dimensional Discrete Fourier Transform
            AForge.Math.FourierTransform.DFT2(data2, FourierTransform.Direction.Forward);

            //// Two dimensional Fast Fourier Transform
            AForge.Math.FourierTransform.FFT2(data2, FourierTransform.Direction.Forward);
            */
            
            //// One dimensional Fast Fourier Transform
            //// AForge.Math.FourierTransform.FFT(data, FourierTransform.Direction.Forward);
            //// AForge.Math.FourierTransform.DFT(data, FourierTransform.Direction.Forward);

            this.Results = new List<SimpleWave>();
            var halfNumber = numberOfPoints / 2;
            for (int i = 1; i <= halfNumber; i++)
            {
                double frequency = i * 1.000 / this.Graph.Duration;
                Complex r = data[i];
                var c = new SimpleWave(frequency, this.Graph.Extent, r);
                this.Results.Add(c);
            }

            //// var x = this.Results.ToArray();
            this.SaveResultsToCSV(fileNameResult);
        }

        #region Properties
        /// <summary>
        /// Gets or sets the discrete points.
        /// </summary>
        /// <value>
        /// The discrete points.
        /// </value>
        public List<Complex> DiscretePoints { get; set; }

        /// <summary>
        /// Gets or sets the graph.
        /// </summary>
        /// <value>
        /// The graph.
        /// </value>
        public SimpleGraph Graph { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public List<SimpleWave> Results { get; set; }
        #endregion

        /// <summary>
        /// Discretizations the specified number of points.
        /// </summary>
        /// <param name="numberOfPoints">The number of points.</param>
        /// <returns>Returns value.</returns>
        public bool Discretization(int numberOfPoints)
        {
            this.DiscretePoints = new List<Complex>();
            var realStep = this.Graph.Duration / (numberOfPoints - 1);
            for (int i = 0; i < numberOfPoints; i++)
            {
                //// var c = new Complex(y, 0);

                var x = i > 0 ? this.Graph.FirstPoint.Real + (i * realStep) : 0;
                var y = i > 0 ? this.Graph.FindValue(x) : 0;
                var p = new Complex(Math.Round(y, 3), 0);
                this.DiscretePoints.Add(p);
            }

            return true;
        }

        /// <summary>
        /// Saves the CSV.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>Returns value.</returns>
        public bool SaveCSV(string filePath)
        {
            var sb = new StringBuilder();
            foreach (var p in this.DiscretePoints)
            {
                sb.AppendFormat("'{0}','{1}'\n", p.Real, p.Imaginary);
            }

            SupportFiles.StringToFile(sb.ToString(), filePath);
            return true;
        }

        /// <summary>
        /// Saves the results to CSV.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>Returns value.</returns>
        public bool SaveResultsToCSV(string filePath)
        {
            var sb = new StringBuilder();
            foreach (var p in this.Results)
            {
                sb.AppendFormat("{0,6:F3};{1,6:F3}\n", p.Period, p.Magnitude);
            }

            SupportFiles.StringToFile(sb.ToString(), filePath);
            return true;
        }
    }
}
