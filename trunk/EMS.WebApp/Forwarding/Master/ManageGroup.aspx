<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageGroup.aspx.cs" Inherits="EMS.WebApp.Forwarding.Master.ManageGroup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="EMS.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
<div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage">
            </div>
        </div>
    </div>
    <div id="headercaption">
       Manage Group
    </div>
    <center>
        <div style="width: 850px;">
             <fieldset style="width:100%;">
            <legend>Search Group</legend>
            <table width="100%" style="height: 69px">
                <tr>
                    
                    <td>
                        <asp:TextBox ID="txtPartyName" runat="server" CssClass="watermark" 
                            ForeColor="#747862" Height="17px" Width="100%"></asp:TextBox>
                        <cc1:textboxwatermarkextender ID="wmetxtPartyName" runat="server" 
                            TargetControlID="txtPartyName" WatermarkText="Type Group Name" 
                            WatermarkCssClass="watermark"></cc1:textboxwatermarkextender>
                    </td>
                   
                </tr> 
                <tr>
                    <td colspan="3" align="center"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" OnClick="btnSearch_Click" />
                     <%--<asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="button" Width="100px" onclick="btnRefresh_Click"  />--%></td>
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
                <legend>Group List</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add Group" Width="130px" PostBackUrl="~/Forwarding/Master/AddEditGroup.aspx"
                         />
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
                            <asp:AsyncPostBackTrigger ControlID="gvwHire" EventName="Sorting" />
                            <asp:PostBackTrigger ControlID="btnAdd"/>
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gvwHire" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" AllowSorting="true" 
                                OnSorting="gvwHire_OnSorting"
                                OnPageIndexChanging="gvwHire_PageIndexChanging"
                                 OnRowCommand="gvwHire_RowCommand"
                                Width="100%" onrowdatabound="gvwHire_RowDataBound">
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                 <emptydatatemplate>No Party(s) Found</emptydatatemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#" ItemStyle-Width="2%">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right"/>
                                        <ItemTemplate>
                                            <%=counter++%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField  HeaderText="Group Name" DataField="GroupName"  HeaderStyle-CssClass="gridviewheader"  SortExpression="GroupName" ItemStyle-CssClass="gridviewitem"/>
                                    <asp:BoundField  HeaderText="Address" DataField="GroupAddress"  HeaderStyle-CssClass="gridviewheader"  SortExpression="Address" ItemStyle-CssClass="gridviewitem"/>
                 <%--                   <asp:BoundField  HeaderText="Phone" DataField="Phone" HeaderStyle-CssClass="gridviewheader"  SortExpression="Phone" ItemStyle-CssClass="gridviewitem"/>
                                    <asp:BoundField  HeaderText="Contact Person" HeaderStyle-CssClass="gridviewheader" DataField="ContactPerson"  SortExpression="ContactPerson" ItemStyle-CssClass="gridviewitem"/>
                               --%>
                                    <asp:TemplateField ItemStyle-Width="8%">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem"  HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" PostBackUrl='<%# String.Format("~/Forwarding/Master/AddEditGroup.aspx?GroupId={0}",EMS.Utilities.GeneralFunctions.EncryptQueryString(Eval("GroupID").ToString()) ) %>' ImageUrl="~/Images/edit.png"
                                                Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="8%">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>                                     
                                       
                                            <asp:ImageButton ID="btnRemove"  runat="server" OnClientClick="return Confirm()" CommandArgument='<%# EMS.Utilities.GeneralFunctions.EncryptQueryString(Eval("GroupID").ToString()) %>' CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                Height="16" Width="16" />
                                               
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
            </fieldset>
        </div>
    </center>
</asp:Content>
