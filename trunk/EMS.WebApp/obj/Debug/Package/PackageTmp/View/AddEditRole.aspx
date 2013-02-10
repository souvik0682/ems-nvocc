<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditRole.aspx.cs" Inherits="EMS.WebApp.View.AddEditRole"
    MasterPageFile="~/Site.Master" Title=":: Liner :: Add / Edit Role" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        ADD / EDIT ROLE</div>
    <center>
        <fieldset style="width: 600px;">
            <legend>Add / Edit Role</legend>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width:60px;">
                            Role:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRole" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRole" runat="server" CssClass="errormessage" ControlToValidate="txtRole" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-top:10px;">
                            <cc1:TabContainer ID="tcPP" runat="server" ActiveTabIndex="0">
                                <cc1:TabPanel ID="tpMst" runat="server">
                                    <HeaderTemplate>
                                        Master</HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:GridView ID="gvwMst" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            BorderStyle="None" BorderWidth="0" OnRowDataBound="gvwMst_RowDataBound" Width="100%">
                                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                            <PagerStyle CssClass="gridviewpager" />
                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                            <EmptyDataTemplate>
                                                No Page(s) Found</EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSlNo" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnAccessId" runat="server" />
                                                        <asp:HiddenField ID="hdnMenuId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pages">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="50%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Add">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAdd" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEdit" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDel" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkView" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="tpImp" runat="server">
                                    <HeaderTemplate>
                                        Import</HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:GridView ID="gvwImp" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            BorderStyle="None" BorderWidth="0" OnRowDataBound="gvwImp_RowDataBound" Width="100%">
                                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                            <PagerStyle CssClass="gridviewpager" />
                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                            <EmptyDataTemplate>
                                                No Page(s) Found</EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSlNo" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnAccessId" runat="server" />
                                                        <asp:HiddenField ID="hdnMenuId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pages">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="50%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Add">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAdd" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEdit" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDel" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkView" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="tpFin" runat="server">
                                    <HeaderTemplate>
                                        Finance</HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:GridView ID="gvwFin" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            BorderStyle="None" BorderWidth="0" OnRowDataBound="gvwFin_RowDataBound" Width="100%">
                                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                            <PagerStyle CssClass="gridviewpager" />
                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                            <EmptyDataTemplate>
                                                No Page(s) Found</EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSlNo" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnAccessId" runat="server" />
                                                        <asp:HiddenField ID="hdnMenuId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pages">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="50%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Add">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAdd" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEdit" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDel" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkView" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="tpLog" runat="server">
                                    <HeaderTemplate>
                                        Logistic</HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:GridView ID="gvwLog" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            BorderStyle="None" BorderWidth="0" OnRowDataBound="gvwLog_RowDataBound" Width="100%">
                                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                            <PagerStyle CssClass="gridviewpager" />
                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                            <EmptyDataTemplate>
                                                No Page(s) Found</EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSlNo" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnAccessId" runat="server" />
                                                        <asp:HiddenField ID="hdnMenuId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pages">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="50%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Add">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAdd" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEdit" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDel" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkView" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="tpExp" runat="server">
                                    <HeaderTemplate>
                                        Export</HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:GridView ID="gvwExp" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                            BorderStyle="None" BorderWidth="0" OnRowDataBound="gvwExp_RowDataBound" Width="100%">
                                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                            <PagerStyle CssClass="gridviewpager" />
                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                            <EmptyDataTemplate>
                                                No Page(s) Found</EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSlNo" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnAccessId" runat="server" />
                                                        <asp:HiddenField ID="hdnMenuId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pages">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="50%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Add">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAdd" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEdit" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDel" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <HeaderStyle CssClass="gridviewheader" />
                                                    <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkView" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-top:10px;">
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="18"
                                OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button"
                                    Text="Back" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </center>
</asp:Content>
