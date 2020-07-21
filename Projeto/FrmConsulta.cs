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
    public partial class FrmConsulta : Form
    {
        public FrmConsulta()
        {
            InitializeComponent();
        }

        SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnhome_Click(object sender, EventArgs e)
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

        private void btnsair_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja sair?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                FrmLogin frmLogin = new FrmLogin();
                this.Hide();
                frmLogin.Show();
            }
        }

        private void FrmConsulta_Load(object sender, EventArgs e)
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand("SELECT * FROM produto WHERE cod=@cod", banco);

            comando.Parameters.Add("@cod", SqlDbType.VarChar).Value = txtCodigo.Text;


            try
            {
                banco.Open();
                SqlDataReader leia = comando.ExecuteReader();
                if (leia.HasRows == false)
                {
                    throw new Exception("Produto não encontrado!");
                }
                leia.Read();
                txtCodigo.Text = Convert.ToString(leia["cod"]);
                txtBarra.Text = Convert.ToString(leia["codBarra"]);
                txtDescricao.Text = Convert.ToString(leia["descricao"]);
                txtPrecocusto.Text = Convert.ToString(leia["precoCusto"]);
                txtPrecovenda.Text = Convert.ToString(leia["precoVenda"]);
                cbAtivo.Text = Convert.ToString(leia["ativo"]);
                cbgrupo.Text = Convert.ToString(leia["grupo"]);
                cbMedida.Text = Convert.ToString(leia["unidadeMedida"]);
                txtData.Text = Convert.ToString(leia["dataHoraCadastro"]);
                if (cbAtivo.Text == "Desativado")
                {
                    MessageBox.Show("Produto Desativado!");
                }
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
         
            SqlCommand comando = new SqlCommand("update produto set descricao=@descricao, grupo=@grupo, codBarra=@codBarra, unidadeMedida=@unidadeMedida, precoCusto=@precoCusto, precoVenda=@precoVenda, dataHoraCadastro=@dataHoraCadastro, ativo=@ativo where cod=@cod", banco);
            //converter 
                decimal venda = Convert.ToDecimal(txtPrecovenda.Text);
                decimal custo = Convert.ToDecimal(txtPrecocusto.Text);
            
            comando.Parameters.Add("@cod", SqlDbType.Int).Value = txtCodigo.Text;
            comando.Parameters.Add("@descricao", SqlDbType.VarChar).Value = txtDescricao.Text;
            comando.Parameters.Add("@codBarra", SqlDbType.VarChar).Value = txtBarra.Text;
            comando.Parameters.Add("@precoCusto", SqlDbType.Decimal).Value = custo;
            comando.Parameters.Add("@precoVenda", SqlDbType.Decimal).Value = venda;
            comando.Parameters.Add("@dataHoraCadastro", SqlDbType.DateTime).Value = txtData.Text;
            comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = cbAtivo.Text;//combo
            comando.Parameters.Add("@unidadeMedida", SqlDbType.VarChar).Value = cbMedida.Text;//combo
            comando.Parameters.Add("@grupo", SqlDbType.VarChar).Value = cbgrupo.Text;//combo

            if (txtDescricao.Text != "" && cbgrupo.Text != "" && txtPrecocusto.Text != "" && txtPrecovenda.Text != "" && txtData.Text != "")
            {
                try
                {
                    banco.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Produto atualizado com Sucesso!", "Sucesso", MessageBoxButtons.OK);
                    txtCodigo.Text = "";
                    txtDescricao.Text = "";
                    txtBarra.Text = "";
                    txtPrecocusto.Text = "";
                    txtPrecovenda.Text = "";
                    txtData.Text = "";
                    cbAtivo.Text = "";
                    cbMedida.Text = "";
                    cbgrupo.Text = "";

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
            

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var data = DateTime.Now.ToLongDateString();
            var hora = DateTime.Now.ToLongTimeString();
            txtData.Text = (data + " " + hora);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
           
            SqlCommand comando = new SqlCommand("DELETE FROM produto WHERE cod=@cod", banco);

            comando.Parameters.Add("@cod", SqlDbType.VarChar).Value = txtCodigo.Text;

            DialogResult result = MessageBox.Show("Tem certeza que deseja excluir?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    banco.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastro efetuado com Sucesso!");
                    txtCodigo.Text = "";
                    txtDescricao.Text = "";
                    txtBarra.Text = "";
                    txtPrecocusto.Text = "";
                    txtPrecovenda.Text = "";
                    txtData.Text = "";
                    cbAtivo.Text = "";
                    cbMedida.Text = "";
                    cbgrupo.Text = "";

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
