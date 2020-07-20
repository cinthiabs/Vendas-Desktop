using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrmLogin
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lbldata.Text = DateTime.Now.ToLongDateString();

            DateTime tempo = DateTime.Now;
            string dia = "Bom Dia!";
            string tarde = "Boa Tarde!";
            string noite = "Boa Noite!";

            if (tempo.Hour > 6 && tempo.Hour < 12)
                lbDia.Text = dia;
            else if (tempo.Hour >= 12 && tempo.Hour < 18)
                lbDia.Text = tarde;
            else
               lbDia.Text = noite;

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmHome frmHome = new FrmHome();
            this.Hide();
            frmHome.Show();
        }

        private void btncadastroProduto_Click(object sender, EventArgs e)
        {
            FrmCadastroProduto frmCadastroProduto = new FrmCadastroProduto();
            this.Hide();
            frmCadastroProduto.Show();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            FrmConsulta frmConsulta = new FrmConsulta();
            this.Hide();
            frmConsulta.Show();
        }

        private void btnVenda_Click(object sender, EventArgs e)
        {
            FrmVendas frmVendas = new FrmVendas();
            this.Hide();
            frmVendas.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
          DialogResult result = MessageBox.Show("Tem certeza que deseja sair?", "Confirmar",MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                FrmLogin frmLogin = new FrmLogin();
                this.Hide();
                frmLogin.Show();
            }
        }

        private void btvenda_Click(object sender, EventArgs e)
        {
            FrmVendas frmVendas = new FrmVendas();
            this.Hide();
            frmVendas.Show();

        }

        private void btnatualizar_Click(object sender, EventArgs e)
        {
            FrmConsulta frmConsulta = new FrmConsulta();
            this.Hide();
            frmConsulta.Show();
        }
        public void RegistroProduto()
        {
            SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");
            SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM produto", banco);

            banco.Open();
            lblproduto.Text = sqlCommand.ExecuteScalar().ToString();
            banco.Close();
        }
        public void RegistroVenda()
        {
            SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");
            SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM venda_produto", banco);

            banco.Open();
            lblvenda.Text = sqlCommand.ExecuteScalar().ToString();
            banco.Close();
        }

        public void Totalvendas()
        {
            SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");
            SqlCommand sqlCommand = new SqlCommand("SELECT SUM(total)FROM venda_produto", banco);
          
            banco.Open();
            lbltotal.Text = sqlCommand.ExecuteScalar().ToString();
            banco.Close();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            RegistroVenda();
            RegistroProduto();
            Totalvendas();
        }
    }
}
