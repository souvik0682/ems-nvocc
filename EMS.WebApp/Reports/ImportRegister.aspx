<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ImportRegister.aspx.cs" Inherits="EMS.WebApp.Reports.ImportRegister" Title=":: EMS :: Import Register" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
    <script type="text/javascript" language="javascript">
        function validateData() {
            var ddlLoc = document.getElementById('<%=ddlLoc.ClientID %>');
            var ddlLine = document.getElementById('<%=ddlLine.ClientID %>');

            if (ddlLoc.options[ddlLoc.selectedIndex].value == '0') {
                alert('Please select location');
                return false;
            }

            if (ddlLine.options[ddlLine.selectedIndex].value == '0') {
                alert('Please select line');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
<center>
    <div style="padding-top: 10px;">
        <fieldset style="width:964px;height:65px;">
            <table>
                <tr>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Location:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:DropDownList ID="ddlLoc" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Line / NVOCC:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:DropDownList ID="ddlLine" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Report Type
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="1" Text="Only B/L"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Only Container"></asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                </tr>
                <tr>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Voyage:
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:TextBox runat="server" ID="txtVoyage" Width="200" autocomplete="off" />
                        <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx1" ID="aceVoyage"
                            TargetControlID="txtVoyage" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                            ShowOnlyCurrentWordInCompletionListItem="true">
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
                                                var behavior = $find('AutoCompleteEx1');
                                                if (!behavior._height) {
                                                    var target = behavior.get_completionList();
                                                    behavior._height = target.offsetHeight - 2;
                                                    target.style.height = '0px';
                                                }" />
                            
                                            <%-- Expand from 0px to the appropriate size while fading in --%>
                                            <Parallel Duration=".4">
                                                <FadeIn />
                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx1')._height" />
                                            </Parallel>
                                        </Sequence>
                                    </OnShow>
                                    <OnHide>
                                        <%-- Collapse down to 0px and fade out --%>
                                        <Parallel Duration=".4">
                                            <FadeOut />
                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx1')._height" EndValue="0" />
                                        </Parallel>
                                    </OnHide>
                            </Animations>
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Vessel
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:TextBox runat="server" ID="txtVessel" Width="200" autocomplete="off" />
                        <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx2" ID="aceVessel"
                            TargetControlID="txtVessel" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                            ShowOnlyCurrentWordInCompletionListItem="true">
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
                    </td>
                    <td colspan="2" style="vertical-align:top;"><asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" OnClientClick="javascript:return validateData();" OnClick="btnShow_Click" /></td>
                </tr>
            </table>
        </fieldset>
        <div style="padding-left:5px;width:980px;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%"></rsweb:ReportViewer>        
        </div>    
    </div>
</center>
</asp:Content>