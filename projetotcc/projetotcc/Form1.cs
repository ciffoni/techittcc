using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controle;//incluir o projeto
using modelo;

namespace projetotcc
{
    public partial class FrmUsuario : Form
    { //declaração variaveis publicas no form
        //chamando o controle da conexao
        Conexao con = new Conexao();
        //chamando o objeto usuario
        UsuarioModelo usuario = new UsuarioModelo();
        //variavel do registro do datagrid view
        int registroId;
        int perfil;
        //contruindo o formulario
        public FrmUsuario()
        {
           
            //chamando os componentes da tela
            InitializeComponent();
        }

        private void btnConexao_Click(object sender, EventArgs e)
        {//instanciando o objeto controle da conexao
            Conexao conec = new Conexao();
            //se retornar verdade
            if (conec.conectar() == true)
            {
                //conexao ok
                MessageBox.Show("Conectado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao conectar!");
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //atribuindo o valor do formulario para o objeto
            usuario.login = txtLogin.Text;
            usuario.usuario = txtNome.Text;
            usuario.senha=con.getMD5Hash(txtSenha.Text);
            usuario.perfil = perfil;
            //usuario.id = Convert.ToInt32(txtCodigo.Text);
           if( con.cadastrar("insert into usuario(nomeusuario,login,senha,perfil)values(@usuario,@login,@senha,@perfil)", usuario) == 1)
            {
                MessageBox.Show("Cadastro com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar!");
            }
            
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            cboPerfil.DataSource = con.obterdados("select * from perfil");
            cboPerfil.DisplayMember = "perfil";
            cboPerfil.ValueMember = "id"; 
            dtUsuario.DataSource = con.obterdados("select * from usuario");
        }

        private void dtUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            registroId = Convert.ToInt32(dtUsuario.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            txtCodigo.Text = dtUsuario.Rows[e.RowIndex].Cells["idusuario"].Value.ToString();
            txtLogin.Text = dtUsuario.Rows[e.RowIndex].Cells["login"].Value.ToString();
            txtNome.Text = dtUsuario.Rows[e.RowIndex].Cells["nomeusuario"].Value.ToString();
            txtSenha.Text = dtUsuario.Rows[e.RowIndex].Cells["senha"].Value.ToString();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            //popula o objeto usuario com as informações do form
           //se não clicar no grid não atualiza
            if (registroId != 0)
            {


                usuario.id = Convert.ToInt32(txtCodigo.Text);
                usuario.usuario = txtNome.Text;
                usuario.login = txtLogin.Text;
                usuario.senha = con.getMD5Hash(txtSenha.Text);
                usuario.perfil = perfil;
                if (con.atualizar("update usuario set login=@login,nomeusuario=@usuario,senha=@senha,perfil=@perfil where idusuario=@id", usuario) == 1)
                {
                    MessageBox.Show("Atualizado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar o usuario");
                }
            }
            else
            {
                MessageBox.Show("Favor escolher um usuário!");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(registroId != 0)
            {
                if (con.apagar("delete from usuario where idusuario=@id", registroId) == 1)
                {
                    MessageBox.Show("Usuario apagado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao ecluir o usuario!");
                }
            }
            else
            {
                MessageBox.Show("Favor escolher um registro!");
            }
        }

        private void cboPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            perfil = Convert.ToInt32(((DataRowView)cboPerfil.SelectedItem)["id"]);

        }
    }
}
