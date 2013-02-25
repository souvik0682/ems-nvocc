<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ShowReport.aspx.cs" Inherits="EMS.WebApp.Reports.ReportViewer.ShowReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr id="main">
            <td style="width:10%">
                BL No.
            </td>
            <td style="width:20%">
                <asp:DropDownList ID="ddlBlNo" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlBlNo" InitialValue="0" ValidationGroup="Save" Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
            </td>
            <td  style="width:10%">
                Line
            </td>
            <td  style="width:20%">
                <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true"
                    onselectedindexchanged="ddlLine_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlLine" InitialValue="0" ValidationGroup="Save" Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="">
            <td>
                Gang Date
            </td>
            <td>
                <asp:TextBox ID="txtGangDate" runat="server" CssClass="" MaxLength="10" Width="150"></asp:TextBox>
                <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="txtGangDate"
                    TargetControlID="txtGangDate">
                </cc1:CalendarExtender>
                <br />
                <asp:RequiredFieldValidator ID="rfvReferenceDate" runat="server" CssClass="errormessage"
                    ControlToValidate="txtGangDate" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtGangDate" ID="revReferenceDate"
                    runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                    Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
            </td>
            <td>
                Shift
            </td>
            <td>
                <asp:DropDownList ID="ddlShift" runat="server">
                <asp:ListItem Text="Shift" Value="0"></asp:ListItem>
                <asp:ListItem Text="Shift-1" Value="1"></asp:ListItem>
                <asp:ListItem Text="Shift-2" Value="2"></asp:ListItem>
                <asp:ListItem Text="Shift-3" Value="3"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlShift" InitialValue="0" ValidationGroup="Save" Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Empty Yard
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlEmptyYard" runat="server">
                <asp:ListItem Text="Empty Yard" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
        <td colspan="4">
        <asp:Button ID="btnReport" runat="server" Text="View Report" 
                onclick="btnReport_Click" />
        </td>
        </tr>
    </table>
    <rsweb:ReportViewer ID="rptViewer" runat="server" Width="900" Height="500">
    </rsweb:ReportViewer>
</asp:Content>
