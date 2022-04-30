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
using System.Net;
using API.Extensions;
using System.IO;
using APICommunication;

namespace ShortenUrlWeb.Controllers
{
    [Route("/home")]
    public class HomeController : Controller
    {
        private IAPIClient client;

        public HomeController()
        {
            client = APIClientFactory.CreateDefault();
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

            var apiResult = client.GetAbsoluteShortURLAsync(longUrl).Result;
            var url = apiResult.Data;

            if(string.IsNullOrEmpty(url))
            {
                result.IsSuccessful = false;
                result.Message = "Can't create short url.";
            }
            else
            {
                result.IsSuccessful = true;
                result.InternalUrl = url;
            }

            return PartialView("Result", result);
        }
    }
}