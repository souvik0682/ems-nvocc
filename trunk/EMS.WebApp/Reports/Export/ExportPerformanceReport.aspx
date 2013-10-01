<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExportPerformanceReport.aspx.cs" Inherits="EMS.WebApp.Transaction.ExportPerformanceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
         EXPORT PERFORMANCE REPORT </div>
    <center>
    <fieldset style="width: 964px; height: 85x;">
        <table>
            <tr>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Location:<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlLoc" runat="server" AutoPostBack="True" Width="135px">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="rfvLoc" runat="server" ErrorMessage="Please select Location"
                        ControlToValidate="ddlLoc"  InitialValue="0"  ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvLoc"
                        WarningIconImageUrl="">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Line / NVOCC:<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="True" Width="135px">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="rfvLine" runat="server" ErrorMessage="Please select Line"
                       ControlToValidate="ddlLine"
                        InitialValue="0" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvLine"
                        WarningIconImageUrl="">
                    </cc1:ValidatorCalloutExtender>
                </td>               
           
            </tr>
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
                <td style="vertical-align: top;">
                    <asp:Button ID="btnShow" runat="server" Text="Generate Excel" CssClass="button" OnClick="btnShow_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    </center>
</asp:Content>
