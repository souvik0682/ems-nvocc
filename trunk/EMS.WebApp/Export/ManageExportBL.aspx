<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageExportBL.aspx.cs" Inherits="EMS.WebApp.Export.ManageExportBL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT EXPORT BL</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Export B/L</legend>
                <asp:UpdatePanel ID="upExportBL" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <cc1:TabContainer ID="tcPP" runat="server" ActiveTabIndex="0">
                                            <!-- General Tab-->
                                            <cc1:TabPanel ID="tpHeader" runat="server">
                                                <HeaderTemplate>
                                                    General Section</HeaderTemplate>
                                                <ContentTemplate>
                                                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                                        <tr>
                                                            <td style="width: 20%;">
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 20%;">
                                                            </td>
                                                            <td style="width: 28%;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Booking No:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvBookingNo" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtBookingNo" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Booking Date:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBookingDate" runat="server" CssClass="textboxuppercase" MaxLength="15"
                                                                    Width="250px" TabIndex="12"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtBookingDate" runat="server"
                                                                    Format="dd-MM-yyyy" Enabled="True" />
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvBookingDate" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtBookingDate" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                B/L No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBLNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Enabled="false" Width="250px" TabIndex="11"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                B/L Date:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBLDate" runat="server" CssClass="textboxuppercase" MaxLength="15"
                                                                    Width="250px" TabIndex="12"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="ceBLDate" TargetControlID="txtBLDate" runat="server" Format="dd-MM-yyyy"
                                                                    Enabled="True" />
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvBLDate" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtBLDate" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Booking Party:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBookingParty" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Ref Booking No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRefBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Location:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtLocation" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                             <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                Line/NVOCC:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtLine" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Vessel:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtVessel" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Voyage:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtVoyage" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                POR:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPOR" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                             <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                POL:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPOL" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                POR Description:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPorDesc" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13"></asp:TextBox><br />
                                                            </td>
                                                             <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                POL Description:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPolDesc" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13"></asp:TextBox><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                POD:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPOD" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                             <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                FPOD:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtFPOD" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                POD Description:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPodDesc" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13"></asp:TextBox><br />
                                                            </td>
                                                             <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                FPOD Description:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtFPodDesc" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13"></asp:TextBox><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Nos. Original B/L:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoShipment" runat="server" TabIndex="9" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Selected="True" Text="3" Value="3"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                             <td style="width: 28%;">
                                                                BL Type:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoBLType" runat="server" TabIndex="9" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="Original" Value="O"></asp:ListItem>
                                                                    <asp:ListItem Text="Express" Value="E"></asp:ListItem>
                                                                    <asp:ListItem Text="Seaway" Value="S"></asp:ListItem>
                                                                </asp:RadioButtonList><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Shipment Mode:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlOrigine" runat="server" CssClass="dropdownlist" TabIndex="60">
                                                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvOrigine" runat="server" ControlToValidate="ddlOrigine"
                                                                    ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Commodity:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCommodity" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Containers:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtContainers" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                BL Issue Place:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtIssuePlace" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvDestination" runat="server" ControlToValidate="txtIssuePlace"
                                                                    ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Net Weight:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtNetWt" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                B/L Release Date:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="false"></asp:TextBox><br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <!-- Other Tab-->
                                            <cc1:TabPanel ID="tpOther" runat="server">
                                                <HeaderTemplate>
                                                    Other Information</HeaderTemplate>
                                                <ContentTemplate>
                                                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                                         <tr>
                                                            <td style="width: 20%;">
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 20%;">
                                                            </td>
                                                            <td style="width: 28%;">
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td style="width: 28%;">
                                                                Shipper Name:
                                                            </td>
                                                            <td style="width: 4%;">
                                                                <asp:TextBox ID="txtShipperName" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="1" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvShipperName" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtShipperName" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>

                                                            </td>
                                                            <td style="width: 40%;">
                                                                Consignee Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtConsigneeName" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="9"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvConsigneeName" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtConsigneeName" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Shipper:
                                                            </td>
                                                            <td style="width: 4%;">
                                                                <asp:TextBox ID="txtShipper" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="1" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvShipper" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtShipper" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                Consignee:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtConsignee" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="9" TextMode="MultiLine" Rows="4"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvConsignee" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtConsignee" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Notify Party Name:
                                                            </td>
                                                            <td style="width: 4%;">
                                                                <asp:TextBox ID="TextNotifyName" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="1" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvNotifyName" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="TextNotifyName" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                BL Clause:
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlBLClause" runat="server" CssClass="dropdownlist" TabIndex="60">
                                                                    <asp:ListItem  Selected="True" Text="Shipped On Board" Value="S"></asp:ListItem>
                                                                    <asp:ListItem Text="Receipt of Shipment" Value="R"></asp:ListItem>
<%--                                                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Notify Party:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtNotify" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="1" TextMode="MultiLine" Rows="4"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvNotify" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtNotify" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                Description of Goods:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtGoodsDescription" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="8" Text="N/M" TextMode="MultiLine" Rows="4"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvGoodsDescription" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtGoodsDescription" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Marks & Nos:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 4%;">
                                                                <asp:TextBox ID="txtMarks" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="5" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvMarks" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtMarks" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Delivery Agent:
                                                            </td>
                                                            <td style="width: 4%;">
                                                                <asp:DropDownList ID="ddlAgent" runat="server" CssClass="dropdownlist" TabIndex="60">
                                                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAgent"
                                                                    ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                           <%-- <td style="width: 28%;">
                                                                BL Type:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" TabIndex="9" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="Original" Value="O"></asp:ListItem>
                                                                    <asp:ListItem Text="Express" Value="E"></asp:ListItem>
                                                                    <asp:ListItem Text="Seaway" Value="S"></asp:ListItem>
                                                                </asp:RadioButtonList><br />
                                                            </td>--%>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <!-- Container Tab-->
                                            <cc1:TabPanel ID="tpFooter" runat="server">
                                                <HeaderTemplate>
                                                    Container Section</HeaderTemplate>
                                                <ContentTemplate>
                                                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                                    <tr>
                                                            <td>
                                                                Booking No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Booking Date:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCBookingDate" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                B/L No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCBLNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Enabled="false" Width="250px" TabIndex="11"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                B/L Date:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCBLDate" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Total TEU:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCTeu" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Enabled="false" Width="250px" TabIndex="11"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Total FEU:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCFeu" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                Total Ton:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCTon" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Enabled="false" Width="250px" TabIndex="11"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Total CBM:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCCbm" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:GridView ID="gvwContainers" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                        BorderStyle="None" BorderWidth="0" Width="100%">
                                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                        <PagerStyle CssClass="gridviewpager" />
                                                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                        <EmptyDataTemplate>
                                                            No Page(s) Found
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Container No">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type/Size">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Seal No">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Package">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tare Weight">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gross Weight">
                                                                <HeaderStyle CssClass="gridviewheader_num" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Part">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shipping Bill No">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shipping Bill Date">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
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
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                        </cc1:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="70" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="71" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" />
                                        <br />
                                        <asp:Label ID="lblErr" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressBL" runat="server" AssociatedUpdatePanelID="upExportBL">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </center>
</asp:Content>
