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
    public partial class FrmCadastro : Form
    {
        public FrmCadastro()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            this.Hide();
            frmLogin.Show();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            //conexao banco
            SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");
            SqlCommand comando = new SqlCommand("INSERT INTO CADASTRO( NOME, SEXO, EMAIL, LOGIN, SENHA)VALUES(@NOME, @SEXO, @EMAIL, @LOGIN, @SENHA)", banco);

            comando.Parameters.Add("@NOME", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@SEXO", SqlDbType.VarChar).Value = cbSexo.Text;
            comando.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtEmail.Text;
            comando.Parameters.Add("@LOGIN", SqlDbType.VarChar).Value = txtLogin.Text;
            comando.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = txtSenha.Text;

            if (txtNome.Text != "" && txtEmail.Text != "" && cbSexo.Text != "" && txtEmail.Text != "" && txtLogin.Text != "" && txtSenha.Text != "")
            {
                try
                {
                    banco.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastro efetuado com Sucesso!", "Sucesso"+ MessageBoxButtons.OK);
                    txtNome.Text = "";
                    cbSexo.Text = "";
                    txtEmail.Text = "";
                    txtLogin.Text = "";
                    txtSenha.Text = "";
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
                MessageBox.Show("Por favor digite todos os campos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNome.Focus();
            }
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            if (txtLogin.Text.Length > 20)
            {
                MessageBox.Show("Maximo de caracteres permitido 20", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            if (txtLogin.Text.Length < 3)
            {
                MessageBox.Show("Minino de caracteres permitido 3", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
