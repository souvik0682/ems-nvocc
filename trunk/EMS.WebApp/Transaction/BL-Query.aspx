<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BL-Query.aspx.cs" Inherits="EMS.WebApp.Transaction.BL_Query" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {

            if (sender._id == "AutoCompleteEx") {
                var hdnBLId = $get('<%=hdnBLId.ClientID %>');
                hdnBLId.value = e.get_value();
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            B/L QUERY</div>
        <center>
            <fieldset style="width: 98%; display: block;">
                <legend>B/L Detail</legend>
                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <td style="width: 9%;">
                                    Location :
                                </td>
                                <td width="6%">
                                    <asp:DropDownList ID="ddlLocation" runat="server" Width="70" AutoPostBack="True"
                                        OnSelectedIndexChanged="LocationLine_Changed">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 5%;">
                                    Line :
                                </td>
                                <td style="width: 5%;">
                                    <asp:DropDownList ID="ddlLine" runat="server" Width="100" AutoPostBack="True" OnSelectedIndexChanged="LocationLine_Changed"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <td style="width: 9%;">
                            B/L No :
                        </td>
                        <td style="width: 5%;">
                            <asp:HiddenField ID="hdnBLId" runat="server" Value="0" />
                            <asp:TextBox ID="txtBlNo" runat="server" Width="150" AutoPostBack="True" OnTextChanged="txtBlNo_TextChanged"
                                onkeyup="SetContextKey();" Enabled="false"></asp:TextBox>
                            <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
                                TargetControlID="txtBlNo" ServicePath="~/GetLocation.asmx" ServiceMethod="GetBLNoList"
                                MinimumPrefixLength="1" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                                ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected"
                                UseContextKey="true">
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
                        <td style="width: 10%;">
                            Delivered To (CHA) :
                        </td>
                        <td style="width: 10%;">
                            <asp:TextBox ID="txtCha" runat="server" Width="150"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Landing Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtLandingDate" runat="server" Width="71"></asp:TextBox>
                        </td>
                        <td>
                            Vessel :
                        </td>
                        <td>
                            <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                            <asp:TextBox ID="txtVessel" runat="server" Width="100"></asp:TextBox>
                        </td>
                        <td>
                            Detention Free Days :
                        </td>
                        <td>
                            <%--<asp:ImageButton ID='btnRemove' runat='server' CommandName='Remove' ImageUrl='~/Images/remove.png' Height='16' Width='16' />--%>
                            <%--<input type="image" onserverclick="DeleteUploadedDoc" id="imgDelDoc" runat="server" src="~/Images/remove.png" style="height:20px; width:20px;" />--%>
                            <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                            <asp:TextBox ID="txtDetentionFreeDays" runat="server" Width="100"></asp:TextBox>
                        </td>
                        <td style="width: 11%;">
                            PGR free days :
                        </td>
                        <td style="width: 10%;">
                            <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                            <asp:TextBox ID="txtPGRFreedays" runat="server" Width="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            DO Valid Upto :
                        </td>
                        <td>
                            <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                            <asp:TextBox ID="txtDoValidUpto" runat="server" Width="71"></asp:TextBox>
                        </td>
                        <td>
                            Voyage :
                        </td>
                        <td>
                            <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                            <asp:TextBox ID="txtVoyage" runat="server" Width="100"></asp:TextBox>
                        </td>
                        <td>
                            Detention till :
                        </td>
                        <td>
                            <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                            <asp:TextBox ID="tstDetentionTill" runat="server" Width="100"></asp:TextBox>
                        </td>
                        <td>
                            PGR till :
                        </td>
                        <td>
                            <asp:TextBox ID="txtPGRTill" runat="server" Width="100"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>
                    <tr style="height: 50px; vertical-align: bottom; display: none;">
                        <td>
                        </td>
                        <td colspan="2">
                        </td>
                        <td colspan="2" align="left">
                            <%-- <asp:HiddenField ID="hdnBlQueryID" runat="server" Value="0" />--%>
                            <asp:Button ID="btnSave1" runat="server" Text="Save" ValidationGroup="vgCharge" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack1" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                            <asp:Label ID="lblMessageBLQuery" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="width: 98%; display: block;">
                <legend>Service Requests</legend>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table width="100%" border="0" cellspacing="0">
                            <tr style="height: 30px;">
                                <td>
                                    <%--<asp:CheckBox ID="" runat="server" AutoPostBack="true" />--%>
                                    <asp:Button ID="btnTemp9" runat="server" Style="display: none;" />
                                    <asp:RadioButton ID="chkFreightToCollect" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkFreightToCollect_CheckedChanged" Enabled="false" />
                                    <asp:LinkButton ID="lnkFreightToCollect" Enabled="false" runat="server" Text="Freight To Collect"
                                        ForeColor="Blue" OnClick="lnkFreightToCollect_Click"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="mpeFreight" runat="server" PopupControlID="pnlFreight"
                                        TargetControlID="btnTemp9" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseFreight">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlFreight" runat="server" Style="display: none;">
                                        <table style="width: 300px; height: 80px; background-color: White; text-align: center;"
                                            border="0" cellspacing="0">
                                            <%-- <tr>
                                                <td colspan="2" align="right">
                                                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Images/close-icon.png" />
                                                </td>
                                            </tr>--%>
                                            <tr style="background-color: #328DC4; height: 28px;">
                                                <td style="font-weight: bold; color: White; font-size: 12pt;">
                                                    Freight to collect
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgCloseFreight" runat="server" ImageUrl="~/Images/close-icon.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" style="padding-top: 10px;" colspan="2">
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/p1.jpeg" ToolTip="Print Examination Do"
                                                        Height="45" Width="45" AlternateText="Print Freight to collect" OnClick="imgBtnExaminationDo_Click" />
                                                    <br />
                                                    Print Freight to collect
                                                </td>
                                                <%--<td width="50%" style="padding-top: 10px;">
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/p2.jpeg" ToolTip="Print Final Do"
                                                        Height="45" Width="45" AlternateText="Print Final Do" Enabled="false" />
                                                    <br />
                                                    Print Final Do
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td colspan="2" height="30px">
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td>
                                    Freight Amt in $
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFreightToCollect" runat="server" Width="75" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="right" style="border-right: Solid 1px Black;">
                                    <asp:LinkButton ID="lnkGenInvFreightToCollect" runat="server" Text="Generate Invoice"
                                        ForeColor="Blue" Enabled="false" OnClick="lnkGenInvFreightToCollect_Click"></asp:LinkButton>
                                </td>
                                <td>
                                    <%--<asp:CheckBox ID="" runat="server" AutoPostBack="true" />--%>
                                    <asp:Button ID="btnTemp7" runat="server" Style="display: none;" />
                                    <asp:RadioButton ID="chkSecurityInv" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkSecurityInv_CheckedChanged" Enabled="false" />
                                    <asp:LinkButton ID="lnkSecurityInv" Enabled="false" runat="server" Text="Security Invoice"
                                        ForeColor="Blue" OnClick="lnkSecurityInv_Click"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="mpeSecurity" runat="server" PopupControlID="pnlSecurity"
                                        TargetControlID="btnTemp7" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseSecurity">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlSecurity" runat="server" Style="display: none;">
                                        <div style="height: 300; width: 350px;">
                                            <div style="background-color: #328DC4; padding-top: 5px;">
                                                <div style="width: 85%; text-align: left; font-weight: bold; color: White; font-size: 12pt;
                                                    padding-left: 15px; float: left;">
                                                    Security Invoice</div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgCloseSecurity" runat="server" ImageUrl="~/Images/close-icon.png"
                                                        Style="display: block;" /></div>
                                                <div style="clear: both;">
                                                </div>
                                            </div>
                                            <div id="dvSecurity" runat="server" style="width: 100%; height: 200px; overflow: auto;
                                                background-color: White; padding-top: 15px; text-align: center;">
                                                Please enter B/L No.
                                                <%--<table style="width:80%;" cellspacing="0" align="center">
                                                <tr style="height:30px;background-color:#328DC4;color:White; font-weight:bold;"><td>Date</td><td>Print</td></tr>
                                                <tr><td>01/01/2012</td><td><a href="#"><img src="../Images/Print.png" /></a></td></tr>
                                                <tr style="background-color:#99CCFF;"><td>01/01/2012</td><td><a href="#"><img src="../Images/Print.png" /></a></td></tr>
                                                <tr><td>01/01/2012</td><td><a href="#"><img src="../Images/Print.png" /></a></td></tr>
                                                <tr style="background-color:#99CCFF;"><td>01/01/2012</td><td><a href="#"><img src="../Images/Print.png" /></a></td></tr>
                                                <tr><td>01/01/2012</td><td><a href="#"><img src="../Images/Print.png" /></a></td></tr>
                                                <tr style="background-color:#99CCFF;"><td>01/01/2012</td><td><a href="#"><img src="../Images/Print.png" /></a></td></tr>
                                                </table>--%>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="lnkGenInvSecirity" runat="server" Text="Generate Invoice" ForeColor="Blue"
                                        Enabled="false" OnClick="lnkGenInvSecirity_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td>
                                    <asp:Button ID="btnTemp1" runat="server" Style="display: none;" />
                                    <%--<asp:CheckBox ID="" runat="server" OnCheckedChanged="chkDo_CheckedChanged" AutoPostBack="true" />--%>
                                    <asp:RadioButton ID="chkDo" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkDo_CheckedChanged" Enabled="false" />
                                    <asp:LinkButton ID="lnkDO" Enabled="false" runat="server" Text="Delivery Order" ForeColor="Blue"
                                        OnClick="lnkDO_Click"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="mpeDo" runat="server" PopupControlID="pnlDo" TargetControlID="btnTemp1"
                                        BackgroundCssClass="ModalPopupBG" CancelControlID="imgClose">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlDo" runat="server" Style="display: none;">
                                        <table style="width: 300px; height: 80px; background-color: White; text-align: center;"
                                            border="0" cellspacing="0">
                                            <%-- <tr>
                                                <td colspan="2" align="right">
                                                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Images/close-icon.png" />
                                                </td>
                                            </tr>--%>
                                            <tr style="background-color: #328DC4; height: 28px;">
                                                <td style="font-weight: bold; color: White; font-size: 12pt;">
                                                    Delivery Order
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Images/close-icon.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" style="padding-top: 10px;">
                                                    <asp:ImageButton ID="imgBtnExaminationDo" runat="server" ImageUrl="~/Images/p1.jpeg"
                                                        ToolTip="Print Examination Do" Height="45" Width="45" AlternateText="Print Examination Do"
                                                        OnClick="imgBtnExaminationDo_Click" />
                                                    <br />
                                                    Print Examination Do
                                                </td>
                                                <td width="50%" style="padding-top: 10px;">
                                                    <asp:ImageButton ID="imgBtnFinalDo" runat="server" ImageUrl="~/Images/p2.jpeg" ToolTip="Print Final Do"
                                                        Height="45" Width="45" AlternateText="Print Final Do" Enabled="false" />
                                                    <br />
                                                    Print Final Do
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" height="30px">
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td>
                                    De-stuffing
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDestuffing" runat="server" Enabled="false" Width="79">
                                        <asp:ListItem Text="CFS" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Factory" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="border-right: Solid 1px Black;">
                                    <asp:LinkButton ID="lnkGenerateInvoiceDo" runat="server" Text="Generate Invoice"
                                        ForeColor="Blue" Enabled="false" OnClick="lnkGenerateInvoiceDo_Click"></asp:LinkButton>
                                </td>
                                <td>
                                    <%--<asp:CheckBox ID="" runat="server" AutoPostBack="true" />--%>
                                    <asp:Button ID="btnTemp10" runat="server" Style="display: none;" />
                                    <asp:RadioButton ID="chkFinalInvoice" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkFinalInvoice_CheckedChanged" Enabled="false" />
                                    <asp:LinkButton ID="lnkFinalInvoice" Enabled="false" runat="server" Text="Final Invoice"
                                        ForeColor="Blue" OnClick="lnkFinalInvoice_Click"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="mpeFinalInv" runat="server" PopupControlID="pnlFinalInv"
                                        TargetControlID="btnTemp10" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseFinalInv">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlFinalInv" runat="server" Style="display: none;">
                                        <table style="width: 300px; height: 80px; background-color: White; text-align: center;"
                                            border="0" cellspacing="0">
                                            <%-- <tr>
                                                <td colspan="2" align="right">
                                                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Images/close-icon.png" />
                                                </td>
                                            </tr>--%>
                                            <tr style="background-color: #328DC4; height: 28px;">
                                                <td style="font-weight: bold; color: White; font-size: 12pt;">
                                                    Final Invoice
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgCloseFinalInv" runat="server" ImageUrl="~/Images/close-icon.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" style="padding-top: 10px;" colspan="2">
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/p1.jpeg" ToolTip="Print Examination Do"
                                                        Height="45" Width="45" AlternateText="Print Final Invoice" OnClick="imgBtnExaminationDo_Click" />
                                                    <br />
                                                    Print Final Invoice
                                                </td>
                                                <%--<td width="50%" style="padding-top: 10px;">
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/p2.jpeg" ToolTip="Print Final Do"
                                                        Height="45" Width="45" AlternateText="Print Final Do" Enabled="false" />
                                                    <br />
                                                    Print Final Do
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td colspan="2" height="30px">
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="lnkGenInvFinalDo" runat="server" Text="Generate Invoice" ForeColor="Blue"
                                        Enabled="false" OnClick="lnkGenInvFinalDo_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td>
                                    <asp:Button ID="btnTemp2" runat="server" Style="display: none;" />
                                    <%--<asp:CheckBox ID="" Enabled="false" runat="server" OnCheckedChanged="chkDoExtension_CheckedChanged"
                                        AutoPostBack="true" />--%>
                                    <asp:RadioButton ID="chkDoExtension" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkDoExtension_CheckedChanged" Enabled="false" />
                                    <asp:LinkButton ID="lnkDoExtension" Enabled="false" runat="server" Text="Delivery Order Extension"
                                        ForeColor="Blue" OnClick="lnkDoExtension_Click"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="mpeDOE" runat="server" PopupControlID="pnlDOE" TargetControlID="btnTemp2"
                                        BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseDOE">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlDOE" runat="server" Style="display: none;">
                                        <div style="height: 300; width: 350px;">
                                            <div style="background-color: #328DC4; padding-top: 5px;">
                                                <div style="width: 85%; text-align: left; font-weight: bold; color: White; font-size: 12pt;
                                                    padding-left: 15px; float: left;">
                                                    Delivery Order Extension</div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgCloseDOE" runat="server" ImageUrl="~/Images/close-icon.png"
                                                        Style="display: block;" /></div>
                                                <div style="clear: both;">
                                                </div>
                                            </div>
                                            <div id="dvDoExtension" runat="server" style="width: 100%; height: 200px; overflow: auto;
                                                background-color: White; padding-top: 15px; text-align: center;">
                                                Please enter B/L No.
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td>
                                    Validity Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVAlidityDate" runat="server" Width="75" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtVAlidityDate"
                                        TargetControlID="txtValidityDate" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="right" style="border-right: Solid 1px Black;">
                                    <asp:LinkButton ID="lnkGenerateInvoiceDOE" runat="server" Text="Generate Invoice"
                                        ForeColor="Blue" Enabled="false" OnClick="lnkGenerateInvoiceDOE_Click"></asp:LinkButton>
                                </td>
                                <td>
                                    <%-- <asp:CheckBox ID="" runat="server" AutoPostBack="true" />--%>
                                    <asp:Button ID="btnTemp8" runat="server" Style="display: none;" />
                                    <asp:RadioButton ID="chkOtherInv" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkOtherInv_CheckedChanged" Enabled="false" />
                                    <asp:LinkButton ID="lnkOtherInv" Enabled="false" runat="server" Text="Other Invoice"
                                        ForeColor="Blue" OnClick="lnkOtherInv_Click"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="mpeOi" runat="server" PopupControlID="pnlOI" TargetControlID="btnTemp8"
                                        BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseOI">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlOI" runat="server" Style="display: none;">
                                        <div style="height: 300; width: 350px;">
                                            <div style="background-color: #328DC4; padding-top: 5px;">
                                                <div style="width: 85%; text-align: left; font-weight: bold; color: White; font-size: 12pt;
                                                    padding-left: 15px; float: left;">
                                                    Other Invoice</div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgCloseOI" runat="server" ImageUrl="~/Images/close-icon.png"
                                                        Style="display: block;" /></div>
                                                <div style="clear: both;">
                                                </div>
                                            </div>
                                            <div id="dvOtherInvoice" runat="server" style="width: 100%; height: 200px; overflow: auto;
                                                background-color: White; padding-top: 15px; text-align: center;">
                                                Please enter B/L No.
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="lnkGenInvOtherInvoice" runat="server" Text="Generate Invoice"
                                        ForeColor="Blue" Enabled="false" OnClick="lnkGenInvOtherInvoice_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td>
                                    <asp:Button ID="btnTemp3" runat="server" Style="display: none;" />
                                    <%--<asp:CheckBox ID="" Enabled="false" runat="server" OnCheckedChanged="chkSlotExtension_CheckedChanged"
                                        AutoPostBack="true" />--%>
                                    <asp:RadioButton ID="chkDetensionExtension" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkSlotExtension_CheckedChanged" Enabled="false" />
                                    <asp:LinkButton ID="lnkSlotExtension" Enabled="false" runat="server" Text="Extension for Detention"
                                        ForeColor="Blue" OnClick="lnkSlotExtension_Click"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="mpeSE" runat="server" PopupControlID="pnlSLE" TargetControlID="btnTemp3"
                                        BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseSLE">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlSLE" runat="server" Style="display: none;">
                                        <div style="height: 300; width: 350px;">
                                            <div style="background-color: #328DC4; padding-top: 5px;">
                                                <div style="width: 85%; text-align: left; font-weight: bold; color: White; font-size: 12pt;
                                                    padding-left: 15px; float: left;">
                                                    Extension for Detention</div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgCloseSLE" runat="server" ImageUrl="~/Images/close-icon.png"
                                                        Style="display: block;" /></div>
                                                <div style="clear: both;">
                                                </div>
                                            </div>
                                            <div id="dvEFD" runat="server" style="width: 100%; height: 200px; overflow: auto;
                                                background-color: White; padding-top: 15px; text-align: center;">
                                                Please enter B/L No.
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExtensionForDetention" runat="server" Width="75" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtExtensionForDetention"
                                        TargetControlID="txtExtensionForDetention" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="right" style="border-right: Solid 1px Black;">
                                    <asp:LinkButton ID="lnkGenerateInvoiceSlotExtension" runat="server" Text="Generate Invoice"
                                        ForeColor="Blue" Enabled="false" OnClick="lnkGenerateInvoiceSlotExtension_Click"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:Button ID="btnTemp4" runat="server" Style="display: none;" />
                                    <%-- <asp:CheckBox ID="" Enabled="false" runat="server" AutoPostBack="true"
                                        OnCheckedChanged="chkAmendment_CheckedChanged" />--%>
                                    <asp:RadioButton ID="chkAmendment" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkAmendment_CheckedChanged" Enabled="false" />
                                    Amendment
                                    <%--  <asp:LinkButton ID="lnkAmendment" Enabled="false" runat="server" Text="Amendment"
                                        ForeColor="Blue" OnClick="lnkAmendment_Click"></asp:LinkButton>--%>
                                    <cc1:ModalPopupExtender ID="mpeAmend" runat="server" PopupControlID="pnlAmendment"
                                        TargetControlID="btnTemp4" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseAmend">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlAmendment" runat="server" Style="display: none;">
                                        <table style="width: 350px; height: 200px; background-color: White; text-align: center;"
                                            border="0" cellspacing="0">
                                            <tr style="background-color: #328DC4; height: 28px;">
                                                <td style="font-weight: bold; color: White; font-size: 12pt; width: 40%;">
                                                    Amendment Entry
                                                </td>
                                                <td align="right" style="width: 60%;">
                                                    <asp:ImageButton ID="imgCloseAmend" runat="server" ImageUrl="~/Images/close-icon.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--<td width="50%">
                                                    <asp:ImageButton ID="imgBtnAmend" runat="server" ImageUrl="~/Images/p1.jpeg" ToolTip="Print Amendment"
                                                        Height="45" Width="45" AlternateText="Print Amendment" />
                                                    <br />
                                                    Print Amendment
                                                </td>--%>
                                                <td>
                                                    Shown As
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtAmendShownAs" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Should Be
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtAmendShouldBe" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Comment
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtAmendComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:ImageButton ID="imgPrintAmend" runat="server" ImageUrl="~/Images/printer.png"
                                                        AlternateText="Print" ToolTip="Print" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td>
                                    Amendment For
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAmendmentFor" runat="server" Width="79" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="lnkPrintAmend" ForeColor="Blue" runat="server" Enabled="false"
                                        Text="Print Amendment"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td align="left">
                                    <%--<asp:CheckBox ID="" Enabled="false" runat="server" AutoPostBack="true" />--%>
                                    <asp:Button ID="btnTemp6" runat="server" Style="display: none;" />
                                    <asp:RadioButton ID="chkPGRExtension" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkPGRExtension_CheckedChanged" Enabled="false" />
                                    <asp:LinkButton ID="lnkPGRExtension" Enabled="false" runat="server" Text="Extension for PGR"
                                        ForeColor="Blue" OnClick="lnkPGRExtension_Click"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="mpePGR" runat="server" PopupControlID="pnlPGR" TargetControlID="btnTemp6"
                                        BackgroundCssClass="ModalPopupBG" CancelControlID="imgClosePGR">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlPGR" runat="server" Style="display: none;">
                                        <div style="height: 300; width: 350px;">
                                            <div style="background-color: #328DC4; padding-top: 5px;">
                                                <div style="width: 85%; text-align: left; font-weight: bold; color: White; font-size: 12pt;
                                                    padding-left: 15px; float: left;">
                                                    Extension for PGR</div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgClosePGR" runat="server" ImageUrl="~/Images/close-icon.png"
                                                        Style="display: block;" /></div>
                                                <div style="clear: both;">
                                                </div>
                                            </div>
                                            <div id="dvPGR" runat="server" style="width: 100%; height: 200px; overflow: auto;
                                                background-color: White; padding-top: 15px; text-align: center;">
                                                Please enter B/L No.
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExtensionForPGR" runat="server" Width="75" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtExtensionForPGR"
                                        TargetControlID="txtExtensionForPGR" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="right" style="border-right: Solid 1px Black;">
                                    <asp:LinkButton ID="lnkGenInvPGR" runat="server" Text="Generate Invoice" ForeColor="Blue"
                                        Enabled="false" OnClick="lnkGenInvPGR_Click"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:Button ID="btnTemp5" runat="server" Style="display: none;" />
                                    <%-- <asp:CheckBox ID="" Enabled="false" runat="server" AutoPostBack="true"
                                        OnCheckedChanged="chkBondCancel_CheckedChanged" />--%>
                                    <asp:RadioButton ID="chkBondCancel" runat="server" GroupName="Service" AutoPostBack="true"
                                        OnCheckedChanged="chkBondCancel_CheckedChanged" Enabled="false" />
                                    <asp:LinkButton ID="lnkBondCancel" Enabled="false" runat="server" Text="Bond Cancellation"
                                        ForeColor="Blue" OnClick="lnkBondCancel_Click"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="mpeBond" runat="server" PopupControlID="pnlBondCancel"
                                        TargetControlID="btnTemp5" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseBC">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlBondCancel" runat="server" Style="display: none;">
                                        <table style="width: 300px; height: 80px; background-color: White; text-align: center;"
                                            border="0" cellspacing="0">
                                            <tr style="background-color: #328DC4; height: 28px;">
                                                <td style="font-weight: bold; color: White; font-size: 12pt; padding-left: 25px;
                                                    text-align: left;">
                                                    Bond Cancellation
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgCloseBC" runat="server" ImageUrl="~/Images/close-icon.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" colspan="2" style="padding-top: 10px;">
                                                    <asp:ImageButton ID="imgBtnBC" runat="server" ImageUrl="~/Images/p1.jpeg" ToolTip="Print Bond Cancellation"
                                                        Height="45" Width="45" AlternateText="Print Bond Cancellation" />
                                                    <br />
                                                    Print Bond Cancellation
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" height="30px">
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td>
                                    Cancellation Date
                                </td>
                                <td style="width: 11%;">
                                    <asp:TextBox ID="txtBondCancellation" runat="server" Width="75" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtBondCancellation"
                                        TargetControlID="txtBondCancellation" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="btnBondSave" ForeColor="Blue" runat="server" Enabled="false"
                                        Text="Save Bond Cancellation"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="txtBlNo" />
                    </Triggers>
                </asp:UpdatePanel>
            </fieldset>
            <div style="float: left; width: 60%;">
                <fieldset>
                    <legend>Documents submitted</legend>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkOriginalBL" runat="server" Text="Original B/Ls" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkInsuranceCopy" runat="server" Text="Insurance Copy" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCopyOfBill" runat="server" Text="Copy of Bill of entry / TR6" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkEndorseHBL" runat="server" Text="Endorse HBL" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCopyOfMasterBL" runat="server" Text="Copy of Master B/L" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkConsoldatorNOC" runat="server" Text="Consoldator's NOC" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkContainerBond" runat="server" Text="Container Bond" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSecurityCheque" runat="server" Text="Security Cheque" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCHSSA" runat="server" Text="Copy of High Seas Sales Agreement" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkBankGuarantee" runat="server" Text="Bank Guarantee" />
                            </td>
                            <td>
                            </td>
                            <td>
                                <div style="float: right; z-index: 100;">
                                    <asp:Button ID="btnDocSubmit" runat="server" Text="Save" OnClick="btnDocSubmit_Click" />
                                    <%-- <asp:Button ID="btnDocback" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                        OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />--%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div style="float: left; width: 40%;">
                <fieldset style="height: 125px;">
                    <legend>Documents uploaded</legend>
                    <table style="font-size: smaller; height: 25px; width: 100%; text-align: center;"
                        cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="width: 10%">
                                <asp:DropDownList ID="ddlUploadedDoc" runat="server" Width="70">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 5%; text-align: left;">
                                <asp:FileUpload ID="FileUpload1" runat="server" size="5%" />
                            </td>
                            <td style="width: 5%; text-align: left;">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" Style="height: 22px;" OnClick="btnUpload_Click" />
                            </td>
                            <td style="width: 80%;">
                                <asp:Label ID="lblUploadMsg" runat="server" Style="color: #FF0000"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <script language="javascript" type="text/javascript">
                        function DeleteUploadedDoc(sndr) {
                            var Id = sndr.id;
                            document.getElementById('<%= hdnUploadedDocId.ClientID %>').value = Id;
                            document.getElementById('<%= btnDeleteDoc.ClientID %>').click();
                        }
                    </script>
                    <asp:HiddenField ID="hdnUploadedDocId" runat="server" Value="0" />
                    <asp:Button ID="btnDeleteDoc" runat="server" OnClick="DeleteUploadedDoc" Style="display: none;" />
                    <%--<input type="image" onserverclick="DeleteUploadedDoc" id="imgDelDoc" runat="server" src="~/Images/remove.png" style="height:20px; width:20px;" />--%>
                    <div id="dvDoc" runat="server" style="height: 78px; overflow: auto; vertical-align: top;">
                    </div>
                </fieldset>
            </div>
            <fieldset style="width: 98%;">
                <legend>Invoice Status</legend>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Button ID="btnTemp11" runat="server" Style="display: none;" />
                            <cc1:ModalPopupExtender ID="mpeMoneyReceivedDetail" runat="server" PopupControlID="pnlMoneyReceived"
                                TargetControlID="btnTemp11" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseMoneyReceived">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlMoneyReceived" runat="server" Style="display: none;">
                                <div style="height: 300; width: 600px;overflow:auto;">
                                    <div style="background-color: #328DC4; padding-top: 5px;">
                                        <div style="width: 89%; text-align: left; font-weight: bold; color: White; font-size: 12pt;
                                            padding-left: 15px; float: left;">
                                            Received Amount</div>
                                        <div style="float: left;">
                                            <asp:ImageButton ID="imgCloseMoneyReceived" runat="server" ImageUrl="~/Images/close-icon.png"
                                                Style="display: block;" /></div>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                    <div id="dvMoneyReceived" runat="server" style="width: 100%; height: 300px; overflow: auto;
                                        background-color: White; padding-top: 15px; text-align: center;">
                                        Please enter B/L No.
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:GridView ID="gvwInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" Width="100%">
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Record(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            Invoice Type</HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("InvoiceType")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            Invoice No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <a id="aInvoice" runat="server" href='<%# "ManageInvoice.aspx?invid=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(Eval("InvoiceID").ToString()) %>'>
                                                <%# Eval("InvoiceNo")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <HeaderStyle CssClass="gridviewheader_num" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Right" />
                                        <HeaderTemplate>
                                            Invoice Amount</HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("Ammount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader_num" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Right" />
                                        <HeaderTemplate>
                                            Received Amount</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnInvID" runat="server" Value='<%# Eval("InvoiceID")%>' />
                                            <a href="#" runat="server" onserverclick="ShowReceivedAmt">
                                                <%# Eval("ReceivedAmt")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader_num" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Right" />
                                        <HeaderTemplate>
                                            CRN Amount</HeaderTemplate>
                                        <ItemTemplate>
                                            <a href="#">N/A</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader_center" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            Print</HeaderTemplate>
                                        <ItemTemplate>
                                            <a href="#?<%# Eval("BlId") %>">
                                                <img src="../Images/Print.png" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader_center" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            Add Money Recpt.</HeaderTemplate>
                                        <ItemTemplate>
                                           <a id="aMoneyRecpt" runat="server" href='<%# "AddEditMoneyReceipts.aspx?invid=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(Eval("InvoiceID").ToString()) %>'
                                            style='<%# Convert.ToDecimal(Eval("ReceivedAmt")) < Convert.ToDecimal(Eval("Ammount")) ? "display:block;" : "display:none;" %>'>
                                            <img alt="Add" src="../Images/ADD.JPG" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField >
                                        <HeaderStyle CssClass="gridviewheader_center" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            Add Credit Note</HeaderTemplate>
                                        <ItemTemplate>
                                            <a href="#">
                                                <img alt="Add" src="../Images/ADD.JPG" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
</asp:Content>
