<%@ Page Title=":: Liner :: Delivery Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DOList.aspx.cs" Inherits="EMS.WebApp.Export.DOList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage"></div>
        </div>
    </div>
    <asp:UpdateProgress ID="uProgressList" runat="server" AssociatedUpdatePanelID="upList">
        <ProgressTemplate>
            <div class="progress">
                <div id="image"><img src="../../Images/PleaseWait.gif" alt="" /></div>
                <div id="text">Please Wait...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="headercaption">DELIVERY ORDER</div>
    <center>
        <div style="width: 850px; ">
            <fieldset style="width: 100%;">
                <legend>Search</legend>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtBookingNo" runat="server" CssClass="watermark" ForeColor="#747862"  MaxLength="50" Width="250"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeBookingNo" runat="server" TargetControlID="txtBookingNo" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" FilterMode="InvalidChars" ValidChars=" "></cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbweBookingNo" runat="server" TargetControlID="txtBookingNo" WatermarkText="Booking No"></cc1:TextBoxWatermarkExtender>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="watermark" ForeColor="#747862" MaxLength="50" Width="250"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeLocation" runat="server" TargetControlID="txtLocation" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" FilterMode="InvalidChars" ValidChars=" "></cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbweLocation" runat="server" TargetControlID="txtLocation" WatermarkText="Booking No"></cc1:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDONo" runat="server" CssClass="watermark" ForeColor="#747862" MaxLength="50" Width="250"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeDONo" runat="server" TargetControlID="txtDONo" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" FilterMode="InvalidChars" ValidChars=" "></cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbweDONo" runat="server" TargetControlID="txtDONo" WatermarkText="Booking No"></cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLine" runat="server" CssClass="watermark" ForeColor="#747862" MaxLength="50" Width="250"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeLine" runat="server" TargetControlID="txtLine" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" FilterMode="InvalidChars" ValidChars=" "></cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="tbweLine" runat="server" TargetControlID="txtLine" WatermarkText="Booking No"></cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" onclick="btnSearch_Click"/>
                            &nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" Width="100px" onclick="btnReset_Click"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset id="fsList" runat="server" style="width: 100%; min-height: 100px;">
                <legend>Delivery Order</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add New DO" Width="150" OnClick="btnAdd_Click" />
                </div>
                <div>
                    <span class="errormessage">&nbsp;</span>
                </div>
                <br />
                <div>
                    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gvwList" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" Width="100%" 
                                OnPageIndexChanging="gvwList_PageIndexChanging" 
                                OnRowCommand="gvwList_RowCommand" OnRowDataBound="gvwList_RowDataBound">
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>No Record(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkLoc" runat="server" CommandName="Sort" CommandArgument="LocationName" Text="Location"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkLine" runat="server" CommandName="Sort" CommandArgument="LineName" Text="Line"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Booking No">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />                                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DO No">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />                                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DO Date">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="11%" />                                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Containers">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />                                    
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditData" ImageUrl="~/Images/edit.png" Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnRemove" runat="server" CommandName="RemoveData" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnPrint" runat="server" CommandName="PrintData" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
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
