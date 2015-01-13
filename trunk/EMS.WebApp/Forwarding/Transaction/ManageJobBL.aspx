﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageJobBL.aspx.cs" Inherits="EMS.WebApp.Farwarding.Transaction.ManageJobBL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .ErrorControl
        {
            background-color: #EE3B3B;
            border: solid 1px Red;
        }
    </style>
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
    <div id="headercaption">
        ADD / EDIT FORWARDING BL</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Forwarding B/L</legend>
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
                                                    General Section
                                                </HeaderTemplate>
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
                                                                Job No:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:HiddenField ID="hdnBLThruEdge" runat="server" />
                                                                <asp:HiddenField ID="hdnShipmentType" runat="server" />
                                                                <asp:TextBox ID="txtBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvBookingNo" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtBookingNo" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Job Date:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBookingDate" runat="server" CssClass="textboxuppercase" MaxLength="15"
                                                                    Width="250px" TabIndex="12" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                House B/L No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBLNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    AutoPostBack="True" OnTextChanged="txtBLNo_TextChanged" Enabled="False" Width="250px"
                                                                    TabIndex="11"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                HBL Date:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBLDate" runat="server" CssClass="textboxuppercase" MaxLength="15"
                                                                    Width="250px" TabIndex="12" AutoPostBack="True"></asp:TextBox>
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
                                                                Party:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBookingParty" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Ref B/L No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRefBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Location:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtLocation" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                Line/NVOCC:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtLine" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Vessel:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtVessel" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Voyage:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtVoyage" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                POR:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPOR" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                POL:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPOL" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                POR Description:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPorDesc" runat="server" CssClass="textboxuppercase" MaxLength="300"
                                                                    Width="250px" TabIndex="13"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                POL Description:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPolDesc" runat="server" CssClass="textboxuppercase" MaxLength="300"
                                                                    Width="250px" TabIndex="13"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                POD:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPOD" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                FPOD:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtFPOD" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                POD Description:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtPodDesc" runat="server" CssClass="textboxuppercase" MaxLength="300"
                                                                    Width="250px" TabIndex="13"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                FPOD Description:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtFPodDesc" runat="server" CssClass="textboxuppercase" MaxLength="300"
                                                                    Width="250px" TabIndex="13"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Nos. Original B/L:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoOriginal" runat="server" TabIndex="9" RepeatDirection="Horizontal">
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
                                                                    </asp:RadioButtonList>
                                                                    <br />
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Shipment Mode:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlShipmentMode" runat="server" CssClass="dropdownlist" TabIndex="60">
                                                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvShipmentMode" runat="server" ControlToValidate="ddlShipmentMode"
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
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Containers:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtContainers" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" Enabled="False"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                BL Issue Place:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <uc1:AC_Port ID="txtIssuePlace" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Gross Weight:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <cc2:CustomTextBox ID="txtNetWt" runat="server" CssClass="numerictextbox" TabIndex="13"
                                                                    Width="250px" Type="Decimal" MaxLength="15" Precision="6" Scale="3"></cc2:CustomTextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                BL Clause:
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlBLClause" runat="server" CssClass="dropdownlist" TabIndex="60"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBLClause_OnSelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Text="Shipped On Board" Value="S"></asp:ListItem>
                                                                    <asp:ListItem Text="Received For Shipment" Value="R"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Net Weight:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <cc2:CustomTextBox ID="TxtNtWt" runat="server" CssClass="numerictextbox" TabIndex="13"
                                                                    Width="250px" Type="Decimal" MaxLength="15" Precision="6" Scale="3"></cc2:CustomTextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                RFS B/L Date:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtBLReleaseDate" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13" AutoPostBack="True" OnTextChanged="txtBLReleaseDate_TextChanged"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="ceReleaseDate" TargetControlID="txtBLReleaseDate" runat="server"
                                                                    Format="dd-MM-yyyy" Enabled="True" />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <!-- Other Tab-->
                                            <cc1:TabPanel ID="tpOther" runat="server">
                                                <HeaderTemplate>
                                                    Other Information
                                                </HeaderTemplate>
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
                                                                Job No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOJobNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Job Date:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOJobDate" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                B/L No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOBLNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Enabled="false" Width="250px" TabIndex="11"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                B/L Date:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOBLDate" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Shipper Name:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 4%;">
                                                                <asp:TextBox ID="txtShipperName" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="1"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvShipperName" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtShipperName" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 40%;">
                                                                Consignee Name:<span class="errormessage">*</span>
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
                                                                <%--  <asp:RequiredFieldValidator ID="rfvShipper" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtShipper" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                Consignee:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtConsignee" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="9" TextMode="MultiLine" Rows="4"></asp:TextBox><br />
                                                                <%-- <asp:RequiredFieldValidator ID="rfvConsignee" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtConsignee" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Notify Party Name:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 4%;">
                                                                <asp:TextBox ID="txtNotifyName" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="1"></asp:TextBox>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvNotifyName" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtNotifyName" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                Delivery Agent:
                                                            </td>
                                                            <td style="width: 4%;">
                                                                <asp:DropDownList ID="ddlAgent" runat="server" CssClass="dropdownlist" TabIndex="60">
                                                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvAgent" runat="server" ControlToValidate="ddlAgent"
                                                                    ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Notify Party:
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
                                                                <asp:TextBox ID="txtGoodsDescription" runat="server" CssClass="textboxuppercase"
                                                                    MaxLength="50" Width="300" TabIndex="8" TextMode="MultiLine" Rows="4"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvGoodsDescription" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtGoodsDescription"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Marks & Nos:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 4%;">
                                                                <asp:TextBox ID="txtMarks" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="300" TabIndex="5" TextMode="MultiLine" Rows="3" Text="N/M"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvMarks" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtMarks" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <!-- Container Tab-->
                                            <cc1:TabPanel ID="tpFooter" runat="server">
                                                <HeaderTemplate>
                                                    Container Section
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                                        <tr>
                                                            <td>
                                                                Job No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCBookingNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Job Date:
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
                                                                <asp:TextBox ID="txtTeu" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Enabled="false" Width="250px" TabIndex="11"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Total FEU:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFeu" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Total Ton:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTon" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Enabled="false" Width="250px" TabIndex="11"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Total CBM:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCbm" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:GridView ID="gvwContainers" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="3" DataKeyNames="ContainerId" OnRowDataBound="gvwContainers_RowDataBound">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <%--<asp:BoundField DataField="HireContainerNumber" HeaderText="Container No" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="HireContainerNumber" HeaderStyle-Width="150" />--%>
                                                            <asp:TemplateField HeaderText="Container No" SortExpression="HireContainerNumber"
                                                                HeaderStyle-Width="150">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtHireContainerNumber" runat="server" CssClass="textboxuppercase"
                                                                        Text='<%# Bind("HireContainerNumber") %>' Width="80" BorderStyle="None" MaxLength="10">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv100" runat="server" ControlToValidate="txtHireContainerNumber"
                                                                        Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="ContainerSize" HeaderText="Size" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="ContainerSize" />--%>
                                                            <asp:TemplateField HeaderText="Size" SortExpression="ContainerSize" HeaderStyle-Width="150">
                                                                <ItemTemplate>
                                                                    <%--<asp:TextBox ID="txtContainerSize" runat="server" CssClass="textboxuppercase" Text='<%# Bind("ContainerSize") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10">
                                                                    </asp:TextBox>--%>
                                                                    <%--<asp:RequiredFieldValidator ID="rfv101" runat="server" ControlToValidate="txtContainerSize" Display="Dynamic" 
                                                                    ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                                    <asp:DropDownList ID="ddlContainerSize" runat="server"  OnSelectedIndexChanged="ddlContainerSize_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Text="20" Value="20" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="ContainerType" HeaderText="Type" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="ContainerType" />--%>
                                                            <asp:TemplateField HeaderText="Type" SortExpression="ContainerType" HeaderStyle-Width="150">
                                                                <ItemTemplate>
                                                                    <%--<asp:TextBox ID="txtContainerType" runat="server" CssClass="textboxuppercase" Text='<%# Bind("ContainerType") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv102" runat="server" ControlToValidate="txtContainerType" Display="Dynamic" 
                                                                    ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                                    <asp:DropDownList ID="ddlContainerType" runat="server" OnSelectedIndexChanged="ddlContainerType_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfv112" runat="server" ControlToValidate="ddlContainerType"
                                                                        Display="Dynamic" ValidationGroup="Save" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Seal Number" SortExpression="Seal Number" HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSealNo" runat="server" CssClass="textboxuppercase" Text='<%# Bind("SealNumber") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtSealNo"
                                                                        Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Package" SortExpression="Package" HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <cc2:CustomTextBox ID="txtPackage" runat="server" Text='<%# Bind("Package") %>' Width="80"
                                                                        BorderStyle="None" Style="text-align: right;" MaxLength="8" Type="Numeric">
                                                                    </cc2:CustomTextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtPackage"
                                                                        Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlUnit" runat="server">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--                                                            <asp:BoundField DataField="TareWeight" HeaderText="Tare Weight" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="TareWeight" />--%>
                                                            <asp:TemplateField HeaderText="Tare Weight" SortExpression="TareWeight" HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <cc2:CustomTextBox ID="txtTareWeight" runat="server" Text='<%# Bind("TareWeight", "{0:n3}") %>'
                                                                        Style="text-align: right;" Width="80" BorderStyle="None" MaxLength="12" AutoPostBack="true"
                                                                        Precision="8" Scale="3" Type="Decimal" Enabled="false">
                                                                    </cc2:CustomTextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv110" runat="server" ControlToValidate="txtTareWeight"
                                                                        Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gross Weight (KG)" SortExpression="GrossWeight" HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <cc2:CustomTextBox ID="txtGrossWeight" runat="server" Text='<%# Bind("GrossWeight", "{0:n3}") %>'
                                                                        Style="text-align: right;" Width="80" BorderStyle="None" MaxLength="12" AutoPostBack="true"
                                                                        Precision="8" Scale="3" Type="Decimal" OnTextChanged="txtGrossWeight_TextChanged">
                                                                    </cc2:CustomTextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvGrossWeight" runat="server" ControlToValidate="txtGrossWeight"
                                                                        Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Part">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlPart" runat="server">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shipping Bill No" SortExpression="ShippingBillNumber"
                                                                HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtShippingBillNumber" runat="server" CssClass="textboxuppercase"
                                                                        Text='<%# Bind("ShippingBillNumber") %>' Width="80" BorderStyle="None" MaxLength="10">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtShippingBillNumber"
                                                                        Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shipping Bill Date" SortExpression="ShippingBillDate"
                                                                HeaderStyle-Width="100">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtShippingBillDate" runat="server" Text='<%# Bind("ShippingBillDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceBillDate" TargetControlID="txtShippingBillDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvShippingDate" runat="server" ControlToValidate="txtShippingBillDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="txtShippingBillDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IsDeleted" HeaderText="Is Deleted" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="IsDeleted" HeaderStyle-Width="120" Visible="false" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnRemove" runat="server" OnClick="btnRemove_Click" ImageUrl="~/Images/remove.png"
                                                                        Height="16" Width="16" />
                                                                    <asp:HiddenField ID="hdnContainerId" runat="server" Value='<%# Eval("ContainerId") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                        </cc1:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="70"
                                            OnClick="btnSave_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="71" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" OnClick="btnBack_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" CssClass="button" TabIndex="71" OnClientClick="javascript:if(!confirm('Want to Close?')) return false;"
                                            Text="Close" OnClick="btnClose_Click" />
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