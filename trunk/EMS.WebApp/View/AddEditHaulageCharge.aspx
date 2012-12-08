<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditHaulageCharge.aspx.cs" Inherits="EMS.WebApp.View.AddEditHaulageCharge" MasterPageFile="~/Site.Master" Title=":: EMS :: Add / Edit Haulage Charge" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT HAULAGE CHARGE</div>
    <center>
        <fieldset style="width:450px;">
            <legend>Add / Edit Haulage Charge</legend>
            <table border="0" cellpadding="3" cellspacing="3" width="100%">
                <tr>
                    <td style="width:150px;">Location From:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlLocFrom" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Location To:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlLocTo" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Container Size:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlSize" runat="server">
                            <asp:ListItem Value="20" Text="TEU"></asp:ListItem>
                            <asp:ListItem Value="40" Text="FEU"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Weight From:<span class="errormessage1">*</span></td>
                    <td>
                        <cc1:CustomTextBox ID="txtWeightFrom" runat="server" CssClass="numerictextbox" Type="Numeric" MaxLength="3" Width="250" TabIndex="9"></cc1:CustomTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Weight To:<span class="errormessage1">*</span></td>
                    <td>
                        <cc1:CustomTextBox ID="txtWeightTo" runat="server" CssClass="numerictextbox" Type="Numeric" MaxLength="3" Width="250" TabIndex="9"></cc1:CustomTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Rate:<span class="errormessage1">*</span></td>
                    <td>
                        <cc1:CustomTextBox ID="txtRate" runat="server" CssClass="numerictextbox" Type="Numeric" MaxLength="3" Width="250" TabIndex="9"></cc1:CustomTextBox>
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