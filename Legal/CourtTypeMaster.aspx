﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CourtTypeMaster.aspx.cs" Inherits="Legal_CourtTypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />
    <style>
        /*.datepicker tbody {
            background-color: #ecfce6 !important;
            color: black;
        }

        .datepicker th {
            background-color: #608640 !important;
        }*/

        .label-orange {
            background-color: #f5ac45;
        }

        .label {
            display: inline;
            padding: 0.2em 0.6em 0.3em;
            font-size: 80%;
            font-weight: 700;
            line-height: 1;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: 0.25em;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #066205;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
            margin-top: 4%;
        }

        a.btn.btn-default.buttons-print {
            background: #1e79e9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
            margin-top: 4%;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }
    </style>
    <style>
        .btn-danger {
            background-color: #cf7b83;
        }

        .btn-info {
            background-color: #548ac5;
        }

        .btn-primary {
            background-color: #548ac5;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Confirmation</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <%--<img src="../assets/images/question-circle.png" width="30" />--%>&nbsp;&nbsp;
                           <i class="fa fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
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
                                Court Type Master
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Court Type<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="Save"
                                                        ErrorMessage="Select Court Type" Text="<i class='fa fa-exclamation-circle' title='Select Court Type'></i>"
                                                        ControlToValidate="ddlCourtType" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlCourtType" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Location<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCourtLocation" ValidationGroup="Save"
                                                        ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Court Location'></i>"
                                                        ControlToValidate="ddlCourtlocation" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlCourtlocation"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--   <div class="col-md-3" id="otherDiv" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>Others</label><span style="color: red;"> *</span>
                                                <asp:RequiredFieldValidator ID="rfvother" ValidationGroup="Save"
                                                    ErrorMessage="Enter Other Location" Text="<i class='fa fa-exclamation-circle' title='Select Court Location'></i>"
                                                    ControlToValidate="txtOther" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" CssClass="form-control select2" ID="txtOther" MaxLength="60" AutoComplete="off" onkeypress="return lettersOnly();" onkeyup="javascript:capFirst(this);"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                        <div class="col-md-3" style="padding-top: 2rem! important;">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Button runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <a href="CourtTypeMaster.aspx" class="btn btn-default btn-block">Clear</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <legend>Detail</legend>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            <asp:GridView Width="100%" ID="grdCourtType" AutoGenerateColumns="false" runat="server"
                                                CssClass="datatable table table-bordered table-hover" OnPageIndexChanging="grdCaseSubject_PageIndexChanging" OnRowCommand="grdCourtType_RowCommand" DataKeyNames="CourtType_ID" EmptyDataText="NO RECORD FOUND">
                                                <RowStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            <asp:Label ID="CaseSubjectID" runat="server" Text='<%#Eval("CourtType_ID") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Court Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCourName" runat="server" Text='<%#Eval("CourtTypeName") %>'></asp:Label>
                                                            <asp:Label ID="lblCourtNameID" runat="server" Text='<%#Eval("CourtName_ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllocation" runat="server" Text='<%#Eval("District_Name") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnDistrictID" Value='<%#Eval("District_Id") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Other Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOtherlocation" runat="server" Text='<%#Eval("OtherLocation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("CourtType_ID") %>' CommandName="EditDetails" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("CourtType_ID") %>'
                                                                OnClientClick="return confirm('Are you sure you want to delete this record?');" ToolTip="Delete" CssClass=""><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
    <%--<script src="../DataTable_CssJs/jquery.js"></script>--%>
    <script src="../DataTable_CssJs/jquery.dataTables.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.bootstrap.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.buttons.min.js"></script>
    <script src="../DataTable_CssJs/buttons.flash.min.js"></script>
    <script src="../DataTable_CssJs/jszip.min.js"></script>
    <script src="../DataTable_CssJs/pdfmake.min.js"></script>
    <script src="../DataTable_CssJs/vfs_fonts.js"></script>
    <script src="../DataTable_CssJs/buttons.html5.min.js"></script>
    <script src="../DataTable_CssJs/buttons.print.min.js"></script>
    <script src="../DataTable_CssJs/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        $('.datatable').DataTable({
            paging: true,
            PageLength: 15,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>
    <script type="text/javascript">
        function NumberOnly() { //only Numeric required.
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return true
            return false
        }

        function capFirst(cpt) { //only Capital First.
            cpt.value = cpt.value[0].toUpperCase() + cpt.value.substring(1);
        }

        function chcode() { // Only English or Hindi Required
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return false
            else if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true
        }

        function lettersOnly() { // Only English Letter Allow.
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true;
            else
                return false;
        }
    </script>
    <script>
        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }
            if (Page_IsValid) {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>

</asp:Content>

