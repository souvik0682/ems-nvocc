﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ShowReport.aspx.cs" Inherits="EMS.WebApp.Reports.ReportViewer.ShowReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
<center>
<fieldset style="padding:5px;width:55%">
    <table style="width: 100%" cellpadding="1" cellspacing="0">
        <tr id="main">
            <td style="width:10%">
                BL No.:<span class="errormessage">*</span>
            </td>
            <td style="width:20%"  align="left" >
                <asp:DropDownList ID="ddlBlNo" runat="server" Width="120">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>                
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlBlNo" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
            </td>
            <td  style="width:10%"   >
                Location:<span class="errormessage">*</span>
            </td>
            <td  style="width:20%"  align="left" >
                <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true"
                     Width="120">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlLine" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trgang1" runat="server" >
            <td   >
                Gang Date:<span class="errormessage">*</span>
            </td>
            <td  align="left" >
                <asp:TextBox ID="txtGangDate" runat="server" CssClass="" MaxLength="10" Width="113"></asp:TextBox>
                <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="txtGangDate"
                    TargetControlID="txtGangDate">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvReferenceDate" runat="server" CssClass="errormessage"
                    ControlToValidate="txtGangDate" ValidationGroup="Report" Display="Dynamic" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtGangDate" ID="revReferenceDate"
                    runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]" ValidationGroup="Report" 
                    Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
            </td>
            <td>
                Shift:<span class="errormessage">*</span>
            </td>
            <td  align="left" >
                <asp:DropDownList ID="ddlShift" runat="server" Width="120">
                <asp:ListItem Text="Shift" Value="0"></asp:ListItem>
                <asp:ListItem Text="Shift-1" Value="1"></asp:ListItem>
                <asp:ListItem Text="Shift-2" Value="2"></asp:ListItem>
                <asp:ListItem Text="Shift-3" Value="3"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlShift" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr  style="visibility:hidden" id="trgang2" runat="server">
            <td  align="left" >
                Empty Yard:<span class="errormessage">*</span>
            </td>
            <td colspan="3"  align="left" >
                <asp:DropDownList ID="ddlEmptyYard" runat="server" Width="120">
                <asp:ListItem Text="Empty Yard" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="trCar">
        <td>Line:<span class="errormessage">*</span></td>
        <td  align="left" >
            <asp:DropDownList ID="ddlLocation" runat="server"  AutoPostBack="true"
                onselectedindexchanged="ddlLocation_SelectedIndexChanged">
            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>            
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlLocation" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
        </td>
        <td>Vessel:<span class="errormessage">*</span></td>
        <td  align="left" > <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true"
                onselectedindexchanged="ddlVessel_SelectedIndexChanged">
            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlVessel" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
            </td>
            </tr>
             <tr runat="server" id="trCar1">
             <td>Voyage:<span class="errormessage">*</span></td> <td  align="left" > <asp:DropDownList ID="ddlVoyage" runat="server" AutoPostBack="true"
                     onselectedindexchanged="ddlVoyage_SelectedIndexChanged">
            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlVoyage" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator></td>
             <td>ETA:<span class="errormessage">*</span></td> <td align="left">
                 <asp:TextBox ID="txtETA" runat="server" Width="113"></asp:TextBox>
                 <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server" PopupButtonID="txtETADt"
                                TargetControlID="txtETA">
                            </cc1:CalendarExtender>
                            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="errormessage"
                    ControlToValidate="txtETA" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ControlToValidate="txtETA" ID="RegularExpressionValidator1"
                    runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]" ValidationGroup="Report" 
                    Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
                 </td>
        </tr>
        <tr>
       
        <td colspan="4" align="left" style="padding:5px 5px 5px 0">
        <asp:Button ID="btnReport" runat="server" Text="View Report" ValidationGroup="Report" 
                onclick="btnReport_Click" />
        </td>
        </tr>
    </table>
</fieldset>
    </center>
    <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" Height="900"  >
    </rsweb:ReportViewer>
</asp:Content>
