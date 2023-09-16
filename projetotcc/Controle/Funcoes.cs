using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using modelo;
using System.Diagnostics;

namespace Controle
{
    public class Funcoes
    {
        Conexao com = new Conexao();
        public Funcoes()
        {

        }
        public void gerarPdf(string sql,string titulo)
        {
            //conexao com o BD
            ProdutoModelo pm=new ProdutoModelo();
            MySqlConnection con = com.getConexao();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            //guardar as informações
            MySqlDataAdapter dados;
            DataSet ds = new DataSet();
            try
            {
                int i = 0;//registros da consulta
                int ypoint = 0;//espaço do conteudo
                con.Open();
                dados = new MySqlDataAdapter(cmd);//guarda os valores
                dados.Fill(ds);//organiza as informações
                PdfDocument pdf = new PdfDocument();//chama a classe PDf    
                pdf.Info.Title = titulo;//titulo 
                PdfPage page = pdf.AddPage();//adicionar a pagina
                XGraphics grafic= XGraphics.FromPdfPage(page);//grafico para pagina
                XFont fonte = new XFont("arial", 12, XFontStyle.Regular);//defino a fonte do arquivo
                ypoint = ypoint + 35; //acrescento 75 ao posição
                //titulos do PDF
                grafic.DrawString(ds.Tables[0].Columns[1].ColumnName, fonte, XBrushes.Black, new XRect(20, ypoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                grafic.DrawString(ds.Tables[0].Columns[2].ColumnName, fonte, XBrushes.Black, new XRect(120, ypoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                ypoint = ypoint + 35;
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pm.Nome = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    pm.preco = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[2].ToString());
                    grafic.DrawString(pm.Nome, fonte, XBrushes.Black, new XRect(20, ypoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                    grafic.DrawString(pm.preco.ToString(), fonte, XBrushes.Black, new XRect(120, ypoint, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                    ypoint = ypoint + 15;
                }
                string pdffilename = "listarProduto.pdf";
                pdf.Save(pdffilename);//salvo o arquivo
                Process.Start(pdffilename);//abrir o arquivo
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
        }
    }
}
