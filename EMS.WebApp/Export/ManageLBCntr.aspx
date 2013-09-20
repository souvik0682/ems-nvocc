<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageLBCntr.aspx.cs" Inherits="EMS.WebApp.Export.ManageLBCntr" %>

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
        MANAGE LEFT BEHIND CONTAINER</div>
    <center>
        <div style="width: 80%;">
            <fieldset style="width: 100%;">
                <legend>Search Container</legend>
                <table>
                    <tr>
                         <td>
                            <asp:TextBox ID="txtContainerNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtContainerNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtContainerNo"
                                WatermarkText="Container No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBookingNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtBookingNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtBookingNo"
                                WatermarkText="Booking No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBLNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtBLNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtBLNo"
                                WatermarkText="Export B/L No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtRefBookingNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtRefBookingNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtRefBookingNo"
                                WatermarkText="Ref. Booking No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>

                        <td>
                            <asp:TextBox ID="txtPortName" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtPortName">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtPortName"
                                WatermarkText="Port Loading">
                            </cc1:TextBoxWatermarkExtender>
                        </td>

                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="80px"
                                OnClick="btnSearch_Click" />
                            <asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="button" Width="80px"
                                OnClick="btnRefresh_Click" />
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
                <legend>Container Left Behind List</legend>
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
                            <asp:GridView ID="gvwContainerTran" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwContainerTran_PageIndexChanging"
                                OnRowDataBound="gvwContainerTran_RowDataBound" OnRowCommand="gvwContainerTran_RowCommand"
                                Width="100%">
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Transaction(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHContNo" runat="server" CommandName="Sort" CommandArgument="ContainerNo"
                                                Text="Container No"></asp:LinkButton></HeaderTemplate>
                                        <%--<ItemTemplate>
                                            <%# Eval("ContainerNo")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="13%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHBooking" runat="server" CommandName="Sort" CommandArgument="BookingNo"
                                                Text="Booking No."></asp:LinkButton></HeaderTemplate>
                                        <%--<ItemTemplate>
                                            <%# Eval("Status")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHILBDate" runat="server" CommandName="Sort" CommandArgument="LBDate"
                                                Text="Date"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Date")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLocation" runat="server" CommandName="Sort" CommandArgument="Location"
                                                Text="Location"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Vessel")%></ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLine" runat="server" CommandName="Sort" CommandArgument="Line"
                                                Text="Line"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Voyage")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="9%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHRefBookingNo" runat="server" CommandName="Sort" CommandArgument="LandingDate"
                                                Text="Ref.Booking"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("LandingDate")%>
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
