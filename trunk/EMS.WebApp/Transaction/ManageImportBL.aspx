<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageImportBL.aspx.cs" Inherits="EMS.WebApp.Transaction.ManageImportBL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Src="~/CustomControls/AC_Vessel.ascx" TagName="AC_Vessel" TagPrefix="uc1" %>
<%@ Register Src="~/CustomControls/AC_PackageUnit.ascx" TagName="AC_PackageUnit"
    TagPrefix="uc2" %>
<%@ Register Src="~/CustomControls/AC_VolumeUnit.ascx" TagName="AC_VolumeUnit" TagPrefix="uc3" %>
<%@ Register Src="~/CustomControls/AC_WeightUnit.ascx" TagName="AC_WeightUnit" TagPrefix="uc4" %>
<%@ Register Src="~/CustomControls/AC_Surveyor.ascx" TagName="AC_Surveyor" TagPrefix="uc5" %>
<%@ Register Src="~/CustomControls/AC_DeliveryTo.ascx" TagName="AC_DeliveryTo" TagPrefix="uc6" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc7" %>
<%@ Register Src="~/CustomControls/AC_CANotice.ascx" TagName="AC_CANotice" TagPrefix="uc8" %>
<%@ Register Src="~/CustomControls/AC_Consignee.ascx" TagName="AC_Consignee" TagPrefix="uc9" %>
<%@ Register Src="~/CustomControls/AC_NParty.ascx" TagName="AC_NParty" TagPrefix="uc10" %>
<%@ Register Src="~/CustomControls/AC_Shipper.ascx" TagName="AC_Shipper" TagPrefix="uc11" %>
<%@ Register Src="~/CustomControls/AC_CFSCode.ascx" TagName="AC_CFSCode" TagPrefix="uc12" %>
<%@ Register src="~/CustomControls/AC_CHA.ascx" tagname="AC_CHA" tagprefix="uc13" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
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

        function PopulateFooter() {
            document.getElementById('<%=txtFtrBLNo.ClientID %>').value = document.getElementById('<%=txtLineBL.ClientID %>').value;
            document.getElementById('<%=txtFtrBLGrossWeight.ClientID %>').value = document.getElementById('<%=txtGrossWeight.ClientID %>').value;
            document.getElementById('<%=txtFtrPackages.ClientID %>').value = document.getElementById('<%=txtPackage.ClientID %>').value;

            SetSelectedCargoType();

            if (document.getElementById('<%=txtFtrCommodity.ClientID %>').value == '')
                document.getElementById('<%=txtFtrCommodity.ClientID %>').value = document.getElementById('<%=txtCommodity.ClientID %>').value;
        }


        function SetSelectedCargoType() {
            //alert(document.getElementById('<%=txtTransinfo.ClientID %>').value);
//            if (document.getElementById('<%=txtTransinfo.ClientID %>').value == '') {
                //alert('Test');
                var list = document.getElementById('<%= rdoCargoType.ClientID %>'); //Client ID of the radiolist
                var inputs = list.getElementsByTagName("input");
                var selected;

                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].checked) {
                        selected = inputs[i];
                        break;
                    }
                }
                if (selected) {
                    //alert(selected.value);
                    if (selected.value == 'F') {
                        document.getElementById('<%=txtFtrCargoType.ClientID %>').value = 'FCL';
                    }
                    else if (selected.value == 'L') {
                        document.getElementById('<%=txtFtrCargoType.ClientID %>').value = 'LCL';

                        document.getElementById('<%=txtFtrGrossWeight.ClientID %>').value = document.getElementById('<%=txtGrossWeight.ClientID %>').value;
                        document.getElementById('<%=txtFtrPackage.ClientID %>').value = document.getElementById('<%=txtPackage.ClientID %>').value;
                    }
                    else if (selected.value == 'E') {
                        document.getElementById('<%=txtFtrCargoType.ClientID %>').value = 'ETY';
                    }
                    else if (selected.value == 'N') {
                        document.getElementById('<%=txtFtrCargoType.ClientID %>').value = 'None';
                    }
                }

                //Transhipment Info
                document.getElementById('<%=txtTransinfo.ClientID %>').value = "CARGO LOADED FROM " + document.getElementById('<%=hdnPortLoading.ClientID %>').value + "  TO BE DISCHARGED AT " + document.getElementById('<%=hdnPortDischarge.ClientID %>').value;
//            }
        } 
</script>

    <div id="headercaption">
        ADD / EDIT IMPORT BL</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Import B/L</legend>
                <asp:UpdatePanel ID="upImportBL" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <cc1:TabContainer ID="tcPP" runat="server" ActiveTabIndex="0" onclick="PopulateFooter();">
                                            <!-- Header Tab-->
                                            <cc1:TabPanel ID="tpHeader" runat="server">
                                                <HeaderTemplate>
                                                    B/L Header</HeaderTemplate>
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
                                                            <td style="width: 20%;">
                                                                Line/NVOCC:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:DropDownList ID="ddlNvocc" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                                                    TabIndex="1" OnSelectedIndexChanged="ddlNvocc_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvNvocc" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="ddlNvocc" InitialValue="0"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 20%;">
                                                                Location:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                    OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Vessel:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <uc1:AC_Vessel ID="AC_Vessel1" runat="server" />
                                                                <asp:Label ID="errVessel" runat="server" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Voyage:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlVoyage" runat="server" CssClass="dropdownlist" TabIndex="4">
                                                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="revVoyage" runat="server" ControlToValidate="ddlVoyage"
                                                                    ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                IGM B/L Number:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLineBL" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="5" OnTextChanged="txtLineBL_TextChanged" AutoPostBack="True"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvLineBL" runat="server" ControlToValidate="txtLineBL"
                                                                    Display="Dynamic" CssClass="errormessage" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                <asp:Label runat="server" ID="errBL" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td>
                                                                IGM B/L Date:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLineBLDate" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="250px" TabIndex="6" OnTextChanged="txtLineBLDate_TextChanged" 
                                                                    AutoPostBack="True"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="cbeLineBLDate" TargetControlID="txtLineBLDate" runat="server"
                                                                    Format="dd-MM-yyyy" Enabled="True" />
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvLineBLDate" runat="server" ControlToValidate="txtLineBLDate"
                                                                    ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Line B/L Type:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoLineBLType" runat="server" TabIndex="7" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="OBL" Value="OB"></asp:ListItem>
                                                                    <asp:ListItem Text="Exp Release" Value="ER"></asp:ListItem>
                                                                    <asp:ListItem Text="Seaway" Value="SE"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Line B/L Vessel Details:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLineBLVessel" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                                                    Width="250px" TabIndex="8"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvLineBLVessel" runat="server" ControlToValidate="txtLineBLVessel"
                                                                    ErrorMessage="This field is required" CssClass="errormessage" ValidationGroup="Save"
                                                                    Display="Dynamic" Visible="False"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Nature of Cargo:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoNatureCargo" runat="server" TabIndex="9" RepeatDirection="Horizontal"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="rdoNatureCargo_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Text="C" Value="C"></asp:ListItem>
                                                                    <asp:ListItem Text="DB" Value="DB"></asp:ListItem>
                                                                    <asp:ListItem Text="LB" Value="LB"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Cargo Type:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoCargoType" runat="server" TabIndex="10" RepeatDirection="Horizontal"
                                                                    OnSelectedIndexChanged="rdoCargoType_SelectedIndexChanged" AutoPostBack="True">
                                                                    <asp:ListItem Selected="True" Text="FCL" Value="F"></asp:ListItem>
                                                                    <asp:ListItem Text="LCL" Value="L"></asp:ListItem>
                                                                    <asp:ListItem Text="ETY" Value="E"></asp:ListItem>
                                                                    <asp:ListItem Text="None" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Line B/L No:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtIgmBLNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                                                    Width="250px" TabIndex="11" OnTextChanged="txtIgmBLNo_TextChanged" 
                                                                    AutoPostBack="True"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvIgmBLNo" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtIgmBLNo" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Line B/L Date:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtIgmBLDate" runat="server" CssClass="textboxuppercase" MaxLength="15"
                                                                    Width="250px" TabIndex="12"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="ceIgmBLDate" TargetControlID="txtIgmBLDate" runat="server"
                                                                    Format="dd-MM-yyyy" Enabled="True" />
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvIgmBLDate" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtIgmBLDate" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Line/Item Prefix:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLinePrefix" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                                                    Width="250px" TabIndex="13"></asp:TextBox><br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Line/Item No:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtLINo" runat="server" CssClass="numerictextbox" MaxLength="6"
                                                                    Width="250px" TabIndex="14"></cc2:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Bill of Entry No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBillEntery" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="250px" TabIndex="15"></asp:TextBox><br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Sub Line No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSublineNo" runat="server" CssClass="textboxuppercase" Width="250px" MaxLength="6"
                                                                    Text="0" TabIndex="16" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Fright Type:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList runat="server" ID="rdoFrightType" TabIndex="16" RepeatDirection="Horizontal"
                                                                    OnSelectedIndexChanged="rdoFrightType_SelectedIndexChanged" AutoPostBack="True">
                                                                    <asp:ListItem Selected="True" Text="Pre Paid" Value="PP"></asp:ListItem>
                                                                    <asp:ListItem Text="To Collect" Value="TC"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Fright To Collect In USD:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFrightToCollect" runat="server" CssClass="numerictextbox"
                                                                    Width="250px" TabIndex="18" Type="Decimal" Enabled="False"
                                                                    MaxLength="13" Precision="10" Scale="2" ></cc2:CustomTextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFrightToCollect" runat="server" ControlToValidate="txtFrightToCollect"
                                                                    ErrorMessage="This field is required" Visible="False" CssClass="errormessage"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Freight Payable At:
                                                            </td>
                                                            <td>
                                                                <uc7:AC_Port ID="AC_Port5" runat="server" />
                                                                <asp:Label ID="errFreight" runat="server" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Stock Location:
                                                            </td>
                                                            <td>
                                                                 <asp:DropDownList ID="ddlStockLocation" runat="server" CssClass="dropdownlist" TabIndex="20"
                                                                    OnSelectedIndexChanged="ddlStockLocation_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                B/L Issue Port:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <uc7:AC_Port ID="AC_Port1" runat="server" />
                                                                <asp:Label ID="errIssuePort" runat="server" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Port of Loading:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <uc7:AC_Port ID="AC_Port2" runat="server" />
                                                                <asp:Label ID="errPortOfLoading" runat="server" CssClass="errormessage"></asp:Label>
                                                                <asp:HiddenField ID="hdnPortLoading" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Port of Discharge:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <uc7:AC_Port ID="AC_Port3" runat="server" />
                                                                <asp:Label ID="errPortOfDischarge" runat="server" CssClass="errormessage"></asp:Label>
                                                                <asp:HiddenField ID="hdnPortDischarge" runat="server" />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Final destination:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <uc7:AC_Port ID="AC_Port4" runat="server" />
                                                                <asp:Label ID="errFinalDestination" runat="server" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Commodity:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCommodity" runat="server" CssClass="textboxuppercase" MaxLength="20"
                                                                    Width="250px" TabIndex="25"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvCommodity" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtCommodity" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Hazardous Cargo:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoHazardousCargo" runat="server" TabIndex="26" RepeatDirection="Horizontal"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="rdoHazardousCargo_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Text="No" Value="No"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                UNO Code:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUNOCode" runat="server" CssClass="textboxuppercase" MaxLength="5"
                                                                    Text="ZZZZZ" Enabled="False" Width="250px" TabIndex="27"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvUNOCode" runat="server" ControlToValidate="txtUNOCode"
                                                                    ErrorMessage="This field is required" CssClass="errormessage" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                IMO Code:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtIMOCode" runat="server" CssClass="textboxuppercase" MaxLength="3"
                                                                    Text="ZZZ" Enabled="False" Width="250px" TabIndex="28"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvIMOCode" runat="server" ControlToValidate="txtIMOCode"
                                                                    ErrorMessage="This field is required" CssClass="errormessage" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Direct Port Transfer:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoDPT" runat="server" TabIndex="29" RepeatDirection="Horizontal"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="rdoDPT_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Text="No" Value="No"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Delivery To(DPT Code):
                                                            </td>
                                                            <td>
                                                                <uc6:AC_DeliveryTo ID="AC_DeliveryTo1" runat="server" />
                                                                <asp:Label ID="errDeliveryTo" runat="server" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                CFS / ICD Name:
                                                            </td>
                                                            <td>
                                                                <uc12:AC_CFSCode ID="AC_CFSCode1" runat="server" />
                                                                <asp:Label ID="errCFS" runat="server" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                CFS / ICD Code:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCFSName" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="250px" TabIndex="32" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                CFS Nominated By:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoCfsNominated" runat="server" TabIndex="33" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="Line" Value="L"></asp:ListItem>
                                                                    <asp:ListItem Text="Consignee" Value="C"></asp:ListItem>
                                                                    <asp:ListItem Text="None" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                CHA:
                                                            </td>
                                                            <td>
                                                                <uc13:AC_CHA ID="AC_CHA1" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Package:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtPackage" runat="server" CssClass="numerictextbox" MaxLength="30"
                                                                    Width="250px" TabIndex="35"></cc2:CustomTextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvPackage" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtPackage" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Unit of Package:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <uc2:AC_PackageUnit ID="AC_PackageUnit1" runat="server" />
                                                                <asp:Label ID="errUnitPackage" runat="server" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblGrossWt" runat="server" Text="Gross Weight:"></asp:Label>
                                                                <span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtGrossWeight" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                                    MaxLength="17" Precision="13" Scale="3" Width="250px" TabIndex="37" ></cc2:CustomTextBox>
                                                                <asp:RequiredFieldValidator ID="rfvGrossWeight" runat="server" ControlToValidate="txtGrossWeight"
                                                                    ErrorMessage="This field is required" CssClass="errormessage" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblUnit" runat="server" Text="Unit of Weight:"></asp:Label>
                                                                <span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <uc4:AC_WeightUnit ID="AC_WeightUnit1" runat="server" />
                                                                <asp:Label ID="errUnitVW" runat="server" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="Volume:"></asp:Label>
                                                                
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtVolume" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                                    MaxLength="17" Precision="13" Scale="3" Width="250px" TabIndex="39"></cc2:CustomTextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Unit of Volume:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc3:AC_VolumeUnit ID="AC_VolumeUnit1" runat="server" />
                                                                <asp:Label ID="Label2" runat="server" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Item type:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoItemType" runat="server" TabIndex="41" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="OT" Value="OT"></asp:ListItem>
                                                                    <asp:ListItem Text="GC" Value="GC"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Transport Mode:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoTransportMode" runat="server" TabIndex="42" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Road" Value="R" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Train" Value="T"></asp:ListItem>
                                                                    <asp:ListItem Text="Ship" Value="S"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Detention Free Days:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtDestFreeDays" runat="server" CssClass="numerictextbox"
                                                                    MaxLength="13" Width="250px" TabIndex="43"></cc2:CustomTextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDestFreeDays" runat="server" ControlToValidate="txtDestFreeDays"
                                                                    ErrorMessage="This field is required" CssClass="errormessage" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Detention Slab:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoDestSlab" runat="server" TabIndex="44" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="Regular" Value="R"></asp:ListItem>
                                                                    <asp:ListItem Text="Overwrite" Value="O"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                PGR Free Days:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtPGRFreeDays" runat="server" CssClass="numerictextbox" MaxLength="13"
                                                                    Width="250px" TabIndex="45"></cc2:CustomTextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPGRFreeDays" runat="server" ControlToValidate="txtPGRFreeDays"
                                                                    ErrorMessage="This field is required" CssClass="errormessage" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Reefer:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoReefer" runat="server" TabIndex="46" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="No" Value="No"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Cargo Movement Code:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCMCode" runat="server" CssClass="textboxuppercase" MaxLength="2"
                                                                    Width="250px" TabIndex="51"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCMCode" runat="server" ControlToValidate="txtCMCode"
                                                                    ErrorMessage="This field is required" CssClass="errormessage" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            
                                                            <td>
                                                            </td>
                                                            <td>
                                                                FreeOut:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoFreeOut" runat="server" TabIndex="48" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="No" Value="No"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                MLO Code:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMLOCode" runat="server" CssClass="textboxuppercase" MaxLength="16"
                                                                    Width="250px" TabIndex="49" Enabled="False"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Part B/L:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoPartBL" runat="server" TabIndex="50" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="No" Value="No"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td>
                                                                Carrier:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>

                                                                <asp:DropDownList ID="ddlCarrier" runat="server" CssClass="dropdownlist" TabIndex="60">
                                                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvCarrier" runat="server" ControlToValidate="ddlCarrier"
                                                                    ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Tax Exempted:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoTaxExempted" runat="server" TabIndex="52" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="No" Value="No"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Lock DO:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoLockDO" runat="server" TabIndex="53" RepeatDirection="Horizontal"
                                                                    OnSelectedIndexChanged="rdoLockDO_SelectedIndexChanged" AutoPostBack="True">
                                                                    <asp:ListItem Selected="True" Text="No" Value="No"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Lock DO Comment:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLockDOComment" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                                                    Width="250px" TabIndex="54" Enabled="False"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvLockDOComment" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtLockDOComment" ValidationGroup="Save"
                                                                    Display="Dynamic" Visible="False"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                No of TEU:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtTEU" runat="server" CssClass="numerictextbox" MaxLength="13"
                                                                    Width="250px" TabIndex="55" Text="0"></cc2:CustomTextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvTEU" runat="server" CssClass="errormessage" ErrorMessage="This field is required"
                                                                    ControlToValidate="txtTEU" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                No of FEU:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFEU" runat="server" CssClass="numerictextbox" MaxLength="13"
                                                                    Width="250px" TabIndex="56"  Text="0"></cc2:CustomTextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFEU" runat="server" CssClass="errormessage" ErrorMessage="This field is required"
                                                                    ControlToValidate="txtFEU" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Waiver %:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtWaiver" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                                    MaxLength="17" Precision="13" Scale="3" Width="250px" TabIndex="57" Text="00.00"
                                                                    OnTextChanged="txtWaiver_TextChanged" AutoPostBack="True"></cc2:CustomTextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Upload Waiver Details:
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="javascript:popWin();"
                                                                    Enabled="False" TabIndex="58" />
                                                                <asp:HiddenField ID="hdnFilePath" runat="server" />
                                                                &nbsp;&nbsp; &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Waiver Type:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoWaiverType" runat="server" TabIndex="59" RepeatDirection="Horizontal"
                                                                    Enabled="False">
                                                                    <asp:ListItem Text="B/L Wise" Value="BW"></asp:ListItem>
                                                                    <asp:ListItem Text="Container Wise" Value="CW"></asp:ListItem>
                                                                    <asp:ListItem Selected="True" Text="None" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Empty Yard:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>

                                                                <asp:DropDownList ID="ddlSurveyor" runat="server" CssClass="dropdownlist" TabIndex="60">
                                                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvSurveyor" runat="server" ControlToValidate="ddlSurveyor"
                                                                    ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                                                    ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>

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
                                                            <td style="width: 28%;">
                                                                Shipper:
                                                                <br />
                                                                <%--<asp:TextBox ID="txtShipper" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="250" TabIndex="1"></asp:TextBox>--%>
                                                                <uc11:AC_Shipper ID="AC_Shipper1" runat="server" />
                                                                <asp:Label runat="server" ID="errShipper" CssClass="errormessage"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtShipperAddr" runat="server" CssClass="textboxuppercase" MaxLength="250"
                                                                    Width="450" TabIndex="2" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 28%;">
                                                                Consignee:<span class="errormessage">*</span><br />
                                                                <%--<asp:TextBox ID="txtConsignee" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="250" TabIndex="3"></asp:TextBox>--%>
                                                                <uc9:AC_Consignee ID="AC_Consignee1" runat="server" />
                                                                <asp:Label runat="server" ID="errConsignee" CssClass="errormessage"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtConsigneeAddr" runat="server" CssClass="textboxuppercase" MaxLength="250"
                                                                    Width="450" TabIndex="4" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Notifying Party:<span class="errormessage">*</span>
                                                                <br />
                                                                <%--<asp:TextBox ID="txtNotifyingParty" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="250" TabIndex="5"></asp:TextBox>--%>
                                                                <uc10:AC_NParty ID="AC_NParty1" runat="server" />
                                                                <asp:Label runat="server" ID="errNP" CssClass="errormessage"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtNotifyingPartyAddr" runat="server" CssClass="textboxuppercase"
                                                                    MaxLength="250" Width="450" TabIndex="6" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                            </td>   
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 28%;">
                                                                Cargo Arrival Notice To:<span class="errormessage">*</span>
                                                                <br />
                                                                <asp:TextBox ID="txtCargoArrivalNotice" runat="server" CssClass="textboxuppercase"
                                                                    MaxLength="50" Width="450" TabIndex="7" TextMode="MultiLine" Rows="3"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvCANotice" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtCargoArrivalNotice" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                <%--<uc8:AC_CANotice ID="AC_CANotice1" runat="server" />--%>
                                                                <%--<asp:TextBox ID="txtCargoArrivalNotice" runat="server" CssClass="textboxuppercase"
                                                                    MaxLength="50" Width="250" TabIndex="7"></asp:TextBox>
                                                                <br />
                                                                <asp:TextBox ID="txtCargoArrivalNoticeAddr" runat="server" CssClass="textboxuppercase"
                                                                    MaxLength="250" Width="250" TabIndex="8" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                --%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Marks & Nos:<span class="errormessage">*</span>
                                                                <br />
                                                                <asp:TextBox ID="txtMarks" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="450" TabIndex="8" Text="N/M" TextMode="MultiLine" Rows="3"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvMarks" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtMarks" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 28%;">
                                                                Description of Goods:<span class="errormessage">*</span>
                                                                <br />
                                                                <asp:TextBox ID="txtDescGoods" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="450" TabIndex="9" TextMode="MultiLine" Rows="3"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="rfvDescGoods" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtDescGoods" ValidationGroup="Save"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 28%;">
                                                                Transhipment Information:
                                                                <br />
                                                                <asp:TextBox ID="txtTransinfo" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="450" TabIndex="10" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 28%;">
                                                                B/L Comment:
                                                                <br />
                                                                <asp:TextBox ID="txtBLComment" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="450" TabIndex="11" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <!-- Footer Tab-->
                                            <cc1:TabPanel ID="tpFooter" runat="server">
                                                <HeaderTemplate>
                                                    B/L Footer</HeaderTemplate>
                                                <ContentTemplate>
                                                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                B/L No:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtFtrBLNo" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="250" TabIndex="1" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 20%;">
                                                                Cargo Type:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtFtrCargoType" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                                                    Width="250" TabIndex="2" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                B/L Gross Weight:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <cc2:CustomTextBox ID="txtFtrBLGrossWeight" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                                    MaxLength="17" Precision="13" Scale="3" Width="250px" Enabled="false" TabIndex="3"></cc2:CustomTextBox>
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 20%;">
                                                                Packages:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                 <cc2:CustomTextBox ID="txtFtrPackages" runat="server" CssClass="numerictextbox" MaxLength="30"
                                                                    Width="250px" TabIndex="4" Enabled="false"></cc2:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Container No:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtFtrContainerNo" runat="server" CssClass="textboxuppercase" MaxLength="11"
                                                                    Width="250" TabIndex="5"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrContainerNo" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtFtrContainerNo" ValidationGroup="Add"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                <asp:Label runat="server" ID="errContainer" CssClass="errormessage"></asp:Label>
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            <td style="width: 20%;">
                                                                Container Size:
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:DropDownList ID="ddlFtrContainerSize" runat="server" CssClass="dropdownlist" AutoPostBack="true"
                                                                    TabIndex="6" OnSelectedIndexChanged="ddlFtrContainerSize_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="--Select--" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Value="20" Text="20'"></asp:ListItem>
                                                                    <asp:ListItem Value="40" Text="40'"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrContainerSize" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="ddlFtrContainerSize"
                                                                    InitialValue="0" ValidationGroup="Add" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Container Type:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:DropDownList ID="ddlFtrContainerType" runat="server" CssClass="dropdownlist"
                                                                    TabIndex="7" OnSelectedIndexChanged="ddlFtrContainerType_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrContainerType" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="ddlFtrContainerType"
                                                                    InitialValue="0" ValidationGroup="Add" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 4%;">
                                                            </td>
                                                            
                                                            <td>
                                                                Agent Code:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFtrAgentCode" runat="server" CssClass="textboxuppercase" MaxLength="15"
                                                                    Width="250" TabIndex="15" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Commodity:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <%--<cc2:CustomTextBox ID="txtFtrCommodity" runat="server" CssClass="numerictextbox"
                                                                    MaxLength="13" Width="250px"></cc2:CustomTextBox>--%>
                                                                <asp:TextBox ID="txtFtrCommodity" runat="server" CssClass="textboxuppercase" MaxLength="20"
                                                                    Width="250" TabIndex="9"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrCommodity" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtFtrCommodity" ValidationGroup="Add"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                SOC:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoFtrSoc" runat="server" TabIndex="10" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Gross Weight:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrGrossWeight" runat="server" CssClass="numerictextbox"
                                                                    Type="Decimal" MaxLength="11" Precision="7" Scale="3" Width="250px" TabIndex="11"
                                                                    ontextchanged="txtFtrGrossWeight_TextChanged" AutoPostBack="true" ></cc2:CustomTextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrGrossWeight" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtFtrGrossWeight" ValidationGroup="Add"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                           
                                                            <td>
                                                                Custom Seal:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFtrCustSeal" runat="server" CssClass="textboxuppercase" MaxLength="15"
                                                                    Width="250" TabIndex="19"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Cargo Weight(Ton):<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrCargoWt" runat="server" CssClass="numerictextbox" Type="Decimal"
                                                                    MaxLength="5" Precision="2" Scale="2" Width="250px" TabIndex="13" Enabled="false"></cc2:CustomTextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrCargoWt" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtFtrCargoWt" ValidationGroup="Add"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            
                                                            <td>
                                                                Stowage:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFtrStowage" runat="server" CssClass="textboxuppercase" MaxLength="6"
                                                                    Width="250" TabIndex="29"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Seal No:<span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 28%;">
                                                                <asp:TextBox ID="txtFtrSealNo" runat="server" CssClass="textboxuppercase" MaxLength="15"
                                                                    Width="250" TabIndex="8"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrSealNo" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtFtrSealNo" ValidationGroup="Add"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Tare Weight:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrTareWt" runat="server" CssClass="numerictextbox" MaxLength="8"
                                                                    Width="250px" TabIndex="16" Type="Decimal" Precision="4" Scale="3"></cc2:CustomTextBox>
                                                                <%--<br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrTareWt" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtFtrTareWt" ValidationGroup="Add"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                             <td>
                                                                Package:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrPackage" runat="server" CssClass="numerictextbox" MaxLength="13"
                                                                    Width="250px" TabIndex="12"></cc2:CustomTextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrPackage" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtFtrPackage" ValidationGroup="Add"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                IMCO No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFtrImcoNo" runat="server" CssClass="textboxuppercase" MaxLength="4"
                                                                    Width="250" TabIndex="20"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                ISO Code:<span class="errormessage">*</span>
                                                            </td>
                                                            <td>
                                                                <%--<asp:DropDownList ID="ddlFtrIsoCode" runat="server" CssClass="dropdownlist" TabIndex="14">
                                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvFtrIsoCode" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="ddlFtrIsoCode" InitialValue="0"
                                                                    ValidationGroup="Add" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                                <asp:TextBox ID="txtFtrISOCode" runat="server"  TabIndex="14" Width="250" 
                                                                    CssClass="textboxuppercase" MaxLength="15"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="rfvISO" runat="server" CssClass="errormessage"
                                                                    ErrorMessage="This field is required" ControlToValidate="txtFtrISOCode" ValidationGroup="Add"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Call No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFtrCallNo" runat="server" CssClass="textboxuppercase" MaxLength="1"
                                                                    Width="250" TabIndex="30"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Temperature:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrTemperature" runat="server" CssClass="numerictextbox"
                                                                    Width="250px" TabIndex="17" Type="Decimal"
                                                                    MaxLength="7" Precision="4" Scale="2"></cc2:CustomTextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Temperature Unit:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoFtrTemperatureUnit" runat="server" TabIndex="18" RepeatDirection="Horizontal"
                                                                    OnSelectedIndexChanged="rdoFtrTemperatureUnit_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Selected="True" Text="Centigrade" Value="C"></asp:ListItem>
                                                                    <asp:ListItem Text="Fahrenheit" Value="F"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                       
                                                        <tr>
                                                            <td>
                                                                Temp. Max:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrTempMax" runat="server" CssClass="numerictextbox"
                                                                    Width="250px" TabIndex="21" Type="Decimal"
                                                                    MaxLength="7" Precision="4" Scale="2"></cc2:CustomTextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Temp. Min:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrTempMin" runat="server" CssClass="numerictextbox"
                                                                    Width="250px" TabIndex="22" Type="Decimal"
                                                                    MaxLength="7" Precision="4" Scale="2"></cc2:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Temp. Unit:
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoFtrTempUnit" runat="server" TabIndex="23" RepeatDirection="Horizontal" Enabled="false">
                                                                    <asp:ListItem Selected="True" Text="Centigrade" Value="C"></asp:ListItem>
                                                                    <asp:ListItem Text="Fahrenheit" Value="F"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                DIM Code:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFtrDimCode" runat="server" CssClass="textboxuppercase" MaxLength="3"
                                                                    Width="250" TabIndex="24"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                OD Length:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrODLength" runat="server" CssClass="numerictextbox"
                                                                    Width="250px" TabIndex="25" Type="Decimal"
                                                                    MaxLength="13" Precision="10" Scale="2"></cc2:CustomTextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                OD Width:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrODWidth" runat="server" CssClass="numerictextbox" MaxLength="13"
                                                                    Width="250px" TabIndex="26" Type="Decimal"
                                                                    Precision="10" Scale="2"></cc2:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                OD Height:
                                                            </td>
                                                            <td>
                                                                <cc2:CustomTextBox ID="txtFtrODHeight" runat="server" CssClass="numerictextbox" MaxLength="13"
                                                                    Width="250px" TabIndex="27" Type="Decimal"
                                                                    Precision="10" Scale="2"></cc2:CustomTextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Cargo:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFtrCargo" runat="server" CssClass="textboxuppercase" MaxLength="3"
                                                                    Width="250" TabIndex="28"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                    <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                                        <tr>
                                                            <td style="padding-top: 10px;">
                                                                <asp:Button ID="btnAddRow" runat="server" Text="Add Row" TabIndex="31" OnClick="btnAddRow_Click"
                                                                    ValidationGroup="Add" />&nbsp;&nbsp;
                                                                <%--<asp:Button ID="btnRefresh" runat="server" CssClass="button" Text="Refresh" TabIndex="31" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:GridView ID="gvwFooter" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                        BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvwFooter_RowDataBound"
                                                        OnRowCommand="gvwFooter_RowCommand">
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
                                                            <asp:TemplateField HeaderText="Type">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                                <HeaderStyle CssClass="gridviewheader"/>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gross Weight">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%"  HorizontalAlign="Right"/>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Package">
                                                                <HeaderStyle CssClass="gridviewheader"/>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%"  HorizontalAlign="Right"/>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Waiver">
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditBLFooter" ImageUrl="~/Images/edit.png"
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
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                        </cc1:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="70"
                                            OnClick="btnSave_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="71"
                                            PostBackUrl="~/Transaction/ImportBL.aspx" Text="Back" />
                                        <br />
                                        <asp:Label ID="lblErr" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressBL" runat="server" AssociatedUpdatePanelID="upImportBL">
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
