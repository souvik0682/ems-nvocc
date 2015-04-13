<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="JobSheet.aspx.cs" Inherits="EMS.WebApp.Forwarding.Report.JobSheet" %>

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
         JOB SHEET </div>
    <center>
        <fieldset style="width:800px; ">
            <legend> Job Sheet </legend>
    
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

            <tr id="Tr1" runat="server">
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                    &nbsp;
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropdownlist" 
                    TabIndex="60" DataSourceID="LocDs" DataTextField="LocName" DataValueField="pk_LocID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="LocDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                    SelectCommand="SELECT 0 [pk_LocID], '-- All --' [LocName]
                    UNION
                    SELECT DISTINCT pk_LocID, LocName FROM dsr.dbo.mstLocation where Active = 'Y' AND IsDeleted = 0"></asp:SqlDataSource>
                </td>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    <asp:Label ID="lblJobType" runat="server" Text="Job Type"></asp:Label>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlJobType" runat="server" CssClass="dropdownlist" 
                    TabIndex="60" DataSourceID="JobTypeDs" DataTextField="JobType" DataValueField="pk_JobTypeID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="JobTypeDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                    SelectCommand="SELECT 0 [pk_JobTypeID], '-- All --' [JobType]
UNION
SELECT DISTINCT pk_JobTypeID, JobType FROM fwd.mstJobType"></asp:SqlDataSource>
                </td>
               
            </tr>
            <tr>
                 <td class="label" style="padding-right: 50px; vertical-align: top;">
                    <asp:Label ID="lblJobStatus" runat="server" Text="Job Status"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="ddlJobStatus" runat="server" Width="155">
                        <asp:ListItem Selected="True" Text="All Jobs" Value="X"></asp:ListItem>
                        <asp:ListItem Text="Proforma Jobs" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Pending Approval" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Approved Jobs" Value="O"></asp:ListItem>
                        <asp:ListItem Text="Closed Jobs" Value="C"></asp:ListItem>
                        
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
