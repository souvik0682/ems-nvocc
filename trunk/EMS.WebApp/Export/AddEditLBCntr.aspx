<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditLBCntr.aspx.cs" Inherits="EMS.WebApp.Export.AddEditLBCntr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">

        function validateData() {

            if (document.getElementById('<%=txtBookingNo.ClientID %>').value == '') {
                alert('Please enter Booking No.');
                return false;
            }
            return true;
        }

        //        function AutoCompleteItemSelected(sender, e) {
        //            if (sender._id == "AutoCompleteBookingNo") {
        //                var hdnBookingNo = $get('<%=hdnBookingNo.ClientID %>');
        //                hdnBookingNo.value = e.get_value();
        //            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">

    <div>
        <div id="headercaption">
            ADD / EDIT LEFT BEHIND CONTAINERS</div>
        <center>
            <fieldset style="width: 60%;">
                <legend>Add / Edit Left Behind Containers</legend>
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
                                    <asp:HiddenField ID="hdnLBCntrTransactionId" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnLineID" runat="server" Value="0" />
                                </td>
                                <td>
                                    Activity Date<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <script type="text/javascript">
                                        function ChangeActivityDate(txt) {
                                            var ID = txt.id;
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
                                <td class="label" style="padding-right: 50px; vertical-align: top;">
                                    Booking No.:<span class="errormessage">*</span>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdnBookingNo" runat="server" />
                                    <asp:TextBox ID="txtBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="60" AutoPostBack="true" OnTextChanged="txtBookingNo_TextChanged"
                                        Width="250px" TabIndex="4"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvBookingNo" runat="server" ControlToValidate="txtBookingNo"
                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
 <%--                           </tr>
                            <tr>
                                <td>
                                    Booking No:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBookingNo" runat="server" Width="175" Enabled="true" AutoPostBack="true"
                                        onselectedindexchanged="ddlBookingNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBookingNo" runat="server" ErrorMessage="Please enter Booking No."
                                        ControlToValidate="ddlBookingNo" Display="None" ValidationGroup="vgContainer"
                                        Enabled="false"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="rfvBookingNo">
                                    </cc1:ValidatorCalloutExtender>
                                </td>--%>
                                <td>
                                    Load Port:
                                </td>
                                <td>
                                    <asp:Label ID="lblLoadPort" runat="server" Width="150"></asp:Label>
                                <%--    <asp:DropDownList ID="txtLoadPort" runat="server" Width="155" OnSelectedIndexChanged="ddlFromLocation_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="Please enter location"
                                        ControlToValidate="ddlLocation" Display="None" InitialValue="0" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="rfvLocation">
                                    </cc1:ValidatorCalloutExtender>--%>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>
                                    Booking Date:<%--<span class="errormessage1">*</span>--%></td>
                                <td>
                                <asp:TextBox ID="txtBookingDate" runat="server" Width="150" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtBookingDate"
                                        Format="dd/MM/yyyy" TargetControlID="txtBookingDate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvBookingDate" runat="server" ErrorMessage="Please enter date"
                                        ControlToValidate="txtBookingDate" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvBookingDate">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    Booking Party:<%--<span class="errormessage1">*</span>--%></td>
                                <td>
                                    <asp:Label ID="lblParty" runat="server" Width="150"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Current Vessel:
                                </td>
                                <td>
                                   <asp:Label ID="lblVessel" runat="server" Width="150"></asp:Label>
                                </td>
                                <td>
                                    Current Voyage:
                                </td>
                                <td>
                                    <asp:Label ID="lblVoyage" runat="server" Width="150"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    Assigned Vessel <span class="errormessage1">*</span>:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="175" AutoPostBack="true" 
                                        onselectedindexchanged="ddlVessel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvVessel" runat="server" ErrorMessage="Please select Vessel"
                                        Display="None" ControlToValidate="ddlVessel" InitialValue="0" ValidationGroup="vgContainer">
                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvVessel">
                                    </cc1:ValidatorCalloutExtender>

                                </td>
                                <td valign="top">
                                    Assigned Voyage <span class="errormessage1">*</span>:
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlVoyage" runat="server" Enabled="false" Width="155" OnSelectedIndexChanged="ddlVoyage_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvVoyage" runat="server" ErrorMessage="Please select Voyage"
                                        Display="None" ControlToValidate="ddlVoyage" InitialValue="0" ValidationGroup="vgContainer">
                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="rfvVoyage">
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
      <%--                                                      <asp:HiddenField ID="hdnContainerType" runat="server" Value='<%# Eval("ContainerType") %>' />--%>
                                                            <asp:HiddenField ID="hdnLMDT" runat="server" Value='<%# Eval("LMDT") %>' />
                                                            <asp:Label ID="lblContainerNo" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContainerSize" runat="server" Text='<%# Eval("Size")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContainerType" runat="server" Text='<%# Eval("ContainerType")%>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <%# Eval("CntrSize")%>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="13%" />
                                        <HeaderStyle CssClass="gridviewheader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <%# Eval("CntrType")%>
                                         <%--   <asp:HiddenField ID="hdnCDT" runat="server" Value='<%# Eval("LMDT").ToString() == "" ? " " : Convert.ToDateTime(Eval("LMDT")).ToShortDateString()%>' />--%>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderStyle CssClass="gridviewheader" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Vessel">
                                        <ItemTemplate>
                                            <%# Eval("Vessel")%>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="13%" />
                                        <HeaderStyle CssClass="gridviewheader" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voyage">
                                        <ItemTemplate>
                                            <%# Eval("Voyage")%>
                                         </ItemTemplate>
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderStyle CssClass="gridviewheader" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Select All" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Select
                                            <%--<asp:CheckBox ID="chkHeader" runat="server" />--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
<%--                                            <asp:CheckBox ID="chkItem" runat="server" Checked="true" Visible='<%# Convert.ToBoolean(Eval("Editable").ToString()) %>' />--%>
                                            <asp:CheckBox ID="chkItem" runat="server" Checked="true" Visible="true"/>
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
