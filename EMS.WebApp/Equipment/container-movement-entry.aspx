<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="container-movement-entry.aspx.cs" Inherits="EMS.WebApp.Equipment.container_movement_entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div>
        <div id="headercaption">
            ADD / EDIT CONTAINER MOVEMENT</div>
        <center>
            <fieldset style="width: 60%;">
                <legend>Add / Edit Container Transaction</legend>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td>
                                    Transaction CODE :
                                </td>
                                <td>
                                    <asp:Label ID="lblTranCode" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnTranCode" runat="server" />
                                    <asp:HiddenField ID="hdnContainerTransactionId" runat="server" Value="0" />
                                </td>
                                <td>
                                    Activity Date<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <script type="text/javascript">
                                        function ChangeActivityDate(txt) {
                                            var ID = txt.id;
                                            //alert(ID);
                                            //alert(txt.value);
                                            document.getElementById("container_gvSelectedContainer_lblStatusDate_0").innerHTML = txt.value;
                                        }
                                    </script>
                                    <asp:TextBox ID="txtDate" runat="server" Width="150"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDate"
                                        Format="dd/MM/yyyy" TargetControlID="txtDate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Please enter date"
                                        ControlToValidate="txtDate" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="rfvDate">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    From Status<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <script type="text/javascript">
                                        function clear() {
                                            document.getElementById('<%= lblMessage.ClientID %>').innerHTML = "";
                                        }
                                    </script>
                                    <asp:DropDownList ID="ddlFromStatus" runat="server" Width="155" onchange="clear();"
                                        OnSelectedIndexChanged="ddlFromStatus_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvFromStatus" runat="server" ErrorMessage="Please enter from status"
                                        ControlToValidate="ddlFromStatus" Display="None" ValidationGroup="vgContainer"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" TargetControlID="rfvFromStatus">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    To Status<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlToStatus" runat="server" Width="155" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlToStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvToStatus" runat="server" ErrorMessage="Please enter transfer location"
                                        ControlToValidate="ddlToStatus" Display="None" ValidationGroup="vgContainer"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" TargetControlID="rfvToStatus">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    From Location<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFromLocation" runat="server" Width="155" OnSelectedIndexChanged="ddlFromLocation_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvFromLocation" runat="server" ErrorMessage="Please enter from location"
                                        ControlToValidate="ddlFromLocation" Display="None" InitialValue="0" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="rfvFromLocation">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    To Location:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTolocation" runat="server" Width="155" Enabled="false">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvToLocation" runat="server" ErrorMessage="Please enter to location"
                                        ControlToValidate="ddlTolocation" Display="None" ValidationGroup="vgContainer"
                                        Enabled="false"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="rfvToLocation">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>
                                    No. Of TEUs<%--<span class="errormessage1">*</span>--%>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTeus" runat="server" Width="150" Style="text-align: right;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtTeus"
                                        FilterMode="ValidChars" FilterType="Numbers">
                                    </cc1:FilteredTextBoxExtender>
                                    <%--<asp:RequiredFieldValidator ID="rfvTeus" runat="server" ErrorMessage="Please enter total no of TEUs"
                                ControlToValidate="txtTeus" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="rfvTeus">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                <td>
                                    No. Of FEUs<%--<span class="errormessage1">*</span>--%>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFEUs" runat="server" Width="150" Style="text-align: right;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFEUs"
                                        FilterMode="ValidChars" FilterType="Numbers">
                                    </cc1:FilteredTextBoxExtender>
                                    <%--<asp:RequiredFieldValidator ID="rfvFeus" runat="server" ErrorMessage="Please enter total no of FEUs"
                                ControlToValidate="txtFEUs" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="rfvFeus">
                            </cc1:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Line <span class="errormessage1">*</span>:
                                </td>
                                <td>
                                    

                                    <asp:DropDownList ID="ddlLine" runat="server" Width="155" AutoPostBack="true" 
                                        onselectedindexchanged="ddlLine_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvLine" runat="server" ErrorMessage="Please select line"
                                        Display="None" ControlToValidate="ddlLine" InitialValue="0" ValidationGroup="vgContainer"
                                        ></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvLine">
                                    </cc1:ValidatorCalloutExtender>

                                </td>
                                <td valign="top">
                                    Empty Yard <span class="errormessage1">*</span>:
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlEmptyYard" runat="server" Enabled="false" Width="155" OnSelectedIndexChanged="ddlEmptyYard_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvEmptyYard" runat="server" ErrorMessage="Please select yard location"
                                        Display="None" ControlToValidate="ddlEmptyYard" InitialValue="0" ValidationGroup="vgContainer"
                                        Enabled="false"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvEmptyYard">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                
                                <td valign="top">                                    
                                    Narration :
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtNarration" runat="server" Width="150" TextMode="MultiLine" Style="text-transform: uppercase;"></asp:TextBox>
                                </td>
                                <td>
                                    Booking No.:</td>
                                <td>
                                    <asp:DropDownList ID="ddlBookingNo" runat="server" Enabled="false" Width="155" OnSelectedIndexChanged="ddlBookingNo_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBookingNo" runat="server" ErrorMessage="Please select Booking No"
                                        Display="None" ControlToValidate="ddlBookingNo" InitialValue="0" ValidationGroup="vgContainer"
                                        Enabled="false"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvBookingNo">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    DO No.:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDONo" runat="server" Enabled="false" Width="155" OnSelectedIndexChanged="ddlDONo_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDONo" runat="server" ErrorMessage="Please select DO No"
                                        Display="None" ControlToValidate="ddlDONo" InitialValue="0" ValidationGroup="vgContainer"
                                        Enabled="false"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvDONo">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td>
                            <div style="margin-left: 53%;">
                                <asp:Button ID="btnShow" runat="server" Text="Show Container" OnClick="btnShow_Click"
                                    ValidationGroup="vgContainer" /></div>
                            <asp:Button ID="Button1" runat="server" Style="display: none;" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                                PopupControlID="pnlContainer" Drag="true" BackgroundCssClass="ModalPopupBG" CancelControlID="btnCancel">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlContainer" runat="server" Style="height: 250px; width: 400px; background-color: White;">
                                <center>
                                    <fieldset>
                                        <div style="overflow: auto; height: 180px; width: 380px;">
                                            <asp:GridView ID="gvContainer" runat="server" AutoGenerateColumns="false" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Container No" HeaderStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnOldTransactionId" runat="server" Value='<%# Eval("TransactionId") %>' />
                                                            <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status") %>' />
                                                            <asp:HiddenField ID="hdnLandingDate" runat="server" Value='<%# Eval("LandingDate") %>' />
                                                            <asp:HiddenField ID="hdnLMDT" runat="server" Value='<%# Eval("LMDT") %>' />
                                                            <asp:Label ID="lblContainerNo" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContainerType" runat="server" Text='<%# Eval("ContainerType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContainerSize" runat="server" Text='<%# Eval("Size")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkContainer" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="GrayText" />
                                                <RowStyle Wrap="true" />
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        <asp:Button ID="btnProceed" runat="server" Text="Proceed" OnClick="btnProceed_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    </fieldset>
                                </center>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvSelectedContainer" runat="server" AutoGenerateColumns="false"
                                ShowFooter="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Container No">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnOldTransactionId" runat="server" Value='<%# Eval("OldTransactionId") %>' />
                                            <asp:HiddenField ID="hdnCurrentTransactionId" runat="server" Value='<%# Eval("NewTransactionId") %>' />
                                            <%# Eval("ContainerNo")%></ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderStyle CssClass="gridviewheader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Status">
                                        <ItemTemplate>
                                            <%# Eval("FromStatus")%>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="13%" />
                                        <HeaderStyle CssClass="gridviewheader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status Date">
                                        <ItemTemplate>
                                            <%# Eval("LMDT").ToString() == "" ? " " : Eval("LMDT")%>
                                            <asp:HiddenField ID="hdnCDT" runat="server" Value='<%# Eval("LMDT").ToString() == "" ? " " : Convert.ToDateTime(Eval("LMDT")).ToShortDateString()%>' />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderStyle CssClass="gridviewheader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Status">
                                        <ItemTemplate>
                                            <%# Eval("ToStatus")%>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="13%" />
                                        <HeaderStyle CssClass="gridviewheader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusDate" runat="server" Text='<%# Eval("ChangeDate").ToString() == "" ? " " : Convert.ToDateTime(Eval("ChangeDate")).ToShortDateString()%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderStyle CssClass="gridviewheader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select All" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Select
                                            <%--<asp:CheckBox ID="chkHeader" runat="server" />--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItem" runat="server" Checked="true" Visible='<%# Convert.ToBoolean(Eval("Editable").ToString()) %>' />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="8%" />
                                        <HeaderStyle CssClass="gridviewheader_center" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="#F8F8F8" />
                                <RowStyle Wrap="true" />
                                <FooterStyle BackColor="GrayText" HorizontalAlign="Center" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <%--<td>
                        </td>--%>
                        <td style="padding-left: 23%;">
                            <%--<asp:HiddenField ID="hdnChargeID" runat="server" Value="0" />--%>
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgContainer"
                                OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button"
                                    Text="Back" ValidationGroup="vgUnknown" OnClick="btnBack_Click" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" />
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
