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
    public class ClienteRepository
    {
        public void Inserir(Cliente cliente)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO clientes (nome, cpf, data_nascimento, numero, complemento, logradouro, cep, id_cidade)
                VALUES (@NOME, @CPF, @DATA_NASCIMENTO, @NUMERO, @COMPLEMENTO, @LOGRADOURO, @CEP, @ID_CIDADE)";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.Cep);
            comando.Parameters.AddWithValue("@ID_CIDADE", cliente.FkCidade);
            comando.ExecuteNonQuery();
            comando.Connection.Close();

        }

        public void Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM clientes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public List<Cliente> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
                                clientes.id AS 'id_cliente',
                                clientes.nome AS 'nome_cliente',
                                clientes.cpf AS 'cpf',
                                clientes.data_nascimento AS 'data_nascimento',
                                clientes.numero AS 'numero',
                                clientes.complemento AS 'complemento',
                                clientes.logradouro AS 'logradouro',
                                clientes.cep AS 'cep',
                                cidades.nome AS 'nome_cidade'
            FROM clientes INNER JOIN cidades ON (clientes.id_cidade = cidades.id);";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<Cliente> lista = new List<Cliente>();
            foreach (DataRow linha in tabela.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(linha["id_cliente"]);
                cliente.Nome = linha["nome_cliente"].ToString();
                cliente.Cpf = linha["cpf"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(linha["data_nascimento"]);
                cliente.Numero = Convert.ToInt32(linha["numero"]);
                cliente.Complemento = linha["complemento"].ToString();
                cliente.Logradouro = linha["logradouro"].ToString();
                cliente.Cep = linha["cep"].ToString();
                cliente.Cidade = new Cidade();
                cliente.Cidade.Nome = linha["nome_cidade"].ToString();
                lista.Add(cliente);
            }
            return lista;
        }

        public Cliente ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT 
                                clientes.id AS 'id_cliente',
                                clientes.nome AS 'nome_cliente',
                                clientes.cpf AS 'cpf',
                                clientes.data_nascimento AS 'data_nascimento',
                                clientes.numero AS 'numero',
                                clientes.complemento AS 'complemento',
                                clientes.logradouro AS 'logradouro',
                                clientes.cep AS 'cep',
                                clientes.id_cidade AS 'id_cidade',
                                cidades.nome AS 'nome_cidade'
            FROM clientes INNER JOIN cidades ON (clientes.id_cidade = cidades.id)
            WHERE clientes.id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            DataRow linha = tabela.Rows[0];
            Cliente cliente = new Cliente();
            cliente.Id = Convert.ToInt32(linha["id_cliente"]);
            cliente.Nome = linha["nome_cliente"].ToString();
            cliente.Cpf = linha["cpf"].ToString();
            cliente.DataNascimento = Convert.ToDateTime(linha["data_nascimento"]);
            cliente.Numero = Convert.ToInt32(linha["numero"]);
            cliente.Complemento = linha["complemento"].ToString();
            cliente.Logradouro = linha["logradouro"].ToString();
            cliente.Cep = linha["cep"].ToString();
            cliente.FkCidade = Convert.ToInt32(linha["id_cidade"]);
            cliente.Cidade = new Cidade();
            cliente.Cidade.Nome = linha["nome_cidade"].ToString();

            return cliente;
        }

        public void Alterar(Cliente cliente)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE clientes SET
                                    nome = @NOME,
                                    cpf = @CPF,
                                    data_nascimento = @DATA_NASCIMENTO,
                                    numero = @NUMERO,
                                    complemento = @COMPLEMENTO,
                                    logradouro = @LOGRAROURO,
                                    cep = @CEP,
                                    id_cidade = @ID_CIDADE
            WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", cliente.Id);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRAROURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.Cep);
            comando.Parameters.AddWithValue("@ID_CIDADE", cliente.FkCidade);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }
    }
}
