<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Export-bl-query.aspx.cs" Inherits="EMS.WebApp.Transaction.Export_bl_query" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ReportPrint1(a, b, c, d, e) {
            window.open('../Popup/Report.aspx?' + a + b + c + d + e, 'mywindow', 'status=1,toolbar=1,location=no,height = 550, width = 800');
            return false;
        }

        function ReportPrint2(a, b, c, d, e, f) {
            window.open('../Popup/Report.aspx?' + a + b + c + d + e + f, 'mywindow', 'status=1,toolbar=1,location=no,height = 550, width = 800');
            return false;
        }

    </script>
    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {

            if (sender._id == "AutoCompleteEx") {
                var hdnBLId = $get('<%=hdnBLId.ClientID %>');
                hdnBLId.value = e.get_value();
            }

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 5%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="progress">
                <div id="image">
                    <img src="<%=Page.ResolveClientUrl("~/Images/PleaseWait.gif") %>" alt="" /></div>
                <div id="text">
                    Please Wait...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="progress">
                <div id="image">
                    <img src="<%=Page.ResolveClientUrl("~/Images/PleaseWait.gif") %>" alt="" /></div>
                <div id="text">
                    Please Wait...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Button ID="btnReset" runat="server" Style="display: none;" Text="Reset" />
    <div>
        <div id="headercaption">
            EXPORT DASHBOARD</div>
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset style="width: 98%; display: block;">
                        <legend>B/L Detail</legend>
                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                                <td style="width: 7%;">
                                     B/L No. 
                                </td>
                                <td style="width: 5%;">
                                    <asp:HiddenField ID="hdnBLId" runat="server" Value="0" />
                                    <asp:TextBox ID="txtBlNo" runat="server" Width="160" AutoPostBack="True" onkeyup="SetContextKey();"
                                        Enabled="false" Style="text-transform: uppercase;" 
                                        ontextchanged="txtBlNo_TextChanged"></asp:TextBox>
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
                                <td style="width: 2%;">
                                    POR
                                </td>
                                <td style="width: 5%;">
   <%--                                 <asp:TextBox ID="txtPOR" runat="server"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtPOR" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 8%;">
                                    Vessel
                                </td>
                                <td style="width: 5%;">
                                    <asp:TextBox ID="txtVessel" runat="server" Width="130" AutoPostBack="True"
                                        onkeyup="SetContextKey();" Enabled="false" Style="text-transform: uppercase;"></asp:TextBox>
                                </td>
                                <td style="width: 2%;">
                                    Booking Party :
                                </td>
                                <td style="width: 20%;">
                                    <asp:TextBox ID="txtBookingParty" runat="server" Width="350" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Booking Date :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBookingDate" runat="server" Width="160" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 3%;">
                                    POL :
                                </td>
                                <td>
                                    <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                                   <asp:TextBox ID="txtPOL" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Voyage :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVoyage" runat="server" Width="130" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 11%;">
                                    Shipper :
                                </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtShipper" runat="server" Width="350" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   Booking No
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBookingNo" runat="server" Width="160"></asp:TextBox>
                                </td>
                                <td class="style1">
                                    POD :
                                </td>
                                <td>
                                    <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                                    <asp:TextBox ID="txtPOD" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Containers :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContainer" runat="server" Width="130" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Remarks :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="350"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>BL Date :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBLDate" runat="server"  Width="160"></asp:TextBox>
                                </td>
                                <td class="style1">
                                    FPOD :
                                </td>
                                <td>
                                    <%--<asp:RequiredFieldValidator ID="rfvWashing" runat="server" ErrorMessage="Please select your choice"
                                Display="None" ControlToValidate="rdbWashing" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvWashing">
                            </cc1:ValidatorCalloutExtender>--%>
                                    <asp:TextBox ID="txtFPOD" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Shipment Type :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShipmentType" runat="server" Width="130" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <td>
                                        <a href="#">DOWNLOAD</a>
                                    </td>
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
                                        OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" 
                                        onclick="btnBack1_Click" />
                                    <asp:Label ID="lblMessageBLQuery" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <fieldset style="width: 98%;">
                        <legend>Invoice Status</legend>
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right;">

                                    <asp:Button ID="btnExit" runat="server" Text="Exit To Exp BL" 
                                        ValidationGroup="BQExit" onclick="btnExit_Click" />
                                    <asp:Button ID="btnAddFreightInvoice" runat="server" Text="Add Freight Invoice" 
                                        OnClientClick="return false;" onclick="btnAddFreightInvoice_Click" />
                                    <asp:Button ID="btnAddOtherInvoice" runat="server" Text="Add Other Invoice" OnClientClick="return false;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnTemp11" runat="server" Style="display: none;" />
                                    <cc1:ModalPopupExtender ID="mpeMoneyReceivedDetail" runat="server" PopupControlID="pnlMoneyReceived"
                                        TargetControlID="btnTemp11" BackgroundCssClass="ModalPopupBG" CancelControlID="imgCloseMoneyReceived">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlMoneyReceived" runat="server" Style="display: none;">
                                        <div style="height: 300; width: 600px; overflow: auto;">
                                            <div style="background-color: #328DC4; padding-top: 5px;">
                                                <div id="headerTest" runat="server" style="width: 89%; text-align: left; font-weight: bold;
                                                    color: White; font-size: 12pt; padding-left: 15px; float: left;">
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgCloseMoneyReceived" runat="server" ImageUrl="~/Images/close-icon.png"
                                                        Style="display: block;" /></div>
                                                <div style="clear: both;">
                                                </div>
                                            </div>
                                            <div id="dvMoneyReceived" runat="server" style="width: 100%; height: 300px; overflow: auto;
                                                background-color: White; padding-top: 15px; text-align: center;">
                                                No records found.
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:GridView ID="gvwInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                        BorderStyle="None" BorderWidth="0" Width="100%" 
                                        onrowdatabound="gvwInvoice_RowDataBound">
                                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                        <EmptyDataTemplate>
                                            No Record(s) Found</EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="7%" />
                                                <HeaderTemplate>
                                                    Invoice Type</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("InvoiceType")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="7%" />
                                                <HeaderTemplate>
                                                    Invoice No.</HeaderTemplate>
                                                <ItemTemplate>
                                                     <a id="aInvoice" runat="server" href='<%# "ManageInvoice.aspx?invid=" + EMS.Utilities.GeneralFunctions.EncryptQueryString(Eval("InvoiceID").ToString()) %>'>
                                                        <%# Eval("InvoiceNo")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="6%" />
                                                <HeaderTemplate>
                                                    Invoice Date</HeaderTemplate>
                                                <ItemTemplate>
                                                     <%# Eval("InvoiceDate")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                <HeaderTemplate>
                                                    Invoice Amount</HeaderTemplate>
                                                <ItemTemplate>
                                                     <%# Eval("Ammount")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                <HeaderTemplate>
                                                    Received Amount</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%-- <asp:HiddenField ID="hdnInvID" runat="server" Value='<%# Eval("InvoiceID")%>' />
                                                    <a href="#" runat="server" onserverclick="ShowReceivedAmt">--%>
                                                        <%# Eval("ReceivedAmt")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                <HeaderTemplate>
                                                    CRN Amount</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%--  <a id="A1" href="#" runat="server" onserverclick="ShowCreditNoteAmt">--%>
                                                        <%# Eval("CNAmt")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader_center" />
                                                <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    Print</HeaderTemplate>
                                                <ItemTemplate>
                                                    <a id="aPrint" runat="server" style="cursor: pointer;">
                                                        <img src="../Images/Print.png" /></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader_center" />
                                                <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    Add Money Recpt.</HeaderTemplate>
                                                <ItemTemplate>
                                                    <a id="aMoneyRecpt" runat="server">
                                                        <img alt="Add" src="../Images/ADD.JPG" /></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader_center" />
                                                <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    Add Credit Note</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%-- <a href="#">
                                                        <img alt="Add" src="../Images/ADD.JPG" /></a>--%>
                                                    <a id="aAddCrdtNote" runat="server">
                                                        <img alt="Add" src="../Images/ADD.JPG" /></a>
                                                    <%--style='<%# Convert.ToDecimal(Eval("ReceivedAmt")) < Convert.ToDecimal(Eval("Ammount")) ? "display:block;": "display:none;" %>'>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader_center" />
                                                <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    Invoice Status</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%-- <a href="#">
                                                        <img alt="Add" src="../Images/ADD.JPG" /></a>--%>
                                                    <a id="dStatus" runat="server">
                                                        <img alt="Add" src="../Images/status.JPG" /></a>
                                                    <%--style='<%# Convert.ToDecimal(Eval("ReceivedAmt")) < Convert.ToDecimal(Eval("Ammount")) ? "display:block;": "display:none;" %>'>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </ContentTemplate>
                <Triggers>
                    <%-- <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="txtBlNo" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="lnkDO" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnUpload" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </center>
    </div>
</asp:Content>
