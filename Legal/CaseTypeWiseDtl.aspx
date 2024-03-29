﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CaseTypeWiseDtl.aspx.cs" Inherits="Legal_CaseTypeWiseDtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">View Case Detail</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Case Details</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case No.</label>
                                        <asp:TextBox ID="txtCaseno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Petitioner Name</label>
                                        <asp:TextBox ID="txtPetitionerName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Subject</label>
                                        <asp:TextBox ID="txtCasesubject" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Respondent Type</label>
                                        <asp:TextBox ID="txtRespondertype" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Court Name</label>
                                        <asp:TextBox ID="txtCourtName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Next Hearing Date</label>
                                        <asp:TextBox ID="txtNextHearingDate" runat="server" CssClass="form-control" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Case Type</label>
                                        <asp:TextBox ID="txtCasetype" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Name</label>
                                        <asp:TextBox ID="txtRespondentName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Mobile No.</label>
                                        <asp:TextBox ID="txtRespondentMobileno" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Email-ID</label>
                                        <asp:TextBox ID="txtRespondentEmailID" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Name</label>
                                        <asp:TextBox ID="txtNodalName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Mobile No.</label>
                                        <asp:TextBox ID="txtNodalMobile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Email-ID</label>
                                        <asp:TextBox ID="txtNodalEmailID" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Name</label>
                                        <asp:TextBox ID="txtOICName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Mobile No.</label>
                                        <asp:TextBox ID="txtOICMObile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Email-ID</label>
                                        <asp:TextBox ID="txtOICEmail" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Name</label>
                                        <asp:TextBox ID="txtAdvocatename" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Mobile No</label>
                                        <asp:TextBox ID="txtAdvocatemobile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Email-Id</label>
                                        <asp:TextBox ID="txtAdvocateEmailID" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Case Detail</label>
                                        <asp:TextBox ID="txtCaseDtl" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <%-- <asp:Button runat="server" CssClass="btn btn-success" Text="OK" ID="btnYes" Style="margin-top: 20px; width: 80px;" />--%>
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="Close" data-dismiss="modal" Style="margin-top: 20px; width: 60px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Case Type Wise Detail
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Type</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                            ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseType" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">WP Case</asp:ListItem>
                                            <asp:ListItem Value="2">Contempt Case</asp:ListItem>
                                            <asp:ListItem Value="3">WA Case</asp:ListItem>
                                            <asp:ListItem Value="4">RP Case</asp:ListItem>
                                            <asp:ListItem Value="4">SLP Case</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" style="margin-top: 3%;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="CaseTypeWiseDtl.aspx" class="btn btn-default btn-block">Clear</a>
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
                                        <asp:GridView ID="grdCaseTypedtl" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grdCaseTypedtl_RowCommand" DataKeyNames="ID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                        <asp:Label ID="LabelOICName" runat="server" Text='<%# Eval("OICName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICMObile" runat="server" Text='<%# Eval("OICMobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICEmail" runat="server" Text='<%# Eval("OICEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalName" runat="server" Text='<%# Eval("NodalName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalMobile" runat="server" Text='<%# Eval("NodalMobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalEmail" runat="server" Text='<%# Eval("NodalEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateName" runat="server" Text='<%# Eval("AdvocateName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateMobile" runat="server" Text='<%# Eval("AdvocateMobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateEmail" runat="server" Text='<%# Eval("AdvocateEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelHearingDate" runat="server" Text='<%# Eval("NextHearingDate") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelRespondertype" runat="server" Text='<%# Eval("Respondertype") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCaseDetail" runat="server" Text='<%# Eval("CaseDetail") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCasetype" runat="server" Text='<%# Eval("CaseType") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Petitioner Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetitionerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Court Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("CourtName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' ForeColor='<%# Eval("Status").ToString() == "Pending" ? System.Drawing.Color.Red : System.Drawing.Color.Green%>' Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-primary" CommandName="ViewDtl" CommandArgument='<%# Eval("ID") %>' ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
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

