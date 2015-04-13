<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExportPerformanceReport.aspx.cs" Inherits="EMS.WebApp.Transaction.ExportPerformanceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript" language="javascript">
   
    function validateData() {

        if (document.getElementById('<%=txtStartDt.ClientID %>').value == '__/__/____') {
            alert('Please enter from date');
            return false;
        }
        if (document.getElementById('<%=txtEndDt.ClientID %>').value == '__/__/____') {
            alert('Please enter to date');
            return false;
        }
        return true;
    }

    function AutoCompleteItemSelected(sender, e) {
        if (sender._id == "AutoCompleteEx") {
            var hdnVessel = $get('<%=hdnVessel.ClientID %>');
            hdnVessel.value = e.get_value();
        }

    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
         EXPORT PERFORMANCE REPORT </div>
    <center>
    <fieldset style="width: 964px; height: 85x;">
        <table>
            <tr>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Location:<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlLoc" runat="server" AutoPostBack="True" Width="135px">
                        <asp:ListItem Value="0" Text="All Locations"></asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Line / NVOCC:<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="True" Width="135px" 
                        onselectedindexchanged="ddlLine_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="--All--"></asp:ListItem>
                    </asp:DropDownList>
                </td>               
           
            </tr>
            <tr>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Services:
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlServices" runat="server" AutoPostBack="True" Width="135px">
                        <asp:ListItem Value="0" Text="All Services"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="label" style="padding-right: 50px; vertical-align: top;">
                    Status:<span class="errormessage">*</span>
                </td>
                <td style="padding-right: 20px; vertical-align: top;">
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="135px" >
                        <asp:ListItem Text="Actual" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Pending Closure" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Booking" Value="B"></asp:ListItem>
                    </asp:DropDownList>
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
                    <asp:Button ID="btnShow" runat="server" Text="Generate Excel" CssClass="button" OnClick="btnShow_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    </center>
</asp:Content>
