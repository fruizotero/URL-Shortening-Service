using Microsoft.AspNetCore.Mvc;
using URL_Shortening_Service.Exceptions;
using URL_Shortening_Service.Models.DTOs;
using URL_Shortening_Service.Models.responses;
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
                await _shortUrlService.IncrementAccessAcount(shortCode);
                return Ok( new ApiResponse<ShortUrlDTO>(true, "Short URL stats retrieved successfully", shortUrlDTO));
            }
            catch (ShortUrlNotFoundException e)
            {
                return NotFound(new ApiResponse<ShortUrlDTO>(false, e.Message, null));
            }
        }

        // funcion para AddShortUrl
        [HttpPost]
        public async Task<IActionResult> AddShortUrl([FromBody] ShortUrlRequestDTO shortUrlRequestDTO)
        {
            try
            {
                var shortUrlDTO = await _shortUrlService.AddShortUrl(shortUrlRequestDTO);
                return Ok(new ApiResponse<ShortUrlDTO>(true, "Short URL created successfully", shortUrlDTO));

            }
            catch (ShortUrlCannotBeEmpty e)
            {
                return BadRequest(new ApiResponse<ShortUrlDTO>(false, e.Message, null));
            }
            catch (ShortUrlIsNotValid e)
            {
                return BadRequest(new ApiResponse<ShortUrlDTO>(false, e.Message, null));

            }
        }

        // funcion para UpdateShortUrl
        [HttpPut("{shortCode}")]
        public async Task<IActionResult> UpdateShortUrl(string shortCode, [FromBody] ShortUrlRequestDTO shortUrlRequestDTO)
        {
            try
            {
                var shortUrlDTO = await _shortUrlService.UpdateOriginalUrl(shortUrlRequestDTO, shortCode);
                return Ok(new ApiResponse<ShortUrlDTO>(true, "Short URL updated successfully", shortUrlDTO));
            }
            catch (ShortUrlNotFoundException e)
            {
                return NotFound(new ApiResponse<ShortUrlDTO>(false, e.Message, null));
            }
            catch (ShortUrlCannotBeEmpty e)
            {
                return BadRequest(new ApiResponse<ShortUrlDTO>(false, e.Message, null));
            }
            catch (ShortUrlIsNotValid e)
            {
                return BadRequest(new ApiResponse<ShortUrlDTO>(false, e.Message, null));
            }
        }

        // endpoint para eliminar un shortUrl
        [HttpDelete("{shortCode}")]
        public async Task<IActionResult> DeleteShortUrl(string shortCode)
        {
            try
            {
                await _shortUrlService.DeleteShortUrl(shortCode);
                return NoContent();
            }
            catch (ShortUrlNotFoundException e)
            {
                return NotFound(new ApiResponse<ShortUrlDTO>(false, e.Message, null));
            }
        }

        // endpoint para stats

        [HttpGet("{shortCode}/stats")]
        public async Task<IActionResult> StatsShortUrl(string shortCode)
        {


            try
            {
                var shortUrlDtoWithAccessAcount = await _shortUrlService.Stats(shortCode);

                return Ok(new ApiResponse<ShortUrlDtoWithAccessAcount>(true, "Short URL stats retrieved successfully", shortUrlDtoWithAccessAcount));
            }
            catch (ShortUrlNotFoundException e)
            {

                return NotFound(new ApiResponse<ShortUrlDtoWithAccessAcount>(false, e.Message, null));


            }


        }




    }
}
