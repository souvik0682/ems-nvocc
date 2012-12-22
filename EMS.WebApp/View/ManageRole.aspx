<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRole.aspx.cs" Inherits="EMS.WebApp.View.ManageRole"  MasterPageFile="~/Site.Master" Title=":: EMS :: Manage Role" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose"><img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage"></div>
        </div>
    </div>
    <div id="headercaption">MANAGE ROLE</div>
    <center>
    <div style="width:475px;">        
        <fieldset style="width:100%;">
            <legend>Search Role</legend>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtRole" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMERole" runat="server" TargetControlID="txtRole" WatermarkText="Type Role" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" OnClick="btnSearch_Click" />&nbsp;<asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" Width="100px" OnClick="btnReset_Click" /></td>
                </tr>
            </table>              
        </fieldset>
        <asp:UpdateProgress ID="uProgressRole" runat="server" AssociatedUpdatePanelID="upRole">
            <ProgressTemplate>
                <div class="progress">
                    <div id="image"><img src="../../Images/PleaseWait.gif" alt="" /></div>
                    <div id="text">Please Wait...</div>
                </div>
            </ProgressTemplate>        
        </asp:UpdateProgress>
        <fieldset id="fsList" runat="server" style="width:100%;min-height:100px;">
            <legend>Role List</legend>
            <div style="float:right;padding-bottom:5px;">
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>&nbsp;&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="Add New Role" Width="130px" OnClick="btnAdd_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upRole" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gvwRole" runat="server" AutoGenerateColumns="false" AllowPaging="true" BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwRole_PageIndexChanging" OnRowDataBound="gvwRole_RowDataBound" OnRowCommand="gvwRole_RowCommand" Width="100%">
                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                            <PagerStyle CssClass="gridviewpager" />
                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                            <EmptyDataTemplate>No Role(s) Found</EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="gridviewitem" Width="10%" />                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role">
                                    <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="gridviewitem" Width="65%" />                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="gridviewitem" Width="15%" />                                    
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkStatus" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="16" Width="16" />
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