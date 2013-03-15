<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditCountry.aspx.cs" Inherits="EMS.WebApp.MasterModule.AddEditCountry" %>

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
        ADD / EDIT COUNTRY</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Country</legend>
            <table border="0" cellpadding="2" cellspacing="3">
                <tr>
                    <td style="width:140px;">Country Name:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtCountryName" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtCountryName" Display="Dynamic" ValidationGroup="Save" Text="This field is Required"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <td>Country Code:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtAbbr" runat="server" CssClass="textboxuppercase" MaxLength="2" Width="250"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvAbbr" runat="server" CssClass="errormessage" Text="This field is Required" ControlToValidate="txtAbbr" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>

                <tr>
                    <td>GMT:</td>
                    <td><asp:TextBox ID="txtGMT" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="250"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>ISD Code:</td>
                    <td><asp:TextBox ID="txtISD" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="250"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>Sector:</td>
                    <td><asp:TextBox ID="txtSector" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                    </td>
                </tr>
              
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button 
                            ID="btnBack" runat="server" CssClass="button" Text="Back" 
                            onclick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>
