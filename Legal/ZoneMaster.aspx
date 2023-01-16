<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="ZoneMaster.aspx.cs" Inherits="Legal_ZoneMaster" %>

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
                        Zone Master
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    Zone Name
                                            <span style="color: red;"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                    ErrorMessage="Enter Zone Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtZoneName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtZoneName" runat="server" placeholder="Enter Zone Name" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    Zone Code
                                            <span style="color: red;"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator ID="rfvcode" ValidationGroup="Save"
                                                    ErrorMessage="Enter Zone Code." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtZoneCode" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtZoneCode" runat="server" placeholder="Enter Zone Code" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-block" ValidationGroup="Save" OnClick="btnSave_Click"/>
                                            </div>
                                            <div class="col-md-6">
                                                <a href="ZoneMaster.aspx" class="btn btn-default btn-block">Clear</a>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Details</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdZoneMaster" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" DataKeyNames="Zone_ID" EmptyDataText="NO RECORD FOUND" OnRowCommand="GrdZoneMaster_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblZoneID" runat="server" Text='<%# Eval("Zone_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Zone Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblZoneName" runat="server" Text='<%# Eval("ZoneName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Zone Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblZoneCode" runat="server" Text='<%# Eval("ZoneCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Zone_ID") %>' CommandName="EditDetails" ToolTip="Edit" CssClass="btn btn-primary"><i class="fa fa-edit"></i></asp:LinkButton>
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
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

