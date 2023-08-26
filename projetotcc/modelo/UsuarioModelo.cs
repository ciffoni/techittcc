using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    //objeto usuario public
    public class UsuarioModelo
    {
        //variaveis do objeto usuario
        public int id;
        public string usuario;
        public string login;
        public string senha;
        public int perfil;
        //crio o construtor do objeto
        public UsuarioModelo()
        {
            //inicializar o objeto vazio
            id = 0;
            usuario = null;
            login = null;
            senha = null;
            perfil = 1;
        }
    }
}
