<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="EditCaseDetail.aspx.cs" Inherits="Legal_EditCaseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 15px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <asp:ValidationSummary ID="VDS2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddResponder" />
    <asp:ValidationSummary ID="VDS3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CaseDtl" />
    <asp:ValidationSummary ID="VDS4" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CaseDispose" />
    <%-- Start Here Using POPUP Modal for Edit Responder --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Case Details</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Add Responder</legend>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Responder Name</label>
                                        <asp:TextBox ID="txtResponderName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Responder Mobile No.</label>
                                        <asp:TextBox ID="txtResponderNo" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Address</label>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Update" ID="btnYes" Style="margin-top: 20px; width: 80px;" OnClick="btnAddResponder_Click" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- End Here Using POPUP Modal for Edit Responder --%>
    <%-- Start Here Edit Documents  ---%>
    <div class="modal fade" id="MymodalEditDocuments" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 50%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left">Documents</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Edit Documents</legend>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Documents Name </label>
                                        <span style="color: red;"><b>*</b></span>

                                        <asp:TextBox ID="txtEditDocumentsName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Document Upload</label>
                                        <asp:FileUpload ID="fileUpload2_EditDoc" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Edit Doc" ID="btnEditDocuments" Style="margin-top: 20px; width: 80px;" OnClick="btnSaveDoc_Click" />
                        <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- End Here Edit Documnets --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Edit Case Detail
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">


                                <%-- Start Here Bind Case && Petitioner Detail --%>
                                <fieldset id="FieldSet_CaseDetail" runat="server" visible="true">
                                    <legend>Case Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case No.</label>
                                                <asp:Label ID="lblCaseNo" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Year</label>
                                                <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Court Name</label>
                                                <asp:DropDownList ID="ddlCourtType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourtType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Court Location</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvDistrict" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Court Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCourtLocation" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCourtLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Case Subject</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvCaseSubject" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCaseSubject" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCaseSubject" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseSubject_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Case Sub Subject</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvtxtOrder" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Case Subject" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCaseSubSubject" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCaseSubSubject" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Party Name</label>
                                                <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Oic Name</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvOic_Name" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Enter OIC Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlOicName" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlOicName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOicName_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Oic Mobile Name</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:TextBox ID="txtOicMobileNo" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Oic Email-ID</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:TextBox ID="txtOicEmailId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>High Priority Case</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvHighpriortiy" ValidationGroup="Save"
                                                    ErrorMessage="Select High Priority Case." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlHighprioritycase" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlHighprioritycase" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    Case Detail</label><span style="color: red;"><b> *</b></span><br />
                                                <asp:RequiredFieldValidator ID="RFVActionByDistrict" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Enter Case Detail." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtCaseDetail" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCaseDetail" runat="server" TextMode="MultiLine" CssClass="form-control" AutoComplete="off" MaxLength="250"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-3" style="padding-top: 3%;">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="CaseDtl" Text="Update" OnClick="btnUpdate_Click" />
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--Start Here Petitioner Detail --%>
                                  <fieldset id="FieldViewPetiDtl" runat="server" visible="true">
                                    <legend>Respondent Details</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdPetiDtl" runat="server" CssClass="table table-bordered" DataKeyNames="Petitioner_Id" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblPetitionerID" runat="server" Text='<%# Eval("Petitioner_Id") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Petitioner Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetitionerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDesignation_Id" runat="server" Text='<%# Eval("Designation_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Petitioner Mobile No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPetitionermobileNo" runat="server" Text='<%# Eval("PetitionerMobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("PetitionerAddress") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Petitioner_Id") %>' CommandName="EditResponder" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--End Here Petitioner Detail --%>
                                <%--Start Here DeptAdv Detail --%>
                                <fieldset id="FieldViewDeptAdvDtl" runat="server" visible="true">
                                    <legend>Department Advocate Details</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdDeptAdvDtl" runat="server" CssClass="table table-bordered text-center" DataKeyNames="DeptAdv_Id" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblPetitionerID" runat="server" Text='<%# Eval("DeptAdv_Id") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Advocate Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdvocateName" runat="server" Text='<%# Eval("DeptAdvName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("DeptAdvMobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("DeptAdv_Id") %>' CommandName="EditResponder" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--End Here DeptAdv Detail --%>
                                <%-- Start Here Bind Responder Detail --%>
                                <fieldset id="FieldViewRespondent" runat="server" visible="true">
                                    <legend>Respondent Details</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdRespondentDtl" runat="server" CssClass="table table-bordered" DataKeyNames="Respondent_ID" AutoGenerateColumns="false" OnRowCommand="GrdRespondentDtl_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblResponderID" runat="server" Text='<%# Eval("Respondent_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfficetype" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblOfficetype_ID" runat="server" Text='<%# Eval("Officetype_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                                <asp:Label ID="lblOffice_Id" runat="server" Text='<%# Eval("Office_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Respondent Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("RespondentName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Responder Mobile No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRespondentNo" runat="server" Text='<%# Eval("RespondentMobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepartent" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Respondent_ID") %>' CommandName="EditResponder" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- End Here Bind Responder Detail --%>
                                <%--Start Here Hearing Detail --%>
                                <fieldset id="FieldViewHearingDtl" runat="server" visible="true">
                                    <legend>Hearing Details</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdHearingDtl" runat="server" CssClass="table table-bordered" DataKeyNames="NextHearing_ID" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblPetitionerID" runat="server" Text='<%# Eval("NextHearing_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hearing Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHearingDate" runat="server" Text='<%# Eval("NextHearingDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyperHearingDoc" runat="server" Target="_blank" Enabled='<%# Eval("HearingDoc") %>' NavigateUrl='<%# "../Legal/HearingDoc/" + Eval("HearingDoc") %>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("NextHearing_ID") %>' CommandName="EditResponder" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--End Here Hearing Detail --%>
                                <%-- Start Here Case Dispose Detail --%>
                                <fieldset id="Fieldset_CaseDispose" runat="server">
                                    <legend>Case Dispose</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:DetailsView ID="dtlCaseDispose" runat="server" BorderWidth="2px" CssClass="table" AutoGenerateRows="false">
                                                <Fields>
                                                    <asp:BoundField HeaderText="Case Dispose Status" DataField="CaseDispose_Status" HeaderStyle-Width="25%" />
                                                    <asp:BoundField HeaderText="Case Dispose Type" DataField="CaseDisposeType" HeaderStyle-Width="25%" />
                                                    <asp:BoundField HeaderText=" Order No." DataField="CaseDispose_OrderNo" HeaderStyle-Width="25%" />
                                                    <asp:TemplateField HeaderText="Order Document" HeaderStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperlinkOrderDoc" runat="server" Target="_blank" NavigateUrl='<%# "UploadOrderDoc/" + Eval("CaseDispose_OrderDoc") %>' CssClass="fa fa-eye">View</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Fields>
                                            </asp:DetailsView>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- End Here Case Dispose Detail --%>

                                <%-- Start Here Bind Documnet Detail --%>
                                <fieldset id="FieldViewDocument" runat="server" visible="true">
                                    <legend>Case Document</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Document Name</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="RfvAddDocumnet" ValidationGroup="Save"
                                                    ErrorMessage="Enter Document Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtDocumentName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:TextBox ID="txtDocumentName" runat="server" CssClass="form-control" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Document Upload</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="RfvUploadDoc" ValidationGroup="Save"
                                                    ErrorMessage="Upload Document." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="FileUpload1" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="padding-top: 3%;">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnSaveDoc" runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" Text="Upload Doc" OnClick="btnSaveDoc_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row pt-4">
                                        <div class="col-md-9">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdCaseDocument" runat="server" CssClass="table table-bordered text-center" AutoGenerateColumns="false" DataKeyNames="CaseDoc_ID" OnRowCommand="GrdCaseDocument_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblDocumentID" runat="server" Text='<%# Eval("CaseDoc_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("Doc_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyperViewDoc" runat="server" CssClass="fa fa-eye" Target="_blank" Enabled='<%#  Eval("Doc_Path").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/AddNewCaseCourtDoc/" +  Eval("Doc_Path") %>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditDoc" runat="server" CssClass="fa fa-edit" CommandName="EditDocument" ToolTip="Edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- End Here Bind Document Detail --%>
                                 <%--Start Here DeptAdv Detail --%>
                                <fieldset id="FieldViewOldCaseDtl" runat="server" visible="true">
                                    <legend>Old Case Details</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdOldCaseDtl" runat="server" CssClass="table table-bordered text-center" DataKeyNames="Id" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lbloldCaseID" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Old Case No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOldCaseNo" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Case Year">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOldCaseYear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Case type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOldCasetype" runat="server" Text='<%# Eval("CaseType") %>'></asp:Label>
                                                                <asp:Label ID="lblOldCasetype_Id" runat="server" Text='<%# Eval("Casetype_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Court">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOldCourt" runat="server" Text='<%# Eval("Court") %>'></asp:Label> 
                                                                <asp:Label ID="lblOldCourt_Id" runat="server" Text='<%# Eval("CourtType_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Court Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOldCourtLoca" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label> 
                                                                 <asp:Label ID="lblOldCourtLoca_Id" runat="server" Text='<%# Eval("CourtDistLoca_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOldDocName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hypOldCaseDtl" runat="server" Enabled='<%# Eval("DocLink").ToString() == "" ? false : true %>' Target="_blank" NavigateUrl='<%# "../Legal/OldCaseDocument" +  Eval("DocLink") %>' CssClass="fa fa-eye"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <%-- <asp:TemplateField HeaderText="कार्यवाही का विवरण">
                                                            <ItemTemplate>
                                                                <asp:Label ID="hypOldProssDtl" runat="server" Enabled='<%# Eval("DocLink").ToString() == "" ? false : true %>' Target="_blank" NavigateUrl='<%# "../Legal/OldCaseDocument" +  Eval("DocLink") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="निर्णय">
                                                            <ItemTemplate>
                                                                <asp:Label ID="hypOldDecision" runat="server" Enabled='<%# Eval("DocLink").ToString() == "" ? false : true %>' Target="_blank" NavigateUrl='<%# "../Legal/OldCaseDocument" +  Eval("DocLink") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="अन्य">
                                                            <ItemTemplate>
                                                                <asp:Label ID="hypOldOther" runat="server" Enabled='<%# Eval("DocLink").ToString() == "" ? false : true %>' Target="_blank" NavigateUrl='<%# "../Legal/OldCaseDocument" +  Eval("DocLink") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="EditResponder" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--End Here DeptAdv Detail --%>
                                <%---End Here For Edit Case Details ---%>
                                <%---Start Here For Add Responder Details ---%>
                                <fieldset id="Field_AddResponder" runat="server" visible="false">
                                    <legend>Add Responder</legend>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Responder Name</label>
                                                <span style="color: red;"><b>*</b></span>

                                                <asp:RequiredFieldValidator ID="RFVtxtAddDocumentName" ValidationGroup="AddResponder"
                                                    ErrorMessage="Enter Responder Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtAddResponderName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtAddResponderName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Responder Mobile No.</label>
                                                <span style="color: red;"><b>*</b></span>

                                                <asp:RequiredFieldValidator ID="RfvtxtAddResponderNo" ValidationGroup="AddResponder"
                                                    ErrorMessage="Enter Responder No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtAddResponderNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtAddResponderNo" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Department</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvtxtAddResponderDepartment" ValidationGroup="AddResponder"
                                                    ErrorMessage="Enter Department Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtAddResponderDepartment" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtAddResponderDepartment" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Address</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvtxtAddResponderAddress" ValidationGroup="AddResponder"
                                                    ErrorMessage="Enter Address Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtAddResponderAddress" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtAddResponderAddress" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="padding-top: 3%;">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnAddResponder" Text="Add" runat="server" ValidationGroup="AddResponder" CssClass="btn btn-primary btn-block" OnClick="btnAddResponder_Click" />
                                                </div>
                                                <%--  <div class="col-md-6">
                                            <a href="EditCaseDetail.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%---End Here For Add Responder Details ---%>
                                <%---Start Here For Add && Edit Documents ---%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script type="text/javascript">
        function chcode() { // for only Character
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return false
            else if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true
        }

        function NumberOnly() {
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return true
            return false
        }
    </script>
</asp:Content>

