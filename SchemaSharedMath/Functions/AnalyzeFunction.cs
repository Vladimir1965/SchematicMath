// <copyright file="AnalyzeFunction.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedMath.Functions
{
    /// <summary>
    /// Analyze Function.
    /// </summary>
    public class AnalyzeFunction
    {
        /// <summary>
        /// Gets or sets the CSV path.
        /// </summary>
        /// <value>
        /// The CSV path.
        /// </value>
        public string CsvPath { get; set; }

        /// <summary>
        /// Analyzes the solar.
        /// </summary>
        public void AnalyzeSolar() {
            this.InitPath();

            var g1 = new SimpleGraph(this.CsvPath);
            var v1 = g1.SimpleIntegral();

            //// var fromPeriod = 3.0; var toPeriod = 9.0; var amplitude = 70; //// ZharkovaRed
            //// var fromPeriod = 18.0; var toPeriod = 21.0; var amplitude = 200; //// ZharkovaRed
            //// var fromPeriod = 7.0; var toPeriod = 11.0; var amplitude = 180; //// ZharkovaBlue
            //// var fromPeriod = 20.0; var toPeriod = 40.0; var amplitude = 630; //// ZharkovaBlue
            //// var fromPeriod = 10.0; var toPeriod = 20.0; var amplitude = 240; //// ZharkovaBlue
            var fromPeriod = 3.0; 
            var toPeriod = 25.0; 
            var amplitude = 10; //// ZharkovaSum 17-25,900
            //// var fromPeriod = 6.0; var toPeriod = 8.0; var amplitude = 210; //// ZharkovaSum
            //// var fromPeriod = 18.0; var toPeriod = 22.0; var amplitude = 5000; //// Sunspot1
            //// var fromPeriod = 9.0; var toPeriod = 13.0; var amplitude = 200; //// Sunspot2
            //// var fromPeriod = 85; var toPeriod = 125; var amplitude = 166; //// Sunspot400

            double minValue = +1000000;
            double minPhase, minPeriod;
            for (double period = fromPeriod; period < toPeriod; period += 0.1) {
                for (double phase = 2000; phase < 2000 + period; phase += 0.1) {
                    var g = g1.AddSinus(-amplitude, period, phase);
                    var v = g.SimpleIntegral();
                    if (v < minValue) {
                        minValue = v;
                        minPeriod = period;
                        minPhase = phase;
                    }
                }
            }

            //// var g2b = g1.AddSinus(166, 93, 0)
            //// var v2b = g2.SimpleIntegral()
            //// var g3 = g2.AddSinus(+166, 93, 40)
            //// var v3 = g3.SimpleIntegral()

            //// var numberOfPoints = 4096; //// 1024
            //// var dgraph = new DiscreteGraph(graph, numberOfPoints)
        }

        /// <summary>
        /// Initializes the path.
        /// </summary>
        private void InitPath() {
            ////this.CsvPath = @"c:\Private\FOURIER-2020\Sunspot1.csv";
            //// this.CsvPath = @"c:\Private\FOURIER-2020\Sunspot2.csv";
            //// this.CsvPath = @"c:\Private\FOURIER-2020\Sunspot3.csv";
            //// this.CsvPath = @"c:\Private\FOURIER-2020\zharkova2.csv";
            //// this.CsvPath = @"c:\Private\FOURIER-2020\zharkova2blue.csv";
            //// this.CsvPath = @"c:\Private\FOURIER-2020\zharkova2red.csv";
            this.CsvPath = @"c:\Private\FOURIER-2020\zharkovasum.csv";
            //// this.CsvPath = @"c:\Private\FOURIER-2020\SolarChen2015.csv";
            //// this.CsvPath = @"c:\Private\FOURIER-2020\Carbon14Gray.csv";
            //// this.CsvPath = @"c:\Private\FOURIER-2020\Sunspot400.csv";
            //// this.CsvPath = @"c:\Private\FOURIER-2020\test.csv";
        }
    }
}
