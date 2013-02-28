<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditLine.aspx.cs" Inherits="EMS.WebApp.MasterModule.AddEditLine" %>

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
        ADD / EDIT LINE</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Line</legend>
            <table border="0" cellpadding="2" cellspacing="3">
                <tr>
                    <td style="width:140px;">Line:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtLineName" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtLineName" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <td>Contact Agent:</td>
                    <td><asp:TextBox ID="txtContact" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="250"></asp:TextBox><br />
                    </td>
                </tr>

                <tr>
                    <td>Default Free Days:</td>
                    <td><asp:TextBox ID="txtFreeDays" runat="server" CssClass="textboxuppercase" style="text-align:right" MaxLength="10" Width="250" onkeyup="IsNumeric(this)"></asp:TextBox><br /></td>
                </tr>

                 <tr>
                    <td>Import Commission:</td>
                    <td><asp:TextBox ID="txtImpCommsn" runat="server" CssClass="textboxuppercase" MaxLength="10" style="text-align:right" Width="250" onkeyup="IsDecimal(this)"></asp:TextBox><br /></td>
                </tr>

                 <tr>
                    <td>Export Commission:</td>
                    <td><asp:TextBox ID="txtExportCommission" runat="server" CssClass="textboxuppercase" style="text-align:right" MaxLength="10" Width="250" onkeyup="IsDecimal(this)"></asp:TextBox><br /></td>
                </tr>
                  <tr>
                    <td>Export B/L:</td>
                    <td><asp:DropDownList ID="ddlExpBook" runat="server">
                    <asp:ListItem Value="y">Yes</asp:ListItem>
                    <asp:ListItem Value="n">No</asp:ListItem>
                    </asp:DropDownList>
                    <br /></td>
                </tr>

                  <tr>
                    <td>Logo:</td>
                    <td>
                     <asp:FileUpload ID="fuLogo" runat="server"></asp:FileUpload>
                     <input type="hidden" id="hdnLogo" runat="server" />
                        <asp:Image ID="imgLogo" runat="server" Height="34px" 
                            ImageUrl="" Width="37px" />
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
