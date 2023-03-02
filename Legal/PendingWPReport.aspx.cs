using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Legal_PendingWPReport : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                FillCasetype();
                FillYear();
            }
        }
        else
        {
            Response.Redirect("~/Legal/Login.aspx", false);
        }
    }
    #region Fill Year
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        for (int i = 1950; i <= DateTime.Now.Year; i++)
        {
            ddlCaseYear.Items.Add(i.ToString());
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    #endregion
    #region Fill Case type
    protected void FillCasetype()
    {
        try
        {
            Helper hl = new Helper();
            DataTable dt = hl.GetCasetype() as DataTable;
            if (dt.Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = dt;
                ddlCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Btn Search
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                GrdPendingReport.DataSource = null;
                GrdPendingReport.DataBind();

                ds = obj.ByProcedure("USP_GetWPPendingRpt", new string[] { "CaseYear", "Casetype_ID" }
                    , new string[] { ddlCaseYear.SelectedValue, ddlCasetype.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GrdPendingReport.DataSource = ds;
                    GrdPendingReport.DataBind();
                    GrdPendingReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GrdPendingReport.UseAccessibleHeader = true;
                }
                else
                {
                    GrdPendingReport.DataSource = null;
                    GrdPendingReport.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
        finally { ds.Clear(); }
    }
    #endregion
    #region Row Command
    protected void GrdPendingReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;               
                string ID = e.CommandArgument.ToString();
                string pageID = "1";
                Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?CaseID=" + Server.UrlEncode(ID) + "&pageID=" + pageID, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
}