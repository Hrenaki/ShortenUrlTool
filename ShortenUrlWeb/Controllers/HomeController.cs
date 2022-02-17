using Core.Services;
using LiteX.Guard;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using ShortenUrlWeb.Models.Home;

namespace ShortenUrlWeb.Controllers
{
    [Route("/home")]
    public class HomeController : Controller
    {
        private IShortenUrlService _shortenUrlService;

        public HomeController(IShortenUrlService shortenUrlService)
        {
            Guard.NotNull(shortenUrlService, nameof(shortenUrlService));

            _shortenUrlService = shortenUrlService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetShortenUrl(string longUrl)
        {
            var result = new Result();

            if (string.IsNullOrEmpty(longUrl))
            {
                result.IsSuccessful = false;
                result.Message = "Long url is empty!";

                return PartialView("Result", result);
            }

            var shortRelativeUrl = _shortenUrlService.CreateShortRelativeUrl(longUrl);
            if(shortRelativeUrl == null)
            {
                result.IsSuccessful = false;
                result.Message = "Can't create short url!";

                return PartialView("Result", result);
            }

            var values = new { shortRelativeUrl = shortRelativeUrl };

            var url = RedirectController.GetUrl(Url, values, Request.Scheme);
            if (url == null)
            {
                result.IsSuccessful = false;
                result.Message = "Can't create url to redirect tool!";

                return PartialView("Result", result);
            }

            result.IsSuccessful = true;
            result.InternalUrl = url;

            return PartialView("Result", result);
        }
    }
}