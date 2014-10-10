<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageSettlement.aspx.cs" Inherits="EMS.WebApp.Transaction.ManageSettlement" %>

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
   
    <style type="text/css">
        .style5
        {
            width: 11%;
        }
        .style7
        {
            width: 5%;
        }
        .style9
        {
            width: 13%;
        }
        .style10
        {
            width: 14%;
        }
        .style13
        {
            width: 9%;
        }
        .style15
        {
            width: 1%;
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

    <div>
        <div id="headercaption">
            Settlement Add / Edit</div>
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset style="width: 98%; display: block;">
                        <legend>B/L Detail</legend>
                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                                <td class="style5">
                                    B/L No :
                                </td>
                                <td class="style10">
                                    <asp:HiddenField ID="hdnBLId" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnSettlementID" runat="server" Value="0" />
                                    <asp:TextBox ID="txtBlNo" runat="server" Width="150" AutoPostBack="True" OnTextChanged="txtBlNo_TextChanged"
                                        onkeyup="SetContextKey();" Enabled="true" Style="text-transform: uppercase;"></asp:TextBox>
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
                                <td class="style5">
                                    Location :
                                </td>
                                <td class="style9">
                                    <asp:TextBox ID="txtLocation" runat="server" Width="148px" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="style13">
                                    Transaction Type :
                                </td>
                                <td class="style15">
                                    <asp:TextBox ID="txtTransactionType" runat="server" Width="137px" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 5%;">
                                    Outstanding :
                                </td>
                                <td style="width: 15%;">
                                    <asp:TextBox ID="txtOutstanding" runat="server" Width="129px" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    B/L Date</td>
                                <td class="style10">
                                    <asp:TextBox ID="txtBlDate" runat="server" Width="148px" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="style5">
                                    Line :
                                </td>
                                <td class="style9">
                                    <asp:TextBox ID="txtLine" runat="server" Width="150" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="style13">
                                    Settlement Date :
                                </td>
                                <td class="style15">
                                    <asp:TextBox ID="txtSettlementDate" runat="server" Width="135px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="cbeInvoiceDate" TargetControlID="txtSettlementDate" runat="server"
                                        Format="dd-MM-yyyy" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="rfvSettlementDate" runat="server" ControlToValidate="txtSettlementDate"
                                        ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 11%;">
                                    <asp:Label ID="lblPaymentRcpt" runat="server" Text="Rcvble Amount"></asp:Label>
                                </td>

                                <td style="width: 10%;">
                                    <cc2:CustomTextBox ID="txtSettlementAmount" runat="server" Width="128px" Type="Decimal"
                                        MaxLength="12" Precision="9" Scale="2" Style="text-align: right;" 
                                        Text="0.00"></cc2:CustomTextBox>
                                </td>
                            </tr>
                            
                            <tr style="height: 50px;">

                                <td class="style5">
                                   <asp:Label ID="lblPayToRcvdFrom" runat="server" Text="Pay To/Rcvd From"></asp:Label>
                                </td>
                                <td class="style10">
                                    <asp:TextBox ID="txtPayToRcvdFrom" runat="server" Width="150" Enabled="true" CssClass="textboxuppercase" ></asp:TextBox>
                                </td>
                                <td class="style5">
                                   Cheque Detail
                                </td>
                                <td class="style9">
                                    <asp:TextBox ID="txtChequeDetail" runat="server" Width="150" Enabled="true" CssClass="textboxuppercase" ></asp:TextBox>
                                </td>
                                <td class="style13">
                                    Bank Name 
                                </td>
                                <td class="style15">
                                    <asp:TextBox ID="txtBankName" runat="server" Width="148px" Enabled="true" CssClass="textboxuppercase" ></asp:TextBox>
                                </td>
                                <td style="width: 11%;">
                                    Settlement No. </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtSettlementNo" runat="server" Width="148px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <%-- <asp:HiddenField ID="hdnBlQueryID" runat="server" Value="0" />--%>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgCharge" 
                                        onclick="btnSave_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                        OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" 
                                        onclick="btnBack_Click"/>
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
                                <td>
                                   <%-- <asp:Button ID="btnTemp11" runat="server" Style="display: none;" />
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
                                    </asp:Panel>--%>
                                    <asp:GridView ID="gvwInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                        BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvwInvoice_RowDataBound">
                                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                        <EmptyDataTemplate>
                                            No Record(s) Found</EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                <HeaderTemplate>
                                                    Document Type</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("DocType")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                <HeaderTemplate>
                                                    Document No.</HeaderTemplate>
                                                <ItemTemplate>
                                                        <%# Eval("DocNo")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                <HeaderTemplate>
                                                    Document Date</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("DocDate")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Right" />
                                                <HeaderTemplate>
                                                    Invoice Amount</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("CrAmount")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Right" />
                                                <HeaderTemplate>
                                                    Received Amount</HeaderTemplate>
                                                <ItemTemplate>
                                                        <%# Eval("DrAmount")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                <HeaderTemplate>
                                                    CRN Amount</HeaderTemplate>
                                                <ItemTemplate>
                                                        <%# Eval("CNAmt")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </ContentTemplate>
                <Triggers>
   <%--                 <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />--%>
                    <asp:AsyncPostBackTrigger ControlID="txtBlNo" EventName="TextChanged" />
<%--                    <asp:AsyncPostBackTrigger ControlID="lnkDO" EventName="Click" />--%>
<%--                    <asp:PostBackTrigger ControlID="btnUpload" />--%>
                      
                </Triggers>
            </asp:UpdatePanel>
        </center>
    </div>
</asp:Content>
