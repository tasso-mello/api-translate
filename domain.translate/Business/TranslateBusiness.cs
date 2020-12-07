namespace domain.translate.Business
{
    using domain.translate.Configurations;
    using domain.translate.Contracts.Business;
    using domain.translate.Utilities;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class TranslateBusiness : ITranslateBusiness
    {
        private readonly ILogger<string> _logger;
        private readonly TranslateConfigurations _translateConfiguration;

        /// <summary>
        ///     
        /// </summary>
        /// <param name="logger"></param>
        public TranslateBusiness(ILogger<string> logger)
        {
            _logger = logger;
            _translateConfiguration = new TranslateConfigurations();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="wordKey"></param>
        /// <returns></returns>
        public object Translate(string wordKey)
        {
            try
            {
                return Messages.GenerateGenericSuccessObjectMessage("Translate", GetXmlSection(wordKey), 200);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe a chave '{wordKey}' no arquivo de tradução.", 404, wordKey);
            }
            catch(DirectoryNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe o caminho do arquivo de tradução.", 404);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return (e.Message != null && e?.InnerException?.Message != null) ? Messages.GenerateGenericErrorMessage(e.Message, e.InnerException.Message) : Messages.GenerateGenericErrorMessage(e.Message ?? e.InnerException.Message);
            }
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="wordKey"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Translate(string wordKey, string strCulture)
        {
            try
            {
                var culture = !string.IsNullOrEmpty(strCulture) ? new CultureInfo(strCulture) : throw new CultureNotFoundException();

                return Messages.GenerateGenericSuccessObjectMessage("Translate", GetXmlSection(wordKey, culture.Name), 200);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe o arquivo de tradução '{strCulture}'.", 404, wordKey);
            }
            catch (CultureNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe a cultura '{strCulture}'.", 404, wordKey);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe a chave '{wordKey}' no arquivo de tradução.", 404, wordKey);
            }
            catch (DirectoryNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe o caminho do arquivo de tradução.", 404, wordKey);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return (e.Message != null && e?.InnerException?.Message != null) ? Messages.GenerateGenericErrorMessage(e.Message, e.InnerException.Message) : Messages.GenerateGenericErrorMessage(e.Message ?? e.InnerException.Message);
            }
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="wordKeys"></param>
        /// <returns></returns>
        public object Translate(List<string> wordKeys)
        {
            try
            {
                return Messages.GenerateGenericSuccessObjectMessage("Translate", GetXmlSection(wordKeys), 200);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe o arquivo tradução.", 404, wordKeys);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existem as chaves no arquivo de tradução.", 404, wordKeys);
            }
            catch (DirectoryNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe o caminho do arquivo de tradução.", 404);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return (e.Message != null && e?.InnerException?.Message != null) ? Messages.GenerateGenericErrorMessage(e.Message, e.InnerException.Message) : Messages.GenerateGenericErrorMessage(e.Message ?? e.InnerException.Message);
            }
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="wordKeys"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Translate(List<string> wordKeys, string strCulture)
        {
            try
            {
                var culture = !string.IsNullOrEmpty(strCulture) ? new CultureInfo(strCulture) : throw new CultureNotFoundException();

                return Messages.GenerateGenericSuccessObjectMessage("Translate", GetXmlSection(wordKeys, culture.Name), 200);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe o arquivo de tradução '{strCulture}'.", 404);
            }
            catch (CultureNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe a cultura '{strCulture}'.", 404, wordKeys);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existem as chaves no arquivo de tradução.", 404, wordKeys);
            }
            catch (DirectoryNotFoundException e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return Messages.GenerateGenericErrorMessage($"Não existe o caminho do arquivo de tradução.", 404);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message ?? e.InnerException.Message, null);
                return (e.Message != null && e?.InnerException?.Message != null) ? Messages.GenerateGenericErrorMessage(e.Message, e.InnerException.Message) : Messages.GenerateGenericErrorMessage(e.Message ?? e.InnerException.Message);
            }
        }

        #region Private methods

        /// <summary>
        ///     
        /// </summary>
        /// <param name="key"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        private string GetXmlSection(string key, string culture = null)
        {
            XDocument doc = XDocument.Load($"{_translateConfiguration.Path}{culture ??_translateConfiguration.Culture}{_translateConfiguration.Extension}");
            var section = doc.Descendants().ToList().Where(n => n.FirstAttribute.Value == key);
            return section.FirstOrDefault()?.Value ?? throw new KeyNotFoundException();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetXmlSection(List<string> keys, string culture = null)
        {
            var dictionary = new Dictionary<string, string>();

            XDocument doc = XDocument.Load($"{_translateConfiguration.Path}{culture ?? _translateConfiguration.Culture}{_translateConfiguration.Extension}");

            foreach (var key in keys)
            {
                var section = doc.Descendants().ToList().Where(n => n.FirstAttribute.Value == key);
                dictionary.Add(key, section.FirstOrDefault().Value);
            }

            return dictionary.Count > 0 ? dictionary : throw new KeyNotFoundException();
        }

        #endregion Private methods
    }
}
