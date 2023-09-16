using Controle;
using modelo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetotcc
{
    public partial class FrmProduto : Form
    {
        string caminho;
        int codProduto;
        Conexao con = new Conexao();
        string sql= "select * from produto";
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
            pmodelo.Data = dataValidade.Value;
            if (chkValidade.Checked)
            {
                pmodelo.Valida = true;
            }
            else
            {
                pmodelo.Valida = false;
            }
           
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
           
            dtProduto.DataSource = con.obterdados(sql);
        }

        private void chkValidade_CheckedChanged(object sender, EventArgs e)
        {
            if(chkValidade.Checked == true)
            {
                label5.Visible = true;
                dataValidade.Visible = true;
            }
            else
            {
                label5.Visible = false;
                dataValidade.Visible = false;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void dtProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int validade;
            codProduto = Convert.ToInt32(dtProduto.Rows[e.RowIndex].Cells[0].Value);
            txtNome.Text = dtProduto.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtValor.Text = dtProduto.Rows[e.RowIndex].Cells[2].Value.ToString();
            picFoto.ImageLocation = dtProduto.Rows[e.RowIndex].Cells[3].Value.ToString();
            validade = Convert.ToInt32(dtProduto.Rows[e.RowIndex].Cells[4].Value);
            if (validade == 0)
            {
                chkValidade.Checked =false;
            }
            else
            {
                chkValidade.Checked = true;
            }
            dataValidade.Value = Convert.ToDateTime(dtProduto.Rows[e.RowIndex].Cells[5].Value.ToString());
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Conexao con = new Conexao();
           
            bool teste;
            int valor;
            if (string.IsNullOrEmpty(txtPesquisa.Text))
            {
                sql = "select * from produto";
            }
            else
            {
                //testar se o textpesquisar é numero
                teste = int.TryParse(txtPesquisa.Text, out valor);
                if (teste)
                {
                    sql = "select * from produto where idproduto=" + valor;

                }
                else
                {
                    sql = "select * from produto where nome like '%" + txtPesquisa.Text+"%'";

                }
            }
            dtProduto.DataSource = con.obterdados(sql);

        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Conexao con = new Conexao();
            string sql = null;
            bool teste;
            int valor;
            if (string.IsNullOrEmpty(txtPesquisa.Text))
            {
                sql = "select * from produto";
            }
            else
            {
                //testar se o textpesquisar é numero
                teste = int.TryParse(txtPesquisa.Text, out valor);
                if (teste)
                {
                    sql = "select * from produto where idproduto=" + valor;

                }
                else
                {
                    sql = "select * from produto where nome like '%" + txtPesquisa.Text + "%'";

                }
            }
            dtProduto.DataSource = con.obterdados(sql);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //nome do arquivo a ser criado
            string endereco= "relatorioExcel.csv";
            //criar a escrita no arquivo
            MySqlConnection conexao = con.getConexao();
            using (StreamWriter writer = new StreamWriter(endereco, false, Encoding.GetEncoding("iso-8859-15")))
            {
                //cria o cabeçalho do excel
                writer.WriteLine("nome;valor"); conexao.Open();
                MySqlCommand cmd=new MySqlCommand(sql,conexao);
               
                //listar as informações do banco
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    //enquanto tiver registro
                    while (reader.Read())
                    {
                        writer.WriteLine(Convert.ToString(reader["nome"]) + ";" + Convert.ToString(reader["valor"]));
                    }
                }
                conexao.Close();
            }
            MessageBox.Show("Relatorio gerado com sucesso.");

        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            Funcoes func = new Funcoes();
            func.gerarPdf(sql, "Listar produto");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawString(dtProduto.Rows[0].Cells[1].Value.ToString(), Font, Brushes.Black, 20,20);

        }

       
    }
}
