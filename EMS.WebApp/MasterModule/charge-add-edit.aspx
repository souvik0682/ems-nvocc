<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="charge-add-edit.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="EMS.WebApp.MasterModule.charge_add_edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("Please select advance date only!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                //sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                sender._textbox.set_Value("")
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div>
        <div id="headercaption">
            ADD / EDIT CHARGE</div>
        <center>
            <fieldset style="width: 90%;">
                <legend>Add / Edit Charge</legend>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td style="width: 18%;">
                                </td>
                                <td style="width: 15%;">
                                    Effective Date<span class="errormessage1">*</span> :
                                </td>
                                <td style="width: 20%;">
                                    <asp:TextBox ID="txtEffectDate" runat="server" Width="150" AutoCompleteType="None"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtEffectDate"
                                        PopupPosition="BottomLeft" TargetControlID="txtEffectDate" Format="dd/MM/yyyy"
                                        OnClientDateSelectionChanged="checkDate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Please select date"
                                        ControlToValidate="txtEffectDate" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="rfvDate">
                                    </cc1:ValidatorCalloutExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtEffectDate"
                                        FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars="/">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="width: 19%;">
                                    Terminal Required<span class="errormessage1">*</span>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbTerminalRequired" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rdbTerminalRequired_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvTerminalRequired" runat="server" ErrorMessage="Please select your choice"
                                        Display="None" ControlToValidate="rdbPrincipleSharing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="rfvTerminalRequired">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Location<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHeaderLocation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHeaderLocation_SelectedIndexChanged"
                                        Width="155">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvHeaderLocation" runat="server" ControlToValidate="ddlHeaderLocation"
                                        Display="None" ErrorMessage="Please select location" InitialValue="0" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvHeaderLocation">
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
                                </td>
                                <td>
                                    Line<span class="errormessage1"></span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLine" runat="server" Width="155">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <%--Washing Charge<span class="errormessage1">*</span> :--%>
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
                                    <asp:RadioButtonList ID="rdbWashing" AutoPostBack="true" runat="server" Style="display: none;"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdbWashing_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%-- <asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                        Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                                    </cc1:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Name of Charge<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtChargeName" runat="server" Width="150" Style="text-transform: uppercase;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvChargeTitle" runat="server" ErrorMessage="Please enter charge title"
                                        ControlToValidate="txtChargeName" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="rfvChargeTitle">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    Service Tax<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbServiceTaxApplicable" runat="server" Width="255" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
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
                                </td>
                                <td>
                                    Import / Export<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlImportExportGeneral" runat="server" Width="155">
                                        <asp:ListItem Text="IMPORT" Value="I"></asp:ListItem>
                                        <asp:ListItem Text="EXPORT" Value="E"></asp:ListItem>
                                        <asp:ListItem Text="GENERAL" Value="G"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Allow Rate Change<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbRateChange" runat="server" RepeatDirection="Horizontal"
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
                                </td>
                                <td>
                                    Charge Basis<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlChargeType" runat="server" Width="155" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlChargeType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvChargeType" runat="server" ErrorMessage="Please select charge type"
                                        Display="None" ControlToValidate="ddlChargeType" ValidationGroup="vgCharge" InitialValue="0"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="rfvChargeType">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <%--Is Freight Component ?<span class="errormessage1">*</span> :--%>
                                   Special Rate</td>
                                <td>
                                    <asp:CheckBox ID="chkSpecialRate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Display Order :
                                </td>
                                <td>
                                    <cc2:CustomTextBox ID="txtDisplayOrder" runat="server" Width="150" Type="Numeric"
                                        Style="text-align: right;"></cc2:CustomTextBox>
                                </td>
                                <td>Delivery Mode
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDeliveryMode" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Currency<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="155">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCurrency" runat="server" ErrorMessage="Please select currency"
                                        ControlToValidate="ddlCurrency" Display="None" ValidationGroup="vgCharge" InitialValue="0"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="rfvCurrency">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="padding-top: 30px; border: none;" align="center">
                                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>--%>
                                    <asp:GridView ID="dgChargeRates" runat="server" AutoGenerateColumns="false" ShowFooter="false"
                                        OnItemCommand="dgChargeRates_ItemCommand" OnItemDataBound="dgChargeRates_ItemDataBound"
                                        OnRowCommand="dgChargeRates_RowCommand" OnRowDataBound="dgChargeRates_RowDataBound"
                                        Font-Names="Calibri" Font-Size="11pt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Location" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnLocationId" runat="server" Value='<%# Eval("LocationId")%>' />
                                                    <%# (Eval("LocationId").ToString() == "0" ? string.Empty : ddlMLocation.Items.FindByValue(Eval("LocationId").ToString()).Text)%></ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddlFLocation" runat="server" Width="60" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlLocationID_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="Please select location"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="ddlFLocation"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvLocation">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    Location
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Terminal">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTerminalId" runat="server" Value='<%# Eval("TerminalId")%>' />
                                                    <%# (Eval("TerminalId").ToString() == "0" ? string.Empty : ddlMTerminal.Items.FindByValue(Eval("TerminalId").ToString()).Text)%>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddlFTerminal" runat="server" Width="100" Enabled="false">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    Terminal
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Washing Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnWashingTypeId" runat="server" Value='<%# Eval("WashingType")%>' />
                                                    <%# (Eval("WashingType").ToString() == "0" ? string.Empty : ddlMWashingType.Items.FindByValue(Eval("WashingType").ToString()).Text )%>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddlFWashingType" runat="server" Width="90" Enabled="false">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select washing type"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="ddlFWashingType"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vceW" runat="server" TargetControlID="rfvWashing">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    Washing Type
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Low" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLow" runat="server" Text='<%# Eval("Low")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtLow" runat="server" Width="40" Type="Numeric" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw1" runat="server" TargetControlID="txtLow" WatermarkText="0">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvLow" runat="server" ErrorMessage="Please enter low slab"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtLow"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvLow">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    Low
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHigh" runat="server" Text='<%# Eval("High")%>'></asp:Label></ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtHigh" runat="server" Width="40" Type="Numeric" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw2" runat="server" TargetControlID="txtHigh" WatermarkText="0">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvHigh" runat="server" ErrorMessage="Please enter high slab"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtHigh"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvHigh">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    High
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/BL" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerBl" runat="server" Text='<%# Eval("RatePerBL")%>'></asp:Label></ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRatePerBL" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw3" runat="server" TargetControlID="txtRatePerBL"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvRatePerBl" runat="server" ErrorMessage="Please enter rate / BL"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRatePerBL"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfvRatePerBl">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    Rate/BL
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/TEU" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerTEU" runat="server" Text='<%# ddlChargeType.SelectedValue == "6" ? Eval("RatePerCBM") : Eval("RatePerTEU") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRatePerTEU" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw4" runat="server" TargetControlID="txtRatePerTEU"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvRatePerTEU" runat="server" ErrorMessage="Please enter rate"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRatePerTEU"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="rfvRatePerTEU">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFRateTeu" runat="server" Text="Rate/TEU"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/FEU" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerFEU" runat="server" Text='<%#  ddlChargeType.SelectedValue == "6" ? Eval("RatePerTON") : Eval("RatePerFEU") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRateperFEU" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw5" runat="server" TargetControlID="txtRateperFEU"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvRatePerFEU" runat="server" ErrorMessage="Please enter rate"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRateperFEU"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="rfvRatePerFEU">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFRateFeu" runat="server" Text="Rate/FEU"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Share/BL" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSharingBL" runat="server" Text='<%# Eval("SharingBL")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtSharingBL" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw6" runat="server" TargetControlID="txtSharingBL"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvSharingBL" runat="server" ErrorMessage="Please enter shared rate"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtSharingBL"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="rfvSharingBL">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    Share/BL
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Share/TEU" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSharingTEU" runat="server" Text='<%# Eval("SharingTEU")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtSharingTEU" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw7" runat="server" TargetControlID="txtSharingTEU"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvSharingTEU" runat="server" ErrorMessage="Please enter shared rate"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtSharingTEU"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="rfvSharingTEU">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFShareTeu" runat="server" Text="Share/TEU"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shar/FEU" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSharingFEU" runat="server" Text='<%# Eval("SharingFEU")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtSharingFEU" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw8" runat="server" TargetControlID="txtSharingFEU"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvSharingFEU" runat="server" ErrorMessage="Please enter shared rate"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtSharingFEU"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="rfvSharingFEU">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFShareFeu" runat="server" Text="Shar/FEU"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
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
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
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
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="#F8F8F8" />
                                        <RowStyle Wrap="true" />
                                        <FooterStyle BackColor="#F8F8F8" HorizontalAlign="Center" />
                                    </asp:GridView>
                                    <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td colspan="4">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </center>
    </div>
</asp:Content>
