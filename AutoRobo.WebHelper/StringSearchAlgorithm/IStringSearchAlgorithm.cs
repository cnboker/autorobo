using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.WebHelper.StringSearchAlgorithm
{
    /// <summary>
    /// Interface containing all methods to be implemented
    /// by string search algorithm
    /// </summary>
    public interface IStringSearchAlgorithm
    {
        #region Methods & Properties

        /// <summary>
        /// List of keywords to search for
        /// </summary>
        string[] Keywords { get; set; }


        /// <summary>
        /// Searches passed text and returns all occurrences of any keyword
        /// </summary>
        /// <param name="text">Text to search</param>
        /// <returns>Array of occurrences</returns>
        StringSearchResult[] FindAll(string text);

        /// <summary>
        /// Searches passed text and returns first occurrence of any keyword
        /// </summary>
        /// <param name="text">Text to search</param>
        /// <returns>First occurrence of any keyword (or StringSearchResult.Empty if text doesn't contain any keyword)</returns>
        StringSearchResult FindFirst(string text);

        /// <summary>
        /// Searches passed text and returns true if text contains any keyword
        /// </summary>
        /// <param name="text">Text to search</param>
        /// <returns>True when text contains any keyword</returns>
        bool ContainsAny(string text);

        #endregion
    }
}
