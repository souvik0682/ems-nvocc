<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="EMS.WebApp.Reports.ReportViewer.ReportList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
<h3>Reports</h3>
<ul>
<li><a href="ShowReport.aspx?reportName=CargoArrivalNotice">CargoArrivalNotice</a></li>
<li><a href="ShowReport.aspx?reportName=DeliveryOrder">DeliveryOrder</a></li>
<li><a href="ShowReport.aspx?reportName=EDeliveryOrder">EDeliveryOrder</a></li>
<li><a href="ShowReport.aspx?reportName=Custom">Custom</a></li>
<li><a href="ShowReport.aspx?reportName=Gang">Gang</a></li>
<li><a href="ShowReport.aspx?reportName=InvoiceDeveloper">Invoice</a></li>
</ul>
</asp:Content>
