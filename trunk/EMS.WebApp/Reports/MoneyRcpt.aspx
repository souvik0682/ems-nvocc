<%@ Page Title=":: Liner :: Money Receipt" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoneyRcpt.aspx.cs" Inherits="EMS.WebApp.Reports.MoneyRcpt" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../CustomControls/AutoCompletepInvoice.ascx" tagname="AutoCompletepInvoice" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">

<asp:UpdateProgress ID="uProgressLoc" runat="server" AssociatedUpdatePanelID="upLoc">
        <ProgressTemplate>
            <div class="progress">
                <div id="image">
                    <img src="../../Images/PleaseWait.gif" alt="" /></div>
                <div id="text">
                    Please Wait...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>

 <div id="headercaption">
        MONEY RECEIPT </div>
    <center>
        <fieldset style="width:750px; ">
            <legend> Money Receipt </legend>
             <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                    <Triggers>
                       <%-- <asp:AsyncPostBackTrigger ControlID="ddlVoyage" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVessel" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLoc" EventName="SelectedIndexChanged" />--%>
                    </Triggers>
                    <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="3" width="100%">
            
           <tr>
                    <td style="text-align:left; width:12%" >Invoice No:</td>
                    <td style="text-align:left; width:20%">
                        <%--<asp:TextBox ID="txtInvoiceNo" runat="server" MaxLength="20" CssClass="textboxuppercase" Width="150"></asp:TextBox>--%>
                        
                        <uc1:AutoCompletepInvoice ID="AutoCompletepInvoice1" runat="server" />
                        
                    </td>
                     <td  style="text-align:center; width:15%">
                    <asp:Button ID="btnGen" runat="server"  Text="Generate >>" 
                        ValidationGroup="Save" onclick="btnGen_Click" />
                   
                </td>
                     <td style="text-align:right; width:20%" >Money Receipt No:</td>
                    <td style="text-align:left; width:20%">
                       <asp:DropDownList ID="ddlMnyRcpt" runat="server" ></asp:DropDownList>
                    </td>
             
                <td  style="text-align:center; width:10%">
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Show" 
                        ValidationGroup="Save" />
                    &nbsp;&nbsp;
                </td>
            </tr>
    
            </table>
             </ContentTemplate>
       </asp:UpdatePanel>
        </fieldset>
    </center>

    <div style="padding-top: 10px;">
        <div style="padding-left:5px;width:98%;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" >
                <%--<LocalReport ReportPath="RDLC\IGMForm2.rdlc">
                </LocalReport>--%>
            </rsweb:ReportViewer>        
        </div>    
    </div>
</center>

</asp:Content>
