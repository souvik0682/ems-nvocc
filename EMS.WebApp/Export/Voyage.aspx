<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Voyage.aspx.cs" Inherits="EMS.WebApp.Export.Voyage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" />
            </div>
        </div>
    </div>
    <div id="headercaption">
        EXPORT VOYAGE</div>
    <center>
        <div style="width: 850px;">
            <fieldset style="width: 100%;" >
                <legend>Search </legend>
                <table>
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="txtVesselName" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="ValidChars" ValidChars=" " TargetControlID="txtVesselName">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtVesselName"
                                WatermarkText="Vessel Number">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVoyageNo" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="InvalidChars" ValidChars=" " TargetControlID="txtVoyageNo">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtVoyageNo"
                                WatermarkText="Voyage No">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="ValidChars" ValidChars=" " TargetControlID="txtLocation">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtLocation"
                                WatermarkText="Location">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        
                        <td align="center">
                            <asp:TextBox ID="txtTerminal" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="ValidChars" ValidChars=" " TargetControlID="txtTerminal">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtTerminal"
                                WatermarkText="Terminal">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" ForeColor="#747862" AutoPostBack="true" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Text="Open" Value="O"></asp:ListItem>
                            <asp:ListItem Text="Closed" Value="C"></asp:ListItem>
                            <asp:ListItem Text="All" Value="A"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="3">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                Width="70px" onclick="btnSearch_Click" />

                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" 
                                Width="70px" onclick="btnReset_Click" />
                        </td>
                    </tr>
                   <%-- <tr>
                        <td colspan="3">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                Width="100px" onclick="btnSearch_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" 
                                Width="100px" onclick="btnReset_Click" />
                        </td>
                    </tr>--%>
                </table>
            </fieldset>
            <asp:UpdateProgress ID="uProgressLoc" runat="server" AssociatedUpdatePanelID="upVoyage">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../../Images/PleaseWait.gif" alt="" />
                        </div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <fieldset id="fsList" runat="server" style="width: 100%; min-height: 100px;">
                <legend>Voyage List</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" 
                        AutoPostBack="true" onselectedindexchanged="ddlPaging_SelectedIndexChanged"
                        >
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add New Voyage" Width="150" 
                        onclick="btnAdd_Click"/>
                </div>
                <div>
                    <span class="errormessage">&nbsp;</span>
                </div>
                <br />
                <div>
                    <asp:UpdatePanel ID="upVoyage" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gvVoyage" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" Width="100%" 
                                onpageindexchanging="gvVoyage_PageIndexChanging" 
                                onrowcommand="gvVoyage_RowCommand" onrowdatabound="gvVoyage_RowDataBound">
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Record(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="3%" />
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="6%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHVessel" runat="server" CommandName="Sort" CommandArgument="VesselName"
                                                Text="Vessel Name"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="6%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHVoyageNo" runat="server" CommandName="Sort" CommandArgument="VoyageNo"
                                                Text="Voyage No"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoyageNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="6%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHPOL" runat="server" CommandName="Sort" CommandArgument="POL"
                                                Text="Loading Port"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPOL" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="6%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHPOD" runat="server" CommandName="Sort" CommandArgument="POD"
                                                Text="Disch.Port"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPOD" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="6%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHTerminal" runat="server" CommandName="Sort" CommandArgument="TerminalName"
                                                Text="Terminal Name"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTerminalName" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--

                                    <asp:TemplateField HeaderText="VESSEL NAME">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="18%" />
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="VOYAGE NO">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                    </asp:TemplateField>
 
                                    <asp:TemplateField HeaderText="POL">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="POD">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TERMINAL">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                    </asp:TemplateField>--%>                                
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
