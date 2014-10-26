<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditFLoc.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="EMS.WebApp.Forwarding.Master.AddEditFLoc" %>

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
            width: 474px;
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
            ADD / EDIT FORWARDING LOCATION</div>
        <center>
            <fieldset style="width: 400px;">
                <legend>Add / Edit Forwarding Location</legend>
                <table border="0" cellpadding="2" cellspacing="3">
                    
                    <tr>
                        <td class="style1">
                            Location<span class="errormessage1">*</span> :
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                            <br />
                            <asp:RequiredFieldValidator ID="rfvLoc" runat="server" CssClass="errormessage"
                                ControlToValidate="txtLocation" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td class="style1">
                            Abbreviation<span class="errormessage1">*</span> :
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtAbbr" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvAbbr" runat="server" CssClass="errormessage"
                                ControlToValidate="txtAbbr" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
  
                    <tr>
                        <td class="style1">
                            Address<span class="errormessage1">*</span> :
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250" Height="50px" TextMode="MultiLine"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" CssClass="errormessage"
                                ControlToValidate="txtAddress" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td class="style1">
                            City:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtCity" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>

                    <tr>
                        <td class="style1">
                            PIN:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="TxtPin" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>

                    <tr>
                        <td class="style1">
                            Phone:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>

                    <tr>
                        <td class="style1">
                        </td>
                        <td class="style2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnFLocID" runat="server" Value="0" />
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
