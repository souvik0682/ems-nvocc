<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExpCntrStatus.aspx.cs" Inherits="EMS.WebApp.Reports.Export.ExpCntrStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">

        function validateData() {

            if (document.getElementById('<%=txtBookingNo.ClientID %>').value == '') {
                alert('Please enter Booking No.');
                return false;
            }
            return true;
        }

//        function AutoCompleteItemSelected(sender, e) {
//            if (sender._id == "AutoCompleteBookingNo") {
//                var hdnBookingNo = $get('<%=hdnBookingNo.ClientID %>');
//                hdnBookingNo.value = e.get_value();
//            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        EXPORT CONTAINER REPORT
    </div>
    <center>
        <fieldset style="width: 964px; height: 95px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="label" style="padding-right: 50px; vertical-align: top;">
                                Booking No.:<span class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnBookingNo" runat="server" />
                                <asp:TextBox ID="txtBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="60" AutoPostBack="true" OnTextChanged="txtBookingNo_TextChanged"
                                    Width="250px" TabIndex="4"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvAccounts" runat="server" ControlToValidate="txtBookingNo"
                                    ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
 <%--                           <td style="padding-right: 20px; vertical-align: top;">
                                <asp:HiddenField ID="hdnBookingNo" runat="server" />
                                <asp:TextBox runat="server" ID="txtBookingNo" Width="250" autocomplete="off" AutoPostBack="True"
                                    MaxLength="50" TabIndex="9" Style="text-transform: uppercase;" OnTextChanged="txtBookingNo_TextChanged" />
                                <br />
                                <asp:RequiredFieldValidator ID="rfvBookingNo" runat="server" ControlToValidate="txtBookingNo"
                                    CssClass="errormessage" ValidationGroup="Export" ErrorMessage="Please select Booking No."
                                    Display="None"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceBookingNo" runat="server" TargetControlID="rfvBookingNo"
                                    WarningIconImageUrl="">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteBookingNo" ID="autoComplete1"
                                    TargetControlID="txtBookingNo" ServicePath="~/GetLocation.asmx" ServiceMethod="GetBooking"
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
                                            var behavior = $find('AutoCompleteBookingNo');
                                            if (!behavior._height) {
                                                var target = behavior.GetBooking();
                                                behavior._height = target.offsetHeight - 2;
                                                target.style.height = '0px';
                                            }" />
                            
                                     
                                        <Parallel Duration=".4">
                                            <FadeIn />
                                            <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteBookingNo')._height" />
                                        </Parallel>
                                    </Sequence>
                                </OnShow>
                                <OnHide>
                                    
                                    <Parallel Duration=".4">
                                        <FadeOut />
                                        <Length PropertyKey="height" StartValueScript="$find('AutoCompleteBookingNo')._height" EndValue="0" />
                                    </Parallel>
                                </OnHide>
                                    </Animations>
                                </cc1:AutoCompleteExtender>
                            </td>--%>
                            <td class="label" style="padding-right: 50px; vertical-align: top;">
                                Booking Party
                            </td>
                            <td style="padding-right: 20px; vertical-align: top;">
                                <asp:Label ID="lblBookingParty" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="padding-right: 50px; vertical-align: top;">
                                Vessel
                            </td>
                            <td style="padding-right: 20px; vertical-align: top;">
                                <asp:Label ID="lblVessel" runat="server"></asp:Label>
                            </td>
                            <td class="label" style="padding-right: 50px; vertical-align: top;">
                                POD
                            </td>
                            <td style="padding-right: 20px; vertical-align: top;">
                                <asp:Label ID="lblPOD" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="padding-right: 50px; vertical-align: top;">
                                Voyage
                            </td>
                            <td style="padding-right: 20px; vertical-align: top;">
                                <asp:Label ID="lblVoyage" runat="server"></asp:Label>
                            </td>
                            <td class="label" style="padding-right: 50px; vertical-align: top;">
                                FPOD
                            </td>
                            <td style="padding-right: 20px; vertical-align: top;">
                                <asp:Label ID="lblFPOD" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <center>
                <asp:Button ID="btnShow" runat="server" Text="Generate Excel" CssClass="button" OnClick="btnShow_Click" />
            </center>
        </fieldset>
    </center>
</asp:Content>
