using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SaleManage.Database;
using SaleManage.Common;
namespace SaleManage
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            txtUserId.Text = "ユーザーID";
            txtUserId.ForeColor = Color.DarkGray;


            txtPassword.Text = "パスワード";
            txtPassword.ForeColor = Color.DarkGray;
        }
      
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "パスワード")
            {
                txtPassword.Text = "";
                txtPassword.PasswordChar = '*';
                txtPassword.ForeColor = Color.Black;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "パスワード";
                txtPassword.PasswordChar = '\0';
                txtPassword.ForeColor = Color.Gray;
            }
        }

        private void txtUserId_Enter(object sender, EventArgs e)
        {
            if (txtUserId.Text == "ユーザーID")
            {
                txtUserId.Text = "";
                txtUserId.ForeColor = Color.Black;
            }
        }

        private void txtUserId_Leave(object sender, EventArgs e)
        {
            if (txtUserId.Text == "")
            {
                txtUserId.Text = "ユーザーID";
                txtUserId.ForeColor = Color.Gray;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
             if (txtUserId.Text == "ユーザーID" ||
                string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                MessageBox.Show(
                    "ユーザーIDを入力してください",
                    "確認",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUserId.Focus();
                return;
            }

            if (txtPassword.Text == "パスワード" ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "パスワードを入力してください",
                    "確認",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }

            bool result =
                UserRepository.Login(
                    txtUserId.Text.Trim(),
                    txtPassword.Text);

            if (result)
            {
                GlobalVariable.LoginId =
                    txtUserId.Text.Trim();

                MessageBox.Show(
                    "ログイン成功",
                    "情報",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                mainmenu frm = new mainmenu();
                frm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show(
                    "ユーザーIDまたはパスワードが違います",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserId.Clear();
            txtPassword.Clear();

            txtUserId.Focus();
        }
    }
}
