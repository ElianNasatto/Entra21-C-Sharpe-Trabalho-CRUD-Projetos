using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TarefaRepository
    {
        public int Inserir(Tarefa tarefa)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO tarefas
                                        (id_usuario_responsavel, 
                                        id_projeto, id_categoria, 
                                        titulo, descricao, duracao)
                                    OUTPUT INSERTED.ID VALUES
                                        (@ID_USUARIO_RESPONSAVEL, 
                                        @ID_PROJETO, @ID_CATEGORIA,
                                        @TITULO, @DESCRICAO, @DURACAO)";

            comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.FkUsuario);
            comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.FkProjeto);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.FkCategoria);
            comando.Parameters.AddWithValue("@TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("@DESCRICAO", tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO", tarefa.Duracao);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public List<Tarefa> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
                                            tarefas.id AS 'TarefaId',
                                            tarefas.titulo AS 'TarefaTitulo',
                                            tarefas.descricao AS 'TarefaDescricao',
                                            tarefas.id_categoria AS 'TarefaIdCategoria',
                                            tarefas.duracao AS 'duracao_tarefa',
                usuarios.nome AS 'nome_usuario',
                projetos.nome AS 'nome_projeto',
                categorias.nome AS 'nome_categoria'
                FROM tarefas 
                INNER JOIN usuarios ON (tarefas.id_usuario_responsavel = usuarios.id)
                INNER JOIN projetos ON (tarefas.id_projeto = projetos.id)
                INNER JOIN categorias ON (tarefas.id_categoria = categorias.id)";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Tarefa> tarefas = new List<Tarefa>();
            foreach (DataRow linha in tabela.Rows)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Id = Convert.ToInt32(linha["TarefaId"]);
                tarefa.Titulo = linha["TarefaTitulo"].ToString();
                tarefa.Descricao = linha["TarefaDescricao"].ToString();
                //Duracao modificada para string pois nao estava convertendo
                tarefa.Duracao = Convert.ToDateTime(linha["duracao_tarefa"]);
                tarefa.Usuario = new Usuario();
                tarefa.Usuario.Nome = linha["nome_usuario"].ToString();
                tarefa.Projeto = new Projeto();
                tarefa.Projeto.Nome = linha["nome_projeto"].ToString();
                tarefas.Add(tarefa);
                tarefa.Categoria = new Categoria();
                tarefa.Categoria.Nome = linha["nome_categoria"].ToString();
            }
            return tarefas;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM tarefas WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Alterar(Tarefa tarefa)

        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE tarefas SET 
                                        id_usuario_responsavel = @ID_USUARIO_RESPONSAVEL,
                                        id_projeto = @ID_PROJETO,
                                        id_categoria = @ID_CATEGORIA,
                                        titulo = @TITULO, 
                                        descricao = @DESCRICAO,
                                        duracao = @DURACAO
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("@ID_USUARIO_RESPONSAVEL", tarefa.FkUsuario);
            comando.Parameters.AddWithValue("@ID_PROJETO", tarefa.FkProjeto);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", tarefa.FkCategoria);
            comando.Parameters.AddWithValue("@DESCRICAO", tarefa.Descricao);
            comando.Parameters.AddWithValue("@DURACAO", tarefa.Duracao);
            comando.Parameters.AddWithValue("@ID", tarefa.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Tarefa ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT tarefas.id AS 'TarefaId',
                                        tarefas.titulo AS 'TarefaTitulo',
                                        tarefas.descricao AS 'TarefaDescricao',
                                        tarefas.duracao AS 'TarefaDuracao',
                                        tarefas.id_usuario_responsavel AS 'TarefaIdUsuario',
                                        tarefas.id_projeto AS 'TarefaIdProjeto',
                                        tarefas.id_categoria AS 'TarefaIdCategoria'
                                    FROM tarefas
                                    WHERE tarefas.id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Tarefa> tarefas = new List<Tarefa>();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            Tarefa tarefa = new Tarefa();
            tarefa.Id = Convert.ToInt32(linha["TarefaId"]);
            tarefa.Titulo = linha["TarefaTitulo"].ToString();
            tarefa.Descricao = linha["TarefaDescricao"].ToString();
            tarefa.Duracao = Convert.ToDateTime(linha["TarefaDuracao"]);
            tarefa.FkUsuario = Convert.ToInt32(linha["TarefaIdUsuario"]);
            tarefa.FkProjeto = Convert.ToInt32(linha["TarefaIdProjeto"]);
            tarefa.FkCategoria = Convert.ToInt32(linha["TarefaIdCategoria"]);
            return tarefa;
        }
    }
}
