<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainLineVoyage.aspx.cs" Inherits="EMS.WebApp.MasterModule.MainLineVoyage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
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
        MANAGE MAIN LINE VOYAGE</div>
    <center>
    <div style="width:750px;">        
        <fieldset style="width:100%;">
            <legend>Search Voyage</legend>
            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                   <td style="width:10%">
                        <asp:TextBox ID="txtVesselName" runat="server" CssClass="watermark"   ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMEName" runat="server"  TargetControlID="txtVesselName" WatermarkText="Type Vessel Name" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    
                    <td style="width:10%">
                        <asp:TextBox ID="txtVoyageNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtVoyageNo" WatermarkText="Type MLVoyage No." WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <%--  <td style="width:10%">
                        <asp:TextBox ID="txtIGMNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtIGMNo" WatermarkText="Type IGM No." WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>--%>
                    <td style="width:30%">
                       <asp:DropDownList ID="ddlVoyageType" runat="server" Width="100%">
                        <asp:ListItem Value="I">Import</asp:ListItem>
                        <asp:ListItem Value="E">Export</asp:ListItem>
                        <asp:ListItem Value="A">All</asp:ListItem>
                       </asp:DropDownList>
                    </td>

                    <td style="width:40%; text-align:right"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="70px" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="button" Width="70px" onclick="btnRefresh_Click"  /></td>
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
        <fieldset id="fsList" runat="server" style="width:100%;min-height:100px;">
            <legend>Voyage List</legend>
            <div style="float:right;padding-bottom:5px;margin-top: -10px">
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>&nbsp;&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="Add New Voyage" Width="130px" OnClick="btnAdd_Click" />
            </div>
          <br />            
            <div>
                <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                    <asp:Label runat="server" ID="lblErrorMsg" Text=""></asp:Label>
                        <asp:GridView ID="gvwLoc" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwLoc_PageIndexChanging" AllowSorting="true" onsorting="gvwLoc_Sorting"
                OnRowDataBound="gvwLoc_RowDataBound" OnRowCommand="gvwLoc_RowCommand" Width="100%">
                <pagersettings mode="NumericFirstLast" position="TopAndBottom" />
                <pagerstyle cssclass="gridviewpager" />
                <emptydatarowstyle cssclass="gridviewemptydatarow" />
                <emptydatatemplate>No Voyage(s) Found</emptydatatemplate>
                <columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="4%" />                                    
                                </asp:TemplateField>
                              
                            
                                <asp:TemplateField HeaderText="id" Visible="false">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="2%" />                                       
                                </asp:TemplateField>

                               

                                <asp:TemplateField HeaderText="ML Voyage No" SortExpression="MLVoyageNo">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />      
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Vessel Name" SortExpression="VesselName">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />                                       
                                </asp:TemplateField>

                              

                            <%--    <asp:TemplateField HeaderText="Port" SortExpression="LastPort">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />                                       
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Activity Date" >
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="18%" />                                       
                                </asp:TemplateField>--%>

                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="7%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="16" Width="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="7%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </columns>
            </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </fieldset>
    </div>
    </center>
</asp:Content>
