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
using System.Net.Mail;
using System.Management;

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
        public string enviarEmail(String dados,string texto)
        {
            string msg = null;
            UsuarioModelo usuario = new UsuarioModelo();
            if (dados == "")
            {
                msg = "Dados não preenchidos para enviar email";
                return msg;
            }
            Conexao com = new Conexao();
            MySqlConnection con = com.getConexao();
           
            string sql = "SELECT * from usuario where email=@usuario";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            //tratar algum erro de acesso
            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                cmd.Parameters.AddWithValue("@usuario", dados);
                if (reader.HasRows)
                {
                    reader.Read();
                    usuario.usuario = reader["usuario"].ToString();
                    usuario.login = reader["login"].ToString();
                    usuario.email = reader["email"].ToString();
                }
                else
                {
                    msg = "Usuario não localizado";
                    return msg;
                }
                //serviço de email
                SmtpClient cliente = new SmtpClient(); 
                    //smtp.gmail.com
                cliente.Host = "smtp.office365.com";//servidor do hotmail
                cliente.Port = 587;
                cliente.EnableSsl=true;
                cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new System.Net.NetworkCredential("jorge.ciffoni@sistemafiep.org.br", "senha");
                MailMessage mail= new MailMessage();
                mail.Sender = new MailAddress("jorge.ciffoni@sistemafiep.org.br", "sistema tecit");
                mail.From = new MailAddress("jorge.ciffoni@sistemafiep.org.br","Admiistrador");
                mail.To.Add(new MailAddress(usuario.email, usuario.login));
                mail.Subject = "Recuperar email";
                mail.Body = texto;
                mail.IsBodyHtml= true;
                mail.Priority = MailPriority.High;
                try
                {
                    //tratar o acesso ao email
                    cliente.Send(mail);
                    msg = "Email enviado com sucesso";
                }catch(Exception ex)
                {
                    msg="Erro ao enviar email:"+ ex.Message;

                }
            }
            catch (MySqlException ms)
            {
                msg = "Erro no banco de dados" + ms.Message;
            }


            return msg;
        }
        //backup
        public void exportar_bd()
        {
            //data
            DateTime data;
            //pega a data atual
            data = DateTime.Today;
            string nomeArquivo = "c:\\backup_.sql";
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = com.getConexao();
            using (MySqlBackup md= new MySqlBackup(cmd))
            {
                cmd.Connection = con;
                con.Open();
                md.ExportInfo.AddCreateDatabase = true;
                md.ExportInfo.ExportProcedures = true;
                md.ExportInfo.ExportRows = true;
                md.ExportToFile(nomeArquivo);
                con.Close();
            }
        }
        public void Import_bd()
        {
            string nomeArquivo = "c:\\backup_.sql";
            string StrCon = "server=localhost;user=root;pwd=''";
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(StrCon);
            using (MySqlBackup md = new MySqlBackup(cmd))
            {
                cmd.Connection = con;
                con.Open();
                md.ImportFromFile(nomeArquivo);
                con.Close();
            }
        }
    }
}
