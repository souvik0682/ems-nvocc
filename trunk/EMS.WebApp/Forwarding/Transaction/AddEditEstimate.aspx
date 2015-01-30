<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditEstimate.aspx.cs" Inherits="EMS.WebApp.Forwarding.Master.AddEditEstimate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc3" %>
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
                       <style>
                          .gridviewheader th
                           {
                            background: #ccc;
                            font-size:13px;  
                            font-family:Calibri;
                           }
                       </style>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnQuoPath" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnLastNo" runat="server" Value="" />
                                Party Type:<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBillingFrom" runat="server" CssClass="dropdownlist"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlBillingFrom_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlBillingFrom" ValidationGroup="Save" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                            <td width="140">
                                Party:<span class="errormessage1">*</span>
                            </td>
                            <td width="300">
                                <asp:DropDownList ID="ddlParty" runat="server" CssClass="dropdownlist" Width="300px">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rvfddlParty" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlParty" ValidationGroup="Save" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                           <%-- <td  width="120">
                                Unit Type:<span class="errormessage1">*</span>
                            </td>
                            <td  width="220">
                                <asp:DropDownList ID="ddlUnitType" runat="server" CssClass="dropdownlist">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlUnitType" ValidationGroup="Save" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>--%>
                        </tr>   
                        <tr>
                            <td>
                                Job No
                            </td>
                            <td>
                                <asp:Label ID="lblJobNo" runat="server" Text="" Width="200px" ></asp:Label>
                            </td>
                            <td>
                                Job Date
                            </td>
                            <td>
                                <asp:Label ID="lblJobDate" runat="server" Text="" Width="200px" ></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="trChargesInDays" visible="true">
                            <td>
                                Estimate No
                            </td>
                            <td>
                                <asp:Label ID="lblEstimateNo" runat="server" Text="" Width="200px" ></asp:Label>
                            </td>
                            <td>
                                Estimate Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtEstimateDate" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox><br />
                                    <cc2:CalendarExtender ID="dtEstimateDate" TargetControlID="txtEstimateDate" Format="dd/MM/yyyy"
                                    runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvEstimateDate" runat="server" CssClass="errormessage"
                                        ControlToValidate="txtEstimateDate" Display="Dynamic" Text="This field is Required"
                                        ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                            <td>
                                Credit in Days:<span class="errormessage1">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCreditInDays" runat="server"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtCreditInDays" ValidationGroup="Save" Display="Dynamic"
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rexpCreditDays" runat="server" ControlToValidate="txtCreditInDays"
                                    ValidationGroup="Save" Display="Dynamic" ErrorMessage="[Please check the value]"
                                    ValidationExpression="\d*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>                        
                        
                        <tr>
                            
                           <td>
                                Job Type
                            </td>
                            <td>
                                <asp:Label ID="lblShippingMode" runat="server" Text="" Width="100px" ></asp:Label>
                            </td>
                            <td>
                                Charges:
                            </td>
                            <td>
                                <asp:Label ID="lblCharges" runat="server" Text="0" Width="100px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                         
                            <td>
                                USD Exchange Rate
                            </td>
                            <td>
<%--                                <ccs:CustomTextBox ID="txtExRate" data='1' TextBoxType="2"  runat="server" CssClass="numerictextbox" MaxLength="80"
                                Width="100" AutoPostBack="true" OnTextChanged="Text_TextChanged"></ccs:CustomTextBox>--%>
                                <asp:HiddenField ID="hdnExRate" runat="server" Value="0" />
                                <ccs:CustomTextBox ID="txtExRate"  data='3' runat="server" AutoPostBack="true" OnTextChanged="TextEx_TextChanged"
                                    CssClass="numerictextbox" Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="70">
                                </ccs:CustomTextBox>
                            </td>
                            
                        </tr>
                        <style>
                                        .gridviewitem2
                                        {
                                          width:120px;
                                          padding: 0 80px 0 5px;
                                          border-left:1px solid #000;
                                        }
                                        #container_grvCharges
                                        {
                                            margin-top: 10px;
                                        }
                                        .dropdownlist
                                        {
                                            width:160px;
                                        }
                                        
                        </style>
                        <tr>
                            <td colspan="4">
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
                                        <asp:TemplateField HeaderText="Charges">
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Left" />
                                              <FooterStyle CssClass="gridviewitem"  />
                                            <ItemTemplate>
                                                <%# Eval("ChargeMasterName")%>
                                            </ItemTemplate>
                                            <FooterTemplate >
                                                <asp:DropDownList ID="ddlCharges" runat="server" CssClass="dropdownlist" AutoPostBack="true" Width="250"
                                                        OnSelectedIndexChanged="ddlCharges_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvddlInvoiceOrAdvNo" runat="server" CssClass="errormessage"
                                                    ControlToValidate="ddlCharges" ValidationGroup="Add" Display="Dynamic" InitialValue="0"
                                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        

                                        <asp:TemplateField HeaderText="Size">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%# Eval("CntrSize")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlSize" runat="server" CssClass="dropdownlist" AutoPostBack="true" Width="80"
                                                        OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <%--<asp:RequiredFieldValidator ID="rfvSize" runat="server" CssClass="errormessage"
                                                    ControlToValidate="ddlSize" ValidationGroup="Add" Display="Dynamic" InitialValue="0"
                                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit Type">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%# Eval("UnitType")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlUnitType" data='5' runat="server" CssClass="dropdownlist" AutoPostBack="true" Width="150"
                                                        OnSelectedIndexChanged="ddlUnitType_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvddlUnitType" runat="server" CssClass="errormessage"
                                                    ControlToValidate="ddlUnitType" ValidationGroup="Add" Display="Dynamic" InitialValue="0"
                                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Right">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("Unit")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ccs:CustomTextBox ID="txtUnit"   data='3' runat="server"  CssClass="numerictextbox" Type="Decimal" 
                                                    AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                                    MaxLength="15" Precision="12" Scale="3" Width="90"></ccs:CustomTextBox><br />
                                                <asp:RequiredFieldValidator ID="rfvttxtUnit" runat="server" CssClass="errormessage"
                                                    ControlToValidate="txtUnit" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
<%--
                                        <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Right">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("Unit")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ccs:CustomTextBox ID="txtUnit" data='3' runat="server" CssClass="numerictextbox" MaxLength="15" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                                    Precision="12" Scale="3" Width="60"></ccs:CustomTextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvttxtUnit" runat="server" CssClass="errormessage"
                                                    ControlToValidate="txtUnit" Display="Dynamic" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Right">
                                            <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("Rate")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ccs:CustomTextBox ID="txtRate"   data='3' runat="server"  CssClass="numerictextbox" Type="Decimal" 
                                                    AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                                    MaxLength="15" Precision="12" Scale="2" Width="90"></ccs:CustomTextBox><br />
                                                <asp:RequiredFieldValidator ID="rfvtxtRate" runat="server" CssClass="errormessage"
                                                    ControlToValidate="txtRate" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Currency" HeaderStyle-HorizontalAlign="Center">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%# Eval("Currency")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlCurrency" data='2' runat="server" CssClass="dropdownlist" Width="150"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvddlCurrency" runat="server" CssClass="errormessage"
                                                    ControlToValidate="ddlCurrency" ValidationGroup="Add" Display="Dynamic" InitialValue="0"
                                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ROE" HeaderStyle-HorizontalAlign="Right">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("ROE")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ccs:CustomTextBox ID="txtROE"  data='3' runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                                  CssClass="numerictextbox" Type="Decimal" Enabled="false"
                                                    MaxLength="15" Precision="12" Scale="2" Width="70"></ccs:CustomTextBox><br />
                                                <asp:RequiredFieldValidator ID="rfvtxtROE" runat="server" CssClass="errormessage"
                                                    ControlToValidate="txtROE" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S.Tax" HeaderStyle-HorizontalAlign="Right">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("STax")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ccs:CustomTextBox ID="lblStax"  data='3' runat="server" CssClass="numerictextbox" Type="Decimal" AutoPostBack="true" OnTextChanged="lblStax_TextChanged"
                                                    MaxLength="15" Precision="12" Scale="2" Width="70"></ccs:CustomTextBox><br />
<%--                                                <asp:RequiredFieldValidator ID="rfvtxtROE" runat="server" CssClass="errormessage"
                                                    ControlToValidate="txtROE" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add"
                                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="INR" HeaderStyle-HorizontalAlign="Right">
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# string.Format("{0:0.00}", Eval("INR"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ccs:CustomTextBox ID="lblINR"  data='3' runat="server" Enabled="false"
                                                  CssClass="numerictextbox" Type="Decimal" 
                                                    MaxLength="15" Precision="12" Scale="2" Width="100"></ccs:CustomTextBox><br />
                                               
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                       <%-- <asp:TemplateField HeaderText="INR" ControlStyle-Width="150">
                                          <FooterStyle CssClass="gridviewitem2" />
                                            <ItemStyle CssClass="gridviewitem gridviewitem2" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Eval("INR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblINR" runat="server" Text="0"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                          <FooterStyle CssClass="gridviewitem"  />
                                            <ItemStyle CssClass="gridviewitem"  HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="Server" Width="24" Height="24" ImageUrl="~/Images/Remove.png"
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
                            <td colspan="4">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"/>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </center>

        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Modal Popup Container Details -->
            <center>
                <table border="0" cellpadding="2" cellspacing="3" width="20%" 
                    style="height: 100px">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Style="display: none;" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                                Enabled="true" PopupControlID="pnlContainer" Drag="true" BackgroundCssClass="ModalPopupBG"
                                >
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlContainer" runat="server" Style="height: 75px; width: 600px; background-color: White;">
                                <asp:FileUpload ID="QuotationUpload" runat="server" />
                                <asp:Button ID="btnCancelContainer" runat="server" AutoPostBack="true" 
                                    Text="Close" onclick="btnCancelContainer_Click" />
                            </asp:Panel>
                        </td>
                        <br />

                    </tr>
                    
                </table>
                <!-- Modal Popup Container Details -->
            </center>
</asp:Content>
