<%@ Page Title="Container Transhipment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ContainerTranshipment.aspx.cs" Inherits="EMS.WebApp.Export.ContainerTranshipment" %>

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
        MANAGE CONTAINER TRANSHIPMENT</div>
    <center>
        <div style="width: 80%;">
            <fieldset style="width: 100%;">
                <legend>Search Container</legend>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtBookingNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeBookingNo" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtBookingNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbwmeBookingNo" runat="server" TargetControlID="txtBookingNo"
                                WatermarkText="Booking No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEdgeBLNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeEdgeBLNo" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtEdgeBLNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbwmeEdgeBLNo" runat="server" TargetControlID="txtEdgeBLNo"
                                WatermarkText="Edge B/L No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtRefBookingNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeRefBookingNo" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtRefBookingNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbwmeRefBookingNo" runat="server" TargetControlID="txtRefBookingNo"
                                WatermarkText="Ref. Booking No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContainerNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeContainerNo" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtContainerNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbwmeContainerNo" runat="server" TargetControlID="txtContainerNo"
                                WatermarkText="Container No">
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
                <legend>Container Transhipment List</legend>
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
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLocation" runat="server" CommandName="Sort" CommandArgument="LOCATION"
                                                Text="Location"></asp:LinkButton></HeaderTemplate>
                                        <%--<ItemTemplate>
                                            <%# Eval("ContainerNo")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLine" runat="server" CommandName="Sort" CommandArgument="LINE"
                                                Text="Line"></asp:LinkButton></HeaderTemplate>
                                        <%--<ItemTemplate>
                                            <%# Eval("Status")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHBLNo" runat="server" CommandName="Sort" CommandArgument="ExpBLNo"
                                                Text="B/L No"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Date")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHDate" runat="server" CommandName="Sort" CommandArgument="ExpBLDate"
                                                Text="Date"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Vessel")%></ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHContainerNo" runat="server" CommandName="Sort" CommandArgument="CONTAINERNO"
                                                Text="Container No"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Voyage")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="9%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLastPort" runat="server" CommandName="Sort" CommandArgument="PortName"
                                                Text="Last Port"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("LandingDate")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHArrival" runat="server" CommandName="Sort" CommandArgument="ActualArrival"
                                                Text="Arrival"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Voyage")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="9%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHDeparture" runat="server" CommandName="Sort" CommandArgument="ActualDeperture"
                                                Text="Departure"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("LandingDate")%>
                                        </ItemTemplate>--%>
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
