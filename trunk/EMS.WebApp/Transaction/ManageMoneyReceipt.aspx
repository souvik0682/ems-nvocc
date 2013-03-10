<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageMoneyReceipt.aspx.cs"   
    Inherits="EMS.WebApp.Transaction.ManageMoneyReceipt" MasterPageFile="~/Site.Master"
    Title=":: Liner :: Manage Money Receipt" %>

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
    <div id="headercaption">
        MANAGE MONEY RECEIPT</div>
    <center>
        <div style="width: 850px;">
            <fieldset style="width: 100%;">
                <legend>Search User</legend>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="txtWMEInvoiceNo" runat="server" TargetControlID="txtInvoiceNo"
                                WatermarkText="Type Invoice No" WatermarkCssClass="watermark">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBLNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="txtWMEBLNo" runat="server" TargetControlID="txtBLNo"
                                WatermarkText="Type BL No" WatermarkCssClass="watermark">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:TextBox ID="txtMoneyReceipt" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="txtWMEMoneyReceipt" runat="server" TargetControlID="txtMoneyReceipt"
                                WatermarkText="Type Money Receipt" WatermarkCssClass="watermark">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDwnExportImport" runat="server" ForeColor="#747862">
                                <asp:ListItem>All</asp:ListItem>
                                <asp:ListItem>Export</asp:ListItem>
                                <asp:ListItem>Import</asp:ListItem>
                            </asp:DropDownList>
                            
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click" />&nbsp;<asp:Button
                                ID="btnReset" runat="server" Text="Reset" CssClass="button" Width="100px" OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
               <asp:UpdateProgress ID="uProgressMoneyReceipts" runat="server" AssociatedUpdatePanelID="upMoneyReceipts">
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
            <legend>M/R List</legend>
            <div style="float:right;padding-bottom:5px;">                
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="30" Value="30" />
                    <asp:ListItem Text="50" Value="50" />
                    <asp:ListItem Text="100" Value="100" />
                </asp:DropDownList>&nbsp;&nbsp;            
                <asp:Button ID="btnAdd" runat="server" Text="Add Money Receipts" Width="130px" OnClick="btnAdd_Click" />
            </div>
            <div>
                <span class="errormessage">* Indicates Inactive Money receipt(s)</span>
            </div><br />            
            <div>
                <asp:UpdatePanel ID="upMoneyReceipts" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gvwMoneyReceipts" runat="server" AutoGenerateColumns="false" 
                            AllowPaging="true" BorderStyle="None" BorderWidth="0" Width="100%" 
                            onrowdatabound="gvwMoneyReceipt_RowDataBound" OnRowCommand="gvwMoneyReceipt_RowCommand">
                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                        <PagerStyle CssClass="gridviewpager" />
                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                        <EmptyDataTemplate>No Manage receipt(s) Found</EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="15%" />    
                                <HeaderTemplate><asp:LinkButton ID="lnkHMRNo" runat="server" CommandName="Sort" CommandArgument="MRNo" Text="M/R No"></asp:LinkButton></HeaderTemplate>                                
                                <ItemTemplate>
                                    <asp:Label ID="lblMRNo" runat="server"></asp:Label><asp:Label ID="lblInActive" runat="server" CssClass="errormessage" Font-Bold="true" Text=" *"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="13%" />
                                <HeaderTemplate><asp:LinkButton ID="lnkHMRDate" runat="server" CommandName="Sort" CommandArgument="MRDate" Text="Date"></asp:LinkButton></HeaderTemplate>                                    
                                <ItemTemplate>
                                    <asp:Label ID="lblMRDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="15%" />           
                                <HeaderTemplate><asp:LinkButton ID="lnkHMRLocation" runat="server" CommandName="Sort" CommandArgument="MRLocation" Text="Location"></asp:LinkButton></HeaderTemplate>                         
                                <ItemTemplate>
                                    <asp:Label ID="lblMRLocation" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="15%" />   
                                <HeaderTemplate><asp:LinkButton ID="lnkHBLNo" runat="server" CommandName="Sort" CommandArgument="BLNo" Text="B/L No"></asp:LinkButton></HeaderTemplate>                                 
                                 <ItemTemplate>
                                    <asp:Label ID="lblBLNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="13%" />       
                                <HeaderTemplate><asp:LinkButton ID="lnkHMRAmount" runat="server" CommandName="Sort" CommandArgument="MRAmount" Text="Amount"></asp:LinkButton></HeaderTemplate>                             
                                 <ItemTemplate>
                                    <asp:Label ID="lblMRAmount" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="16" Width="16"  />
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
