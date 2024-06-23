// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace PVATestFramework.Console.Helpers.Extensions
{
    internal static class GenericExtensions
    {
        internal static bool IsOneOf<T>(this T obj, params T[] these)
        {
            return these.Contains(obj);
        }

        internal static JObject TryParseJObject(this string inputString)
        {
            if (inputString?.TrimStart().StartsWith("{") == true)
            {
                try
                {
                    return JObject.Parse(inputString);
                }
                catch
                {
                }
            }

            return null;
        }

        internal static JObject ToJObject(this object input, bool shouldParseStrings = false)
        {
            if (input is string inputString)
            {
                if (shouldParseStrings)
                {
                    return inputString.TryParseJObject();
                }
            }
            else if (input is JObject inputJObject)
            {
                return inputJObject;
            }
            else if (input != null)
            {
                return JToken.FromObject(input) as JObject;
            }

            return null;
        }

        /// <summary>
        /// Used to unescape a string until it contains no more escape sequences or characters. Allows stings with differing levels of escaping to be compared
        /// </summary>
        /// <param name="input">The string to be completely unescaped</param>
        /// <returns>The completely unescaped version of the string</returns>
        internal static string CompletelyUnescape(this string input)
        {
            string inp = input;
            string unesc = Regex.Unescape(inp);
            while (inp.GetHashCode() != unesc.GetHashCode())
            {
                inp = unesc;
                unesc = Regex.Unescape(inp);
            }

            return unesc;
        }
    }
}
