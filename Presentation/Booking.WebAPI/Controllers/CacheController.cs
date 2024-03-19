using Booking.Application.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        // clears all values in redis
        [HttpGet]
        [Route("ClearAllCache")]
        public IActionResult ClearAllCache()
        {
            _cacheService.ClearAll();

            return Ok();
        }

        // clears the value stored in redis by key
        [HttpPost]
        [Route("ClearByKey")]
        public IActionResult ClearByKey(string key)
        {
            _cacheService.Clear(key);

            return Ok();
        }
    }
}
