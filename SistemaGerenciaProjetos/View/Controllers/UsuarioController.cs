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
            List<Usuario> lista = repository.ObterTodos();
            ViewBag.Usuarios = lista;
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Alterar(int id)
        {
            Usuario usuario = new Usuario();
            usuario = repository.ObterPeloId(id);
            ViewBag.Usuario = usuario;
            return View();
        }

        public ActionResult Insert(string nome, string login, string senha)
        {
            Usuario usuario = new Usuario();
            usuario.Nome = nome;
            usuario.Login = login;
            usuario.Senha = senha;
            repository.Inserir(usuario);
            return RedirectToAction("Index");
        }
        public ActionResult Update(string nome, string login, string senha, int id)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            usuario.Nome = nome;
            usuario.Login = login;
            usuario.Senha = senha;
            repository.Alterar(usuario);
            return RedirectToAction("Index");
        }
        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}