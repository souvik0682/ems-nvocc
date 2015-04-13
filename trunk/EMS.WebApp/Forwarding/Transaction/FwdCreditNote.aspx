<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FwdCreditNote.aspx.cs" Inherits="EMS.WebApp.Forwarding.Transaction.FwdCreditNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            ADD/EDIT FORWARDING CREDIT NOTE
        </div>
        <center>
            <fieldset style="width: 85%;">
                <legend>Add / Edit Forwarding Credit Note</legend>
                <asp:UpdatePanel ID="upCreditNote" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <!-- Header Section -->
                            <tr>
                                <td>
                                    Location
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLocation" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Party
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLine" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Credit Note No
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCreditNoteNo" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Invoice Type
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvoiceType" runat="server" CssClass="textboxuppercase" Enabled="false" Width="250px"></asp:TextBox>
                                </td>
                                <td>
                                    Invoice Ref
                                </td>
                                <td>
                                     <asp:TextBox ID="txtInvoiceRef" Enabled="false" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250px"></asp:TextBox>
                                </td>
                                <td>
                                    C/N Date<span class="errormessage1">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCNDate" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtCNDate" runat="server"
                                        Format="dd-MM-yyyy" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCNDate"
                                        ValidationGroup="VGCrnSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Containers
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContainers" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Invoice Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvoiceDate" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Job Ref
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBLRef" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    CRN For
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExpImp" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <!-- Footer Section -->
                            <tr>
                                <td colspan="6" style="padding-top: 10; border: none;">
                                    <fieldset style="width: 95%;">
                                        <legend>Add Charges</legend>
                                        <table>
                                            <tr>
                                                <td style="font-weight:bold">
                                                    Charge Name
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    Charged In Invoice
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    S/Tax
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    CRN Amount
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    S/Tax %
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    S/Tax
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlFChargeName" runat="server" Width="320" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlChargeName_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvChargeName" runat="server" ErrorMessage="Required"
                                                        CssClass="errormessage" ValidationGroup="vgAdd" ControlToValidate="ddlFChargeName"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtChargeInvoice" runat="server" Width="128" Type="Decimal" MaxLength="10"
                                                        Enabled="false" Precision="8" Scale="2" Style="text-align: right;" Text="0.00"
                                                        ></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtChargeServiceTax" runat="server" Width="117" Type="Decimal" MaxLength="10"
                                                        Enabled="false" Precision="8" Scale="2" Style="text-align: right;" Text="0.00"
                                                        ></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtCNAmount" runat="server" Width="131" Type="Decimal" MaxLength="10" AutoPostBack="true"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00" ontextchanged="txtCNAmount_TextChanged"
                                                        ></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtStaxper" runat="server" Width="118" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"
                                                        Enabled="false"></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtCNServiceTax" runat="server" Width="118" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"
                                                        Enabled="false"></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                 <td>
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="vgAdd" /><br />
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <asp:GridView ID="gvwCreditNote" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                    BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvwCreditNote_RowDataBound"
                                                    OnRowCommand="gvwCreditNote_RowCommand">
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                    <PagerStyle CssClass="gridviewpager" />
                                                    <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                    <EmptyDataTemplate>
                                                        No Page(s) Found
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Charge Name">
                                                            <HeaderStyle CssClass="gridviewheader" />
                                                            <ItemStyle CssClass="gridviewitem" Width="210" />
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Charged In Invoice">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="75" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="S/Tax">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="75" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit Note Amount">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="75" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S/Tax %">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="75" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S/Tax">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="75" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderStyle CssClass="gridviewheader" />
                                                            <ItemStyle CssClass="gridviewitem" Width="75" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditRow" ImageUrl="~/Images/edit.png"
                                                                    Height="16" Width="16" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderStyle CssClass="gridviewheader" />
                                                            <ItemStyle CssClass="gridviewitem" Width="75" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                                    Height="16" Width="16" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="VGCrnSave" OnClick="btnSave_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClick="btnBack_Click"
                                        OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressCreditNote" runat="server" AssociatedUpdatePanelID="upCreditNote">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </center>
    </div>
</asp:Content>
