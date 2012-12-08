<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageHaulageCharge.aspx.cs" Inherits="EMS.WebApp.View.ManageHaulageCharge" MasterPageFile="~/Site.Master" Title=":: EMS :: Manage Haulage Charge" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage">
            </div>
        </div>
    </div>
    <div id="headercaption">MANAGE HAULAGE CHARGE</div>
    <center>
    <div style="width:700px;">        
        <fieldset style="width:100%;">
            <legend>Search Haulage Charge</legend>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtLocFrom" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMELocFrom" runat="server" TargetControlID="txtLocFrom" WatermarkText="Type Location From" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLocTo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMELocTo" runat="server" TargetControlID="txtLocTo" WatermarkText="Type Location To" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSize" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMESize" runat="server" TargetControlID="txtSize" WatermarkText="Type Container Size" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" OnClick="btnSearch_Click" /></td>
                </tr>
            </table>              
        </fieldset>
        <asp:UpdateProgress ID="uProgressHaulage" runat="server" AssociatedUpdatePanelID="upHaulage">
            <ProgressTemplate>
                <div class="progress">
                    <div id="image">
                        <img src="../../Images/PleaseWait.gif" alt="" /></div>
                    <div id="text">
                        Please Wait...</div>
                </div>
            </ProgressTemplate>        
        </asp:UpdateProgress>
        <fieldset id="fsList" runat="server" style="width:100%;min-height:100px;">
            <legend>Haulage Charge List</legend>
            <div style="float:right;padding-bottom:5px;">
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>&nbsp;&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="Add New Location" Width="130px" OnClick="btnAdd_Click" />
            </div><br />            
            <div>
                <asp:UpdatePanel ID="upHaulage" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gvwHaulage" runat="server" AutoGenerateColumns="false" AllowPaging="true" BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwHaulage_PageIndexChanging" OnRowDataBound="gvwHaulage_RowDataBound" OnRowCommand="gvwHaulage_RowCommand" Width="100%">
                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                            <PagerStyle CssClass="gridviewpager" />
                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                            <EmptyDataTemplate>No Charge(s) Found</EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location From">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location To">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Container Size">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="10%" />    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight From">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="11%" />    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight To">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="11%" />    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="13%" />    
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="16" Width="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
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