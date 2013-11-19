<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Export_Monthly_Report_Excel.aspx.cs" Inherits="EMS.WebApp.Transaction.Export_Monthly_Report_Brokerage_Excel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">

<script type="text/javascript">
    function AutoCompleteItemSelected(sender, e) {
        if (sender._id == "AutoCompleteEx") {
            var hdnVessel = $get('<%=hdnVessel.ClientID %>');
            hdnVessel.value = e.get_value();
        }

    }
</script>
  <div id="headercaption">
        EXPORT MONTHLY REPORTS</div>
<center>
    <fieldset style="width: 964px; height: 85x;">
        <table>
       
            <tr>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Report:<span class="errormessage">*</span></td>
               <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlReport" runat="server" AutoPostBack="True" Width="135px">
<%--                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>--%>
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="rqfReport" runat="server" ErrorMessage="Please select Report"
                        ControlToValidate="ddlReport"  InitialValue="0"  ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceReport" runat="server" TargetControlID="rqfReport"
                        WarningIconImageUrl="">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    &nbsp;</td>
                <td style="padding-right: 20px; vertical-align: top;">
                    &nbsp;</td>               
            </tr>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
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
                    Line / NVOCC:<span class="errormessage">*</span>
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
            <tr>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Vessel :
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:HiddenField ID="hdnVessel" runat="server" />
                    <asp:TextBox ID="txtVessel" runat="server" CssClass="textbox" Width="180" autocomplete="off"
                        AutoPostBack="True" ontextchanged="txtVessel_TextChanged">
                    </asp:TextBox>
             <%--       <asp:RequiredFieldValidator ID="rqfVessel" runat="server" 
                        ErrorMessage="Please Enter Vessel" ControlToValidate="txtVessel" 
                        ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceVessel" runat="server" TargetControlID="rqfVessel">
                    </cc1:ValidatorCalloutExtender>--%>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEx"
                        TargetControlID="txtVessel" ServicePath="~/GetLocation.asmx" ServiceMethod="GetVesselList"
                        MinimumPrefixLength="1" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
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

                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Voyage :
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                        <asp:DropDownList ID="ddlVoyage" runat="server">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                </td>
<%--                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:HiddenField ID="hdnVoyage" runat="server" />
                    <asp:TextBox ID="txtVoyage" runat="server" CssClass="textbox" Width="180" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqfVoyage" runat="server" 
                        ErrorMessage="Please Enter Voyage" ControlToValidate="txtVoyage" 
                        ForeColor="#CC3300"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rqfVoyage">
                    </cc1:ValidatorCalloutExtender>
                    <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx2" ID="autoComplete2"
                        TargetControlID="txtVoyage" ServicePath="~/GetLocation.asmx" ServiceMethod="GetVoyageList"
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
                </td>--%>

            </tr>
            <tr id="DateRange" runat="server">
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    <asp:Label ID="lblStartDt" runat="server" Text="Start Date"></asp:Label>
                    &nbsp;
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:TextBox ID="txtStartDt" runat="server" CssClass="textbox" Width="180"  AutoPostBack="True"></asp:TextBox>
                    <cc1:CalendarExtender ID="cbeFromDt" runat="server" TargetControlID="txtStartDt"
                        Format="dd/MM/yyyy" />
                               <cc1:MaskedEditExtender ID="msk1" runat="server" UserDateFormat="MonthDayYear" Mask="99/99/9999"
                        TargetControlID="txtStartDt" ClearMaskOnLostFocus="False">
                    </cc1:MaskedEditExtender>
                  
                </td>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    <asp:Label ID="lblEndDt" runat="server" Text="End Date"></asp:Label>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:TextBox ID="txtEndDt" runat="server" CssClass="textbox" Width="180"></asp:TextBox>
                    <cc1:CalendarExtender ID="cbeToDt" runat="server" TargetControlID="txtEndDt" Format="dd/MM/yyyy" />                   
                    <cc1:MaskedEditExtender ID="msk2" runat="server" UserDateFormat="MonthDayYear" Mask="99/99/9999"
                        TargetControlID="txtEndDt" ClearMaskOnLostFocus="False">
                    </cc1:MaskedEditExtender>
                </td>
            </tr>
         
            <tr>
                <td style="vertical-align: top;">
                    <asp:Button ID="btnShow" runat="server" Text="Generate Excel" CssClass="button" OnClientClick="javascript:return
                    ();" OnClick="btnShow_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</center>
</asp:Content>
