using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShortUrls.DAL;
using ShortUrls.Models;

namespace ShortUrls.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrlContext _context = new UrlContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult RedirectToLong(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
                return RedirectToAction("NotFound", "Home");

            using (_context)
            {
                var urlInDb = _context.Urls
                    .FirstOrDefault(u => u.ShortUrl == shortUrl);

                if (urlInDb == null)
                    return RedirectToAction("NotFound", "Home");

                Response.StatusCode = (int)HttpStatusCode.Redirect;
                return Redirect(urlInDb.LongUrl);
            }
        }

        public ActionResult NotFound()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ShorterUrl(string longUrl)
        {
            if ( string.IsNullOrEmpty(longUrl) )
                return Json(new { status = false, message = "Please provide URL" }, JsonRequestBehavior.AllowGet);

            if ( !Helpers.UrlHelper.IsUrlExists(longUrl) )
                return Json(new { status = false, message = "Not valid URL provided" }, JsonRequestBehavior.AllowGet);

            if (!Helpers.UrlHelper.HasHttpProtocol(longUrl))
                longUrl = "http://" + longUrl;

            using (_context)
            {
                var urlInDb =
                    _context.Urls.FirstOrDefault(u => u.LongUrl == longUrl);

                if (urlInDb == null)
                {
                    var url = new Url
                    {
                        LongUrl = longUrl,
                        DateGenerated = DateTime.Now,
                        ShortUrl = Helpers.UrlHelper.GenerateRandomShortUrl()
                    };

                    _context.Urls.Add(url);
                    _context.SaveChanges();

                    var shortUrl = Helpers.UrlHelper.BuildUrlFromSegments(
                        Request.Url.Scheme, 
                        Request.Url.Authority, 
                        url.ShortUrl);

                    return Json(new { status = true, url = shortUrl }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var shortUrl = Helpers.UrlHelper.BuildUrlFromSegments(
                        Request.Url.Scheme, 
                        Request.Url.Authority, 
                        urlInDb.ShortUrl);

                    return Json(new {status = true, url = shortUrl }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}