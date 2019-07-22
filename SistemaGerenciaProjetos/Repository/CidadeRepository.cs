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
    public class CidadeRepository
    {
        public void Inserir(Cidade cidade)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO cidades (nome,numero_habitantes,id_estado) VALUES
            (@NOME,@NUMERO_HABITANTES,@ID_ESTADO)";
            comando.Parameters.AddWithValue("@NOME", cidade.Nome);
            comando.Parameters.AddWithValue("@NUMERO_HABITANTES", cidade.NumeroHabitantes);
            comando.Parameters.AddWithValue("@ID_ESTADO", cidade.IdEstado);
            comando.ExecuteNonQuery();

        }

        public void Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM cidades WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public List<Cidade> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT  
                                    cidades.id AS 'id_cidade',
                                    cidades.nome AS 'nome_cidade',
                                    cidades.id_estado AS 'idEstado',
                                    cidades.numero_habitantes AS 'numero_habitantes',
                                    estados.nome AS 'nome_estado'
                                    FROM cidades INNER JOIN estados ON (cidades.id_estado = estados.id)";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Cidade> lista = new List<Cidade>();

            foreach (DataRow linha in tabela.Rows)
            {
                Cidade cidade = new Cidade();
                cidade.Id = Convert.ToInt32(linha["id_cidade"]);
                cidade.Nome = linha["nome_cidade"].ToString();
                cidade.NumeroHabitantes = Convert.ToInt32(linha["numero_habitantes"]);
                cidade.IdEstado = Convert.ToInt32(linha["idEstado"]);
                cidade.Estado = new Estado();
                cidade.Estado.Nome = linha["nome_estado"].ToString();
                lista.Add(cidade);   
            }
            return lista;
        }

        public List<Cidade> ObterTodosCombobox()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT * FROM cidades";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<Cidade> lista = new List<Cidade>();

            foreach (DataRow linha in tabela.Rows)
            {
                Cidade cidade = new Cidade();
                cidade.Id = Convert.ToInt32(linha["id"]);
                cidade.Nome = linha["nome"].ToString();
                cidade.NumeroHabitantes = Convert.ToInt32(linha["numero_habitantes"]);
                lista.Add(cidade);
            }
            return lista;
        }
        public Cidade ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT  
                                    cidades.id AS 'id_cidade',
                                    cidades.nome AS 'nome_cidade',
                                    cidades.numero_habitantes AS 'numero_habitantes',
                                    estados.nome AS 'nome_estado'
                                    FROM cidades INNER JOIN estados ON(cidades.id_estado = estados.id)
                    WHERE cidades.id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            DataRow linha = tabela.Rows[0];
            Cidade cidade = new Cidade();
            cidade.Id = Convert.ToInt32(linha["id_cidade"]);
            cidade.Nome = linha["nome_cidade"].ToString();
            cidade.NumeroHabitantes = Convert.ToInt32(linha["numero_habitantes"]);
            cidade.Estado = new Estado();
            cidade.Estado.Nome = linha["nome_estado"].ToString();

            return cidade;
        }

        public void Atualizar(Cidade cidade)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandText = "UPDATE cidades SET nome = @NOME, numero_habitantes = @NUMERO_HABITANTES, id_estado = @ID_ESTADO";
            comando.Parameters.AddWithValue("@ID", cidade.Id);
            comando.Parameters.AddWithValue("@NOME", cidade.Nome);
            comando.Parameters.AddWithValue("@NUMERO_HABITANTES", cidade.NumeroHabitantes);
            comando.Parameters.AddWithValue("@ID_ESTADO", cidade.IdEstado);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

    }
}
