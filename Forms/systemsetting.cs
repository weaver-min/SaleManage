
using SaleManage.DataBase;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace SaleManage.Forms
{
    public partial class systemsetting : Form
    {
        private bool _isEditMode = false;

        private string _originalCompanyName;
        private string _originalAddress;
        private string _originalPhone;
        private string _originalBank;
        private string _originalBankNo;
        private string _originalId;
        private string _originalPassword;
        private string _originalTax;
        public systemsetting()
        {
            InitializeComponent();
            LoadSettings();
            SetViewMode();
        }
        private void LoadSettings()
        {
            SystemRepo repo = new SystemRepo();

            // ← company info
            DataTable dt = repo.GetSettings();
            if (dt.Rows.Count > 0)
            {
                txtCompanyName.Text = dt.Rows[0]["company_name"].ToString();
                txtAddress.Text = dt.Rows[0]["company_address"].ToString();
                txtPhone.Text = dt.Rows[0]["company_phone"].ToString();
                txtBank.Text = dt.Rows[0]["company_bank"].ToString();
                txtBankNo.Text = dt.Rows[0]["company_AccountData"].ToString();
                txtTax.Text = dt.Rows[0]["tax"].ToString();
            }

            // ← user info
            DataTable userDt = repo.GetUserSettings();
            if (userDt.Rows.Count > 0)
            {
                txtId.Text = userDt.Rows[0]["login_id"].ToString();
                txtPassword.Text = userDt.Rows[0]["login_password"].ToString();
            }

            // ← save original values
            _originalCompanyName = txtCompanyName.Text;
            _originalAddress = txtAddress.Text;
            _originalPhone = txtPhone.Text;
            _originalBank = txtBank.Text;
            _originalBankNo = txtBankNo.Text;
            _originalId = txtId.Text;
            _originalPassword = txtPassword.Text;
            _originalTax = txtTax.Text;
        }

        private void SetViewMode()
        {
            _isEditMode = false;
            btnEdit.Text = "編集";
            btnClose.Text = "閉じる";

            txtCompanyName.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtBank.ReadOnly = true;
            txtBankNo.ReadOnly = true;
            txtId.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtTax.ReadOnly = true;
            txtPassword.PasswordChar = '*';
        }

        private void SetEditMode()
        {
            _isEditMode = true;
            btnEdit.Text = "完了";
            btnClose.Text = "キャンセル";

            txtCompanyName.ReadOnly = false;
            txtAddress.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtBank.ReadOnly = false;
            txtBankNo.ReadOnly = false;
            txtId.ReadOnly = false;
            txtPassword.ReadOnly = false;
            txtTax.ReadOnly = false;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("会社名を入力してください。");
                txtCompanyName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("住所を入力してください。");
                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("電話番号を入力してください。");
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBank.Text))
            {
                MessageBox.Show("取引銀行を入力してください。");
                txtBank.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBankNo.Text))
            {
                MessageBox.Show("口座情報を入力してください。");
                txtBankNo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("IDを入力してください。");
                txtId.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtId.Text, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("IDはアルファベット、数字のみ入力してください。");
                txtId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("パスワードを入力してください。");
                txtPassword.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtPassword.Text, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("パスワードはアルファベット、数字のみ入力してください。");
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTax.Text))
            {
                MessageBox.Show("消費税を入力してください。");
                txtTax.Focus();
                return false;
            }

            if (!int.TryParse(txtTax.Text, out _))
            {
                MessageBox.Show("消費税は数値のみ入力してください。");
                txtTax.Focus();
                return false;
            }

            return true;
        }
   
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                SetEditMode();
                return;
            }

            if (!ValidateInput())
                return;

            SystemRepo repo = new SystemRepo();

            // ← update company info
            repo.UpdateSettings(
                txtCompanyName.Text.Trim(),
                txtAddress.Text.Trim(),
                txtPhone.Text.Trim(),
                txtBank.Text.Trim(),
                txtBankNo.Text.Trim(),
                int.Parse(txtTax.Text.Trim()));

            // ← update user info
            repo.UpdateUserSettings(
                txtId.Text.Trim(),
                txtPassword.Text.Trim());

            MessageBox.Show(
                "更新が完了しました。",
                "完了",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _originalCompanyName = txtCompanyName.Text;
            _originalAddress = txtAddress.Text;
            _originalPhone = txtPhone.Text;
            _originalBank = txtBank.Text;
            _originalBankNo = txtBankNo.Text;
            _originalId = txtId.Text;
            _originalPassword = txtPassword.Text;
            _originalTax = txtTax.Text;

            SetViewMode();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                txtCompanyName.Text = _originalCompanyName;
                txtAddress.Text = _originalAddress;
                txtPhone.Text = _originalPhone;
                txtBank.Text = _originalBank;
                txtBankNo.Text = _originalBankNo;
                txtId.Text = _originalId;
                txtPassword.Text = _originalPassword;
                txtTax.Text = _originalTax;
                SetViewMode();
            }
            else
            {
                this.Close();
            }
        }

        private void txtTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
       !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
    }

