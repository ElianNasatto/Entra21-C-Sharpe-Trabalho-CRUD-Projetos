using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class TarefaController : Controller
    {
        // GET: Tarefa
        private TarefaRepository repository;

        public TarefaController()
        {
            repository = new TarefaRepository();
        }
        public ActionResult Index()
        {
            if (LoginController.retorno == true)
            {
                List<Tarefa> lista = repository.ObterTodos();
                ViewBag.Tarefa = lista;
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
                CategoriaRepository categoriaRepository = new CategoriaRepository();
                ViewBag.Categorias = categoriaRepository.ObterTodos();

                UsuarioRepository usuarioRepository = new UsuarioRepository();
                ViewBag.Usuarios = usuarioRepository.ObterTodos();

                ProjetoRepository projetoRepository = new ProjetoRepository();
                ViewBag.Projetos = projetoRepository.ObterTodos();

                return View();

            }
            else
            {
                return Redirect("/login");
            }
        }

        public ActionResult Insert(int usuario, int projeto, int categoria, string titulo, string descricao, DateTime duracao)
        {
            if (LoginController.retorno == true)
            {

                Tarefa tarefa = new Tarefa();
                tarefa.Usuario = new Usuario();
                tarefa.FkUsuario = usuario;
                tarefa.FkProjeto = projeto;
                tarefa.FkCategoria = categoria;
                tarefa.Titulo = titulo;
                tarefa.Descricao = descricao;
                tarefa.Duracao = duracao;
                repository.Inserir(tarefa);
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

        public ActionResult Alterar(int id)
        {
            if (LoginController.retorno == true)
            {
                ViewBag.Tarefas = repository.ObterPeloId(id);

                CategoriaRepository categoriaRepository = new CategoriaRepository();
                ViewBag.Categorias = categoriaRepository.ObterTodos();

                UsuarioRepository usuarioRepository = new UsuarioRepository();
                ViewBag.Usuarios = usuarioRepository.ObterTodos();

                ProjetoRepository projetoRepository = new ProjetoRepository();
                ViewBag.Projetos = projetoRepository.ObterTodos();

                return View();

            }
            else
            {
                return Redirect("/login");
            }

        }

        public ActionResult Update(int id, int usuario, int projeto, int categoria, string titulo, string descricao, DateTime duracao)
        {
            if (LoginController.retorno == true)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Id = id;
                tarefa.Usuario = new Usuario();
                tarefa.FkUsuario = usuario;
                tarefa.FkProjeto = projeto;
                tarefa.FkCategoria = categoria;
                tarefa.Titulo = titulo;
                tarefa.Descricao = descricao;
                tarefa.Duracao = duracao;
                repository.Alterar(tarefa);
                return RedirectToAction("Index");

            }
            else
            {
                return Redirect("/login");
            }
        }
    }
}