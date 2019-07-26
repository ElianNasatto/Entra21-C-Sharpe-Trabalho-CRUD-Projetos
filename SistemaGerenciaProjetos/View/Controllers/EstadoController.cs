using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class EstadoController : Controller
    {
        private EstadoRepository repository;

        public EstadoController()
        {
            repository = new EstadoRepository();
        }

        // GET: Estado
        public ActionResult Index()
        {
            if (LoginController.retorno == true)
            {
                List<Estado> estados = repository.ObterTodos();
                ViewBag.Estados = estados;
                return View();

            }
            else
            {
                return Redirect("/login");
            }
        }

        public ActionResult Cadastro()
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

        public ActionResult Store(string nome, string sigla)
        {
            if (LoginController.retorno == true)
            {
                Estado estado = new Estado();
                estado.Nome = nome;
                estado.Sigla = sigla;
                repository.Inserir(estado);
                return RedirectToAction("Index");

            }
            else
            {
                return Redirect("/login");
            }
        }

        public ActionResult Alterar(int id)
        {
            if (LoginController.retorno == true)
            {
                Estado estado = repository.ObterPeloId(id);
                ViewBag.Estado = estado;
                return View();

            }
            else
            {
                return Redirect("/login");
            }


        }

        public ActionResult Update(int id, string nome, string sigla)
        {
            if (LoginController.retorno == true)
            {
                Estado estado = new Estado();
                estado.Id = id;
                estado.Nome = nome;
                estado.Sigla = sigla;
                repository.Alterar(estado);
                return RedirectToAction("Index");

            }
            else
            {
                return Redirect("/login");
            }
        }

        public ActionResult Apagar(int id)
        {
            if (LoginController.retorno == true)
            {
                repository.Apagar(id);
                return RedirectToAction("Index");

            }
            else
            {
                return Redirect("/login");
            }
        }
    }
}