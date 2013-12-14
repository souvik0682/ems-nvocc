<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpManifestEdge.aspx.cs" MasterPageFile="~/Site.Master"
    Inherits="EMS.WebApp.Reports1.ExpManifestEdge" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Import Namespace="EMS.Utilities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
       
    </script>
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        EXPORT MANIFEST
    </div>
    <center>
  
        <fieldset style="padding: 5px; width: 55%">
            <table style="width: 100%" cellpadding="1" cellspacing="0">
                <tr id="main">
                    <td align="left" style="width: 15%">
                        Location:<span class="errormessage">*</span>
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLine_SelectedIndexChanged"
                            Width="120">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlLine" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        Line / NVOCC:<span class="errormessage" style="width: 15%">*</span>
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlLocation" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Manifest Type:<span class="errormessage">*</span>
                    </td>
                    <td align="left" style="width: 15%">  
                        <asp:DropDownList ID="ddlCargoOrFreight" runat="server">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Freight" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Cargo" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlCargoOrFreight" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>

                    <td>Voyage / BL:<span class="errormessage">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlVoyBL" runat="server" AutoPostBack="true"
                            onselectedindexchanged="ddlVoyBL_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Voyage" Value="1"></asp:ListItem>
                            <asp:ListItem Text="B/L" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvVoyBL" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlVoyBL" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                
                </tr>
                <tr runat="server" id="tr1">
                    <td>Vessel:<span class="errormessage">*</span>
                    </td>
                    <td align="left" style="width: 15%">  
                        <asp:DropDownList AutoPostBack="true" ID="ddlVessel" runat="server" 
                        onselectedindexchanged="ddlVessel_SelectedIndexChanged"  >
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                             <asp:ListItem Text="BL" Value="1"></asp:ListItem>
                              <asp:ListItem Text="Manifest" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvVessel" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlVessel" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                        </td>
                     <td>
                        Voyage:<span class="errormessage">*</span>
                    </td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddlVoyage" runat="server" Width="120" onselectedindexchanged="ddlVoyage_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvVoyage" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlVoyage" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr runat="server" id="tr3">
                    <td>FPOD:<span class="errormessage">*</span></td><td align="left" colspan="3">  
                    <asp:DropDownList ID="ddlPOD" runat="server">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ID="rfvPOD" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlPOD" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>--%>
                    </td>
                
                </tr>

                <tr runat="server" id="tr2">
                    <td>B/L No:<span class="errormessage">*</span></td><td align="left" colspan="3">  
                    <asp:DropDownList ID="ddlBLNo" runat="server">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                          
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvBlNo" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlBLNo" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator></td>
                
                </tr>
                   
 
                <tr>
                    <td colspan="4" align="left" style="padding: 5px 5px 5px 0">
                        <asp:Button ID="btnReport" runat="server" Text="View Report" ValidationGroup="Report"
                            OnClick="btnReport_Click" />
                      
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
         <div style="padding-left:5px;width:100%;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%"></rsweb:ReportViewer>        
        </div>    
        </fieldset>
    </center>
</asp:Content>
