using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using LiteX.Guard;
using API.Extensions;

namespace API.Controllers
{
    public class ShortenUrlController : ControllerBase
    {
        private IShortenUrlService _shortenService;

        public ShortenUrlController(IShortenUrlService shortenUrlService)
        {
            Guard.NotNull(shortenUrlService, nameof(shortenUrlService));

            _shortenService = shortenUrlService;
        }

        [HttpGet("online")]
        public string Index()
        {
            return "Online!";
        }

        [HttpPost("short")]
        public string Shorten(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            var shortUrl = _shortenService.CreateShortRelativeUrl(url);
            var absoluteUrl = Url.Action(nameof(GetLongUrl), typeof(ShortenUrlController).GetName(), new { shortUrl = shortUrl }, Request.Scheme);
            
            return absoluteUrl;
        }

        [HttpGet("get/{shorturl}")]
        public string GetLongUrl(string shortUrl)
        {
            var longUrl = _shortenService.GetLongUrlFromShortRelativeUrl(shortUrl);
            return longUrl;
        }
    }
}