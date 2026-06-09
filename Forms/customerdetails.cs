using SaleManage.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SaleManage
{
    public partial class customerdetails : Form
    { 

        public customerdetails()
        {
            InitializeComponent();
        }
          
            public string CustomerID { get; set; }
        private void customerdetails_Load(object sender, EventArgs e)
        {
            txtCustomerName.ReadOnly = false;
            txtFurigana.ReadOnly = false;
            txtAddress.ReadOnly = false;
            txtCustomerId.ReadOnly = true;
            dgvCustomerDetails.AllowUserToAddRows = false;
            dgvCustomerDetails.AllowUserToDeleteRows = false;
            if (!string.IsNullOrEmpty(CustomerID))
            {
                CustomerRepo repo = new CustomerRepo();

                DataTable dt =
                    repo.GetCustomerById(CustomerID);

                if (dt.Rows.Count > 0)
                {
                    txtCustomerId.Text =
                        dt.Rows[0]["customer_id"].ToString();

                    txtCustomerName.Text =
                        dt.Rows[0]["customer_name"].ToString();

                    txtFurigana.Text =
                        dt.Rows[0]["customer_furigana"].ToString();

                    txtAddress.Text =
                        dt.Rows[0]["customer_address"].ToString();
                }
            }
            else
            {
                btnDelete.Enabled = false;
            }
        
    }
            
        
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("顧客名を入力してください。");
                txtCustomerName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFurigana.Text))
            {
                MessageBox.Show("フリガナを入力してください。");
                txtFurigana.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("住所を入力してください。");
                txtAddress.Focus();
                return false;
            }

            return true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
                if (!ValidateInput())
                    return;

                CustomerRepo repo = new CustomerRepo();

                if (string.IsNullOrEmpty(CustomerID))
                {
                    // New Registration
                    repo.InsertCustomer(
                        txtCustomerName.Text.Trim(),
                        txtFurigana.Text.Trim(),
                        txtAddress.Text.Trim());

                    MessageBox.Show(
                        "登録が完了しました。",
                        "完了",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // Update
                    repo.UpdateCustomer(
                        CustomerID,
                        txtCustomerName.Text.Trim(),
                        txtFurigana.Text.Trim(),
                        txtAddress.Text.Trim());

                    MessageBox.Show(
                        "更新が完了しました。",
                        "完了",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                if (string.IsNullOrEmpty(CustomerID))
                    return;

                DialogResult result =
                    MessageBox.Show(
                        "この顧客を削除しますか？",
                        "確認",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    CustomerRepo repo = new CustomerRepo();

                    repo.DeleteCustomer(CustomerID);

                    MessageBox.Show(
                        "削除が完了しました。",
                        "完了",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

        private void btnClose_Click(object sender, EventArgs e)
        {
                this.Close();
            }
    }
    }
  


