<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BookingCharges.aspx.cs" Inherits="EMS.WebApp.Export.BookingCharges" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <script type="text/javascript">
        function CurrentDateShowing(e) {
            if (!e.get_selectedDate() || !e.get_element().value)
                e._selectedDate = (new Date()).getDateOnly();
        }

        function popWin() {
            window.open("FileUpload.aspx", "", "height=300px,toolbar=0,menubar=0,resizable=1,status=1,scrollbars=1"); return false;
        }
        function update(val) {
            document.getElementById('<%=hdnFilePath.ClientID %>').value = val;
            alert(document.getElementById('<%=hdnFilePath.ClientID %>').value);
        }
    </script>
    <div>
        <div id="headercaption">
            BOOKING CHARGES
        </div>
        <center>
            <fieldset style="width: 85%;">
                <legend>Add / Edit Charges</legend>
                <asp:UpdatePanel ID="upCharges" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    Shipper
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtShipper" runat="server" Width="855px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    POL
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPOL" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    POD
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPOD" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    FPOD
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFPOD" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Containers
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContainers" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Freight Payable At
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFreight" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td>
                                    Brokerage Payable
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdblBorkerage" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Borkerage%
                                </td>
                                <td>
                                    <cc2:CustomTextBox ID="txtBrokeragePercent" runat="server" Width="250px" Type="Decimal"
                                        MaxLength="10" Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                </td>
                                <td>
                                    Brokerage Payable To
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBrokeragePayableTo" runat="server" Width="250px" Text="Auto Correct"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Refund Payable
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdblRefund" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    Refund Payable To
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Width="250px" Text="Auto Correct"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Remarks
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="250px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </td>
                                <td>
                                    Rate Reference
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRateReference" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Rate Type
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRateType" runat="server" CssClass="dropdownlist">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Upload
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="javascript:popWin();"
                                        TabIndex="58" />
                                    <asp:HiddenField ID="hdnFilePath" runat="server" />
                                    &nbsp;&nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="padding-top: 10; border: none;">
                                    <fieldset style="width: 95%;">
                                        <legend>Add Charges</legend>
                                        <table>
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Charge Name
                                                </td>
                                                <td style="font-weight: bold">
                                                    Applicable
                                                </td>
                                                <td style="text-align: right; font-weight: bold">
                                                    Currency
                                                </td>
                                                <td style="text-align: right; font-weight: bold">
                                                    PP/CC
                                                </td>
                                                <td style="text-align: right; font-weight: bold">
                                                    Size/Type
                                                </td>
                                                <td style="text-align: right; font-weight: bold">
                                                    Weight CBM
                                                </td>
                                                <td style="text-align: right; font-weight: bold">
                                                    Weight Ton
                                                </td>
                                                <td style="text-align: right; font-weight: bold">
                                                    Manifest
                                                </td>
                                                <td style="text-align: right; font-weight: bold">
                                                    Charged
                                                </td>
                                                <td style="text-align: right; font-weight: bold">
                                                    Refund
                                                </td>
                                                <td style="text-align: right; font-weight: bold">
                                                    Brokerage Basic
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlFChargeName" runat="server" Width="150">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvChargeName" runat="server" ErrorMessage="Required"
                                                        CssClass="errormessage" ValidationGroup="vgAdd" ControlToValidate="ddlFChargeName"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlApplicable" runat="server" Width="80">
                                                        <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="80">
                                                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPpCc" runat="server" Width="60">
                                                        <asp:ListItem Text="PP" Value="P" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="CC" Value="C"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSizeType" runat="server" Width="60">
                                                        <asp:ListItem Text="20GP" Value="G" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="40HC" Value="H"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCbm" runat="server" Width="80">
                                                        <asp:ListItem Text="For LCL" Value="L" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Bulk" Value="B"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtWeightTon" runat="server" Width="80" MaxLength="10" Style="text-align: right;"></cc2:CustomTextBox>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtManifest" runat="server" Width="80" Type="Decimal" MaxLength="10"
                                                        Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtCharged" runat="server" Width="80" Type="Decimal" MaxLength="10"
                                                        Enabled="false" Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtRefund" runat="server" Width="80" Type="Decimal" MaxLength="10"
                                                        Enabled="false" Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtBrokerageBasic" runat="server" Width="80" Type="Decimal" MaxLength="10"
                                                        Enabled="false" Precision="8" Scale="2" Style="text-align: right;" Text="0.00"></cc2:CustomTextBox>
                                                    <br />
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="vgAdd" /><br />
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="10">
                                                    <asp:GridView ID="gvwCharges" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                        BorderStyle="None" BorderWidth="0" Width="100%">
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
                                                            <asp:TemplateField HeaderText="Applicable">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Currency">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PP/PC">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size/Type">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Weight CBM">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Weight Ton">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="6%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Manifest">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="4%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Charged">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Refund">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Brokerage Basic">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditRow" ImageUrl="~/Images/edit.png"
                                                                        Height="16" Width="16" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                                        Height="16" Width="16" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSave" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back"
                                        OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnLock" runat="server" Text="Save/Locked" ValidationGroup="vgSave" />
                                    <br />
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressCharges" runat="server" AssociatedUpdatePanelID="upCharges">
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
