<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageInvoice.aspx.cs" Inherits="EMS.WebApp.Transaction.ManageInvoice" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageInvoice.aspx.cs" Inherits="EMS.WebApp.Transaction.ManageInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 91px;
        }
        .style2
        {
            width: 239px;
        }
        .style3
        {
            height: 22px;
        }
        .style4
        {
            width: 239px;
            height: 22px;
        }
        .style5
        {
            width: 123px;
        }
        .style6
        {
            width: 123px;
            height: 22px;
        }
        .style7
        {
            width: 91px;
            height: 26px;
        }
        .style8
        {
            width: 239px;
            height: 26px;
        }
        .style9
        {
            width: 123px;
            height: 26px;
        }
        .style10
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            ADD/EDIT INVOICE
        </div>
        <center>
            <fieldset style="width: 85%;">
                <legend>Add / Edit Invoice</legend>
                <table style="width: 100%;">
                    <tr>
                        <td class="style7">
                            Invoice Type<span class="errormessage1">*</span>
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="ddlInvoiceType" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvInvoiceType" ControlToValidate="ddlInvoiceType"
                                runat="server" ErrorMessage="*Required" Font-Bold="true" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style9">
                        </td>
                        <td class="style10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Location<span class="errormessage1">*</span>
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlLocation" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="ddlLocation" runat="server"
                                ErrorMessage="*Required" InitialValue="0" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style5">
                            Line / NVOCC<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLineNvocc" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLineNvocc" runat="server" ControlToValidate="ddlLineNvocc"
                                ErrorMessage="*Required" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Invoice No<span class="errormessage1">*</span>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtInvoiceNo" runat="server" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceNo" ControlToValidate="txtInvoiceNo" runat="server"
                                ErrorMessage="*Required" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style5">
                            Invoice Date<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceDate" runat="server" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate"
                                ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            B/L No<span class="errormessage1">*</span>
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlBLno" runat="server" OnSelectedIndexChanged="ddlBLno_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBLno" runat="server" ControlToValidate="ddlBLno"
                                ErrorMessage="*Required" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style5">
                            B/L Date<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBLdate" runat="server" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBLdate" runat="server" ControlToValidate="txtBLdate"
                                ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            CHA ID<span class="errormessage1">*</span>
                        </td>
                        <td class="style2">
                            &nbsp;<asp:DropDownList ID="ddlCHAid" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCHAid" runat="server" ControlToValidate="ddlCHAid"
                                ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style5">
                            Account for
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdblAccountFor" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Text="Consignee" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Notify" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Exchange Rate<span class="errormessage1">*</span>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtExchangeRate" runat="server" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvExchangeRate" runat="server" ControlToValidate="txtExchangeRate"
                                ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style5">
                            Total amount
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalAmount" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            All-in-freight
                        </td>
                        <td class="style4">
                            <asp:RadioButtonList ID="rdblAllinFreight" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="style6">
                            &nbsp;
                        </td>
                        <td class="style3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Gross Weight TON
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtGrossWeightTON" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="style5">
                            Volume (CBM)
                        </td>
                        <td>
                            <asp:TextBox ID="txtVolume" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            TEU
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtTEU" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="style5">
                            FEU
                        </td>
                        <td>
                            <asp:TextBox ID="txtFFU" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-top: 30px; border: none;">
                            <asp:UpdatePanel ID="UpdatePanelInvoice" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgInvoiceChargeRates" runat="server" AutoGenerateColumns="false"
                                        ShowFooter="false" OnItemCommand="dgChargeRates_ItemCommand" OnItemDataBound="dgChargeRates_ItemDataBound"
                                        OnRowCommand="dgChargeRates_RowCommand" OnRowDataBound="dgChargeRates_RowDataBound"
                                        OnSelectedIndexChanged="dgInvoiceChargeRates_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Charge Name">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnChargeID" runat="server" Value='<%# Eval("ChargeID")%>' />
                                                    <%# (Eval("ChargeID").ToString() == "0" ? string.Empty : ddlMLocation.Items.FindByValue(Eval("LocationId").ToString()).Text)%></ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddlFChargeName" runat="server" Width="120" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlChargeName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvChargeName" runat="server" ErrorMessage="Please select a Charge Name"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="ddlFChargeName"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvChargeName">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    Charge Name
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Terminal">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTerminalId" runat="server" Value='<%# Eval("TerminalId")%>' />
                                                    <%# (Eval("TerminalId").ToString() == "0" ? string.Empty : ddlMTerminal.Items.FindByValue(Eval("TerminalId").ToString()).Text)%>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddlFTerminal" runat="server" Width="120">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    Terminal
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Washing Type">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnWashingTypeId" runat="server" Value='<%# Eval("WashingType")%>' />
                                                    <%# (Eval("WashingType").ToString() == "0" ? string.Empty : ddlMWashingType.Items.FindByValue(Eval("WashingType").ToString()).Text )%>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddlFWashingType" runat="server" Width="120" Enabled="false">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    Washing Type
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/BL" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerBl" runat="server" Text='<%# Eval("RatePerBL")%>'></asp:Label></ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRatePerBL" runat="server" Width="90" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    <br />
                                                    Rate/BL
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/TEU" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerTEU" runat="server" Text='<%# Eval("RatePerTEU")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRatePerTEU" runat="server" Width="90" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFRateTeu" runat="server" Text="Rate/TEU"></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/FEU" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerFEU" runat="server" Text='<%# Eval("RatePerFEU")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRateperFEU" runat="server" Width="90" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFRateFeu" runat="server" Text="Rate/FEU"></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/CBM" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerCBM" runat="server" Text='<%# Eval("RatePerCBM")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRatePerCBM" runat="server" Width="90" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFRatePerCBM" runat="server" Text="Rate/CBM"></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/Ton" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerTon" runat="server" Text='<%# Eval("RatePerTon")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRatePerTon" runat="server" Width="90" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFRatePerTon" runat="server" Text="Rate/Ton"></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="USD" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUSD" runat="server" Text='<%# Eval("USD")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtUSD" runat="server" Width="90" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFUSD" runat="server" Text="USD"></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Amount" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrossAmount" runat="server" Text='<%# Eval("GrossAmount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtGrossAmount" runat="server" Width="90" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFGrossAmount" runat="server" Text="Gross Amount"></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Tax" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblService" runat="server" Text='<%# Eval("ServiceTax")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtServiceTax" runat="server" Width="90" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblServiceTax" runat="server" Text="Service Tax"></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtTotal" runat="server" Width="90" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFTotal" runat="server" Text="Total"></asp:Label>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                                FooterStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnSlno" runat="server" Value='<%# Eval("SlNo")%>' />
                                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("ChargesRateID")%>' />
                                                    <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument="Edit" ImageUrl="~/Images/edit.png"
                                                        Height="16" Width="16" ToolTip="Edit" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:HiddenField ID="hdnFSlno" runat="server" Value="-1" />
                                                    <asp:HiddenField ID="hdnFId" runat="server" Value="0" />
                                                    <asp:Button ID="lnkAdd" runat="server" CommandArgument="Save" Text="Add" Font-Bold="true"
                                                        ValidationGroup="vgGridFooter" Height="24" />
                                                    <br />
                                                    <br />
                                                    Edit
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                                FooterStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument="Delete" ImageUrl="~/Images/remove.png"
                                                        Height="16" Width="16" ToolTip="Delete" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:Button ID="lnkCancel" runat="server" CommandArgument="Cancel" Text="Cancel"
                                                        Font-Bold="true" Height="24" />
                                                    <br />
                                                    <br />
                                                    Delete
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="GrayText" />
                                        <RowStyle Wrap="true" />
                                        <FooterStyle BackColor="GrayText" HorizontalAlign="Center" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnChargeID" runat="server" Value="0" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgCharge" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClick="btnBack_Click" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td colspan="4">
                            <asp:DropDownList ID="ddlMLocation" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlMTerminal" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlMWashingType" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
</asp:Content>
