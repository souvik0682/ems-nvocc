<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditGroup.aspx.cs" Inherits="EMS.WebApp.Forwarding.Master.AddEditGroup" %>

<%@ Register Src="~/CustomControls/AutoCompleteCountry.ascx" TagPrefix="uc1" TagName="AutoCompleteCountry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">

    <div id="headercaption">
        ADD / EDIT GROUP</div>
    <center>
        <fieldset style="width:600px;">
            <legend>Add / Edit Group</legend>
            <table border="0" cellpadding="2" cellspacing="3">
                <tr>
                    <td style="width:100px;">Group Name<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtPartyName" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="300"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvtxtPartyName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtPartyName" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>Address:</td>
                    <td><asp:TextBox ID="txtAddress" runat="server" CssClass="textboxuppercase" 
                            MaxLength="60" Width="300px" Height="63px" TextMode="MultiLine"></asp:TextBox>
                            
                   </td>
                </tr>

                                
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" 
                            Text="Cancel" onclick="btnCancel_Click"  />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>
