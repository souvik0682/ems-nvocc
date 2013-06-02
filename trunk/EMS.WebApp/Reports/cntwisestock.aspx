<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="cntwisestock.aspx.cs" Inherits="EMS.WebApp.Reports.cntwisestock" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {

            if (sender._id == "AutoCompleteEx") {
                var hdnContId = $get('<%=hdnContId.ClientID %>');
                hdnContId.value = e.get_value();
            }

        }

        function clear() {
            if (document.getElementById('<%= txtContainerNo.ClientID %>').value == "") {
                var dd = document.getElementById('<%= ddlLine.ClientID %>');
                dd.selectedIndex = 0;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
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
    <center>
        <div id="headercaption">
            CONTAINER WISE STOCK SUMMERY
        </div>
        <center>
            <fieldset style="width: 700px;">
                <legend>Container Wise Stock Details </legend>
                <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <%-- <asp:AsyncPostBackTrigger ControlID="ddlVoyage" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVessel" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLoc" EventName="SelectedIndexChanged" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td style="width: 40%">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right">
                                                Location:<%--<span class="errormessage1">*</span>--%></td>
                                            <td>
                                                <asp:DropDownList ID="ddlLoc" runat="server" Width="100%" OnSelectedIndexChanged="LocationLine_Changed"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" 
                            ControlToValidate="ddlLoc" Display="Dynamic" InitialValue="0" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td style="text-align: right">
                                                Line:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLine" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="LocationLine_Changed">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right">
                                                Container No:
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnContId" runat="server" Value="0" />
                                                <asp:TextBox ID="txtContainerNo" runat="server" Width="160" AutoPostBack="false"
                                                    Style="text-transform: uppercase;" onchange="clear();"></asp:TextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" MaxLength="11"
                                                    ControlToValidate="txtContainerNo" Display="Dynamic" Text="Please enter container no."
                                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                <cc2:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
                                                    TargetControlID="txtContainerNo" ServicePath="~/GetLocation.asmx" ServiceMethod="GetContainerListByLocLine"
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
                                                </cc2:AutoCompleteExtender>
                                            </td>
                                    </table>
                                </td>
                                <td colspan="2" style="text-align: right; width: 10%">
                                    <asp:Button ID="btnSave" runat="server" Text="Show" ValidationGroup="Save" OnClick="btnSave_Click" />
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>

 </fieldset>
        </center>
        <center>
                <div style="padding-top: 10px;">
                    <div style="padding-left: 5px; width: 67%;">
                        <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt">
                            <%--<LocalReport ReportPath="RDLC\IGMForm2.rdlc">
                </LocalReport>--%>
                        </rsweb:ReportViewer>
                    </div>
                </div>
           </center>
    </center>
</asp:Content>
