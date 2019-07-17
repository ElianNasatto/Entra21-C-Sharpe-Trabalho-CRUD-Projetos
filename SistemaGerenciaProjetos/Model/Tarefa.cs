using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tarefa
    {
        public int Id;
        public int FkUsuario;
        public int FkProjeto;
        public int FkCategoria;
        public string Titulo;
        public string Descricao;
        public DateTime Duracao;

        //Criado esses dois objetos pois quando mostramos as tarefas mostramos tambem o nome do projeto e usuario que a tarefa pertence
        public Usuario Usuario;
        public Projeto Projeto;
    }
}
