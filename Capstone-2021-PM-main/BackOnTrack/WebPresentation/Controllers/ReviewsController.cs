using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/04/04
    /// Controller for a basic reviews landing page.
    /// </summary>
    public class ReviewsController : Controller
    {
        // GET: Reviews
        public ActionResult Reviews()
        {
            return View();
        }
    }
}