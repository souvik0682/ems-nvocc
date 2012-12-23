<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ImpBLChkLst.aspx.cs" Inherits="EMS.WebApp.Reports.ImpBLChkLst" Title=":: EMS :: Import B/L Checklist" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
<center>
    <div style="padding-top: 10px;">
        <fieldset style="width:964px;height:65px;">
            <table>
                <tr>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Location:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:DropDownList ID="ddlLoc" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Line / NVOCC:<span class="errormessage">*</span>
                    </td>
                    <td style="vertical-align:top;">
                        <asp:DropDownList ID="ddlLine" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Import Vessel & Voyage:
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:DropDownList ID="ddlVoyage" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="2" style="vertical-align:top;"><asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" OnClick="btnShow_Click" /></td>
                </tr>
            </table>
        </fieldset>
        <div style="padding-left:5px;width:980px;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%"></rsweb:ReportViewer>        
        </div>    
    </div>
</center>
</asp:Content>