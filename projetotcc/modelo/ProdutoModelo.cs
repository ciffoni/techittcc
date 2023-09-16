using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class ProdutoModelo
    {
        public int codigo;
        private string nomeProduto;
        private decimal valor;
        private string foto;//foto caminho
        private DateTime data;//campo data
        private bool valida;//campo checkbox
        public ProdutoModelo()
        {
            codigo = 0;
            nomeProduto = null;
            valor = 0;
            foto = null;
            valida = false;
        }
        //metodo de acesso a variavel
        public string Nome
        {
            //obter a informação
            get { return nomeProduto; }
            //alterar o valor
            set { nomeProduto = value; }
        }
        public decimal preco
        {
            get { return valor; }
            set { valor = value; }
        }
        public string Caminhofoto
        {
            get { return foto; }
            set { foto = value; }
        }
        public DateTime Data
        {
            get { return data; }
            set{ data = value; }
        }
        public bool Valida
        {
            get { return valida; }
            set { valida = value; }
        }
    }
}
