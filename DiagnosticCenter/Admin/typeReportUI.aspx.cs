﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenter.BLL;
using DiagnosticCenter.Models;

namespace DiagnosticCenter.Admin
{
    public partial class typeReportUI : System.Web.UI.Page
    {
        ReportManager reportManager = new ReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] != null)
            {
                //Response.Write("Welcome to AdminPanel");
            }
            else
            {
                Response.Redirect("../Index.aspx");
            }
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("../Index.aspx");
        }

        protected void testReportButton_Click(object sender, EventArgs e)
        {
            if (fromDateTextBox.Value == string.Empty || toDateTextBox.Value == string.Empty)
            {
                messageLabel.Text = "Please select both date.";
                return;
            }

            DateTime fromDate = Convert.ToDateTime(fromDateTextBox.Value);
            DateTime toDate = Convert.ToDateTime(toDateTextBox.Value);

            List<Report> ReportList = reportManager.GetTypeWiseReport(fromDate, toDate);

            if (ReportList.Count != 0)
            {
                decimal superTotal = ReportList.Sum(item => item.TotalFee);
                superTotalValue.Text = superTotal.ToString();

                superTotalLabel.Text = "Total";

                typeReportGridView.Visible = true;
                typeReportGridView.DataSource = ReportList;
                typeReportGridView.DataBind();
                messageLabel.Visible = false;
            }
            else
            {
                typeReportGridView.Visible = true;
                messageLabel.Text = "Sorry, No data found.";
            }
        }
    }
}