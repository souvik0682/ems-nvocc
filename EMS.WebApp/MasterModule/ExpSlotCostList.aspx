<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExpSlotCostList.aspx.cs" Inherits="EMS.WebApp.MasterModule.ExpSlotCostList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="EMS.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .custtable
        {
            width: 100%;
        }
        .custtable td
        {
            vertical-align: top;
        }
        button[type="reset"] 
        {
        background-color: #f3f6f8;
        color: black;
        border: solid 1px #c8c8c8;
        cursor: pointer;
        font-family: "Trebuchet MS", Helvetica, sans-serif;
        font-size: 10pt;
        height: 28px;
        }

    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }

        function AutoCompleteItemSelected(sender, e) {
             if (sender._id == "DestinationPortBehaviorID") {
                var hdnFromLocation = $get('<%=hdnDestinationPort.ClientID %>');
                hdnFromLocation.value = e.get_value();
                //   alert(hdnFromLocation.value);
            }
            else if (sender._id == "LoadPortBehaviorID") {
                var hdnFromLocation = $get('<%=hdnLoadPort.ClientID %>');
                hdnFromLocation.value = e.get_value();
                //   alert(hdnFromLocation.value);
            }
            else { }
        }
    </script>
    
    <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage">
            </div>
        </div>
    </div>
    <div id="headercaption">
        SLOT COST LIST
    </div>
    <center>
        <div style="width: 850px;">
            <fieldset style="width: 100%;">
                <legend>Search Slot Cost</legend>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSlotOperator" runat="server" CssClass="textboxuppercase watermark1" ForeColor="#747862"></asp:TextBox>
                              <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSlotOperator" WatermarkText="SLOT OPERATOR" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLine" runat="server" CssClass="dropdownlist">
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr>                        
                        <td>
                            <asp:TextBox ID="txtLoadPort" runat="server" CssClass="textboxuppercase watermark1" ForeColor="#747862"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtLoadPort" WatermarkText="LOAD PORT" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                            <asp:HiddenField ID="hdnLoadPort" runat="server" />
                            <cc1:AutoCompleteExtender runat="server" BehaviorID="LoadPortBehaviorID" ID="AutoCompleteExtenderLoadPort"
                                TargetControlID="txtLoadPort" ServicePath="~/GetLocation.asmx" ServiceMethod="GetCompletionList"
                                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
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
                       
                        <td>
                            <asp:TextBox ID="txtDestinationPort" runat="server" CssClass="textboxuppercase watermark1" ForeColor="#747862"></asp:TextBox>
                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDestinationPort" WatermarkText="DESTINATION PORT" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
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
                        <td colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"/>
                          <asp:Button ID="Button1" runat="server" Text="Reset" />  
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:UpdateProgress ID="uProgressLoc" runat="server" AssociatedUpdatePanelID="upLoc">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <fieldset id="fsList" runat="server" style="width: 100%; min-height: 100px;">
                <legend>Slot Cost List</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add New" Width="130px" 
                        PostBackUrl="ExpAddEditSlotCost.aspx" onclick="btnAdd_Click" />
                </div>
                 <div style="height: 30px;">
                    &nbsp; <span class="errormessage" style="display: none;">* Indicates Inactive Location(s)</span>
                </div>
                <br />
                <div>
                    <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                       
                            <asp:GridView ID="gvwSlotCost" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwSlotCost_PageIndexChanging"
                                OnRowCommand="gvwSlotCost_RowCommand" Width="100%" OnRowDataBound="gvwSlotCost_RowDataBound">
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Slot Cost Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#" ItemStyle-Width="2%">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%=counter++%>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                    <asp:BoundField ItemStyle-CssClass="gridviewitem" HeaderStyle-CssClass="gridviewheader" SortExpression="SlotOperatorName"
                                        DataField="SlotOperatorName" HeaderText="Operator Name" ItemStyle-Width="30%" />
                                    <asp:BoundField ItemStyle-CssClass="gridviewitem"   ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-CssClass="gridviewheader" DataField="LineName" HeaderText="Line" SortExpression="LINE"/>
                                    <asp:BoundField ItemStyle-CssClass="gridviewitem"  ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-CssClass="gridviewheader" DataField="LoadPort" HeaderText="Load Port" SortExpression="POL" ItemStyle-Width="18%"/>
                                    <asp:BoundField ItemStyle-CssClass="gridviewitem"   HeaderStyle-CssClass="gridviewheader"
                                        DataField="DischargePort" HeaderText="Discharge Port" SortExpression="POD" ItemStyle-Width="18%"/>
                                   
                                    <asp:TemplateField HeaderText="Effective Date">
                                    <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center" VerticalAlign="Middle"/>
                                    <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <%# Convert.ToDateTime(Eval("effDate")).ToString("d")%>                                          
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField ItemStyle-CssClass="gridviewitem"   ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-CssClass="gridviewheader" DataField="Terms" HeaderText="Terms" />
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle   CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" PostBackUrl='<%# String.Format("~/MasterModule/ExpAddEditSlotCost.aspx?id={0}",EMS.Utilities.GeneralFunctions.EncryptQueryString(Eval("pk_SlotID").ToString()) ) %>'
                                                ImageUrl="~/Images/edit.png" Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnRemove" runat="server" OnClientClick="return Confirm()" CommandArgument='<%# EMS.Utilities.GeneralFunctions.EncryptQueryString(Eval("pk_SlotID").ToString()) %>'
                                                CommandName="Remove" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="display: none;">
                    <asp:DropDownList ID="ddlIEC" runat="server" Width="255" >
                        <asp:ListItem Text="Import" Value="I"></asp:ListItem>
                        <asp:ListItem Text="Export" Value="E"></asp:ListItem>
                        <asp:ListItem Text="General" Value="G"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </fieldset>
        </div>
    </center>
</asp:Content>
