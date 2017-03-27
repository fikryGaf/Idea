using Idea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Idea.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
             IdeaEntities db = new IdeaEntities();

            return View("~/Views/test.cshtml", db.Items);
        }
    }
}
