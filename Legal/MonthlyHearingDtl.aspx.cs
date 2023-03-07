using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_MonthlyHearingDtl : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetCaseType();
                FillYear();
            }
        }
        else
        {
            Response.Redirect("/Login.aspx", false);
        }
    }

    #region Fill Year
    protected void FillYear()
    {
        ddlYear.Items.Clear();
        for (int i = 1950; i <= DateTime.Now.Year; i++)
        {
            ddlYear.Items.Add(i.ToString());
        }
        ddlYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    #endregion


    private void GetCaseType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select * from tbl_Legal_Casetype");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseType.DataSource = ds.Tables[0];
                ddlCaseType.DataTextField = "Casetype_Name";
                ddlCaseType.DataValueField = "Casetype_ID";
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseType.DataSource = null;
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void BindGrid()
    {
        try
        {
            ds = new DataSet();
            grdMonthlyHearingdtl.DataSource = null;
            grdMonthlyHearingdtl.DataBind();
            string OICID = Session["OICMaster_ID"] != null ? Session["OICMaster_ID"].ToString() : null;
            ds = obj.ByProcedure("USP_Legal_CaseRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "OICMaster_Id" },
                new string[] { "6", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, OICID }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdMonthlyHearingdtl.DataSource = ds;
                grdMonthlyHearingdtl.DataBind();
                grdMonthlyHearingdtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdMonthlyHearingdtl.UseAccessibleHeader = true;
            }
            else
            {
                grdMonthlyHearingdtl.DataSource = null;
                grdMonthlyHearingdtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            if (Page.IsValid)
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdMonthlyHearingdtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        if (e.CommandName == "ViewDtl")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?CaseID=" + e.CommandArgument.ToString() + "&pageID=" + 8, false);

        }
    }
    protected void grdMonthlyHearingdtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdMonthlyHearingdtl.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}