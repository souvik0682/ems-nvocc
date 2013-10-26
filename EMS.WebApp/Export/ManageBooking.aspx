<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageBooking.aspx.cs" Inherits="EMS.WebApp.Export.ManageBooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {
            if (sender._id == "AutoCompleteExPOR") {
                var hdnPOR = $get('<%=hdnPOR.ClientID %>');
                hdnPOR.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteExPOL") {
                var hdnPOL = $get('<%=hdnPOL.ClientID %>');
                hdnPOL.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteExPOD") {
                var hdnPOD = $get('<%=hdnPOD.ClientID %>');
                hdnPOD.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteExFPOD") {
                var hdnFPOD = $get('<%=hdnFPOD.ClientID %>');
                hdnFPOD.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteExPOT") {
                var hdnPOT = $get('<%=hdnPOT.ClientID %>');
                hdnPOT.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteVes") {
                var hdnVessel = $get('<%=hdnVessel.ClientID %>');
                hdnVessel.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteMVes") {
                var hdnMainLineVessel = $get('<%=hdnMainLineVessel.ClientID %>');
                hdnMainLineVessel.value = e.get_value();
            }
        }

        function onDataShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 1000001;
            //sender._popupBehavior._element.style.left = "54px"; //set positions according to your requriment.
            //sender._popupBehavior._element.style.top = "50px"; //set top postion accorind to you requirement.

            //you can either use left,top or right,bottom or any combination u want to set ur divlist.           
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 85px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT BOOKING</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Booking</legend>
                <asp:UpdatePanel ID="upBooking" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                <tr>
                                    <td style="width: 20%;">
                                        Line/NVOCC<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:DropDownList ID="ddlNvocc" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                            TabIndex="1" OnSelectedIndexChanged="ddlNvocc_SelectedIndexChanged">
                                            <%--                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvNvocc" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="ddlNvocc" InitialValue="0"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 20%;">
                                        Location<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropdownlist" Width="250px" AutoPostBack="true"
                                            TabIndex="2" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvloc" runat="server" CssClass="errormessage" ErrorMessage="This field is required"
                                            ControlToValidate="ddlLocation" InitialValue="0" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Booking Party<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBookingParty" runat="server" CssClass="dropdownlist" Width="250px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlBookingParty_SelectedIndexChanged" TabIndex="3">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvParty" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="ddlBookingParty" InitialValue="0"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        Accounts<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAccounts" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                            Width="250px" TabIndex="4"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="rfvAccounts" runat="server" ControlToValidate="txtAccounts"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Booking Number<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                            Width="250px" TabIndex="5" Enabled="false"></asp:TextBox><br />
                                    </td>
                                    <td>
                                        Booking Date<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBookingDate" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cbeBookingDate" TargetControlID="txtBookingDate" runat="server"
                                            Format="dd-MM-yyyy" Enabled="True" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvBookingDate" runat="server" ControlToValidate="txtBookingDate"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ref Booking No
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRefBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                            Width="250px" TabIndex="7"></asp:TextBox><br />
                                    </td>
                                    <td>
                                        Ref Booking Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRefBookingDate" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="8"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cbeRefBookingNo" TargetControlID="txtRefBookingDate" runat="server"
                                            Format="dd-MM-yyyy" Enabled="True" />
                                        <%--                                        <br />
                                        <asp:RequiredFieldValidator ID="rfbRefBookingDate" runat="server" ControlToValidate="txtRefBookingDate"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Place of Receipt<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnPOR" runat="server" />
                                        <asp:TextBox runat="server" ID="txtPOR" Width="250" autocomplete="off" AutoPostBack="True"
                                            MaxLength="50" TabIndex="9" Style="text-transform: uppercase;" OnTextChanged="txtPOR_TextChanged" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvPOR" runat="server" ControlToValidate="txtPOR"
                                            CssClass="errormessage" ValidationGroup="Save" ErrorMessage="Please select Place of Receipt"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <%-- <asp:RequiredFieldValidator ID="rfvPOR" runat="server" ErrorMessage="Please select Place of Receipt"
                                            Display="None" ControlToValidate="txtPOR" ValidationGroup="vgBooking" ></asp:RequiredFieldValidator>--%>
                                        <%--                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvPOR"
                                            WarningIconImageUrl="">
                                        </cc1:ValidatorCalloutExtender>--%>
                                        <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteExPOR" ID="autoComplete1"
                                            TargetControlID="txtPOR" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
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
                                                                var behavior = $find('AutoCompleteExPOR');
                                                                if (!behavior._height) {
                                                                    var target = behavior.get_completionList();
                                                                    behavior._height = target.offsetHeight - 2;
                                                                    target.style.height = '0px';
                                                                }" />
                            
                                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                                            <Parallel Duration=".4">
                                                                <FadeIn />
                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExPOR')._height" />
                                                            </Parallel>
                                                        </Sequence>
                                                    </OnShow>
                                                    <OnHide>
                                                        <%-- Collapse down to 0px and fade out --%>
                                                        <Parallel Duration=".4">
                                                            <FadeOut />
                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExPOR')._height" EndValue="0" />
                                                        </Parallel>
                                                    </OnHide>
                                            </Animations>
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        Port of Loading<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnPOL" runat="server" />
                                        <asp:TextBox runat="server" ID="txtPOL" Width="250" autocomplete="off" MaxLength="50"
                                            TabIndex="10" Style="text-transform: uppercase;" AutoPostBack="True" 
                                            ontextchanged="txtPOL_TextChanged" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvPOL" runat="server" ControlToValidate="txtPOL"
                                            CssClass="errormessage" ValidationGroup="Save" ErrorMessage="Please select Load Port"
                                            Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <%--                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvPOL"
                                            WarningIconImageUrl="">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteExPOL" ID="AutoCompleteExtender1"
                                            TargetControlID="txtPOL" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
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
                                                                var behavior = $find('AutoCompleteExPOL');
                                                                if (!behavior._height) {
                                                                    var target = behavior.get_completionList();
                                                                    behavior._height = target.offsetHeight - 2;
                                                                    target.style.height = '0px';
                                                                }" />
                            
                                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                                            <Parallel Duration=".4">
                                                                <FadeIn />
                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExPOL')._height" />
                                                            </Parallel>
                                                        </Sequence>
                                                    </OnShow>
                                                    <OnHide>
                                                        <%-- Collapse down to 0px and fade out --%>
                                                        <Parallel Duration=".4">
                                                            <FadeOut />
                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExPOL')._height" EndValue="0" />
                                                        </Parallel>
                                                    </OnHide>
                                            </Animations>
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port of Discharge<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnPOD" runat="server" />
                                        <asp:TextBox runat="server" ID="txtPOD" Width="250" autocomplete="off" AutoPostBack="True"
                                            MaxLength="50" TabIndex="11" Style="text-transform: uppercase;" OnTextChanged="txtPOD_TextChanged" />
                                        <%--                                        <asp:TextBox ID="txtPOR" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>--%>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvPOD" runat="server" ControlToValidate="txtPOD"
                                            CssClass="errormessage" ValidationGroup="Save" ErrorMessage="Please select Discharge Port"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <%--                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvPOD"
                                            WarningIconImageUrl="">--%>
                                        <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteExPOD" ID="AutoCompleteExtender2"
                                            TargetControlID="txtPOD" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
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
                                                                var behavior = $find('AutoCompleteExPOD');
                                                                if (!behavior._height) {
                                                                    var target = behavior.get_completionList();
                                                                    behavior._height = target.offsetHeight - 2;
                                                                    target.style.height = '0px';
                                                                }" />
                            
                                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                                            <Parallel Duration=".4">
                                                                <FadeIn />
                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExPOD')._height" />
                                                            </Parallel>
                                                        </Sequence>
                                                    </OnShow>
                                                    <OnHide>
                                                        <%-- Collapse down to 0px and fade out --%>
                                                        <Parallel Duration=".4">
                                                            <FadeOut />
                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExPOD')._height" EndValue="0" />
                                                        </Parallel>
                                                    </OnHide>
                                            </Animations>
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        Final Destination<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnFPOD" runat="server" />
                                        <asp:TextBox runat="server" ID="txtFPOD" Width="250" autocomplete="off" AutoPostBack="True"
                                            MaxLength="50" TabIndex="12" Style="text-transform: uppercase;" OnTextChanged="txtFPOD_TextChanged" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvFPOD" runat="server" ControlToValidate="txtFPOD"
                                            CssClass="errormessage" ValidationGroup="Save" ErrorMessage="Please select final Destination"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteExFPOD" ID="AutoCompleteExtender3"
                                            TargetControlID="txtFPOD" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
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
                                                                var behavior = $find('AutoCompleteExFPOD');
                                                                if (!behavior._height) {
                                                                    var target = behavior.get_completionList();
                                                                    behavior._height = target.offsetHeight - 2;
                                                                    target.style.height = '0px';
                                                                }" />
                            
                                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                                            <Parallel Duration=".4">
                                                                <FadeIn />
                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExFPOD')._height" />
                                                            </Parallel>
                                                        </Sequence>
                                                    </OnShow>
                                                    <OnHide>
                                                        <%-- Collapse down to 0px and fade out --%>
                                                        <Parallel Duration=".4">
                                                            <FadeOut />
                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExFPOD')._height" EndValue="0" />
                                                        </Parallel>
                                                    </OnHide>
                                            </Animations>
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commodity<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommodity" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="13"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvCommodity" runat="server" ControlToValidate="txtCommodity"
                                            CssClass="errormessage" ValidationGroup="Save" ErrorMessage="Please select Commodity"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        Shipment Type<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlShipmentType" runat="server" Width="250" CssClass="dropdownlist"
                                            TabIndex="14">
                                            <%--<asp:ListItem Value="0" Text="--Select--"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Loading Vessel<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnVessel" runat="server" />
                                        <asp:TextBox runat="server" ID="txtVessel" MaxLength="100" Width="250" OnTextChanged="txtVessel_TextChanged"
                                            autocomplete="off" AutoPostBack="True" TabIndex="15" Style="text-transform: uppercase;" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvVessel" runat="server" ControlToValidate="txtVessel"
                                            CssClass="errormessage" ValidationGroup="Save" ErrorMessage="Please select Vessel"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteVes" ID="aceVessel"
                                            TargetControlID="txtVessel" ServicePath="~/GetLocation.asmx" ServiceMethod="GetVesselList"
                                            MinimumPrefixLength="1" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                            OnClientItemSelected="AutoCompleteItemSelected" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            DelimiterCharacters=";, :" ShowOnlyCurrentWordInCompletionListItem="true">
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
                                                                var behavior = $find('AutoCompleteVes');
                                                                if (!behavior._height) {
                                                                    var target = behavior.get_completionList();
                                                                    behavior._height = target.offsetHeight - 2;
                                                                    target.style.height = '0px';
                                                                }" />
                            
                                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                                            <Parallel Duration=".4">
                                                                <FadeIn />
                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteVes')._height" />
                                                            </Parallel>
                                                        </Sequence>
                                                    </OnShow>
                                                    <OnHide>
                                                        <%-- Collapse down to 0px and fade out --%>
                                                        <Parallel Duration=".4">
                                                            <FadeOut />
                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteVes')._height" EndValue="0" />
                                                        </Parallel>
                                                    </OnHide>
                                            </Animations>
                                        </cc1:AutoCompleteExtender>
                                        <%-- <td>
                                        <asp:TextBox ID="txtLoadingVessel" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>--%>
                                    </td>
                                    <td>
                                        Loading Voyage<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLoadingVoyage" runat="server" Width="250" CssClass="dropdownlist"
                                            TabIndex="16" AutoPostBack="True" 
                                            onselectedindexchanged="ddlLoadingVoyage_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mainline Vessel<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <%--                                        <asp:TextBox ID="txtMainLineVessel" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>--%>
                                        <asp:HiddenField ID="hdnMainLineVessel" runat="server" />
                                        <asp:TextBox runat="server" ID="txtMainLineVessel" MaxLength="100" Width="250" OnTextChanged="txtMainLineVessel_TextChanged"
                                            Style="text-transform: uppercase;" autocomplete="off" TabIndex="17" AutoPostBack="True" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvMainLineVessel" runat="server" ControlToValidate="txtMainLineVessel"
                                            CssClass="errormessage" ValidationGroup="Save" ErrorMessage="Please select Vessel"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteMVes" ID="AutoCompleteExtender4"
                                            TargetControlID="txtMainLineVessel" ServicePath="~/GetLocation.asmx" ServiceMethod="GetVesselList"
                                            MinimumPrefixLength="1" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                            OnClientItemSelected="AutoCompleteItemSelected" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            DelimiterCharacters=";, :" ShowOnlyCurrentWordInCompletionListItem="true">
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
                                                                var behavior = $find('AutoCompleteMVes');
                                                                if (!behavior._height) {
                                                                    var target = behavior.get_completionList();
                                                                    behavior._height = target.offsetHeight - 2;
                                                                    target.style.height = '0px';
                                                                }" />
                            
                                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                                            <Parallel Duration=".4">
                                                                <FadeIn />
                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteMVes')._height" />
                                                            </Parallel>
                                                        </Sequence>
                                                    </OnShow>
                                                    <OnHide>
                                                        <%-- Collapse down to 0px and fade out --%>
                                                        <Parallel Duration=".4">
                                                            <FadeOut />
                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteMVes')._height" EndValue="0" />
                                                        </Parallel>
                                                    </OnHide>
                                            </Animations>
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        Mainline Voyage<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMainLineVoyage" runat="server" Width="250" CssClass="dropdownlist"
                                            TabIndex="18">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        B/L Through Edge
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoBLThruEdge" runat="server" TabIndex="19" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rdoBLThruEdge_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Selected="True" Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <%--                                        <asp:DropDownList ID="ddlBLEdge" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </td>
                                    <td>
                                        Hazardous Cargo
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoHazardousCargo" runat="server" TabIndex="20" RepeatDirection="Horizontal"
                                            AutoPostBack="True" OnSelectedIndexChanged="rdoHazardousCargo_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Text="No" Value="No"></asp:ListItem>
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <%--                                
                                    <td>        
                                        <asp:DropDownList ID="ddlHAZCargo" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                        IMO
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtImo" runat="server" TabIndex="21" Width="250"></asp:TextBox>
                                    </td>
                                    <td>
                                        UNO
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUno" runat="server" TabIndex="22" Width="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Weight
                                    </td>
                                    <td>
                                        <cc2:CustomTextBox ID="txtGrossWeight" runat="server" TabIndex="23" CssClass="numerictextbox" Enabled="false"
                                            Width="250px" Type="Decimal" MaxLength="13" Precision="10" Scale="2">
                                        </cc2:CustomTextBox>
                                    </td>
                                    <td>
                                        CBM
                                    </td>
                                    <td>
                                        <cc2:CustomTextBox ID="txtCbm" runat="server" CssClass="numerictextbox" TabIndex="24"
                                            Width="250px" Type="Decimal" MaxLength="13" Precision="10" Scale="2">
                                        </cc2:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Service<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlService" runat="server" CssClass="dropdownlist" Width="250"
                                            TabIndex="25" AutoPostBack="True">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Reefer
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoReefer" runat="server" TabIndex="26" RepeatDirection="Horizontal"
                                            AutoPostBack="True" OnSelectedIndexChanged="rdoReffer_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Text="No" Value="No"></asp:ListItem>
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkContainerDtls" runat="server" Text="Container Details" OnClick="lnkContainerDtls_Click"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContainerDtls" runat="server" CssClass="textboxuppercase" Width="250px"
                                            Enabled="False" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        Temp Min<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <cc2:CustomTextBox ID="txtTempMin" runat="server" CssClass="numerictextbox" TabIndex="27"
                                            Width="250px" Type="Decimal" MaxLength="7" Precision="4" Scale="2"></cc2:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkTransitRoute" runat="server" Text="Transit Route" OnClick="lnkTransitRoute_Click"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTransitRoute" runat="server" CssClass="textboxuppercase" Width="250px"
                                            Enabled="False" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        Temp Max<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <cc2:CustomTextBox ID="txtTempMax" runat="server" CssClass="numerictextbox" TabIndex="28"
                                            Width="250px" Type="Decimal" MaxLength="7" Precision="4" Scale="2"></cc2:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Related Sales Person
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSalesman" runat="server"></asp:Label>
                                     </td>
                                    <td>
                                        Charge Approver
                                    </td>
                                    <td>
                                       <asp:Label ID="lblApprover" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnBookingID" runat="server" Value="0" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="70"
                                            OnClick="btnSave_Click1" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="71" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" OnClick="btnBack_Click" />
                                        <br />
                                        <asp:Label ID="lblError" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- Modal Popup Container Details -->
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Style="display: none;" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                                Enabled="true" PopupControlID="pnlContainer" Drag="true" BackgroundCssClass="ModalPopupBG"
                                CancelControlID="btnCancelContainer">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlContainer" runat="server" Style="height: 360px; width: 450px; background-color: White;">
                                <fieldset>
                                    <legend>Container Breakup</legend>
                                    <center>
                                        <asp:UpdatePanel ID="udpContainer" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="hdnBookingContainerID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnIndex" runat="server" />
                                                <div style="overflow: auto; height: 90px; width: 420px;">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                <asp:Label ID="lblType" Text="Type" runat="server"></asp:Label><span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <asp:Label ID="lblSize" Text="Size" runat="server"></asp:Label><span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <asp:Label ID="lblUnit" Text="Unit" runat="server"></asp:Label><span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <asp:Label ID="lblWt" Text="Wt/Cont" runat="server"></asp:Label><span class="errormessage">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                <asp:DropDownList ID="ddlCntrType" runat="server" CssClass="dropdownlist" Width="80px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <asp:DropDownList ID="ddlSize" runat="server" CssClass="dropdownlist" Width="80px">
                                                                    <asp:ListItem Text="20" Value="20" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <cc2:CustomTextBox ID="txtNos" runat="server" CssClass="numerictextbox" Width="77px"
                                                                    Type="Numeric" MaxLength="8" Precision="10" Scale="2">
                                                                </cc2:CustomTextBox>
                                                                <%--<asp:TextBox ID="txtNos" runat="server" Width="77px"></asp:TextBox>--%>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <cc2:CustomTextBox ID="txtWtPerCntr" runat="server" CssClass="numerictextbox" Width="82px"
                                                                    Type="Decimal" MaxLength="13" Precision="10" Scale="2">
                                                                </cc2:CustomTextBox>
                                                                <%--<asp:TextBox ID="txtWtPerCntr" runat="server" Width="82px"></asp:TextBox>--%>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <asp:ImageButton ID="btnimgSave" runat="server" ImageUrl="~/Images/action_add2.gif"
                                                                    Height="16" Width="16" OnClick="btnimgSave_Click" CausesValidation="true" ValidationGroup="Container" />&nbsp;&nbsp;
                                                                <asp:ImageButton ID="btnimgReset" runat="server" ImageUrl="~/Images/Undo.gif" Height="16"
                                                                    Width="16" OnClick="btnimgReset_Click" CausesValidation="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <asp:RequiredFieldValidator ID="rfvType" runat="server" CssClass="errormessage" ErrorMessage="Please Select Type"
                                                                    ControlToValidate="ddlCntrType" InitialValue="0" ValidationGroup="Container"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvNos" runat="server" CssClass="errormessage" ErrorMessage="Please Enter Unit"
                                                                    ControlToValidate="txtNos" ValidationGroup="Container" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvwt" runat="server" CssClass="errormessage" ErrorMessage="Please Enter Wt/Cont"
                                                                    ControlToValidate="txtWtPerCntr" ValidationGroup="Container" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="overflow: auto; height: 180px; width: 420px;">
                                                    <asp:GridView ID="gvContainer" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                        BorderStyle="None" BorderWidth="0" Width="100%" Style="margin-right: 0px" ShowHeader="False"
                                                        OnRowCommand="gvContainer_RowCommand" OnDataBound="gvContainer_DataBound" OnRowDataBound="gvContainer_OnRowDataBound">
                                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                        <PagerStyle CssClass="gridviewpager" />
                                                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="gvhdnBookingContainerID" runat="server" Value='<%# Eval("BookingContainerID") %>' />
                                                                    <asp:HiddenField ID="gvhdnContainerTypeId" runat="server" Value='<%# Eval("ContainerTypeID") %>' />
                                                                    <asp:Label ID="lblContainerType" runat="server" Text='<%# Eval("ContainerType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContainerSize" runat="server" Text='<%# Eval("CntrSize")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("NoofContainers")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwtPerCont" runat="server" Text='<%# Eval("wtPerCntr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditGrid" ImageUrl="~/Images/EditInGrid.gif"
                                                                        Height="16" Width="16" CausesValidation="false" />&nbsp;&nbsp;
                                                                    <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/trash_icon.gif"
                                                                        Height="16" Width="16" CausesValidation="false" OnClientClick="javascript:if(!confirm('Want to Delete this Container?')) return false;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                        <%--<asp:Button ID="btnSaveContainer" runat="server" Text="Save" CausesValidation="false" />--%>
                                        <asp:Button ID="btnCancelContainer" runat="server" Text="Close" CausesValidation="false" />
                                    </center>
                                </fieldset>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <!-- Modal Popup Container Details -->
                <!-- Modal Popup Transit Route -->
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="Button2" runat="server" Style="display: none;" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2"
                                PopupControlID="pnlTransit" Drag="true" BackgroundCssClass="ModalPopupBG" 
                                CancelControlID="btnCalcelTransit">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlTransit" runat="server" Style="height: 340px; width: 400px; background-color: White;
                                display: none">
                                <fieldset>
                                    <legend>Transit Route</legend>
                                    <center>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 10%;">
                                                                Order
                                                            </td>
                                                            <td style="width: 70%;">
                                                                Port Name<span class="errormessage">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <cc2:CustomTextBox ID="ctbSlNo" runat="server" CssClass="numerictextbox" Width="30px"
                                                                    Type="Numeric" MaxLength="8" Precision="10" Scale="2">
                                                                </cc2:CustomTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:HiddenField ID="hdnPOT" runat="server" />
                                                                <asp:TextBox runat="server" ID="txtPOT" Width="250" autocomplete="off" MaxLength="50" ValidationGroup="vgTransit"
                                                                    Style="text-transform: uppercase;" AutoPostBack="true" OnTextChanged="txtPOT_TextChanged" />
                                                                <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteExPOT" ID="AutoCompleteExPOT"
                                                                    OnClientShown="onDataShown" TargetControlID="txtPOT" ServicePath="~/GetLocation.asmx"
                                                                    ServiceMethod="GetCompletionList" MinimumPrefixLength="2" CompletionInterval="100"
                                                                    EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                    DelimiterCharacters=";,:" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected">
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
                                                                                var behavior = $find('AutoCompleteExPOT');
                                                                                if (!behavior._height) {
                                                                                    var target = behavior.get_completionList();
                                                                                    behavior._height = target.offsetHeight - 2;
                                                                                    target.style.height = '0px';
                                                                                }" />
                            
                                                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                                                            <Parallel Duration=".4">
                                                                                <FadeIn />
                                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExPOT')._height" />
                                                                            </Parallel>
                                                                        </Sequence>
                                                                    </OnShow>
                                                                    <OnHide>
                                                                        <%-- Collapse down to 0px and fade out --%>
                                                                        <Parallel Duration=".4">
                                                                            <FadeOut />
                                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExPOT')._height" EndValue="0" />
                                                                        </Parallel>
                                                                    </OnHide>
                                                                    </Animations>
                                                                </cc1:AutoCompleteExtender>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgbtnAddToGrid" runat="server" ImageUrl="~/Images/action_add2.gif"
                                                                    Height="16" Width="16" OnClick="imgbtnAddToGrid_Click" CausesValidation="true"
                                                                    ValidationGroup="vgTransit" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:RequiredFieldValidator ID="rfvPOT" runat="server" ErrorMessage="Please select Place of Receipt"
                                                                    ControlToValidate="txtPOT" ValidationGroup="vgTransit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="rfvPOT"
                                                                    WarningIconImageUrl="">
                                                                </cc1:ValidatorCalloutExtender>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="overflow: auto; height: 180px; width: 380px;">
                                                    <asp:GridView ID="gvTransit" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                        BorderStyle="None" BorderWidth="0" Width="100%" HeaderStyle-Wrap="true" OnDataBound="gvTransit_DataBound"
                                                        OnRowCommand="gvTransit_RowCommand" ShowHeader="false">
                                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                        <PagerStyle CssClass="gridviewpager" />
                                                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                        <EmptyDataTemplate>
                                                            No Page(s) Found
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <%--<HeaderStyle CssClass="gridviewheader_num" />
                                                                <HeaderTemplate>
                                                                    Order
                                                                </HeaderTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="gvhdnBookingTranshipmentID" runat="server" Value='<%# Eval("BookingTranshipmentID") %>' />
                                                                    <asp:HiddenField ID="gvhdnPortId" runat="server" Value='<%# Eval("PortId") %>' />
                                                                    <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("OrderNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <%--<HeaderStyle CssClass="gridviewheader" />
                                                                <HeaderTemplate>
                                                                    Port
                                                                </HeaderTemplate>--%>
                                                                <ItemStyle CssClass="gridviewitem" Width="80%" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPortName" runat="server" Text='<%# Eval("PortName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                                        Height="16" Width="16" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                        <%--<asp:Button ID="btnSaveTransit" runat="server" Text="Save" />--%>
                                        <asp:Button ID="btnCalcelTransit" runat="server" Text="Close" />
                                    </center>
                                </fieldset>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <!-- Modal Popup Transit Route -->
            </fieldset>
            <asp:UpdateProgress ID="uProgressBooking" runat="server" AssociatedUpdatePanelID="upBooking">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </center>
</asp:Content>
