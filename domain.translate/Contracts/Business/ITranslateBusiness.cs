namespace domain.translate.Contracts.Business
{
    using System.Collections.Generic;
    using System.Globalization;

    public interface ITranslateBusiness
    {

        /// <summary>
        ///     Get word translate using default O. S language. Necessary create xml file with culture. If don't exist, return string empty
        /// </summary>
        /// <param name="word">word to translate</param>
        /// <returns>word translated</returns>
        object Translate(string wordKey);

        /// <summary>
        ///     Get word translate using default O. S language. Necessary create xml file with culture. If don't exist, return string empty
        /// </summary>
        /// <param name="word">word to translate</param>
        /// <returns>word translated</returns>
        object Translate(string wordKey, string culture);

        /// <summary>
        ///     Get word list translate using default O. S language. Necessary create xml file with culture. If don't exist, return string empty
        /// </summary>
        /// <param name="wordKeys">word list to translate</param>
        /// <returns>Dictionary with key and value translated</returns>
        object Translate(List<string> wordKeys);

        /// <summary>
        ///     Get word translate using default O. S language. Necessary create xml file with culture. If don't exist, return string empty
        /// </summary>
        /// <param name="word">word list to translate</param>
        /// <returns>Dictionary with key and value translated</returns>
        object Translate(List<string> wordKeys, string culture);
    }
}
