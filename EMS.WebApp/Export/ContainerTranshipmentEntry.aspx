<%@ Page Title="Container Transhipment Entry" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ContainerTranshipmentEntry.aspx.cs" Inherits="EMS.WebApp.Export.ContainerTranshipmentEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {
            if (sender._id == "AutoCompleteExExpBL") {
                var hdnExpBL = $get('<%=hdnExpBL.ClientID %>');
                hdnExpBL.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteVes") {
                var hdnExpBL = $get('<%=hdnExpBL.ClientID %>');
                hdnExpBL.value = e.get_value();
            }
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 85px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div>
        <div id="headercaption">
            ADD / EDIT CONTAINER TRANSHIPMENT</div>
        <center>
            <fieldset style="width: 60%;">
                <legend>Add / Edit Container Transhipment</legend>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td>
                                    B/L No<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdnExpBL" runat="server" />
                                    <asp:TextBox runat="server" ID="txtExpBL" Width="250" autocomplete="off" AutoPostBack="True"
                                        MaxLength="50" Style="text-transform: uppercase;" OnTextChanged="txtExpBL_TextChanged" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvExpBL" runat="server" ControlToValidate="txtExpBL"
                                        CssClass="errormessage" ValidationGroup="Select" ErrorMessage="Please select B/L No"
                                        Display="None"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceExpBL" runat="server" TargetControlID="rfvExpBL">
                                    </cc1:ValidatorCalloutExtender>
                                    <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteExExpBL" ID="autoComplete1"
                                        TargetControlID="txtExpBL" ServicePath="~/GetLocation.asmx" ServiceMethod="GetExportBLList"
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
                                                                var behavior = $find('AutoCompleteExExpBL');
                                                                if (!behavior._height) {
                                                                    var target = behavior.GetExportBLList();
                                                                    behavior._height = target.offsetHeight - 2;
                                                                    target.style.height = '0px';
                                                                }" />
                            
                                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                                            <Parallel Duration=".4">
                                                                <FadeIn />
                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExExpBL')._height" />
                                                            </Parallel>
                                                        </Sequence>
                                                    </OnShow>
                                                    <OnHide>
                                                        <%-- Collapse down to 0px and fade out --%>
                                                        <Parallel Duration=".4">
                                                            <FadeOut />
                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExExpBL')._height" EndValue="0" />
                                                        </Parallel>
                                                    </OnHide>
                                        </Animations>
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    B/L Date :
                                </td>
                                <td>
                                    <asp:Label ID="lblBLDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Port Name :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPortName" runat="server" Width="250" CssClass="dropdownlist">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPortName" runat="server" ErrorMessage="Please select port name"
                                        ControlToValidate="ddlPortName" Display="None" ValidationGroup="Select" InitialValue="0"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vcePortName" runat="server" TargetControlID="rfvPortName">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    Booking No :
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdnBookingId" runat="server" />
                                    <asp:Label ID="lblBookingNo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vessel Name<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdnVessel" runat="server" />
                                    <asp:TextBox runat="server" ID="txtVessel" MaxLength="100" Width="250" OnTextChanged="txtVessel_TextChanged"
                                        autocomplete="off" AutoPostBack="True" Style="text-transform: uppercase;" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvVessel" runat="server" ControlToValidate="txtVessel"
                                        CssClass="errormessage" ValidationGroup="Select" ErrorMessage="Please select Vessel"
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
                                </td>
                                <td>
                                    Voyage<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVoyage" runat="server" Width="250" CssClass="dropdownlist">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvVoyage" runat="server" ErrorMessage="Please enter Voyage"
                                        ControlToValidate="ddlVoyage" InitialValue="0" Display="None" ValidationGroup="Select"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceVoyage" runat="server" TargetControlID="rfvVoyage">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date of Arrival<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateofArrival" runat="server" Width="150"></asp:TextBox>
                                    <cc1:CalendarExtender ID="ceDateofArrival" runat="server" PopupButtonID="txtDateofArrival"
                                        Format="dd/MM/yyyy" TargetControlID="txtDateofArrival">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvDateofArrival" runat="server" ErrorMessage="Please enter date of arrival"
                                        ControlToValidate="txtDateofArrival" Display="None" ValidationGroup="Select"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceDateofArrival" runat="server" TargetControlID="rfvDateofArrival">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    Date of Departure<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateofDeparture" runat="server" Width="150"></asp:TextBox>
                                    <cc1:CalendarExtender ID="ceDateofDeparture" runat="server" PopupButtonID="txtDateofDeparture"
                                        Format="dd/MM/yyyy" TargetControlID="txtDateofDeparture">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvDateofDeparture" runat="server" ErrorMessage="Please enter date of departure"
                                        ControlToValidate="txtDateofDeparture" Display="None" ValidationGroup="Select"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceDateofDeparture" runat="server" TargetControlID="rfvDateofDeparture">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td>
                            <div style="margin-left: 53%;">
                                <asp:Button ID="btnShow" runat="server" Text="Show Containers" OnClick="btnShow_Click"
                                    ValidationGroup="Select" /></div>
                            <asp:Button ID="Button1" runat="server" Style="display: none;" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                                PopupControlID="pnlContainer" Drag="true" BackgroundCssClass="ModalPopupBG" CancelControlID="btnCancel">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlContainer" runat="server" Style="height: 250px; width: 400px; background-color: White;">
                                <center>
                                    <fieldset>
                                        <div style="overflow: auto; height: 180px; width: 380px;">
                                            <asp:GridView ID="gvContainer" runat="server" AutoGenerateColumns="false" Width="100%"
                                                OnRowDataBound="gvContainer_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Container No" HeaderStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnImpFooterId" runat="server" Value='<%# Eval("fk_ImpBLFooterID") %>' />
                                                            <asp:HiddenField ID="hdnContainerId" runat="server" Value='<%# Eval("fk_HireContainerID") %>' />
                                                            <asp:HiddenField ID="hdnTranshipmentId" runat="server" Value='<%# Eval("TranshipmentID") %>' />
                                                            <asp:Label ID="lblContainerNo" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContainerSize" runat="server" Text='<%# Eval("CntrSize")%>'></asp:Label>
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
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    </fieldset>
                                </center>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvSelectedContainer" runat="server" AutoGenerateColumns="false"
                                ShowFooter="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Container No" HeaderStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnImpFooterId" runat="server" Value='<%# Eval("ImportBLFooterId") %>' />
                                            <asp:HiddenField ID="hdnContainerId" runat="server" Value='<%# Eval("HireContainerId") %>' />
                                            <asp:HiddenField ID="hdnTranshipmentId" runat="server" Value='<%# Eval("TranshipmentId") %>' />
                                            <asp:Label ID="lblContainerNo" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContainerSize" runat="server" Text='<%# Eval("Size")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="#F8F8F8" />
                                <RowStyle Wrap="true" />
                                <FooterStyle BackColor="GrayText" HorizontalAlign="Center" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <%--<td>
                        </td>--%>
                        <td style="padding-left: 23%;">
                            <%--<asp:HiddenField ID="hdnChargeID" runat="server" Value="0" />--%>
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Select" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClick="btnBack_Click" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
