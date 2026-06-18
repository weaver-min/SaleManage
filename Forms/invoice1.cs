using Microsoft.Reporting.WinForms;
using SaleManage.DataBase;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace SaleManage
{
    public partial class invoice1 : Form
    {
        private readonly DataTable _salesDt;
        private readonly DateTime _issueDate;
        private readonly DateTime _billingMonth;
        private readonly DateTime _paymentDeadline;

        private const string RdlcResource = "SaleManage.Forms.invoice1.rdlc";
        private const string DatasetName = "InvoiceDataset";
        private const int LinesPerPage = 10;

        public invoice1(
            DataTable salesDt,
            DateTime issueDate,
            DateTime billingMonth,
            DateTime paymentDeadline)
        {
            InitializeComponent();
            _salesDt = salesDt ?? throw new ArgumentNullException(nameof(salesDt));
            _issueDate = issueDate;
            _billingMonth = billingMonth;
            _paymentDeadline = paymentDeadline;
        }

        private void invoice1_Load(object sender, EventArgs e)
        {
            try
            {
                SystemSettings settings = LoadSystemSettings();
                DataTable dt = BuildPagedInvoiceTable(_salesDt, settings);
                BindReport(dt, settings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "請求書プレビューの表示に失敗しました。\n\n" + ex.Message,
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
        }

        private SystemSettings LoadSystemSettings()
        {
            SystemRepo repo = new SystemRepo();

            string taxRateStr = repo.GetSetting("tax") ?? "0";
            int taxRate = int.TryParse(taxRateStr, out int t) ? t : 0;

            return new SystemSettings
            {
                CompanyName = repo.GetSetting("company_name") ?? string.Empty,
                CompanyAddress = repo.GetSetting("company_address") ?? string.Empty,
                CompanyPhone = repo.GetSetting("company_phone") ?? string.Empty,
                BankName = repo.GetSetting("company_bank") ?? string.Empty,
                BankAccount = repo.GetSetting("company_AccountData") ?? string.Empty,
                TaxRate = taxRate,
                TaxRateStr = taxRateStr
            };
        }

        private DataTable BuildPagedInvoiceTable(DataTable salesDt, SystemSettings settings)
        {
            DataTable result = CreateInvoiceTable();

            var customers = salesDt.AsEnumerable()
                .OrderBy(r => Convert.ToString(r["customer_id"]))
                .ThenBy(r => ToDateTime(r["purchase_date"]))
                .ThenBy(r => Convert.ToString(r["sales_id"]))
                .GroupBy(r => new
                {
                    Id = Convert.ToString(r["customer_id"]),
                    Name = Convert.ToString(r["customer_name"]),
                    Address = salesDt.Columns.Contains("customer_address")
                        ? Convert.ToString(r["customer_address"])
                        : string.Empty
                })
                .ToList();

            int totalPages = customers.Sum(g => Math.Max(1, (int)Math.Ceiling(g.Count() / (double)LinesPerPage)));
            int reportPage = 1;

            foreach (var customer in customers)
            {
                var rows = customer.ToList();
                int customerPages = Math.Max(1, (int)Math.Ceiling(rows.Count / (double)LinesPerPage));
                int customerSubtotal = rows.Sum(r => ToInt(r["amount"]));
                int customerTotalWithTax = ApplyTax(customerSubtotal, settings.TaxRate);

                for (int pageIndex = 0; pageIndex < customerPages; pageIndex++)
                {
                    var pageRows = rows.Skip(pageIndex * LinesPerPage).Take(LinesPerPage).ToList();
                    int pageSubtotal = pageRows.Sum(r => ToInt(r["amount"]));
                    int pageStart = pageIndex * LinesPerPage + 1;
                    int pageEnd = pageStart + pageRows.Count - 1;
                    bool isLastCustomerPage = pageIndex == customerPages - 1;

                    for (int i = 0; i < pageRows.Count; i++)
                    {
                        DataRow source = pageRows[i];
                        DataRow row = result.NewRow();
                        row["page_key"] = reportPage.ToString(CultureInfo.InvariantCulture);
                        row["page_number"] = reportPage;
                        row["total_pages"] = totalPages;
                        row["customer_page_number"] = pageIndex + 1;
                        row["customer_total_pages"] = customerPages;
                        row["is_last_customer_page"] = isLastCustomerPage;
                        row["customer_id"] = customer.Key.Id;
                        row["customer_name"] = customer.Key.Name;
                        row["customer_address"] = customer.Key.Address;
                        row["company_name"] = settings.CompanyName;
                        row["company_address"] = settings.CompanyAddress;
                        row["company_phone"] = settings.CompanyPhone;
                        row["bank_name"] = settings.BankName;
                        row["bank_account"] = settings.BankAccount;
                        row["tax_rate"] = settings.TaxRate;
                        row["line_no"] = pageStart + i;
                        row["sales_id"] = Convert.ToString(source["sales_id"]);
                        row["purchase_date"] = FormatShortDate(source["purchase_date"]);
                        row["goods_name"] = Convert.ToString(source["goods_name"]);
                        row["goods_price"] = ToInt(source["goods_price"]);
                        row["units_sold"] = ToInt(source["units_sold"]);
                        row["amount"] = ToInt(source["amount"]);
                        row["page_subtotal"] = pageSubtotal;
                        row["customer_subtotal"] = customerSubtotal;
                        row["customer_total_with_tax"] = customerTotalWithTax;
                        row["total_order_count"] = rows.Count;
                        row["page_start_index"] = pageStart;
                        row["page_end_index"] = pageEnd;
                        row["continued_text"] = isLastCustomerPage
                            ? string.Empty
                            : "→ 次のページに続く";
                        row["invoice_title"] = pageIndex == 0 ? "請 求 書" : "請 求 書（続き）";
                        result.Rows.Add(row);
                    }

                    reportPage++;
                }
            }

            return result;
        }

        private static DataTable CreateInvoiceTable()
        {
            DataTable table = new DataTable("InvoiceTable");
            table.Columns.Add("page_key", typeof(string));
            table.Columns.Add("page_number", typeof(int));
            table.Columns.Add("total_pages", typeof(int));
            table.Columns.Add("customer_page_number", typeof(int));
            table.Columns.Add("customer_total_pages", typeof(int));
            table.Columns.Add("is_last_customer_page", typeof(bool));
            table.Columns.Add("customer_id", typeof(string));
            table.Columns.Add("customer_name", typeof(string));
            table.Columns.Add("customer_address", typeof(string));
            table.Columns.Add("company_name", typeof(string));
            table.Columns.Add("company_address", typeof(string));
            table.Columns.Add("company_phone", typeof(string));
            table.Columns.Add("bank_name", typeof(string));
            table.Columns.Add("bank_account", typeof(string));
            table.Columns.Add("tax_rate", typeof(int));
            table.Columns.Add("line_no", typeof(int));
            table.Columns.Add("sales_id", typeof(string));
            table.Columns.Add("purchase_date", typeof(string));
            table.Columns.Add("goods_name", typeof(string));
            table.Columns.Add("goods_price", typeof(int));
            table.Columns.Add("units_sold", typeof(int));
            table.Columns.Add("amount", typeof(int));
            table.Columns.Add("page_subtotal", typeof(int));
            table.Columns.Add("customer_subtotal", typeof(int));
            table.Columns.Add("customer_total_with_tax", typeof(int));
            table.Columns.Add("total_order_count", typeof(int));
            table.Columns.Add("page_start_index", typeof(int));
            table.Columns.Add("page_end_index", typeof(int));
            table.Columns.Add("continued_text", typeof(string));
            table.Columns.Add("invoice_title", typeof(string));
            return table;
        }

        private void BindReport(DataTable dt, SystemSettings settings)
        {
            if (reportViewer1 == null)
                throw new InvalidOperationException("ReportViewer が初期化されていません。");

            reportViewer1.LocalReport.ReportEmbeddedResource = RdlcResource;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource(DatasetName, dt));

            reportViewer1.LocalReport.SetParameters(new[]
            {
                new ReportParameter("IssueDate", _issueDate.ToString("yyyy年M月d日")),
                new ReportParameter("BillingMonth", _billingMonth.ToString("yyyy年M月")),
                new ReportParameter("PaymentDeadline", _paymentDeadline.ToString("yyyy年M月d日")),
                new ReportParameter("TaxRate", settings.TaxRateStr),
            });

            reportViewer1.RefreshReport();
        }

        private static int ToInt(object value)
        {
            return value == DBNull.Value ? 0 : Convert.ToInt32(value);
        }

        private static DateTime ToDateTime(object value)
        {
            return value == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(value);
        }

        private static string FormatShortDate(object value)
        {
            DateTime date = ToDateTime(value);
            return date == DateTime.MinValue ? string.Empty : date.ToString("MM/dd");
        }

        private static int ApplyTax(int subtotal, int taxRate)
        {
            return (int)Math.Round(subtotal * (1 + taxRate / 100m), MidpointRounding.AwayFromZero);
        }

        private class SystemSettings
        {
            public string CompanyName { get; set; }
            public string CompanyAddress { get; set; }
            public string CompanyPhone { get; set; }
            public string BankName { get; set; }
            public string BankAccount { get; set; }
            public int TaxRate { get; set; }
            public string TaxRateStr { get; set; }
        }
    }
}
