<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Advance.aspx.cs"
    Inherits="EMS.WebApp.Forwarding.Transaction.Advance" MasterPageFile="~/Site.Master"
    Title=":: Liner :: Add / Edit Advance" %>

<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CheckTotal() {

            var ch = document.getElementById('<%= txtChequeAmt.ClientID %>').value;
            if (ch == "" || ch <= 0) {
                document.getElementById('<%= txtChqNo.ClientID %>').disabled = true;
                document.getElementById('<%= txtBankName.ClientID %>').disabled = true;
                document.getElementById('<%= txtChqDate.ClientID %>').disabled = true;

                document.getElementById('<%= txtChqNo.ClientID %>').value = "";
                document.getElementById('<%= txtBankName.ClientID %>').value = "";
                document.getElementById('<%= txtChqDate.ClientID %>').value = "";
            }
            else {
                document.getElementById('<%= txtChqNo.ClientID %>').disabled = false;
                document.getElementById('<%= txtBankName.ClientID %>').disabled = false;
                document.getElementById('<%= txtChqDate.ClientID %>').disabled = false;
            }

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

            if ((v1 + v2 + v3) > 0) {
                document.getElementById('<%= txtCurrentAmount.ClientID %>').value = v1 + v2 + v3;
            }
            else
                document.getElementById('<%= txtCurrentAmount.ClientID %>').value = "";



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
        <asp:Literal ID="litHeader" runat="server"></asp:Literal>      </div>
<%--        ADD / EDIT ADVACE</div>--%>
    <center>
        <fieldset style="width: 60%;">
            <legend>Add / Edit Advance</legend>
            <table border="0" width="100%">
                <tr>
                    <td style="width: 15%;">
                        Advance No:
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
                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Please enter receipt date"
                            ControlToValidate="txtDate" ValidationGroup="vgMoneyRecpt" Display="None"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvDate">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Job No:
                    </td>
                    <td>
                        <asp:HiddenField ID="hdnJobNo" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnJobID" runat="server" Value="0" />
                        <asp:TextBox ID="txtJobNo" runat="server" CssClass="textboxuppercase" MaxLength="30"
                            Width="150px" AutoPostBack="True" Enabled="false"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="errormessage" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        Job Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobDate" runat="server" CssClass="textboxuppercase" MaxLength="100"
                            Width="150px" Enabled="False"></asp:TextBox>
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
            </table>
        </fieldset>
        <fieldset style="width: 60%;">
            <legend>Payment Details</legend>
            <table style="width: 100%;" border="0">
               
                
                <tr>
                    <td>
                        Cheque Payment
                    </td>
                    <td>
                        <%-- <cc2:CustomTextBox ID="txtChequeAmt" runat="server" Width="150" Type="Decimal" MaxLength="13"
                            Precision="10" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>--%>
                        <asp:TextBox ID="txtChequeAmt" runat="server" Width="180" MaxLength="13" Style="text-align: right;"
                            onblur="CheckTotal();" AutoPostBack="false"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtChequeAmt"
                            FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        Cash Payment:
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
                        <asp:TextBox ID="txtChqNo" runat="server" Width="180px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        Cheque Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtChqDate" Width="150" runat="server" Enabled="false"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChqDate">
                        </cc1:CalendarExtender>

<%--                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtChqDate"
                            TargetControlID="txtChqDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Bank Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtBankName" Width="180px" runat="server" Enabled="false" 
                            style="text-transform:uppercase;"></asp:TextBox>
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
                    </td>
                    <td>
                    </td>
                    <td>
                        Net Amount:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <cc2:CustomTextBox ID="txtCurrentAmount" runat="server" Width="150" Type="Decimal"
                            MaxLength="13" Precision="10" Scale="2" Style="text-align: right;" ReadOnly="true"></cc2:CustomTextBox>
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
