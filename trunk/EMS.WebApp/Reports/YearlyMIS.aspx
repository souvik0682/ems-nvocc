<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YearlyMIS.aspx.cs" Inherits="EMS.WebApp.Reports.YearlyMIS"
    MasterPageFile="~/Site.Master" Title=":: Liner :: Yearly MIS" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
<center>
    <div style="padding-top: 10px;">
        <fieldset style="width:964px;height:35px;">
        <table>
            <tr>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Year:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Location:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlLoc" runat="server">
                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">Line:</td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlLine" runat="server"></asp:DropDownList>
                </td>                
                <td class="label" style="padding-right:5px;vertical-align:top;">Size:</td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlSize" runat="server">
                        <asp:ListItem Value="All" Text="TEUs"></asp:ListItem>
                        <asp:ListItem Value="20" Text="20'"></asp:ListItem>
                        <asp:ListItem Value="40" Text="40'"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">Status:</td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="All" Text="All"></asp:ListItem>
                        <asp:ListItem Value="E" Text="Empty"></asp:ListItem>
                        <asp:ListItem Value="L" Text="Loaded"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="vertical-align:top;"><asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" OnClick="btnShow_Click" /></td>
            </tr>
        </table>
        </fieldset>
        <div style="padding-left:5px;width:980px;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%"></rsweb:ReportViewer>        
        </div>
    </div>
</center>
</asp:Content>
