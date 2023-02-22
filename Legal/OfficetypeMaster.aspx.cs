using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_OfficetypeMaster : System.Web.UI.Page
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
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillGrid();
                FillOfficeLevel();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx",false);
        }
    }

    protected void FillOfficeLevel()
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeLevel.Items.Clear();
            ds = obj.ByDataSet("select OfficeLevel_Id, OfficeLevelName from tblOfficeLevelMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeLevel.DataTextField = "OfficeLevelName";
                ddlOfficeLevel.DataValueField = "OfficeLevel_Id";
                ddlOfficeLevel.DataSource = ds;
                ddlOfficeLevel.DataBind();
            }
            ddlOfficeLevel.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_OfficetypeMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdOfficetypeMst.DataSource = ds;
                grdOfficetypeMst.DataBind();
            }
            else
            {
                grdOfficetypeMst.DataSource = null;
                grdOfficetypeMst.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_InsertOfficetypeMaster", new string[] { "OfficeLevel_Id", "OfficeType_Name", "CreatedBy", "CreatedByIP" }
                    , new string[] { ddlOfficeLevel.SelectedValue, txtOfficeTypeName.Text.Trim(),  ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress()}, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["OfficeTypeID"].ToString() != "" && ViewState["OfficeTypeID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_UpdateOfficetypeMaster", new string[] { "OfficeLevel_Id", "OfficeType_Name", "LastupdatedBy", "LastupdatedByIP", "OfficeType_Id" }
                    , new string[] {ddlOfficeLevel.SelectedValue , txtOfficeTypeName.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["OfficeTypeID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        ddlOfficeLevel.ClearSelection();
                        txtOfficeTypeName.Text = "";
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Warning !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
                FillGrid();
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void grdOfficetypeMst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdOfficetypeMst.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void grdOfficetypeMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                lblMsg.Text = "";
                ViewState["OfficeTypeID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblOfficetypeName = (Label)row.FindControl("lblOfficetypeName");
                Label lblOfficetypeID = (Label)row.FindControl("lblOfficetypeID");
                Label lblOfficelevel = (Label)row.FindControl("lblOfficelevelID");
                txtOfficeTypeName.Text = lblOfficetypeName.Text;
                if (lblOfficelevel.Text != "")
                {
                    ddlOfficeLevel.ClearSelection();
                    ddlOfficeLevel.Items.FindByValue(lblOfficelevel.Text).Selected = true;
                }
                ViewState["OfficeTypeID"] = e.CommandArgument;
                btnSave.Text = "Update";
            }
            if (e.CommandName == "DeleteDetails")
            {
                int OfficeType_Id = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from tblOfficeTypeMaster where OfficeType_Id=" + OfficeType_Id);
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}