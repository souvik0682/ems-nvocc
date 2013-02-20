<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditHire.aspx.cs" Inherits="EMS.WebApp.Hire.AddEditHire" %>

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
                var hdnFromLocation = $get('<%=hdnReturn.ClientID %>');
                hdnFromLocation.value = e.get_value();
             //   alert(hdnFromLocation.value);
            }
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
        ADD / EDIT HIRE</div>
    <center>
        <asp:UpdatePanel ID="upHire" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAddToList" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <fieldset style="width: 95%;">
                    <legend>Add / Edit HIRE</legend>
                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                        <tr>
                            <td>
                                Stock Location:<asp:Label ID="lblStock" runat="server" CssClass="errormessage" Text="*"></asp:Label> 
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlLocation" InitialValue="0" ValidationGroup="Save" Display="Dynamic"
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
                            <td style="width: 20%;">
                                Transaction Type:<span class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdTransactionType" runat="server" 
                                    RepeatDirection="Horizontal" AutoPostBack="True" 
                                    onselectedindexchanged="rdTransactionType_SelectedIndexChanged" 
                                    RepeatLayout="Flow">
                                    <asp:ListItem Text="On Hire " Selected="True" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Off Hire " Value="F"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                Hire Reference:<span class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHireReference" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                    Width="250"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvHireReference" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtHireReference" ValidationGroup="Save" Display="Dynamic"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Reference Date:<span class="errormessage">*</span>
                            </td>
                              <td >
                                <asp:TextBox ID="txtReferenceDate" runat="server" CssClass="" MaxLength="10" Width="150"></asp:TextBox>
                                <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="txtReferenceDate"
                                TargetControlID="txtReferenceDate">
                            </cc1:CalendarExtender>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvReferenceDate" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtReferenceDate" ValidationGroup="Save" Display="Dynamic"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtReferenceDate" ID="revReferenceDate"
                                    runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                                    Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                Valid Till:<asp:Label ID="lblValid" runat="server" CssClass="errormessage" Text="*"></asp:Label> 
                            </td>
                              <td >
                                <asp:TextBox ID="txtValidTill" runat="server" CssClass="textboxuppercase" MaxLength="10"
                                    Width="150"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtValidTill"
                                TargetControlID="txtValidTill" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvValidTill" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtValidTill" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtValidTill" ID="RegularExpressionValidator1"
                                    runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                                    Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Return At:<asp:Label ID="lblReturn" runat="server" CssClass="errormessage" Text="*"></asp:Label> 
                            </td>
                              <td>
                                <asp:HiddenField ID="hdnReturn" runat="server" Value="0" />
                                <asp:TextBox ID="txtReturn" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                    Width="250"></asp:TextBox>
                                    <br />
                                <asp:RequiredFieldValidator ID="rfvReturn" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtReturn" ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Required]">
                                    </asp:RequiredFieldValidator>
                               
                                <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
                                    TargetControlID="txtReturn" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
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
                                Narration:
                            </td>
                             <td >
                                <asp:TextBox ID="txtNarration" runat="server" CssClass="textboxuppercase" TextMode="MultiLine"
                                    Rows="3" Columns="200" Width="250"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Release Ref No:
                            </td>
                              <td >
                                <asp:TextBox ID="txtReleaseRefNo" runat="server" CssClass="textboxuppercase" MaxLength="200"
                                    Width="250"></asp:TextBox><br />
                            </td>
                            <td>
                                Release Date:
                            </td>
                             <td >
                                <asp:TextBox ID="txtReleaseDate" runat="server" CssClass="textboxuppercase" MaxLength="10"
                                    Width="150"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtReleaseDate" Format="dd/MM/yyyy"
                                TargetControlID="txtReleaseDate">
                            </cc1:CalendarExtender>
                                <asp:RegularExpressionValidator ControlToValidate="txtReleaseDate" ID="RegularExpressionValidator2" 
                                    runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                                    Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                <br />
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
                                           SI#
                                        </th>
                                          <th style="width: 13%;" class="gridviewheader" scope="col">
                                            <asp:TextBox ID="txtContainerNo" MaxLength="11" runat="server" CssClass="textboxuppercase" ForeColor="#747862"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="txtContainerNo"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </th>
                                          <th style="width: 13%;" class="gridviewheader" scope="col">
                                            <asp:TextBox ID="txtLGNo" runat="server" CssClass="textboxuppercase" ForeColor="#747862"></asp:TextBox>
                                        </th>
                                          <th style="width: 13%;" class="gridviewheader" scope="col">
                                            <asp:TextBox ID="txtIGMNo" runat="server" CssClass="numerictextbox" ForeColor="#747862"></asp:TextBox>
                                            <br />
                                             <asp:RegularExpressionValidator ControlToValidate="txtIGMNo" ID="RegularExpressionValidator5"
                                                runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                                                Display="Dynamic" ValidationExpression="^\d$"></asp:RegularExpressionValidator>
                                       </th>
                                          <th style="width: 13%;" class="gridviewheader" scope="col" align="center">
                                            <asp:DropDownList ID="ddlSize" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Value="0" Text="---Size---"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                            </asp:DropDownList>
                                           <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                                                ValidationGroup="AddToList" ErrorMessage="[Required]" ControlToValidate="ddlSize"
                                                InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                       </th>
                                          <th style="width: 13%;" class="gridviewheader" scope="col"  align="center">
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="dropdownlist">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ValidationGroup="AddToList" ID="RequiredFieldValidator3"
                                                runat="server" CssClass="errormessage" ErrorMessage="[Required]" ControlToValidate="ddlType"
                                                InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                       </th>
                                          <th style="width: 13%;" class="gridviewheader" scope="col">
                                            <asp:TextBox ID="txtIGMDate" runat="server" CssClass="textboxuppercase" ForeColor="#747862"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtIGMDate" Format="dd/MM/yyyy"
                                TargetControlID="txtIGMDate">
                            </cc1:CalendarExtender>
                                            <asp:RegularExpressionValidator ControlToValidate="txtIGMDate" ID="RegularExpressionValidator3"
                                                runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                                                Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                       </th>
                                          <th style="width: 13%;" class="gridviewheader" scope="col">
                                            <asp:TextBox ID="txtOnHireDate" runat="server" CssClass="textboxuppercase" ForeColor="#747862"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtOnHireDate" Format="dd/MM/yyyy"
                                TargetControlID="txtOnHireDate">
                            </cc1:CalendarExtender>
                                            <br />
                                            <asp:RegularExpressionValidator ControlToValidate="txtOnHireDate" ID="RegularExpressionValidator4"
                                                runat="server" CssClass="errormessage" ErrorMessage="[Please check the input]"
                                                Display="Dynamic" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                       </th>
                                          <th style="width: 22%;" class="gridviewheader" scope="col">
                                            <asp:Button ID="btnAddToList" ValidationGroup="AddToList" runat="server" Style="float: right;" CssClass="button" Text="Add to List" OnClick="btnAddToList_Click" />
                                        </td>
                                    </tr>
                                </table>
                    <asp:GridView ID="gvwHire" runat="server" AutoGenerateColumns="false" BorderStyle="None" ShowHeaderWhenEmpty="true"
                                    BorderWidth="0" Width="100%" EnableViewState="true" OnRowCommand="gvwHire_OnRowCommand"
                                   OnRowEditing="gvwHire_RowEditing" OnRowDeleting="gvwHire_OnRowDeleting"
                                    >                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%= counter++%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridviewheader" />
                                            <ItemStyle CssClass="gridviewitem" />
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="13%" HeaderStyle-CssClass="gridviewheader"
                                            DataField="ContainerNo" HeaderText="Container No." />
                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="13%" HeaderStyle-CssClass="gridviewheader"
                                            DataField="LGNo" HeaderText="LG No." />
                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-CssClass="gridviewheader" DataField="IGMNo" HeaderText="IGM No." />
                                        <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-CssClass="gridviewheader" DataField="CntrSize" HeaderText="Size" />
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-Width="13%" ItemStyle-CssClass="gridviewitem"
                                            HeaderStyle-CssClass="gridviewheader">
                                            <ItemTemplate>
                                                <%# GetTypeData(Eval("ContainerTypeID").ToString()) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGMDate" HeaderStyle-Width="13%" ItemStyle-CssClass="gridviewitem"
                                            HeaderStyle-CssClass="gridviewheader">
                                            <ItemTemplate>
                                                <%# Eval("IGMDate").DataToValue<DateTime>()%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="12%" HeaderText="On Hire Date" ItemStyle-CssClass="gridviewitem"
                                            HeaderStyle-CssClass="gridviewheader">
                                            <ItemTemplate>
                                                <%# Eval("ActualOnHireDate").DataToValue<DateTime>()%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                <asp:ImageButton ID="btnRemove" runat="server" CommandArgument='<%#Eval("ContainerNo")%>'
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
