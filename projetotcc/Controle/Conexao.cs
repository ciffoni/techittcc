using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using modelo;
using MySql.Data.MySqlClient;
namespace Controle
{
    public class Conexao
    {
        //configuração do servidor
        static private string servidor = "localhost";
        static private string bancodedados = "tcc";
        static private string usuario = "root";
        static private string senha = "";
        MySqlConnection conexao = null;//variavel de conexao
        //string de conexao
        static public string strCon = "server=" + servidor + ";database="+ bancodedados + ";user id=" + usuario + ";password=" + senha;
        //metodo para obter a conexao do mysql
        public MySqlConnection getConexao()
        {
            //metodo de conexao com retorno da conexao
            //passo a string de conexao como parametro
            MySqlConnection conn = new MySqlConnection(strCon);
            return conn;//retorna a conexao do mysql

        }
        //metodo de conectar ao banco
        public bool conectar()
        {
            bool resultado = false;
            getConexao().Open();//abro a conexao
            resultado = true;
            return resultado;//
        }
        public int cadastrar(string Sql,UsuarioModelo us)
        {
            //declaro a variavel de validacao
            int registro = 0;
            conexao = getConexao();//abro o banco
            conexao.Open();//abro o banco
            //comando sql
            MySqlCommand cmd = new MySqlCommand(Sql, conexao);
            //substiuir a variavel do objeto para o sql
            cmd.Parameters.AddWithValue("@login", us.login);
            cmd.Parameters.AddWithValue("@senha", us.senha);
            cmd.Parameters.AddWithValue("@usuario", us.usuario);
            //guarda o valor retornado da execução do sql
            registro=cmd.ExecuteNonQuery();
            conexao.Close();//fecha a sessao do banco
            return registro;
        }
        public DataTable obterdados(string Sql)
        {
            //crio uma tabela de dados
            DataTable dt = new DataTable();
            conexao = getConexao();//abro a conexao
            conexao.Open();
            //preparo o comando sql para executar
            MySqlCommand cmd = new MySqlCommand(Sql, conexao);
            //preparar para ler as informações do banco
            MySqlDataAdapter dados = new MySqlDataAdapter(cmd);
            dados.Fill(dt);//monta a estrutura
            conexao.Close();
            return dt;

        }
        public int atualizar(string sql,UsuarioModelo us)
        {
            int resultado = 0;
            conexao=getConexao();
            conexao.Open();
            MySqlCommand cmd= new MySqlCommand(sql,conexao);
            cmd.Parameters.AddWithValue("@login",us.login);
            cmd.Parameters.AddWithValue("@senha", us.senha);
            cmd.Parameters.AddWithValue("@usuario", us.usuario);
            cmd.Parameters.AddWithValue("@id", us.id);
            resultado = cmd.ExecuteNonQuery();
            conexao.Close();
            return resultado;
        }

    }
}
