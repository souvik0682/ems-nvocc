<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EDI_Export.aspx.cs" Inherits="EMS.WebApp.Export.EDI_Export" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
         EDI TEXT FILE GENERATION 
    </div>
    <center>
    <fieldset style="width: 964px; height: 85x;">
        <table>
            <tr>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Location:<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top; margin-left: 40px;">
                    <asp:DropDownList ID="ddlLoc" runat="server" AutoPostBack="True" Width="135px">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="rfvLoc" runat="server" ErrorMessage="Please select Location"
                        ControlToValidate="ddlLoc"  InitialValue="0"  ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:validatorcalloutextender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvLoc"
                        WarningIconImageUrl="">
                    </cc1:validatorcalloutextender>
                </td>
                         <td class="label" style="padding-right: 50px; vertical-align: top;" 
                    nowrap="nowrap">
                    Vessel :<span class="errormessage">*</span>
                </td>
           <td>                    <asp:DropDownList ID="ddlVessel" runat="server" 
                   AutoPostBack="True" Width="135px" 
                   onselectedindexchanged="ddlVessel_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList></td>
                     <asp:RequiredFieldValidator ID="rfvVessel" runat="server" ErrorMessage="Please select Location"
                        ControlToValidate="ddlVessel"  InitialValue="0"  ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:validatorcalloutextender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvVessel"
                        WarningIconImageUrl="">
                    </cc1:validatorcalloutextender>
            </tr>          
            <tr>
              <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Voyage:<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlVoyage" runat="server" AutoPostBack="True" 
                        Width="135px" onselectedindexchanged="ddlVoyage_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="rfvLine" runat="server" ErrorMessage="Please select Line"
                       ControlToValidate="ddlVoyage"
                        InitialValue="0" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:validatorcalloutextender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvLine"
                        WarningIconImageUrl="">
                    </cc1:validatorcalloutextender>
                </td>    



             <td style="width: 7%;">
                                    Loading Port: 
                                </td>
                                <td style="width: 5%;">
                                    
                                                       <asp:DropDownList ID="ddlLoadingPort" runat="server" AutoPostBack="True" Width="135px">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                                    
                                </td>
            </tr>         
            <tr>
                <td style="vertical-align: top;">
                    <asp:Button ID="btnShow" runat="server" Text="Download Text" CssClass="button" OnClientClick="javascript:return sure
                    ();" OnClick="btnShow_Click" />
                </td>
               <td>                       
                            &nbsp;</td>
            </tr>
        </table>
    </fieldset>
    </center>
</asp:Content>
