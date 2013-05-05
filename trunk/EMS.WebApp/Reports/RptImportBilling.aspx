<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RptImportBilling.aspx.cs" Inherits="EMS.WebApp.Reports.RptImportBilling" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="../CustomControls/AutoCompletepInvoice.ascx" TagName="AutoCompletepInvoice"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <center>
        <asp:UpdateProgress ID="uProgressLoc" runat="server" AssociatedUpdatePanelID="upLoc">
            <ProgressTemplate>
                <div class="progress">
                    <div id="image">
                        <img src="../../Images/PleaseWait.gif" alt="" /></div>
                    <div id="text">
                        Please Wait...</div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div id="headercaption">
            IMPORT BILLING
        </div>
        <center>
            <fieldset style="width: 800px;">
                <legend>Import Billing Statement </legend>
                <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <%-- <asp:AsyncPostBackTrigger ControlID="ddlVoyage" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVessel" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLoc" EventName="SelectedIndexChanged" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td style="width: 12%;">
                                    Location :
                                </td>
                                <td style="width: 10%">
                                    <asp:DropDownList ID="ddlLocation" runat="server" Width="70" AutoPostBack="True"
                                        OnSelectedIndexChanged="LocationLine_Changed">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 8%;">
                                    Line :
                                </td>
                                <td style="width: 10%;">
                                    <asp:DropDownList ID="ddlLine" runat="server" Width="100" AutoPostBack="True" OnSelectedIndexChanged="LocationLine_Changed"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 12%;">
                                    B/L No :
                                </td>
                                <td style="width: 10%;">
                                    <asp:HiddenField ID="hdnBLId" runat="server" Value="0" />
                                    <asp:TextBox ID="txtBlNo" runat="server" Width="150" AutoPostBack="True" onkeyup="SetContextKey();"
                                        Enabled="false"></asp:TextBox>
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
                                <td style="text-align: left; width: 12%">
                                    Date Till:
                                </td>
                                <td style="text-align: left; width: 10%">
                                    <asp:TextBox ID="txtdtBill" runat="server" CssClass="textboxuppercase" Width="100"></asp:TextBox>
                                    <cc2:CalendarExtender ID="dtBill_" Format="dd/MM/yyyy" TargetControlID="txtdtBill"
                                        runat="server" />
                                </td>
                                <td style="text-align: right; width: 12%; vertical-align: middle">
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Show" ValidationGroup="Save" />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                            <td colspan="9">
                                <asp:RadioButtonList ID="rbRpt" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Text="Import Billing" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Import Billing Annexture" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </center>
        &nbsp;<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <div style="padding-top: 10px;">
            <div style="padding-left: 5px; width: 98%; visibility: visible">
                <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt">
                    <%--<LocalReport ReportPath="RDLC\IGMForm2.rdlc">
                </LocalReport>--%>
                </rsweb:ReportViewer>
            </div>
        </div>
    </center>
</asp:Content>
