﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class Legal_ViewWPPendingCaseDetail : System.Web.UI.Page
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
                string multiCharString = Request.QueryString.ToString();
                string[] multiArray = multiCharString.Split(new Char[] { '=', '&' });
                string CaseID = Decrypt(HttpUtility.UrlDecode(multiArray[1]));
                string PageID = Decrypt(HttpUtility.UrlDecode(multiArray[3]));
                ViewState["Page"] = PageID.ToString();
                if (!string.IsNullOrEmpty(CaseID))
                {
                    ViewState["CaseID"] = CaseID;
                    BindCaseDetail();
                    if (PageID == "2" || PageID == "4")
                    {
                        dvOrderSummary.Visible = true;
                        dvCaseDisposalType.Visible = true;
                        Compilance_Div.Visible = true;
                    }
                }
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }
    #region Bind Case Detail
    protected void BindCaseDetail()
    {
        try
        {
            // lblMsg.Text = "";

            GrdResponderDtl.DataSource = null;
            GrdResponderDtl.DataBind();
            GrdHearingDtl.DataSource = null;
            GrdHearingDtl.DataBind();

            ds = obj.ByProcedure("USP_ViewWPPendingCaseFullDtlRpt", new string[] { "Case_ID" }
                , new string[] { ViewState["CaseID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CaseNo"].ToString() != "") lblCaseNo.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString(); else lblCaseNo.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseYear"].ToString() != "") txtCaseYear.Text = ds.Tables[0].Rows[0]["CaseYear"].ToString(); else txtCaseYear.Text = "NA";
                if (ds.Tables[0].Rows[0]["CourtTypeName"].ToString() != "") txtCourtType.Text = ds.Tables[0].Rows[0]["CourtTypeName"].ToString(); else txtCourtType.Text = "NA";
                if (ds.Tables[0].Rows[0]["District_Name"].ToString() != "") txtCourtLocation.Text = ds.Tables[0].Rows[0]["District_Name"].ToString(); else txtCourtLocation.Text = "NA";
                if (ds.Tables[0].Rows[0]["PartyName"].ToString() != "") txtParty.Text = ds.Tables[0].Rows[0]["PartyName"].ToString(); else txtParty.Text = "NA";
                if (ds.Tables[0].Rows[0]["Casetype_Name"].ToString() != "") txtCasetype.Text = ds.Tables[0].Rows[0]["Casetype_Name"].ToString(); else txtCasetype.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseSubject"].ToString() != "") txtCaseSubject.Text = ds.Tables[0].Rows[0]["CaseSubject"].ToString(); else txtCaseSubject.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseSubSubject"].ToString() != "") txtCaseSubSubject.Text = ds.Tables[0].Rows[0]["CaseSubSubject"].ToString(); else txtCaseSubSubject.Text = "NA";
                if (ds.Tables[0].Rows[0]["OICNAME"].ToString() != "") txtOicName.Text = ds.Tables[0].Rows[0]["OICNAME"].ToString(); else txtOicName.Text = "NA";
                if (ds.Tables[0].Rows[0]["OICMobileNO"].ToString() != "") txtOicMobileNo.Text = ds.Tables[0].Rows[0]["OICMobileNO"].ToString(); else txtOicMobileNo.Text = "NA";
                if (ds.Tables[0].Rows[0]["OICEmailID"].ToString() != "") txtOicEmailId.Text = ds.Tables[0].Rows[0]["OICEmailID"].ToString(); else txtOicEmailId.Text = "NA";
                if (ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString() != "") txtHighprioritycase.Text = ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString(); else txtHighprioritycase.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseDetail"].ToString() != "") txtCaseDetail.Text = ds.Tables[0].Rows[0]["CaseDetail"].ToString(); else txtCaseDetail.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseStatus"].ToString() != "") txtCaseStatus.Text = ds.Tables[0].Rows[0]["CaseStatus"].ToString(); else txtCaseStatus.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseDisposeType"].ToString() != "") txtcasedisposaltype.Text = ds.Tables[0].Rows[0]["CaseDisposeType"].ToString(); else txtcasedisposaltype.Text = "NA";
                if (ds.Tables[0].Rows[0]["OrderSummary"].ToString() != "") txtOrderSummary.Text = ds.Tables[0].Rows[0]["OrderSummary"].ToString(); else txtOrderSummary.Text = "NA";
                if (ds.Tables[0].Rows[0]["Compliance_Status"].ToString() != "") txtComplianceStatus.Text = ds.Tables[0].Rows[0]["Compliance_Status"].ToString(); else txtComplianceStatus.Text = "NA";

                if (ds.Tables[1].Rows.Count > 0) GrdPetitioner.DataSource = ds.Tables[1]; GrdPetitioner.DataBind();
                if (ds.Tables[2].Rows.Count > 0) GrdPetiAdv.DataSource = ds.Tables[2]; GrdPetiAdv.DataBind();
                if (ds.Tables[3].Rows.Count > 0) GrdDeptAdv.DataSource = ds.Tables[3]; GrdDeptAdv.DataBind();
                if (ds.Tables[4].Rows.Count > 0) GrdResponderDtl.DataSource = ds.Tables[4]; GrdResponderDtl.DataBind();
                if (ds.Tables[5].Rows[0]["CaseDoc_ID"].ToString() != "") GrdDocument.DataSource = ds.Tables[5]; GrdDocument.DataBind();
                if (ds.Tables[6].Rows[0]["NextHearing_ID"].ToString() != "") GrdHearingDtl.DataSource = ds.Tables[6]; GrdHearingDtl.DataBind();
            }
            else
            {
                GrdResponderDtl.DataSource = null;
                GrdResponderDtl.DataBind();
                GrdHearingDtl.DataSource = null;
                GrdHearingDtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Row Command
    protected void GrdDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Link = (HyperLink)e.Row.FindControl("hyperLink");
                HyperLink DocWithFolderPath = (HyperLink)e.Row.FindControl("lnkDocPath");
                Label lbldoc = (Label)e.Row.FindControl("lbldoc");

                string name = lbldoc.Text;
                name.StartsWith("https");
                if (name.StartsWith("https") == true)
                    Link.Visible = true;
                else DocWithFolderPath.Visible = true;

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Btn Back
    protected void lbkBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Page"].ToString() == "1") Response.Redirect("../Legal/pendingwpreport.aspx", false); //Pendig Rpt
            if (ViewState["Page"].ToString() == "2") Response.Redirect("../Legal/ConcludedwpReport.aspx", false);//Concolude Rpt
            if (ViewState["Page"].ToString() == "3") Response.Redirect("../Legal/SubjectWiseCaseDtl.aspx", false);// SubjectWise Case Rpt
            if (ViewState["Page"].ToString() == "4") Response.Redirect("../Legal/disposecaserpt.aspx", false);// Disposal Case Rpt
            if (ViewState["Page"].ToString() == "5") Response.Redirect("../Legal/respondentwisecaserpt.aspx", false);// Respondent Case Rpt
            if (ViewState["Page"].ToString() == "6") Response.Redirect("../Legal/WeekelyHearingCaseRpt.aspx", false);// Weekely Hearing Case Rpt
            if (ViewState["Page"].ToString() == "7") Response.Redirect("../Legal/LongPendingCaseRpt.aspx", false);// Long Pendinh Case Rpt
            if (ViewState["Page"].ToString() == "8") Response.Redirect("../Legal/monthlyhearingdtl.aspx", false);// Monthly Hearing Case Rpt
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}