using modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle
{
    public class ProdutoControle
    {
        //valor de resultado mysql
        bool resultado = false;
        //metodo sql
        string sql = null;
        //instanciar o controle conexao
        Conexao con = new Conexao();

        public bool crudProduto(ProdutoModelo prod, int operacao)
        {
            try
            {
                switch (operacao)
                {// inserir produto
                    case 1:
                        sql = "insert into produto(nome,valor,foto) values(@nome,@preco,@foto)";
                        break;
                        case 2://atualizar produto
                        sql = "UPdate produto set nome=@nome,valor=@preco,foto=@foto where idproduto=@id";
                        break;
                    case 3://apagar produto
                        sql = "delete from produto where idproduto=@id";
                        break;
                }
                //vetor de atributos
                string[] campos = { "@nome", "@preco", "@foto" };
                object[] valores = { prod.Nome, prod.preco, prod.Caminhofoto };
                if (con.ProdutoCrud(campos,valores,sql)>=1)
                    resultado=true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultado;

        }
    }
}
