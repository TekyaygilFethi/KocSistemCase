using AutoMapper;
using KUSYS.Business.Caching.Base;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Data.Exceptions;
using KUSYS.Data.Web.Base;
using KUSYS.WEBUI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;

namespace KUSYS.WEBUI.Controllers
{
    [Route("cache")]
    public class CacheController : Controller
    {
        private readonly ICacheService _cacheService;

        public CacheController(IUnitOfWork uow, IMapper mapper)
        {
            _cacheService = uow.GetCacheService();
        }

        //chacheleri GUI veya Redis CLI olmadan silmek için
        [Authorize(Roles = "Admin"), HttpGet, Route("delete")]
        public IActionResult DeleteCache(string key, bool byPattern = false)
        {
            if (byPattern) _cacheService.DeleteKeysByPattern(key);
            else
            {
                if (!_cacheService.Any(key)) return BadRequest("Cache Key bulunamadı!");
                else _cacheService.Remove(key);
            }

            return Ok($"{key} key has been deleted successfully!");
        }




    }
}
