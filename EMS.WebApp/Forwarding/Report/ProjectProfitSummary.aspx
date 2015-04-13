<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProjectProfitSummary.aspx.cs" Inherits="EMS.WebApp.Forwarding.Report.ProjectProfitSummary" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<script type="text/javascript" language="javascript">

    function validateData() {

        if (document.getElementById('<%=txtStartDt.ClientID %>').value == '__/__/____') {
            alert('Please enter from date');
            return false;
        }
        if (document.getElementById('<%=txtEndDt.ClientID %>').value == '__/__/____') {
            alert('Please enter to date');
            return false;
        }
        return true;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">

    <center>

 <div id="headercaption">
         PROJECT PROFIT SUMMARY </div>
    <center>
        <fieldset style="width:800px; ">
            <legend> Project Profit </legend>
    
            <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="3" width="100%">
             <tr id="DateRange" runat="server">
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    <asp:Label ID="lblStartDt" runat="server" Text="Start Date"></asp:Label>
                    &nbsp;
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:TextBox ID="txtStartDt" runat="server" CssClass="textbox" Width="180"  AutoPostBack="True"></asp:TextBox>
                    <cc1:CalendarExtender ID="cbeFromDt" runat="server" TargetControlID="txtStartDt"
                        Format="dd/MM/yyyy" />
                               <cc1:MaskedEditExtender ID="msk1" runat="server" UserDateFormat="MonthDayYear" Mask="99/99/9999"
                        TargetControlID="txtStartDt" ClearMaskOnLostFocus="False">
                    </cc1:MaskedEditExtender>
                  
                </td>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    <asp:Label ID="lblEndDt" runat="server" Text="End Date"></asp:Label>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:TextBox ID="txtEndDt" runat="server" CssClass="textbox" Width="180"></asp:TextBox>
                    <cc1:CalendarExtender ID="cbeToDt" runat="server" TargetControlID="txtEndDt" Format="dd/MM/yyyy" />                   
                    <cc1:MaskedEditExtender ID="msk2" runat="server" UserDateFormat="MonthDayYear" Mask="99/99/9999"
                        TargetControlID="txtEndDt" ClearMaskOnLostFocus="False">
                    </cc1:MaskedEditExtender>
                </td>
               
            </tr>
            <tr>
                <td>
                    Location:<span class="errormessage">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropdownlist" TabIndex="60"
                        DataSourceID="OpsDs" DataTextField="LocName" DataValueField="pk_LocID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="OpsDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                        SelectCommand="SELECT 0 [pk_LocID], '-- All --' [LocName]
                        UNION
                        SELECT pk_LocID, LocName FROM fwd.mstFLocation"></asp:SqlDataSource>
                </td>
                <td>
                    Job Type:<span class="errormessage">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlJobStatus" runat="server" Width="155" Enabled = "false">
<%--                        <asp:ListItem Selected="True" Text="All Jobs" Value=""></asp:ListItem>
                        <asp:ListItem Text="Proforma Jobs" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Pending Approval" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Approved Jobs" Value="O"></asp:ListItem>--%>
                        <asp:ListItem Selected="True" Text="Closed Jobs" Value="C"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Principal:<span class="errormessage">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPrincipal" runat="server" CssClass="dropdownlist" TabIndex="60"
                        DataSourceID="PrincipalDs" DataTextField="LineName" DataValueField="pk_FLineID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="PrincipalDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                        SelectCommand="SELECT 0 [pk_FLineID], '-- All --' [LineName]
                        UNION
                        SELECT pk_FLineID, LineName FROM fwd.mstFLine"></asp:SqlDataSource>
                </td>
                <td>
                    Job Scope:<span class="errormessage">*</span>
                </td>
                 <td>
                    <asp:DropDownList ID="ddlJobScope" runat="server" CssClass="dropdownlist" TabIndex="60"
                        DataSourceID="ScopeDs" DataTextField="JobScope" DataValueField="pk_JobScopeID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="ScopeDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                        SelectCommand="SELECT 0 [pk_JobScopeID], '-- All --' [JobScope]
                        UNION
                        SELECT pk_JobScopeID, JobScope FROM fwd.mstJobScope">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    Sales:<span class="errormessage">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSales" runat="server" CssClass="dropdownlist"
                        TabIndex="60" DataSourceID="SalesDs" DataTextField="UserName" DataValueField="pk_UserID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SalesDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                        SelectCommand="SELECT 0 [pk_UserID], '-- Select --' [UserName]
                        UNION
                        SELECT pk_UserID, FirstName + ' ' + LastName + '(' + UserName + ')' AS UserName FROM dsr.dbo.mstUser WHERE (fk_RoleID in (2,4)) ORDER BY USERNAME">
                    </asp:SqlDataSource>
                </td>
                <td>
                    Booking Party:<span class="errormessage">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="dropdownlist" TabIndex="60" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <asp:Button ID="btnShow" runat="server" Text="Generate Excel" CssClass="button" OnClick="btnShow_Click" />
                </td>
            </tr>
            </table>
            </ContentTemplate>

        </fieldset>
    </center>

</center>
</asp:Content>
