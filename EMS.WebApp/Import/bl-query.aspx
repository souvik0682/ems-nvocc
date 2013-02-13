<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="bl-query.aspx.cs" Inherits="EMS.WebApp.Import.bl_query" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            B/L QUERY</div>
        <center>
            <fieldset style="width: 90%;">
                <legend>B/L Detail</legend>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td style="width: 50px;">
                                </td>
                                <td style="width: 150px;">
                                    B/L No :
                                </td>
                                <td style="width: 230px;">
                                    <asp:TextBox ID="txtBlNo" runat="server" Width="150" AutoCompleteType="None" 
                                        AutoPostBack="True" ontextchanged="txtBlNo_TextChanged"></asp:TextBox>
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
                                <td style="width: 150px;">
                                    Delivered To (CHA)
                                </td>
                                <td>
                                    <%--<asp:DropDownList ID="ddlDeleveredToCha" runat="server" Width="155">
                            </asp:DropDownList>--%>
                                    <asp:TextBox ID="txtCha" runat="server" Width="150"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvTerminalRequired" runat="server" ErrorMessage="Please select your choice"
                                        Display="None" ControlToValidate="rdbPrincipleSharing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:validatorcalloutextender id="ValidatorCalloutExtender18" runat="server" targetcontrolid="rfvTerminalRequired">
                                    </cc1:validatorcalloutextender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    House B/L No :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHouseBlNo" runat="server" Width="150" Style="text-transform: uppercase;"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvChargeTitle" runat="server" ErrorMessage="Please enter charge title"
                                        ControlToValidate="txtChargeName" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:validatorcalloutextender id="ValidatorCalloutExtender11" runat="server" targetcontrolid="rfvChargeTitle">
                                    </cc1:validatorcalloutextender>--%>
                                </td>
                                <td>
                                    Detention Fee :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDetentionFee" runat="server" Width="150" Style="text-transform: uppercase;"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvPS" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbPrincipleSharing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="rfvPS">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    DO validated up to :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDoValidUpto" runat="server" Width="150"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    Landing Date :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLandingDate" runat="server" Width="150"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Vessel :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVessel" runat="server" Width="150"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    Voyage :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVoyage" runat="server" Width="150"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Detention Free Days :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDetentionFreeDays" runat="server" Width="150"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    Detention till :
                                </td>
                                <td>
                                    <asp:TextBox ID="tstDetentionTill" runat="server" Width="150"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    PGR free days :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPGRFreedays" runat="server" Width="150"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    PGR till :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPGRTill" runat="server" Width="150"></asp:TextBox>
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
            <fieldset style="width: 90%;">
                <legend>Service Requests</legend>
                <table width="100%" border="0" cellspacing="0">
                    <tr style="height: 30px;">
                        <td>
                            <asp:CheckBox ID="chkDo" runat="server" />
                            <asp:LinkButton ID="lnkDO" runat="server" Text="Delivery Order" ForeColor="Blue"></asp:LinkButton>
                        </td>
                        <td>
                            De-stuffing
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDestuffing" runat="server" Width="55">
                                <asp:ListItem Text="CFS" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Factory" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Do-Valid Till
                        </td>
                        <td>
                            <asp:TextBox ID="txtDoValidTill" runat="server" Width="75"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtDoValidTill"
                                TargetControlID="txtDoValidTill">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            <asp:CheckBox ID="ckkFreightPaid" runat="server" />Freight Paid
                        </td>
                        <td>
                            <asp:CheckBox ID="chkBankGuarantee" runat="server" />Bank Guarantee
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lnkGenerateInvoiceDo" runat="server" Text="Generate Invoice"
                                ForeColor="Blue"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td>
                            <asp:CheckBox ID="chkDoExtension" runat="server" />
                            <asp:LinkButton ID="lnkDoExtension" runat="server" Text="Delivery Order Extension"
                                ForeColor="Blue"></asp:LinkButton>
                        </td>
                        <td>
                            Validity Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtVAlidityDate" runat="server" Width="75"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtVAlidityDate"
                                TargetControlID="txtValidityDate">
                            </cc1:CalendarExtender>
                        </td>
                        <%--<td colspan="4">
                        </td>  --%>
                        <td colspan="5" align="right">
                            <asp:LinkButton ID="lnkGenerateInvoiceDOE" runat="server" Text="Generate Invoice"
                                ForeColor="Blue"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td>
                            <asp:CheckBox ID="chkSlotExtension" runat="server" />
                            <asp:LinkButton ID="lnkSlotExtension" runat="server" Text="Slot Extension" ForeColor="Blue"></asp:LinkButton>
                        </td>
                        <td>
                            Extension For<br />
                            Detention
                        </td>
                        <td>
                            <asp:TextBox ID="txtExtensionForDetention" runat="server" Width="75"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtExtensionForDetention"
                                TargetControlID="txtExtensionForDetention">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            Extension For<br />
                            PGR
                        </td>
                        <td>
                            <asp:TextBox ID="txtExtensionForPGR" runat="server" Width="75"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtExtensionForPGR"
                                TargetControlID="txtExtensionForPGR">
                            </cc1:CalendarExtender>
                        </td>
                        <td colspan="3" align="right">
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Generate Invoice" ForeColor="Blue"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td>
                            <asp:CheckBox ID="chkAmendment" runat="server" />
                            <asp:LinkButton ID="lnkAmendment" runat="server" Text="Amendment" ForeColor="Blue"></asp:LinkButton>
                        </td>
                        <td>
                            Amendment For
                        </td>
                        <td colspan="5">
                            <asp:DropDownList ID="ddlAmendmentFor" runat="server" Width="55">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td>
                            <asp:CheckBox ID="chkBondCancel" runat="server" />
                            <asp:LinkButton ID="lnkBondCancel" runat="server" Text="Bond Cancellation" ForeColor="Blue"></asp:LinkButton>
                        </td>
                        <td>
                            Cancellation Date
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtBondCancellation" runat="server" Width="75"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtBondCancellation"
                                TargetControlID="txtBondCancellation">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr style="height: 50px; vertical-align: bottom;">
                        <td colspan="3">
                        </td>
                        <td colspan="5" align="left">
                            <asp:Button ID="btnSave2" runat="server" Text="Save" ValidationGroup="vgCharge" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack2" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                            <asp:Label ID="lblMessageServiceReq" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="width: 90%;">
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
                        <td>
                            <asp:CheckBox ID="chkFreightPaid" runat="server" Text="Freight Prepaid" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkBLSurrender" runat="server" Text="B/L Surrender" />
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
                        <td>
                            <asp:CheckBox ID="chkCFSNomination" runat="server" Text="CFS Nomination" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkBLExtension" runat="server" Text="B/L Extension" />
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
                            <asp:CheckBox ID="chkCHSSA" runat="server" Text="Copy of High Seas Sales Agreement" />
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkDetentionWaiver" runat="server" Text="Detention Waiver" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="width: 90%;">
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
                                    <asp:TemplateField HeaderText="Sl#">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            Invoice Type</HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            Document No.</HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            Ref. Inv. No.</HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            Invoice Amount</HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            Payment Received</HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            CRN Amount</HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" />
                                        <HeaderTemplate>
                                            Print</HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            Money Recpt.</HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            Credit Note</HeaderTemplate>
                                        <ItemTemplate>
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
