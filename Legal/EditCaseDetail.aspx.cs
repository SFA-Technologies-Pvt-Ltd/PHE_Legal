using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;

public partial class Legal_EditCaseDetail : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["Emp_Id"] != "")
        {
            if (!IsPostBack)
            {
                ViewState["ID"] = Request.QueryString["ID"];
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                BindDetails();
                FillOldCaseYear();
                FillYear();
                BindCourtName();
                FillParty();
                FillCaseSubject();
                FillOicName();
            }
        }
        else
        {
            // Response.Redirect("../Login.aspx");
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPAGETOKEN"] = Session["PAGETOKEN"];
    }

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
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Year
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        for (int i = 2010; i <= DateTime.Now.Year; i++)
        {
            ddlCaseYear.Items.Add(i.ToString());
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }

    protected void FillOldCaseYear()
    {
        try
        {
            //ddloldCaseYear.Items.Clear();
            //DataSet dsCase = objdb.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year");
            //if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            //{
            //    ddloldCaseYear.DataSource = dsCase.Tables[0];
            //    ddloldCaseYear.DataTextField = "year";
            //    ddloldCaseYear.DataValueField = "year";
            //    ddloldCaseYear.DataBind();
            //    ddloldCaseYear.Items.Insert(0, new ListItem("Select", "0"));
            //}
            //else
            //{
            //    ddloldCaseYear.DataSource = null;
            //    ddloldCaseYear.DataBind();
            //    ddloldCaseYear.Items.Insert(0, new ListItem("Select", "0"));
            //}
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
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CourtName
    protected void BindCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear();// ddloldCaseCourt.Items.Clear();
            DataSet dsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "dataset");
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataTextField = "CourtTypeName";
                ddlCourtType.DataValueField = "CourtType_ID";
                ddlCourtType.DataSource = dsCourt;
                ddlCourtType.DataBind();

                //ddloldCaseCourt.DataTextField = "CourtTypeName";
                //ddloldCaseCourt.DataValueField = "CourtType_ID";
                //ddloldCaseCourt.DataSource = ds;
                //ddloldCaseCourt.DataBind();
            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
            // ddloldCaseCourt.Items.Insert(0, new ListItem("Select", "0"));
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



    protected void FieldClose()
    {

        FieldSet_CaseDetail.Visible = false; ;
        FieldViewDocument.Visible = false;
        FieldViewRespondent.Visible = false;
        Field_AddResponder.Visible = false;
    }

    protected void BindDetails()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_NewCaseRegis", new string[] { "Case_ID" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblCaseNo.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString();
                ddlCourtType.ClearSelection();
                if (ddlCourtType.Items.Count > 0) ddlCourtType.Items.FindByValue(ds.Tables[0].Rows[0]["CourtType_Id"].ToString().Trim()).Selected = true;

                ddlCaseSubject.ClearSelection();
                if (ddlCaseSubject.Items.Count > 0) ddlCaseSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubject_Id"].ToString().Trim()).Selected = true;

                ddlParty.ClearSelection();
                if (ddlParty.Items.Count > 0) ddlParty.Items.FindByValue(ds.Tables[0].Rows[0]["Party_Id"].ToString().Trim()).Selected = true;

                ddlCaseYear.ClearSelection();
                if (ddlCaseYear.Items.Count > 0) ddlCaseYear.Items.FindByText(ds.Tables[0].Rows[0]["CaseYear"].ToString().Trim()).Selected = true;

                if (ds.Tables[1].Rows.Count > 0) GrdRespondentDtl.DataSource = ds.Tables[1]; GrdRespondentDtl.DataBind();
                if (ds.Tables[2].Rows.Count > 0) GrdHearingDtl.DataSource = ds.Tables[2]; GrdHearingDtl.DataBind();
                if (ds.Tables[3].Rows.Count > 0) GrdPetiDtl.DataSource = ds.Tables[3]; GrdPetiDtl.DataBind();
                if (ds.Tables[4].Rows.Count > 0) GrdCaseDocument.DataSource = ds.Tables[4]; GrdCaseDocument.DataBind();
                if (ds.Tables[5].Rows.Count > 0) GrdDeptAdvDtl.DataSource = ds.Tables[5]; GrdDeptAdvDtl.DataBind();
                if (ds.Tables[6].Rows.Count > 0) GrdOldCaseDtl.DataSource = ds.Tables[6]; GrdOldCaseDtl.DataBind();
            }
            
        } 
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
        finally { ds.Clear(); }
    }
    protected void GrdRespondentDtl_RowCommand(object sender, GridViewCommandEventArgs e)  // Navigate on the Edit Case Detail Div.
    {
        try
        {
            if (e.CommandName == "EditResponder")
            {
                lblMsg.Text = "";
                ViewState["ResponderID"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblResponderID = (Label)row.FindControl("lblResponderID");
                Label lblResponderName = (Label)row.FindControl("lblResponderName");
                Label lblResponderNo = (Label)row.FindControl("lblResponderNo");
                Label lblDepartent = (Label)row.FindControl("lblDepartent");
                Label lblAddress = (Label)row.FindControl("lblAddress");

                txtResponderName.Text = lblResponderName.Text;
                txtResponderNo.Text = lblResponderNo.Text;
                txtDepartment.Text = lblDepartent.Text;
                txtAddress.Text = lblAddress.Text;
                ViewState["ResponderID"] = lblResponderID.Text;
                btnAddResponder.Text = "Update";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }

    }
   
    protected void btnUpdate_Click(object sender, EventArgs e) // Only Case & Petitioner INformation Update.
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnUpdate.Text == "Update" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Legal_Update_CaseRegistraion", new string[] { "Petitoner_Name", "District_ID", "CaseSubject", "WPCaseNo", "WPOrder", "Whether_WA_RP", "NodalOfficer_Name", "NodalOfficerMobileNo", "OICName", "OICMobileNo", "LastupdatedBy", "LastupdatedByIp", "Case_ID", "Action_TakenByDistrict" }
                        , new string[] { "", ddlCourtLocation.SelectedValue, "", "", "", "", "", "", "", "", ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["ID"].ToString(), txtCaseDetail.Text.Trim() }, "dataset");

                    //if (rdCaseDispose.SelectedItem.Text == "Yes")
                    //{
                    //    UploadOrderDoc();
                    //    ds = obj.ByProcedure("USP_Legal_CaseDispose", new string[] { "Case_ID", "CaseDisposeType_Id", "CaseDispose_Status", "CaseDispose_OrderNo", "CaseDispose_OrderDoc", "LastupdatedBy", "LastupdatedByIp" }
                    //        , new string[] { ViewState["ID"].ToString(), ddlDisponsType.SelectedValue, rdCaseDispose.SelectedItem.Text.Trim(), txtCaseDispose_OrderNo.Text.Trim(), ViewState["FileOrderDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    //}
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);

                        ddlCourtLocation.ClearSelection();
                        txtCaseDetail.Text = "";

                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
                BindDetails();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void lnkAddResponderDtl_Click(object sender, EventArgs e) // Navigate on the Add Responder Div.
    {
        try
        {
            lblMsg.Text = "";
            Field_AddResponder.Visible = true;
            FieldSet_CaseDetail.Visible = false; ;
            FieldViewDocument.Visible = false;
            FieldViewRespondent.Visible = false;
            Fieldset_CaseDispose.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnAddResponder_Click(object sender, EventArgs e) // Add New Responder.
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnAddResponder.Text == "Add" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Legal_Insert_ResponderName", new string[] { "Case_ID", "Respondent_Name", "RespondentNo", "Address", "Department", "CreatedBy", "CreatedByIP" }
                        , new string[] { ViewState["ID"].ToString(), txtAddResponderName.Text.Trim(), txtAddResponderNo.Text.Trim(), txtAddResponderAddress.Text.Trim(), txtAddResponderDepartment.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnAddResponder.Text == "Update" && ViewState["ResponderID"].ToString() != null && ViewState["ResponderID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Legal_Update_ResponderDtl", new string[] { "Respondent_ID", "Case_ID", "Respondent_Name", "RespondentNo", "Address", "Department", "LastupdatedBy", "LastupdatedByIp" }
                        , new string[] { ViewState["ResponderID"].ToString(), ViewState["ID"].ToString(), txtResponderName.Text.Trim(), txtResponderNo.Text.Trim(), txtAddress.Text.Trim(), txtDepartment.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);
                        txtResponderName.Text = "";
                        txtResponderNo.Text = "";
                        txtAddress.Text = "";
                        txtDepartment.Text = "";
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
                BindDetails();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    
    protected void btnSaveDoc_Click(object sender, EventArgs e) // Add & Edit Document.
    {
        if (Page.IsValid)
        {


            try
            {
                lblMsg.Text = "";
                ViewState["FileUploadDOC"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).Substring(1);
                    string[] supportedTypes = { "png", "jpg", "jpeg", "pdf", "JPG", "JPEG", "PNG", "PDF" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FileUpload1.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 500
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {

                        strFileName = FileUpload1.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "-Supplier-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/Documents/"), strFileName);
                        FileUpload1.SaveAs(path);

                        ViewState["FileUploadDOC"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }

                }
                else if (fileUpload2_EditDoc.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(fileUpload2_EditDoc.FileName).Substring(1);
                    string[] supportedTypes = { "png", "jpg", "jpeg", "pdf", "JPG", "JPEG", "PNG", "PDF" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (fileUpload2_EditDoc.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 500
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {

                        strFileName = fileUpload2_EditDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "-Supplier-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/Documents/"), strFileName);
                        fileUpload2_EditDoc.SaveAs(path);

                        ViewState["FileUploadDOC"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in(png, jpg, jpeg, pdf, JPG, JPEG, PNG, PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 500 KB \\n"; }

                if (errormsg == "")
                {
                    if (btnSaveDoc.Text == "Upload Doc")
                    {

                        ds = obj.ByProcedure("USP_Legal_Insert_AddDocument", new string[] { "Case_ID", "Doc_Name", "Doc_Path", "CreatedBy", "CreatedByIP" }
                            , new string[] { ViewState["ID"].ToString(), txtDocumentName.Text.Trim(), ViewState["FileUploadDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnSaveDoc.Text == "Edit Doc" && ViewState["DocID"].ToString().ToString() != "" && ViewState["DocID"].ToString() != null)
                    {
                        ds = obj.ByProcedure("USP_Legal_Update_CaseDocument", new string[] { "CaseDoc_ID", "Case_ID", "Doc_Name", "Doc_Path", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { ViewState["DocID"].ToString(), ViewState["ID"].ToString(), txtEditDocumentsName.Text.Trim(), ViewState["FileUploadDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);
                            txtDocumentName.Text = "";
                            ViewState["FileUploadDOC"] = "";
                        }
                        else
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                        }
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                    }
                    BindDetails();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            }
        }
    }
    protected void GrdCaseDocument_RowCommand(object sender, GridViewCommandEventArgs e)// on row command Event for Edit Document
    {
        try
        {
            ViewState["DocID"] = "";
            if (e.CommandName == "EditDocument")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDocumentID = (Label)row.FindControl("lblDocumentID");
                Label lblDocName = (Label)row.FindControl("lblDocName");
                txtEditDocumentsName.Text = lblDocName.Text;
                ViewState["DocID"] = lblDocumentID.Text;
                btnSaveDoc.Text = "Edit Doc";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#MymodalEditDocuments').modal('show')", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void ddlCourtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCourtLocation.Items.Clear();
            ds = obj.ByDataSet("select  CT.District_Id, District_Name  from tbl_LegalCourtType CT INNER Join Mst_District DM on DM.District_ID = CT.District_Id where CourtType_ID = " + ddlCourtType.SelectedValue);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtLocation.DataTextField = "District_Name";
                ddlCourtLocation.DataValueField = "District_Id";
                ddlCourtLocation.DataSource = ds;
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
            lblMsg.Text = "";
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
            ds = obj.ByDataSet("select * from tblOICMaster where OICMaster_Id = " + ddlOicName.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtOicMobileNo.Text = ds.Tables[0].Rows[0]["OICMobileNo"].ToString();
                txtOicEmailId.Text = ds.Tables[0].Rows[0]["OICEmailID"].ToString();
            }
            else
            {
                txtOicMobileNo.Text = "";
                txtOicEmailId.Text = "";

            }
        }
        catch (Exception ex)
        {

            ErrorLogCls.SendErrorToText(ex);
        }
    }
}
