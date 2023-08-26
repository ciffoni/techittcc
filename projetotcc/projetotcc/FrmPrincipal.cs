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
        public FrmPrincipal()
        {
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
    }
}
