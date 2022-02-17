using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using LiteX.Guard;

namespace API.Controllers
{
    [Route("/" + _controllerRelativeRoute)]
    public class ShortenUrlController : ControllerBase
    {
        private const string _controllerRelativeRoute = "shorten";

        private IShortenUrlService _shortenService;

        public ShortenUrlController(IShortenUrlService shortenUrlService)
        {
            Guard.NotNull(shortenUrlService, nameof(shortenUrlService));

            _shortenService = shortenUrlService;
        }

        [HttpGet]
        public string Index()
        {
            return "Online!";
        }

        [HttpPost("get")]
        public string Shorten(string url)
        {
            var shortUrl = _shortenService.CreateShortRelativeUrl(url);
            var absoluteUrl = RedirectController.GetUrl(Url, new { shortRelativeUrl = shortUrl }, Request.Scheme);

            return absoluteUrl;
        }
    }
}