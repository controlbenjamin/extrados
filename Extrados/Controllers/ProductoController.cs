using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Extrados.Controllers
{
    public class ProductoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tabla()
        {
            return View();
        }

        public JsonResult ListarUsuarios()
        {

            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}