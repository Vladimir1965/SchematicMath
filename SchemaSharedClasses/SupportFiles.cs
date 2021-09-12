// <copyright file="SupportFiles.cs" company="Traced-Ideas, Czech republic">
// Copyright (c) 1990-2021 All Right Reserved
// </copyright>
// <author></author>
// <email></email>
// <date>2021-09-01</date>
// <summary>Part of Schematic Math</summary>

namespace SchemaSharedClasses
{
    using System.IO;
    using JetBrains.Annotations;

    /// <summary>
    /// Support Files.
    /// </summary>
    public static class SupportFiles {
        #region FileToString

        /// <summary> Read File. </summary>
        /// <returns> Returns value. </returns>
        /// <param name="filePath">File path.</param>
        /// <returns> Returns value. </returns>
        [UsedImplicitly]
        public static string FileToString(string filePath) {
            using (StreamReader sr = new StreamReader(filePath)) {
                string s = sr.ReadToEnd();
                //// sr.Close();
                return s;
            }
        }

        /// <summary> Write string to file. </summary>
        /// <param name="content">Content of file.</param>
        /// <param name="filePath">File path.</param>
        public static void StringToFile(string content, string filePath) {
            using (StreamWriter sw = new StreamWriter(filePath)) {
                sw.Write(content);
                //// sw.Close();
            }
        }

        #endregion
    }
}
