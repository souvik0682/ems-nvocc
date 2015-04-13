<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OutstandingAnalysis.aspx.cs" Inherits="EMS.WebApp.Forwarding.Report.OutstandingAnalysis" %>

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

        return true;
  
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">

    <center>

 <div id="headercaption">
         OUTSTANDING ANALYSIS </div>
    <center>
        <fieldset style="width:800px; ">
            <legend> Outstanding Analysis </legend>
    
            <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="3" width="100%">
            <tr id="DateRange" runat="server">
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    <asp:Label ID="lblStartDt" runat="server" Text="As On Date"></asp:Label>
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
                    <asp:Label ID="lblOutType" runat="server" Text="Report Type"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReportType" runat="server" Width="200">
                        <asp:ListItem Selected="True" Text="Summary" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Details" Value="D"></asp:ListItem>
                    </asp:DropDownList>
                </td>
               
            </tr>
            <tr>
                <td>
                    Party Type
                </td>
                <td>
                    <asp:DropDownList ID="ddlPartyType" runat="server" CssClass="dropdownlist"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlPartyType_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>                   
                </td>
                <td>
                    Party:
                </td>
                <td>
                    <asp:DropDownList ID="ddlParty" runat="server" CssClass="dropdownlist" Width="200px">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
               
            </tr>

            <tr>
                <td style="vertical-align: top;">
                    <asp:Button ID="btnShow" runat="server" Text="Submit" CssClass="button" OnClick="btnShow_Click" />
                </td>
            </tr>
    
            </table>
            </ContentTemplate>

        </fieldset>
        <div style="padding-left:5px;width:86%;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="RDLC\ForwardingPartywiseOutstanding.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odsOutstanding" Name="OutstandingDs" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="odsOutstanding" runat="server" 
                SelectMethod="GetOutstanding" TypeName="EMS.BLL.ReportBLL" 
                onselecting="odsParty_Selecting" 
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Name="Creditor" Type="Int32" />
                    <asp:Parameter Name="FromDate" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>    
    </center>

</center>
</asp:Content>
