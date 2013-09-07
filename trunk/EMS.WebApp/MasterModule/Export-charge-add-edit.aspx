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

    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {

            if (sender._id == "AutoCompleteEx") {
                var hdnFPOD = $get('<%=hdnFPOD.ClientID %>');
                hdnFPOD.value = e.get_value();
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
                                <td style="width: 18%;">
                                </td>
                                <td style="width: 15%;">
                                    Charge Name<span class="errormessage1">*</span> :
                                </td>
                                <td style="width: 20%;">
                                    <asp:TextBox ID="txtChargeName" runat="server" Width="150" Style="text-transform: uppercase;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvChargeTitle" runat="server" ErrorMessage="Please enter charge title"
                                        ControlToValidate="txtChargeName" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="rfvChargeTitle">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td style="width: 19%;">
                                    Terminal Specific<span class="errormessage1">*</span>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbTerminalRequired" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rdbTerminalRequired_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0" ></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvTR" runat="server" ErrorMessage="Please select your choice"
                                        Display="None" ControlToValidate="rdbTerminalRequired" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Line<span class="errormessage1"></span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLine" runat="server" Width="155" 
                                        OnSelectedIndexChanged="ddlLine_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <%--Washing Charge<span class="errormessage1">*</span> :--%>
                                    Destination Charge :
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbDestinationCharge" AutoPostBack="true" runat="server"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdbDestinationCharge_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvFreight" runat="server" ErrorMessage="Please select your choice"
                                        Display="None" ControlToValidate="rdbDestinationCharge" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" TargetControlID="rfvFreight">
                                    </cc1:ValidatorCalloutExtender>
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
                                    Location<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHeaderLocation" runat="server" AutoPostBack="true" Width="155"
                                        OnSelectedIndexChanged="ddlHeaderLocation_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvHeaderLocation" runat="server" ControlToValidate="ddlHeaderLocation"
                                        Display="None" ErrorMessage="Please select location" InitialValue="0" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvHeaderLocation">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    FPOD :
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="txtFPOD" runat="server" Width="150" Enabled="false"></asp:TextBox>--%>
                                    <asp:HiddenField ID="hdnFPOD" runat="server" Value="0" />
                                    <asp:TextBox runat="server" ID="txtFPOD" Width="150" autocomplete="off" 
                                        Enabled="false" Style="text-transform: uppercase;" 
                                        ontextchanged="txtFPOD_TextChanged" AutoPostBack="True" />
                                    <asp:RequiredFieldValidator ID="rfvFPOD" runat="server" ErrorMessage="Please select FPOD"
                                        Display="None" ControlToValidate="txtFPOD" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvFPOD"
                                        WarningIconImageUrl="">
                                    </cc1:ValidatorCalloutExtender>
                                    <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
                                        TargetControlID="txtFPOD" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                                        ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected">
                                        <Animations>
                                        <OnShow>
                                            <Sequence>
                                                <%-- Make the completion list transparent and then show it --%>
                                                <OpacityAction Opacity="0" />
                                                <HideAction Visible="true" />
                            
                                                <%--Cache the original size of the completion list the first time
                                                    the animation is played and then set it to zero --%>
                                                <ScriptAction Script="
                                                    // Cache the size and setup the initial size
                                                    var behavior = $find('AutoCompleteEx');
                                                    if (!behavior._height) {
                                                        var target = behavior.get_completionList();
                                                        behavior._height = target.offsetHeight - 2;
                                                        target.style.height = '0px';
                                                    }" />
                            
                                                <%-- Expand from 0px to the appropriate size while fading in --%>
                                                <Parallel Duration=".4">
                                                    <FadeIn />
                                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                                                </Parallel>
                                            </Sequence>
                                        </OnShow>
                                        <OnHide>
                                            <%-- Collapse down to 0px and fade out --%>
                                            <Parallel Duration=".4">
                                                <FadeOut />
                                                <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                            </Parallel>
                                        </OnHide>
                                        </Animations>
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Service :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlService" runat="server" Width="155">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Invoice Link :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlInvLink" runat="server" Width="155">
                                        <asp:ListItem Selected="True" Text="Select Invoice"></asp:ListItem>
                                        <asp:ListItem Text="Freight Invoice" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Other Invoice" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
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
                                    Effective Date <span class="errormessage1">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEffectDate" runat="server" Width="150" AutoCompleteType="None"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select date"
                                        ControlToValidate="txtEffectDate" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtEffectDate"
                                        PopupPosition="BottomLeft" TargetControlID="txtEffectDate" Format="dd/MM/yyyy"
                                        OnClientDateSelectionChanged="checkDate">
                                    </cc1:CalendarExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtEffectDate"
                                        FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars="/">
                                    </cc1:FilteredTextBoxExtender>
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
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
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
                                <td colspan="5" style="padding-top: 30px; border: none;" align="center">
                                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>--%>
                                    <asp:GridView ID="dgChargeRates" runat="server" AutoGenerateColumns="false" ShowFooter="false"
                                        Font-Names="Calibri" Font-Size="11pt" OnRowDataBound="dgChargeRates_RowDataBound"
                                        OnRowCommand="dgChargeRates_RowCommand" 
                                        ondatabound="dgChargeRates_DataBound">
                                        <Columns>
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
                                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTypeId" runat="server" Value='<%# Eval("Type")%>' />
                                                    <%# (Eval("Type").ToString() == "0" ? string.Empty : ddlMType.Items.FindByValue(Eval("Type").ToString()).Text)%>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddlType" runat="server" Width="100">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ErrorMessage="Please select type"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="ddlType" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="rfvType">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    Type
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddlSize" runat="server" Width="100">
                                                        <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSize" runat="server" ErrorMessage="Please select size"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="ddlSize" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="rfvSize">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblSize" runat="server" Text="Size"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/UNIT" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerUnit" runat="server" Text='<%#  Eval("RatePerUnit") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRateperUnit" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw5" runat="server" TargetControlID="txtRateperUnit"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvRatePerUnit" runat="server" ErrorMessage="Please enter rate"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRateperUnit"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="rfvRatePerUnit">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFRateUnit" runat="server" Text="Rate/Unit"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/Doc" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerDoc" runat="server" Text='<%# Eval("RatePerDoc")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRatePerDoc" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw6" runat="server" TargetControlID="txtRatePerDoc"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvRatePerDoc" runat="server" ErrorMessage="Please enter rate"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRatePerDoc"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="rfvRatePerDoc">
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
                                                    <asp:Label ID="lblRatePerCBM" runat="server" Text='<%# Eval("RatePerCBM")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRatePerCBM" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw7" runat="server" TargetControlID="txtRatePerCBM"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvRatePerCBM" runat="server" ErrorMessage="Please enter rate"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRatePerCBM"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="rfvRatePerCBM">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFRatePerCBM" runat="server" Text="Rate/CBM"></asp:Label>
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate/TON" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRatePerTON" runat="server" Text='<%# Eval("RatePerTON")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <cc2:CustomTextBox ID="txtRatePerTON" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="tw8" runat="server" TargetControlID="txtRatePerTON"
                                                        WatermarkText="0.00">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="rfvRatePerTON" runat="server" ErrorMessage="Please enter rate"
                                                        Display="None" ValidationGroup="vgGridFooter" ControlToValidate="txtRatePerTON"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="rfvRatePerTON">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFRatePerTON" runat="server" Text="Rate/TON"></asp:Label>
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
                                    <asp:DropDownList ID="ddlMType" runat="server">
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
