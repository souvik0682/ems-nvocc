<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExportInvoice.aspx.cs" Inherits="EMS.WebApp.Export.ExportInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            ADD/EDIT EXPORT INVOICE
        </div>
        <center>
            <fieldset style="width: 85%;">
                <legend>Add / Edit Invoice</legend>
                <asp:UpdatePanel ID="upInvoice" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    Invoice No
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvoiceNo" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                               <td>
                                   Booking Number<span class="errormessage1">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBooking" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                        Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBooking"
                                        ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Invoice Date<span class="errormessage1">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                        Width="250px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="cbeInvoiceDate" TargetControlID="txtInvoiceDate" runat="server"
                                        Format="dd-MM-yyyy" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="rfvLineBLDate" runat="server" ControlToValidate="txtInvoiceDate"
                                        ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    Location
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLocation" runat="server" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"
                                        AutoPostBack="true" Enabled="false">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    B/L No
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBLno" runat="server" OnSelectedIndexChanged="ddlBLno_SelectedIndexChanged"
                                        AutoPostBack="true" Enabled="false">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                  <td>
                                    Line / NVOCC
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlNvocc" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlNvocc_SelectedIndexChanged" Enabled="false">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            
                            </tr>
                            <tr>
                                <td>
                                    BL Date
                                </td>
                                <td>
                                  <asp:TextBox ID="txtBLDate" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                        Width="250px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtBLDate" runat="server"
                                        Format="dd-MM-yyyy" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBLDate"
                                        ValidationGroup="vgSave" ErrorMessage="This field is required*" CssClass="errormessage"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>

                                    <td>
                                    Invoice Type
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlInvoiceType" runat="server" Enabled="false">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                               
                            </tr>
                            <tr>
                                <td>
                                    Invoice amount
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" Width="250px" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                </td>

                                <%--<td colspan="2">&nbsp; </td>--%>
                            
                                <td>
                                    Containers
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContainers" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    Rounding Off
                                </td>
                                <td>
                                    <asp:TextBox ID="txtROff" runat="server" Width="250px" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                </td>
                                <td>
                                    All In Freight
                                </td>
                                <td>
                                   <asp:RadioButtonList ID="rdoHazardousCargo" runat="server" TabIndex="20" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="padding-top: 10; border: none;">
                                    <fieldset style="width: 95%;">
                                        <legend>Add Charges</legend>
                                        <div>
                                        <table width="100%" cellpadding="0" cellspacing="0" style="border-width:0px;border-style:None;width:100%;border-collapse:collapse;">
                                            <tr>
                                                <td style="font-weight:bold">
                                                    Charge Name
                                                </td>
                                                <td style="font-weight:bold">
                                                    Terminal
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    Per BL
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    TEU
                                                </td>

                                                <td style="text-align: right;font-weight:bold">
                                                    FEU
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    CBM
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    Ton
                                                </td>

                                                <td style="text-align: center;font-weight:bold">
                                                    Currency
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    Conv Rate
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    Gross (INR)
                                                </td>

                                                <td style="text-align: right;font-weight:bold">
                                                    S. Tax
                                                </td>
                                                <td style="text-align: right;font-weight:bold">
                                                    Total Amt
                                                </td>
                                            </tr>
                                            <tbody>
                                          
                                            <tr>
                                                <td class="gridviewitem" style="width:20%;">
                                                    <asp:DropDownList ID="ddlFChargeName" runat="server" Width="213" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlChargeName_SelectedIndexChanged" Enabled="false">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvChargeName" runat="server" ErrorMessage="Required"
                                                        CssClass="errormessage" ValidationGroup="vgAdd" ControlToValidate="ddlFChargeName"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFTerminal" runat="server" Width="150" Enabled="false">
                                                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvTerminal" runat="server" ErrorMessage="Required"
                                                        CssClass="errormessage" ValidationGroup="vgAdd" ControlToValidate="ddlFTerminal"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                    &nbsp;
                                                </td>                                             
                                                <td class="gridviewitem" style="width:6%;">
                                                    <cc2:CustomTextBox ID="txtRatePerBL" runat="server" Width="50" Type="Decimal" MaxLength="10"
                                                          Precision="8" Scale="2" Style="text-align: right;" Text="0.00" Enabled="false"
                                                        OnTextChanged="txtRatePerBL_TextChanged" AutoPostBack="true" ></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                <td class="gridviewitem" align="right" style="width:6%;">
                                                    <cc2:CustomTextBox ID="txtRatePerTEU" runat="server" Width="60" Type="Decimal" MaxLength="10"
                                                          Precision="8" Scale="2" Style="text-align: right;" Text="0.00" Enabled="false"
                                                        OnTextChanged="txtRatePerTEU_TextChanged" AutoPostBack="true"></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>

                                                <td class="gridviewitem"  style="width:6%">
                                                    <cc2:CustomTextBox ID="txtRateperFEU" runat="server" Width="55" Type="Decimal" MaxLength="10"
                                                          Precision="8" Scale="2" Style="text-align: right;" Text="0.00" Enabled="false"
                                                        OnTextChanged="txtRateperFEU_TextChanged" AutoPostBack="true"></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                <td class="gridviewitem"  style="width:6%">
                                                    <cc2:CustomTextBox ID="txtRatePerCBM" runat="server" Width="55" Type="Decimal" MaxLength="10"
                                                          Precision="8" Scale="2" Style="text-align: right;" Text="0.00" Enabled="false"
                                                        OnTextChanged="txtRatePerCBM_TextChanged" AutoPostBack="true"></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                <td style="width:6%">
                                                    <cc2:CustomTextBox ID="txtRatePerTon" runat="server" Width="55" Type="Decimal" MaxLength="10"
                                                          Precision="8" Scale="2" Style="text-align: right;" Text="0.00" Enabled="false"
                                                        OnTextChanged="txtRatePerTon_TextChanged" AutoPostBack="true"></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>

                                                <td class="gridviewitem" style="width:10%">
                                                   <asp:DropDownList ID="ddlCurrency" runat="server"  AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required"
                                                        CssClass="errormessage" ValidationGroup="vgAdd" ControlToValidate="ddlCurrency"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="gridviewitem"  style="width:8%">
                                                    <cc2:CustomTextBox ID="custTxtConvRate" runat="server" Width="80" Type="Decimal" MaxLength="10" Enabled="false"
                                                          Precision="8" Scale="2" Style="text-align: right;" Text="0.00"  OnTextChanged="txtRatePerBL_TextChanged" AutoPostBack="true"></cc2:CustomTextBox><br />
                                                    &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required"
                                                        CssClass="errormessage" ValidationGroup="vgAdd" ControlToValidate="custTxtConvRate"
                                                       ></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="gridviewitem"  style="width:8%">
                                                    <cc2:CustomTextBox ID="txtGrossAmount" runat="server" Width="80" Type="Decimal" MaxLength="10" Enabled="false"
                                                          Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>

                                                <td class="gridviewitem" style="width:6%">
                                                    <cc2:CustomTextBox ID="txtServiceTax" runat="server" Width="80" Type="Decimal" MaxLength="10" Enabled="false"
                                                          Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                <td class="gridviewitem"  style="width:8%">
                                                    <cc2:CustomTextBox ID="txtTotal" runat="server" Width="75" Type="Decimal" MaxLength="10" Enabled="false"
                                                          Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox><br />
                                                    &nbsp;
                                                </td>
                                                <td class="gridviewitem"  style="width:10%">
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="vgAdd" /><br />
                                                    &nbsp;
                                                </td>
                                            </tr></tbody>
                                        </table></div>
                                        <asp:GridView ID="gvwInvoice" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                    BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvwInvoice_RowDataBound"
                                                    OnRowCommand="gvwInvoice_RowCommand">
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                    <PagerStyle CssClass="gridviewpager" />
                                                    <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                    <EmptyDataTemplate>
                                                        No Page(s) Found
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Charge Name">
                                                            <HeaderStyle CssClass="gridviewheader" />
                                                            <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Terminal">
                                                            <HeaderStyle CssClass="gridviewheader" />
                                                            <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Per BL">
                                                            <HeaderStyle CssClass="gridviewheader" />
                                                            <ItemStyle CssClass="gridviewitem" Width="6%" />
                                                        </asp:TemplateField>                                                      
                                                        <asp:TemplateField HeaderText="TEU">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FEU">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CBM">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ton">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Currency">
                                                            <HeaderStyle CssClass="gridviewheader" />
                                                            <ItemStyle CssClass="gridviewitem" Width="10%" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Conv Rate">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gross (INR)">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S. Tax">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amt">
                                                            <HeaderStyle CssClass="gridviewheader_num" />
                                                            <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditRow" ImageUrl="~/Images/edit.png"
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
                                            
                                        
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSave" OnClick="btnSave_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back"
                                        OnClick="btnBack_Click" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                                    <br />
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressInvoice" runat="server" AssociatedUpdatePanelID="upInvoice">
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
