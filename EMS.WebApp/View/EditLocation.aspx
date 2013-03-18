<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditLocation.aspx.cs" Inherits="EMS.WebApp.View.EditLocation" MasterPageFile="~/Site.Master" Title=":: Liner :: Add / Edit Location" %>
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
    <div id="headercaption">EDIT LOCATION</div>
    <center>
        <fieldset style="width:80%;">
            <legend>Edit Location</legend>
            <table border="0" cellpadding="1" cellspacing="0" width="100%">
                <tr>
                    <td style="width:19%;">Location Name:</td>
                    <td style="width:28%;"><asp:TextBox ID="txtLocName" runat="server" CssClass="textboxuppercase" Enabled="false" MaxLength="50" Width="250" TabIndex="1"></asp:TextBox></td>
                    <td style="width:6%;"></td>
                    <td style="width:19%;">Custom House Code:</td>
                    <td style="width:28%;"><asp:TextBox ID="txtCustomhouseCode" runat="server" CssClass="textboxuppercase" MaxLength="6" Width="250" TabIndex="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Address:</td>
                    <td><asp:TextBox ID="txtAddress" runat="server" CssClass="textboxuppercase" TextMode="MultiLine" Enabled="false" MaxLength="200" Rows="5" Width="250" TabIndex="2"></asp:TextBox></td>
                    <td></td>
                    <td>Gateway Port:</td>
                    <td><asp:TextBox ID="txtGatewayPort" runat="server" CssClass="textboxuppercase" MaxLength="6" Width="250" TabIndex="11"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>City:</td>
                    <td><asp:TextBox ID="txtCity" runat="server" CssClass="textboxuppercase" Enabled="false" MaxLength="20" Width="250" TabIndex="3"></asp:TextBox></td>
                    <td></td>
                    <td>ICEGATE Login ID:</td>
                    <td><asp:TextBox ID="txtICEGATE" runat="server" CssClass="textboxuppercase" MaxLength="20" Width="250" TabIndex="12"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Pin:</td>
                    <td><asp:TextBox ID="txtPin" runat="server" CssClass="textboxuppercase" Enabled="false" MaxLength="10" Width="250" TabIndex="4"></asp:TextBox></td>
                    <td></td>
                    <td>PCS Login ID:</td>
                    <td><asp:TextBox ID="txtPCS" runat="server" CssClass="textboxuppercase" MaxLength="8" Width="250" TabIndex="13"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Abbreviation:</td>
                    <td><asp:TextBox ID="txtAbbr" runat="server" CssClass="textboxuppercase" Enabled="false" MaxLength="3" Width="250" TabIndex="5"></asp:TextBox></td>
                    <td></td>
                    <td>ISO 20':</td>
                    <td><asp:TextBox ID="txtISO20" runat="server" CssClass="textboxuppercase" MaxLength="4" Width="250" TabIndex="14"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Phone:</td>
                    <td><asp:TextBox ID="txtPhone" runat="server" CssClass="textboxuppercase" Enabled="false" MaxLength="30" Width="250" TabIndex="6"></asp:TextBox></td>
                    <td></td>
                    <td>ISO 40':</td>
                    <td><asp:TextBox ID="txtISO40" runat="server" CssClass="textboxuppercase" MaxLength="4" Width="250" TabIndex="15"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>CAN Footer:</td>
                    <td><asp:TextBox ID="txtCAN" runat="server" CssClass="textboxuppercase" TextMode="MultiLine" MaxLength="300" Rows="5" Width="250" TabIndex="7"></asp:TextBox></td>
                    <td></td>
                    <td>Carting Footer:</td>
                    <td><asp:TextBox ID="txtCarting" runat="server" CssClass="textboxuppercase" TextMode="MultiLine" MaxLength="300" Rows="5" Width="250" TabIndex="16"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>SLOT footer:</td>
                    <td><asp:TextBox ID="txtSlot" runat="server" CssClass="textboxuppercase" TextMode="MultiLine" MaxLength="300" Rows="5" Width="250" TabIndex="8"></asp:TextBox></td>
                    <td></td>
                    <td>Pickup Footer:</td>
                    <td><asp:TextBox ID="txtPickup" runat="server" CssClass="textboxuppercase" TextMode="MultiLine" MaxLength="300" Rows="5" Width="250" TabIndex="17"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>PGR Free Days:</td>
                    <td><cc1:CustomTextBox ID="txtPGR" runat="server" CssClass="numerictextbox" Type="Numeric" MaxLength="3" Width="250" TabIndex="9"></cc1:CustomTextBox></td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="18" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" TabIndex="19" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>