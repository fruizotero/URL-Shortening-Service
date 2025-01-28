using Microsoft.AspNetCore.Mvc;
using URL_Shortening_Service.Exceptions;
using URL_Shortening_Service.Models.DTOs;
using URL_Shortening_Service.Services;

namespace URL_Shortening_Service.Controllers
{

    [Route("/shorten")]
    public class ShortUrlController : Controller
    {

        private readonly ShortUrlService _shortUrlService;

        public ShortUrlController(ShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        // funcion para GetShortUrlByShortCode
        [HttpGet("{shortCode}")]
        public async Task<IActionResult> GetShortUrl(string shortCode)
        {
            try
            {
                var shortUrlDTO = await _shortUrlService.GetShortUrlByShortCode(shortCode);
                return Ok(shortUrlDTO);
            }
            catch (ShortUrlNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // funcion para AddShortUrl
        [HttpPost]
        public async Task<IActionResult> AddShortUrl([FromBody] ShortUrlRequestDTO shortUrlRequestDTO)
        {
            try
            {
                var shortUrlDTO = await _shortUrlService.AddShortUrl(shortUrlRequestDTO);
                return Ok(shortUrlDTO);
            }
            catch (ShortUrlCannotBeEmpty e)
            {
                return BadRequest(e.Message);
            }
            catch (ShortUrlIsNotValid e)
            {
                return BadRequest(e.Message);
            }
        }

        // funcion para UpdateShortUrl
        [HttpPut("{shortCode}")]
        public async Task<IActionResult> UpdateShortUrl(string shortCode, [FromBody] ShortUrlRequestDTO shortUrlRequestDTO)
        {
            try
            {
                var shortUrlDTO = await _shortUrlService.UpdateOriginalUrl(shortUrlRequestDTO, shortCode);
                return Ok(shortUrlDTO);
            }
            catch (ShortUrlNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ShortUrlCannotBeEmpty e)
            {
                return BadRequest(e.Message);
            }
            catch (ShortUrlIsNotValid e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
