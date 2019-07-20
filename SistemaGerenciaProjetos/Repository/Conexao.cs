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
<<<<<<< HEAD
            conexao.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\Downloads\Entra21-C-Sharpe-Trabalho-CRUD-Projetos-master\SistemaGerenciaProjetos\View\App_Data\BD_Projeto.mdf;Integrated Security=True";
=======
            conexao.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\elian\Documents\GitHub\Entra21-C-Sharpe-Trabalho-CRUD-Projetos\SistemaGerenciaProjetos\View\App_Data\BD_Projeto.mdf;Integrated Security=True";
>>>>>>> 20f8bacd697e6541d3a8912ee1270b94c8eb2995
                //NAO FUNCIONOU - ConfigurationManager.ConnectionStrings["DefautConnection"].ConnectionString;
            conexao.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            return comando;
        }
    }
}
