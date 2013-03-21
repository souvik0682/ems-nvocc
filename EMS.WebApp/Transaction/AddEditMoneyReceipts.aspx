<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditMoneyReceipts.aspx.cs"
    Inherits="EMS.WebApp.Transaction.AddEditUser" MasterPageFile="~/Site.Master"
    Title=":: Liner :: Add / Edit Money Receipts" %>

<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage">
            </div>
        </div>
    </div>
    <div id="headercaption">
        ADD / EDIT MONEY RECEIPTS</div>
    <center>
        <fieldset style="width: 60%;">
            <legend>Add / Edit Money Receipts</legend>
            <table border="0" width="100%">
                <tr>
                    <td style="width: 15%;">
                        M/R No:<span class="errormessage1">*</span>
                    </td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txtMRNo" runat="server" CssClass="textboxuppercase" MaxLength="10"
                            Width="150px" Enabled="false"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvFName" runat="server" CssClass="errormessage" ControlToValidate="txtFName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td style="width: 10%;">
                        Date:<span class="errormessage1">*</span>
                    </td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txtDate" runat="server" Width="150px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        BL No:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBLNo" runat="server" CssClass="textboxuppercase" MaxLength="30"
                            Width="150px" AutoPostBack="True" OnTextChanged="txtBLNo_TextChanged" Enabled="false"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="errormessage" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        Line/NVOCC:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNvocc" runat="server" CssClass="textboxuppercase" MaxLength="100"
                            Width="150px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Export/Import:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlExportImport" runat="server" Enabled="false" Width="155px">
                            <asp:ListItem Value="0">None</asp:ListItem>
                            <asp:ListItem Value="E">Export</asp:ListItem>
                            <asp:ListItem Value="I">Import</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="rfvRole" runat="server" CssClass="errormessage" ControlToValidate="ddlRole" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        Location:*
                    </td>
                    <td>
                        <asp:TextBox ID="txtLocation" runat="server" CssClass="textboxuppercase" MaxLength="100"
                            Width="150px" Enabled="False"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvLoc" runat="server" CssClass="errormessage" ControlToValidate="ddlLoc" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset style="width: 60%;">
            <legend>Add/ Edit Charge Details</legend>
            <table style="width: 100%;" border="0">
                <tr>
                    <td style="width: 15%;">
                        Invoice No.:
                    </td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txtInvoiceNo" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        Invoice Amount:
                    </td>
                    <td style="width: 15%;">
                        <cc2:CustomTextBox ID="txtInvoiceAmount" runat="server" Width="150" Type="Decimal"
                            MaxLength="13" Precision="10" Scale="2" Style="text-align: right;" Enabled="false"></cc2:CustomTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Invoice Type:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvoiceType" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        Pay Amount:
                    </td>
                    <td>
                        <cc2:CustomTextBox ID="txtPayAmount" runat="server" Width="150" Type="Decimal" MaxLength="13"
                            Precision="10" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Invoice Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textboxuppercase" MaxLength="100"
                            Width="150px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        TDS:
                    </td>
                    <td>
                        <cc2:CustomTextBox ID="txtTDS" runat="server" Width="150" Type="Decimal" MaxLength="13"
                            Precision="10" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Net Amount:
                    </td>
                    <td>
                        <cc2:CustomTextBox ID="txtCurrentAmount" runat="server" Width="150" Type="Decimal"
                            MaxLength="13" Precision="10" Scale="2" Style="text-align: right;" Enabled="false"></cc2:CustomTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <div>
            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button
                ID="btnBack" runat="server" CssClass="button" Text="Back" OnClick="btnBack_Click" />
        </div>
    </center>
</asp:Content>
