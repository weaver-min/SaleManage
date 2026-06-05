
namespace SaleManage
{
    partial class mainmenu
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSystem = new System.Windows.Forms.Button();
            this.btnSaleRegist = new System.Windows.Forms.Button();
            this.btnGoods = new System.Windows.Forms.Button();
            this.btnInvoice = new System.Windows.Forms.Button();
            this.lblmain_menu = new System.Windows.Forms.Label();
            this.btnCustomerMaster = new System.Windows.Forms.Button();
            this.btnSalelst = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1087, 701);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 360F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 377F));
            this.tableLayoutPanel2.Controls.Add(this.btnSystem, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnSaleRegist, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnGoods, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnInvoice, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblmain_menu, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCustomerMaster, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSalelst, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1081, 695);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnSystem
            // 
            this.btnSystem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSystem.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSystem.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSystem.Location = new System.Drawing.Point(768, 483);
            this.btnSystem.Name = "btnSystem";
            this.btnSystem.Size = new System.Drawing.Size(249, 161);
            this.btnSystem.TabIndex = 6;
            this.btnSystem.Text = "システム設定";
            this.btnSystem.UseVisualStyleBackColor = true;
            // 
            // btnSaleRegist
            // 
            this.btnSaleRegist.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaleRegist.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSaleRegist.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaleRegist.Location = new System.Drawing.Point(411, 483);
            this.btnSaleRegist.Name = "btnSaleRegist";
            this.btnSaleRegist.Size = new System.Drawing.Size(226, 161);
            this.btnSaleRegist.TabIndex = 5;
            this.btnSaleRegist.Text = "販売登録";
            this.btnSaleRegist.UseVisualStyleBackColor = true;
            // 
            // btnGoods
            // 
            this.btnGoods.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGoods.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnGoods.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGoods.Location = new System.Drawing.Point(59, 483);
            this.btnGoods.Name = "btnGoods";
            this.btnGoods.Size = new System.Drawing.Size(226, 161);
            this.btnGoods.TabIndex = 4;
            this.btnGoods.Text = "商品マスタ";
            this.btnGoods.UseVisualStyleBackColor = true;
            // 
            // btnInvoice
            // 
            this.btnInvoice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnInvoice.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnInvoice.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInvoice.Location = new System.Drawing.Point(763, 222);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new System.Drawing.Size(258, 161);
            this.btnInvoice.TabIndex = 3;
            this.btnInvoice.Text = "請求書";
            this.btnInvoice.UseVisualStyleBackColor = true;
            // 
            // lblmain_menu
            // 
            this.lblmain_menu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblmain_menu.AutoSize = true;
            this.lblmain_menu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel2.SetColumnSpan(this.lblmain_menu, 3);
            this.lblmain_menu.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblmain_menu.Location = new System.Drawing.Point(3, 0);
            this.lblmain_menu.Name = "lblmain_menu";
            this.lblmain_menu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblmain_menu.Size = new System.Drawing.Size(1075, 173);
            this.lblmain_menu.TabIndex = 0;
            this.lblmain_menu.Text = "販売管理システム";
            this.lblmain_menu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblmain_menu.UseMnemonic = false;
            // 
            // btnCustomerMaster
            // 
            this.btnCustomerMaster.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCustomerMaster.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCustomerMaster.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCustomerMaster.Location = new System.Drawing.Point(59, 222);
            this.btnCustomerMaster.Name = "btnCustomerMaster";
            this.btnCustomerMaster.Size = new System.Drawing.Size(226, 161);
            this.btnCustomerMaster.TabIndex = 1;
            this.btnCustomerMaster.Text = "顧客マスタ";
            this.btnCustomerMaster.UseVisualStyleBackColor = true;
            // 
            // btnSalelst
            // 
            this.btnSalelst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSalelst.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSalelst.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalelst.Location = new System.Drawing.Point(411, 222);
            this.btnSalelst.Name = "btnSalelst";
            this.btnSalelst.Size = new System.Drawing.Size(226, 161);
            this.btnSalelst.TabIndex = 2;
            this.btnSalelst.Text = "販売一覧";
            this.btnSalelst.UseVisualStyleBackColor = true;
            // 
            // mainmenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 701);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "mainmenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mainmenu";
            this.Load += new System.EventHandler(this.mainmenu_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        internal System.Windows.Forms.Button btnSystem;
        internal System.Windows.Forms.Button btnSaleRegist;
        internal System.Windows.Forms.Button btnGoods;
        internal System.Windows.Forms.Button btnInvoice;
        internal System.Windows.Forms.Label lblmain_menu;
        internal System.Windows.Forms.Button btnCustomerMaster;
        internal System.Windows.Forms.Button btnSalelst;
    }
}