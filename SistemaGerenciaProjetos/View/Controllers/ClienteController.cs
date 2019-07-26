using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        private ClienteRepository repository;
        public ClienteController()
        {
            repository = new ClienteRepository();
        }
        public ActionResult Index()
        {
            if (LoginController.retorno == true)
            {
                ViewBag.Clientes = repository.ObterTodos();
                return View();
            }
            else
            {
                return Redirect("/login");
            }
        }
        public ActionResult Inserir()
        {
            if (LoginController.retorno == true)
            {

                CidadeRepository cidadeRepository = new CidadeRepository();
                ViewBag.Cidades = cidadeRepository.ObterTodos();
                return View();
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
        public ActionResult Insert(string nome, string cpf, DateTime dataNascimento, int cidade, int numero, string complemento, string logradouro, string cep)
        {
            if (LoginController.retorno == true)
            {
                Cliente cliente = new Cliente();
                cliente.Nome = nome;
                cliente.Cpf = cpf;
                cliente.DataNascimento = dataNascimento;
                cliente.FkCidade = cidade;
                cliente.Numero = numero;
                cliente.Complemento = complemento;
                cliente.Logradouro = logradouro;
                cliente.Cep = cep;
                repository.Inserir(cliente);
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
                ViewBag.Clientes = repository.ObterPeloId(id);
                CidadeRepository cidadeRepository = new CidadeRepository();
                ViewBag.Cidades = cidadeRepository.ObterTodos();
                return View();

            }
            else
            {
                return Redirect("/login");
            }
        }
        public ActionResult Update(int id, string nome, string cpf, DateTime dataNascimento, int cidade, int numero, string complemento, string logradouro, string cep)
        {
            if (LoginController.retorno == true)
            {
                Cliente cliente = new Cliente();
                cliente.Id = id;
                cliente.Nome = nome;
                cliente.Cpf = cpf;
                cliente.DataNascimento = dataNascimento;
                cliente.FkCidade = cidade;
                cliente.Numero = numero;
                cliente.Complemento = complemento;
                cliente.Logradouro = logradouro;
                cliente.Cep = cep;
                repository.Alterar(cliente);
                return RedirectToAction("Index");

            }
            else
            {
                return Redirect("/login");
            }
        }
    }
}