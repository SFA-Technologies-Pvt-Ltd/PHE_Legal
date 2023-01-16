<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="zonetocircle.aspx.cs" Inherits="Legal_zonetocircle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
        <section class="content">
            <div class="container-fluid">
                <div class="box">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="card">
                            <div class="card-header">
                                Zone To Circle Mapping
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Zone<span style="color: red;"><b> *</b></span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvZone" ValidationGroup="Save"
                                                        ErrorMessage="Select Zone" Text="<i class='fa fa-exclamation-circle' title='Select Zone'></i>"
                                                        ControlToValidate="ddlCircle" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlzone" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Circle<span style="color: red;"><b> *</b></span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCirlce" ValidationGroup="Save"
                                                        ErrorMessage="Select Circle" Text="<i class='fa fa-exclamation-circle' title='Select Circle'></i>"
                                                        ControlToValidate="ddlCircle" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlCircle" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Circle Code<span style="color: red;"><b> *</b></span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCirlceCode" ValidationGroup="Save"
                                                        ErrorMessage="Enter Cirlce Code" Text="<i class='fa fa-exclamation-circle' title='Select Circle'></i>"
                                                        ControlToValidate="ddlCircle" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" ID="txtCircleCode" CssClass="form-control" MaxLength="20" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3 pt-3">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6" style="margin-top: 1rem;">
                                                        <asp:Button runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" />
                                                    </div>
                                                    <div class="col-md-6" style="margin-top: 1rem;">
                                                        <a href="zonetocircle.aspx" class="btn btn-default btn-block">Clear</a>
                                                    </div>
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
                                                <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                <asp:GridView ID="GrdZoneCircle" AutoGenerateColumns="false" runat="server" DataKeyNames=""
                                                    CssClass="table table-bordered table-hover" PageSize="10" AllowPaging="true">
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                    <Columns>
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

