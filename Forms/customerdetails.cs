using SaleManage.DataBase;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SaleManage
{
    public partial class customerdetails : Form
    {
        public string CustomerID { get; set; }
        private bool _isEditMode = false;

        private string _originalName;
        private string _originalFurigana;
        private string _originalAddress;
        public customerdetails(string customerId = "")
        {
            InitializeComponent();
            CustomerID = customerId;

            txtCustomerId.ReadOnly = true;

            if (!string.IsNullOrEmpty(CustomerID))
            {
                LoadCustomerData();
                LoadSalesHistory();
                SetViewMode();
            }
            else
            {
                txtCustomerId.Text = "自動採番";
                SetNewMode();
            }
        }

        private void LoadCustomerData()
        {
            CustomerRepo repo = new CustomerRepo();
            DataTable dt = repo.GetCustomerById(CustomerID);

            if (dt.Rows.Count > 0)
            {
                txtCustomerId.Text = dt.Rows[0]["customer_id"].ToString();
                txtCustomerName.Text = dt.Rows[0]["customer_name"].ToString();
                txtFurigana.Text = dt.Rows[0]["customer_furigana"].ToString();
                txtAddress.Text = dt.Rows[0]["customer_address"].ToString();

                _originalName = dt.Rows[0]["customer_name"].ToString();
                _originalFurigana = dt.Rows[0]["customer_furigana"].ToString();
                _originalAddress = dt.Rows[0]["customer_address"].ToString();
            }
        }

        private void LoadSalesHistory()
        {
            CustomerRepo repo = new CustomerRepo();
            DataTable dt = repo.GetSalesByCustomerId(CustomerID);

            dgvCustomerDetails.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvCustomerDetails.Rows.Add(
                    row["sales_id"],
                    row["purchase_date"],
                    row["goods_name"],
                    row["goods_price"],
                    row["units_sold"],
                    row["amount"],
                    row["remarks"]
                );
            }
        }

        private void SetViewMode()
        {
            _isEditMode = false;
            btnEdit.Text = "編集";
            btnClose.Text = "閉じる";
            btnDelete.Visible = true;

            txtCustomerName.ReadOnly = true;
            txtFurigana.ReadOnly = true;
            txtAddress.ReadOnly = true;
        }

        private void SetEditMode()
        {
            _isEditMode = true;
            btnEdit.Text = "完了";
            btnClose.Text = "キャンセル";
            btnDelete.Visible = true;

            txtCustomerName.ReadOnly = false;
            txtFurigana.ReadOnly = false;
            txtAddress.ReadOnly = false;
        }

        private void SetNewMode()
        {
            _isEditMode = true;
            btnEdit.Text = "登録";
            btnClose.Text = "閉じる";
            btnDelete.Visible = false;

            txtCustomerName.ReadOnly = false;
            txtFurigana.ReadOnly = false;
            txtAddress.ReadOnly = false;
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
            if (!_isEditMode && !string.IsNullOrEmpty(CustomerID))
            {
                SetEditMode();
                return;
            }

            if (!ValidateInput())
                return;

            CustomerRepo repo = new CustomerRepo();

            if (string.IsNullOrEmpty(CustomerID)) // ← INSERT
            {
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
            else // ← UPDATE
            {
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

                // ← check if sales exist for this customer
                CustomerRepo repo = new CustomerRepo();
                DataTable salesCheck = repo.GetSalesByCustomerId(CustomerID);

                if (salesCheck.Rows.Count > 0)
                {
                    MessageBox.Show(
                        "この顧客には販売履歴があるため削除できません。",
                        "削除不可",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "この顧客を削除しますか？",
                    "確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    repo.DeleteCustomer(CustomerID);
                    MessageBox.Show("削除が完了しました。", "完了",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

            private void btnClose_Click(object sender, EventArgs e)
        {
            if (_isEditMode && !string.IsNullOrEmpty(CustomerID))
            {
                // ← restore original values
                txtCustomerName.Text = _originalName;
                txtFurigana.Text = _originalFurigana;
                txtAddress.Text = _originalAddress;
                SetViewMode();
            }
            else
            {
                this.Close();
            }
        }
    }
    }
  


