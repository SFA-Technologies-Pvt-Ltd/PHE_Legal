﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            if (!Page.IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillCasetype();

                FillCourt();
                FillYear();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }
    #region Bind_CourtName
    protected void FillCourt()
    {
        try
        {
            ddlCourt.Items.Clear();
            Helper court = new Helper();
            DataTable dtCourt = new DataTable();
            dtCourt = court.GetCourt() as DataTable;
            ddlCourt.Enabled = true;
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
    #endregion
    #region Bind_Year
    protected void FillYear()
    {
        try
        {
            ddlCaseYear.Items.Clear();
            DataSet dsCase = obj.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year desc");
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
    #endregion
    #region Bind_CaseNo
    protected void FillCaseNo()
    {
        try
        {
            ddlCaseNo.Items.Clear();
            DataTable dtCN = new DataTable();
            Helper CaseNo = new Helper();
            if (!string.IsNullOrEmpty(Session["OICMaster_ID"].ToString()))
            {
                dtCN = CaseNo.GetOICWiseCaseNo(Session["OICMaster_ID"].ToString()) as DataTable;
            }
            else
            {
                string CourtType_Id = ddlCourt.SelectedValue;
                dtCN = CaseNo.GetCaseNoByCourt(CourtType_Id) as DataTable;

            }
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
    #endregion
    #region Bind_Casetype
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
    #endregion
    #region SearchButton
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdCaseDetails.DataSource = null;
            GrdCaseDetails.DataBind();
            string OICMaster_Id = "";
            if (!string.IsNullOrEmpty(Session["OICMaster_ID"].ToString()))
            {
                OICMaster_Id = Session["OICMaster_ID"].ToString();
                ds = obj.ByProcedure("USP_GetCaseRegisDetail", new string[] { "Casetype_ID", "CourtType_Id", "CaseNo", "Year", "CaseStatus", "OICMaster_Id", "flag" }
                   , new string[] { ddlCaseType.SelectedValue, ddlCourt.SelectedValue, ddlCaseNo.SelectedItem.Text, ddlCaseYear.SelectedItem.Text, ddlCaseStatus.SelectedItem.Text, OICMaster_Id, "1" }, "dataset");
            }
            else
            {
                ds = obj.ByProcedure("USP_GetCaseRegisDetail", new string[] { "Casetype_ID", "CourtType_Id", "CaseNo", "Year", "CaseStatus", "flag" }
                  , new string[] { ddlCaseType.SelectedValue, ddlCourt.SelectedValue, ddlCaseNo.SelectedItem.Text, ddlCaseYear.SelectedItem.Text, ddlCaseStatus.SelectedItem.Text, "0" }, "dataset");
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdCaseDetails.DataSource = ds;
                GrdCaseDetails.DataBind();
                GrdCaseDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdCaseDetails.UseAccessibleHeader = true;
            }
            else { GrdCaseDetails.DataSource = null; GrdCaseDetails.DataBind(); }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
        finally { ds.Clear(); }
    }
    #endregion
    #region RowCommand
    protected void GrdCaseDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblUniqueNo = (Label)row.FindControl("lblUniqueNo");
            Label lblStatus = (Label)row.FindControl("lblStatus");
            string UniqueNO = lblUniqueNo.Text;
            string ID = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
            string Uniqueno = HttpUtility.UrlEncode(Encrypt(lblUniqueNo.Text));
            string CaseID = HttpUtility.UrlEncode(Encrypt("CaseID"));
            string pageID = HttpUtility.UrlEncode(Encrypt("pageID"));
            //Response.Redirect("../Legal/EditCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&UniqueNO=" + UniqueNO, false);
            Response.Redirect("~/Legal/EditCaseDetail.aspx?" + CaseID + "=" + ID + "&" + pageID + "=" + Uniqueno, false);
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region DropdownSelectted Event
    protected void ddlCourt_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCaseNo();
    }
    #endregion
    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
}