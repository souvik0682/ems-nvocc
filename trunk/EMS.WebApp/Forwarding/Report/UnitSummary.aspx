<%@ Page Language="C#" MasterPageFile="~/Site.Master" CodeBehind="UnitSummary.aspx.cs" Inherits="EMS.WebApp.Forwarding.Report.UnitSummary" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .style1
        {
            color: #000000;
            width: 100px;
        }
        .style2
        {
            color: #000000;
            width: 100px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
  <div id="headercaption">
        UNIT SUMMARY</div>
<center>
    <div style="padding-top: 10px;">
        <fieldset style="width:800px;height:65px;">
            <table>
                <tr>
                    <td class="style1" style="padding-right:8px;vertical-align:top;">
                        From Date:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:30px;vertical-align:top;">
                        <asp:TextBox ID="txtFromDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox>
                        <cc1:CalendarExtender ID="cbeFromDt" runat="server" TargetControlID="txtFromDt" Format="dd-MM-yyyy" />
                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDt" Display="Dynamic" 
                        ErrorMessage="From Date is required" ValidationGroup="vgReport" CssClass="errormessage"></asp:RequiredFieldValidator>
                    </td>
                    <td class="style2" style="padding-right:8px;vertical-align:top;">
                        To Date:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:30px;vertical-align:top;">
                        <asp:TextBox ID="txtToDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox>
                        <cc1:CalendarExtender ID="cbeToDt" runat="server" TargetControlID="txtToDt" Format="dd-MM-yyyy"/>
                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDt" Display="Dynamic" 
                        ErrorMessage="To Date is required" ValidationGroup="vgReport" CssClass="errormessage"></asp:RequiredFieldValidator>
                    </td>
                    <td style="vertical-align:top;">    
                        <asp:Button ID="btnReport" runat="server" Text="Show Report" CssClass="button" 
                            ValidationGroup="vgReport" onclick="btnReport_Click"/>
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
                    <asp:Label ID="lblError" runat="server" CssClass="errormessage"></asp:Label>
                </td>
                <td>
                    Line:<span class="errormessage">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLine" runat="server" CssClass="dropdownlist" TabIndex="60"
                        DataSourceID="LineDs" DataTextField="LineName" DataValueField="pk_fLineID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="LineDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                        SelectCommand="SELECT 0 [pk_fLineID], '-- All --' [LineName]
UNION
SELECT pk_fLineID, LineName FROM fwd.mstFline"></asp:SqlDataSource>
                    
                </td>
                </tr>
            </table>
        </fieldset>
        <div style="padding-left:5px;width:86%;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="RDLC\FwdUnitSummary.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odsRevSummary" Name="RevDs" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="odsRevSummary" runat="server" 
                SelectMethod="GetUnitSummary" TypeName="EMS.BLL.ReportBLL" 
                onselecting="odsRevSummary_Selecting" 
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Name="StartDate" Type="DateTime" />
                    <asp:Parameter Name="EndDate" Type="DateTime" />
                    <asp:Parameter Name="Line" Type="Int32" />
                    <asp:Parameter Name="Location" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>    
    </div>
</center>
</asp:Content>
