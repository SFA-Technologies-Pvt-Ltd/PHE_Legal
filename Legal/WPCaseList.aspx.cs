﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_WPCaseList : System.Web.UI.Page
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
                FillCasetype();
                FillCaseNo();
                FillCourt();
                FillYear();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void FillCourt()
    {
        try
        {
            ddlCourt.Items.Clear();
            Helper court = new Helper();
            DataTable dtCourt = court.GetCourt() as DataTable;
            if (dtCourt != null && dtCourt.Rows.Count > 0)
            {
                ddlCourt.DataValueField = "CourtType_ID";
                ddlCourt.DataTextField = "CourtTypeName";
                ddlCourt.DataSource = dtCourt;
                ddlCourt.DataBind();
            }
            ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void FillYear()
    {
        try
        {
            ddlCaseYear.Items.Clear();
            DataSet dsCase = obj.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlCaseYear.DataSource = dsCase.Tables[0];
                ddlCaseYear.DataTextField = "year";
                ddlCaseYear.DataValueField = "year";
                ddlCaseYear.DataBind();
            }
            ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void FillCaseNo()
    {
        try
        {
            ddlCaseNo.Items.Clear();
            Helper CaseNo = new Helper();
            DataTable dtCN = CaseNo.GetCaseNo() as DataTable;
            if (dtCN != null && dtCN.Rows.Count > 0)
            {
                ddlCaseNo.DataValueField = "Case_ID";
                ddlCaseNo.DataTextField = "CaseNo";
                ddlCaseNo.DataSource = dtCN;
                ddlCaseNo.DataBind();
            }
            ddlCaseNo.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void FillCasetype()
    {
        try
        {
            Helper HP = new Helper();
            DataTable dt = HP.GetCasetype() as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCaseType.DataValueField = "Casetype_ID";
                ddlCaseType.DataTextField = "Casetype_Name";
                ddlCaseType.DataSource = dt;
                ddlCaseType.DataBind();
            }
            ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
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

            lblMsg.Text = "";
            GrdCaseDetails.DataSource = null;
            GrdCaseDetails.DataBind();
            if (txtFromDate.Text != "" && txtEndDate.Text != "")
            {
                ds = obj.ByProcedure("USP_GetCaseRegisDetail", new string[] { "FromDate", "EndDate", "Casetype_ID", "CourtID", "CaseNo", "Year" }
                    , new string[] { Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), 
                        Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy/MM/dd"), ddlCaseType.SelectedValue,ddlCourt.SelectedValue,ddlCaseNo.SelectedItem.Text,ddlCaseYear.SelectedItem.Text }, "dataset");

            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdCaseDetails.DataSource = ds;
                GrdCaseDetails.DataBind();
                GrdCaseDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdCaseDetails.UseAccessibleHeader = true;
            }
            else
            {
                GrdCaseDetails.DataSource = null;
                GrdCaseDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrdCaseDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblUniqueNo = (Label)row.FindControl("lblUniqueNo");
            string UniqueNO = lblUniqueNo.Text;
            string ID = e.CommandArgument.ToString();
            Response.Redirect("../Legal/EditCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&UniqueNO=" + UniqueNO, false);
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    //protected void GrdCaseDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        GrdCaseDetails.PageIndex = e.NewPageIndex;
    //        btnSearch_Click(sender,  e);
    //        GrdCaseDetails.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
    //    }
    //}
}