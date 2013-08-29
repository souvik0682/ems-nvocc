<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditAgent.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="EMS.WebApp.Export.AddEditAgent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {
            var hdnFPOD = $get('<%=hdnFPOD.ClientID %>');
            hdnFPOD.value = e.get_value();
        }
    </script>

    <style type="text/css">
        .style1
        {
            width: 474px;
        }
        .style2
        {
            width: 503px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            ADD / EDIT AGENT</div>
        <center>
            <fieldset style="width: 400px;">
                <legend>Add / Edit Agent</legend>
                <table border="0" cellpadding="2" cellspacing="3">
                    
                    <tr>
                        <td class="style1">
                            Agent Name<span class="errormessage1">*</span> :
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtAgent" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Line<span class="errormessage1">*</span> :
                        </td>
                        <td class="style2">
                            <%--<asp:HiddenField ID="hdnLine" runat="server" />--%>
                            <asp:DropDownList ID="ddlLine" runat="server" Width="255">
                            </asp:DropDownList>
                            <%-- Make the completion list transparent and then show it --%>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Final POD<span class="errormessage1">*</span> :
                        </td>
                        <td class="style2">
                            <asp:HiddenField ID="hdnFPOD" runat="server" />
                            <asp:TextBox runat="server" ID="txtFPOD" Width="250" autocomplete="off" Style="text-transform: uppercase;" />
                            <asp:RequiredFieldValidator ID="rfvFPOD" runat="server" ErrorMessage="Please select location"
                                Display="None" ControlToValidate="txtFPOD" ValidationGroup="vgAgent"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvFPOD"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
                                TargetControlID="txtFPOD" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
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
                    </tr>
                    
                 
                    <tr>
                        <td class="style1">
                            Contact Person:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtContPerson" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Address:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxuppercase" MaxLength="300" Width="250" TextMode="MultiLine"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Phone:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>

                    <tr>
                        <td class="style1">
                            Fax:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtFax" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>

                    <tr>
                        <td class="style1">
                            Email:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td class="style2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnAgentID" runat="server" Value="0" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="vgHaulage" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClick="btnBack_Click" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
</asp:Content>
