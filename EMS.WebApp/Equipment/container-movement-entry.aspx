<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="container-movement-entry.aspx.cs" Inherits="EMS.WebApp.Equipment.container_movement_entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {

            if (sender._id == "AutoCompleteEx") {
                var hdnFromLocation = $get('<%=hdnFromLocation.ClientID %>');
                hdnFromLocation.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteEx2") {
                var hdnToLocation = $get('<%=hdnToLocation.ClientID %>');
                hdnToLocation.value = e.get_value();
            }
        }

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div>
        <div id="headercaption">
            ADD / EDIT CONTAINER MOVEMENT</div>
        <center>
            <fieldset style="width: 85%;">
                <legend>Add / Edit Container Transaction</legend>
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td style="width: 150px;">
                            Transaction CODE :
                        </td>
                        <td style="width: 375px;">
                            <asp:Label ID="lblTranCode" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnContainerId" runat="server" Value="0" />
                        </td>
                        <td>
                            Activity Date<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" Width="250"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDate"
                                TargetControlID="txtDate">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Please enter date"
                                ControlToValidate="txtDate" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="rfvDate">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            From Status<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFromStatus" runat="server" Width="255" OnSelectedIndexChanged="ddlFromStatus_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFromStatus" runat="server" ErrorMessage="Please enter from status"
                                ControlToValidate="ddlFromStatus" Display="None" ValidationGroup="vgContainer" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" TargetControlID="rfvFromStatus">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            To Status<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlToStatus" runat="server" Width="255" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlToStatus_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvToStatus" runat="server" ErrorMessage="Please enter transfer location"
                                ControlToValidate="ddlToStatus" Display="None" ValidationGroup="vgContainer" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" TargetControlID="rfvToStatus">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            From Location<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnFromLocation" runat="server" Value="0" />
                            <asp:TextBox ID="txtFromLocation" runat="server" Width="250" AutoPostBack="true"
                                OnTextChanged="txtFromLocation_TextChanged"></asp:TextBox>
                            <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
                                TargetControlID="txtFromLocation" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
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
                            <asp:RequiredFieldValidator ID="rfvFromLocation" runat="server" ErrorMessage="Please enter from location"
                                ControlToValidate="txtFromLocation" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="rfvFromLocation">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            To Location:
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnToLocation" runat="server" Value="0" />
                            <asp:TextBox ID="txtToLocation" runat="server" Width="250" Enabled="false"></asp:TextBox>
                            <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx2" ID="autoComplete2"
                                TargetControlID="txtToLocation" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
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
                            <asp:RequiredFieldValidator ID="rfvToLocation" runat="server" ErrorMessage="Please enter to location"
                                ControlToValidate="txtToLocation" Display="None" ValidationGroup="vgContainer" Enabled="false"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="rfvToLocation">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Of TEUs<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtTeus" runat="server" Width="250"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtTeus"
                                FilterMode="ValidChars" FilterType="Numbers">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rfvTeus" runat="server" ErrorMessage="Please enter total no of TEUs"
                                ControlToValidate="txtTeus" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="rfvTeus">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            No. Of FEUs<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFEUs" runat="server" Width="250"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFEUs"
                                FilterMode="ValidChars" FilterType="Numbers">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rfvFeus" runat="server" ErrorMessage="Please enter total no of FEUs"
                                ControlToValidate="txtFEUs" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="rfvFeus">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Narration :
                        </td>
                        <td>
                            <asp:TextBox ID="txtNarration" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td valign="top">
                            Empty Yard :
                        </td>
                        <td valign="top">
                            <asp:DropDownList ID="ddlEmptyYard" runat="server" Enabled="false" Width="255">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEmptyYard" runat="server" ErrorMessage="Please select yard location"
                                Display="None" ControlToValidate="ddlEmptyYard" InitialValue="0" ValidationGroup="vgContainer"
                                Enabled="false"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvEmptyYard">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                        <td>
                            <asp:Button ID="btnShow" runat="server" Text="Show Container" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow"
                                PopupControlID="pnlContainer" Drag="true" BackgroundCssClass="ModalPopupBG" CancelControlID="btnCancel">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvSelectedContainer" runat="server" AutoGenerateColumns="false"
                                ShowFooter="false" Width="80%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Container No">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnTransactionId" runat="server" Value='<%# Eval("TransactionId") %>' />
                                            <%# Eval("ContainerNo")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Status">
                                        <ItemTemplate>
                                            <%# Eval("FromStatus")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Landing Date">
                                        <ItemTemplate>
                                            <%# Eval("LandingDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Changed Status">
                                        <ItemTemplate>
                                            <%# Eval("ToStatus")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date of Change">
                                        <ItemTemplate>
                                            <%# Eval("ChangeDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select All" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Select
                                            <%--<asp:CheckBox ID="chkHeader" runat="server" />--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItem" runat="server" Checked="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="GrayText" />
                                <RowStyle Wrap="true" />
                                <FooterStyle BackColor="GrayText" HorizontalAlign="Center" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnChargeID" runat="server" Value="0" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgContainer" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown" />
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
    <asp:Panel ID="pnlContainer" runat="server" Style="height: 250px; width: 400px; background-color: White;">
        <center>
            <fieldset>
                <div style="overflow: auto; height: 180px; width: 380px;">
                    <asp:GridView ID="gvContainer" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Container No" HeaderStyle-Width="50%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnTransactionId" runat="server" Value='<%# Eval("TransactionId") %>' />
                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status") %>' />
                                    <asp:HiddenField ID="hdnLandingDate" runat="server" Value='<%# Eval("LandingDate") %>' />
                                    <asp:Label ID="lblContainerNo" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblContainerSize" runat="server" Text='<%# Eval("Size")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkContainer" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="GrayText" />
                        <RowStyle Wrap="true" />
                    </asp:GridView>
                </div>
                <br />
                <asp:Button ID="btnProceed" runat="server" Text="Proceed" OnClick="btnProceed_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
            </fieldset>
        </center>
    </asp:Panel>
</asp:Content>
