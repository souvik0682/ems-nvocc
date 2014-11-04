<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateMUST.aspx.cs" Inherits="EMS.WebApp.View.UpdateMUST" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 268px;
        }
        .style2
        {
            color: #000000;
            width: 158px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">

        <div id="headercaption">
          Update MUST
        </div>
        <center>
            <asp:Label ID="lblError" runat="server" Text="Port of Discharge cannot be left blank"
                Style="color: Red; display: none"></asp:Label>
            <fieldset style="width: 1020px;">
                <legend> Update MUST </legend>
                <table width="100%">
                    <tr>
                        <td class="style2" style="padding-right: 50px; vertical-align: top;">
                            Month:
                        </td>
                      
                        <td style="padding-right: 20px; vertical-align: top;">
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="135px" >
                                <asp:ListItem Text="January" Value="01"></asp:ListItem>
                                <asp:ListItem Text="February" Value="02"></asp:ListItem>
                                <asp:ListItem Text="March" Value="03"></asp:ListItem>
                                <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                <asp:ListItem Text="June" Value="06"></asp:ListItem>
                                <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </td>             
                        <td>
                             Year:
                        </td>
                        <td class="style1">
                            <cc2:CustomTextBox ID="txtYear" runat="server" TabIndex="23" 
                                CssClass="numerictextbox" Enabled="true"
                                Width="82px" Type="Decimal" MaxLength="4" Precision="4" Scale="0"></cc2:CustomTextBox>
                        </td>
                                                
                        <td style="text-align: right; width: 5%;">
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Update MUST"  />
                        </td>
                    </tr>
                 </table>
                 <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
            </fieldset>
           
    </center>
</asp:Content>
