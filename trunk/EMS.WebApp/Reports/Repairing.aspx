<%@ Page Title=":: Liner :: Repairing Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Repairing.aspx.cs"  Inherits="EMS.WebApp.Reports.Repairing" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <%--             <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">--%>
    <center>

 <div id="headercaption">
        REPAIRING REPORT </div>
    <center>
        <fieldset style="width:900px; ">
            <legend> Reparing Report </legend>
            <%--&nbsp;&nbsp;--%>

         <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="3" width="100%">
            
                <tr>
                  <td style="width:60%" >
                  <table width="100%">
                     <tr>
                        <td style="text-align:left">Line:</td>
                        <td>
                            <asp:DropDownList ID="ddlLine" runat="server" Width="150" 
                                onselectedindexchanged="ddlLine_SelectedIndexChanged" > 
                            </asp:DropDownList>
                        </td>
           
                        <td style="text-align:left">Location:<span class="errormessage1">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlLoc" runat="server" Width="150" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged"
                                            AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td style="text-align:left">Yard:<span class="errormessage1">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlEmptyYard" runat="server" Width="250" OnSelectedIndexChanged="ddlEmptyYard_SelectedIndexChanged"
                                            AutoPostBack="true"></asp:DropDownList>
                        </td>

                     </tr>
                     <tr>
                        <td style="text-align:left">Start Date:</td>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="textboxuppercase" Width="150"></asp:TextBox>
                            <cc2:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtStartDate" runat="server" />
                        </td>
                        <td style="text-align:left" >End Date:</td>
                        <td>
                            <asp:TextBox ID="txtdtStock" runat="server" CssClass="textboxuppercase" Width="150"></asp:TextBox>
                            <cc2:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtdtStock" runat="server" />
                        </td>
                        <td colspan="2" style="text-align:right; width:5%">
                            <asp:Button ID="Button1" runat="server" onclick="btnExcel_Click" Text="Generate Excel" 
                            ValidationGroup="Excel" />
                            <%--       </asp:UpdatePanel>--%>
                    </td>
                     </tr>                    
                  </table>
                   
                </tr>
            </table>
            </ContentTemplate>
<%--       </asp:UpdatePanel>--%>
        </fieldset>
    </center>

</center>
</asp:Content>
