namespace domain.translate.Contracts.Business
{
	using domain.translate.Models;
	using System.Collections.Generic;
    using System.Globalization;

    public interface ITranslateBusiness
    {
        /// <summary>
        ///     Get word list translate using default O. S language. Necessary create xml file with culture. If don't exist, return empty list
        /// </summary>
        /// <returns>word list translated</returns>
        object TranslateAll();

        /// <summary>
        ///     Get word list translate using default O. S language. Necessary create xml file with culture. If don't exist, return empty list
        /// </summary>
        /// <returns>word list translated</returns>
        object TranslateAllByCulture(string culture);

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

        /// <summary>
        ///     Insert new key to dictionary for specific culture
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        object Insert(string culture, string key, string value);
		object Insert(BachTranslate translate);
	}
}
