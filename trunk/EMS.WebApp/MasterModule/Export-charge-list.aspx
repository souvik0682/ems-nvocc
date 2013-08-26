<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Export-charge-list.aspx.cs" Inherits="EMS.WebApp.MasterModule.Export_charge_list" %>

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
        EXPORT CHARGE ENTRY</div>
    <center>
        <div style="width: 90%;">
            <fieldset style="width: 100%;">
                <legend>Search Export Charge</legend>
                <table border="0">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtLine" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters"
                                FilterMode="ValidChars" ValidChars=" " TargetControlID="txtLine">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtLine"
                                WatermarkText="LINE">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLocation" runat="server" Width="200">
                                <asp:ListItem Selected="True" Text="Select Location"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtEfectDate" runat="server" CssClass="watermark" ForeColor="#747862"
                                onkeyup="return false;"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtEfectDate"
                                TargetControlID="txtEfectDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtEfectDate"
                                WatermarkText="On Date">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtService" runat="server" CssClass="watermark" ForeColor="#747862"
                                onkeyup="return false;"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtService"
                                WatermarkText="Service">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" />
                            <asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="button" Width="100px" />
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
                <legend>Export Charge List</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add New Charge" Width="130px" 
                        onclick="btnAdd_Click" />
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
                            <asp:GridView ID="gvwCharge" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" Width="100%">
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Charge(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="25%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHName" runat="server" CommandName="Sort" CommandArgument="Name"
                                                Text="Charge Title"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblChargeTitle" runat="server" Style="text-transform: uppercase;"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHIEc" runat="server" CommandName="Sort" CommandArgument="CB"
                                                Text="Charge Basis"></asp:LinkButton></HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLine" runat="server" CommandName="Sort" CommandArgument="Line"
                                                Text="Line"></asp:LinkButton></HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLocation" runat="server" CommandName="Sort" CommandArgument="Location"
                                                Text="Location"></asp:LinkButton></HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHDate" runat="server" CommandName="Sort" CommandArgument="Date"
                                                Text="Up To Date"></asp:LinkButton></HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="13%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHType" runat="server" CommandName="Sort" CommandArgument="Type"
                                                Text="Charge Type"></asp:LinkButton></HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service">
                                        <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHType" runat="server" CommandName="Sort" CommandArgument="Type"
                                                Text="Service"></asp:LinkButton></HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EDI">
                                        <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHType" runat="server" CommandName="Sort" CommandArgument="Type"
                                                Text="EDI"></asp:LinkButton></HeaderTemplate>
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
                        <asp:ListItem Text="IMPORT" Value="I"></asp:ListItem>
                        <asp:ListItem Text="EXPORT" Value="E"></asp:ListItem>
                        <asp:ListItem Text="GENERAL" Value="G"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </fieldset>
        </div>
    </center>
</asp:Content>
