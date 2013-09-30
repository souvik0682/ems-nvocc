<%@ Page Title=":: Liner :: Add / Edit Delivery Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DOEdit.aspx.cs" Inherits="EMS.WebApp.Export.DOEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">ADD / EDIT DELIVERY ORDER</div>
    <center>
        <div style="width: 800px;">
            <fieldset style="width:100%;">
                <legend>General</legend>
                <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                    <tr>
                        <td style="width: 18%;padding-bottom:5px;">Booking No<span class="errormessage">*</span></td>
                        <td style="width: 32%;padding-bottom:5px;"><asp:DropDownList ID="ddlBooking" runat="server" AutoPostBack="true" CssClass="dropdownlist" OnSelectedIndexChanged="ddlBooking_SelectedIndexChanged"></asp:DropDownList></td>
                        <td style="width: 18%;padding-bottom:5px;">Empty Yard<span class="errormessage">*</span></td>
                        <td style="width: 32%;padding-bottom:5px;"><asp:DropDownList ID="ddlYard" runat="server" AutoPostBack="true" CssClass="dropdownlist" OnSelectedIndexChanged="ddlYard_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>DO No<span class="errormessage">*</span></td>
                        <td><asp:TextBox ID="txtDoNo" runat="server"></asp:TextBox></td>
                        <td>DO Date<span class="errormessage">*</span></td>
                        <td>
                            <asp:TextBox ID="txtDoDate" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="80px" TabIndex="6"></asp:TextBox>
                            <cc1:CalendarExtender ID="cbeDoDate" TargetControlID="txtDoDate" runat="server" Format="dd/MM/yyyy" Enabled="True" />
                            <br />
                            <asp:RequiredFieldValidator ID="rfvDoDate" runat="server" ControlToValidate="txtDoDate" ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="width:100%;">
                <legend>Container Detail</legend>
                <asp:GridView ID="gvwList" runat="server" AutoGenerateColumns="false" AllowPaging="true" BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvwList_RowDataBound">
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle CssClass="gridviewpager" />
                    <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                    <EmptyDataTemplate>No Record(s) Found</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Available Units">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Required Units">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
            <div style="text-align:left;">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnPrint" runat="server" Text="Save & Print" ValidationGroup="Save" OnClick="btnPrint_Click" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" onclick="btnBack_Click" />
            </div>
        </div>
    </center>
</asp:Content>
