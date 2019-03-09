using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using NicoApi;
using NicoDataExtractor.Models;

namespace NicoDataExtractor.Controllers {
    public class HomeController : Controller {

        private static readonly HttpClient client = new HttpClient();
        private readonly Regex nicoRegex = new Regex(@"^https?://www\.nicovideo\.jp/watch/((?:sm|nm)\d+)");

        public async Task<IActionResult> Index(string nicoUrl = null) {

            if (string.IsNullOrEmpty(nicoUrl))
                return View(new HomeViewModel());

            var match = nicoRegex.Match(nicoUrl);

            if (!match.Success) {
                return View(new HomeViewModel { Error = "Could not recognize Nico URL" });
            }

            var nicoId = match.Groups[1].Value;
            var stringResult = await client.GetStringAsync("https://ext.nicovideo.jp/api/getthumbinfo/" + nicoId);

            var serializer = new XmlSerializer(typeof(NicoResponse));
            NicoResponse parsed;
            using (var reader = new StringReader(stringResult)) {
                parsed = (NicoResponse)serializer.Deserialize(reader);
            }

            return View(new HomeViewModel { NicoUrl = nicoUrl, Result = stringResult, NicoResponse = parsed });

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
