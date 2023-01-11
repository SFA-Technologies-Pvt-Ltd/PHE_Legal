<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="ImplementationAuthorityMst.aspx.cs" Inherits="Legal_ImplementationAuthorityMst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .pt-4 {
            padding-top: 1.9rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Implementation Authority Master
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Enter Details</legend>
                            <div class="row">
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>
                                            Name<span style="color: red;"><b> *</b></span></label>
                                        <asp:RequiredFieldValidator ID="RfvAuthorityName" ValidationGroup="Save"
                                            ErrorMessage="Enter Authority Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtAuthorityName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtAuthorityName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>
                                            Mobile No.<span style="color: red;"><b> *</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfvMobileNo" ValidationGroup="Save"
                                            ErrorMessage="Enter Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtMobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>
                                            Email-ID<span style="color: red;"><b> *</b></span></label>
                                        <asp:RequiredFieldValidator ID="RfvEmailID" ValidationGroup="Save"
                                            ErrorMessage="Enter Email-ID." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtEmailID" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>
                                            Designation<span style="color: red;"><b> *</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfvDesignation" ValidationGroup="Save"
                                            ErrorMessage="Select Designation." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDesignation" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Location<span style="color: red;"><b> *</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfvLocation" ValidationGroup="Save"
                                            ErrorMessage="Select Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlLocation" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 pt-4">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="Save" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="ImplementationAuthorityMst.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Detail</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">

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

