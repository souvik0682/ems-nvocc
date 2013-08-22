<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditFreeDays.aspx.cs" Inherits="EMS.WebApp.MasterModule.AddEditFreeDays" %>
    <%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        ADD / EDIT FREE DAYS</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Slot Operator</legend>
            <table border="0" cellpadding="2" cellspacing="3">
                <tr>
                    <td>
                        Location:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLoc" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Line:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLine" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlLine_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width:140px;">Free Days:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtFreeDays" runat="server" CssClass="textboxuppercase" style="text-align:right" MaxLength="50" Width="250" onkeyup="IsDecimal(this)"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtFreeDays" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
               
                <tr>
                    <td>Status:</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>Inactive</asp:ListItem>
                        </asp:DropDownList>
                  
                   </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button 
                            ID="btnBack" runat="server" CssClass="button" Text="Back" 
                            onclick="btnBack_Click" Height="28px" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>

