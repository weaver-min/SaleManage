using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleManage
{
    public partial class mainmenu : Form
    {
        public mainmenu()
        {
            InitializeComponent();
        }

        private void btnCustomerMaster_Click(object sender, EventArgs e)
        {
            customermain frm = new customermain();
            frm.ShowDialog();
        }

        private void btnSalelst_Click(object sender, EventArgs e)
        {
            sale_list frm = new sale_list();
            frm.ShowDialog();
           }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            invoice inv = new invoice();
            inv.ShowDialog();
        }

        private void btnGoods_Click(object sender, EventArgs e)
        {
            productmain frm = new productmain();
            frm.ShowDialog();
        }

        private void btnSaleRegist_Click(object sender, EventArgs e)
        {
            sale_register frm = new sale_register();
            frm.ShowDialog();
        }

        private void btnSystem_Click(object sender, EventArgs e)
        {
            Forms.systemsetting sym = new Forms.systemsetting();
            sym.ShowDialog();
        }
    }
}
