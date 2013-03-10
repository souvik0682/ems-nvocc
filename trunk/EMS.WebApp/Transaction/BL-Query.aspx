<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BL-Query.aspx.cs" Inherits="EMS.WebApp.Transaction.BL_Query" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            B/L QUERY</div>
        <center>
            <fieldset style="width: 95%;">
                <legend>B/L Detail</legend>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td style="width: 15%;">
                                    B/L No :
                                </td>
                                <td style="width: 10%;">
                                    <asp:HiddenField ID="hdnBLId" runat="server" Value="0" />
                                    <asp:TextBox ID="txtBlNo" runat="server" Width="100" AutoCompleteType="None" AutoPostBack="True"
                                        OnTextChanged="txtBlNo_TextChanged"></asp:TextBox>
                                    <%--<cc1:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="txtEffectDate"
                                        popupposition="BottomLeft" targetcontrolid="txtEffectDate" format="dd/MM/yyyy"
                                        onclientdateselectionchanged="checkDate">
                                    </cc1:calendarextender>
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Please select date"
                                        ControlToValidate="txtEffectDate" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:validatorcalloutextender id="ValidatorCalloutExtender10" runat="server" targetcontrolid="rfvDate">
                                    </cc1:validatorcalloutextender>
                                    <cc1:filteredtextboxextender id="FilteredTextBoxExtender1" runat="server" targetcontrolid="txtEffectDate"
                                        filtermode="ValidChars" filtertype="Numbers,Custom" validchars="/">
                                    </cc1:filteredtextboxextender>--%>
                                </td>
                                <td style="width: 15%;">
                                    Delivered To (CHA) :
                                </td>
                                <td style="width: 10%;">
                                    <%--<asp:DropDownList ID="ddlDeleveredToCha" runat="server" Width="155">
                            </asp:DropDownList>--%>
                                    <asp:TextBox ID="txtCha" runat="server" Width="100"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvTerminalRequired" runat="server" ErrorMessage="Please select your choice"
                                        Display="None" ControlToValidate="rdbPrincipleSharing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:validatorcalloutextender id="ValidatorCalloutExtender18" runat="server" targetcontrolid="rfvTerminalRequired">
                                    </cc1:validatorcalloutextender>--%>
                                </td>
                                <td style="width: 11%;">
                                    House B/L No :
                                </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtHouseBlNo" runat="server" Width="100" Style="text-transform: uppercase;"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvChargeTitle" runat="server" ErrorMessage="Please enter charge title"
                                        ControlToValidate="txtChargeName" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:validatorcalloutextender id="ValidatorCalloutExtender11" runat="server" targetcontrolid="rfvChargeTitle">
                                    </cc1:validatorcalloutextender>--%>
                                </td>
                                <td style="width: 11%;">
                                    Detention Fee :
                                </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtDetentionFee" runat="server" Width="100" Style="text-transform: uppercase;"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvPS" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbPrincipleSharing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="rfvPS">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    DO validated up to :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDoValidUpto" runat="server" Width="100"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    Landing Date :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLandingDate" runat="server" Width="100"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    Vessel :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVessel" runat="server" Width="100"></asp:TextBox>
                                    <%--<asp:ImageButton ID='btnRemove' runat='server' CommandName='Remove' ImageUrl='~/Images/remove.png' Height='16' Width='16' />--%>
                                    <%--<input type="image" onserverclick="DeleteUploadedDoc" id="imgDelDoc" runat="server" src="~/Images/remove.png" style="height:20px; width:20px;" />--%>
                                    <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    Voyage :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVoyage" runat="server" Width="100"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Detention Free Days :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDetentionFreeDays" runat="server" Width="100"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    Detention till :
                                </td>
                                <td>
                                    <asp:TextBox ID="tstDetentionTill" runat="server" Width="100"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    PGR free days :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPGRFreedays" runat="server" Width="100"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
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
                                    <asp:HiddenField ID="hdnBlQueryID" runat="server" Value="0" />
                                    <asp:Button ID="btnSave1" runat="server" Text="Save" ValidationGroup="vgCharge" />&nbsp;&nbsp;<asp:Button
                                        ID="btnBack1" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                        OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                                    <asp:Label ID="lblMessageBLQuery" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <fieldset style="width: 95%;">
                <legend>Service Requests</legend>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%" border="0" cellspacing="0">
                            <tr style="height: 30px;">
                                <td>
                                    <asp:CheckBox ID="chkDo" runat="server" OnCheckedChanged="chkDo_CheckedChanged" AutoPostBack="true" />
                                    <asp:LinkButton ID="lnkDO" runat="server" Text="Delivery Order" ForeColor="Blue"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlDo"
                                        TargetControlID="lnkDO" BackgroundCssClass="ModalPopupBG" CancelControlID="imgClose">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlDo" runat="server">
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
                                    <asp:DropDownList ID="ddlDestuffing" runat="server" Width="55" Enabled="false">
                                        <asp:ListItem Text="CFS" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Factory" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    DO-Valid Till
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDoValidTill" runat="server" Width="75" Enabled="false"></asp:TextBox>
                                    <%-- <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtDoValidTill"
                                TargetControlID="txtDoValidTill">
                            </cc1:CalendarExtender>--%>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkFreightPaidstatus" runat="server" Enabled="false" />Freight
                                    Paid
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkBankGuarantee" runat="server" Enabled="false" />Bank Guarantee
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="lnkGenerateInvoiceDo" runat="server" Text="Generate Invoice"
                                        ForeColor="Blue" Enabled="false" OnClick="lnkGenerateInvoiceDo_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td>
                                    <asp:CheckBox ID="chkDoExtension" runat="server" OnCheckedChanged="chkDoExtension_CheckedChanged"
                                        AutoPostBack="true" />
                                    <asp:LinkButton ID="lnkDoExtension" runat="server" Text="Delivery Order Extension"
                                        ForeColor="Blue"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlDOE"
                                        TargetControlID="lnkDoExtension" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseDOE">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlDOE" runat="server">
                                        <div style="height: 300; width: 350px;">
                                            <div style="background-color: #328DC4; padding-top: 5px; ">
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
                                                background-color: White;padding-top:15px;text-align:center; ">
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
                                    Validity Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVAlidityDate" runat="server" Width="75" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtVAlidityDate"
                                        TargetControlID="txtValidityDate" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <%--<td colspan="4">
                        </td>  --%>
                                <td colspan="5" align="right">
                                    <asp:LinkButton ID="lnkGenerateInvoiceDOE" runat="server" Text="Generate Invoice"
                                        ForeColor="Blue" Enabled="false" OnClick="lnkGenerateInvoiceDOE_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td>
                                    <asp:CheckBox ID="chkSlotExtension" runat="server" OnCheckedChanged="chkSlotExtension_CheckedChanged"
                                        AutoPostBack="true" />
                                    <asp:LinkButton ID="lnkSlotExtension" runat="server" Text="Slot Extension" ForeColor="Blue"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlSLE"
                                        TargetControlID="lnkSlotExtension" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseSLE">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlSLE" runat="server">
                                        <table style="width: 300px; height: 80px; background-color: White; text-align: center;"
                                            border="0" cellspacing="0">
                                            <tr style="background-color: #328DC4; height: 28px;">
                                                <td style="font-weight: bold; color: White; font-size: 12pt; text-align: left; padding-left: 25px;">
                                                    Slot Extension
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgCloseSLE" runat="server" ImageUrl="~/Images/close-icon.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" colspan="2" style="padding: 10px;">
                                                    <asp:ImageButton ID="imgBtnSLE" runat="server" ImageUrl="~/Images/p1.jpeg" ToolTip="Print Slote Extension"
                                                        Height="45" Width="45" AlternateText="Print Slote Extension" />
                                                    <br />
                                                    Print Slot Extension
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
                                    For Detention
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExtensionForDetention" runat="server" Width="75" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtExtensionForDetention"
                                        TargetControlID="txtExtensionForDetention" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    For PGR
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExtensionForPGR" runat="server" Width="75" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtExtensionForPGR"
                                        TargetControlID="txtExtensionForPGR" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td colspan="3" align="right">
                                    <asp:LinkButton ID="lnkGenerateInvoiceSlotExtension" runat="server" Text="Generate Invoice"
                                        ForeColor="Blue" Enabled="false" OnClick="lnkGenerateInvoiceSlotExtension_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td>
                                    <asp:CheckBox ID="chkAmendment" runat="server" AutoPostBack="true" OnCheckedChanged="chkAmendment_CheckedChanged" />
                                    <asp:LinkButton ID="lnkAmendment" runat="server" Text="Amendment" ForeColor="Blue"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlAmendment"
                                        TargetControlID="lnkAmendment" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseAmend">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlAmendment" runat="server">
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
                                <td colspan="5">
                                    <asp:DropDownList ID="ddlAmendmentFor" runat="server" Width="79" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td>
                                    <asp:CheckBox ID="chkBondCancel" runat="server" AutoPostBack="true" OnCheckedChanged="chkBondCancel_CheckedChanged" />
                                    <asp:LinkButton ID="lnkBondCancel" runat="server" Text="Bond Cancellation" ForeColor="Blue"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" PopupControlID="pnlBondCancel"
                                        TargetControlID="lnkBondCancel" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseBC">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlBondCancel" runat="server">
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
                                <td colspan="4" align="left">
                                    <asp:Button ID="btnSave2" runat="server" Text="Save" ValidationGroup="vgCharge" OnClick="btnSave2_Click" />&nbsp;&nbsp;<asp:Button
                                        ID="btnBack2" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                        OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                                    <asp:Label ID="lblMessageServiceReq" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="txtBlNo" />
                    </Triggers>
                </asp:UpdatePanel>
            </fieldset>
            <div style="float: left; width: 60%; padding-left: 1.5%;">
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
            <div style="float: left; width: 37%;">
                <fieldset style="height: 102px;">
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
                    <div id="dvDoc" runat="server" style="height: 58px; overflow: auto; vertical-align: top;">
                    </div>
                </fieldset>
            </div>
            <fieldset style="width: 95%;">
                <legend>Invoice Status</legend>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="gvwVendor" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" Width="100%">
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Record(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <%-- <asp:TemplateField HeaderText="Sl#">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="2%" />
                                    </asp:TemplateField>--%>
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
                                            <%# Eval("InvoiceNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Name">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            Ref. Inv. No.</HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Address">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            Invoice Amount</HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("Ammount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            Received Amount</HeaderTemplate>
                                        <ItemTemplate>
                                            <a href="#">
                                                <%# Eval("ReceivedAmt")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            CRN Amount</HeaderTemplate>
                                        <ItemTemplate>
                                            <a href="#">Yet to be decided</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" />
                                        <HeaderTemplate>
                                            Print</HeaderTemplate>
                                        <ItemTemplate>
                                            <a href="#?<%# Eval("BlId") %>">
                                                <img src="../Images/Print.png" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            Money Recpt.</HeaderTemplate>
                                        <ItemTemplate>
                                        <a href="#"><img alt="Add" tooltip="add" src="../Images/add.jpeg" height="16" width="16" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            Credit Note</HeaderTemplate>
                                        <ItemTemplate>
                                        <a href="#"><img alt="Add" src="../Images/add.jpeg" height="16" width="16" /></a>
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
