<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditRepairEstimate.aspx.cs" Inherits="EMS.WebApp.Equipment.AddEditRepairEstimate" %>

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
    </script>  

    <script type="text/javascript">
        function AutoCompleteItemSelected(sender, e) {
            if (sender._id == "AutoCompleteEx") {

                var textToFind = e.get_value();
                var dd = document.getElementById('<%= ddlLine.ClientID %>');
                for (var i = 0; i < dd.options.length; i++) {                    
                    if (dd.options[i].value == textToFind) {
                        dd.selectedIndex = i;
                        break;
                    }
                }
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
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        ADD / EDIT ESTIMATE</div>
    <center>
        <fieldset style="width: 550px;">
            <legend>Add / Edit Estimate</legend>
            <table border="0" cellpadding="2" cellspacing="3" width="100%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblError" runat="server" Text="" Style="color: Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Location:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLoc" runat="server" Width="60%" AutoPostBack="true" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlLoc" Display="Dynamic" InitialValue="0" Text="This field is Required"
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 140px;">
                        Container No:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <%--<asp:TextBox ID="txtContainerNo" runat="server" CssClass="textboxuppercase" MaxLength="11"
                            Width="160"> </asp:TextBox>--%>
                        <asp:HiddenField ID="hdnContId" runat="server" Value="0" />
                        <asp:TextBox ID="txtContainerNo" runat="server" Width="160" AutoPostBack="false"
                            Style="text-transform: uppercase;" onchange="clear();"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage"
                            MaxLength="11" ControlToValidate="txtContainerNo" Display="Dynamic" Text="Please enter container no."
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <cc2:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
                            TargetControlID="txtContainerNo" ServicePath="~/GetLocation.asmx" ServiceMethod="GetContainerList"
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
                </tr>
                <tr>
                    <td>
                        Line:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLine" runat="server" Width="60%" Enabled="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 140px;">
                        Transaction Date:<span class="errormessage1">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox><br />
                        <cc2:CalendarExtender ID="dtTransDate" TargetControlID="txtTransactionDate" Format="dd/MM/yyyy"
                            runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                            ControlToValidate="txtTransactionDate" Display="Dynamic" Text="This field is Required"
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 140px;">
                        Estimate Reference:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEstimateRef" runat="server" CssClass="textboxuppercase" MaxLength="100"
                            Width="250"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td style="width: 70%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 33%">
                                            </td>
                                            <td style="width: 33%">
                                                Material
                                            </td>
                                            <td style="width: 33%">
                                                Labour
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%; text-align: right; padding-right: 30px">
                                                &nbsp;&nbsp;Estimate
                                            </td>
                                            <td style="width: 33%">
                                                <cc1:CustomTextBox ID="txtMaterialEst" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                    MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                                            </td>
                                            <td style="width: 33%">
                                                <cc1:CustomTextBox ID="txtLabourEst" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                    MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%; text-align: right; padding-right: 30px">
                                                &nbsp;&nbsp;Approved
                                            </td>
                                            <td style="width: 33%">
                                                <cc1:CustomTextBox ID="txtMaterialApp" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                    MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                                            </td>
                                            <td style="width: 33%">
                                                <cc1:CustomTextBox ID="txtLabourApp" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                    MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr id="trTocharge" runat="server">
                                            <td style="width: 33%; text-align: right; padding-right: 30px">
                                                &nbsp;&nbsp;To Charge
                                            </td>
                                            <td style="width: 33%">
                                                <cc1:CustomTextBox ID="txtMaterialBill" runat="server" CssClass="numerictextbox"
                                                    Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                                            </td>
                                            <td style="width: 33%">
                                                <cc1:CustomTextBox ID="txtLabourBill" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                    MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 30%; height: 100%">
                                    <asp:Panel ID="Panel1" runat="server" GroupingText="<span style='font-size:small;font-style:normal; color:Black;font-weight:lighter'>Approved By<span>"
                                        Style="min-height: 100%;">
                                        <br />
                                        <%--<asp:DropDownList ID="ddlUser" runat="server" Width="100%">                        
                      </asp:DropDownList>--%>
                                        <asp:TextBox ID="txtAppUser" CssClass="textboxuppercase" MaxLength="11" Width="120"
                                            runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        OnHold:<asp:CheckBox ID="chkpOnHold" runat="server"></asp:CheckBox>
                        &nbsp; Damage:
                        <asp:CheckBox ID="chkDamage" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 140px;">
                        Released On:
                    </td>
                    <td>
                        <asp:TextBox ID="txtReleasedOn" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox>
                        <cc2:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtReleasedOn"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 140px;">
                        Stock Return Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtStockRetDate" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox>
                        <cc2:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtStockRetDate"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 140px;">
                        Reason
                    </td>
                    <td>
                        <asp:TextBox ID="txtReason" runat="server" CssClass="textboxuppercase" MaxLength="100"
                            Width="250"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click1" />&nbsp;&nbsp;<asp:Button
                            ID="btnBack" runat="server" CssClass="button" Text="Back" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>
