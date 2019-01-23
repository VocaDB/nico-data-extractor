using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NicoDataExtractor.Models;

namespace NicoDataExtractor.Controllers {
    public class HomeController : Controller {

        readonly Regex nicoRegex = new Regex(@"^https?://www\.nicovideo\.jp/watch/((?:sm|nm)\d+)");

        public async Task<IActionResult> Index(string nicoUrl = null) {

            if (string.IsNullOrEmpty(nicoUrl))
                return View(new HomeViewModel());

            var match = nicoRegex.Match(nicoUrl);

            if (!match.Success) {
                return View(new HomeViewModel { Error = "Could not recognize Nico URL" });
            }

            var nicoId = match.Groups[1].Value;

            var result = await new HttpClient().GetStringAsync("https://ext.nicovideo.jp/api/getthumbinfo/" + nicoId);
            return View(new HomeViewModel { NicoUrl = nicoUrl, Result = result });

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
