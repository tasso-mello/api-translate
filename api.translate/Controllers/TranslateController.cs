namespace api.translate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using domain.translate.Contracts.Business;
    using domain.translate.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    //[Authorize("Bearer")]
    [AllowAnonymous]
    [Route("[controller]")]
    public class TranslateController : ControllerBase
    {
        private readonly ITranslateBusiness _translateBusiness;

        /// <summary>
        ///     
        /// </summary>
        /// <param name="translateBusiness"></param>
        public TranslateController(ITranslateBusiness translateBusiness)
        {
            _translateBusiness = translateBusiness;
        }

        /// <summary>
        ///     Translate
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Json list</response>
        /// <response code="400">Error to try get translated word</response>  
        /// <response code="404">Error to not found translate file path or word</response>  
        [HttpGet("all")]
        public IActionResult Get()
        {
            var result = _translateBusiness.TranslateAll();

            if (result.ToString().Contains("Error"))
            {
                if (result.ToString().Contains("404"))
                    return NotFound(result);
                else
                    return BadRequest(result);
            }
            else
                return Ok(result);
        }

        /// <summary>
        ///     Translate
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <response code="200">Json list</response>
        /// <response code="400">Error to try get translated word</response>  
        /// <response code="404">Error to not found translate file path or word</response>  
        [HttpGet("all/{culture}")]
        public IActionResult GetByCulture(string culture)
        {
            var result = _translateBusiness.TranslateAllByCulture(culture);

            if (result.ToString().Contains("Error"))
            {
                if (result.ToString().Contains("404"))
                    return NotFound(result);
                else
                    return BadRequest(result);
            }
            else
                return Ok(result);
        }

        /// <summary>
        ///     Translate
        /// </summary>
        /// <param name="wordkey"></param>
        /// <returns></returns>
        /// <response code="200">Json list</response>
        /// <response code="400">Error to try get translated word</response>  
        /// <response code="404">Error to not found translate file path or word</response>  
        [HttpGet("{wordkey}")]
        public IActionResult Get(string wordkey)
        {
            var result = _translateBusiness.Translate(wordkey);

            if (result.ToString().Contains("Error"))
            {
                if (result.ToString().Contains("404"))
                    return NotFound(result);
                else
                    return BadRequest(result);
            }
            else
                return Ok(result);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="wordkey"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <response code="200">Json list</response>
        /// <response code="400">Error to try get translated word</response>  
        /// <response code="404">Error to not found translate file path or word</response>  
        [HttpGet("{wordkey}/{culture}")]
        public IActionResult Get(string wordkey, string culture = null)
        {
            var result = _translateBusiness.Translate(wordkey, culture);

            if (result.ToString().Contains("Error"))
            {
                if (result.ToString().Contains("404"))
                    return NotFound(result);
                else
                    return BadRequest(result);
            }
            else
                return Ok(result);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="wordkeys"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult GetList(List<string> wordkeys)
        {
            var result = _translateBusiness.Translate(wordkeys);

            if (result.ToString().Contains("Error"))
                return BadRequest(result);
            else
                return Ok(result);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="wordkeys"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        [HttpPost("{culture}")]
        public IActionResult GetList(List<string> wordkeys, string culture = null)
        {
            var result = _translateBusiness.Translate(wordkeys, culture);

            if (result.ToString().Contains("Error"))
                return BadRequest(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("{culture}/{key}/{value}")]
        public IActionResult Post(string culture, string key, string value)
        {
			var result = _translateBusiness.Insert(culture, key, value);

			if (result.ToString().Contains("Error"))
				return BadRequest(result);
			else
				return Created(string.Empty, result);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("bach")]
        public IActionResult Post(BachTranslate translate)
        {
            var result = _translateBusiness.Insert(translate);

            if (result.ToString().Contains("Error"))
                return BadRequest(result);
            else
                return Created(string.Empty, result);
        }
    }
}
