<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="EMS.WebApp.Reports.ReportViewer.ReportList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
<h3>Reports</h3>
<ul>
<%--http://cmc-pc/ReportsV2/Pages/Report.aspx?ItemPath=%2fEMS.Report%2fCargoArrivalNotice--%>
<li><a href="ShowReport.aspx?reportName=CargoArrivalNotice">CargoArrivalNotice</a></li>
<%--http://cmc-pc/ReportsV2/Pages/Report.aspx?ItemPath=%2fEMS.Report%2fDeliveryOrder--%>
<li><a href="ShowReport.aspx?reportName=DeliveryOrder">DeliveryOrder</a></li>
<%--http://cmc-pc/ReportsV2/Pages/Report.aspx?ItemPath=%2fEMS.Report%2fEDeliveryOrder--%>
<li><a href="ShowReport.aspx?reportName=EDeliveryOrder">EDeliveryOrder</a></li>
<%--http://cmc-pc/ReportsV2/Pages/Report.aspx?ItemPath=%2fEMS.Report%2fCustom--%>
<li><a href="ShowReport.aspx?reportName=Custom">Custom</a></li>
<%--http://cmc-pc/ReportsV2/Pages/Report.aspx?ItemPath=%2fEMS.Report%2fGang--%>
<li><a href="ShowReport.aspx?reportName=Gang">Gang</a></li>
</ul>
</asp:Content>
