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
    public partial class FrmVendas : Form
    {
        public FrmVendas()
        {
            InitializeComponent();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");
            SqlCommand comando = new SqlCommand("SELECT * FROM produto WHERE cod=@cod", banco);

            comando.Parameters.Add("@cod", SqlDbType.VarChar).Value = txtcodigo.Text;


            try
            {
                banco.Open();
                SqlDataReader leia = comando.ExecuteReader();
                if (leia.HasRows == false)
                {
                    throw new Exception("Produto não encontrado!");
                }
                leia.Read();
                lblcodigo.Text = Convert.ToString(leia["cod"]);
                lblBarra.Text = Convert.ToString(leia["codBarra"]);
                lbldescricao.Text = Convert.ToString(leia["descricao"]);
                lblcusto.Text = Convert.ToString(leia["precoCusto"]);
                lblvenda.Text = Convert.ToString(leia["precoVenda"]);
                lblativo.Text = Convert.ToString(leia["ativo"]);
                lblgrupo.Text = Convert.ToString(leia["grupo"]);
                lblmedida.Text = Convert.ToString(leia["unidadeMedida"]);
                lbldata.Text = Convert.ToString(leia["dataHoraCadastro"]);
                if (lblativo.Text == "Desativado")
                {
                    MessageBox.Show("Produto Indisponivel para venda!");
                    banco.Close();
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

        private void FrmVendas_Load(object sender, EventArgs e)
        {
            string SQL = "SELECT * FROM venda";

            SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");
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

        private void txtNome_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja inserir o nome do Cliente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                txtNome.ReadOnly = true;

            }
            else
            {
                txtNome.ReadOnly = false;
            }
        }

        private void txtcpf_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja inserir o CPF do Cliente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                txtcpf.ReadOnly = true;

            }
            else
            {
                txtcpf.ReadOnly = false;
            }
        }

        private void txtcalcular_Click(object sender, EventArgs e)
        {
            calcular();
        }
        private void calcular()
        {
            try
            {
                var preco = Convert.ToDecimal(txtpreco.Text);
                var quant = Convert.ToDecimal(txtquant.Text);
                var resul = preco * quant;

                txttotal.Text = resul.ToString();
            }
            catch
            {
                MessageBox.Show("Digite somente números!", "Erro de formato", MessageBoxButtons.OK);
            }

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            var data = DateTime.Now.ToLongDateString();
            var hora = DateTime.Now.ToLongTimeString();
            txtData.Text = (data + " " + hora);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");
            SqlCommand comando = new SqlCommand("insert into venda(clienteDocumento, clienteNome, obs, dataHora)values(@clienteDocumento, @clienteNome, @obs, @dataHora)", banco);

            comando.Parameters.Add("@clienteDocumento", SqlDbType.VarChar).Value = txtcpf.Text;
            comando.Parameters.Add("@clienteNome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@obs", SqlDbType.VarChar).Value = txtobs.Text;
            comando.Parameters.Add("@dataHora", SqlDbType.DateTime).Value = txtData.Text;

            try
            {
                banco.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Venda iniciada com Sucesso!");
                txtcpf.Text = "";
                txtNome.Text = "";
                txtobs.Text = "";
                txtData.Text = "";
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

        private void btnfinalizar_Click(object sender, EventArgs e)
        {
            SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");
            SqlCommand comando = new SqlCommand("insert into venda_produto(codVenda, codProduto, precoVenda, quantidade, total)values(@codvenda, @codProduto, @precoVenda, @quantidade, @total)", banco);


            var preco = Convert.ToDecimal(txtpreco.Text);
            var quant = Convert.ToDecimal(txtquant.Text);
            var resul = preco * quant;

            comando.Parameters.Add("@codVenda", SqlDbType.Int).Value = txtcodvenda.Text;
            comando.Parameters.Add("@codProduto", SqlDbType.Int).Value = txtcodproduto.Text;
            comando.Parameters.Add("@precoVenda", SqlDbType.Decimal).Value = preco;
            comando.Parameters.Add("@quantidade", SqlDbType.Decimal).Value = quant;
            comando.Parameters.Add("@total", SqlDbType.Decimal).Value = txttotal.Text;


            if (txtcodvenda.Text != "" && txtcodproduto.Text != "" && txtpreco.Text != "" && txtquant.Text != "" && txttotal.Text != "")
            {
                try
                {
                    banco.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastro efetuado com Sucesso!");
                    txtcodvenda.Text = "";
                    txtcodproduto.Text = "";
                    txtpreco.Text = "";
                    txtquant.Text = "";
                    txttotal.Text = "";

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
                txtcodvenda.Focus();
            }

        }

        private void txtpreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                if (((int)e.KeyChar) != ((int)Keys.Back))
                    if (e.KeyChar != ',')
                        e.Handled = true;

                    else if (txtpreco.Text.IndexOf(',') > 0)
                        e.Handled = true;
            }
        }

        private void txtquant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                if (((int)e.KeyChar) != ((int)Keys.Back))
                        e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
