<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="AddEditUnit.aspx.cs" Inherits="EMS.WebApp.Forwarding.Master.AddEditUnit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">

<div id="headercaption">
    ADD / EDIT UNIT</div>
<center>
    <fieldset style="width: 400px;">
        <legend>Add / Edit Unit</legend>
        <table border="0" cellpadding="2" cellspacing="3">
            <tr>
                <td>
                    Unit Name:<span class="errormessage1">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtUnit" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="250"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                        ControlToValidate="txtUnit" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                </td>
            </tr>
<%--            <tr>
                <td style="width: 140px;">
                    Prefix
                </td>
                <td>
                    <asp:TextBox ID="txtPrefix" runat="server" CssClass="textboxuppercase" MaxLength="60"
                        Width="250"></asp:TextBox><br />
                </td>
            </tr>--%>
            <tr>
                <td>
                    Unit Type:<span class="errormessage1">*</span>
                </td>
                <td style="width: 50%">
                    <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatDirection="Horizontal" >
                    <asp:ListItem Text="Non Equipment" Selected="True" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Equipment" Value="E"></asp:ListItem>
                    </asp:RadioButtonList>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                    &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" 
                        Text="Cancel" onclick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</center>
</asp:Content>