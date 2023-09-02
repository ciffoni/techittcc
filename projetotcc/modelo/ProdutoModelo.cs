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
        private string foto;
        public ProdutoModelo()
        {
            codigo = 0;
            nomeProduto = null;
            valor = 0;
            foto = null;
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
    }
}
