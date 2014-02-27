<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditLeaseReference.aspx.cs" Inherits="EMS.WebApp.Equipment.AddEditLeaseReference" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT LEASE REFERENCE</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Lease Reference</legend>
                <asp:UpdatePanel ID="upBooking" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                <tr>
                                    <td style="width: 20%;">
                                        Line/NVOCC<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:DropDownList ID="ddlNvocc" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                            TabIndex="1" OnSelectedIndexChanged="ddlNvocc_SelectedIndexChanged">
                                            <%--                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvNvocc" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="ddlNvocc" InitialValue="0"
                                            ValidationGroup="Save" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 20%;">
                                        Location<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropdownlist" Width="250px" AutoPostBack="true"
                                            TabIndex="2" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvloc" runat="server" CssClass="errormessage" ErrorMessage="This field is required"
                                            ControlToValidate="ddlLocation" InitialValue="0" ValidationGroup="Save" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Empty Yard<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEmptyYard" runat="server" CssClass="dropdownlist" Width="250px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlEmptyYard_SelectedIndexChanged" TabIndex="3">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvEmptyYard" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="ddlEmptyYard" InitialValue="0"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        Leasing Company 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLeaseCompany" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                            Width="250px" TabIndex="4"></asp:TextBox><br />
    <%--                                    <asp:RequiredFieldValidator ID="rfvAccounts" runat="server" ControlToValidate="txtAccounts"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Lease No<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                      <asp:TextBox ID="txtLeaseNo" runat="server" CssClass="textboxuppercase" MaxLength="60"
                                            Width="250px" TabIndex="4"></asp:TextBox><br />
                                      <asp:RequiredFieldValidator ID="rfvLeaseNo" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtLeaseNo" InitialValue="0"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>

                                   <%--     <asp:textbox runat="server" id="txtLeaseNo" MaxLength="60"  Width="250px" TabIndex="5" onfocus="if (this.value == 'Display Only') this.value = '';" 
                                        onblur="if (this.value == '') this.value = 'Display Only';" value="Display Only"  xmlns:asp="#unknown" CssClass="textboxuppercase"
                                        style="color:Gray;" Enabled="false">
                                        </asp:textbox><br />--%>
                                        
<%--                                        style="border-top-left-radius: 20px; border-top-right-radius: 20px;border-bottom-left-radius: 20px;border-bottom-right-radius: 20px; 
                                        color:Gray; text-align:center;"--%>
                                    </td>
                                    <td>
                                        Lease Date<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLeaseDate" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cbeLeaseDate" TargetControlID="txtLeaseDate" runat="server"
                                            Format="dd-MM-yyyy" Enabled="True" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvLeaseDate" runat="server" ControlToValidate="txtLeaseDate"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkContainerDtls" runat="server" Text="Container Details" OnClick="lnkContainerDtls_Click"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContainerDtls" runat="server" CssClass="textboxuppercase" Width="250px"
                                            Enabled="False" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        Lease Validity<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLeaseValidity" runat="server" CssClass="textboxuppercase" MaxLength="50"
                                            Width="250px" TabIndex="6"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtLeaseValidity" runat="server"
                                            Format="dd-MM-yyyy" Enabled="True" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLeaseValidity"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnLeaseID" runat="server" Value="0" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="70"
                                            OnClick="btnSave_Click1" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="71" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" OnClick="btnBack_Click" />
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
                                Enabled="true" PopupControlID="pnlContainer" Drag="true" BackgroundCssClass="ModalPopupBG"
                                CancelControlID="btnCancelContainer">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlContainer" runat="server" Style="height: 360px; width: 450px; background-color: White;">
                                <fieldset>
                                    <legend>Container Breakup</legend>
                                    <center>
                                        <asp:UpdatePanel ID="udpContainer" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="hdnLeaseContainerID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnIndex" runat="server" />
                                                <div style="overflow: auto; height: 90px; width: 420px;">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                <asp:Label ID="lblType" Text="Type" runat="server"></asp:Label><span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <asp:Label ID="lblSize" Text="Size" runat="server"></asp:Label><span class="errormessage">*</span>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <asp:Label ID="lblUnit" Text="Unit" runat="server"></asp:Label><span class="errormessage">*</span>
                                                            </td>
                                                            <%--<td style="width: 20%;">
                                                                <asp:Label ID="lblWt" Text="Wt/Cont" runat="server"></asp:Label><span class="errormessage">*</span>
                                                            </td>--%>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                <asp:DropDownList ID="ddlCntrType" runat="server" CssClass="dropdownlist" Width="80px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <asp:DropDownList ID="ddlSize" runat="server" CssClass="dropdownlist" Width="80px">
                                                                    <asp:ListItem Text="20" Value="20" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <cc2:CustomTextBox ID="txtNos" runat="server" CssClass="numerictextbox" Width="77px"
                                                                    Type="Numeric" MaxLength="8" Precision="10" Scale="2">
                                                                </cc2:CustomTextBox>
                                                                <%--<asp:TextBox ID="txtNos" runat="server" Width="77px"></asp:TextBox>--%>
                                                            </td>
                                                           <%-- <td style="width: 20%;">
                                                                <cc2:CustomTextBox ID="txtWtPerCntr" runat="server" CssClass="numerictextbox" Width="82px"
                                                                    Type="Decimal" MaxLength="13" Precision="10" Scale="2">
                                                                </cc2:CustomTextBox>
                                                                
                                                            </td>--%>
                                                            <td style="width: 20%;">
                                                                <asp:ImageButton ID="btnimgSave" runat="server" ImageUrl="~/Images/action_add2.gif"
                                                                    Height="16" Width="16" OnClick="btnimgSave_Click" CausesValidation="true" ValidationGroup="Container" />&nbsp;&nbsp;
                                                                <asp:ImageButton ID="btnimgReset" runat="server" ImageUrl="~/Images/Undo.gif" Height="16"
                                                                    Width="16" OnClick="btnimgReset_Click" CausesValidation="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <asp:RequiredFieldValidator ID="rfvType" runat="server" CssClass="errormessage" ErrorMessage="Please Select Type"
                                                                    ControlToValidate="ddlCntrType" InitialValue="0" ValidationGroup="Container"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvNos" runat="server" CssClass="errormessage" ErrorMessage="Please Enter Unit"
                                                                    ControlToValidate="txtNos" ValidationGroup="Container" Display="Dynamic"></asp:RequiredFieldValidator>
                                                               <%-- <asp:RequiredFieldValidator ID="rfvwt" runat="server" CssClass="errormessage" ErrorMessage="Please Enter Wt/Cont"
                                                                    ControlToValidate="txtWtPerCntr" ValidationGroup="Container" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="overflow: auto; height: 180px; width: 420px;">
                                                    <asp:GridView ID="gvContainer" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                        BorderStyle="None" BorderWidth="0" Width="100%" Style="margin-right: 0px" ShowHeader="False"
                                                        OnRowCommand="gvContainer_RowCommand" OnDataBound="gvContainer_DataBound" OnRowDataBound="gvContainer_OnRowDataBound">
                                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                        <PagerStyle CssClass="gridviewpager" />
                                                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="gvhdnLeaseContainerID" runat="server" Value='<%# Eval("BookingContainerID") %>' />
                                                                    <asp:HiddenField ID="gvhdnContainerTypeId" runat="server" Value='<%# Eval("ContainerTypeID") %>' />
                                                                    <asp:Label ID="lblContainerType" runat="server" Text='<%# Eval("ContainerType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContainerSize" runat="server" Text='<%# Eval("CntrSize")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("NoofContainers")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           <%-- <asp:TemplateField>
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwtPerCont" runat="server" Text='<%# Eval("wtPerCntr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField>
                                                                <HeaderStyle CssClass="gridviewheader" />
                                                                <ItemStyle CssClass="gridviewitem" Width="20%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditGrid" ImageUrl="~/Images/EditInGrid.gif"
                                                                        Height="16" Width="16" CausesValidation="false" />&nbsp;&nbsp;
                                                                    <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/trash_icon.gif"
                                                                        Height="16" Width="16" CausesValidation="false" OnClientClick="javascript:if(!confirm('Want to Delete this Container?')) return false;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                        <%--<asp:Button ID="btnSaveContainer" runat="server" Text="Save" CausesValidation="false" />--%>
                                        <asp:Button ID="btnCancelContainer" runat="server" Text="Close" CausesValidation="false" />
                                    </center>
                                </fieldset>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <!-- Modal Popup Container Details -->
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

