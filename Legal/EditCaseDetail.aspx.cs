﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public partial class Legal_EditCaseDetail : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != "" && Session["Office_Id"] != "")
        {
            if (!IsPostBack)
            {
                string multiCharString = Request.QueryString.ToString();
                string[] multiArray = multiCharString.Split(new Char[] { '=', '&' });
                string CaseID = Decrypt(HttpUtility.UrlDecode(multiArray[1]));
                string UniqueNO = Decrypt(HttpUtility.UrlDecode(multiArray[3]));
                ViewState["ID"] = CaseID;
                ViewState["UniqueNO"] = UniqueNO;
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillOldCaseYear();
                FillYear();
                BindCourtName();
                BindDisposalType();
                FillParty();
                FillCaseSubject();
                FillOicName();
                FillDesignation();
                BindOfficeType();
                CaseDisposeStatus(); // by deafult Case Dispose on NO text.
                FillCasetype();
                BindDetails(sender, e);
                //By Default Set No for OldCase.
                OldCase_BydefaultNO();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPAGETOKEN"] = Session["PAGETOKEN"];
    }
    protected void OldCase_BydefaultNO()
    {
        ddlOldCaseAsk.ClearSelection();
        ddlOldCaseAsk.Items.FindByText("No").Selected = true;
        DivOldCase.Visible = false;

    }
    #region Fill CaseDispose Status
    protected void CaseDisposeStatus() // Case Dispose By Default On NO condtiton
    {
        foreach (ListItem item in rdCaseDispose.Items)
        {
            if (item.Text.Contains("No"))
            {
                item.Selected = true;
                break;

            }
            caseDisposeYes.Visible = false;
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
            HearingDtl_CaseDispose.Visible = false;
        }
    }
    #endregion
    #region Fill Case DisposeType
    protected void BindDisposalType()
    {
        try
        {
            ddlDisponsType.Items.Clear();
            ds = obj.ByDataSet("select CaseDisposeType_Id, CaseDisposeType from tbl_LegalCaseDisposeType");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDisponsType.DataTextField = "CaseDisposeType";
                ddlDisponsType.DataValueField = "CaseDisposeType_Id";
                ddlDisponsType.DataSource = ds;
                ddlDisponsType.DataBind();
            }
            ddlDisponsType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill OfficeType
    protected void BindOfficeType()
    {
        try
        {
            ddlResOfficetypeName.Items.Clear();
            ds = obj.ByProcedure("USP_Select_Officetype", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlResOfficetypeName.DataTextField = "OfficeType_Name";
                ddlResOfficetypeName.DataValueField = "OfficeType_Id";
                ddlResOfficetypeName.DataSource = ds;
                ddlResOfficetypeName.DataBind();
            }
            ddlResOfficetypeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region Fill OICName
    protected void FillOicName()
    {
        try
        {
            Helper oic = new Helper();
            ddlOicName.Items.Clear();
            DataTable dtOic = oic.GetOIC() as DataTable;
            if (dtOic != null && dtOic.Rows.Count > 0)
            {
                ddlOicName.DataTextField = "OICName";
                ddlOicName.DataValueField = "OICMaster_ID";
                ddlOicName.DataSource = dtOic;
                ddlOicName.DataBind();
            }
            ddlOicName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
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

    protected void FillOldCaseYear()
    {
        try
        {
            ddloldCaseYear.Items.Clear();
            DataSet dsCase = obj.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddloldCaseYear.DataSource = dsCase.Tables[0];
                ddloldCaseYear.DataTextField = "year";
                ddloldCaseYear.DataValueField = "year";
                ddloldCaseYear.DataBind();
                ddloldCaseYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddloldCaseYear.DataSource = null;
                ddloldCaseYear.DataBind();
                ddloldCaseYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill OICName
    protected void BiunOicName()
    {
        try
        {
            Helper oic = new Helper();
            ddlOicName.Items.Clear();
            DataTable dtOic = oic.GetOIC() as DataTable;
            if (dtOic != null && dtOic.Rows.Count > 0)
            {
                ddlOicName.DataTextField = "OICName";
                ddlOicName.DataValueField = "OICMaster_ID";
                ddlOicName.DataSource = dtOic;
                ddlOicName.DataBind();
            }
            ddlOicName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CourtName
    protected void BindCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear(); ddloldCaseCourt.Items.Clear();
            DataSet dsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "dataset");
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataTextField = "CourtTypeName";
                ddlCourtType.DataValueField = "CourtType_ID";
                ddlCourtType.DataSource = dsCourt;
                ddlCourtType.DataBind();

                ddloldCaseCourt.DataTextField = "CourtTypeName";
                ddloldCaseCourt.DataValueField = "CourtType_ID";
                ddloldCaseCourt.DataSource = dsCourt;
                ddloldCaseCourt.DataBind();
            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
            ddloldCaseCourt.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CaseSubject
    protected void FillCaseSubject()
    {
        try
        {
            ddlCaseSubject.Items.Clear();
            DataSet ds_SC = obj.ByDataSet("SELECT CaseSubject, CaseSubjectID FROM tbl_LegalMstCaseSubject");
            if (ds_SC != null && ds_SC.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubject.DataTextField = "CaseSubject";
                ddlCaseSubject.DataValueField = "CaseSubjectID";
                ddlCaseSubject.DataSource = ds_SC;
                ddlCaseSubject.DataBind();
            }
            ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region FillParty
    protected void FillParty()
    {
        try
        {
            ddlParty.Items.Clear();
            Helper hlp = new Helper();
            DataTable dtParty = hlp.GetPartyName() as DataTable;
            if (dtParty != null && dtParty.Rows.Count > 0)
            {
                ddlParty.DataValueField = "Party_ID";
                ddlParty.DataTextField = "PartyName";
                ddlParty.DataSource = dtParty;
                ddlParty.DataBind();
            }
            ddlParty.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion
    #region Fill Designation
    protected void FillDesignation()
    {
        try
        {

            ddlPetiDesigNation.Items.Clear(); ddlResDesig.Items.Clear();
            ds = obj.ByDataSet("select Designation_Id, Designation_Name from tblDesignationMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPetiDesigNation.DataTextField = "Designation_Name";
                ddlPetiDesigNation.DataValueField = "Designation_Id";
                ddlPetiDesigNation.DataSource = ds;
                ddlPetiDesigNation.DataBind();

                ddlResDesig.DataTextField = "Designation_Name";
                ddlResDesig.DataValueField = "Designation_Id";
                ddlResDesig.DataSource = ds;
                ddlResDesig.DataBind();
            }
            ddlResDesig.Items.Insert(0, new ListItem("Select", "0"));
            ddlPetiDesigNation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region CaseType
    protected void FillCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ddloldCasetype.Items.Clear();
            ds = obj.ByDataSet("select Casetype_ID, Casetype_Name from  tbl_Legal_Casetype");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = ds;
                ddlCasetype.DataBind();
                // For old Case type
                ddloldCasetype.DataTextField = "Casetype_Name";
                ddloldCasetype.DataValueField = "Casetype_ID";
                ddloldCasetype.DataSource = ds;
                ddloldCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
            ddloldCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion

    #region Bind MainDtl
    protected void BindDetails(object sender, EventArgs e)
    {
        try
        {
            ViewState["SubjectID"] = "";
            ds = obj.ByProcedure("USP_Select_NewCaseRegis", new string[] { "Case_ID" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblCaseNo.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString();
                if (ds.Tables[0].Rows[0]["CaseDetail"].ToString() != "")txtCaseDetail.Text = ds.Tables[0].Rows[0]["CaseDetail"].ToString();
                else txtCaseDetail.Text = "Not Available";

                ddlCourtType.ClearSelection();
                if (ddlCourtType.Items.Count > 0)
                    ddlCourtType.Items.FindByValue(ds.Tables[0].Rows[0]["CourtType_Id"].ToString().Trim()).Selected = true; ddlCourtType.Enabled = false;
                if (ds.Tables[0].Rows[0]["Casetype_ID"].ToString() != "") ddlCasetype.ClearSelection(); ddlCasetype.Items.FindByValue(ds.Tables[0].Rows[0]["Casetype_ID"].ToString()).Selected = true; ddlCasetype.Enabled = false;
                if (ds.Tables[0].Rows[0]["District_Id"].ToString() != "")
                {
                    ddlCourtLocation.ClearSelection();
                    ddlCourtType_SelectedIndexChanged(sender, e);
                    ddlCourtLocation.Items.FindByValue(ds.Tables[0].Rows[0]["District_Id"].ToString()).Selected = true;
                    ddlCourtLocation.Enabled = false;
                }
                if (ds.Tables[0].Rows[0]["CaseSubjectID"].ToString() != "")
                {
                    ddlCaseSubject.ClearSelection();
                    ddlCaseSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubjectID"].ToString().Trim()).Selected = true;
                    ViewState["SubjectID"] = ds.Tables[0].Rows[0]["CaseSubjectID"].ToString();// for check before Case Dispose.
                }
                else { ViewState["SubjectID"] = "NA"; }
                if (ds.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString() != "")
                {
                    ddlCaseSubject_SelectedIndexChanged(sender, e);
                    ddlCaseSubSubject.ClearSelection();
                    ddlCaseSubSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString()).Selected = true;

                }
                if (ds.Tables[0].Rows[0]["Party_Id"].ToString() != "")
                {
                    ddlParty.ClearSelection();
                    ddlParty.Items.FindByValue(ds.Tables[0].Rows[0]["Party_Id"].ToString().Trim()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["OICMaster_Id"].ToString() != "")
                {
                    ddlOicName.ClearSelection();
                    ddlOicName.Items.FindByValue(ds.Tables[0].Rows[0]["OICMaster_Id"].ToString().Trim()).Selected = true;
                    ddlOicName_SelectedIndexChanged(sender, e);
                }
                if (ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString() != "")
                {
                    ddlHighprioritycase.ClearSelection();
                    ddlHighprioritycase.Items.FindByText(ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString()).Selected = true;
                }
                ddlCaseYear.ClearSelection();
                if (ddlCaseYear.Items.Count > 0) ddlCaseYear.Items.FindByText(ds.Tables[0].Rows[0]["CaseYear"].ToString().Trim()).Selected = true; ddlCaseYear.Enabled = false;

                if (ds.Tables[1].Rows.Count > 0) GrdRespondentDtl.DataSource = ds.Tables[1]; GrdRespondentDtl.DataBind();
                if (ds.Tables[2].Rows.Count > 0) GrdHearingDtl.DataSource = ds.Tables[2]; GrdHearingDtl.DataBind();
                if (ds.Tables[3].Rows.Count > 0) GrdPetiDtl.DataSource = ds.Tables[3]; GrdPetiDtl.DataBind();
                if (ds.Tables[4].Rows.Count > 0) GrdCaseDocument.DataSource = ds.Tables[4]; GrdCaseDocument.DataBind();
                if (ds.Tables[5].Rows.Count > 0) GrdDeptAdvDtl.DataSource = ds.Tables[5]; GrdDeptAdvDtl.DataBind();
                if (ds.Tables[7].Rows.Count > 0) GrdOldCaseDtl.DataSource = ds.Tables[7]; GrdOldCaseDtl.DataBind();
                if (ds.Tables[8].Rows.Count > 0) GrdPetiAdv.DataSource = ds.Tables[8]; GrdPetiAdv.DataBind();
                if (ds.Tables[6].Rows[0]["CaseDisposal_Status"].ToString() != "")
                {
                    GrdCaseDispose.DataSource = ds.Tables[6]; GrdCaseDispose.DataBind();
                    DisposalStatus.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }

    }
    #endregion

    #region Update_MainDetail_Button
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnUpdate.Text == "Update" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Update_CaseRegisDtl", new string[] { "flag", "CaseSubject_Id", "CaseSubSubj_Id", "OICMaster_Id", "Party_Id", "HighPriorityCase_Status", "CaseDetail", "Case_ID", "UniqueNo", "LastupdatedBy", "LastupdatedByIP" }
                        , new string[] { "1", ddlCaseSubject.SelectedValue, ddlCaseSubSubject.SelectedValue, ddlOicName.SelectedValue, ddlParty.SelectedValue, ddlHighprioritycase.SelectedItem.Text.Trim(), txtCaseDetail.Text.Trim(), ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);

                        ddlCaseSubject.ClearSelection();
                        ddlCaseSubSubject.ClearSelection();
                        ddlOicName.ClearSelection();
                        ddlParty.ClearSelection();
                        ddlHighprioritycase.ClearSelection();
                        txtCaseDetail.Text = "";
                        BindDetails(sender, e);
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    protected void ddlCourtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddloldCourtLoca_Id.Items.Clear();
            DataSet dsCourt = obj.ByDataSet("select  CT.District_Id, District_Name  from tbl_LegalCourtType CT INNER Join Mst_District DM on DM.District_ID = CT.District_Id where CourtType_ID = " + ddlCourtType.SelectedValue);
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddlCourtLocation.DataTextField = "District_Name";
                ddlCourtLocation.DataValueField = "District_Id";
                ddlCourtLocation.DataSource = dsCourt;
                ddlCourtLocation.DataBind();
            }
            ddlCourtLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCaseSubSubject.Items.Clear();
            DataSet DsSubs = obj.ByDataSet("select CaseSubSubj_Id, CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectID=" + ddlCaseSubject.SelectedValue);
            if (DsSubs != null && DsSubs.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubSubject.DataTextField = "CaseSubSubject";
                ddlCaseSubSubject.DataValueField = "CaseSubSubj_Id";
                ddlCaseSubSubject.DataSource = DsSubs;
                ddlCaseSubSubject.DataBind();
            }
            ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    protected void ddlOicName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dsOic = obj.ByDataSet("select OICMobileNo,OICEmailID from tblOICMaster where OICMaster_Id = " + ddlOicName.SelectedValue);
            if (dsOic.Tables[0].Rows.Count > 0)
            {
                 txtOicMobileNo.Text = dsOic.Tables[0].Rows[0]["OICMobileNo"].ToString(); 
                 txtOicEmailId.Text = dsOic.Tables[0].Rows[0]["OICEmailID"].ToString(); 
            }
            else
            {
                txtOicMobileNo.Text = "";
                txtOicEmailId.Text =  "";
            }
        }
        catch (Exception ex)
        {

            ErrorLogCls.SendErrorToText(ex);
        }
    }

    #region PetitionerDtl
    protected void GrdPetiDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditRecord")
            {
                ViewState["Petitioner_ID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblUniqNO = (Label)row.FindControl("lblUniqNO");
                Label lblPetitionerName = (Label)row.FindControl("lblPetitionerName");
                Label lblDesignation_Id = (Label)row.FindControl("lblDesignation_Id");
                Label lblPetitionermobileNo = (Label)row.FindControl("lblPetitionermobileNo");
                Label lblAddress = (Label)row.FindControl("lblAddress");
                ViewState["Petitioner_ID"] = e.CommandArgument;
                btnPetitioner.Text = "Update";
                if (lblPetitionerName.Text != "") txtPetiName.Text = lblPetitionerName.Text;
                if (lblPetitionermobileNo.Text != "") txtPetiMobileNo.Text = lblPetitionermobileNo.Text;
                if (lblAddress.Text != "") txtPetiAddRess.Text = lblAddress.Text;
                if (lblDesignation_Id.Text != "")
                {
                    ddlPetiDesigNation.ClearSelection();
                    ddlPetiDesigNation.Items.FindByValue(lblDesignation_Id.Text).Selected = true;
                }
            }
            if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_PetiDtlForCaseRegi", new string[] { "flag", "UniqueNo", "Petitioner_Id", "LastIsactiveBy", "LastIsactiveByIP" }
                    , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnPetitioner_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dspeti = new DataSet();
            if (btnPetitioner.Text == "Save")
            {
                dspeti = obj.ByProcedure("USP_InsertUpdate_PetiDtlForCaseRegi", new string[] { "flag", "UniqueNo", "Case_Id", "PetitionerName", "Designation_Id", "PetitionerMobileNo", "PetitionerAddress", "CreatedBy", "CreatedByIP" }
                    , new string[] { "1", ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), txtPetiName.Text.Trim(), ddlPetiDesigNation.SelectedValue, txtPetiMobileNo.Text.Trim(), txtPetiAddRess.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            else if (btnPetitioner.Text == "Update")
            {
                dspeti = obj.ByProcedure("USP_InsertUpdate_PetiDtlForCaseRegi", new string[] { "flag", "UniqueNo", "Petitioner_Id", "PetitionerName", "Designation_Id", "PetitionerMobileNo", "PetitionerAddress", "LastupdatedBy", "LastupdatedByIP" }
                  , new string[] { "2", ViewState["UniqueNO"].ToString(), ViewState["Petitioner_ID"].ToString(), txtPetiName.Text.Trim(), ddlPetiDesigNation.SelectedValue, txtPetiMobileNo.Text.Trim(), txtPetiAddRess.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            if (dspeti != null && dspeti.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = dspeti.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (dspeti.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtPetiName.Text = "";
                    ddlPetiDesigNation.ClearSelection();
                    txtPetiMobileNo.Text = "";
                    txtPetiAddRess.Text = "";
                    btnPetitioner.Text = "Save";
                    BindDetails(sender, e);
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

    #region Petitioner_AdvDtl
    protected void btnPetiAdvSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnPetiAdvSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_InsertUpdate_PetiAdvForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "PetiAdv_Name", "PetiAdv_MobileNo", "CreatedBy", "CreatedByIP" }
                        , new string[] { "1", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), txtPetiAdvocateName.Text.Trim(), txtPetiAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnPetiAdvSave.Text == "Update" && ViewState["PetiAdv_Id"].ToString().ToString() != "" && ViewState["PetiAdv_Id"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_InsertUpdate_PetiAdvForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "PetiAdv_Id", "PetiAdv_Name", "PetiAdv_MobileNo", "LastupdatedBy", "LastupdatedByIP" }
                        , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["PetiAdv_Id"].ToString(), txtPetiAdvocateName.Text.Trim(), txtPetiAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtPetiAdvocateName.Text = "";
                        txtPetiAdvocateMobileNo.Text = "";
                        ViewState["PetiAdv_Id"] = "";
                        BindDetails(sender, e);
                        btnPetiAdvSave.Text = "Save";
                    }
                    else
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdPetiAdv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["PetiAdv_Id"] = "";
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblPetiAdvocatename = (Label)row.FindControl("lblPetiAdvocatename");
                Label lblPetiAdvocatMObile = (Label)row.FindControl("lblPetiAdvocatMObile");


                ViewState["PetiAdv_Id"] = e.CommandArgument;
                btnPetiAdvSave.Text = "Update";
                if (lblPetiAdvocatename.Text != "") txtPetiAdvocateName.Text = lblPetiAdvocatename.Text;
                if (lblPetiAdvocatMObile.Text != "") txtPetiAdvocateMobileNo.Text = lblPetiAdvocatMObile.Text;
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_PetiAdvForCaseRegis", new string[] { "flag", "UniqueNo", "PetiAdv_Id", "LastisactiveBy", "LastisactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

    #region RespondentDtl
    protected void ddlResOfficetypeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlResOfficeName.Items.Clear();
            ds = obj.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlResOfficetypeName.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlResOfficeName.DataTextField = "OfficeName";
                ddlResOfficeName.DataValueField = "Office_Id";
                ddlResOfficeName.DataSource = ds;
                ddlResOfficeName.DataBind();
            }
            ddlResOfficeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdRespondentDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                ViewState["ResponderID"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblOfficetype_ID = (Label)row.FindControl("lblOfficetype_ID");
                Label lblOffice_Id = (Label)row.FindControl("lblOffice_Id");
                Label lblDesignation_Id = (Label)row.FindControl("lblDesignation_Id");
                Label lblResName = (Label)row.FindControl("lblRespondentName");
                Label lblResMobileNo = (Label)row.FindControl("lblRespondentNo");
                Label lblDepartent = (Label)row.FindControl("lblDepartment");
                Label lblAddress = (Label)row.FindControl("lblAddress");

                ViewState["RespondentID"] = e.CommandArgument;
                btnRespondent.Text = "Update";

                if (lblResName.Text != "") txtResName.Text = lblResName.Text;
                if (lblResMobileNo.Text != "") txtResMobileNo.Text = lblResMobileNo.Text;
                if (lblDepartent.Text != "") txtResDepartment.Text = lblDepartent.Text;
                if (lblAddress.Text != "") txtResAddress.Text = lblAddress.Text;
                if (lblDesignation_Id.Text != "")
                {
                    ddlResDesig.ClearSelection();
                    ddlResDesig.Items.FindByValue(lblDesignation_Id.Text).Selected = true;
                }
                if (lblOfficetype_ID.Text != "")
                {
                    ddlResOfficetypeName.ClearSelection();
                    ddlResOfficetypeName.Items.FindByValue(lblOfficetype_ID.Text).Selected = true;
                }
                if (lblOffice_Id.Text != "")
                {
                    ddlResOfficetypeName_SelectedIndexChanged(sender, e);
                    ddlResOfficeName.ClearSelection();
                    ddlResOfficeName.Items.FindByValue(lblOffice_Id.Text).Selected = true;
                }
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_RespondentForCaseRegis", new string[] { "flag", "UniqueNo", "Respondent_ID", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    protected void btnRespondent_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsres = new DataSet();
            if (btnRespondent.Text == "Save")
            {
                dsres = obj.ByProcedure("USP_InsertUpdate_RespondentForCaseRegis", new string[] { "flag", "UniqueNo", "Case_Id", "Officetype_Id", "Office_Id", "Designation_Id", "RespondentName", "RespondentMobileNo", "Address", "Department", "CreatedBy", "CreatedByIP" }
                    , new string[] { "1", ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), ddlResOfficetypeName.SelectedValue, ddlResOfficeName.SelectedValue, ddlResDesig.SelectedValue, txtResName.Text.Trim(), txtResMobileNo.Text.Trim(), txtResAddress.Text.Trim(), txtResDepartment.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            else if (btnRespondent.Text == "Update")
            {
                dsres = obj.ByProcedure("USP_InsertUpdate_RespondentForCaseRegis", new string[] { "flag", "UniqueNo", "Respondent_ID", "Officetype_Id", "Office_Id", "Designation_Id", "RespondentName", "RespondentMobileNo", "Address", "Department", "LastupdatedBy", "LastupdatedByIP" }
                  , new string[] { "2", ViewState["UniqueNO"].ToString(), ViewState["RespondentID"].ToString(), ddlResOfficetypeName.SelectedValue, ddlResOfficeName.SelectedValue, ddlResDesig.SelectedValue, txtResName.Text.Trim(), txtResMobileNo.Text.Trim(), txtResAddress.Text.Trim(), txtResDepartment.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            if (dsres != null && dsres.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = dsres.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (dsres.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtResName.Text = "";
                    ddlResOfficetypeName.ClearSelection();
                    txtResMobileNo.Text = "";
                    txtResAddress.Text = "";
                    txtResDepartment.Text = "";
                    ddlResDesig.ClearSelection();
                    ddlResOfficeName.ClearSelection();
                    btnRespondent.Text = "Save";
                    BindDetails(sender, e);
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

    #region Dept_AdvocateDtl
    protected void btnDeptAdvocate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsdept = new DataSet();
            if (btnDeptAdvocate.Text == "Save")
            {
                dsdept = obj.ByProcedure("USP_InsertUpdate_DeptAdvForCaseRegis", new string[] { "flag", "UniqueNo", "Case_Id", "DeptAdvName", "DeptAdvMobileNo", "CreatedBy", "CreatedByIP" }
                    , new string[] { "1", ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            else if (btnDeptAdvocate.Text == "Update")
            {
                dsdept = obj.ByProcedure("USP_InsertUpdate_DeptAdvForCaseRegis", new string[] { "flag", "UniqueNo", "DeptAdv_Id", "DeptAdvName", "DeptAdvMobileNo", "LastupdatedBy", "LastupdatedByIP" }
                  , new string[] { "2", ViewState["UniqueNO"].ToString(), ViewState["DeptAdv_Id"].ToString(), txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            if (dsdept != null && dsdept.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = dsdept.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (dsdept.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtDeptAdvocateName.Text = "";
                    txtDeptAdvocateMobileNo.Text = "";
                    btnDeptAdvocate.Text = "Save";
                    BindDetails(sender, e);
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdDeptAdvDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                ViewState["DeptAdv_Id"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblAdvocateName = (Label)row.FindControl("lblAdvocateName");
                Label lblMobileNo = (Label)row.FindControl("lblMobileNo");
                ViewState["DeptAdv_Id"] = e.CommandArgument;
                btnDeptAdvocate.Text = "Update";
                if (lblMobileNo.Text != "") txtDeptAdvocateMobileNo.Text = lblMobileNo.Text;
                if (lblAdvocateName.Text != "") txtDeptAdvocateName.Text = lblAdvocateName.Text;
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_DeptAdvForCaseRegis", new string[] { "flag", "UniqueNo", "DeptAdv_Id", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

    #region DocumentDtl
    protected void btnSaveDoc_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                if (ViewState["AddNewCaseDoc"] == "")
                    ViewState["AddNewCaseDoc"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileCaseDoc.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FileCaseDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FileCaseDoc.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FileCaseDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseCourtDoc/"), strFileName);
                        FileCaseDoc.SaveAs(path);

                        ViewState["AddNewCaseDoc"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                if (errormsg == "")
                {
                    if (btnSaveDoc.Text == "Save")
                    {

                        ds = obj.ByProcedure("USP_InsertUpdate_DocsForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "Doc_Name", "Doc_Path", "CreatedBy", "CreatedByIP" }
                            , new string[] { "1", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), txtDocumentName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnSaveDoc.Text == "Update" && ViewState["DocID"].ToString().ToString() != "" && ViewState["DocID"].ToString() != null)
                    {
                        if (ViewState["DocPath"] != ViewState["AddNewCaseDoc"])
                        {
                            string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseCourtDoc/"), ViewState["DocPath"].ToString());
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }

                        ds = obj.ByProcedure("USP_InsertUpdate_DocsForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "CaseDoc_ID", "Doc_Name", "Doc_Path", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["DocID"].ToString(), txtDocumentName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            txtDocumentName.Text = "";
                            ViewState["AddNewCaseDoc"] = "";
                            BindDetails(sender, e);
                            btnSaveDoc.Text = "Save";
                        }
                        else
                            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                    if (ViewState["AddNewCaseDoc"] != "")
                    {
                        string path = ViewState["AddNewCaseDoc"].ToString();
                        if (File.Exists(path)) File.Delete(path); ViewState["AddNewCaseDoc"] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            }
        }
    }
    protected void GrdCaseDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["DocID"] = "";
            if (e.CommandName == "EditRecord")
            {
                ViewState["DocPath"] = "";
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDocPath = (Label)row.FindControl("lblDocPath");
                Label lblDocName = (Label)row.FindControl("lblDocName");
                if (lblDocPath.Text != "")
                {
                    ViewState["DocPath"] = lblDocPath.Text;
                    ViewState["AddNewCaseDoc"] = lblDocPath.Text;
                    RfvUploadDoc.Enabled = false;
                }
                if (lblDocName.Text != "")
                    txtDocumentName.Text = lblDocName.Text;
                ViewState["DocID"] = e.CommandArgument;
                btnSaveDoc.Text = "Update";

            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_DocsForCaseRegis", new string[] { "flag", "UniqueNo", "CaseDoc_ID", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrdCaseDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Link = (HyperLink)e.Row.FindControl("hyperViewLink");
                HyperLink DocWithFolderPath = (HyperLink)e.Row.FindControl("hyperViewDoc");
                Label lbldoc = (Label)e.Row.FindControl("lblDocPath");
                int coul = GrdCaseDocument.Columns.Count;

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

    #region HearingDtl
    protected void btnAddHeairng_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                ViewState["HearingDoc"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileHearingDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FileHearingDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FileHearingDoc.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FileHearingDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "Hearing-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/HearingDoc/"), strFileName);
                        FileHearingDoc.SaveAs(path);

                        ViewState["HearingDoc"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                if (errormsg == "")
                {
                    string NextHearingDate = txtNextHearingDate.Text != "" ? Convert.ToDateTime(txtNextHearingDate.Text, cult).ToString("yyyy/MM/dd") : "";

                    if (btnAddHeairng.Text == "Save")
                    {
                        ds = obj.ByProcedure("USP_InsertUpdate_HearingForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "NextHearingDate", "HearingDoc", "CreatedBy", "CreatedByIP" }
                            , new string[] { "1", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), NextHearingDate, ViewState["HearingDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnAddHeairng.Text == "Update" && ViewState["Hearing_Id"].ToString().ToString() != "" && ViewState["Hearing_Id"].ToString() != null)
                    {
                        if (ViewState["EditDocPath"] != ViewState["HearingDoc"])
                        {
                            string path = Path.Combine(Server.MapPath("../Legal/HearingDoc/"), ViewState["EditDocPath"].ToString());
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }
                        ds = obj.ByProcedure("USP_InsertUpdate_HearingForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "NextHearing_ID", "NextHearingDate", "HearingDoc", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["Hearing_Id"].ToString(), NextHearingDate, ViewState["HearingDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            ViewState["HearingDoc"] = "";
                            BindDetails(sender, e);
                            btnSaveDoc.Text = "Save";
                        }
                        else
                            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                    if (ViewState["HearingDoc"] != "") { if (File.Exists(ViewState["HearingDoc"].ToString()))File.Exists(ViewState["HearingDoc"].ToString()); ViewState["HearingDoc"] = ""; }
                }
            }
            catch (Exception ex)
            {
                ErrorLogCls.SendErrorToText(ex);
            }
        }
    }
    protected void GrdHearingDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["Hearing_Id"] = "";
            if (e.CommandName == "EditRecord")
            {
                ViewState["EditDocPath"] = "";
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblHearingDate = (Label)row.FindControl("lblHearingDate");
                Label lblHearingDocPath = (Label)row.FindControl("lblHearingDocPath");
                if (lblHearingDocPath.Text != "")
                {
                    ViewState["EditDocPath"] = lblHearingDocPath.Text;
                    ViewState["HearingDoc"] = lblHearingDocPath.Text;
                    rfvhearingFile.Enabled = false;
                }
                if (lblHearingDate.Text != "") txtNextHearingDate.Text = lblHearingDate.Text;
                ViewState["Hearing_Id"] = e.CommandArgument;
                btnAddHeairng.Text = "Update";

            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_HearingForCaseRegis", new string[] { "flag", "UniqueNo", "NextHearing_ID", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

    #region CaseDispose
    protected void ddlDisponsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
            if (ddlDisponsType.SelectedIndex > 0)
            {
                OrderBy1.Visible = true;
                OrderBy2.Visible = true;
                HearingDtl_CaseDispose.Visible = true;
                DivOrderTimeline.Visible = true;
                OrderSummary_Div.Visible = true;
                CimplianceSt_Div.Visible = false;
                if (ddlDisponsType.SelectedIndex == 2)
                {
                    ddlCompliaceSt.ClearSelection();
                    CimplianceSt_Div.Visible = true;
                    RequiredFieldValidator6.Enabled = true;
                }
            }
            else
            {
                HearingDtl_CaseDispose.Visible = false;
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
                DivOrderTimeline.Visible = false;
                OrderSummary_Div.Visible = false;
                CimplianceSt_Div.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void rdCaseDispose_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            caseDisposeYes.Visible = false;
            if (rdCaseDispose.SelectedValue == "1")
            {
                caseDisposeYes.Visible = true;
            }
            else
            {
                caseDisposeYes.Visible = false;
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
                DivOrderTimeline.Visible = false;
                ddlDisponsType.ClearSelection();
                HearingDtl_CaseDispose.Visible = false;
                OrderSummary_Div.Visible = false;
                CimplianceSt_Div.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnCaseDispose_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                lblMsg.Text = "";

                ViewState["DisposeDOC"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FielUpcaseDisposeOrderDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FielUpcaseDisposeOrderDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {

                        strFileName = FielUpcaseDisposeOrderDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                        FielUpcaseDisposeOrderDoc.SaveAs(path);

                        ViewState["DisposeDOC"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                if (errormsg == "")
                {
                    if (ViewState["SubjectID"].ToString() != "NA")
                    {
                        if (btnCaseDispose.Text == "Disposal")
                        {
                            string Compliance_Status = ddlCompliaceSt.SelectedIndex > 0 ? ddlCompliaceSt.SelectedItem.Text : null;
                            string ComplianceStatus_ID = ddlCompliaceSt.SelectedIndex > 0 ? ddlCompliaceSt.SelectedValue : null;
                            string DisposalDate = txtCaseDisposeDate.Text != "" ? Convert.ToDateTime(txtCaseDisposeDate.Text, cult).ToString("yyyy/MM/dd") : "";
                            ds = obj.ByProcedure("USP_Update_CaseRegisDtl", new string[] { "flag", "Case_ID", "UniqueNo", "CaseDisposal_Status", "CaseDisposalType_Id", "CaseDisposal_Date", "CaseDisposal_Timeline", "CaseDisposal_Doc", "OrderSummary", "LastupdatedBy", "LastupdatedByIP", "Compliance_Status", "ComplianceStatus_ID" }
                               , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), rdCaseDispose.SelectedItem.Text, ddlDisponsType.SelectedValue, DisposalDate, txtOrderimpletimeline.Text.Trim(), ViewState["DisposeDOC"].ToString(), txtorderSummary.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), Compliance_Status, ComplianceStatus_ID }, "dataset");
                        }
                        else if (btnCaseDispose.Text == "Update" && ViewState["Dispose_ID"] != null)
                        {
                            ds = obj.ByProcedure("USP_UpdateCaseDisOrderByDirec", new string[] { "Compliance_Status", "OrderSummary", "ComplianceStatus_ID", "LastupdatedBy", "LastupdatedByIp", "Case_ID" }
                            , new string[] { ddlCompliaceSt.SelectedItem.Text.Trim(), txtorderSummary.Text.Trim(), ddlCompliaceSt.SelectedValue, Session["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Dispose_ID"].ToString() }, "dataset");
                        }
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                            {
                                lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                                txtOrderimpletimeline.Text = "";
                                txtorderSummary.Text = "";
                                rdCaseDispose.ClearSelection();
                                ddlDisponsType.ClearSelection();
                                ddlCompliaceSt.ClearSelection();
                                txtCaseDisposeDate.Text = "";
                                ViewState["DisposeDOC"] = "";
                                BindDetails(sender, e);
                                btnCaseDispose.Text = "Disposal";
                                rdCaseDispose_SelectedIndexChanged(sender, e);

                            }
                            else
                                lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Basic Detail OIC And Case Subject is Mandatory.')", true);
                        if (ViewState["DisposeDOC"] != "") { string path = ViewState["DisposeDOC"].ToString(); if (File.Exists(path)) File.Delete(path); ViewState["DisposeDOC"] = ""; }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                    if (ViewState["DisposeDOC"] != "") { string path = ViewState["DisposeDOC"].ToString(); if (File.Exists(path)) File.Delete(path); ViewState["DisposeDOC"] = ""; }
                }
            }
        }

        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdCaseDispose_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDoc")
            {
                ViewState["Dispose_ID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDisposalStatus = (Label)row.FindControl("lblDisposalStatus");
                Label lblDisposaltype_ID = (Label)row.FindControl("lblDisposaltype_ID");
                Label lblcomplianceSts = (Label)row.FindControl("lblcomplianceSts");
                Label lblorderSummary = (Label)row.FindControl("lblorderSummary");
                if (lblorderSummary.Text != "")
                {
                    txtorderSummary.Text = lblorderSummary.Text;
                }
                if (lblDisposaltype_ID.Text != "")
                {
                    ddlDisponsType.ClearSelection();
                    ddlDisponsType.Items.FindByValue(lblDisposaltype_ID.Text).Selected = true; caseDisposeYes.Visible = true;

                }
                if (lblDisposaltype_ID.Text == "2")
                {
                    if (lblcomplianceSts.Text != "")
                    {
                        ddlDisponsType.Enabled = false;
                        ddlCompliaceSt.ClearSelection();
                        ddlCompliaceSt.Items.FindByValue(lblcomplianceSts.Text).Selected = true; CimplianceSt_Div.Visible = true;
                        HearingDtl_CaseDispose.Visible = true;
                        OrderSummary_Div.Visible = true;
                    }
                    ViewState["Dispose_ID"] = e.CommandArgument.ToString();
                    btnCaseDispose.Text = "Update";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

    #region OldCaseDtl
    protected void ddlOldCaseAsk_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DivOldCase.Visible = false;
            if (ddlOldCaseAsk.SelectedValue == "1") DivOldCase.Visible = true;
            else
                DivOldCase.Visible = false;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnOldCase_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ViewState["FU1"] = "";
                ViewState["FU2"] = "";
                ViewState["FU3"] = "";
                ViewState["FU4"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FU1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FU1.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU1.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU1.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "OldCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU1.SaveAs(path);

                        ViewState["FU1"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                if (FU2.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU2.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU2.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU2.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "OldCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU2.SaveAs(path);

                        ViewState["FU2"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                if (FU3.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU3.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU3.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU3.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "OldCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU3.SaveAs(path);

                        ViewState["FU3"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                if (FU4.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU4.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU4.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU4.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "Hearing-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU4.SaveAs(path);

                        ViewState["FU4"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                if (errormsg == "")
                {

                    if (btnOldCase.Text == "Save")
                    {
                        if (txtoldCaseNo.Text != "")
                        {
                            if (FU1.HasFile)// Insert data into oldCase Record table
                            {
                                ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", 
                                           "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                 new string[] { "1", ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text,  ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "केस का विवरण", ViewState["FU1"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, 
                               ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                            }
                            if (FU2.HasFile)
                            {
                                ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                   new string[] { "1", ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "कार्यवाही का विवरण", ViewState["FU2"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                            }
                            if (FU3.HasFile)
                            {
                                ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                   new string[] { "1", ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "निर्णय", ViewState["FU3"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                            }
                            if (FU4.HasFile)
                            {
                                ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                   new string[] { "1", ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "अन्य", ViewState["FU4"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                            }

                        }
                    }
                    else if (btnOldCase.Text == "Update" && ViewState["OldCase_Id"] != "")
                    {
                        ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Id", "UniqueNo", "oldCaseYear", "CourtType_Id", "Court", "CourtDistLoca_Id", "Casetype_Id", "OldCasetype", "LastupdatedBy", "LastupdatedByIP" }
                            , new string[] { "2", ViewState["OldCase_Id"].ToString(), ViewState["UniqueNO"].ToString(), ddloldCaseYear.SelectedItem.Text, ddloldCaseCourt.SelectedValue, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedValue, ddloldCasetype.SelectedValue, ddloldCasetype.SelectedItem.Text, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            txtoldCaseNo.Text = "";
                            ddloldCaseYear.ClearSelection();
                            ddloldCasetype.ClearSelection();
                            ddloldCourtLoca_Id.ClearSelection();
                            ddloldCaseCourt.ClearSelection();
                            ViewState["FU1"] = ""; ViewState["FU2"] = ""; ViewState["FU3"] = ""; ViewState["FU4"] = "";
                            //By Default Set No for OldCase.
                            OldCase_BydefaultNO();
                            BindDetails(sender, e);
                        }
                        else lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                    if (ViewState["FU1"] != "") { string path = ViewState["FU1"].ToString(); if (File.Exists(path)) File.Delete(path); ViewState["FU1"] = ""; }
                    if (ViewState["FU2"] != "") { string path = ViewState["FU2"].ToString(); if (File.Exists(path)) File.Delete(path); ViewState["FU2"] = ""; }
                    if (ViewState["FU3"] != "") { string path = ViewState["FU3"].ToString(); if (File.Exists(path)) File.Delete(path); ViewState["FU3"] = ""; }
                    if (ViewState["FU4"] != "") { string path = ViewState["FU4"].ToString(); if (File.Exists(path)) File.Delete(path); ViewState["FU4"] = ""; }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdOldCaseDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["OldCase_Id"] = "";
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblOldCaseNo = (Label)row.FindControl("lblOldCaseNo");
                Label lblOldCaseYear = (Label)row.FindControl("lblOldCaseYear");
                Label lblOldCasetype_Id = (Label)row.FindControl("lblOldCasetype_Id");
                Label lblOldCourt_Id = (Label)row.FindControl("lblOldCourt_Id");
                Label lblOldCourtLoca_Id = (Label)row.FindControl("lblOldCourtLoca_Id");
                Label lblOldDocName = (Label)row.FindControl("lblOldDocName");

                ViewState["OldCase_Id"] = e.CommandArgument;
                btnOldCase.Text = "Update";
                if (lblOldCaseNo.Text != "") txtoldCaseNo.Text = lblOldCaseNo.Text;
                if (lblOldCaseYear.Text != "") ddloldCaseYear.ClearSelection(); ddloldCaseYear.Items.FindByValue(lblOldCaseYear.Text).Selected = true;
                if (lblOldCourt_Id.Text != "") ddloldCaseCourt.ClearSelection(); ddloldCaseCourt.Items.FindByValue(lblOldCourt_Id.Text).Selected = true;
                if (lblOldCasetype_Id.Text != "") ddloldCasetype.ClearSelection(); ddloldCasetype.Items.FindByValue(lblOldCasetype_Id.Text).Selected = true;
                if (lblOldCourtLoca_Id.Text != "")
                {
                    ddloldCaseCourt_SelectedIndexChanged(sender, e);
                    ddloldCourtLoca_Id.ClearSelection();
                    ddloldCourtLoca_Id.Items.FindByValue(lblOldCourtLoca_Id.Text).Selected = true;
                    // Visible All Control
                    ddlOldCaseAsk.ClearSelection();
                    ddlOldCaseAsk.Items.FindByValue("1").Selected = true;
                    ddlOldCaseAsk_SelectedIndexChanged( sender,  e);
                }
                Div_Doc1.Visible = false; Div_Doc2.Visible = false; Div_Doc3.Visible = false; Div_Doc4.Visible = false;
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "UniqueNo", "Id", "LastisactiveBy", "LastisactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddloldCaseCourt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddloldCourtLoca_Id.Items.Clear();
            DataSet dsCourt = obj.ByDataSet("select  CT.District_Id, District_Name  from tbl_LegalCourtType CT INNER Join Mst_District DM on DM.District_ID = CT.District_Id where CourtType_ID = " + ddloldCaseCourt.SelectedValue);
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddloldCourtLoca_Id.DataTextField = "District_Name";
                ddloldCourtLoca_Id.DataValueField = "District_Id";
                ddloldCourtLoca_Id.DataSource = dsCourt;
                ddloldCourtLoca_Id.DataBind();
            }
            ddloldCourtLoca_Id.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion

    // Decrypt Method
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
