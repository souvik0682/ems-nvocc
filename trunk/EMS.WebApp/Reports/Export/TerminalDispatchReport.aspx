<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TerminalDispatchReport.aspx.cs" Inherits="EMS.WebApp.Reports.Export.TerminalDispatchReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            color: #000000;
            width: 137px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {
            if (sender._id == "AutoCompleteEx") {
                var hdnVessel = $get('<%=hdnVessel.ClientID %>');
                hdnVessel.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteEx2") {
                var hdnPOL = $get('<%=hdnPOL.ClientID %>');
                hdnPOL.value = e.get_value();
            }
        }
    </script>
    <center>
    <fieldset style="width: 964px; height: 15x;">
        <table>
            <tr>
                <td style="padding-right: 50px; vertical-align: top;" >
                    Vessel :<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:HiddenField ID="hdnVessel" runat="server" />
                    <asp:TextBox ID="txtVessel" runat="server" CssClass="textbox" Width="180" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqfVessel" runat="server" 
                        ErrorMessage="Please Enter Vessel" ControlToValidate="txtVessel" 
                        ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceVessel" runat="server" TargetControlID="rqfVessel">
                    </cc1:ValidatorCalloutExtender>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEx"
                        TargetControlID="txtVessel" ServicePath="~/GetLocation.asmx" ServiceMethod="GetVesselList"
                        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                        ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected">
                        <Animations>
                                        <OnShow>
                                            <Sequence>
                                              <OpacityAction Opacity="0" />
                                                <HideAction Visible="true" />                           
                                                <ScriptAction Script="
                                                    // Cache the size and setup the initial size
                                                    var behavior = $find('AutoCompleteEx');
                                                    if (!behavior._height) {
                                                        var target = behavior.get_completionList();
                                                        behavior._height = target.offsetHeight - 2;
                                                        target.style.height = '0px';
                                                    }" />
                                                <Parallel Duration=".4">
                                                    <FadeIn />
                                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                                                </Parallel>
                                            </Sequence>
                                        </OnShow>
                                        <OnHide>                                            
                                            <Parallel Duration=".4">
                                                <FadeOut />
                                                <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                            </Parallel>
                                        </OnHide>
                        </Animations>
                    </cc1:AutoCompleteExtender>
                </td>
                <td class="label" style="padding-right:50px; vertical-align: top;">
                    Line :<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="True" Width="135px">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvLine" runat="server" ErrorMessage="Please select Line"
                       ControlToValidate="ddlLine"
                        InitialValue="0" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvLine"
                        WarningIconImageUrl="">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr id="DateRange" runat="server">
                <td style="padding-right: 50px; vertical-align: top;">
                    Location:<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlLoc" runat="server" AutoPostBack="True" Width="135px">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvLoc" runat="server" ErrorMessage="Please select Location"
                        ControlToValidate="ddlLoc"  InitialValue="0"  ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvLoc"
                        WarningIconImageUrl="">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Pol :<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:HiddenField ID="hdnPOL" runat="server" />
                    <asp:TextBox ID="txtPol" runat="server" CssClass="textbox" Width="180" autocomplete="off">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqfPOL" runat="server" 
                        ErrorMessage="Please enter POL" ControlToValidate="txtPol" ForeColor="#CC3300">
                        </asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcePOL" runat="server" TargetControlID="rqfPOL">
                    </cc1:ValidatorCalloutExtender>

                    <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx2" ID="autoComplete2"
                        TargetControlID="txtPol" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                        ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected">
                        <Animations>
                                        <OnShow>
                                            <Sequence>
                                              
                                                <OpacityAction Opacity="0" />
                                                <HideAction Visible="true" />                                             
                                                <ScriptAction Script="
                                                    // Cache the size and setup the initial size
                                                    var behavior = $find('AutoCompleteEx2');
                                                    if (!behavior._height) {
                                                        var target = behavior.get_completionList();
                                                        behavior._height = target.offsetHeight - 2;
                                                        target.style.height = '0px';
                                                    }" />
                                                <Parallel Duration=".4">
                                                    <FadeIn />
                                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx2')._height" />
                                                </Parallel>
                                            </Sequence>
                                        </OnShow>
                                        <OnHide>
                                            <Parallel Duration=".4">
                                                <FadeOut />
                                                <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx2')._height" EndValue="0" />
                                            </Parallel>
                                        </OnHide>
                        </Animations>
                    </cc1:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right; width: 10%">
                    <asp:Button ID="btnPrint" runat="server" OnClick="btnShow_Click" Text="Generate Excel" ValidationGroup="Print" />
                    &nbsp;&nbsp;
                </td>
<%--                <td style="vertical-align: top;">
                    <asp:Button ID="btnShow" runat="server" Text="Generate Excel" CssClass="button" OnClientClick="javascript:return sure
                    ();" OnClick="btnShow_Click" />
                </td>--%>
            </tr>
        </table>
    </fieldset>
   </center>
</asp:Content>
