<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Export-charge-add-edit.aspx.cs" Inherits="EMS.WebApp.MasterModule.Export_charge_add_edit" %>

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
            ADD / EDIT EXPORT CHARGE</div>
        <center>
            <fieldset style="width: 90%;">
                <legend>Add / Edit Export Charge</legend>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Charge Name<span class="errormessage1">*</span> :
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtChargeName" runat="server" Width="350" Style="text-transform: uppercase;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvChargeTitle" runat="server" ErrorMessage="Please enter charge title"
                                        ControlToValidate="txtChargeName" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="rfvChargeTitle">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 18%;">
                                </td>
                                <td style="width: 15%;">
                                    Line<span class="errormessage1"></span> :
                                </td>
                                <td style="width: 20%;">
                                    <asp:DropDownList ID="ddlLine" runat="server" Width="155">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 19%;">
                                    Terminal Specific<span class="errormessage1">*</span>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbTerminalRequired" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true" 
                                        onselectedindexchanged="rdbTerminalRequired_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvTerminalRequired" runat="server" ErrorMessage="Please select your choice"
                                        Display="None" ControlToValidate="rdbFPOD" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
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
                                    <asp:DropDownList ID="ddlHeaderLocation" runat="server" AutoPostBack="true" Width="155">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvHeaderLocation" runat="server" ControlToValidate="ddlHeaderLocation"
                                        Display="None" ErrorMessage="Please select location" InitialValue="0" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvHeaderLocation">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <%--Washing Charge<span class="errormessage1">*</span> :--%>
                                    Destination Charge :
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbDestinationCharge" AutoPostBack="true" runat="server"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" 
                                        onselectedindexchanged="rdbDestinationCharge_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvFreight" runat="server" ErrorMessage="Please select your choice"
                                        Display="None" ControlToValidate="rdbDestinationCharge" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" TargetControlID="rfvFreight">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:RadioButtonList ID="rdbWashing" AutoPostBack="true" runat="server" Style="display: none;"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
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
                                    Service:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtService" runat="server" Width="150"></asp:TextBox>
                                </td>
                                <td>
                                    FPOD<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbFPOD" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true" Enabled="false">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvPS" runat="server" ErrorMessage="Please select your choice"
                                        Display="None" ControlToValidate="rdbFPOD" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="rfvPS">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Charge Code
                                </td>
                                <td>
                                    <asp:TextBox ID="txtChargeCode" runat="server" Width="150"></asp:TextBox>
                                </td>
                                <td>
                                    Invoice Link :
                                </td>
                                <td>
                                
                                <asp:DropDownList ID="ddlInvLink" runat="server" Width="155">
                                    <asp:ListItem Selected="True" Text="Select Invoice"></asp:ListItem>
                                    <asp:ListItem Text="Freight Invoice"></asp:ListItem>
                                    <asp:ListItem Text="Other Invoice"></asp:ListItem>
                                </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Charge Basis<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlChargeType" runat="server" Width="155" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="Select Type"></asp:ListItem>
                                        <asp:ListItem Text="Per Doc"></asp:ListItem>
                                        <asp:ListItem Text="Per Unit"></asp:ListItem>
                                        <asp:ListItem Text="Type & Size"></asp:ListItem>
                                        <asp:ListItem Text="CBM / TON"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvChargeType" runat="server" ErrorMessage="Please select charge type"
                                        Display="None" ControlToValidate="ddlChargeType" ValidationGroup="vgCharge" InitialValue="0"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="rfvChargeType">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <%--Is Freight Component ?<span class="errormessage1">*</span> :--%>
                                    Effective Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEffectDate" runat="server" Width="150" AutoCompleteType="None"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select date"
                                        ControlToValidate="txtEffectDate" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
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
                                <td colspan="5" style="padding-top: 30px; border: none;" align="center">
                                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>--%>
                                    <asp:GridView ID="dgChargeRates" runat="server" AutoGenerateColumns="false" ShowFooter="false"
                                        Font-Names="Calibri" Font-Size="11pt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Location" Visible="false">
                                                <ItemTemplate>
                                                    <%--<asp:HiddenField ID="hdnLocationId" runat="server" Value='<%# Eval("LocationId")%>' />
                                                    <%# (Eval("LocationId").ToString() == "0" ? string.Empty : ddlMLocation.Items.FindByValue(Eval("LocationId").ToString()).Text)%>--%></ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddlFLocation" runat="server" Width="60" AutoPostBack="true">
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
                                                    <%-- <asp:HiddenField ID="hdnTerminalId" runat="server" Value='<%# Eval("TerminalId")%>' />
                                                    <%# (Eval("TerminalId").ToString() == "0" ? string.Empty : ddlMTerminal.Items.FindByValue(Eval("TerminalId").ToString()).Text)%>--%>
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
                                                    <%--<asp:HiddenField ID="hdnWashingTypeId" runat="server" Value='<%# Eval("WashingType")%>' />
                                                    <%# (Eval("WashingType").ToString() == "0" ? string.Empty : ddlMWashingType.Items.FindByValue(Eval("WashingType").ToString()).Text )%>--%>
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
                                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblRatePerBl" runat="server" Text='<%# Eval("RatePerBL")%>'></asp:Label>--%></ItemTemplate>
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
                                                    Type
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblRatePerTEU" runat="server" Text='<%# ddlChargeType.SelectedValue == "6" ? Eval("RatePerCBM") : Eval("RatePerTEU") %>'></asp:Label>--%>
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
                                                    <asp:Label ID="lblFRateTeu" runat="server" Text="Size"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/UNIT" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblRatePerFEU" runat="server" Text='<%#  ddlChargeType.SelectedValue == "6" ? Eval("RatePerTON") : Eval("RatePerFEU") %>'></asp:Label>--%>
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
                                                    <asp:Label ID="lblFRateFeu" runat="server" Text="Rate/Unit"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/Doc" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblSharingBL" runat="server" Text='<%# Eval("SharingBL")%>'></asp:Label>--%>
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
                                                    Rate/Doc
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/CBM" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblSharingTEU" runat="server" Text='<%# Eval("SharingTEU")%>'></asp:Label>--%>
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
                                                    <asp:Label ID="lblFShareTeu" runat="server" Text="Rate/CBM"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/TON" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblSharingFEU" runat="server" Text='<%# Eval("SharingFEU")%>'></asp:Label>--%>
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
                                                    <asp:Label ID="lblFShareFeu" runat="server" Text="Rate/TON"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"
                                                FooterStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <%--<asp:HiddenField ID="hdnSlno" runat="server" Value='<%# Eval("SlNo")%>' />
                                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("ChargesRateID")%>' />--%>
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
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgCharge" />&nbsp;&nbsp;<asp:Button
                                        ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                        OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" OnClick="btnBack_Click" />
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
