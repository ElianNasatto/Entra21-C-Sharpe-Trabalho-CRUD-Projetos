using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cliente
    {
        public int Id;
        public string Nome;
        public string Cpf;
        public DateTime DataNascimento;
        public int Numero;
        public string Complemento;
        public string Logradouro;
        public string Cep;
        public int FkCidade;
        public Cidade Cidade;
    }
}
