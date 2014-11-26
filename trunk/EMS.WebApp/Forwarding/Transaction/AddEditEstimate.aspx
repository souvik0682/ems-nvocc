<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditEstimate.aspx.cs" Inherits="EMS.WebApp.Forwarding.Master.AddEditEstimate" %>


<%@ Register Assembly="EMS.WebApp"  
                Namespace="EMS.WebApp.CustomControls" 
                TagPrefix="ccs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <asp:UpdateProgress ID="uProgressLoc" runat="server" AssociatedUpdatePanelID="upLoc">
        <ProgressTemplate>
            <div class="progress">
                <div id="image">
                    <img src="../../Images/PleaseWait.gif" alt="" /></div>
                <div id="text">
                    Please Wait...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="headercaption">
                Estimate Payable / Receivale</div>
            <center>
                <fieldset style="width: 800px;">
                    <legend>Estimate Payable / Receivale</legend>
                    <table border="0" cellpadding="2" cellspacing="3" width="100%">
                        <tr>
                            <td width="10px">
                                Party:<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlParty" runat="server" CssClass="dropdownlist" Width="200px">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rvfddlParty" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlParty" ValidationGroup="Save" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="100px">
                                Unit Type:<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUnitType" runat="server" CssClass="dropdownlist">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlUnitType" ValidationGroup="Save" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 140px;">
                                Total Unit<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:Label ID="lblTotalUnit" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Billing From:<span class="errormessage1">*</span>
                            </td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="ddlBillingFrom" runat="server" CssClass="dropdownlist">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlBillingFrom" ValidationGroup="Save" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPayment" runat="server" Text="Payment In"></asp:Label>:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoPayment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoPayment_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Advance" Value="A" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Credit" Value="C"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr runat="server" id="trChargesInDays" visible="false">
                            <td>
                                Charges in Days:<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChargesInDays" runat="server"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtChargesInDays" ValidationGroup="Save" Display="Dynamic"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtChargesInDays"
                                    ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Please check the value]"
                                    ValidationExpression="\d*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Charges:<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:Label ID="lblCharges" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ShowFooter="True" ShowHeaderWhenEmpty="True" ID="grvCharges" runat="server"
                                    AutoGenerateColumns="False" OnRowCommand="grvCharges_RowCommand" OnRowDataBound="grvCharges_RowDataBound"
                                    Width="100%">
                                    <HeaderStyle CssClass="gridviewheader"></HeaderStyle>
                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                    <PagerStyle CssClass="gridviewpager" />
                                    <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                    <EmptyDataTemplate>
                                        No Charge(s) Found</EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Charges" ControlStyle-Width="205">
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Left" />
                                              <FooterStyle CssClass="gridviewitem"  />
                                            <ItemTemplate>
                                                <%# Eval("ChargeMasterName")%>
                                            </ItemTemplate>
                                            <FooterTemplate >
                                                <asp:DropDownList ID="ddlCharges" runat="server" CssClass="dropdownlist" Width="200">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvddlInvoiceOrAdvNo" runat="server" CssClass="errormessage"
                                                    ControlToValidate="ddlCharges" ValidationGroup="Add" Display="Dynamic" InitialValue="0"
                                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit" ControlStyle-Width="100">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("Unit")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ccs:CustomTextBox ID="txtUnit" data='1' TextBoxType="2"  runat="server" CssClass="numerictextbox" MaxLength="60" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                                    Width="100"></ccs:CustomTextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvttxtUnit" runat="server" CssClass="errormessage"
                                                    ControlToValidate="txtUnit" Display="Dynamic" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ControlStyle-Width="205">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("Rate")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ccs:CustomTextBox ID="txtRate"   data='3' runat="server"  CssClass="numerictextbox" Type="Decimal" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                                    MaxLength="15" Precision="12" Scale="2" Width="100"></ccs:CustomTextBox><br />
                                                <asp:RequiredFieldValidator ID="rfvtxtRate" runat="server" CssClass="errormessage"
                                                    ControlToValidate="txtRate" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Currency" ControlStyle-Width="205">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%# Eval("Currency")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlCurrency" data='2' runat="server" CssClass="dropdownlist" Width="200"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvddlCurrency" runat="server" CssClass="errormessage"
                                                    ControlToValidate="ddlCurrency" ValidationGroup="Add" Display="Dynamic" InitialValue="0"
                                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ROE"  ControlStyle-Width="205">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("ROE")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ccs:CustomTextBox ID="txtROE"  data='3' runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                                  CssClass="numerictextbox" Type="Decimal"
                                                    MaxLength="15" Precision="12" Scale="2" Width="100"></ccs:CustomTextBox><br />
                                                <asp:RequiredFieldValidator ID="rfvtxtROE" runat="server" CssClass="errormessage"
                                                    ControlToValidate="txtROE" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INR" ControlStyle-Width="205">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("INR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblINR" runat="server" Text="0"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem"  HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="Server" Width="24" Height="24" ImageUrl="~/Images/Remove.jpeg"
                                                    ID="Remove" ValidationGroup="Remove" CommandName="Remove" CommandArgument='<%# Eval("ChargeId") %>'
                                                    AlternateText="Remove" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton runat="Server" Width="24" Height="24" ImageUrl="~/Images/add.jpeg"
                                                    ValidationGroup="Add" ID="Add" CommandName="Add" AlternateText="Add" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"/>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </center>
            <script type="text/javascript">
//                function pageLoad() {
//                    alert('d');
//                }
////                set();
////                function set() {
////                    $("input[data='1']").each(function () {
////                        $(this).attr('onblur', 'CalculateINR(this)')
////                    });
////                    $("input[data='3']").each(function () {
////                        $(this).attr('onblur', 'CalculateINR(this)')
////                    });
////                };

//                function CalculateINR(obj) {
//                    alert('Calculate');
//                    var unitV = 0;
//                    $("input[data='1']").each(function () {
//                        unitV = $(this).val();
//                    });
//                    var rateV = 0;
//                    var roeV = 0;
//                    $("input[data='3']").each(function (index) {
//                        if (index == 0) {
//                            rateV = $(this).val();
//                        } else { roeV = $(this).val(); }
//                    });
//                   
//                    alert(unitV);
//                    alert(rateV); 
//                    alert(roeV);

//                    var unitI = 0;
//                    var rateD = 0;
//                    var roeD = 0;
//                    if (parseInt(unitV) === unitV) {
//                        unitI = unitV;
//                    } 

//                    if (parseFloat(rateV) === rateV) {
//                        rateD = rateV;
//                    }


//                    if (parseFloat(roeV) === roeV) {
//                        roeD = roeV;
//                    }

//                    $("span[data='4']").each(function () {
//                        $(this).text((unitI * rateD * roeD).toString());
//                    });
//                   
//                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
