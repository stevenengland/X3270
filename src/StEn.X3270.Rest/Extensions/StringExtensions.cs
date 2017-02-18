namespace StEn.X3270.Rest.Extensions
{
    using System;

    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Takes a mask in ASCII representation (new line separated lines of ASCII symbols) and transforms it to a 2d array of ASCII symbols.
        /// </summary>
        /// <param name="mask">
        /// The mask.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>string[][]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static string[][] ToAsciiMatrix(this string mask)
        {
            var lines = mask.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            var returnStr = new string[lines.Length][];
            for (var i = 0; i < lines.Length; i++)
            {
                returnStr[i] = new string[lines[i].Length];
                for (var j = 0; j < lines[i].Length; j++)
                {
                    returnStr[i][j] = lines[i][j].ToString();
                }
            }

            return returnStr;
        }

        /// <summary>
        /// Takes a mask in byte representation (new line separated lines of bytes) to a 2d array of byte symbols.
        /// </summary>
        /// <param name="mask">
        /// The mask.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>string[][]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static string[][] ToByteMatrix(this string mask)
        {
            var lines = mask.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            var returnStr = new string[lines.Length][];
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(' ');
                returnStr[i] = line;
            }

            return returnStr;
        }

    }
}