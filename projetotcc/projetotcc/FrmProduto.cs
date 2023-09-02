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
    public partial class FrmProduto : Form
    {
        string caminho;
        public FrmProduto()
        {
            InitializeComponent();
        }

        private void btnFoto_Click(object sender, EventArgs e)
        {
            //abrir a caixa de dialogo
           
            OpenFileDialog foto= new OpenFileDialog();
            foto.Filter = "Image file(*.jpg;*.png)|*.jpg;*.png";
           //verificar se clicou em OK no form
            if(foto.ShowDialog() == DialogResult.OK)
            {
                //guarda na variavel o caminho da foto
                caminho= foto.FileName;
                //converto a imagem para exibir no form
                Image arquivo= Image.FromFile(caminho);
                //exibe no form
                picFoto.Image = arquivo;
            }
            else
            {
                MessageBox.Show("Não há foto para exibir");
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            ProdutoModelo pmodelo= new ProdutoModelo(); ;
            pmodelo.Caminhofoto = caminho;
            pmodelo.preco = Convert.ToDecimal(txtValor.Text);
            pmodelo.Nome = txtNome.Text;
            ProdutoControle pcontrole = new ProdutoControle();
            if(pcontrole.crudProduto(pmodelo, 1) == true)
            {
                MessageBox.Show("Produto cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar o produto");
            }
        }

        private void FrmProduto_Load(object sender, EventArgs e)
        {
            Conexao con = new Conexao();
            dtProduto.DataSource = con.obterdados("select * from produto");
        }
    }
}
