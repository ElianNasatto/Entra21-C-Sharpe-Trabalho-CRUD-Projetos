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
    public class CategoriaRepository
    {
        public void Inserir(Categoria categoria)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "INSERT INTO categorias (nome) VALUES @NOME";
            comando.Parameters.AddWithValue("@ID", categoria.Id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();

        }

        public void Apagar(int id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandText = "DELETE FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
        }

        public List<Categoria> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM categorias";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<Categoria> lista = new List<Categoria>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Categoria categoria = new Categoria();
                categoria.Id = Convert.ToInt32(linha["id"]);
                categoria.Nome = linha["nome"].ToString();
                lista.Add(categoria);
            }
            return lista;
        }
        public bool Alterar(Categoria categoria)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "UPDATE categorias SET nome = @NOME WHERE @ID = id";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            comando.Parameters.AddWithValue("@ID", categoria.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Categoria ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            DataRow linha = tabela.Rows[0];
            Categoria categoria = new Categoria();
            categoria.Id = Convert.ToInt32(linha["id"]);
            categoria.Nome = linha["nome"].ToString();
            return categoria;
            
        }

        public void Alterar(Categoria categoria)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandText = "UPDATE cidades SET nome = @NOME WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", categoria.Id);
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            comando.ExecuteNonQuery();

        }
    }
}
