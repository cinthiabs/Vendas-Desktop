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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {

            SqlConnection banco = new SqlConnection("Data Source=DESKTOP-7VCU04E;Initial Catalog=Projeto;Integrated Security=True");
            SqlCommand comando = new SqlCommand("SELECT * FROM CADASTRO where LOGIN=@LOGIN AND SENHA=@SENHA", banco);

            comando.Parameters.Add("@LOGIN", SqlDbType.VarChar).Value = txtLogin.Text;
            comando.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = txtSenha.Text;

            if (txtLogin.Text == "" && txtSenha.Text == "")
            {
                MessageBox.Show("Preencha todos os campos!");
                txtLogin.Focus();
            }

            try
            {
                banco.Open();
                SqlDataReader leia = comando.ExecuteReader();
                if (leia.HasRows == false)
                {
                    throw new Exception("Usuário ou senha Incorreta!");

                }
                else
                {
                    leia.Read();
                    FrmHome frmHome = new FrmHome();
                    this.Hide();
                    frmHome.Show();
                  
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

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //vazio
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmCadastro frmCadastro = new FrmCadastro();
            this.Hide();
            frmCadastro.Show();
        }
    }
}
