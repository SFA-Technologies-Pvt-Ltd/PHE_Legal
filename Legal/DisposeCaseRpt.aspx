<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="DisposeCaseRpt.aspx.cs" Inherits="Legal_DisposeCaseRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Disposal Case Detail
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Disposal Type</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                            ErrorMessage="Select Case Dispose type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDisposetype" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDisposetype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDisposetype_SelectedIndexChanged">   
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-3" id="ComplianceSt_Div" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>
                                                    Compliance Status
                                                </label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="CaseDispose" Enabled="false"
                                                    ErrorMessage="Select Compliance" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCompliaceSt" Display="Dynamic" InitialValue="0" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCompliaceSt" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                    <asp:ListItem Value="3">Pending</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case type</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                            ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseType" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding: 3.5% 0 0 0">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" Text="Search" ValidationGroup="Save" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-4">
                                            <a href="DisposeCaseRpt.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Report</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdSubjectWiseCasedtl" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grdSubjectWiseCasedtl_RowCommand" DataKeyNames="Case_ID" EmptyDataText="NO RECORD FOUND" OnPageIndexChanging="grdSubjectWiseCasedtl_PageIndexChanging" AllowPaging="true" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Case Year" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseYear" runat="server" Text='<%# Eval("CaseYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Subject" Visible="false" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>' Visible="false"></asp:Label>
                                                    <%--    <asp:Label ID="LabelOICName" runat="server" Text='<%# Eval("petiAdvocateName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICMObile" runat="server" Text='<%# Eval("petiAdvocateMobile") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICEmail" runat="server" Text='<%# Eval("PetiAdvocateEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalName" runat="server" Text='<%# Eval("NodalOfficer_Name") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalMobile" runat="server" Text='<%# Eval("NodalOfficerMobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalEmail" runat="server" Text='<%# Eval("NodalOfficerEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateName" runat="server" Text='<%# Eval("DeptAdvocateName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateMobile" runat="server" Text='<%# Eval("DeptAdvocateMobileNO") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateEmail" runat="server" Text='<%# Eval("DeptAdvocateEmailId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelRespondertype" runat="server" Text='<%# Eval("RespondertypeName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCaseDetail" runat="server" Text='<%# Eval("CaseDetail") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCasetype" runat="server" Text='<%# Eval("Casetype_Name") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("Respondent_Name") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblRespondentMobileNo" runat="server" Text='<%# Eval("RespondentNo") %>' Visible="false"></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Petitioner Name" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("Petitoner_Name") %>'></asp:Label>--%>
                                                         <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetitonerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Court Name" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("CourtTypeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseStatus" runat="server" Text='<%# Eval("CaseStatus") %>' Font-Bold="true" ForeColor='<%# Eval("CaseStatus").ToString() == "Dispose" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server" CssClass="" CommandName="ViewDtl" CommandArgument='<%# Eval("Case_ID") %>' ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

