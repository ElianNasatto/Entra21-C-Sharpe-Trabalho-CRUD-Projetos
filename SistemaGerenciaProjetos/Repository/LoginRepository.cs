using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LoginRepository
    {
        public bool VerificaLogin(string login,string senha)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "SELECT (id) FROM usuarios WHERE login = @LOGIN AND senha = @SENHA";
            comando.Parameters.AddWithValue("@LOGIN", login);
            comando.Parameters.AddWithValue("@SENHA", senha);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if (tabela.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
