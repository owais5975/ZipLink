using Microsoft.AspNetCore.Mvc;
using ZipLink.API.Models;
using ZipLink.Core.Commons;
using ZipLink.Infrastructure.Implementation;

namespace ZipLink.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlServiceRepo _urlServiceRepo;

        public UrlController(IUrlServiceRepo urlServiceRepo)
        {
            _urlServiceRepo = urlServiceRepo;
        }

        [HttpPost]
        public IActionResult Post([FromBody] string url) =>
            Ok(new Response { Status = true, Data = _urlServiceRepo.CreateURL(url), Message = string.Format(CustomMessages.CREATED, "Url") });

        [HttpGet]
        public IActionResult Get([FromQuery] string url) =>
            Ok(new Response { Status = true, Data = _urlServiceRepo.GetURL(url) });
    }
}