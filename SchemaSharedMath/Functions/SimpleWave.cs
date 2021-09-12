// <copyright file="SimpleWave.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Functions
{
    using System;
    using System.Numerics;
    using System.Text;

    /// <summary>
    /// Simple Wave.
    /// </summary>
    public class SimpleWave
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleWave"/> class.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="extent">The extent.</param>
        /// <param name="fftResult">The FFT result.</param>
        /// f = sum Magnitude * sin(2 * Math.PI * time * Frequency + Phase)
        public SimpleWave(double frequency, int extent, Complex fftResult)
        {
            this.Frequency = frequency;
            this.Period = 1.000 / frequency;
            this.Phase = (Math.Atan2(fftResult.Imaginary, fftResult.Real) * 180) / Math.PI; ////  phase information
            this.Magnitude = extent * Math.Sqrt((fftResult.Real * fftResult.Real) + (fftResult.Imaginary * fftResult.Imaginary));
        }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        public double Frequency { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public double Period { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        /// <value>
        /// The phase.
        /// </value>
        public double Phase { get; set; }

        /// <summary>
        /// Gets or sets the magnitude.
        /// </summary>
        /// <value>
        /// The magnitude.
        /// </value>
        public double Magnitude { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Period:    {0,6:F3}\n", this.Period);
            sb.AppendFormat("Magnitude: {0,6:F3}\n", this.Magnitude);
            sb.AppendFormat("Phase:     {0,6:F3}", this.Phase);
            return sb.ToString();
        }
    }
}
