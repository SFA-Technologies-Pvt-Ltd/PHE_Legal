﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class Legal_SubjectWiseCaseDtl : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetCaseSubject();
                GetCaseType();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    #region Get Case Subject
    private void GetCaseSubject()
    {
        try
        {
            ds = obj.ByDataSet("select * from tbl_LegalMstCaseSubject");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubject.DataSource = ds.Tables[0];
                ddlCaseSubject.DataTextField = "CaseSubject";
                ddlCaseSubject.DataValueField = "CaseSubjectID";
                ddlCaseSubject.DataBind();
                ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseSubject.DataSource = null;
                ddlCaseSubject.DataBind();
                ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region Get Case type
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
    #region Bind Grid
    protected void BindGrid()
    {
        try
        {
            string OIC = "";
            if (Session["OICMaster_ID"] != "" && Session["OICMaster_ID"] != null) OIC = Session["OICMaster_ID"].ToString();
            ds = obj.ByProcedure("USP_Legal_CaseRpt", new string[] { "flag", "Casetype_ID", "CaseSubject_Id", "OICMaster_Id" },
                    new string[] { "1", ddlCaseType.SelectedItem.Value, ddlCaseSubject.SelectedItem.Value,OIC }, "dataset");
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
    #endregion
    #region Btn Search
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
                string ID = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
                string page_ID = HttpUtility.UrlEncode(Encrypt("3"));
                string CaseID = HttpUtility.UrlEncode(Encrypt("CaseID"));
                string pageID = HttpUtility.UrlEncode(Encrypt("pageID"));
                Response.Redirect("~/Legal/ViewWPPendingCaseDetail.aspx?" + CaseID + "=" + ID + "&" + pageID + "=" + page_ID, false);
            }
            if (grdSubjectWiseCasedtl.Rows.Count > 0)
            {
                grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSubjectWiseCasedtl.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Btn Clear
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/legal/subjectwisecasedtl.aspx");
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