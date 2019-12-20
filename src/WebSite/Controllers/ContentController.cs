using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebSite.Controllers
{
    public class ContentController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        public ContentController(
            IMemoryCache cache, 
            IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("content/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var html = await GetFileContent(name);

            if (string.IsNullOrWhiteSpace(html))
                return NotFound();

            ViewData["Title"] = string.Join(" ", name.Split('-').Select(UppercaseFirstLetter));

            return View((object) html);
        }

        private static string UppercaseFirstLetter(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            if (text.Length == 1)
                return text.ToUpper();

            return text.First().ToString().ToUpper() + text.Substring(1);
        }

        private async Task<string> GetFileContent(string name)
        {
            name = name.Replace("-", "");
            
            var path = Directory
                .GetFiles(Path.Combine(_hostingEnvironment.WebRootPath, "content"))
                .SingleOrDefault(o => string.Equals(
                    Path.GetFileNameWithoutExtension(o).Trim(),
                    name.Trim(),
                    StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path))
                return null;
            
            return await System.IO.File.ReadAllTextAsync(path);
        }

        [Route("content/telegram")]
        public async Task<IActionResult> Telegram()=> RedirectPermanent("/content/platform");

        [Route("partners")]
        public async Task<IActionResult> PartnersRedirect() => RedirectPermanent("/content/partners");  
        
        [Route("search")]
        public async Task<IActionResult> SearchRedirect() => RedirectPermanent("/content/search");
        
        [Route("content/platform")]
        [Route("content/telegram")]
        public async Task<IActionResult> Platform()=> RedirectPermanent("/platform");
    }
}