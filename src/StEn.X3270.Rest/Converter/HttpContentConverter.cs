﻿namespace StEn.X3270.Rest.Converter
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Web;

    using Types;

    /// <summary>
    /// Class with helper methods to handle HTTP content conversion.
    /// </summary>
    internal static class HttpContentConverter
    {
        /// <summary>
        /// Converts parameters for HTTP requests.
        /// </summary>
        /// <param name="value">
        /// The object that should be converted.
        /// </param>
        /// <returns>
        /// The <see cref="HttpContent"/>.
        /// </returns>
        internal static HttpContent ConvertParameterValue(object value)
        {
            var typeName = value.GetType().Name;

            switch (typeName)
            {
                case "String":
                case "Int32":
                    return new StringContent(value.ToString(), Encoding.UTF8);
                case "Boolean":
                    return new StringContent((bool)value ? "true" : "false");
                case "FileToSend":
                    return new StreamContent(((FileToSend)value).Content);
                default:
                    throw new NotSupportedException();

                // return new StringContent(JsonConvert.SerializeObject(value));
            }
        }

        /// <summary>
        /// Constructs a GET query string.
        /// </summary>
        /// <param name="parameters">
        /// The parameters to convert.
        /// </param>
        /// <returns>
        /// The <see cref="string"/> in URL query manner.
        /// </returns>
        internal static string ConstructQueryString(NameValueCollection parameters)
        {
            return string.Join(
                "&",
                // ReSharper disable once StyleCop.SA1118
                parameters.Cast<string>()
                    .Select(name => string.Concat(name, "=", HttpUtility.UrlEncode(parameters[name])))
                    .ToArray());
        }
    }
}