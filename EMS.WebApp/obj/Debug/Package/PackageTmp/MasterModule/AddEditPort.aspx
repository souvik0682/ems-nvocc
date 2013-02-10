<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditPort.aspx.cs" Inherits="EMS.WebApp.MasterModule.AddEditPort" %>

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
        ADD / EDIT PORT</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Port</legend>
            <table border="0" cellpadding="2" cellspacing="3">
                <tr>
                    <td style="width:140px;">Port Name:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtPortName" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtPortName" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <td>Port Code:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtPortCode" runat="server" CssClass="textboxuppercase" MaxLength="6" Width="250"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvAbbr" runat="server" CssClass="errormessage" Text="This field is Required" ControlToValidate="txtPortCode" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>

                <tr>
                    <td>ICD:</td>
                    <td>
                      <asp:DropDownList ID="ddlICD" runat="server">
                      <asp:ListItem Text="False" Value="0"></asp:ListItem>
                      <asp:ListItem Text="True" Value="1"></asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>

                  <tr>
                    <td>Port Addressee:</td>
                    <td><asp:TextBox ID="txtPortAddressee" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                   </td>
                </tr>

                  <tr>
                    <td>Address1:</td>
                    <td><asp:TextBox ID="txtAdd1" runat="server" CssClass="textboxuppercase" 
                            MaxLength="50" Width="250" TextMode="MultiLine"></asp:TextBox><br />
                   </td>
                </tr>

                  <tr>
                    <td>Address2:</td>
                    <td><asp:TextBox ID="txtAdd2" runat="server" CssClass="textboxuppercase" 
                            MaxLength="50" Width="250" TextMode="MultiLine"></asp:TextBox><br />
                   </td>
                </tr>
                
                  <tr>
                    <td>Export Port:</td>
                    <td><asp:TextBox ID="txtExportPort" runat="server" CssClass="textboxuppercase" 
                            MaxLength="3" Width="250" TextMode="SingleLine"></asp:TextBox><br />
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
