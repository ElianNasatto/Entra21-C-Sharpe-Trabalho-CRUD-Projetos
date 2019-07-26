using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ProjetoController : Controller
    {
        // GET: Projeto
        private ProjetoRepository repository;

        public ProjetoController()
        {
            repository = new ProjetoRepository();
        }
        public ActionResult Index()
        {
            if (LoginController.retorno == true)
            {
                ViewBag.Projetos = repository.ObterTodos();
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

                ClienteRepository clienteRepository = new ClienteRepository();
                ViewBag.Clientes = clienteRepository.ObterTodos();
                return View();
            }
            else
            {
                return Redirect("/login");
            }
        }

        public ActionResult Insert(int cliente, string nome, DateTime dataCriacao, DateTime dataFinalizacao)
        {
            if (LoginController.retorno == true)
            {
                Projeto projeto = new Projeto();
                projeto.FkCliente = cliente;
                projeto.Nome = nome;
                projeto.DataCriacao = dataCriacao;
                projeto.DataFinalizacao = dataFinalizacao;
                repository.Inserir(projeto);
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
                ClienteRepository clienteRepository = new ClienteRepository();
                List<Cliente> lista = clienteRepository.ObterTodos();
                ViewBag.Clientes = lista;

                ViewBag.Projetos = repository.ObterPeloId(id);

                return View();

            }
            else
            {
                return Redirect("/login");
            }
        }

        public ActionResult Update(int id,int cliente, string nome, DateTime dataCriacao, DateTime dataFinalizacao)
        {
            Projeto projeto = new Projeto();
            projeto.Id = id;
            projeto.Nome = nome;
            projeto.FkCliente = cliente;
            projeto.DataCriacao = dataCriacao;
            projeto.DataFinalizacao = dataFinalizacao;
            repository.Alterar(projeto);
            return Redirect("/projeto");
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