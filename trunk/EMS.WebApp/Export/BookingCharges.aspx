<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BookingCharges.aspx.cs" Inherits="EMS.WebApp.Export.BookingCharges" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Src="~/CustomControls/AC_Brockerage.ascx" TagName="AC_Brockerage" TagPrefix="uc1" %>
<%@ Register Src="~/CustomControls/AC_Refund.ascx" TagName="AC_Refund" TagPrefix="uc2" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
        .style1
        {
            width: 85px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    
    <script type="text/javascript">
        function WebForm_OnSubmit() {
            if (typeof (ValidatorOnSubmit) == 'function' && ValidatorOnSubmit() == false) {
                var cntr = null;
                for (var i in Page_Validators) {
                    try {
                        var control = document.getElementById(Page_Validators[i].controltovalidate);
                        if (!Page_Validators[i].isvalid) {
                            control.className = 'ErrorControl';
                            cntr = control;
                        } else {
                            if (cntr == control) {
                                control.className = 'ErrorControl';
                            }
                            else {
                                control.className = '';
                            }
                        }


                    } catch (e) { }
                }
                return false;
            }
            return true;
        }
    </script>
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
                                    Booking No
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBookingNo" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    Booking Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBookingDate" runat="server" Width="250px" Enabled="false"></asp:TextBox>
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
                                    <uc3:AC_Port ID="txtFreightPayableAt" runat="server" />
                                </td>
                                <td>
                                    Brokerage Payable
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdblBorkerage" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rdblBorkerage_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Borkerage%
                                </td>
                                <td>
                                    <cc2:CustomTextBox ID="txtBrokeragePercent" runat="server" Width="250px" Type="Decimal"
                                        Enabled="false" MaxLength="10" Precision="8" Scale="2" Style="text-align: right;"
                                        Text="0.00"></cc2:CustomTextBox>
                                </td>
                                <td>
                                    Brokerage Payable To
                                </td>
                                <td>
                                    <uc1:AC_Brockerage ID="txtBrokeragePayableTo" runat="server" Width="250px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Refund Payable
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdblRefundPayable" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rdblRefundPayable_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    Refund Payable To
                                </td>
                                <td>
                                    <uc2:AC_Refund ID="txtRefundPayableTo" runat="server" Width="250px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Remarks
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="250px" TextMode="MultiLine" Rows="3" CssClass="textboxuppercase"></asp:TextBox>
                                </td>
                                <td>
                                    PP/CC
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPpCc" runat="server">
                                        <asp:ListItem Value="P" Text="Pre Paid" Selected="True" />
                                        <asp:ListItem Value="T" Text="To Pay" />
                                    </asp:DropDownList>
                                </td>
                               <%-- <td>
                                    Shipper
                                </td>--%>
                                <td colspan="4">
                                    <asp:TextBox ID="txtShipper" runat="server" Width="250px" TextMode="MultiLine" Rows="3" Visible="false" CssClass="textboxuppercase"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Rate Reference
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRateReference" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                                    <%--<br />
                                    <asp:RequiredFieldValidator ID="rfvRateReference" runat="server" ControlToValidate="txtRateReference"
                                        Display="Dynamic" CssClass="errormessage" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td>
                                    Rate Type
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRateType" runat="server" CssClass="dropdownlist">
                                        <asp:ListItem Value="FreeHand" Text="FREE HAND"></asp:ListItem>
                                        <asp:ListItem Value="Nomination" Text="NOMINATION"></asp:ListItem>
                                        <asp:ListItem Value="SalesLead" Text="SALES LEAD"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Upload
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="javascript:popWin();"
                                        TabIndex="58" />
                                    <asp:HiddenField ID="hdnFilePath" runat="server" />
                                    &nbsp;&nbsp; &nbsp;
                                </td>
                                <td>
                                    Slot Operator<span class="errormessage">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSlot" runat="server" CssClass="dropdownlist">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvSlot" runat="server" CssClass="errormessage" ErrorMessage="This field is required"
                                        ControlToValidate="ddlSlot" InitialValue="0" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4" style="padding-top: 10; border: none;">
                                    <fieldset style="width: 95%;">
                                        <legend>Add Charges</legend>
                                        <table>
                                            <tr>
                                                <td colspan="10">
                                                    <asp:GridView ID="gvwCharges" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="3" DataKeyNames="BookingChargeId" OnRowDataBound="gvwContainers_RowDataBound">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ChargeName" HeaderText="Charge Name" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="ChargeName" HeaderStyle-Width="300" />
                                                            <asp:BoundField DataField="Unit" HeaderText="Unit" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Unit"  />
                                                            <%--<asp:TemplateField HeaderText="Applicable">
                                                                <ItemTemplate>
                                                                
                                                                    <asp:DropDownList ID="ddlApplicable" runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnBookingChargeId" runat="server" Value='<%# Eval("BookingChargeId") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:BoundField DataField="CurrencyName" HeaderText="Currency" InsertVisible="False"
                                                                ReadOnly="True" />
                                                            <asp:BoundField DataField="Size" HeaderText="Size" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Size" />
                                                            <asp:BoundField DataField="ContainerType" HeaderText="Type" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="ContainerType" />
                                                            <asp:BoundField DataField="WtInCBM" HeaderText="Weight CBM" InsertVisible="False"
                                                                HeaderStyle-Width="150" ReadOnly="True" SortExpression="WtInCBM" ItemStyle-HorizontalAlign="Right" />
                                                            <asp:BoundField DataField="WtInTon" HeaderText="Weight Ton" InsertVisible="False"
                                                                HeaderStyle-Width="150" ReadOnly="True" SortExpression="WtInTon" ItemStyle-HorizontalAlign="Right" />
                                                            <asp:TemplateField HeaderText="Manifest @" SortExpression="ManifestRate" HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnBookingChargeId" runat="server" Value='<%# Eval("BookingChargeId") %>' />
                                                                    <asp:HiddenField ID="hdnDocumentType" runat="server" Value='<%# Eval("DocumentTypeID") %>' />
                                                                    <cc2:CustomTextBox ID="txtManifest" runat="server" Text='<%# Bind("ManifestRate") %>'
                                                                        Width="80" BorderStyle="None" Style="text-align: right;" OnTextChanged="TextBox_TextChanged"
                                                                        MaxLength="10" Precision="8" Scale="2" Type="Decimal" Enabled='<%# Eval("ManifestEditabe").ToString() == "True" %>'>
                                                                    </cc2:CustomTextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtManifest"
                                                                        Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ID="cv1" runat="server" ControlToValidate="txtManifest" Operator="GreaterThan"
                                                                        Type="Double" ValueToCompare="0" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Charged @" SortExpression="ActualRate" HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <cc2:CustomTextBox ID="txtCharged" runat="server" Text='<%# Bind("ActualRate") %>'
                                                                        Width="80" BorderStyle="None" Style="text-align: right;" OnTextChanged="TextBox_TextChanged"
                                                                        MaxLength="10" Precision="8" Scale="2" Type="Decimal" Enabled='<%# Eval("ChargedEditable").ToString() == "True" %>'>
                                                                    </cc2:CustomTextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtCharged" Display="Dynamic" 
                                                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ID="cv2" runat="server" ControlToValidate="txtCharged" Operator="GreaterThan"
                                                                        Type="Double" ValueToCompare="0" ValidationGroup="Save" >
                                                                    </asp:CompareValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Refund" SortExpression="RefundAmount" HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <cc2:CustomTextBox ID="txtRefund" runat="server" Text='<%# Bind("RefundAmount") %>'
                                                                        Width="80" BorderStyle="None" Style="text-align: right;" OnTextChanged="TextBox_TextChanged"
                                                                        MaxLength="10" Precision="8" Scale="2" Type="Decimal" Enabled='<%# Eval("RefundEditable").ToString() == "True" && Eval("DocumentTypeID").ToString() == "1" %>'>
                                                                    </cc2:CustomTextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtRefund" Display="Dynamic" 
                                                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Brkg. Basic" SortExpression="BrokerageBasic" HeaderStyle-Width="120"> 
                                                                <ItemTemplate>
                                                                    <cc2:CustomTextBox ID="txtBrokerageBasic" runat="server" Text='<%# Bind("BrokerageBasic") %>'
                                                                        Width="100" BorderStyle="None" Style="text-align: right;" OnTextChanged="TextBox_TextChanged"
                                                                        MaxLength="10" Precision="8" Scale="2" Type="Decimal" Enabled='<%# Eval("BrokerageEditable").ToString() == "True" && Eval("DocumentTypeID").ToString() == "1" %>'>
                                                                    </cc2:CustomTextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtBrokerageBasic" Display="Dynamic" 
                                                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remove">
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnRemove" runat="server" OnClick="btnRemove_Click" ImageUrl="~/Images/remove.png"
                                                                        Height="16" Width="16" OnClientClick="javascript:if(!confirm('Want to Delete?')) return false;"/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                                    &nbsp;&nbsp;
                                     <asp:Button ID="btnDel" runat="server" CssClass="button" Text="Delete" OnClientClick="javascript:if(!confirm('Want to Delete?')) return false;" 
                                    OnClick="btnDel_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" 
                                    OnClick="btnBack_Click" />
                                    &nbsp;&nbsp;
                                   
                                    <%--<asp:Button ID="btnLock" runat="server" Text="Save/Locked" ValidationGroup="vgSave" />--%><br />
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
