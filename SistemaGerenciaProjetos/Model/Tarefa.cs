using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tarefa
    {
        public int id;
        public int FkUsuario;
        public int FkProjeto;
        public int FkCategoria;
        public string Titulo;
        public string Descricao;
        public DateTime Duracao;
    }
}
