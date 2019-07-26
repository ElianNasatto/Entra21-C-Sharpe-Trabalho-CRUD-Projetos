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
    public class ProjetoRepository
    {
        public int Inserir(Projeto projeto)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO projetos 
(nome, data_criacao, data_finalizacao, id_cliente) 
OUTPUT INSERTED.ID VALUES 
(@NOME, @DATA_CRIACAO, @DATA_FINALIZACAO, @ID_CLIENTE)";
            comando.Parameters.AddWithValue("@NOME", projeto.Nome);
            comando.Parameters.AddWithValue("@DATA_CRIACAO", projeto.DataCriacao);
            comando.Parameters.AddWithValue("@DATA_FINALIZACAO", projeto.DataFinalizacao);
            comando.Parameters.AddWithValue("@ID_CLIENTE", projeto.FkCliente);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public List<Projeto> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT projetos.id AS 'ProjetoId',
                                        projetos.id_cliente AS 'ProjetoIdCliente',
                                        projetos.nome AS 'ProjetoNome',
                                        projetos.data_criacao AS 'ProjetoDataCriação',
                                        projetos.data_finalizacao AS 'ProjetoDataFinalização',
                                        clientes.nome AS 'ClienteNome'
                                        FROM projetos
                                    INNER JOIN clientes ON (projetos.id_cliente = clientes.id)";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Projeto> projetos = new List<Projeto>();
            foreach (DataRow linha in tabela.Rows)
            {
                Projeto projeto = new Projeto();
                projeto.Id = Convert.ToInt32(linha["ProjetoId"]);
                projeto.Nome = linha["ProjetoNome"].ToString();
                projeto.DataCriacao = Convert.ToDateTime(linha["ProjetoDataCriação"]);
                projeto.DataFinalizacao = Convert.ToDateTime(linha["ProjetoDataFinalização"]);
                projeto.FkCliente = Convert.ToInt32(linha["ProjetoIdCliente"]);
                projeto.Cliente = new Cliente();
                projeto.Cliente.Nome = linha["ClienteNome"].ToString();
                projetos.Add(projeto);
            }
            return projetos;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM projetos WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", id);
            int qunatidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return qunatidadeAfetada == 1;
        }

        public bool Alterar(Projeto projeto)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE projetos SET 
                                    nome = @NOME, 
                                    id_cliente = @ID_CLIENTE, 
                                    data_criacao = @DATA_CRIACAO,
                                    data_finalizacao = @DATA_FINALIZACAO
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", projeto.Nome);
            comando.Parameters.AddWithValue("@ID_CLIENTE", projeto.FkCliente);
            comando.Parameters.AddWithValue("@DATA_CRIACAO", projeto.DataCriacao);
            comando.Parameters.AddWithValue("@DATA_FINALIZACAO", projeto.DataFinalizacao);
            comando.Parameters.AddWithValue("@ID", projeto.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Projeto ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT projetos.id AS 'ProjetoId',
                                        projetos.id_cliente AS 'ProjetoIdCliente',
                                        projetos.nome AS 'ProjetoNome',
                                        projetos.data_criacao AS 'ProjetoDataCriacao',
                                        projetos.data_finalizacao AS 'ProjetoDataFinalizacao',
                                        clientes.nome AS 'ClienteNome'
                                    FROM projetos
                                    INNER JOIN clientes ON (projetos.id_cliente = clientes.id) WHERE projetos.id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Projeto> projetos = new List<Projeto>();
            if(tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            Projeto projeto = new Projeto();
            projeto.Id = Convert.ToInt32(linha["ProjetoId"]);
            projeto.Nome = linha["ProjetoNome"].ToString();
            projeto.FkCliente = Convert.ToInt32(linha["ProjetoIdCliente"]);
            projeto.DataCriacao = Convert.ToDateTime(linha["ProjetoDataCriacao"]);
            projeto.DataFinalizacao = Convert.ToDateTime(linha["ProjetoDataFinalizacao"]);
            projeto.Cliente = new Cliente();
            projeto.Cliente.Nome = linha["ClienteNome"].ToString();

            return projeto;
        }
    }
}
