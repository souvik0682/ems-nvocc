<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditSTax.aspx.cs" Inherits="EMS.WebApp.MasterModule.AddEditSTax" %>
<%@ Register src="../CustomControls/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>
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
        ADD / EDIT SERVICE TAX</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Tax</legend>
            <table border="0" cellpadding="2" cellspacing="3">
                <tr>
                    <td style="width:140px;">Tax %:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtTax" runat="server" CssClass="textboxuppercase" style="text-align:right" MaxLength="50" Width="250" onkeyup="IsDecimal(this)"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtTax" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <td>Cess %:</td>
                    <td><asp:TextBox ID="txtCess" runat="server" style="text-align:right" CssClass="textboxuppercase" MaxLength="10" Width="250" onkeyup="IsDecimal(this)"></asp:TextBox><br />
                    </td>
                </tr>

                <tr>
                    <td>Additional Cess %:</td>
                    <td><asp:TextBox ID="txtAddiCess" runat="server" style="text-align:right" CssClass="textboxuppercase" MaxLength="10" Width="250" onkeyup="IsDecimal(this)"></asp:TextBox><br /></td>
                </tr>

                  <tr>
                    <td>Start Date:<span class="errormessage1">*</span></td>
                    <td>
                        <uc1:DatePicker ID="DatePicker1" runat="server" />
                        <asp:TextBox ID="txtStDate" ReadOnly="true" runat="server" CssClass="textboxuppercase" 
                            MaxLength="10" Width="250" ></asp:TextBox>
                        <br />
                           
                   </td>
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
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" 
                            onclick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>
