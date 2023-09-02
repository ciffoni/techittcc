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
    public partial class FrmPrincipal : Form
    {
        int codigo;//declaro o codigo
        //declaro o valor no parametro para passar o ID do 
        //usuário
        UsuarioModelo us = new UsuarioModelo();
        Conexao com = new Conexao();
        public FrmPrincipal(int valor)
        {
            codigo = valor;
            MessageBox.Show("Codigo usuário" + codigo.ToString());
            InitializeComponent();
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chamo o formulario
            FrmUsuario usuario = new FrmUsuario();
           //digo que é um filho
            usuario.MdiParent = this;
            usuario.Show();//mostro ele
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //buscar as informações do usuario
            us = com.CarregaUsuario(codigo);
            MessageBox.Show("Seja bem vindo" + us.usuario);
            switch (us.perfil)
            {
                case 1:
                usuarioToolStripMenuItem.Enabled = false;
                    break;
                case 2:
                    usuarioToolStripMenuItem.Enabled = true;
                    break;

            }

        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProduto prod = new FrmProduto();
            prod.MdiParent = this;
            pictureBox1.Visible = false;
            prod.Show();
        }
    }
}
