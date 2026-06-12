
namespace SaleManage.Forms
{
    partial class systemsetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBankNo = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtBankNo = new System.Windows.Forms.TextBox();
            this.txtBank = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserId = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(990, 610);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(984, 91);
            this.label1.TabIndex = 0;
            this.label1.Text = "システム設定";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBankNo);
            this.panel1.Controls.Add(this.lblBank);
            this.panel1.Controls.Add(this.lblPhone);
            this.panel1.Controls.Add(this.lblAddress);
            this.panel1.Controls.Add(this.txtBankNo);
            this.panel1.Controls.Add(this.txtBank);
            this.panel1.Controls.Add(this.txtPhone);
            this.panel1.Controls.Add(this.txtAddress);
            this.panel1.Controls.Add(this.txtCompanyName);
            this.panel1.Controls.Add(this.lblCompanyName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 94);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(489, 513);
            this.panel1.TabIndex = 1;
            // 
            // lblBankNo
            // 
            this.lblBankNo.AutoSize = true;
            this.lblBankNo.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBankNo.Location = new System.Drawing.Point(7, 381);
            this.lblBankNo.Name = "lblBankNo";
            this.lblBankNo.Size = new System.Drawing.Size(118, 24);
            this.lblBankNo.TabIndex = 14;
            this.lblBankNo.Text = "口座情報：";
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBank.Location = new System.Drawing.Point(7, 295);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(118, 24);
            this.lblBank.TabIndex = 13;
            this.lblBank.Text = "取引銀行：";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPhone.Location = new System.Drawing.Point(7, 221);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(118, 24);
            this.lblPhone.TabIndex = 12;
            this.lblPhone.Text = "電話番号：";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAddress.Location = new System.Drawing.Point(50, 157);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(70, 24);
            this.lblAddress.TabIndex = 11;
            this.lblAddress.Text = "住所：";
            // 
            // txtBankNo
            // 
            this.txtBankNo.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBankNo.Location = new System.Drawing.Point(132, 380);
            this.txtBankNo.Name = "txtBankNo";
            this.txtBankNo.Size = new System.Drawing.Size(280, 30);
            this.txtBankNo.TabIndex = 10;
            // 
            // txtBank
            // 
            this.txtBank.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBank.Location = new System.Drawing.Point(132, 291);
            this.txtBank.Name = "txtBank";
            this.txtBank.Size = new System.Drawing.Size(280, 30);
            this.txtBank.TabIndex = 9;
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtPhone.Location = new System.Drawing.Point(132, 217);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(280, 30);
            this.txtPhone.TabIndex = 8;
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtAddress.Location = new System.Drawing.Point(132, 153);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(280, 35);
            this.txtAddress.TabIndex = 7;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCompanyName.Location = new System.Drawing.Point(132, 65);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(280, 30);
            this.txtCompanyName.TabIndex = 6;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCompanyName.Location = new System.Drawing.Point(26, 69);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(94, 24);
            this.lblCompanyName.TabIndex = 1;
            this.lblCompanyName.Text = "会社名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(26, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "〇自社情報";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblPassword);
            this.panel2.Controls.Add(this.lblUserId);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.txtTax);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.txtId);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(498, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(489, 513);
            this.panel2.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPassword.Location = new System.Drawing.Point(17, 153);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(55, 24);
            this.lblPassword.TabIndex = 17;
            this.lblPassword.Text = "PW：";
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUserId.Location = new System.Drawing.Point(17, 71);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(44, 24);
            this.lblUserId.TabIndex = 15;
            this.lblUserId.Text = "ID：";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(134, 381);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 62);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.LimeGreen;
            this.btnEdit.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEdit.Location = new System.Drawing.Point(284, 381);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(130, 62);
            this.btnEdit.TabIndex = 15;
            this.btnEdit.Text = "編集";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(184, 292);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 28);
            this.label12.TabIndex = 14;
            this.label12.Text = "%";
            // 
            // txtTax
            // 
            this.txtTax.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTax.Location = new System.Drawing.Point(41, 291);
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(128, 30);
            this.txtTax.TabIndex = 13;
            this.txtTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTax_KeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtPassword.Location = new System.Drawing.Point(134, 149);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(280, 30);
            this.txtPassword.TabIndex = 12;
            // 
            // txtId
            // 
            this.txtId.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtId.Location = new System.Drawing.Point(134, 68);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(280, 30);
            this.txtId.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(3, 249);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 24);
            this.label11.TabIndex = 8;
            this.label11.Text = "〇消費税";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(3, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 24);
            this.label8.TabIndex = 6;
            this.label8.Text = "〇ユーザー情報";
            // 
            // systemsetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 610);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "systemsetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "system";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBankNo;
        private System.Windows.Forms.TextBox txtBank;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.Label lblBankNo;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserId;
    }
}