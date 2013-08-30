<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageAgent.aspx.cs" MasterPageFile="~/Site.Master" Inherits="EMS.WebApp.Export.ManageAgent" %>
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
        MANAGE EXPORT AGENT</div>
    <center>
        <div style="width: 880px; ">
            <fieldset style="width: 100%;">
                <legend>Search</legend>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtPort" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters"
                                FilterMode="ValidChars" ValidChars=" " TargetControlID="txtPort">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPort"
                                WatermarkText="FPOD">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLine" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="ValidChars" ValidChars=" " TargetControlID="txtLine">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtLine"
                                WatermarkText="Line">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAgent" runat="server" CssClass="watermark" ForeColor="#747862" ></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="ValidChars" ValidChars=" " TargetControlID="txtAgent">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtAgent"
                                WatermarkText="Agent Name">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="70px"
                                OnClick="btnSearch_Click" />
                            <asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="button" Width="70px"
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
                <legend>Agent List</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add Agent" Width="150px"
                        OnClick="btnAdd_Click" />
                </div>
                <div>
                    <span class="errormessage">&nbsp;</span>
                </div>
                <br />
                <div>
                    <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gvwAgent" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwAgent_PageIndexChanging"
                                OnRowDataBound="gvwAgent_RowDataBound" OnRowCommand="gvwAgent_RowCommand"
                                Width="100%">
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Record(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="4%" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="6%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHLine" runat="server" CommandName="Sort" CommandArgument="LineName"
                                                Text="Line"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLineName" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHPort" runat="server" CommandName="Sort" CommandArgument="PortName"
                                                Text="FPOD"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPortName" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="20%"  />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHAgentName" runat="server" CommandName="Sort" CommandArgument="AgentName"
                                                Text="Agent Name"></asp:LinkButton></HeaderTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="18%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHPhone" runat="server" CommandName="Sort" CommandArgument="Phone"
                                                Text="Phone"></asp:LinkButton></HeaderTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="18%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHContactPerson" runat="server" CommandName="Sort" CommandArgument="ContactPerson"
                                                Text="Contact Person"></asp:LinkButton></HeaderTemplate>
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
            </fieldset>
        </div>
    </center>
</asp:Content>
