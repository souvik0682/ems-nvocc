<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="import-haulage-chrg-add-edit.aspx.cs" MasterPageFile="~/Site.Master" Inherits="EMS.WebApp.MasterModule.import_haulage_chrg_add_edit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
  
    <div>
        <div id="headercaption">
            ADD / EDIT IMPORT HAULAGE</div>
        <center>
            <fieldset style="width: 400px;">
                <legend>Add / Edit Import Haulage</legend>
                <table border="0" cellpadding="2" cellspacing="3">
                    <tr>
                        <td style="width: 140px;">
                            Location From<span class="errormessage1">*</span> :
                        </td>
                        <td>
                           <%-- <asp:DropDownList ID="ddlFromLocation" runat="server" Width="255">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFLocation" runat="server" ErrorMessage="Please select location"
                                Display="None" ControlToValidate="ddlFromLocation" ValidationGroup="vgHaulage"
                                InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvFLocation"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                            <br />--%>
                            <asp:DropDownList ID="ddlLocation" runat="server" Width="255" Visible="false">
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="txtFromLocation" Width="250" autocomplete="off" />
                            <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
                                TargetControlID="txtFromLocation" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                                ShowOnlyCurrentWordInCompletionListItem="true">
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
                            Location To<span class="errormessage1">*</span> :
                        </td>
                        <td>
                           <%-- <asp:DropDownList ID="ddlToLocation" runat="server" Width="255">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTLocation" runat="server" ErrorMessage="Please select location"
                                Display="None" ControlToValidate="ddlToLocation" ValidationGroup="vgHaulage"
                                InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvTLocation"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                            <br />--%>
                            <asp:TextBox runat="server" ID="txtToLocation" Width="250" autocomplete="off" />
                            <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx2" ID="autoComplete2"
                                TargetControlID="txtToLocation" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                                ShowOnlyCurrentWordInCompletionListItem="true">
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
                                                    var behavior = $find('AutoCompleteEx2');
                                                    if (!behavior._height) {
                                                        var target = behavior.get_completionList();
                                                        behavior._height = target.offsetHeight - 2;
                                                        target.style.height = '0px';
                                                    }" />
                            
                                                <%-- Expand from 0px to the appropriate size while fading in --%>
                                                <Parallel Duration=".4">
                                                    <FadeIn />
                                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx2')._height" />
                                                </Parallel>
                                            </Sequence>
                                        </OnShow>
                                        <OnHide>
                                            <%-- Collapse down to 0px and fade out --%>
                                            <Parallel Duration=".4">
                                                <FadeOut />
                                                <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx2')._height" EndValue="0" />
                                            </Parallel>
                                        </OnHide>
                                </Animations>
                            </cc1:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Container Size<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlContainerSize" runat="server" Width="255">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvContainerSize" runat="server" ErrorMessage="Please select container size"
                                Display="None" ControlToValidate="ddlContainerSize" ValidationGroup="vgHaulage"
                                InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvContainerSize"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Weight From<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtWFrom" runat="server" Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvWFrom" runat="server" ErrorMessage="Please enter minimum weight"
                                Display="None" ControlToValidate="txtWFrom" ValidationGroup="vgHaulage"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfvWFrom"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtWFrom"
                                FilterMode="ValidChars" FilterType="Numbers">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Weight To<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtWTo" runat="server" Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvWTo" runat="server" ErrorMessage="Please enter maximum weight"
                                Display="None" ControlToValidate="txtWTo" ValidationGroup="vgHaulage"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="rfvWTo"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtWTo"
                                FilterMode="ValidChars" FilterType="Numbers">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Rate<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtRate" runat="server" Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRate" runat="server" ErrorMessage="Please enter rate"
                                Display="None" ControlToValidate="txtRate" ValidationGroup="vgHaulage"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="rfvRate"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtRate"
                                FilterMode="ValidChars" FilterType="Numbers">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnHaulageChrgID" runat="server" Value="0" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="vgHaulage" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
</asp:Content>