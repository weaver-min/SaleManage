using Microsoft.Reporting.WinForms;
using SaleManage.DataBase;
using System;
using System.Data;
using System.Windows.Forms;

namespace SaleManage
{
    public partial class invoice1 : Form
    {
        private string _customerId;
        private DateTime _issueDate;
        private DateTime _billingMonth;
        private DateTime _paymentDeadline;

        public invoice1() : this("", DateTime.Today, DateTime.Today, DateTime.Today) { }

        public invoice1(string customerId, DateTime issueDate,
                DateTime billingMonth, DateTime paymentDeadline)
        {
            InitializeComponent();
            _customerId = customerId;
            _issueDate = issueDate;
            _billingMonth = billingMonth;
            _paymentDeadline = paymentDeadline;

            if (string.IsNullOrEmpty(_customerId))
                return;

            CustomerRepo customerRepo = new CustomerRepo();
            DataTable customerDt = customerRepo.GetCustomerById(_customerId);

            if (customerDt.Rows.Count == 0)
                return;

            SalesRepo salesRepo = new SalesRepo();
            DataTable salesDt = salesRepo.GetSalesByCustomerAndMonth(
                _customerId, _billingMonth);

            SystemRepo sysRepo = new SystemRepo();
            string companyInfo =
                (sysRepo.GetSetting("company_name") ?? "") + "\n" +
                (sysRepo.GetSetting("company_address") ?? "") + "\n" +
                (sysRepo.GetSetting("company_phone") ?? "");
            string bankInfo =
                (sysRepo.GetSetting("company_bank") ?? "") + "\n" +
                (sysRepo.GetSetting("company_AccountData") ?? "");
            string taxRate = sysRepo.GetSetting("tax") ?? "0";


            int subTotal = CalculateTotal(salesDt);
            int tax = string.IsNullOrEmpty(taxRate) ? 0 : (int)(subTotal * int.Parse(taxRate) / 100.0);
            int totalAmount = subTotal + tax;

            reportViewer1.LocalReport.ReportEmbeddedResource = "SaleManage.Forms.invoice1.rdlc";
            reportViewer1.LocalReport.Refresh();

            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
               {
        new ReportParameter("CustomerName",    customerDt.Rows[0]["customer_name"].ToString() ?? ""),
        new ReportParameter("IssueDate",       issueDate.ToShortDateString()),
        new ReportParameter("CompanyInfo",     companyInfo ?? ""),
        new ReportParameter("TotalAmount", totalAmount.ToString() + "（税込）"),
        new ReportParameter("PaymentDeadline", paymentDeadline.ToShortDateString()),
        new ReportParameter("BankInfo",        bankInfo ?? ""),
        new ReportParameter("TaxRate",         taxRate ?? "0"),
               });


            ReportDataSource rds = new ReportDataSource("InvoiceDataset", salesDt);
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }

        private int CalculateTotal(DataTable dt)
        {
            int total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += int.Parse(row["amount"].ToString());
            }
            return total;
        }
    }
}