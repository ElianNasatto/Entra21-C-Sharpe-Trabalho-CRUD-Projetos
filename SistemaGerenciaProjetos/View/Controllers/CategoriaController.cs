using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CategoriaController : Controller
    {
        private CategoriaRepository repository;

        public CategoriaController()
        {
            repository = new CategoriaRepository();
        }

        public ActionResult Index()
        {
            if (LoginController.retorno == true)
            {
                List<Categoria> categorias = repository.ObterTodos();
                ViewBag.Categorias = categorias;
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

        public ActionResult Store(string nome)
        {
            if (LoginController.retorno == true)
            {
                Categoria categoria = new Categoria();
                categoria.Nome = nome;
                repository.Inserir(categoria);
                return RedirectToAction("Index");

            }
            else
            {
                return Redirect("/login");
            }
        }

        public ActionResult Editar(int id)
        {
            if (LoginController.retorno == true)
            {
                Categoria categoria = repository.ObterPeloId(id);
                ViewBag.Categoria = categoria;
                return View();
            }
            else
            {
                return Redirect("/login");
            }

        }

        public ActionResult Update(int id, string nome)
        {
            if (LoginController.retorno == true)
            {
                Categoria categoria = new Categoria();
                categoria.Id = id;
                categoria.Nome = nome;
                repository.Alterar(categoria);
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
