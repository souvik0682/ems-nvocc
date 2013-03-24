<%@ Page Title=":: Liner :: Money Receipt" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="MoneyRcpt.aspx.cs" Inherits="EMS.WebApp.Reports.MoneyRcpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../CustomControls/AutoCompletepInvoice.ascx" TagName="AutoCompletepInvoice"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <center>
        <div id="headercaption">
            MONEY RECEIPT
        </div>
        <center>
            <fieldset style="width: 90%;">
                <legend>Money Receipt </legend>
                <div style="padding-top: 10px;">
                    <div style="padding-left: 5px; width: 98%;">
                        <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt">
                            <%--<LocalReport ReportPath="RDLC\IGMForm2.rdlc">
                </LocalReport>--%>
                        </rsweb:ReportViewer>
                    </div>
                </div>
            </fieldset>
        </center>
    </center>
</asp:Content>
