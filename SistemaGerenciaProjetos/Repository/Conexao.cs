using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Conexao
    {
        public static SqlCommand Conectar()
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\Downloads\Entra21-C-Sharpe-Trabalho-CRUD-Projetos-master\SistemaGerenciaProjetos\View\App_Data\BD_Projeto.mdf;Integrated Security=True";
                //NAO FUNCIONOU - ConfigurationManager.ConnectionStrings["DefautConnection"].ConnectionString;
            conexao.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            return comando;
        }
    }
}
