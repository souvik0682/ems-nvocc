﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="container-movement-entry.aspx.cs" Inherits="EMS.WebApp.Equipment.container_movement_entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div id="headercaption">
                    ADD / EDIT CONTAINER MOVEMENT</div>
                <center>
                    <fieldset style="width: 70%;">
                        <legend>Add / Edit Container Transaction</legend>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td style="width: 150px;">
                                    Transaction CODE :
                                </td>
                                <td style="width: 375px;">
                                    <asp:Label ID="lblTranCode" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnContainerTransactionId" runat="server" Value="0" />
                                </td>
                                <td>
                                    Activity Date<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" Width="250"></asp:TextBox>
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
                                    <asp:DropDownList ID="ddlFromStatus" runat="server" Width="255" OnSelectedIndexChanged="ddlFromStatus_SelectedIndexChanged"
                                        AutoPostBack="true">
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
                                    <asp:DropDownList ID="ddlToStatus" runat="server" Width="255" AutoPostBack="True"
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
                                    <asp:DropDownList ID="ddlFromLocation" runat="server" Width="255" OnSelectedIndexChanged="ddlFromLocation_SelectedIndexChanged"
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
                                    <asp:DropDownList ID="ddlTolocation" runat="server" Width="255" Enabled="false">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvToLocation" runat="server" ErrorMessage="Please enter to location"
                                        ControlToValidate="ddlTolocation" Display="None" ValidationGroup="vgContainer"
                                        Enabled="false"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="rfvToLocation">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    No. Of TEUs<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTeus" runat="server" Width="250" Style="text-align: right;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtTeus"
                                        FilterMode="ValidChars" FilterType="Numbers">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvTeus" runat="server" ErrorMessage="Please enter total no of TEUs"
                                        ControlToValidate="txtTeus" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="rfvTeus">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    No. Of FEUs<span class="errormessage1">*</span> :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFEUs" runat="server" Width="250" Style="text-align: right;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFEUs"
                                        FilterMode="ValidChars" FilterType="Numbers">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvFeus" runat="server" ErrorMessage="Please enter total no of FEUs"
                                        ControlToValidate="txtFEUs" Display="None" ValidationGroup="vgContainer"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="rfvFeus">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Narration :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNarration" runat="server" Width="250" TextMode="MultiLine" Style="text-transform: uppercase;"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    Empty Yard <span class="errormessage1">*</span>:
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlEmptyYard" runat="server" Enabled="false" Width="255" OnSelectedIndexChanged="ddlEmptyYard_SelectedIndexChanged"
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
                                <td colspan="3">
                                </td>
                                <td>
                                    <asp:Button ID="btnShow" runat="server" Text="Show Container" OnClick="btnShow_Click"
                                        ValidationGroup="vgContainer" />
                                    <asp:Button ID="Button1" runat="server" Style="display: none;" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                                        PopupControlID="pnlContainer" Drag="true" BackgroundCssClass="ModalPopupBG" CancelControlID="btnCancel">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvSelectedContainer" runat="server" AutoGenerateColumns="false"
                                        ShowFooter="false" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Container No">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnOldTransactionId" runat="server" Value='<%# Eval("OldTransactionId") %>' />
                                                    <asp:HiddenField ID="hdnCurrentTransactionId" runat="server" Value='<%# Eval("NewTransactionId") %>' />
                                                    <%# Eval("ContainerNo")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Previous Status">
                                                <ItemTemplate>
                                                    <%# Eval("FromStatus")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Landing Date">
                                                <ItemTemplate>
                                                    <%# Eval("LandingDate").ToString() == "" ? " " : Convert.ToDateTime(Eval("LandingDate")).ToShortDateString()%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Status">
                                                <ItemTemplate>
                                                    <%# Eval("ToStatus")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of Change">
                                                <ItemTemplate>
                                                    <%# Eval("ChangeDate").ToString() == "" ? " " : Convert.ToDateTime(Eval("ChangeDate")).ToShortDateString()%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select All" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    Select
                                                    <%--<asp:CheckBox ID="chkHeader" runat="server" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItem" runat="server" Checked="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="GrayText" />
                                        <RowStyle Wrap="true" />
                                        <FooterStyle BackColor="GrayText" HorizontalAlign="Center" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
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
        </ContentTemplate>
    </asp:UpdatePanel>
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
                                    <asp:Label ID="lblContainerNo" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
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
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
            </fieldset>
        </center>
    </asp:Panel>
</asp:Content>