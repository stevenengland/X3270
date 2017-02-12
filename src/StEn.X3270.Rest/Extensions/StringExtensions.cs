namespace StEn.X3270.Rest.Extensions
{
    using System;

    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a Newline separated string into a two dimensional array of strings.
        /// Assumes that each line has the same count of characters as normal in terminals.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// A 2 dimensional array of strings.
        /// </returns>
        public static string[][] To2DimensionalTerminalArray(this string input)
        {
            var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
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
    }
}