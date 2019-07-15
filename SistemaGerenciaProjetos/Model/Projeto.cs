using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Projeto
    {
        public int Id;
        public string Nome;
        public DateTime DataCriacao;
        public DateTime DataFinalizacao;
        public int FkCliente;
        public Cliente Cliente;
    }
}
