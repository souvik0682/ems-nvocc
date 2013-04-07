<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdvanceContainerList.aspx.cs" Inherits="EMS.WebApp.Reports.AdvanceContainerList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
<%@ Import Namespace="EMS.Utilities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <asp:HiddenField ID="hdnReturn" runat="server" />
    <center>
<fieldset style="padding:5px;width:55%">
    <table style="width: 100%" cellpadding="1" cellspacing="0">
        <tr id="main">
         <td   align="left" style="width: 20%">
                Location:<span class="errormessage">*</span>
            </td>
            <td   align="left" style="width: 30%">
                <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLine_SelectedIndexChanged"
                     Width="120">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlLine" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
            </td>
              <td  align="left" style="width: 20%">
                  <asp:Label ID="lblLine" runat="server" Text="Line"></asp:Label>:<span class="errormessage" style="width: 15%">*</span>
              </td>
        <td  align="left" style="width: 30%">
            <asp:DropDownList ID="ddlLocation" runat="server"  AutoPostBack="true"
                onselectedindexchanged="ddlLocation_SelectedIndexChanged">
            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>            
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlLocation" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
        </td>
           
        </tr>
        <tr runat="server" id="trCar">
      
        <td>Vessel:<span class="errormessage">*</span></td>
        <td  align="left" > <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true"
                onselectedindexchanged="ddlVessel_SelectedIndexChanged">
            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlVessel" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
            </td>
             <td>Voyage:<span class="errormessage">*</span></td> <td  align="left" > <asp:DropDownList ID="ddlVoyage" runat="server" 
                    >
            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="errormessage"
                    ControlToValidate="ddlVoyage" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator></td>
            </tr>
        <tr >
              <td >
              Port of Discharge:<span class="errormessage">*</span>
            </td>
            <td   align="left" >
                 <asp:TextBox ID="txtReturn" runat="server" Width="113"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" CssClass="errormessage"
                    ControlToValidate="txtReturn" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
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
             <td>VIA No:<span class="errormessage">*</span></td> <td align="left">
                 <asp:TextBox ID="txtVIANo" runat="server" Width="113"></asp:TextBox>
                            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="errormessage"
                    ControlToValidate="txtVIANo" InitialValue="0" ValidationGroup="Report"  Display="Dynamic"
                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                 </td>
                 
          
        </tr>
        <tr>       
        <td colspan="4" align="left" style="padding:5px 5px 5px 0">
        <asp:Button ID="btnReport" runat="server" Text="Generate Excel" ValidationGroup="Report" 
                onclick="btnReport_Click" />
        </td>
        </tr>
    </table>
</fieldset>
    </center>

</asp:Content>
