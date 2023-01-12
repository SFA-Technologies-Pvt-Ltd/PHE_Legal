﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="ConcludedwpReport.aspx.cs" Inherits="mis_Legal_ConcludedwpReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        td, th {
            border: 1px solid #dddddd;
            text-align: center;
            padding: 8px;
            font-weight: 400;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Concluded WP Report
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search WP Case</legend>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            From Date<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                ErrorMessage="Enter From Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtFromdate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            प्रारंभिक तिथि</label>
                                        <asp:TextBox ID="txtFromdate" runat="server" CssClass="form-control disableFuturedate" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY" AutoComplete="off"></asp:TextBox>
                                    </div>
                                
                                </div>
                          
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        To Date<span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RfvEndDate" ValidationGroup="Save"
                                            ErrorMessage="Enter End Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtTodate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator><br />
                                        अंतिम तिथि</label>
                                    <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control disableFuturedate" data-date-end-date="0d" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding:5.5% 0 0 0">
                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                    <div class="col-md-5">
                                        <a href="ConcludedwpReport.aspx" class="btn btn-default btn-block">Clear</a>
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
                                        <asp:GridView ID="GrdConcludeReport" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="Case_ID"  OnPageIndexChanging="GrdConcludeReport_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblCaseId" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WP Case No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWpCaseNo" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              <%--   <asp:TemplateField HeaderText="Case Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseYear" runat="server" Text='<%# Eval("WPCaseYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Petitioner Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("Petitoner_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Case Disposal Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisposedate" runat="server" Text='<%# Eval("CaseDisposeDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Case Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCasesubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseStatus" runat="server" Font-Bold="true" ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Text='<%# Eval("CaseStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="WA<br /> (Writ Appeal">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnWA" runat="server" CssClass="btn btn-primary" CommandName="Appeal" CommandArgument='<%# Eval("Case_ID") %>' ToolTip="Appeal"><i class="fa fa-pen"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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



