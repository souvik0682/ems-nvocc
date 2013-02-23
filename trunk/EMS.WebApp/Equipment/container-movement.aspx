<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="container-movement.aspx.cs" Inherits="EMS.WebApp.Equipment.container_movement" %>

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
        MANAGE CONTAINER MOVEMENT</div>
    <center>
        <div style="width: 80%;">
            <fieldset style="width: 100%;">
                <legend>Search Container</legend>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtContainerNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtContainerNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtContainerNo"
                                WatermarkText="Container No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVessel" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtVessel">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtVessel"
                                WatermarkText="Vessel">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVoyage" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtVoyage">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtVoyage"
                                WatermarkText="Voyage">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtStatus" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>-" TargetControlID="txtStatus">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtStatus"
                                WatermarkText="Status">
                            </cc1:TextBoxWatermarkExtender>
                        </td>

                        <td>
                            <asp:TextBox ID="txtTranCode" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterMode="InvalidChars"
                                InvalidChars="<>" TargetControlID="txtTranCode">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtTranCode"
                                WatermarkText="Transaction Code">
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
                <legend>Container Movement List</legend>
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
                                        <ItemStyle CssClass="gridviewitem" Width="6%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHStatus" runat="server" CommandName="Sort" CommandArgument="Status"
                                                Text="Status"></asp:LinkButton></HeaderTemplate>
                                        <%--<ItemTemplate>
                                            <%# Eval("Status")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHIDate" runat="server" CommandName="Sort" CommandArgument="Date"
                                                Text="Date"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Date")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHVessel" runat="server" CommandName="Sort" CommandArgument="Vessel"
                                                Text="Vessel"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Vessel")%></ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHVoyage" runat="server" CommandName="Sort" CommandArgument="Voyage"
                                                Text="Voyage"></asp:LinkButton></HeaderTemplate>
                                        <%--  <ItemTemplate>
                                            <%# Eval("Voyage")%>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="9%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLandingDate" runat="server" CommandName="Sort" CommandArgument="LandingDate"
                                                Text="Landing Date"></asp:LinkButton></HeaderTemplate>
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
