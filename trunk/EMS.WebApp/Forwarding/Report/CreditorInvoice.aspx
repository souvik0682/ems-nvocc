<%@ Page Language="C#" MasterPageFile="~/Site.Master" CodeBehind="CreditorInvoice.aspx.cs" Inherits="EMS.WebApp.Forwarding.Report.CreditorInvoice" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
  <div id="headercaption">
        CREDITOR INVOICE</div>
<center>
    <div style="padding-top: 10px;">
        <fieldset style="width:964px;height:65px;">
            <table>
                <tr>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        From Date:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:TextBox ID="txtFromDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox>
                        <cc1:CalendarExtender ID="cbeFromDt" runat="server" TargetControlID="txtFromDt" Format="dd-MM-yyyy" />
                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFromDt" Display="Dynamic" 
                        ErrorMessage="From Date is required" ValidationGroup="vgReport" CssClass="errormessage"></asp:RequiredFieldValidator>
                    </td>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        To Date:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:TextBox ID="txtToDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox>
                        <cc1:CalendarExtender ID="cbeToDt" runat="server" TargetControlID="txtToDt" Format="dd-MM-yyyy"/>
                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtToDt" Display="Dynamic" 
                        ErrorMessage="To Date is required" ValidationGroup="vgReport" CssClass="errormessage"></asp:RequiredFieldValidator>
                    </td>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
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
                    </td>
                    <td style="vertical-align:top;">    
                        <asp:Button ID="btnReport" runat="server" Text="Show Report" CssClass="button" 
                            ValidationGroup="vgReport" onclick="btnReport_Click"/>
                    </td>
                </tr>
            </table>
        </fieldset>
        <div style="padding-left:5px;width:980px;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="RDLC\CredInvoice1.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="odsCredInv" Name="CredInvDs" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>    
            <asp:ObjectDataSource ID="odsCredInv" runat="server" 
                SelectMethod="GetCredInvoice" TypeName="EMS.BLL.ReportBLL">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlCreditor" DefaultValue="0" 
                        Name="CreditorId" PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="txtFromDt" DefaultValue="" Name="StartDate" 
                        PropertyName="Text" Type="DateTime" />
                    <asp:ControlParameter ControlID="txtToDt" Name="EndDate" PropertyName="Text" 
                        Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>    
    </div>
</center>
</asp:Content>
