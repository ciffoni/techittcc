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
        public int ProdutoCrud(string[] campos, object[] valores,string sql)
        {
            int registro = 0;
            conexao = getConexao();
            conexao.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            for(int i = 0; i<valores.Length; i++)
            {
                cmd.Parameters.AddWithValue(campos[i], valores[i]);
            }
            registro = cmd.ExecuteNonQuery();
            conexao.Close();
            return registro;
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
            cmd.Parameters.AddWithValue("@perfil", us.perfil);
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
            cmd.Parameters.AddWithValue("@perfil", us.perfil);
            cmd.Parameters.AddWithValue("@usuario", us.usuario);
            cmd.Parameters.AddWithValue("@id", us.id);
            resultado = cmd.ExecuteNonQuery();
            conexao.Close();
            return resultado;
        }
        public int apagar(string sql, int codigo)
        {
            int resultado = 0;
            conexao = getConexao();
            conexao.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", codigo);
            resultado = cmd.ExecuteNonQuery();
            conexao.Close();
            return resultado;

        }
        public int logar(UsuarioModelo us)
        {
            try
            {
                int registro = 0;
                string sql = "select idusuario from usuario where login=@usuario and senha=@senha";
                conexao = getConexao();
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@usuario", us.login);
                cmd.Parameters.AddWithValue("@senha", us.senha);
                //retorna o valor do id do usuario consultado
                registro = Convert.ToInt32(cmd.ExecuteScalar());
                return registro;
                //trato  o erro da exceção 
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UsuarioModelo CarregaUsuario(int codigo)
        {//instancia o objeto usuario
            UsuarioModelo us = new UsuarioModelo();
            conexao= getConexao();
            conexao.Open();
            string sql = "SELECT * from usuario where idusuario= @id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", codigo);
            MySqlDataReader registro=cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                us.usuario = registro["nomeusuario"].ToString();
                us.login = registro["login"].ToString();
                us.perfil = Convert.ToInt32(registro["perfil"].ToString());

            }
            registro.Close();

            //retorna os dados do usuario
            return us;
        }
        public string getMD5Hash(string senha)
        {
            System.Security.Cryptography.MD5 md5=System.Security.Cryptography.MD5.Create();
            //vetor de bytes da senha passada codificar para tabela ASCII
            byte[] imputBytes=System.Text.Encoding.ASCII.GetBytes(senha);
            //arquivo hash em bytes convertendo
            byte[] hash=md5.ComputeHash(imputBytes);
            //construir a string
            StringBuilder sb = new StringBuilder();
            //trocar o caracter convertido 
            //percorrer o vetor hash
            for(int i = 0; i < hash.Length; i++)
            {
                //adicionar a codificação X2 em cada caracter lido do hash
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
       
    }
}
