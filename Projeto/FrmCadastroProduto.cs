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
    public partial class FrmCadastroProduto : Form
    {
        public FrmCadastroProduto()
        {
            InitializeComponent();
        }
        //banco
        SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");

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
            DialogResult result = MessageBox.Show("Tem certeza que deseja sair?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                FrmLogin frmLogin = new FrmLogin();
                this.Hide();
                frmLogin.Show();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //vazio
        }

        private void FrmCadastroProduto_Load(object sender, EventArgs e)
        {
            listaGrid();

        }


        private void timer_Tick_1(object sender, EventArgs e)
        {
            var data = DateTime.Now.ToLongDateString();
            var hora = DateTime.Now.ToLongTimeString();
             txtData.Text = (data +" " +hora);
        }

        public void listaGrid()
        {
            string SQL = "SELECT * FROM produto";

            SqlCommand sqlCommand = null;
            sqlCommand = new SqlCommand(SQL, banco);

            try
            {
                SqlDataAdapter sqlData = new SqlDataAdapter(sqlCommand);
                DataTable dtLista = new DataTable();

                sqlData.Fill(dtLista);
                dataGridView.DataSource = dtLista;
            }
            catch
            {
                MessageBox.Show("Ocorreu erro ao se conectar!");
                banco.Close();
            }


        }

        private void btncadastrar_Click(object sender, EventArgs e)
        {
            //banco
            SqlCommand comando = new SqlCommand("insert into produto (descricao, grupo, codBarra, unidadeMedida, precoCusto, precoVenda, dataHoraCadastro, ativo)values(@descricao, @grupo, @codBarra, @unidadeMedida, @precoCusto, @precoVenda, @dataHoraCadastro, @ativo)", banco);
               //converter
            decimal venda = Convert.ToDecimal(txtPrecovenda.Text);
            decimal custo = Convert.ToDecimal(txtPrecocusto.Text);

            comando.Parameters.Add("@descricao", SqlDbType.VarChar).Value = txtDescricao.Text;
            comando.Parameters.Add("@codBarra", SqlDbType.VarChar).Value = txtBarra.Text;
            comando.Parameters.Add("@precoCusto", SqlDbType.Decimal).Value = custo;
            comando.Parameters.Add("@precoVenda", SqlDbType.Decimal).Value = venda;
            comando.Parameters.Add("@dataHoraCadastro", SqlDbType.DateTime).Value = txtData.Text;
            comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = cbAtivo.Text;//combo
            comando.Parameters.Add("@unidadeMedida", SqlDbType.VarChar).Value = cbMedida.Text;//combo
            comando.Parameters.Add("@grupo", SqlDbType.VarChar).Value = cbgrupo.Text;//combo


            if (txtDescricao.Text != "" && cbgrupo.Text != "" && txtPrecocusto.Text != "" && txtPrecovenda.Text != "" && txtData.Text != "" )
            {
                try
                {
                    banco.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastro efetuado com Sucesso!");
                    txtDescricao.Text = "";
                    txtBarra.Text = "";
                    txtPrecocusto.Text = "";
                    txtPrecovenda.Text = "";
                    txtData.Text = "";
                    cbAtivo.Text = "";
                    cbMedida.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    banco.Close();

                }
            }
            else
            {
                MessageBox.Show("Por favor Digite todos os Campos!");
                txtDescricao.Focus();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtDescricao.Text = "";
            txtBarra.Text = "";
            cbAtivo.Text = "";
            cbgrupo.Text = "";
            txtPrecocusto.Text = "";
            txtPrecovenda.Text = "";
            txtData.Text = "";
            cbMedida.Text = "";
        }

        private void txtPrecocusto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                if (((int)e.KeyChar) != ((int)Keys.Back))
                    if (e.KeyChar != ',')
                        e.Handled = true;

                    else if (txtPrecocusto.Text.IndexOf(',') > 0)
                        e.Handled = true;
            }
        }

        private void txtPrecovenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                if (((int)e.KeyChar) != ((int)Keys.Back))
                    if (e.KeyChar != ',')
                        e.Handled = true;

                    else if (txtPrecovenda.Text.IndexOf(',') > 0)
                        e.Handled = true;
            }
        }
    }
}
