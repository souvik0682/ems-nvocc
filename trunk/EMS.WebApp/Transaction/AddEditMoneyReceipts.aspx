<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditMoneyReceipts.aspx.cs"
    Inherits="EMS.WebApp.Transaction.AddEditUser" MasterPageFile="~/Site.Master"
    Title=":: Liner :: Add / Edit Money Receipts"%>

<%@ Register Src="../CustomControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
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
        ADD / EDIT MONEY RECEIPTS</div>
    <center>
        <div style="width: 850px;">
            <fieldset>
                <legend>Add / Edit Money Receipts</legend>
                <table border="0" cellpadding="3" cellspacing="3" width="100%">
                    <tr>
                        <td style="width: 150px;">
                            M/R No:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMRNo" runat="server" CssClass="textboxuppercase" MaxLength="10"
                                Width="250"></asp:TextBox><br />
                            <span id="spnMRNo" runat="server" class="errormessage" style="display: none;"></span>
                            <%--<asp:RequiredFieldValidator ID="rfvFName" runat="server" CssClass="errormessage" ControlToValidate="txtFName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td>
                            Date:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <uc1:DatePicker ID="dtPckrMRDate" runat="server" />
                            <br />
                            <span id="spnMRDate" runat="server" class="errormessage" style="display: none;">
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            BL No:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBLNo" runat="server" CssClass="textboxuppercase" MaxLength="30"
                                Width="250" AutoPostBack="True" ontextchanged="txtBLNo_TextChanged"></asp:TextBox><br />
                            <span id="spnBLNo" runat="server" class="errormessage" style="display: none;"></span>
                            <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="errormessage" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td>
                            Location:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                Width="250" Enabled="False"></asp:TextBox><br />
                            <span id="spnLocation" runat="server" class="errormessage" style="display: none;">
                            </span>
                            <%--<asp:RequiredFieldValidator ID="rfvRole" runat="server" CssClass="errormessage" ControlToValidate="ddlRole" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Line/NVOCC:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNvocc" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                Width="250" Enabled="False"></asp:TextBox><br />
                            <span id="spnNvocc" runat="server" class="errormessage" style="display: none;"></span>
                            <%--<asp:RequiredFieldValidator ID="rfvRole" runat="server" CssClass="errormessage" ControlToValidate="ddlRole" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td>
                            Export/Import:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlExportImport" runat="server" Enabled="false">
                            <asp:ListItem>None</asp:ListItem>
                            <asp:ListItem>Export</asp:ListItem>
                            <asp:ListItem>Import</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <span id="spnExpImp" runat="server" class="errormessage" style="display: none;">
                            </span>
                            <%--<asp:RequiredFieldValidator ID="rfvLoc" runat="server" CssClass="errormessage" ControlToValidate="ddlLoc" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div style="width: 850px;">
                <fieldset style="width: 100%;">
                    <legend>Add/ Edit Charge Details</legend>
                    <table>
                        <tr>
                            <td>
                                Invoice Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpDwnInvoiceType" runat="server" Width="250" 
                                    OnSelectedIndexChanged="drpDwnInvoiceType_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvFName" runat="server" CssClass="errormessage" ControlToValidate="txtFName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                Invoice No.:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpDwnInvoiceNo" runat="server" Width="250" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="drpDwnInvoiceNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Invoice Date:
                            </td>
                            <td>
                                <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                    Width="250"></asp:TextBox>
                            </td>
                            <td>
                                Invoice Amount:
                            </td>
                            <td>
                                <asp:TextBox ID="txtInvoiceAmount" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                    Width="250" Enabled="false" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Received Amount:
                            </td>
                            <td>
                                <asp:TextBox ID="txtReceivedAmount" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                    Width="250" Enabled="false" Text="0"></asp:TextBox>
                            </td>
                            <td>
                                Current Amount:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCurrentAmount" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                    Width="250" AutoPostBack="True" Enabled="False" 
                                    ontextchanged="txtCurrentAmount_TextChanged" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                TDS:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTDS" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                    Width="250" AutoPostBack="True" ontextchanged="txtTDS_TextChanged" Text="0"></asp:TextBox>
                            </td>
                            <td>
                                Cash Amount
                            </td>
                            <td>
                                <asp:TextBox ID="txtCashAmount" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                    Width="250" AutoPostBack="True" ontextchanged="txtCashAmount_TextChanged" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cheque Amount
                            </td>
                            <td>
                                <asp:TextBox ID="txtChequeAmount" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                    Width="250" AutoPostBack="True" 
                                    ontextchanged="txtChequeAmount_TextChanged" Text="0"></asp:TextBox>
                            </td>
                            <td>
                                Cheque Details:
                            </td>
                            <td>
                                <asp:TextBox ID="txtChequeDetails" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                    Width="250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" Text="Add To List" Width="130px" 
                                    onclick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <%--<asp:Button ID="btnAdd" runat="server" Text="Add To List" Width="130px" OnClick="btnAdd_Click" />--%>
            </div>
            <fieldset id="fsList" runat="server">
                <legend>Charge Details List</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                </div>
                <br />
                <div>
                    <%--<asp:UpdatePanel ID="upAddEditMoneyReceipts" runat="server" UpdateMode="Always">
                        <ContentTemplate>--%>
                            <asp:GridView ID="gvwAddEditMoneyReceipts" runat="server"
                                AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" Width="100%" 
                                onrowdatabound="gvwAddEditMoneyReceipts_RowDataBound"
                                OnRowCommand="gvwAddEditMoneyReceipts_RowCommand" onrowediting="gvwAddEditMoneyReceipts_RowEditing"
                             
                               >
                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Manage receipt(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHInvoiceType" runat="server" CommandName="Sort" CommandArgument="InvoiceType"
                                                Text="Invoice Type"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceType" runat="server"></asp:Label><asp:Label ID="lblInActive"
                                                runat="server" CssClass="errormessage" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="13%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHInvoiceNo" runat="server" CommandName="Sort" CommandArgument="InvoiceNo"
                                                Text="Invoice No"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHInvoiceDate" runat="server" CommandName="Sort" CommandArgument="InvoiceDate"
                                                Text="InvoiceDate"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHInvoiceAmount" runat="server" CommandName="Sort" CommandArgument="InvoiceAmount"
                                                Text="Invoice Amount"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="13%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHReceivedAmount" runat="server" CommandName="Sort" CommandArgument="ReceivedAmount"
                                                Text="Received Amount"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceivedAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHCurrentReceived" runat="server" CommandName="Sort" CommandArgument="CurrentReceived"
                                                Text="Current Received"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrentReceived" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHTDS" runat="server" CommandName="Sort" CommandArgument="TDS"
                                                Text="TDS"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTDS" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHCashAmount" runat="server" CommandName="Sort" CommandArgument="CashAmount"
                                                Text="Cash Amount"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCashAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHChequeAmount" runat="server" CommandName="Sort" CommandArgument="ChequeAmount"
                                                Text="Cheque Amount"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblChequeAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkHChequeNo" runat="server" CommandName="Sort" CommandArgument="ChequeNo"
                                                Text="Cheque No"></asp:LinkButton></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblChequeNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png"
                                                Height="16" Width="16"/> 
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
                        <%--</ContentTemplate>--%>
                     <%--</asp:UpdatePanel>--%>
                </div>
            </fieldset>
           
            <div>
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button
                    ID="btnBack" runat="server" CssClass="button" Text="Back" />
            </div>
        </div>
    </center>
</asp:Content>
