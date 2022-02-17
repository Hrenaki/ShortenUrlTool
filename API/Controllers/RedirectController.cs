using Core.Services;
using LiteX.Guard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;

namespace API.Controllers
{
    [Route("/" + _controllerRelativeRoute)]
    public class RedirectController : ControllerBase
    {
        private const string _controllerRelativeRoute = "redirect";
        private const string _shortenUrlHandlingMethodRelativeRoute = "Index";

        private IShortenUrlService _shortenUrlService;

        public RedirectController(IShortenUrlService shortenUrlService)
        {
            Guard.NotNull(shortenUrlService, nameof(shortenUrlService));

            _shortenUrlService = shortenUrlService;
        }

        [HttpGet]
        [Route(_shortenUrlHandlingMethodRelativeRoute)]
        public IActionResult Index(string shortRelativeUrl)
        {
            if(string.IsNullOrEmpty(shortRelativeUrl))
            {
                return this.NotFound();
            }

            var longUrl = _shortenUrlService.GetLongUrlFromShortRelativeUrl(shortRelativeUrl);
            if(longUrl == null)
            {
                return this.NotFound();
            }

            return Redirect(longUrl);
        }

        public static string GetUrl(IUrlHelper helper, object values, string scheme)
        {
            return helper.Action(nameof(Index), typeof(RedirectController).GetName(), values, scheme);
        }
    }
}