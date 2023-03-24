using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Legal_OfficeMaster : System.Web.UI.Page
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
                //FillOfficetypeName();
                FillOfficeLevel();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }

    protected void FillOfficeLevel()
    {
        try
        {
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

            ds = obj.ByProcedure("USP_Select_OfficeMaster", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdOfficeMaster.DataSource = ds;
                GrdOfficeMaster.DataBind();
                GrdOfficeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdOfficeMaster.UseAccessibleHeader = true;
            }
            else
            {
                GrdOfficeMaster.DataSource = null;
                GrdOfficeMaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    //protected void FillOfficetypeName()
    //{
    //    try
    //    {
    //        ddlOfficeType.Items.Clear();
    //        ds = obj.ByDataSet("select OfficeType_Id,OfficeType_Name from tblOfficeTypeMaster");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlOfficeType.DataTextField = "OfficeType_Name";
    //            ddlOfficeType.DataValueField = "OfficeType_Id";
    //            ddlOfficeType.DataSource = ds;
    //            ddlOfficeType.DataBind();
    //        }
    //        ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogCls.SendErrorToText(ex);
    //        //lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
    //    }
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_OfficeMaster", new string[] { "OfficeType_Id", "OfficeLevel_Id", "OfficeName", "Officelocation", "CreatedBy", "CreatedByIP" }
                        , new string[] { ddlOfficeType.SelectedValue, ddlOfficeLevel.SelectedValue, txtOfficeName.Text.Trim(), txtOfficelocation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["OfficeID"].ToString() != "" && ViewState["OfficeID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_OfficeMaster", new string[] { "OfficeLevel_Id", "OfficeType_Id", "OfficeName", "Officelocation", "LastupdatedBy", "LastupdatedByIP", "Office_Id" }
                        , new string[] { ddlOfficeLevel.SelectedValue, ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOfficelocation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["OfficeID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtOfficeName.Text = "";
                        txtOfficelocation.Text = "";
                        ddlOfficeLevel.ClearSelection();
                        ddlOfficeType.ClearSelection();
                        FillGrid();
                        btnSave.Text = "Save";
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-warning", "Warning !", ErrMsg);

                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void GrdOfficeMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdOfficeMaster.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void GrdOfficeMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                ViewState["OfficeID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblofficelevel = (Label)row.FindControl("lblofficelevel_ID");
                Label lblOfficeTypeID = (Label)row.FindControl("lblOfficeTypeID");
                Label lblOficeName = (Label)row.FindControl("lblOficeName");
                Label lblOficelocation = (Label)row.FindControl("lblOficelocation");

                btnSave.Text = "Update";
                ViewState["OfficeID"] = e.CommandArgument;
                GrdOfficeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdOfficeMaster.UseAccessibleHeader = true;
                if (lblofficelevel.Text != "")
                {
                    ddlOfficeLevel.ClearSelection();
                    ddlOfficeLevel.Items.FindByValue(lblofficelevel.Text).Selected = true;
                }

                if (lblOficeName.Text != "")
                    txtOfficeName.Text = lblOficeName.Text;
                if (lblOficelocation.Text != "")
                    txtOfficelocation.Text = lblOficelocation.Text;
                if (lblOfficeTypeID.Text != "")
                {
                    ddlOfficeLevel_SelectedIndexChanged(sender, e);
                    ddlOfficeType.ClearSelection();
                    ddlOfficeType.Items.FindByValue(lblOfficeTypeID.Text).Selected = true;
                }
            }
            if (e.CommandName == "DeleteDetails")
            {
                int Office_Id = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from tblOfficeMaster where Office_Id=" + Office_Id);
                FillGrid();
            }

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void ddlOfficeLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeType.Items.Clear();
            ds = obj.ByProcedure("USP_Select_OfficeTypeName", new string[] { "OfficeLevel_Id" }
                , new string[] { ddlOfficeLevel.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeType.DataValueField = "OfficeType_Id";
                ddlOfficeType.DataTextField = "OfficeType_Name";
                ddlOfficeType.DataSource = ds;
                ddlOfficeType.DataBind();
            }
            ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void lnkbtndelete_Click(object sender, EventArgs e, GridViewCommandEventArgs er)
    {
        if (er.CommandName == "DeleteDetails")
        {
            int Office_Id = Convert.ToInt32(er.CommandArgument);
            obj.ByTextQuery("delete from tblOfficeMaster where Office_Id=" + Office_Id);
            FillGrid();
        }
    }
    protected void lnkbtndelete_Click1(object sender, EventArgs e)
    {

    }
}