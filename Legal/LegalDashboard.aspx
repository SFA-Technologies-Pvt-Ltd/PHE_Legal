﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="LegalDashboard.aspx.cs" Inherits="mis_Legal_LegalDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     
    <style>
        .LegalImage {
            background-image: url("../image/Legal%203.png");
            /*background-position: center;
            background-repeat: no-repeat;
            background-size: 70px 70px;*/
            height: 70px;
            width: 70px;
            opacity: 1;
        }

        .Libraimage {
            height: 50px;
            width: 50px;
            display: inline;
            align-content: center;
            margin: 0 0 0 34px;
        }

        .boxshape {
            height: 110px;
            /*width: 66%;*/
            width: 100%;
            border-top-right-radius: 34px;
            /*border-top-left-radius: 44px;
            border-bottom-left-radius: 44px;*/
            border-top-left-radius: 55px;
            border-bottom-left-radius: 55px;
            border-radius: -50px 0px;
            opacity: 1;
        }

        .circle {
            z-index: 50;
            position: absolute;
            border-radius: 100%;
            height: 90px;
            width: 97px;
            border: 3px solid white;
            box-shadow: inset 0 0 8px black;
            margin: 5px 5px 10px 9px;
            background-color: #140d0d;
        }

        .insideBox {
            border-radius: 100%;
            margin: 5px 7px 2px 6px;
            height: 75px;
            width: 80px;
            text-align: center;
            font-weight: 700;
            color: white;
            font-family: sans-serif;
            line-height: 1;
            padding-top: 10px;
        }

        .lableBox {
            /*position: relative;
            height: 35px;
            width: 147%;
            border: 2px solid white;
            border-top-right-radius: 15px;
            margin: -53px 28px 28px 83px;
            background-color: #140d0d;
            border-left-color: white;
            border-left: none;*/
            COLOR: white;
            /*font-size: 20px;*/
            font-weight: 900;
            display: inline;
            position: absolute;
            border: solid 3px white;
            padding: 1px 21px;
            top: 34%;
            right: 44%;
            background-color: black;
            border-top-right-radius: 15px;
        }

        .buttonDiv {
            /*height: 26px;
            width: 110%;
            background-color: white;
            margin: -26px 0px 0px 208px;
            border-radius: 30%;*/
            position: absolute;
            display: inline;
            right: 22px;
            bottom: 9px;
        }

        .Buttons {
            border-radius: 21%;
            background-color: white;
        }

        span {
            font-size: 12px;
        }

        .Heading {
            font-size: 18px;
            font-weight: bold;
            color: blue;
        }

        .Space {
            margin-bottom: 40px;
        }

        .NewCaseCourtImage {
            width: 50%;
            display: inline;
            position: relative;
            left: 100px;
        }
    </style>

    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>


    <%-- <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                                <script type="text/javascript">
                                    google.charts.load('current', { 'packages': ['corechart'] });
                                    google.charts.setOnLoadCallback(drawChart);

                                    function drawChart() {

                                        var data = google.visualization.arrayToDataTable([
                                          ['Task', 'Hours per Day'],
                                          ['Work', 11],
                                          ['Eat', 2],
                                          ['Commute', 2],
                                          ['Watch TV', 2],
                                          ['Sleep', 7]
                                        ]);

                                        var options = {
                                            title: 'My Daily Activities'
                                        };

                                        var chart = new google.visualization.PieChart(document.getElementById('piechart'));

                                        chart.draw(data, options);
                                    }
                                </script>

          graph                      <div id="piechart" style="width: 900px; height: 500px;"></div>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 80%; position:absolute; left:10%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">High Priority Case Details</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <div class="container-fluid">
                            <fieldset>
                                <legend>High Priority Case</legend>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdHighpriorityCase" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("CaseNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Petitioner Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetionerName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Court Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("CourtTypeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="High Priority Case" DataField="HighPrirtyCaseSts" />
                                                    <asp:BoundField HeaderText="High Priority Case" DataField="Casetype_Name" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <%--  <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" />--%>
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
        <section class="content-header">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card">
                    <div class="card-header" style="text-align: center;">
                        <span style="font-size: 18px; color: #e5e5e5">Dashboard</span>
                    </div>
                    <div class="card-body" style="opacity: 1;">

                        <%-- backgRound image remove from here on date 12/12/22 
                            <div class="row">
                                <div class="col-md-5"></div>
                                <div class="col-md-3">
                                    <img src="../image/Libra.png" class="Libraimage" />
                                </div>
                            </div>--%>
                         <div class="row Space">
                            <div class="col-md-12">
                                <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">HIGH PRIORITY CASE :</span>
                                <asp:LinkButton ID="btnHighPriorityCase" runat="server" CssClass="btn-sm label label-warning" OnClick="btnHighPriorityCase_Click">TOTAL Case<span id="spnhighpriorityCase" runat="server" style="font-weight:bold;"></span></asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ">
                                <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">UPCOMING HEARING :</span>

                            </div>
                            <div class="col-md-12" style="margin-top: 10px;">
                                <marquee direction="left"> <span id="spnHearing" runat="server" style="font-size:18px; color:red; font-weight:800;"></span></marquee>
                            </div>
                        </div>                        
                      
                        <div class="row Space">
                            <div class="col-md-6">
                                <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">COURT WISE :
                                      <asp:Label ID="lblCaseCount" runat="server" CssClass="Heading" />
                                                                                                      </span>
                            </div>
                            <div class="col-md-6">
                                <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">CASE WISE :(TOTAL CASE : <span id="CasetypeCountno" runat="server" class="Heading"></span> No's)</span>
                            </div>
                        </div>
                        <div class="row Space">
                            <%-- <div class="col-md-1"></div>--%>
                            <div class="col-md-6">
                                <%--<img src="../image/Courtwise.png"  class="NewCaseCourtImage"/>--%>
                              <%--  <img src="../image/New1.png" class="NewCaseCourtImage"/>--%>
                               <div id="sbid" runat="server"></div>
                                
                            </div>

                            <%-- <div class="col-md-1"></div>--%>
                            <div class="col-md-6">
                              <%--  <img src="../image/New2.png" class="NewCaseCourtImage" />--%>
                                <%--<img src="../image/CaseWise.png" class="NewCaseCourtImage" />--%>
                                <div id="CasetypeCountID" runat="server"></div>
                            </div>
                        </div>

                        
                        <%--<div class="row Space">
                                <div class="col-md-12">
                                    <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">OFFICE WISE :</span>
                                </div>
                            </div>--%>
                        <div class="row Space">
                            <div class="col-md-12">
                                <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">RESPONDENT WISE DETAILS :</span>
                            </div>
                        </div>
                        <div class="row Space">
                            <div class="col-6 col-md-4">
                                <div class="boxshape" style="background-color: #1fa6ad; border: 2px solid #cd7a32;">
                                    <div class="circle">
                                        <div class="insideBox" style="border: 2px solid #7DF9FF;">
                                            <span>MP<br />GOVT<br />
                                                CASES</span>

                                        </div>
                                    </div>
                                    <div class="lableBox">
                                        <asp:Label ID="lblPPCase" runat="server"></asp:Label>
                                    </div>
                                    <div class="buttonDiv">
                                        <asp:Button ID="btnPPCase" runat="server" CssClass="Buttons" Text="View Detail" OnClientClick="SetTarget();" OnClick="btnPPCase_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 col-md-4">
                                <div class="boxshape" style="background-color: yellowgreen; border: 2px solid red;">
                                    <div class="circle">
                                        <div class="insideBox" style="border: 2px solid green;">
                                            <span>ENC<br />
                                                CASES</span>
                                        </div>
                                    </div>
                                    <div class="lableBox">
                                        <asp:Label ID="lblDPICase" runat="server"></asp:Label>
                                    </div>
                                    <div class="buttonDiv">
                                        <asp:Button ID="btnDPICase" runat="server" CssClass="Buttons" Text="View Detail" OnClientClick="SetTarget();" OnClick="btnDPICase_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 col-md-4">
                                <div class="boxshape" style="background-color: #e74c4c; border: 2px solid black;">
                                    <div class="circle">
                                        <div class="insideBox" style="border: 2px solid #810101;">
                                            <span style="text-align: center; line-height: normal;">ZONE<br />
                                                CASES</span>
                                        </div>
                                    </div>
                                    <div class="lableBox">
                                        <asp:Label ID="lblJDCases" runat="server"></asp:Label>
                                    </div>
                                    <div class="buttonDiv">
                                        <asp:Button ID="btnJDCases" runat="server" CssClass="Buttons" Text="View Detail" OnClientClick="SetTarget();" OnClick="btnJDCases_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row Space">
                            <div class="col-6 col-md-4">
                                <div class="boxshape" style="background-color: #FA2B17; border: 2px solid #cd7a32;">
                                    <div class="circle">
                                        <div class="insideBox" style="border: 2px solid #FA2B17;">
                                            <span>CIRCLE<br />
                                                CASES</span>
                                        </div>
                                    </div>
                                    <div class="lableBox">
                                        <asp:Label ID="lblDEOCases" runat="server"></asp:Label>
                                    </div>
                                    <div class="buttonDiv">
                                        <asp:Button ID="btnDEOCases" runat="server" CssClass="Buttons" Text="View Detail" OnClientClick="SetTarget();" OnClick="btnDEOCases_Click" />
                                    </div>
                                </div>
                            </div>
                             <div class="col-6 col-md-4">
                                <div class="boxshape" style="background-color: yellow; border: 2px solid black;">
                                    <div class="circle" style="">
                                        <div class="insideBox" style="border: 2px solid yellow;">
                                            <span>DIVISION<br />
                                                CASES</span>
                                        </div>
                                    </div>
                                    <div class="lableBox">
                                        <asp:Label ID="lblTBCCases" runat="server">22</asp:Label>
                                    </div>
                                    <div class="buttonDiv">
                                        <asp:Button ID="btnTBCCases" runat="server" CssClass="Buttons" Text="View Detail" OnClientClick="SetTarget();" OnClick="btnTBCCases_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 col-md-4">
                                <div class="boxshape" style="background-color: #EE9A4D; border: 2px solid red;">
                                    <div class="circle">
                                        <div class="insideBox" style="border: 2px solid #EE9A4D;">
                                            <span>JAL<br />NIGAM<br />
                                                CASES</span>
                                        </div>
                                    </div>
                                    <div class="lableBox">
                                        <asp:Label ID="lblRskCases" runat="server"></asp:Label>
                                    </div>
                                    <div class="buttonDiv">
                                        <asp:Button ID="btnRskCases" runat="server" CssClass="Buttons" Text="View Detail" OnClientClick="SetTarget();" OnClick="btnRskCases_Click" />
                                    </div>
                                </div>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],

            "bSort": false,

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
                    title: $('h1').text(),
                    exportOptions: {

                        // javascript: print(),

                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
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
</asp:Content>

