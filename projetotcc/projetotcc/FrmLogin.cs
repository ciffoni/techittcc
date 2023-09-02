using Controle;
using modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetotcc
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexao conexao = new Conexao();
            UsuarioModelo usModelo = new UsuarioModelo();
            int codigoUsuario;
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                MessageBox.Show("Campo usuário está vazio!");
                txtUsuario.Focus();//pega o focu 
            }
            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Campo senha está vazio!");
                txtSenha.Focus();
            }
            usModelo.login = txtUsuario.Text;
            usModelo.senha = conexao.getMD5Hash(txtSenha.Text);
            //verificar se encontrou o usuario
            codigoUsuario = conexao.logar(usModelo);
            if ( codigoUsuario> 0)
            {
                FrmPrincipal principal= new FrmPrincipal(codigoUsuario);
                this.Hide();//oculta a janela do login
                principal.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválida!");
            }
        }
    }
}
