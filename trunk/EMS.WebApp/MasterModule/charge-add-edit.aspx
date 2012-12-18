﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="charge-add-edit.aspx.cs" MasterPageFile="~/Site.Master" Inherits="EMS.WebApp.MasterModule.charge_add_edit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div>
        <div id="headercaption">
            ADD / EDIT CHARGE</div>
        <center>
            <fieldset style="width: 85%;">
                <legend>Add / Edit Charge</legend>
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td style="width: 150px;">
                            Effective Date<span class="errormessage1">*</span> :
                        </td>
                        <td style="width: 375px;">
                            <asp:TextBox ID="txtEffectDate" runat="server" Width="250"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtEffectDate"
                                PopupPosition="BottomLeft" TargetControlID="txtEffectDate">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Please select date"
                                ControlToValidate="txtEffectDate" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="rfvDate">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            Currency<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCurrency" runat="server" Width="255">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCurrency" runat="server" ErrorMessage="Please select currency"
                                ControlToValidate="ddlCurrency" Display="None" ValidationGroup="vgCharge" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="rfvCurrency">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name of Charge<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtChargeName" runat="server" Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvChargeTitle" runat="server" ErrorMessage="Please enter charge title"
                                ControlToValidate="txtChargeName" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="rfvChargeTitle">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            Principle Sharing<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbPrincipleSharing" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rdbPrincipleSharing_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvPS" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbPrincipleSharing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="rfvPS">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Import / Export<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlImportExportGeneral" runat="server" Width="255">
                                <asp:ListItem Text="Import" Value="I"></asp:ListItem>
                                <asp:ListItem Text="Export" Value="E"></asp:ListItem>
                                <asp:ListItem Text="General" Value="G"></asp:ListItem>
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                        </td>
                        <td>
                            washing charge<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbWashing" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" OnSelectedIndexChanged="rdbWashing_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Charge Type<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlChargeType" runat="server" Width="255">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvChargeType" runat="server" ErrorMessage="Please select charge type"
                                Display="None" ControlToValidate="ddlChargeType" ValidationGroup="vgCharge" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="rfvChargeType">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            Service Tax<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbServiceTaxApplicable" runat="server"
                                Width="255" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvServiceTax" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbServiceTaxApplicable" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" TargetControlID="rfvServiceTax">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Display Order :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDisplayOrder" runat="server" Width="250"></asp:TextBox>
                        </td>
                        <td>
                            Allow Rate Change<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbRateChange"  runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvRateChange" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbRateChange" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" TargetControlID="rfvRateChange">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Line<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLine" runat="server" Width="255">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLine" runat="server" ErrorMessage="Please select line"
                                Display="None" ControlToValidate="ddlLine" InitialValue="0" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="rfvLine">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            Is Freight Component ?<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbFreightComponent" AutoPostBack="true" runat="server"
                                RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvFreight" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbFreightComponent" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" TargetControlID="rfvFreight">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="height: 80px;">
                            <asp:HiddenField ID="hdnChargeID" runat="server" Value="0" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgCharge" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClick="btnBack_Click" />
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="dgChargeRates" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                OnItemCommand="dgChargeRates_ItemCommand" OnItemDataBound="dgChargeRates_ItemDataBound"
                                OnRowCommand="dgChargeRates_RowCommand" OnRowDataBound="dgChargeRates_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnLocationId" runat="server" Value='<%# Eval("LocationId")%>' />
                                            <%# (Eval("LocationId").ToString() == "0" ? string.Empty : ddlMLocation.Items.FindByValue(Eval("LocationId").ToString()).Text)%></ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlFLocation" runat="server" Width="120" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlLocationID_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="Please select location"
                                                Display="None" ValidationGroup="vgGridFooter" ControlToValidate="ddlFLocation"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvLocation">
                                            </cc1:ValidatorCalloutExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Terminal">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnTerminalId" runat="server" Value='<%# Eval("TerminalId")%>' />
                                            <%# (Eval("TerminalId").ToString() == "0" ? string.Empty : ddlMTerminal.Items.FindByValue(Eval("TerminalId").ToString()).Text)%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlFTerminal" runat="server" Width="120">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Washing Type">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnWashingTypeId" runat="server" Value='<%# Eval("WashingType")%>' />
                                            <%# (Eval("WashingType").ToString() == "0" ? string.Empty : ddlMWashingType.Items.FindByValue(Eval("WashingType").ToString()).Text )%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlFWashingType" runat="server" Width="120">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Low">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLow" runat="server" Text='<%# Eval("Low")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLow" runat="server" Width="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLow" runat="server" ErrorMessage="Please enter low slab"
                                                Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtLow"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvLow">
                                            </cc1:ValidatorCalloutExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="High">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHigh" runat="server" Text='<%# Eval("High")%>'></asp:Label></ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtHigh" runat="server" Width="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvHigh" runat="server" ErrorMessage="Please enter high slab"
                                                Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtHigh"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvHigh">
                                            </cc1:ValidatorCalloutExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate / BL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRatePerBl" runat="server" Text='<%# Eval("RatePerBL")%>'></asp:Label></ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRatePerBL" runat="server" Width="90" Text="0.0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRatePerBl" runat="server" ErrorMessage="Please enter rate / BL"
                                                Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRatePerBL"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfvRatePerBl">
                                            </cc1:ValidatorCalloutExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate / TEU">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRatePerTEU" runat="server" Text='<%# Eval("RatePerTEU")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRatePerTEU" runat="server" Width="90" Text="0.0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRatePerTEU" runat="server" ErrorMessage="Please enter rate"
                                                Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRatePerTEU"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="rfvRatePerTEU">
                                            </cc1:ValidatorCalloutExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate / FEU">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRatePerFEU" runat="server" Text='<%# Eval("RatePerFEU")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRateperFEU" runat="server" Width="90" Text="0.0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRatePerFEU" runat="server" ErrorMessage="Please enter rate"
                                                Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRateperFEU"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="rfvRatePerFEU">
                                            </cc1:ValidatorCalloutExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sharing / BL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSharingBL" runat="server" Text='<%# Eval("SharingBL")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtSharingBL" runat="server" Width="90" Text="0.0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSharingBL" runat="server" ErrorMessage="Please enter shared rate"
                                                Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtSharingBL"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="rfvSharingBL">
                                            </cc1:ValidatorCalloutExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sharing / TEU">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSharingTEU" runat="server" Text='<%# Eval("SharingTEU")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtSharingTEU" runat="server" Width="90" Text="0.0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSharingTEU" runat="server" ErrorMessage="Please enter shared rate"
                                                Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtSharingTEU"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="rfvSharingTEU">
                                            </cc1:ValidatorCalloutExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sharing / FEU">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSharingFEU" runat="server" Text='<%# Eval("SharingFEU")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtSharingFEU" runat="server" Width="90" Text="0.0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSharingFEU" runat="server" ErrorMessage="Please enter shared rate"
                                                Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtSharingFEU"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="rfvSharingFEU">
                                            </cc1:ValidatorCalloutExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                        FooterStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("ChargesRateID")%>' />
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="Edit">Edit</asp:LinkButton>&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument="Delete">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:HiddenField ID="hdnFId" runat="server" Value="0" />
                                            <asp:Button ID="lnkAdd" runat="server" CommandArgument="Save" Text="Save" Font-Bold="true"
                                                ValidationGroup="vgGridFooter" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="GrayText" />
                                <RowStyle Wrap="true" />
                                <FooterStyle BackColor="GrayText" HorizontalAlign="Center" />
                            </asp:GridView>
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