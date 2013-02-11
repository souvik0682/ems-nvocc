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
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td style="width: 150px;">
                        </td>
                        <td style="width: 150px;">
                            B/L No<span class="errormessage1">*</span> :
                        </td>
                        <td style="width: 375px;">
                            <asp:TextBox ID="txtBlNo" runat="server" Width="150" AutoCompleteType="None"></asp:TextBox>
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
                            Delivered To (CHA)<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDeleveredToCha" runat="server" Width="155">
                            </asp:DropDownList>
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
                            House B/L No<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtHouseBlNo" runat="server" Width="150" Style="text-transform: uppercase;"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvChargeTitle" runat="server" ErrorMessage="Please enter charge title"
                                        ControlToValidate="txtChargeName" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                    <cc1:validatorcalloutextender id="ValidatorCalloutExtender11" runat="server" targetcontrolid="rfvChargeTitle">
                                    </cc1:validatorcalloutextender>--%>
                        </td>
                        <td>
                            Free Days<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFreedays" runat="server" Width="150" Style="text-transform: uppercase;"></asp:TextBox>
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
                            DO validated up to<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtValidUpto" runat="server" Width="150"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation" WarningIconImageUrl="" >
                            </cc1:ValidatorCalloutExtender>--%>
                        </td>
                        <td>
                            Detension Charges Up To<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDetentionChargeUpto" runat="server" Width="150"></asp:TextBox>
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
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnBlQueryID" runat="server" Value="0" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgCharge" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="width: 90%;">
                <legend>Service Requests</legend>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkDo" runat="server" />
                            <asp:LinkButton ID="lnkDO" runat="server" Text="Delivery Order"></asp:LinkButton>
                        </td>
                        <td>
                            De-stuffing
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDestuffing" runat="server" Width="130">
                                <asp:ListItem Text="CFS" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Factory" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CheckBox ID="ckkFreightPaid" runat="server" />Freight Paid
                        </td>
                        <td>
                            <asp:CheckBox ID="chkBankGuarantee" runat="server" />Bank Guarantee
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkGenerateInvoiceDo" runat="server" Text="Generate Invoice"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkDoExtension" runat="server" />
                            <asp:LinkButton ID="lnkDoExtension" runat="server" Text="Delivery Order Extension"></asp:LinkButton>
                        </td>
                        <td>
                            Validity Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtVAlidityDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtVAlidityDate"
                                TargetControlID="txtValidityDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkGenerateInvoiceDOE" runat="server" Text="Generate Invoice"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkSlotExtension" runat="server" />
                            <asp:LinkButton ID="lnkSlotExtension" runat="server" Text="Slot Extension"></asp:LinkButton>
                        </td>
                        <td>
                            Extension Upto
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtVAlidityDate"
                                TargetControlID="txtValidityDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Generate Invoice"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="width: 90%;">
                <legend>Documents submitted</legend>
            </fieldset>
        </center>
    </div>
</asp:Content>
