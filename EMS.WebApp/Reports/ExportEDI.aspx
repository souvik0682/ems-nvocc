<%@ Page Title=":: Liner :: Export EDI" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExportEDI.aspx.cs" Inherits="EMS.WebApp.Reports.ExportEDI" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function validateData() {return true;
            var ddlLoc = document.getElementById('<%=ddlLoc.ClientID %>');
            var ddlVoyage = document.getElementById('<%=ddlVoyage.ClientID %>');
            var ddlPort = document.getElementById('<%=ddlPort.ClientID %>');

            if (ddlLoc.options[ddlLoc.selectedIndex].value == '0') {
                alert('Please select location');
                return false;
            }

            if (ddlPort.options[ddlPort.selectedIndex].value == '0') {
                alert('Please select port');
                return false;
            }

            if (document.getElementById('<%=txtVessel.ClientID %>').value == '') {
                alert('Please select vessel');
                return false;
            }

            if (ddlVoyage.options[ddlVoyage.selectedIndex].value == '0') {
                alert('Please select voyage');
                return false;
            }

            return true;
        }

        function getSelectedVesselId(source, eventArgs) {
            var hdnVessel = document.getElementById('<%=hdnVessel.ClientID %>');
            var selVal = eventArgs.get_value();

            hdnVessel.value = '';

            if (selVal != '')
                hdnVessel.value = selVal;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <asp:HiddenField ID="hdnVessel" runat="server" />
    <asp:HiddenField ID="hdnVoyage" runat="server" />
    <div id="headercaption">EXPORT EDI</div>
    <div style="padding-top: 10px;">
        <fieldset style="width:964px;height:65px;">
            <table>
                <tr>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Vessel:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:TextBox runat="server" ID="txtVessel" MaxLength="100" Width="200" 
                            autocomplete="off" AutoPostBack="True" ontextchanged="txtVessel_TextChanged" />
                        <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx2" ID="aceVessel"
                            TargetControlID="txtVessel" ServicePath="~/GetLocation.asmx" ServiceMethod="GetVesselList"
                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="getSelectedVesselId"
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
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Voyage:<span class="errormessage">*</span>
                    </td>
                    <td style="vertical-align:top;">
                        <asp:DropDownList ID="ddlVoyage" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Port of Loading:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:DropDownList ID="ddlPort" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="label" style="padding-right:5px;vertical-align:top;">
                        Location:<span class="errormessage">*</span>
                    </td>
                    <td style="padding-right:20px;vertical-align:top;">
                        <asp:DropDownList ID="ddlLoc" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="vertical-align:top;"><asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" OnClientClick="javascript:return validateData();" OnClick="btnShow_Click" /></td>
                </tr>
            </table>
        </fieldset>
        <div style="padding-left:5px;width:980px;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%"></rsweb:ReportViewer>        
        </div> 
    </div>
</asp:Content>
