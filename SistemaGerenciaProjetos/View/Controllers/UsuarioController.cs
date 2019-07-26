using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepository repository;
        // GET: Usuario
        public UsuarioController()
        {
            repository = new UsuarioRepository();
        }
        public ActionResult Index()
        {
            if (LoginController.retorno == true)
            {
                List<Usuario> lista = repository.ObterTodos();
                ViewBag.Usuarios = lista;
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

        public ActionResult Alterar(int id)
        {
            if (LoginController.retorno == true)
            {
                Usuario usuario = new Usuario();
                usuario = repository.ObterPeloId(id);
                ViewBag.Usuario = usuario;
                return View();

            }
            else
            {
                return Redirect("/login");
            }
        }

        public ActionResult Insert(string nome, string login, string senha)
        {
            if (LoginController.retorno == true)
            {
                Usuario usuario = new Usuario();
                usuario.Nome = nome;
                usuario.Login = login;
                usuario.Senha = senha;
                repository.Inserir(usuario);
                return RedirectToAction("Index");

            }
            else
            {
                return Redirect("/login");
            }
        }
        public ActionResult Update(string nome, string login, string senha, int id)
        {
            if (LoginController.retorno == true)
            {
                Usuario usuario = new Usuario();
                usuario.Id = id;
                usuario.Nome = nome;
                usuario.Login = login;
                usuario.Senha = senha;
                repository.Alterar(usuario);
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