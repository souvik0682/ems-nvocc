<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ImpInvoiceOutstanding.aspx.cs" Inherits="EMS.WebApp.Reports.ImpInvoiceOutstanding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <%@ import namespace="EMS.Utilities" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }
    </script>
    <style type="text/css">
        .custtable
        {
            width: 100%;
        }
        .custtable td
        {
            vertical-align: top;
        }
    </style>
    <asp:HiddenField ID="hdnReturn" runat="server" />
    <div id="headercaption">
        OUTSTANDING INVOICES</div>
    <center>
        <fieldset style="padding: 5px; width: 55%">
            <table style="width: 100%" cellpadding="1" cellspacing="0">
                <tr runat="server" id="tr1">
                    <td>Report For:<span class="errormessage">*</span></td>
                    <td align="left" style="width: 15%">  
                        <asp:DropDownList AutoPostBack="true" ID="ddlReportType" runat="server" 
                        onselectedindexchanged="ddlReportType_SelectedIndexChanged"  >
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                             <asp:ListItem Text="Import" Value="1"></asp:ListItem>
                              <asp:ListItem Text="Export" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlReportType" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlReportType" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="main">
                    <td align="left" style="width: 18%">
                        Location:<span class="errormessage">*</span>
                    </td>
                    <td align="left" style="width: 20%">
                        <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLine_SelectedIndexChanged"
                            Width="150">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlLine" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="width: 10%">
                        <asp:Label ID="lblLine" runat="server" Text="Line"></asp:Label>:<span class="errormessage"
                            style="width: 10%">*</span>
                    </td>
                    <td align="left" style="width: 30%">
                        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" Width="150"
                            OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlLocation" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="trCar">
                    <td>
                        Vessel:<span class="errormessage">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true" Width="150" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlVessel" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        Voyage:<span class="errormessage">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlVoyage" runat="server" Width="150">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlVoyage" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="4" align="left" style="padding: 5px 5px 5px 0">
                        <asp:Button ID="btnReport" runat="server" Text="Generate Excel" ValidationGroup="Report"
                            OnClick="btnReport_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>

