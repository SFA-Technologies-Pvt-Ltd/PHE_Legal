using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_DisposeCaseRpt : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetCaseDisposeType();
                GetCaseType();
            }
        }
        else
        {
            Response.Redirect("/Login.aspx");
        }
    }
    #region Get Case Dispose Type
    private void GetCaseDisposeType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select * from tbl_LegalCaseDisposeType ");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDisposetype.DataSource = ds.Tables[0];
                ddlDisposetype.DataTextField = "CaseDisposeType";
                ddlDisposetype.DataValueField = "CaseDisposeType_Id";
                ddlDisposetype.DataBind();
                ddlDisposetype.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDisposetype.DataSource = null;
                ddlDisposetype.DataBind();
                ddlDisposetype.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region Get Case Type
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
    #endregion
    #region Bing Grid
    protected void BindGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Legal_CaseRpt", new string[] { "flag", "Casetype_ID", "CaseDisposeType_Id" }, new string[] { "9", ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdSubjectWiseCasedtl.DataSource = ds;
                grdSubjectWiseCasedtl.DataBind();
            }
            else
            {
                grdSubjectWiseCasedtl.DataSource = null;
                grdSubjectWiseCasedtl.DataBind();
            }
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
    #endregion
    #region Row Command
    protected void grdSubjectWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "ViewDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string ID = e.CommandArgument.ToString();
                string pageID = "4";
                Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?CaseID=" + Server.UrlEncode(ID) + "&pageID=" + pageID, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Page Index Changing
    protected void grdSubjectWiseCasedtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdSubjectWiseCasedtl.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region ddlDisposetype Selected Index Changed
    protected void ddlDisposetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlDisposetype.SelectedIndex == 2)
            {
                ddlCompliaceSt.ClearSelection();
                ComplianceSt_Div.Visible = true;
            }
            else { ComplianceSt_Div.Visible = false; }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
}