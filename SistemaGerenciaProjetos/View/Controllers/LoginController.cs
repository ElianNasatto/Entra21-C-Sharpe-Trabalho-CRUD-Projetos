using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class LoginController : Controller
    {
        private LoginRepository repository;
        // GET: Login
        public LoginController()
        {
            repository = new LoginRepository();
        }
        public ActionResult Index()
        {
            retorno = false;
            return View();
        }

        public static bool retorno;
        public ActionResult Verifica(string login, string senha)
        {
            retorno = repository.VerificaLogin(login, senha);

            if (retorno == true)
            {
                return Redirect("/telainicial/index");
            }
            else
            {
                return RedirectToAction("Index");

            }


        }
    }
}
