<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageBooking.aspx.cs" Inherits="EMS.WebApp.Export.ManageBooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 85px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT BOOKING</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Booking</legend>
                <asp:UpdatePanel ID="upBooking" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                <tr>
                                    <td style="width: 20%;">
                                        Line/NVOCC<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:DropDownList ID="ddlNvocc" runat="server" CssClass="dropdownlist" TabIndex="1">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvNvocc" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="ddlNvocc" InitialValue="0"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 20%;">
                                        Location
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Booking Party<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBookingParty" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="errBookingParty" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                    <td>
                                        Accounts<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAccounts" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                            Width="250px" TabIndex="5"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="rfvAccounts" runat="server" ControlToValidate="txtAccounts"
                                            Display="Dynamic" CssClass="errormessage" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Booking Number<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                            Width="250px" TabIndex="5" Enabled="false"></asp:TextBox><br />
                                    </td>
                                    <td>
                                        Booking Date<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBookingDate" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cbeBookingDate" TargetControlID="txtBookingDate" runat="server"
                                            Format="dd-MM-yyyy" Enabled="True" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvBookingDate" runat="server" ControlToValidate="txtBookingDate"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ref Booking No
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRefBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                            Width="250px" TabIndex="8"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="rfvRefBookingNo" runat="server" ControlToValidate="txtRefBookingNo"
                                            ErrorMessage="This field is required" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic" Visible="False"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        Ref Booking Date<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRefBookingDate" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cbeRefBookingNo" TargetControlID="txtRefBookingDate" runat="server"
                                            Format="dd-MM-yyyy" Enabled="True" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfbRefBookingDate" runat="server" ControlToValidate="txtRefBookingDate"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Place of Receipyt
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>
                                    </td>
                                    <td>
                                        Port of Loading
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port of Discharge
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>
                                    </td>
                                    <td>
                                        Final Destination
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commodity
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommodity" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6"></asp:TextBox>
                                    </td>
                                    <td>
                                        Shipment Type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlShipmentType" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Loading Vessel
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLoadingVessel" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>
                                    </td>
                                    <td>
                                        Loading Voyage
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLoadingVoyage" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mainline Vessel
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMainLineVessel" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>
                                    </td>
                                    <td>
                                        Mainline Voyage
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMainLineVoyage" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6" Test="Auto Correct"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        B/L Through Edge
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBLEdge" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        HAZ Cargo
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlHAZCargo" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        IMO<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtImo" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        UNO<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUno" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Weight<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <cc2:CustomTextBox ID="txtGrossSeight" runat="server" CssClass="numerictextbox" Width="250px"
                                            Type="Decimal" MaxLength="13" Precision="10" Scale="2">
                                        </cc2:CustomTextBox>
                                    </td>
                                    <td>
                                        CBM<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <cc2:CustomTextBox ID="txtCbm" runat="server" CssClass="numerictextbox" Width="250px"
                                            Type="Decimal" MaxLength="13" Precision="10" Scale="2">
                                        </cc2:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Service<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlService" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Reefer
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlReefer" runat="server" CssClass="dropdownlist" TabIndex="2">
                                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkContainerDtls" runat="server" Text="Container Details" OnClick="lnkContainerDtls_Click"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContainerDtls" runat="server" CssClass="textboxuppercase" Enabled="False"
                                            TabIndex="27"></asp:TextBox>
                                    </td>
                                    <td>
                                        Temp Min<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <cc2:CustomTextBox ID="txtTempMin" runat="server" CssClass="numerictextbox" Width="250px"
                                            Type="Decimal" MaxLength="7" Precision="4" Scale="2"></cc2:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkTransitRoute" runat="server" Text="Transit Route" OnClick="lnkTransitRoute_Click"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTransitRoute" runat="server" CssClass="textboxuppercase" Enabled="False"
                                            TabIndex="27"></asp:TextBox>
                                    </td>
                                    <td>
                                        Temp Max<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <cc2:CustomTextBox ID="txtTempMax" runat="server" CssClass="numerictextbox" Width="250px"
                                            Type="Decimal" MaxLength="7" Precision="4" Scale="2"></cc2:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="70" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="71" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" onclick="btnBack_Click" />
                                        <br />
                                        <asp:Label ID="lblError" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- Modal Popup Container Details -->
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Style="display: none;" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                                PopupControlID="pnlContainer" Drag="true" BackgroundCssClass="ModalPopupBG" CancelControlID="btnCancelContainer">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlContainer" runat="server" Style="height: 250px; width: 400px; background-color: White;">
                                <center>
                                    <fieldset>
                                        <legend>Container Breakup</legend>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td style="width: 5%;">
                                                        <asp:TextBox ID="TextBox5" Text="Type" runat="server" Width="80px"></asp:TextBox>
                                                    </td>
                                                     <td>
                                                        <asp:TextBox ID="TextBox6" Text="Size" runat="server" Width="86px"></asp:TextBox>
                                                    </td>
                                                     <td class="style1">
                                                        <asp:TextBox ID="TextBox7" Text="Units" runat="server" Width="77px"></asp:TextBox>
                                                    </td>
                                                     <td>
                                                        <asp:TextBox ID="TextBox8" Text="Wt/Cont." runat="server" Width="82px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button3" runat="server" Text="Add" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="overflow: auto; height: 180px; width: 380px;">
                                            <asp:GridView ID="gvContainer" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                BorderStyle="None" BorderWidth="0" Width="100%" style="margin-right: 0px">
                                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                <PagerStyle CssClass="gridviewpager" />
                                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                <EmptyDataTemplate>
                                                    No Page(s) Found
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <HeaderStyle CssClass="gridviewheader" />
                                                        <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size">
                                                        <HeaderStyle CssClass="gridviewheader_num" />
                                                        <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Units">
                                                        <HeaderStyle CssClass="gridviewheader_num" />
                                                        <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WT/CONT.">
                                                        <HeaderStyle CssClass="gridviewheader_num" />
                                                        <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
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
                                        </div>
                                        <br />
                                        <asp:Button ID="btnSaveContainer" runat="server" Text="Save" />
                                        <asp:Button ID="btnCancelContainer" runat="server" Text="Cancel" />
                                    </fieldset>
                                </center>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <!-- Modal Popup Container Details -->
                <!-- Modal Popup Transit Route -->
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="Button2" runat="server" Style="display: none;" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2"
                                PopupControlID="pnlTransit" Drag="true" BackgroundCssClass="ModalPopupBG" CancelControlID="btnCalcelTransit">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlTransit" runat="server" Style="height: 250px; width: 400px; background-color: White;">
                                <center>
                                    <fieldset>
                                        <legend>Transit Route</legend>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox Text="Ports" runat="server" Width="260px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAddToGrid" runat="server" Text="Add" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="overflow: auto; height: 180px; width: 380px;">
                                            <asp:GridView ID="gvTransit" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                BorderStyle="None" BorderWidth="0" Width="100%">
                                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                <PagerStyle CssClass="gridviewpager" />
                                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                <EmptyDataTemplate>
                                                    No Page(s) Found
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Serial No">
                                                        <HeaderStyle CssClass="gridviewheader" />
                                                        <ItemStyle CssClass="gridviewitem" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ports">
                                                        <HeaderStyle CssClass="gridviewheader_num" />
                                                        <ItemStyle CssClass="gridviewitem" Width="50%" HorizontalAlign="Right" />
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
                                        </div>
                                        <br />
                                        <asp:Button ID="btnSaveTransit" runat="server" Text="Save" />
                                        <asp:Button ID="btnCalcelTransit" runat="server" Text="Cancel" />
                                    </fieldset>
                                </center>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <!-- Modal Popup Transit Route -->
            </fieldset>
            <asp:UpdateProgress ID="uProgressBooking" runat="server" AssociatedUpdatePanelID="upBooking">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </center>
</asp:Content>
