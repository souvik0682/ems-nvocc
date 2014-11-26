<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditFLine.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="EMS.WebApp.Forwarding.Master.AddEditFLine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%-- <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {
            var hdnFPOD = $get('<%=hdnFPOD.ClientID %>');
            hdnFPOD.value = e.get_value();
        }
    </script>--%>

    <style type="text/css">
        .style1
        {
            width: 770px;
        }
        .style2
        {
            width: 503px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            ADD / EDIT FORWARDING PRINCIPALS</div>
        <center>
            <fieldset style="width: 400px;">
                <legend>Add / Edit Forwarding Principal</legend>
                <table border="0" cellpadding="2" cellspacing="3">
<%--                     <tr>
                        <td class="style1">
                            Line Type<span class="errormessage1">*</span> :
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlLineType" runat="server"  CssClass="dropdownlist">
                            <asp:ListItem Text="Sea" Value="S"></asp:ListItem>
                            <asp:ListItem Text="Air" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Agent" Value="O"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="style1">
                            Principal Name<span class="errormessage1">*</span> :
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtLine" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>

                    <tr>
                        <td class="style1">
                            Prefix:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtPrefix" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>
  
                    <tr>
                        <td class="style1">
                        </td>
                        <td class="style2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnFLineID" runat="server" Value="0" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="vgHaulage" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClick="btnBack_Click" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
</asp:Content>
