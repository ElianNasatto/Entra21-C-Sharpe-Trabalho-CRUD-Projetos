using Model;
using Repository;
using System;
using System.Collections.Generic;
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
            ViewBag.Projetos = repository.ObterTodos();
            return View();
        }

        public ActionResult Cadastro()
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Clientes = clienteRepository.ObterTodos();
            return View();
        }
        
        public ActionResult Insert(int cliente,string nome, DateTime dataCriacao, DateTime dataFinalizacao)
        {
            Projeto projeto = new Projeto();
            projeto.FkCliente = cliente;
            projeto.Nome = nome;
            projeto.DataCriacao = dataCriacao;
            projeto.DataFinalizacao = dataFinalizacao;
            repository.Inserir(projeto);
            return RedirectToAction("Index");
        }

        public ActionResult Alterar(int id)
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Clientes = clienteRepository.ObterTodos();

            ViewBag.Projetos = repository.ObterPeloId(id);

            return View();
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

    }
}