<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExpAddEditSlotCost.aspx.cs" Inherits="EMS.WebApp.MasterModule.ExpAddEditSlotCost" %>

<%@ Import Namespace="EMS.Utilities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }

        function AutoCompleteItemSelected(sender, e) {
            if (sender._id == "AutoCompleteEx") {
                var hdnFromLocation = $get('<%=hdnReturn.ClientID %>');
                hdnFromLocation.value = e.get_value();
                //   alert(hdnFromLocation.value);
            }
            else if (sender._id == "DestinationPortBehaviorID") {
                var hdnFromLocation = $get('<%=hdnDestinationPort.ClientID %>');
                hdnFromLocation.value = e.get_value();
                //   alert(hdnFromLocation.value);
            }
            else if (sender._id == "LoadPortBehaviorID") {
                var hdnFromLocation = $get('<%=hdnLoadPort.ClientID %>');
                hdnFromLocation.value = e.get_value();
                //   alert(hdnFromLocation.value);
            }
            else if (sender._id == "DestinationPortBehaviorID") {
                var hdnFromLocation = $get('<%=hdnDestinationPort.ClientID %>');
                hdnFromLocation.value = e.get_value();
                //   alert(hdnFromLocation.value);
            }
            else { }
        }
    </script>
    <style type="text/css">
        .custtable
        {
            width: 100%;
        }
        .custtable td
        {
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        SLOT COST</div>
    <center>
        <asp:UpdatePanel ID="upSLOT" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAddToList" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <fieldset style="width: 95%;">
                    <legend>Add / Edit Slot Cost</legend>
                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                        <tr>
                            <td>
                                Operator:<asp:Label ID="lblOperator" runat="server" CssClass="errormessage" Text="*"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlOperator" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlOperator" InitialValue="0" ValidationGroup="Save" Display="Dynamic"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 20%;">
                                Load Port:<span class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLoadPort" runat="server" CssClass="textboxuppercase watermark1" ForeColor="#747862"></asp:TextBox>
                                   <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtLoadPort" WatermarkText="Load Port" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                                 <br /> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtLoadPort" InitialValue="" ValidationGroup="Save" Display="Dynamic"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdnLoadPort" runat="server" />
                                <cc1:AutoCompleteExtender runat="server" BehaviorID="LoadPortBehaviorID" ID="AutoCompleteExtenderLoadPort"
                                    TargetControlID="txtLoadPort" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                                    ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected">
                                    <animations>
                                        <OnShow>
                                            <Sequence>
                                                <%-- Make the completion list transparent and then show it --%>
                                                <OpacityAction Opacity="0" />
                                                <HideAction Visible="true" />
                            
                                                <%--Cache the original size of the completion list the first time
                                                    the animation is played and then set it to zero --%>
                                                <ScriptAction Script="
                                                    // Cache the size and setup the initial size
                                                    var behavior = $find('LoadPortBehaviorID');
                                                    if (!behavior._height) {
                                                        var target = behavior.get_completionList();
                                                        behavior._height = target.offsetHeight - 2;
                                                        target.style.height = '0px';
                                                    }" />
                            
                                                <%-- Expand from 0px to the appropriate size while fading in --%>
                                                <Parallel Duration=".4">
                                                    <FadeIn />
                                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('LoadPortBehaviorID')._height" />
                                                </Parallel>
                                            </Sequence>
                                        </OnShow>
                                        <OnHide>
                                            <%-- Collapse down to 0px and fade out --%>
                                            <Parallel Duration=".4">
                                                <FadeOut />
                                                <Length PropertyKey="height" StartValueScript="$find('LoadPortBehaviorID')._height" EndValue="0" />
                                            </Parallel>
                                        </OnHide>
                                    </animations>
                                </cc1:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mov Origin:<span class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMovOrigin" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlMovOrigin" ValidationGroup="Save" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Destination Port:<span class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDestinationPort" runat="server" CssClass="textboxuppercase watermark1" ForeColor="#747862"></asp:TextBox>
                                 <cc1:TextBoxWatermarkExtender ID="txtWMEtxtDestinationPort" runat="server" TargetControlID="txtDestinationPort" WatermarkText="Destination Port" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                                  <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtDestinationPort" InitialValue="" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdnDestinationPort" runat="server" />
                                <cc1:AutoCompleteExtender runat="server" BehaviorID="DestinationPortBehaviorID" ID="AutoCompleteExtenderDestinationPort"
                                    TargetControlID="txtDestinationPort" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                                    ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected">
                                    <animations>
                                        <OnShow>
                                            <Sequence>
                                                <%-- Make the completion list transparent and then show it --%>
                                                <OpacityAction Opacity="0" />
                                                <HideAction Visible="true" />
                            
                                                <%--Cache the original size of the completion list the first time
                                                    the animation is played and then set it to zero --%>
                                                <ScriptAction Script="
                                                    // Cache the size and setup the initial size
                                                    var behavior = $find('DestinationPortBehaviorID');
                                                    if (!behavior._height) {
                                                        var target = behavior.get_completionList();
                                                        behavior._height = target.offsetHeight - 2;
                                                        target.style.height = '0px';
                                                    }" />
                            
                                                <%-- Expand from 0px to the appropriate size while fading in --%>
                                                <Parallel Duration=".4">
                                                    <FadeIn />
                                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('DestinationPortBehaviorID')._height" />
                                                </Parallel>
                                            </Sequence>
                                        </OnShow>
                                        <OnHide>
                                            <%-- Collapse down to 0px and fade out --%>
                                            <Parallel Duration=".4">
                                                <FadeOut />
                                                <Length PropertyKey="height" StartValueScript="$find('DestinationPortBehaviorID')._height" EndValue="0" />
                                            </Parallel>
                                        </OnHide>
                                    </animations>
                                </cc1:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mov Destination:<asp:Label ID="lblValid" runat="server" CssClass="errormessage" Text="*"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMovDestination" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlMovDestination" InitialValue="0" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Pod Terminal:<asp:Label ID="lblReturn" runat="server" CssClass="errormessage" Text="*"></asp:Label>
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnReturn" runat="server" Value="0" />
                                <asp:TextBox ID="txtPodTerminal" runat="server" CssClass="textboxuppercase watermark1" ForeColor="#747862" MaxLength="50"
                                    Width="250"></asp:TextBox>
                                      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtPodTerminal" WatermarkText=" Pod Terminal" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvReturn" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtPodTerminal" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]">
                                </asp:RequiredFieldValidator>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Line Code:<span class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLineCode" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvLineCode" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlLineCode" ValidationGroup="Save" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Effective Date:<asp:Label ID="lblEffective" runat="server" CssClass="errormessage"
                                    Text="*"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="textboxuppercase watermark1" ForeColor="#747862"></asp:TextBox>
                                  <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtEffectiveDate" WatermarkText="EFFECTIVE DATE" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                                <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="txtEffectiveDate"
                                    TargetControlID="txtEffectiveDate">
                                </cc1:CalendarExtender>
                                 <br /> <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtEffectiveDate" ValidationGroup="Save" Display="Dynamic"
                                    ErrorMessage="[Required]">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtEffectiveDate" ID="RegularExpressionValidator6"
                                    runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]" ValidationGroup="Save"
                                    Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                             <div style="display:none">
                            <asp:TextBox ID="TextBox1" runat="server" Width="0" Text="0"></asp:TextBox>
                            </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="errormessage" runat="server" ControlToValidate="TextBox1" InitialValue="0"
                                 ErrorMessage="Please add atleast one Slot Cost." ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <table id="gvwSlotCostUpper" cellspacing="0" style="border-width: 0px; border-style: None; width: 100%;
                                    border-collapse: collapse;" rules="all">
                                    <tr>
                                        <th style="width: 53px;" color: #fff" class="gridviewheader" scope="col">
                                            SI#
                                        </th>
                                        <th style="width: 118px;" class="gridviewheader" scope="col">
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Value="0" Selected="True" Text="---Type---"></asp:ListItem>                                                
                                                <asp:ListItem  Text="FCL" Value="F"></asp:ListItem>
                                             <asp:ListItem Text="LCL" Value="L"></asp:ListItem>
                                             <asp:ListItem Text="ETY" Value="E"></asp:ListItem>
                                             <asp:ListItem Text="Break Bulk" Value="N"></asp:ListItem>
                                             <asp:ListItem Text="Bulk" Value="N"></asp:ListItem>
                                             <asp:ListItem Text="None" Value="N"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="ddlType" InitialValue="0"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </th>
                                        <th style="width: 118px;" class="gridviewheader" scope="col">
                                            <asp:DropDownList ID="ddlSize" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Value="0" Text="---Size---"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="ddlSize" InitialValue="0"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </th>
                                        <th style="width: 194px;" class="gridviewheader" scope="col">
                                            <asp:DropDownList ID="ddlContainerType" runat="server" CssClass="dropdownlist">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="ddlContainerType"
                                                InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </th>
                                        <th style="width: 181px;" class="gridviewheader" scope="col" align="center">
                                            <asp:DropDownList ID="ddlCargo" runat="server" CssClass="dropdownlist">
                                               <asp:ListItem Selected="True" Value="0" Text="---Cargo---"></asp:ListItem>                                                
                                                <asp:ListItem  Text="GEN" Value="G"></asp:ListItem>
                                             <asp:ListItem Text="HAZ" Value="H"></asp:ListItem>
                                             <asp:ListItem Text="REEFER" Value="R"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ValidationGroup="AddToList" ID="RequiredFieldValidator3"
                                                runat="server" CssClass="errormessage" ErrorMessage="[Required]" ControlToValidate="ddlCargo"
                                                InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </th>
                                        <th style="width: 181px;" class="gridviewheader" scope="col" align="right">
                                           <%-- <asp:TextBox ID="txtAmount" runat="server" CssClass="textboxuppercase watermark1" ForeColor="#747862"></asp:TextBox>--%>
                                             <cc2:CustomTextBox style="float:right" ID="txtAmount" runat="server" CssClass="numerictextbox"
                                                    Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="100"></cc2:CustomTextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtAmount" WatermarkText="AMOUNT" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="txtAmount"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ControlToValidate="txtAmount" ID="RegularExpressionValidator3"
                                                runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                                                Display="Dynamic" ValidationExpression="^\d{1,6}.\d{1,2}|\d{1,6}$"></asp:RegularExpressionValidator>
                                        </th>
                                        <th style="width: 181px;text-align:right" class="gridviewheader" scope="col" align="right">
                                         <cc2:CustomTextBox style="float:right" ID="txtRevTon" runat="server" CssClass="numerictextbox"
                                                    Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="100"></cc2:CustomTextBox>

                                            <%--<asp:TextBox ID=""  runat="server" CssClass="textboxuppercase watermark1" ForeColor="#747862"></asp:TextBox>--%>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtRevTon" WatermarkText="REVTON" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="txtRevTon"
                                                Display="Dynamic"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator ControlToValidate="txtRevTon" ID="RegularExpressionValidator1"
                                                runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                                                Display="Dynamic" ValidationExpression="^\d{1,6}.\d{1,2}|\d{1,6}$"></asp:RegularExpressionValidator>
                                      
                                        </th>
                                        <th style="width: 78px;" class="gridviewheader" scope="col" colspan="2">
                                            <asp:Button ID="btnAddToList" ValidationGroup="AddToList" runat="server" Style="float: right;"
                                                CssClass="button" Text="Add to List" OnClick="btnAddToList_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvwSlotCost" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                        ShowHeaderWhenEmpty="true" BorderWidth="0" Width="100%" EnableViewState="true"
                        OnRowCommand="gvwSlotCost_OnRowCommand" OnRowEditing="gvwSlotCost_RowEditing" OnRowDeleting="gvwSlotCost_OnRowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                <%=counter++ %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" />
                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="TYPE" >
                                <HeaderStyle CssClass="gridviewheader" Width="10%" />
                                <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                  <%#GetItemFromValue(Eval("TYPE"), "ddlType")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="10%" HeaderStyle-CssClass="gridviewheader"
                                DataField="SIZE" HeaderText="SIZE" />

                                <asp:TemplateField HeaderText="CONTAINERTYPE"  HeaderStyle-Width="16%">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                  <%#GetItemFromValue(Eval("CONTAINERTYPE"), "ddlContainerType")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            

                                 <asp:TemplateField HeaderText="CARGO" HeaderStyle-Width="15%">
                                <HeaderStyle CssClass="gridviewheader"  />
                                <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                  <%#GetItemFromValue(Eval("CARGO"), "ddlCargo")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField ItemStyle-CssClass="gridviewitem"  HeaderStyle-Width="15%"
                                HeaderStyle-CssClass="gridviewheader" HeaderStyle-HorizontalAlign="Right" DataField="AMOUNT" HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField ItemStyle-CssClass="gridviewitem" 
                                HeaderStyle-CssClass="gridviewheader"  HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right" DataField="REVTON" HeaderText="REV TON" HeaderStyle-Width="15%"/>
                            <asp:TemplateField >
                                <HeaderStyle CssClass="gridviewheader" Width="7%" />
                                <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("SLOTCOSTID")%>'
                                        CommandName="Edit" ImageUrl="~/Images/edit.png" Height="16" Width="16" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" Width="7%" />
                                <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnRemove" runat="server" OnClientClick="return Confirm()" CommandArgument='<%#Eval("SLOTCOSTID")%>'
                                        CommandName="Remove" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </td> </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Style="margin-right: 10px"
                                CssClass="button" Text="Save" ValidationGroup="Save" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" PostBackUrl="~/Hire/Hire.aspx"
                                Text="Cancel" />
                        </td>
                    </tr>
                    </table>
                </fieldset>
                <script type="text/javascript">
                    // Create the event handler for PageRequestManager.endRequest
                    //   var prm = Sys.WebForms.PageRequestManager.getInstance();

                    //prm.add_endRequest();
                    function pageLoad(sender, args) {
                        var idGr = '#<%=gvwSlotCost.ClientID %>';
                        var idgvwSlotCostUpper = '#gvwSlotCostUpper';
                        $(idGr).find('th').each(function (i) {
                            if (i < 8) {
                                var th = $(idgvwSlotCostUpper).find('th').eq(i);
                                $(th).attr('style', $(this).attr('style'));
                                $(th).width($(this).width() + 1);
                                $(th).position().left = $(this).position().left;
                                if (i == 5 || i == 6) {
                                    $(this).css('text-align', 'right');
                                }
                            }
                        });
//                        var t = '<tr>' + $(idgvwSlotCostUpper).find('tr').eq(0).html() + '</tr><tr>' + $(idGr).find('tr').eq(0).html() + '</tr>';
//                        $(idgvwSlotCostUpper).remove();
//                        $(idGr).find('tr').eq(0).html(t);
                       
                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
