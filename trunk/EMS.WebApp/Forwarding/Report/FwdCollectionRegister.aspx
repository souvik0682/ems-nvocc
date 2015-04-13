<%@ Page Language="C#" MasterPageFile="~/Site.Master" CodeBehind="FwdCollectionRegister.aspx.cs" Inherits="EMS.WebApp.Forwarding.Report.FwdCollectionRegister" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .style1
        {
            color: #000000;
            width: 86px;
        }
        .style2
        {
            color: #000000;
            width: 62px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
  <div id="headercaption">
        CREDITOR INVOICE</div>
<center>
    <div style="padding-top: 10px;">
        <fieldset style="width:964px;height:65px;">
            <table>
                <tr>
                    <td class="style1" style="padding-right:5px;vertical-align:top;">
                        From Date:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:TextBox ID="txtFromDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox>
                        <cc1:CalendarExtender ID="cbeFromDt" runat="server" TargetControlID="txtFromDt" Format="dd-MM-yyyy" />
                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDt" Display="Dynamic" 
                        ErrorMessage="From Date is required" ValidationGroup="vgReport" CssClass="errormessage"></asp:RequiredFieldValidator>
                    </td>
                    <td class="style2" style="padding-right:5px;vertical-align:top;">
                        To Date:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:TextBox ID="txtToDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox>
                        <cc1:CalendarExtender ID="cbeToDt" runat="server" TargetControlID="txtToDt" Format="dd-MM-yyyy"/>
                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDt" Display="Dynamic" 
                        ErrorMessage="To Date is required" ValidationGroup="vgReport" CssClass="errormessage"></asp:RequiredFieldValidator>
                    </td>
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

                    <%--<td class="label" style="padding-right:5px;vertical-align:top;">
                        Creditor:
                    </td>
                    
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:DropDownList ID="ddlCreditor" runat="server"
                            DataSourceID="CreditorDataSource" DataTextField="CreditorName" 
                            DataValueField="CreditorId" AppendDataBoundItems="true">
                            <asp:ListItem Value="0" Text="--All--"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="CreditorDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DbConnectionString %>" 
                            SelectCommand="select p.pk_fwPartyID [CreditorId], p.PartyName [CreditorName] 
from fwd.mstparty p inner join fwd.mstPartyType pt on p.PartyType = pt.pk_PartyTypeID
where AssociatedWith &lt;&gt; 'D'">
                        </asp:SqlDataSource>
                    </td>--%>
                    <td style="vertical-align:top;">    
                        <asp:Button ID="btnReport" runat="server" Text="Show Report" CssClass="button" 
                            ValidationGroup="vgReport" onclick="btnReport_Click"/>
                    </td>
                </tr>
            </table>
        </fieldset>
        <div style="padding-left:5px;width:86%;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="RDLC\fwdCollRegister.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odsCollReg" Name="CollRegDs" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="odsCollReg" runat="server" 
                SelectMethod="GetFwdCollRegister" TypeName="EMS.BLL.ReportBLL" 
                onselecting="odsCollReg_Selecting" 
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Name="LocationID" Type="Int32" />
                    <asp:Parameter Name="StartDate" Type="DateTime" />
                    <asp:Parameter Name="EndDate" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>    
    </div>
</center>
</asp:Content>
