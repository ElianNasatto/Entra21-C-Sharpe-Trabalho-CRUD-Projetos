using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CidadeController : Controller
    {

        private CidadeRepository repository;

        public CidadeController()
        {
            repository = new CidadeRepository();
        }

        // GET: Estado
        public ActionResult Index()
        {
            if (LoginController.retorno == true)
            {
                List<Cidade> cidades = repository.ObterTodos();
                ViewBag.Cidades = cidades;
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
                EstadoRepository estadoRepository = new EstadoRepository();
                List<Estado> estados = estadoRepository.ObterTodos();
                ViewBag.Estados = estados;

                return View();
            }
            else
            {
                return Redirect("/login");
            }

        }

        public ActionResult Store(string nome, int numeroHabitantes, int estado)
        {
            if (LoginController.retorno == true)
            {
                Cidade cidade = new Cidade();
                cidade.Nome = nome;
                cidade.NumeroHabitantes = numeroHabitantes;
                cidade.Estado = new Estado();
                cidade.IdEstado = estado;
                repository.Inserir(cidade);
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
                Cidade cidade = new Cidade();
                cidade = repository.ObterPeloId(id);
                ViewBag.Cidade = cidade;
                EstadoRepository estadoRepository = new EstadoRepository();
                ViewBag.Estados = estadoRepository.ObterTodos();
                return View();

            }
            else
            {
                return Redirect("/login");
            }
        }

        public ActionResult Update(int id, int estado, string nome, int numeroHabitantes)
        {
            if (LoginController.retorno == true)
            {
                Cidade cidade = new Cidade();
                cidade.Id = id;
                cidade.IdEstado = estado;
                cidade.Nome = nome;
                cidade.NumeroHabitantes = numeroHabitantes;

                repository.Atualizar(cidade);
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