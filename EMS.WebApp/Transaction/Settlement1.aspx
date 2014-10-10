<%@ Page Title="Settlement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Settlement1.aspx.cs" Inherits="EMS.WebApp.Transaction.Settlement1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        MANAGE SETTLEMENT</div>
    <center>
        <div style="width: 80%;">
            <fieldset style="width: 100%;">
                <legend>Search Settlement</legend>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSettlementNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeSettlementNo" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>" TargetControlID="txtSettlementNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbwmeSettlementNo" runat="server" TargetControlID="txtSettlementNo"
                                WatermarkText="Settlement No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEdgeBLNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeEdgeBLNo" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>" TargetControlID="txtEdgeBLNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbwmeEdgeBLNo" runat="server" TargetControlID="txtEdgeBLNo"
                                WatermarkText="B/L No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>

                         <td>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeLocation" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>" TargetControlID="txtLocation">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbwmeLocation" runat="server" TargetControlID="txtLocation"
                                WatermarkText="Location">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLine" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeLine" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>" TargetControlID="txtLine">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbwmeLine" runat="server" TargetControlID="txtLine"
                                WatermarkText="Line">
                            </cc1:TextBoxWatermarkExtender>
                        </td>

                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="80px"
                                OnClick="btnSearch_Click" />
                            <asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="button" Width="80px"
                                OnClick="btnReset_Click" />
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
                <legend>Settlement List</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add New" Width="80px" OnClick="btnAdd_Click" />
                </div>
                <div style="height: 30px;">
                    &nbsp; <span class="errormessage" style="display: none;">* Indicates Inactive Container Transhipment(s)</span>
                </div>
                <br />
                <div>
                    <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gvwContainerTran" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwContainerTran_PageIndexChanging"
                                OnRowDataBound="gvwContainerTran_RowDataBound" OnRowCommand="gvwContainerTran_RowCommand"
                                Width="100%" ShowHeaderWhenEmpty="true">
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Transaction(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="6%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLocation" runat="server" CommandName="Sort" CommandArgument="LOCATION"
                                                Text="Location"></asp:LinkButton></HeaderTemplate>
                                        <%--<ItemTemplate>
                                            <%# Eval("ContainerNo")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="6%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLine" runat="server" CommandName="Sort" CommandArgument="LINE"
                                                Text="Line"></asp:LinkButton></HeaderTemplate>
                                        <%--<ItemTemplate>
                                            <%# Eval("Status")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="16%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkSettlementNo" runat="server" CommandName="Sort" CommandArgument="SettlementNo"
                                                Text="SettlementNo No"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Date")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="16%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkBLNO" runat="server" CommandName="Sort" CommandArgument="ImpLineBLNO"
                                                Text="BL No"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Vessel")%></ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkSettlementDate" runat="server" CommandName="Sort" CommandArgument="SettlementDate"
                                                Text="Settlement Date"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Voyage")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="7%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkType" runat="server" CommandName="Sort" CommandArgument="PorR"
                                                Text="Type"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("LandingDate")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader_num" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Right"/>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkSettlementAmt" runat="server" CommandName="Sort" CommandArgument="SettlementAmount" 
                                                Text="Amount"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Voyage")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png"
                                                Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

<%--
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="display: none;">
                    <asp:DropDownList ID="ddlIEC" runat="server" Width="255">
                        <asp:ListItem Text="Import" Value="I"></asp:ListItem>
                        <asp:ListItem Text="Export" Value="E"></asp:ListItem>
                        <asp:ListItem Text="General" Value="G"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </fieldset>
        </div>
    </center>
</asp:Content>
