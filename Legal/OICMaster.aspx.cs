﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Legal_Oicmaster : System.Web.UI.Page
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
                FillZone();
                FillHo();
                FillDesignation();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx",false);
        }
    }
    #region Fill Grid
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_OICMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gridoicmaster.DataSource = ds;
                gridoicmaster.DataBind();
                gridoicmaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridoicmaster.UseAccessibleHeader = true;
            }
            else
            {
                gridoicmaster.DataSource = null;
                gridoicmaster.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Ho
    protected void FillHo()
    {
        try
        {
            lblMsg.Text = "";
            ddlHoName.Items.Clear();
            ds = obj.ByDataSet("select Ho_Id,HoName from tblHoMaster");
            if (ds !=null && ds.Tables[0].Rows.Count > 0)
            {
                ddlHoName.DataTextField = "HoName";
                ddlHoName.DataValueField = "Ho_Id";
                ddlHoName.DataSource = ds;
                ddlHoName.DataBind();
            }
            ddlHoName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill Zone
    protected void FillZone()
    {
        try
        {
            lblMsg.Text = "";
            ddlzone.Items.Clear();
            ds = obj.ByProcedure("USP_SelectZoneForCircleMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlzone.DataTextField = "ZoneName";
                ddlzone.DataValueField = "Zone_ID";
                ddlzone.DataSource = ds;
                ddlzone.DataBind();
            }
            ddlzone.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Button Insert_update
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_OICMaster", new string[] { "Ho_Id", "Zone_ID", "Circle_ID", "Division_ID", "OICName", "Designation_ID", "OICMobileNo", "OICEmailID", "Office_ID", "CreatedBy", "CreatedByIP" }
                    , new string[] { ddlHoName.SelectedValue,ddlzone.SelectedValue, ddlcircle.SelectedValue, ddldivision.SelectedValue, txtoicnme.Text.Trim(), ddlDesignation.SelectedValue, txtmobileno.Text.Trim(),txtEmailID.Text.Trim(), ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["OICID"].ToString() != "" && ViewState["OICID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_OICMaster", new string[] { "Ho_Id", "Zone_ID", "Circle_ID", "Division_ID", "OICName", "Designation_ID", "OICMobileNo", "OICEmailID", "Office_ID", "LastupdatedBy", "LastupdatedByIP", "OICMaster_ID" }
                    , new string[] { ddlHoName.SelectedValue,ddlzone.SelectedValue, ddlcircle.SelectedValue, ddldivision.SelectedValue, txtoicnme.Text.Trim(), ddlDesignation.SelectedValue,txtmobileno.Text.Trim(),txtEmailID.Text.Trim(), ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["OICID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        ddlHoName.ClearSelection();
                        ddlzone.ClearSelection();
                        ddlcircle.ClearSelection();
                        ddldivision.ClearSelection();
                        ddlDesignation.ClearSelection();
                        txtEmailID.Text = "";
                        txtoicnme.Text = "";
                        txtmobileno.Text = "";
                        ViewState["OICID"] = "";
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
                ddlHoName.ClearSelection();
                ddlzone.ClearSelection();
                ddlcircle.ClearSelection();
                ddldivision.ClearSelection();
                txtoicnme.Text = "";
                txtmobileno.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Designation
    protected void FillDesignation()
    {
        try
        {
            ddlDesignation.Items.Clear();
            ds = obj.ByDataSet("select Designation_Id, Designation_Name from tblDesignationMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesignation.DataValueField = "Designation_Id";
                ddlDesignation.DataTextField = "Designation_Name";
                ddlDesignation.DataSource = ds;
                ddlDesignation.DataBind();
            }
            ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    protected void gridoicmaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gridoicmaster.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void gridoicmaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDetails")
            {
                ViewState["OICID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblHoID = (Label)row.FindControl("lblHoID");
                Label lblZoneID = (Label)row.FindControl("lblZoneID");
                Label lblCircleID = (Label)row.FindControl("lblCircleID");
                Label lblDivisionID = (Label)row.FindControl("lblDivisionID");
                Label lblOICName = (Label)row.FindControl("lblOICName");
                Label lblMobileNo = (Label)row.FindControl("lblMobileNo");
                Label lblDesignationId = (Label)row.FindControl("lblDesignationId");
                Label lblEmailID = (Label)row.FindControl("lblEmailID");

                ViewState["OICID"] = e.CommandArgument;

                gridoicmaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridoicmaster.UseAccessibleHeader = true;

                btnSave.Text = "Update";
                txtoicnme.Text = lblOICName.Text;
                txtEmailID.Text = lblEmailID.Text;
                txtmobileno.Text = lblMobileNo.Text;
                ddlHoName.ClearSelection();
                ddlHoName.Items.FindByValue(lblHoID.Text).Selected = true;
                ddlzone.ClearSelection();
                ddlzone.Items.FindByValue(lblZoneID.Text).Selected = true;
                ddlzone_SelectedIndexChanged(sender, e);
                ddlcircle.ClearSelection();
                ddlcircle.Items.FindByValue(lblCircleID.Text).Selected = true;
                ddlcircle_SelectedIndexChanged(sender, e);
                ddldivision.ClearSelection();
                ddldivision.Items.FindByValue(lblDivisionID.Text).Selected = true;
                ddlDesignation.ClearSelection();
                ddlDesignation.Items.FindByValue(lblDesignationId.Text).Selected = true;
                
            }
            if (e.CommandName == "DeleteDetails")
            {
                    int OICMaster_ID = Convert.ToInt32(e.CommandArgument);
                    obj.ByTextQuery("delete from tblOICMaster where OICMaster_ID=" + OICMaster_ID);
                    FillGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
          //  lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlzone_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlcircle.Items.Clear();
            ds = obj.ByProcedure("USP_Select_CircleMaster", new string[] { "flag", "Zone_ID" }
                    , new string[] { "2", ddlzone.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlcircle.DataTextField = "CirlceName";
                ddlcircle.DataValueField = "Circle_ID";
                ddlcircle.DataSource = ds;
                ddlcircle.DataBind();
            }
            ddlcircle.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlcircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddldivision.Items.Clear();
            ds = obj.ByProcedure("USP_SelectDivisionByCircle", new string[] { "Circle_ID" }
                    , new string[] { ddlcircle.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddldivision.DataTextField = "DivisionName";
                ddldivision.DataValueField = "Division_ID";
                ddldivision.DataSource = ds;
                ddldivision.DataBind();
            }
            ddldivision.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}