<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageVoyage.aspx.cs" Inherits="EMS.WebApp.Export.ManageVoyage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {

            if (sender._id == "AutoCompleteEx3") {
                var hdnPOL = $get('<%=hdnPOL.ClientID %>');
                hdnPOL.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteEx4") {
                var hdnNextPortCall = $get('<%=hdnNextPortCall.ClientID %>');
                hdnNextPortCall.value = e.get_value();
            }
    }
    
    </script>

    <style type="text/css">
        .style2
        {
            width: 503px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">

    <div id="headercaption">
        ADD/EDIT VOYAGE
    </div>
    <center>
        <fieldset style="width: 60%;">
            <legend>Add / Edit Invoice</legend>
            <table border="0" cellpadding="2" cellspacing="3">
            <%--<asp:UpdatePanel ID="upInvoice" runat="server" UpdateMode="Always">--%>
<%--
                <ContentTemplate>--%>
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Vessel Name<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Voyage No<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVoyageNo" runat="server" CssClass="textboxuppercase" MaxLength="50" 
                                    Width="250px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvVoyageNo" runat="server" ControlToValidate="txtVoyageNo"
                                    ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                    
                        </tr>
                        <tr>
                            <td>
                                POL<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnPOL" runat="server" />
                                <asp:TextBox ID="txtPOL" runat="server" Width="250px" Style="text-transform: uppercase;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPOL" runat="server" ControlToValidate="txtPOL"
                                    ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvPOL"
                                    WarningIconImageUrl="">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx3" ID="AutoComplete3"
                                    TargetControlID="txtPOL" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                                    ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected">
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
                                                        var behavior = $find('AutoCompleteEx3');
                                                        if (!behavior._height) {
                                                            var target = behavior.get_completionList();
                                                            behavior._height = target.offsetHeight - 2;
                                                            target.style.height = '0px';
                                                        }" />
                            
                                                    <%-- Expand from 0px to the appropriate size while fading in --%>
                                                    <Parallel Duration=".4">
                                                        <FadeIn />
                                                        <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx3')._height" />
                                                    </Parallel>
                                                </Sequence>
                                            </OnShow>
                                            <OnHide>
                                                <%-- Collapse down to 0px and fade out --%>
                                                <Parallel Duration=".4">
                                                    <FadeOut />
                                                    <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx3')._height" EndValue="0" />
                                                </Parallel>
                                            </OnHide>
                                    </Animations>
                                </cc1:AutoCompleteExtender>
                            </td>
       
                        </tr>
                        <tr>
                            <td>
                                ETA POL<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtETA" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtETA" runat="server" Format="dd-MM-yyyy"
                                    Enabled="True" />
                                <asp:RequiredFieldValidator ID="rfvETA" runat="server" ControlToValidate="txtETA"
                                        ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
  
                        </tr>
                        <tr>
                            <td>
                                ETD POL<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtETD" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                                <cc1:CalendarExtender ID="cbeETD" TargetControlID="txtETD" runat="server" Format="dd-MM-yyyy"
                                    Enabled="True" />
                                <asp:RequiredFieldValidator ID="rfvETD" runat="server" ControlToValidate="txtETD"
                                        ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
  
                        </tr>
                        <tr>
                            <td>
                                Terminal ID
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTerminalID" runat="server">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
       
                        </tr>
                        <tr>
                            <td>
                                Next port call
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnNextPortCall" runat="server" />
                                <asp:TextBox ID="txtNextportcall" runat="server" Width="250px" Style="text-transform: uppercase;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNextPortID" runat="server" ControlToValidate="txtNextportcall"
                                    ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfvNextPortID"
                                    WarningIconImageUrl="">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx4" ID="AutoCompleteExtender1"
                                    TargetControlID="txtNextportcall" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                                    ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected">
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
                                                        var behavior = $find('AutoCompleteEx4');
                                                        if (!behavior._height) {
                                                            var target = behavior.get_completionList();
                                                            behavior._height = target.offsetHeight - 2;
                                                            target.style.height = '0px';
                                                        }" />
                            
                                                    <%-- Expand from 0px to the appropriate size while fading in --%>
                                                    <Parallel Duration=".4">
                                                        <FadeIn />
                                                        <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx4')._height" />
                                                    </Parallel>
                                                </Sequence>
                                            </OnShow>
                                            <OnHide>
                                                <%-- Collapse down to 0px and fade out --%>
                                                <Parallel Duration=".4">
                                                    <FadeOut />
                                                    <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx4')._height" EndValue="0" />
                                                </Parallel>
                                            </OnHide>
                                    </Animations>
                                </cc1:AutoCompleteExtender>
                            </td>
        
                        </tr>

                        <tr>
                            <td>
                                ETA Next Port<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtETANextPort" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                                <cc1:CalendarExtender ID="cbeETANextPort" TargetControlID="txtETANextPort" runat="server" Format="dd-MM-yyyy"
                                    Enabled="True" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtETANextPort"
                                        ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
  
                        </tr>
                        <tr>
                            <td style="width: 140px;">
                                <asp:Label ID="lblSailingDate" runat="server" Text="Sailing Date/Time"></asp:Label>
                                <%--Sailing Date &amp; time--%>
                            </td>
                            <td >
                                <asp:TextBox ID="txtSailingDateTime" runat="server" Width="250px"></asp:TextBox>
                                <cc1:CalendarExtender ID="cbeSailingDateTime" TargetControlID="txtSailingDateTime"
                                    runat="server" Format="dd-MM-yyyy HH':'mm':'ss" Enabled="True" />
                            </td>
                        </tr>
                       
                       
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 140px;">
                                Line code
                            </td>
                            <td>
                                <asp:TextBox ID="txtLinecode" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 140px;">
                                Agent code
                            </td>
                            <td>
                                <asp:TextBox ID="txtAgentcode" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                VIA
                            </td>
                            <td>
                                <asp:TextBox ID="txtVIA" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                            </td>
           
                        </tr>
                        
                        <tr>
                            <td>
                                Rotation No
                            </td>
                            <td>
                                <asp:TextBox ID="txtRotationNo" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                            </td>
   
                        </tr>
                        <tr>
                            <td>
                                Rotation Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtRotationDate" runat="server" Width="250px"></asp:TextBox>
                                <cc1:CalendarExtender ID="cbeRotationDate" TargetControlID="txtRotationDate" runat="server"
                                    Format="dd-MM-yyyy" Enabled="True" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
               
                        <tr>
                            <td>
                                Vessel Cut off Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtVesselcutoffDate" runat="server" Width="250px"></asp:TextBox>
                                <cc1:CalendarExtender ID="cbeVesselcutoffDate" TargetControlID="txtVesselcutoffDate"
                                    runat="server" Format="dd-MM-yyyy HH':'mm':'ss" Enabled="True" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Docks cut off Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtlstsiDockscutoffDate" runat="server" Width="250px"></asp:TextBox>
                                <cc1:CalendarExtender ID="cbelstsiDockscutoffDate" TargetControlID="txtlstsiDockscutoffDate"
                                    runat="server" Format="dd-MM-yyyy HH':'mm':'ss" Enabled="True" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 140px;">
                                PCC No
                            </td>
                            <td>
                                <asp:TextBox ID="txtPCCNo" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 140px;">
                                PCC Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtPCCDate" runat="server" Width="250px"></asp:TextBox>
                                <cc1:CalendarExtender ID="cbePCCDate" TargetControlID="txtPCCDate" runat="server"
                                    Format="dd-MM-yyyy" Enabled="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 140px;">
                                VCN No
                            </td>
                            <td>
                                <asp:TextBox ID="txtVCNNo" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                            </td>
                        </tr>
                       
                    </table>
                </td>
            </tr>    
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSave" OnClick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnBack_Click"
                        OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                    <br />
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
         </table>
            <%--    </ContentTemplate>--%>
 <%--           </asp:UpdatePanel>--%>
 <%--               </tr>
            </table>--%>
        </fieldset>
    </center>

            </table>

</asp:Content>
