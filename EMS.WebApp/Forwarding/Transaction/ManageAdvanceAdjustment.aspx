<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageAdvanceAdjustment.aspx.cs" Inherits="EMS.WebApp.Forwarding.Transaction.ManageAdvanceAdjustment" %>
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
       Manage Adjustment of Advance
    </div>
    <center>
        <div style="width: 850px;">
             <fieldset style="width:100%;">
            <legend>Search Adjustment of Advance</legend>
               
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:DropDownList ID="ddlParyType" runat="server"  CssClass="dropdownlist" Width="100" Height="26" 
                            AutoPostBack="true" onselectedindexchanged="ddlParyType_SelectedIndexChanged">
                        <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Debtors" Value="D" />
                        <asp:ListItem Text="Creditors" Value="C" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:textboxwatermarkextender ID="wmetxtHBLNo" runat="server" 
                            TargetControlID="txtJobNo" WatermarkText="Type HBL No" 
                            WatermarkCssClass="watermark"></cc1:textboxwatermarkextender>
                    </td>
                </tr> 
                <tr>
                    <td><asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="watermark" ForeColor="#747862" ></asp:TextBox>
                        <cc1:textboxwatermarkextender ID="wmetxtInvoiceNo" runat="server" 
                            TargetControlID="txtInvoiceNo" WatermarkText="Type Invoice No" 
                            WatermarkCssClass="watermark"></cc1:textboxwatermarkextender>
                    </td>
                    <td><asp:TextBox ID="txtAdjNo" runat="server" CssClass="watermark" ForeColor="#747862" ></asp:TextBox>
                        <cc1:textboxwatermarkextender ID="wmetxtAdjNo" runat="server" 
                            TargetControlID="txtAdjNo" WatermarkText="Type Adjustment No" 
                            WatermarkCssClass="watermark"></cc1:textboxwatermarkextender>
                    </td>
                </tr> 
                <tr>
                    <td align="left">
                        <asp:DropDownList ID="ddlAdvOrAdj" runat="server"  CssClass="dropdownlist" 
                            Width="100" Height="26"  AutoPostBack="true" onselectedindexchanged="ddlAdvOrAdj_SelectedIndexChanged" >
                        <asp:ListItem Text="Adjustments" Value="J" />
                        <asp:ListItem Text="Advances" Value="A" />
                        </asp:DropDownList>
                    </td>
                    <td colspan="3" align="right"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" OnClick="btnSearch_Click" />
                  <%--   <asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="button" Width="100px" onclick="btnRefresh_Click"  />--%></td>
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
                <legend>Party List</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add Adjustment" Width="130px" PostBackUrl="~/Forwarding/Transaction/AddEditAdvanceAdjustment.aspx"
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
                                 <emptydatatemplate>No Data Found</emptydatatemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#" ItemStyle-Width="2%">
  <%--                                      <HeaderStyle CssClass="gridviewheader" />--%>
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right"/>
                                        <ItemTemplate>
                                            <%=counter++%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField  HeaderText="Party" DataField="PartyName"  SortExpression="PartyName" ItemStyle-CssClass="gridviewitem"  ItemStyle-Width="200px"/>
                                <%--<asp:BoundField  HeaderText="Date" DataField="AdjustmentDate"  HeaderStyle-CssClass="gridviewheader"  SortExpression="AdjustmentDate" ItemStyle-CssClass="gridviewitem"/>--%>
                                <asp:TemplateField  HeaderText="Date" SortExpression="AdjustmentDate" >
<%--                                        <HeaderStyle CssClass="gridviewheader" />--%>
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Left" Width="70"/>
                                        <ItemTemplate>
                                            <%#string.Format("{0:dd/MM/yyyy}",Eval("AdjustmentDate"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField  HeaderText="JobNo" DataField="JobNo"  SortExpression="JobNo" ItemStyle-CssClass="gridviewitem" ItemStyle-Width="110px"/>
                                <asp:BoundField  HeaderText="Tran Type" DataField="DorC" SortExpression="Dorc" ItemStyle-CssClass="gridviewitem"/>
                                <asp:BoundField  HeaderText="Dr Amount" DataField="DrAmount" ItemStyle-CssClass="gridviewitem" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField  HeaderText="Cr Amount" DataField="CrAmount" ItemStyle-CssClass="gridviewitem" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                               
                                    <asp:TemplateField ItemStyle-Width="8%">
<%--                                        <HeaderStyle CssClass="gridviewheader" />--%>
                                        <ItemStyle CssClass="gridviewitem"  HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Images/edit.png" CommandName="Edit"
                                                Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="8%">
<%--                                        <HeaderStyle CssClass="gridviewheader" />--%>
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>                                     
                                       
                                            <asp:ImageButton ID="btnRemove"  runat="server" OnClientClick="return Confirm()" CommandArgument='<%# EMS.Utilities.GeneralFunctions.EncryptQueryString(Eval("pk_AdvAdjID").ToString()) %>' CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                Height="16" Width="16"/>
                                               
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
