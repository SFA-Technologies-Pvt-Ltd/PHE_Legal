using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;

public partial class Legal_AddNewCase : System.Web.UI.Page
{
    DataSet ds;
    DataSet dsRecord;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    Helper hlp = new Helper();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Office_ID"] = Session["Office_ID"];
                    FillDistrict();
                    BindCourtName();
                    BindRespondertype();
                    BindOfficeType();
                    FillCasetype();
                    BindCaseSubject();
                    DtColumn();
                    FillYear();
                    FillColumn();
                    FillDesignation();
                    BiunOicName();
                    FillParty();
                    FillOldCaseYear();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void FillParty()
    {
        try
        {
            ddlParty.Items.Clear();
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

    #region Fill Designation
    protected void FillDesignation()
    {
        try
        {
            ddlDesignation.Items.Clear();
            ddlPetiDesigNation.Items.Clear();
            ds = objdb.ByDataSet("select Designation_Id, Designation_Name from tblDesignationMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesignation.DataTextField = "Designation_Name";
                ddlDesignation.DataValueField = "Designation_Id";
                ddlDesignation.DataSource = ds;
                ddlDesignation.DataBind();

                ddlPetiDesigNation.DataTextField = "Designation_Name";
                ddlPetiDesigNation.DataValueField = "Designation_Id";
                ddlPetiDesigNation.DataSource = ds;
                ddlPetiDesigNation.DataBind();
            }
            ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
            ddlPetiDesigNation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Year
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        for (int i = 2018; i <= DateTime.Now.Year; i++)
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
            DataSet dsCase = objdb.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year");
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
    #region Fill Respondent
    protected void BindRespondertype()
    {
        try
        {
            ddlRespondertype.Items.Clear();
            ds = objdb.ByProcedure("USP_Get_ResponderType", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRespondertype.DataTextField = "RespondertypeName";
                ddlRespondertype.DataValueField = "Respondertype_ID";
                ddlRespondertype.DataSource = ds;
                ddlRespondertype.DataBind();
            }
            ddlRespondertype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    # region Doc Datatable
    protected void DtColumn()
    {
        DataTable dtcol = new DataTable();
        if (dtcol.Columns.Count == 0)
        {
            dtcol.Columns.Add("DocName", typeof(string));
            dtcol.Columns.Add("Document", typeof(string));
        }
        ViewState["dtcol"] = dtcol;
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
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CourtName
    protected void BindCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear(); ddloldCaseCourt.Items.Clear();
            ds = objdb.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataTextField = "CourtTypeName";
                ddlCourtType.DataValueField = "CourtType_ID";
                ddlCourtType.DataSource = ds;
                ddlCourtType.DataBind();

                ddloldCaseCourt.DataTextField = "CourtTypeName";
                ddloldCaseCourt.DataValueField = "CourtType_ID";
                ddloldCaseCourt.DataSource = ds;
                ddloldCaseCourt.DataBind();
            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
            ddloldCaseCourt.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill OfficeType
    protected void BindOfficeType()
    {
        try
        {
            ddlOfficetypeName.Items.Clear();
            ds = objdb.ByProcedure("USP_Select_Officetype", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficetypeName.DataTextField = "OfficeType_Name";
                ddlOfficetypeName.DataValueField = "OfficeType_Id";
                ddlOfficetypeName.DataSource = ds;
                ddlOfficetypeName.DataBind();
            }
            ddlOfficetypeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region Fill District as Loaction
    protected void FillDistrict()
    {
        try
        {
            ddloldCourtLoca_Id.Items.Clear(); ddlCourtLocation.Items.Clear();
            ds = objdb.ByProcedure("USP_Select_District", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtLocation.DataTextField = "District_Name";
                ddlCourtLocation.DataValueField = "District_ID";
                ddlCourtLocation.DataSource = ds;
                ddlCourtLocation.DataBind();

                ddloldCourtLoca_Id.DataTextField = "District_Name";
                ddloldCourtLoca_Id.DataValueField = "District_ID";
                ddloldCourtLoca_Id.DataSource = ds;
                ddloldCourtLoca_Id.DataBind();
            }
            ddlCourtLocation.Items.Insert(0, new ListItem("Select", "0"));
            ddloldCourtLoca_Id.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CaseStatus
    protected void BindCaseSubject()
    {
        try
        {
            ddlCaseSubject.Items.Clear();
            ds = objdb.ByDataSet("SELECT CaseSubject, CaseSubjectID FROM tbl_LegalMstCaseSubject");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubject.DataTextField = "CaseSubject";
                ddlCaseSubject.DataValueField = "CaseSubjectID";
                ddlCaseSubject.DataSource = ds;
                ddlCaseSubject.DataBind();
            }
            ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region SubmitButton
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string msg = "";
                if (txtCaseNo.Text == "")
                {
                    msg += "Enter Case Number.\\n";
                }
                if (ddlCourtType.SelectedIndex < 0)
                {
                    msg += "Select Court Type.\\n";
                }
                if (ddlCaseSubject.SelectedIndex < 0)
                {
                    msg += "Select Case Subject.\\n";
                }
                if (ddlHighprioritycase.SelectedIndex < 0)
                {
                    msg += "Select High Priority Case.\\n";
                }
                if (txtCaseDetail.Text == "")
                {
                    msg += "Enter Subject Of Case.\\n";
                }
                string UniqueNo = "0125";
                string filePath = Server.MapPath("~/Legal/OldCaseDocument");
                string filename1 = string.Empty;
                string filename2 = string.Empty;
                string filename3 = string.Empty;
                string filename4 = string.Empty;
                if (FU1.HasFile)
                    filename1 = FU1.FileName;
                if (FU2.HasFile)
                    filename2 = FU2.FileName;
                if (FU3.HasFile)
                    filename3 = FU3.FileName;
                if (FU4.HasFile)
                    filename4 = FU4.FileName;
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

                    if (msg == "")
                    {
                        lblMsg.Text = "";
                        if (btnSubmit.Text == "Save")
                        {
                            DataTable dtDoc = ViewState["DocData"] as DataTable;
                            DataTable dtresponder = ViewState["dt"] as DataTable;
                            if (GrdViewDoc.Rows.Count > 0 && GrdRespondent.Rows.Count > 0)
                            {
                                string RegDate = txtDateOfCaseReg.Text != "" ? Convert.ToDateTime(txtDateOfCaseReg.Text, cult).ToString("yyyy/MM/dd") : "";
                                string LastHearingDate = txtDateOfLastHearing.Text != "" ? Convert.ToDateTime(txtDateOfLastHearing.Text, cult).ToString("yyyy/MM/dd") : "";
                                string NextHearingDate = txtNextHearingDate.Text != "" ? Convert.ToDateTime(txtNextHearingDate.Text, cult).ToString("yyyy/MM/dd") : "";

                                ds = objdb.ByProcedure("USP_Insert_NewCaseReg", new string[] {"CaseNo", "Casetype_ID", "CaseYear", "CourtType_Id", 
                                "CourtLocation_Id", "CaseSubject_Id", 
                                "CaseSubSubj_Id", "CaseRegDate", "lastHearingDate", "HighPriorityCase_Status", "PetitonerName", "Designation_Id", "PetitionerMobileNo", 
                                "PetitionerAddress", "PetiAdvocateName", "PetiAdvocateMobile", "OICMaster_Id", "Party_Id", "DeptAdvocateName", 
                                "DeptAdvocateMobileNo", "Office_Id","CaseDetail", "CreatedBy", "CreatedByIP","NextHearingDate","HearingDoc" },
                                    new string[] {txtCaseNo.Text.Trim(),ddlCasetype.SelectedValue,ddlCaseYear.SelectedItem.Text.Trim(),ddlCourtType.SelectedValue,ddlCourtLocation.SelectedValue,
                                    ddlCaseSubject.SelectedValue,
                                ddlSubSubject.SelectedValue,RegDate,LastHearingDate,ddlHighprioritycase.SelectedItem.Text,txtPetiName.Text.Trim(),ddlPetiDesigNation.SelectedValue,txtPetiMobileNo.Text.Trim(),
                                txtPetiAddRess.Text.Trim(),txtPetiAdvocateName.Text.Trim(),txtPetiAdvocateMobileNo.Text.Trim(),ddlOicName.SelectedValue, ddlParty.SelectedValue,txtDeptAdvocateName.Text.Trim(),
                                txtDeptAdvocateMobileNo.Text.Trim(),ViewState["Office_ID"].ToString(),txtCaseDetail.Text.Trim(),ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress(),NextHearingDate,ViewState["HearingDoc"].ToString()}, new string[] { "type_RespondentDtl", "type_DocumentDtl" }, new DataTable[] { dtresponder, dtDoc }, "dataset");

                                if (txtoldCaseNo.Text != "" && ds.Tables[0].Rows[0]["Case_ID"].ToString() != "")
                                {
                                    if (FU1.HasFile)
                                    {
                                        DataSet dsCase = objdb.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                              new string[] { ds.Tables[0].Rows[0]["Case_ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "केस का विवरण", filename1, ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCaseCourt.SelectedValue, ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
                                        string fname = Path.GetFileNameWithoutExtension(FU1.PostedFile.FileName) + "_" + UniqueNo + "_" + dsCase.Tables[0].Rows[0][0].ToString();
                                        string ext = System.IO.Path.GetExtension(FU1.PostedFile.FileName);
                                        FU1.SaveAs(filePath + "/" + fname + ext);
                                        
                                    }
                                    if (FU2.HasFile)
                                    {
                                        DataSet dsCase = objdb.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                            new string[] { ds.Tables[0].Rows[0]["Case_ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "कार्यवाही का विवरण", filename2, ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCaseCourt.SelectedValue, ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
                                        string fname = Path.GetFileNameWithoutExtension(FU1.PostedFile.FileName) + "_" + UniqueNo + "_" + dsCase.Tables[0].Rows[0][0].ToString();
                                        string ext = System.IO.Path.GetExtension(FU1.PostedFile.FileName);
                                        FU1.SaveAs(filePath + "/" + fname + ext);
                                        

                                    }
                                    if (FU3.HasFile)
                                    {
                                        DataSet dsCase = objdb.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                            new string[] { ds.Tables[0].Rows[0]["Case_ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "निर्णय", filename3, ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCaseCourt.SelectedValue, ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
                                        string fname = Path.GetFileNameWithoutExtension(FU1.PostedFile.FileName) + "_" + UniqueNo + "_" + dsCase.Tables[0].Rows[0][0].ToString();
                                        string ext = System.IO.Path.GetExtension(FU1.PostedFile.FileName);
                                        FU1.SaveAs(filePath + "/" + fname + ext);
                                       
                                    }
                                    if (FU4.HasFile)
                                    {
                                        DataSet dsCase = objdb.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                            new string[] { ds.Tables[0].Rows[0]["Case_ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "अन्य", filename4, ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCaseCourt.SelectedValue, ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
                                        string fname = Path.GetFileNameWithoutExtension(FU1.PostedFile.FileName) + "_" + UniqueNo + "_" + dsCase.Tables[0].Rows[0][0].ToString();
                                        string ext = System.IO.Path.GetExtension(FU1.PostedFile.FileName);
                                        FU1.SaveAs(filePath + "/" + fname + ext);
                                       
                                    }

                                }
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('please Add Document & Respondent Detail');", true);
                        }
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks!", ErrMsg);
                                ClearText();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", ErrMsg);
                            }
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region ClearButton
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();

    }
    #endregion
    #region ClearAllControl
    protected void ClearText()
    {
        txtCaseNo.Text = "";
        ddlSubSubject.ClearSelection();
        ddlParty.ClearSelection();
        ddlPetiDesigNation.ClearSelection();
        txtPetiAddRess.Text = "";
        ddlCourtType.ClearSelection();
        ddlCaseSubject.ClearSelection();
        txtDateOfCaseReg.Text = "";
        txtDateOfLastHearing.Text = "";
        txtCaseDetail.Text = "";
        txtDepartment.Text = "";
        txtOICMobileNo.Text = "";
        txtDeptAdvocateName.Text = "";
        txtDeptAdvocateMobileNo.Text = "";
        txtPetiName.Text = "";
        txtPetiMobileNo.Text = "";
        txtPetiAdvocateName.Text = "";
        txtPetiAdvocateMobileNo.Text = "";
        txtNextHearingDate.Text = "";
        GrdViewDoc.DataSource = null;
        GrdViewDoc.DataBind();
        ddlCasetype.ClearSelection();
        ddlCaseYear.ClearSelection();
        ddlCourtLocation.ClearSelection();
        ddlHighprioritycase.ClearSelection();
        ddlOicName.ClearSelection();
        GrdRespondent.DataSource = null;
        GrdRespondent.DataBind();
    }
    #endregion
    #region AddResponder Button
    protected void btnAddresponder_Click(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddRespondent()", true);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
            ds = objdb.ByDataSet("select Casetype_ID, Casetype_Name from  tbl_Legal_Casetype");
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
    #region AddDoc
    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["AddNewCaseDoc"] = "";
            int DocFailedCntExt = 0;
            int DocFailedCntSize = 0;
            string strFileName = "";
            string strExtension = "";
            string strTimeStamp = "";
            if (FileUpload10.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {

                string fileExt = System.IO.Path.GetExtension(FileUpload10.FileName).Substring(1);
                string[] supportedTypes = { "PDF", "pdf" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (FileUpload10.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                {
                    DocFailedCntSize += 1;
                }
                else
                {
                    strFileName = FileUpload10.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseCourtDoc/"), strFileName);
                    FileUpload10.SaveAs(path);

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
                DataTable dt = ViewState["dtcol"] as DataTable;
                if (dt.Columns.Count > 0)
                {
                    dt.Rows.Add(txtDocName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString());
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["DocData"] = dt;
                    GrdViewDoc.DataSource = dt;
                    GrdViewDoc.DataBind();
                    txtDocName.Text = "";
                    ViewState["AddNewCaseDoc"] = "";

                }
                else
                {
                    GrdViewDoc.DataSource = null;
                    GrdViewDoc.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region  Respondent Column
    protected void FillColumn()
    {
        DataTable dtCol = new DataTable();
        if (dtCol.Columns.Count == 0)
        {
            //dtCol.Columns.Add("RespondenttypeID", typeof(int));
            dtCol.Columns.Add("OfficeTypeID", typeof(int));
            dtCol.Columns.Add("OfficeNameId", typeof(int));
            dtCol.Columns.Add("DesignationId", typeof(int));
            dtCol.Columns.Add("DesignationName", typeof(string));
            dtCol.Columns.Add("RespondentName", typeof(string));
            dtCol.Columns.Add("RespondentMobileNo", typeof(string));
            dtCol.Columns.Add("Department", typeof(string));
            dtCol.Columns.Add("Address", typeof(string));
            //dtCol.Columns.Add("RespondenttypeName", typeof(string));
            dtCol.Columns.Add("OfficeTypeName", typeof(string));
            dtCol.Columns.Add("OfficeName", typeof(string));
        }
        ViewState["Responder"] = dtCol;
    }
    #endregion
    #region Bind OfficeName
    protected void ddlOfficetypeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeName.Items.Clear();
            ds = objdb.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlOfficetypeName.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataTextField = "OfficeName";
                ddlOfficeName.DataValueField = "Office_Id";
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataBind();
            }
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddRespondent()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Save Add ResponderDtl
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnYes.Text == "Add")
                {
                    DataTable dt = ViewState["Responder"] as DataTable;
                    if (dt.Columns.Count > 0)
                    {
                        dt.Rows.Add(ddlOfficetypeName.SelectedValue, ddlOfficeName.SelectedValue, ddlDesignation.SelectedValue, ddlDesignation.SelectedItem.Text, txtResponderName.Text.Trim(),
                            txtMobileNo.Text.Trim(), txtDepartment.Text.Trim(), txtAddress.Text.Trim(), ddlOfficetypeName.SelectedItem.Text.Trim(), ddlOfficeName.SelectedItem.Text.Trim());
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GrdRespondent.DataSource = dt;
                        GrdRespondent.DataBind();
                        ViewState["dt"] = dt;

                        //ddlOfficeName.ClearSelection();
                        ddlDesignation.ClearSelection();
                        ddlRespondertype.ClearSelection();
                        ddlOfficetypeName.ClearSelection();
                        txtResponderName.Text = "";
                        txtMobileNo.Text = "";
                        txtDepartment.Text = "";
                        txtAddress.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {

            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Bind MobileNo&Email
    protected void ddlOicName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByDataSet("select * from tblOICMaster where OICMaster_Id = " + ddlOicName.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtOICMobileNo.Text = ds.Tables[0].Rows[0]["OICMobileNo"].ToString();
                txtEmailID.Text = ds.Tables[0].Rows[0]["OICEmailID"].ToString();
            }
            else
            {
                txtOICMobileNo.Text = "";
                txtEmailID.Text = "";

            }
        }
        catch (Exception ex)
        {

            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlSubSubject.Items.Clear();
            DataSet DsSubs = objdb.ByDataSet("select CaseSubSubj_Id, CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectID=" + ddlCaseSubject.SelectedValue);
            if (DsSubs != null && DsSubs.Tables[0].Rows.Count > 0)
            {
                ddlSubSubject.DataTextField = "CaseSubSubject";
                ddlSubSubject.DataValueField = "CaseSubSubj_Id";
                ddlSubSubject.DataSource = DsSubs;
                ddlSubSubject.DataBind();
            }
            ddlSubSubject.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}

