<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditMoneyReceipts.aspx.cs"
    Inherits="EMS.WebApp.Transaction.AddEditUser" MasterPageFile="~/Site.Master"
    Title=":: Liner :: Add / Edit Money Receipts" %>

<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CheckTotal() {

            var v1 = document.getElementById('<%= txtCashAmt.ClientID %>').value;
            var v2 = document.getElementById('<%= txtChequeAmt.ClientID %>').value;
            var v3 = document.getElementById('<%= txtTDS.ClientID %>').value;

            if (v1 == "") {
                v1 = "0";
            }
            if (v2 == "") {
                v2 = "0";
            }
            if (v3 == "") {
                v3 = "0";
            }

            v1 = parseFloat(v1, 10);
            v2 = parseFloat(v2, 10);
            v3 = parseFloat(v3, 10);

            document.getElementById('<%= txtCurrentAmount.ClientID %>').value = v1 + v2 + v3;

            if (parseFloat(document.getElementById('<%= txtPendingAmt.ClientID %>').value, 10) < v1 + v2 + v3) {
                alert("Pay amount can not be greater than penfing amount");
                return false;
            }
            else
                return true;


        }


        function checkDate(sender, args) {
            var invDate = document.getElementById('<%= hdnInvDt.ClientID %>').value;

            if (sender._selectedDate < new Date(invDate)) {
                alert("Date should be greater than Invoice date!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                //sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                sender._textbox.set_Value("")
            }
        }

    
    </script>
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
                        M/R No:
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
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"
                            OnClientDateSelectionChanged="checkDate">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Please enter receipt date"
                            ControlToValidate="txtDate" ValidationGroup="vgMoneyRecpt" Display="None"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvDate">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        BL No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtBLNo" runat="server" CssClass="textboxuppercase" MaxLength="30"
                            Width="150px" AutoPostBack="True" OnTextChanged="txtBLNo_TextChanged" Enabled="false"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="errormessage" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        Line/NVOCC:
                    </td>
                    <td>
                        <asp:HiddenField ID="hdnNvoccId" runat="server" Value="0" />
                        <asp:TextBox ID="txtNvocc" runat="server" CssClass="textboxuppercase" MaxLength="100"
                            Width="150px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Export/Import:
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
                        Location:
                    </td>
                    <td>
                        <asp:HiddenField ID="hdnLocationID" runat="server" Value="0" />
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
                        Pending Amount
                    </td>
                    <td>
                        <cc2:CustomTextBox ID="txtPendingAmt" runat="server" Width="150" Type="Decimal" MaxLength="13"
                            Precision="10" Scale="2" Style="text-align: right;" onblur="AddTotal();" Enabled="false"></cc2:CustomTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Invoice Date:
                    </td>
                    <td>
                        <asp:HiddenField ID="hdnInvDt" runat="server" />
                        <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textboxuppercase" MaxLength="100"
                            Width="150px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        Pay Amount in Cash:
                    </td>
                    <td>
                        <%--<cc2:CustomTextBox ID="txtCashAmt" runat="server" Width="150" Type="Decimal" MaxLength="13"
                            Precision="10" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>--%>
                        <asp:TextBox ID="txtCashAmt" runat="server" Width="150" MaxLength="13" Style="text-align: right;"
                            onblur="CheckTotal();"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCashAmt"
                            FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cheque No.
                    </td>
                    <td>
                        <asp:TextBox ID="txtChqNo" runat="server" Width="150"></asp:TextBox>
                    </td>
                    <td>
                        Pay Amount in Cheque
                    </td>
                    <td>
                        <%-- <cc2:CustomTextBox ID="txtChequeAmt" runat="server" Width="150" Type="Decimal" MaxLength="13"
                            Precision="10" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>--%>
                        <asp:TextBox ID="txtChequeAmt" runat="server" Width="150" MaxLength="13" Style="text-align: right;"
                            onblur="CheckTotal();" AutoPostBack="true" OnTextChanged="txtChequeAmt_TextChanged"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtChequeAmt"
                            FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Bank Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtBankName" Width="150" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        TDS:
                    </td>
                    <td>
                        <%--  <cc2:CustomTextBox ID="txtTDS" runat="server" MaxLength="13" Precision="10" Scale="2"
                            Style="text-align: right;" Type="Decimal" Width="150"></cc2:CustomTextBox>--%>
                        <asp:TextBox ID="txtTDS" runat="server" Width="150" MaxLength="13" Style="text-align: right;"
                            onblur="CheckTotal();"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtTDS"
                            FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cheque Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtChqDate" Width="150" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtChqDate"
                            TargetControlID="txtChqDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td>
                        Net Amount:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <cc2:CustomTextBox ID="txtCurrentAmount" runat="server" Width="150" Type="Decimal"
                            MaxLength="13" Precision="10" Scale="2" Style="text-align: right;" Enabled="false"></cc2:CustomTextBox>
                        <asp:RequiredFieldValidator ID="rfvNetAmt" runat="server" ErrorMessage="Net Amount can not be blank"
                            ControlToValidate="txtCurrentAmount" ValidationGroup="vgMoneyRecpt" Display="None"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvNetAmt">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
        </fieldset>
        <div>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="vgMoneyRecpt" />&nbsp;&nbsp;<asp:Button
                ID="btnBack" runat="server" CssClass="button" Text="Back" OnClick="btnBack_Click" />
            &nbsp;
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </center>
</asp:Content>
