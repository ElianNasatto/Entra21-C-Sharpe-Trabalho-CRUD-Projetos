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
(id_usuario_responsavel, id_projeto, id_categoria, titulo, descricao, duracao)
OUTPUT INSERTED.ID VALUES
(@ID_USUARIO_RESPONSAVEL, @ID_PROJETO, @ID_CATEGORIA, @TITULO, @DESCRICAO, @DURACAO)";
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
            comando.CommandText = @"SELECT tarefas.id AS 'TarefaId',
tarefas.titulo AS 'TarefaTitulo',
tarefas.descricao AS 'TarefaDescrição',
tarefas.id_usuario_responsavel AS 'TarefaIdUsuarioResponsavel',
tarefas.id_projeto AS 'TarefaIdProjeto',
tarefas.id_categoria AS 'TarefaIdCategoria',
categorias.nome AS 'CategoriaNome'
FROM tarefas 
INNER JOIN ;";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Tarefa> tarefas = new List<Tarefa>();
            foreach (DataRow linha in tabela.Rows)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Id = Convert.ToInt32("TarefaId");
                tarefa.Titulo = linha["TarefaTitulo"].ToString();
                tarefa.Descricao = linha["TarefaDescricao"].ToString();
                tarefa.Duracao = Convert.ToDateTime(linha["TarefaDuracao"]);
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
titulo = @TITULO, 
id_usuario_responsavel = @ID_USUARIO_RESPONSAVEL,
id_projeto = @ID_PROJETO,
id_categoria = @ID_CATEGORIA,
descricao = @DESCRICAO,
duracao = @DURACAO
WHERE id = @Id";
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
tarefas.id_categoria AS 'TarefaIdCategoria',
FROM tarefas
INNER JOIN 
WHERE veiculos.id = @ID";
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
            tarefa.FkCategoria = Convert.ToInt32(linha["TarefaIdCategoria"]);
            

            return tarefa;
        }
    }
}
