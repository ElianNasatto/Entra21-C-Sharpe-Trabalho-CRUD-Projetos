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
    public class UsuarioRepository
    {
        public List<Usuario> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM usuarios";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            List<Usuario> usuarios = new List<Usuario>();
            foreach(DataRow linha in tabela.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.Id = Convert.ToInt32(linha["id"]);
                usuario.Nome = linha["nome"].ToString();
                usuario.Login = linha["login"].ToString();
                usuario.Senha = linha["senha"].ToString();
                usuarios.Add(usuario);
            }
            comando.Connection.Close();
            return usuarios;
        }

        public int Inserir(Usuario usuario)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO usuarios (nome, login, senha) 
OUTPUT INSERTED.ID 
VALUES (@NOME, @LOGIN, @SENHA)";
            comando.Parameters.AddWithValue("@NOME", usuario.Nome);
            comando.Parameters.AddWithValue("@LOGIN", usuario.Login);
            comando.Parameters.AddWithValue("@SENHA", usuario.Senha);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Usuario ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT * FROM usuarios WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if(tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            Usuario usuario = new Usuario();
            usuario.Id = Convert.ToInt32(linha["id"]);
            usuario.Nome = linha["nome"].ToString();
            usuario.Login = linha["login"].ToString();
            usuario.Senha = linha["senha"].ToString();
            return usuario;
        }

        public bool Alterar(Usuario usuario)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "UPDATE usuarios SET nome = @NOME, login = @LOGIN, senha = @SENHA WHERE @ID = id";
            comando.Parameters.AddWithValue("@NOME", usuario.Nome);
            comando.Parameters.AddWithValue("@LOGIN", usuario.Login);
            comando.Parameters.AddWithValue("@SENHA", usuario.Senha);
            comando.Parameters.AddWithValue("@ID", usuario.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM usuarios WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
