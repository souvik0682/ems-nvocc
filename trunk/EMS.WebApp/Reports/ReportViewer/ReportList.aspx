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
<li><a href="ShowReport.aspx?reportName=InvoiceDeveloper">Invoice1</a></li>
<li><a href="ShowReport.aspx?reportName=creditnote">creditnote</a></li>
<li><a href="../../Popup/Report.aspx?ReportName=InvoiceDeveloper&LineBLNo=UAFLMJN/NHV01&Location=1&LoginUserName=1&InvoiceId=1">Invoice</a></li>
<li><a href="ShowReport.aspx?reportName=OnOffHire">On/Off-Hire</a></li>
<li><a href="ShowReport.aspx?reportName=PendingDelivaryOrder">PendingDelivaryOrder</a></li>
<li><a href="ShowReport.aspx?reportName=RptMoneyRcptNoWithInDates">RptMoneyRcptNoWithInDates</a></li>
<li><a href="ShowReport.aspx?reportName=CrnWithinDates">CrnWithinDates</a></li>
</ul>
</asp:Content>
