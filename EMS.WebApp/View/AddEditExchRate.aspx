<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditExchRate.aspx.cs" Inherits="EMS.WebApp.View.AddEditExchRate" MasterPageFile="~/Site.Master" Title=":: EMS :: Add / Edit Exchange Rate" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT EXCHANGE RATE</div>
    <center>
        <fieldset style="width:500px;">
            <legend>Add / Edit Exchange Rate</legend>
            <table border="0" cellpadding="3" cellspacing="3" width="100%">
                <tr>
                    <td style="width:180px;">Date:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        <cc2:CalendarExtender ID="ceDate" TargetControlID="txtDate" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" CssClass="errormessage" ControlToValidate="txtDate" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>USD to INR Conversion Rate:<span class="errormessage1">*</span></td>
                    <td>
                        <cc1:CustomTextBox ID="txtRate" runat="server" CssClass="numerictextbox" Type="Decimal" MaxLength="13" Precision="10" Scale="2" Width="100"></cc1:CustomTextBox>
                        <asp:RequiredFieldValidator ID="rfvRate" runat="server" CssClass="errormessage" ControlToValidate="txtRate" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Free Days:<span class="errormessage1">*</span></td>
                    <td>
                        <cc1:CustomTextBox ID="txtFreeDays" runat="server" CssClass="numerictextbox" Type="Numeric" MaxLength="2" Width="50"></cc1:CustomTextBox>
                        <asp:RequiredFieldValidator ID="rfvFreeDays" runat="server" CssClass="errormessage" ControlToValidate="txtFreeDays" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>