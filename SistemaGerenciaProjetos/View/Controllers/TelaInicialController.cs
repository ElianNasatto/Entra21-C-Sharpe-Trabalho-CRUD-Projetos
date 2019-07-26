using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class TelaInicialController : Controller
    {
        // GET: TelaInicial
        public ActionResult Index()
        {
            if (LoginController.retorno == true)
            {
                return View();
            }
            else
            {
                return Redirect("/login");
            }
        }

    }
}