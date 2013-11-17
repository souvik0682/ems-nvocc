<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditSlot.aspx.cs" Inherits="EMS.WebApp.Export.AddEditSlot" %>

<%@ Import Namespace="EMS.Utilities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }

        function AutoCompleteItemSelected(sender, e) {

            if (sender._id == "AutoCompleteEx") {
                var hdnPOL = $get('<%=hdnFromLocation.ClientID %>');
                hdnPOL.value = e.get_value();
            }
            else if (sender._id == "AutoCompleteEx2") {
                var hdnPOD = $get('<%=hdnToLocation.ClientID %>');
                hdnPOD.value = e.get_value();
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
        .style2
        {
            color: #000000;
            font-size: 12px;
            font-weight: bold;
            border: 1px solid #e8e8e8;
            padding: 5px;
            text-align: left;
            vertical-align: top;
            width: 13%;
        }
        .style3
        {
            color: #000000;
            font-size: 12px;
            font-weight: bold;
            border: 1px solid #e8e8e8;
            padding: 5px;
            text-align: left;
            vertical-align: top;
            width: 11%;
        }
        .style4
        {
            color: #000000;
            font-size: 12px;
            font-weight: bold;
            border: 1px solid #e8e8e8;
            padding: 5px;
            text-align: left;
            vertical-align: top;
            width: 12%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        ADD / EDIT SLOT COST</div>
    <center>
        <asp:UpdatePanel ID="upSlotCost" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAddToList" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <fieldset style="width: 95%;">
                    <legend>Add / Edit SLOT COST</legend>
                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                        <tr>
                            <td>
                                Operator:<asp:Label ID="lblOperator" runat="server" CssClass="errormessage" Text="*"></asp:Label> 
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlOperator" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvOperator" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlOperator" InitialValue="0" ValidationGroup="Save" Display="Dynamic"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Line Code:<span class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLineCode" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvLineCode" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlLineCode" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                Port of Loading:<asp:Label ID="lblPOL" runat="server" CssClass="errormessage" Text="*"></asp:Label> 
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnPOL" runat="server" Value="0" />
                                <asp:TextBox ID="txtPOL" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                    Width="250"></asp:TextBox>
                                    <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtPOL" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]">
                                    </asp:RequiredFieldValidator>
                               
                                <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="AutoCompleteExtender1"
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
                                <br />
                            </td>
                            <td>
                                Port of Dispatch:<asp:Label ID="lblPOD" runat="server" CssClass="errormessage" Text="*"></asp:Label> 
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnPOD" runat="server" Value="0" />
                                <asp:TextBox ID="txtPOD" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                    Width="250"></asp:TextBox>
                                    <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtPOD" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]">
                                    </asp:RequiredFieldValidator>
                               
                                <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx2" ID="AutoCompleteExtender2"
                                    TargetControlID="txtPOD" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
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
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                Effective Date<span class="errormessage1">*</span> :
                            </td>
                            <td style="width: 20%;">
                                <asp:TextBox ID="txtEffectDate" runat="server" Width="150" AutoCompleteType="None"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtEffectDate"
                                    PopupPosition="BottomLeft" TargetControlID="txtEffectDate" Format="dd/MM/yyyy"
                                    OnClientDateSelectionChanged="checkDate">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Please select date"
                                    ControlToValidate="txtEffectDate" Display="None" ValidationGroup="vgCharge"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="rfvDate">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtEffectDate"
                                    FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars="/">
                                </cc1:FilteredTextBoxExtender>
                            </td>


                            <td>
                                POD Terminal:<span class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPODTerminal" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                    Width="250"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvHireReference" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtHireReference" ValidationGroup="Save" Display="Dynamic"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                         <tr>
                            <td>
                                Movement Origin:<asp:Label ID="lblTerm1" runat="server" CssClass="errormessage" Text="*"></asp:Label> 
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTerm1" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlTerm1" InitialValue="0" ValidationGroup="Save" Display="Dynamic" 
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Movement Destinatino:<asp:Label ID="lblTerm2" runat="server" CssClass="errormessage" Text="*"></asp:Label> 
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTerm2" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlTerm2" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" 
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
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
                            <table cellspacing="0" style="border-width:0px;border-style:None;width:100%;border-collapse:collapse;" rules="all">
                                    <tr>
                                  
                                        <th style="width: 2%;color:#fff" class="gridviewheader" scope="col">
                                            <asp:HiddenField ID="hdnFSlno" runat="server" Value="-1" />
                                            SI#
                                        </th>
                                        <th class="style2" scope="col" align="center">
                                            <asp:DropDownList ID="ddlCargoType" runat="server" CssClass="dropdownlist" 
                                                Height="21px" Width="108px" 
                                                onselectedindexchanged="ddlCargoType_SelectedIndexChanged">
                                                <asp:ListItem Value="F" Text="FCL"></asp:ListItem>
                                                <asp:ListItem Value="E" Text="ETY"></asp:ListItem>
                                                <asp:ListItem Value="B" Text="BULK"></asp:ListItem>
                                            </asp:DropDownList>
                                           <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="ddlCargoType"
                                                InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                       </th>

                                    <%--    <th style="width: 13%;" class="gridviewheader" scope="col">
                                            <asp:TextBox ID="txtContainerNo" MaxLength="11" runat="server" AutoPostBack="true"
                                            OnTextChanged="txtContainerNo_TextChanged"
                                                  CssClass="textboxuppercase" ForeColor="#747862" 
                                                  ></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="txtContainerNo"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </th>
                                        <th style="width: 13%;" class="gridviewheader" scope="col">
                                            <asp:TextBox ID="txtLGNo" runat="server" CssClass="textboxuppercase" ForeColor="#747862"></asp:TextBox>
                                        </th>
                                        <th style="width: 13%;" class="gridviewheader" scope="col">
                                            <asp:TextBox ID="txtIGMNo" runat="server" CssClass="textboxuppercase" ForeColor="#747862"></asp:TextBox>
                                            <br />
                                             <asp:RegularExpressionValidator ControlToValidate="txtIGMNo" ID="RegularExpressionValidator5"
                                                runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                                                Display="Dynamic" ValidationExpression="^\d*"></asp:RegularExpressionValidator>
                                       </th>--%>
                                       <th class="style3" scope="col" align="center">
                                            <asp:DropDownList ID="ddlSize" runat="server" CssClass="dropdownlist" 
                                                Height="21px" Width="126px">
                                                <asp:ListItem Value="0" Text="---Size---"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                            </asp:DropDownList>
                                           <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="ddlSize"
                                                InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                       </th>
                                       <th class="style4" scope="col"  align="center">
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="dropdownlist" 
                                                Height="21px" Width="118px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ValidationGroup="AddToList" ID="RequiredFieldValidator3"
                                                runat="server" CssClass="errormessage" ErrorMessage="[Required]" ControlToValidate="ddlType"
                                                InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                       </th>
                                       <th class="style2" scope="col" align="center" >
                                            <asp:DropDownList ID="ddlSpecialType" runat="server" CssClass="dropdownlist" 
                                                Height="20px" Width="137px">
                                                <asp:ListItem Value="G" Text="General"></asp:ListItem>
                                                <asp:ListItem Value="H" Text="Haz"></asp:ListItem>
                                                <asp:ListItem Value="R" Text="Reefer"></asp:ListItem>
                                            </asp:DropDownList>
                                           <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="ddlSpecialType"
                                                InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </th>

                                        <th class="style4" scope="col">
                                            <asp:TextBox ID="txtContainerRate" runat="server" CssClass="textboxuppercase" 
                                                ForeColor="#747862" MaxLength="10" Width="135px" onkeyup="IsDecimal(this)" 
                                                Height="24px"></asp:TextBox>
                                        </th>

                                        <th class="style4" scope="col">
                                            <asp:TextBox ID="txtRatePerTon" runat="server" CssClass="textboxuppercase" 
                                                ForeColor="#747862" MaxLength="10" Width="125px" onkeyup="IsDecimal(this)" 
                                                Height="25px"></asp:TextBox>
                                        </th>
                                    
                                        <th class="style4" scope="col">
                                            <asp:TextBox ID="txtRatePerCBM" runat="server" CssClass="textboxuppercase" 
                                                ForeColor="#747862" MaxLength="10" Width="110px" onkeyup="IsDecimal(this)"></asp:TextBox>
                                        </th>
                                       <th style="width: 22%;" class="gridviewheader" scope="col">
                                            <asp:Button ID="btnAddToList" ValidationGroup="AddToList" runat="server" Style="float: right;" CssClass="button" Text="Add to List" OnClick="btnAddToList_Click" />
                                       </td>
                                    </tr>
                                </table>
                                    <asp:GridView ID="gvwSlot" runat="server" AutoGenerateColumns="false" BorderStyle="None" ShowHeaderWhenEmpty="true"
                                    BorderWidth="0" Width="100%" EnableViewState="true" OnRowCommand="gvwSlot_OnRowCommand"
                                   OnRowEditing="gvwSlot_RowEditing" OnRowDeleting="gvwSlot_OnRowDeleting"
                                    >                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%= counter++%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridviewheader" />
                                            <ItemStyle CssClass="gridviewitem" />
                                        </asp:TemplateField>

                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="7%" HeaderStyle-CssClass="gridviewheader"
                                            DataField="CargoType" HeaderText="Cargo Type" />
                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-CssClass="gridviewheader" DataField="CntrSize" HeaderText="Size" />
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-Width="7%" ItemStyle-CssClass="gridviewitem"
                                            HeaderStyle-CssClass="gridviewheader">
                                            <ItemTemplate>
                                                <%# GetTypeData(Eval("ContainerTypeID").ToString()) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="8%" HeaderStyle-CssClass="gridviewheader"
                                            DataField="SpecialType" HeaderText="Special Type" />
                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-CssClass="gridviewheader" DataField="ContainerRate" HeaderText="Rate / Container" />
                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-CssClass="gridviewheader" DataField="RatePerTon" HeaderText="Rate / Ton" />
                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-CssClass="gridviewheader" DataField="RateperCBM" HeaderText="Rate / CBM" />
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="gridviewheader"  Width="4%" />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("ContainerNo")%>'
                                                    CommandName="Edit" ImageUrl="~/Images/edit.png" Height="16" Width="16" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="gridviewheader"  Width="4%" />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnRemove" runat="server" OnClientClick="return Confirm()" CommandArgument='<%#Eval("ContainerNo")%>'
                                                    CommandName="Remove" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Style="margin-right: 10px"
                                    CssClass="button" Text="Save" ValidationGroup="Save" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" PostBackUrl="~/Hire/Hire.aspx" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <script type="text/javascript">
                    // Create the event handler for PageRequestManager.endRequest
                    //   var prm = Sys.WebForms.PageRequestManager.getInstance();

                    //prm.add_endRequest();
                    function pageLoad(sender, args) {
                        //InitialzeDP();
                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
