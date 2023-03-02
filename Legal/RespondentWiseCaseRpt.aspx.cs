using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_RespondentWiseCaseRpt : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetOfficetype();
                GetCaseType();
            }
        }
        else
        {
            Response.Redirect("/Login.aspx", false);
        }
    }
    private void GetOfficetype()
    {
        try
        {

            ds = obj.ByDataSet("select OfficeType_Id, OfficeType_Name from tblOfficeTypeMaster");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlofficetype.DataSource = ds.Tables[0];
                ddlofficetype.DataTextField = "OfficeType_Name";
                ddlofficetype.DataValueField = "OfficeType_Id";
                ddlofficetype.DataBind();
                ddlofficetype.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlofficetype.DataSource = null;
                ddlofficetype.DataBind();
                ddlofficetype.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
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
            ds = obj.ByProcedure("USP_Legal_CaseRpt", new string[] { "flag", "Casetype_ID", "OfficeType_Id" },
                new string[] { "3", ddlCaseType.SelectedItem.Value, ddlofficetype.SelectedItem.Value }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {

                grdSubjectWiseCasedtl.DataSource = ds;
                grdSubjectWiseCasedtl.DataBind();
                grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSubjectWiseCasedtl.UseAccessibleHeader = true;
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
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
    protected void grdSubjectWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "ViewDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSubjectWiseCasedtl.UseAccessibleHeader = true;
                Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?ID=" + e.CommandArgument.ToString() + "&PageID=" + 5, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }

}
