<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreInvoice.aspx.cs" Inherits="EMS.WebApp.Forwarding.Master.CreInvoice" %>
<%@ Register Assembly="EMS.WebApp"  
                Namespace="EMS.WebApp.CustomControls" 
                TagPrefix="ccs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
  &nbsp;&nbsp;&nbsp;
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
        ADD / EDIT Creditor Invoice </div>
    <center>
        <fieldset style="width:800px;">
            <legend>Add / Edit Creditor Invoice </legend>
            <table border="0" cellpadding="2" cellspacing="3" width="100%" >
               <style>
                          .gridviewheader th
                           {
                            background: #ccc;
                            font-size:13px;  
                            font-family:Calibri;
                           }
                       </style>
                <tr>
                    
                    <td style="width:10%">Party Type:<span class="errormessage1">*</span></td>
                    
                    <td style="width:40%"><asp:DropDownList ID="ddlPartyType" runat="server"  CssClass="dropdownlist" AutoPostBack="true"
                            onselectedindexchanged="ddlPartyType_SelectedIndexChanged" >
                      <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:HiddenField ID="hdnLastNo" runat="server" Value="" />
                      <asp:RequiredFieldValidator ID="rfvPartyType" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlCreditorName" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]">
                      </asp:RequiredFieldValidator>
                            

                    <td style="width:10%">Creditor Name:<span class="errormessage1">*</span></td>
                    <td style="width:40%"><asp:DropDownList ID="ddlCreditorName" runat="server"  CssClass="dropdownlist" >
                      <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlCreditorName" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]">
                      </asp:RequiredFieldValidator>
                </tr>

                <tr>
                    <td >Invoice No<span class="errormessage1">*</span></td>
                    <td>
                     <asp:TextBox ID="txtCreInvoiceNo" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvtxtAdjustmentDate" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtCreInvoiceNo" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator>    </td>
                
                    <td>Invoice Date:</td>
                    <td >                      
                       <asp:TextBox ID="txtCreInvoiceDate" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="250"></asp:TextBox><br />
                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="txtCreInvoiceDate"
                                TargetControlID="txtCreInvoiceDate"></cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtCreInvoiceDate" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator>                     
                   </td>
                </tr>
                
               
                <tr>
                    <td>Reference No:</td>
                    <td>  <asp:Label ID="lblOurInvoiceRef" runat="server" Text="AutoGenerated"></asp:Label>  
                    </td>
               
                    <td>Reference Date:<span class="errormessage1">*</span></td>
                    <td> <asp:TextBox ID="txtReferenceDate" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="250"></asp:TextBox><br />
                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server" PopupButtonID="txtReferenceDate"
                                TargetControlID="txtReferenceDate"></cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtReferenceDate" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator>
                   </td>
                </tr>
                
                <tr>
                    <td style="width:10%">Job Number:</td>
                    <td style="width:40%"><asp:Label ID="lblJobNumber" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        Job Date
                    </td>
                    <td>
                        <asp:Label ID="lblJobDate" runat="server" Text="" Width="200px" ></asp:Label>
                    </td>
                </tr> 
                <tr>
                    <td>
                        USD Exchange Rate
                    </td>
                    <td>
                        <ccs:CustomTextBox ID="txtExRate"  data='3' runat="server" AutoPostBack="true" OnTextChanged="TextEx_TextChanged"
                            CssClass="numerictextbox" Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="70">
                        </ccs:CustomTextBox>
                    </td>
                    <td>Rounding Off</td>
                    <td>
                        <asp:CheckBox ID="chkRoff" runat="server" AutoPostBack="true"
                            oncheckedchanged="chkRoff_CheckedChanged"/>
                        <ccs:CustomTextBox ID="txtRoff" data='1' TextBoxType="2"  runat="server" CssClass="numerictextbox" MaxLength="60"
                                                    Width="100"></ccs:CustomTextBox>
                    </td>
         
                </tr>
                <tr>
                    <td>Location:</td>
                    <td> <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>      
                   </td><td>Invoice Amount:</td>
                    <td> 
                        <asp:Label ID="lblInvoiceAmount" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdnInvoiceAmount" runat="server" Value="" />      
                   </td>
                </tr>   
                <tr>   
                <td colspan="4">    
                       <asp:GridView ShowFooter="True" ShowHeaderWhenEmpty="True" ID="grvInvoice" runat="server" 
                       OnRowCommand="grvInvoice_RowCommand" OnRowDataBound="grvInvoice_RowDataBound"
                            AutoGenerateColumns="False"   Width="100%" >
                            <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center"></HeaderStyle>
                             <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <emptydatatemplate>No Invoice(s)/Advance(s) Found</emptydatatemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Charge Name"  ControlStyle-Width="300" >  
    <%--                                    <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>    --%>                          
                                        <ItemStyle CssClass="gridviewheader" HorizontalAlign="Center"/>      
                                        <FooterStyle CssClass="gridviewheader" HorizontalAlign="Center"/>
                                        <ItemTemplate>
                                            <%# Eval("ChargeName")%>
                                        </ItemTemplate>
                                        <FooterTemplate >
                                          <asp:DropDownList ID="ddlCharges" runat="server"  CssClass="dropdownlist" Width="200"  AutoPostBack="true" OnSelectedIndexChanged="ddlCharges_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                            <asp:RequiredFieldValidator ID="rfvddlCharges" runat="server" CssClass="errormessage"
                                        ControlToValidate="ddlCharges" ValidationGroup="Add" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size">
<%--                                    <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>--%>
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
<%--                                    <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>--%>
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
                                            <%# string.Format("{0:0.00}", Eval("Unit"))%>
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

                                    <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Right">
                                        <FooterStyle CssClass="gridviewitem"  />
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%# string.Format("{0:0.00}", Eval("Rate"))%>
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

                                    <asp:TemplateField HeaderText="Total" ControlStyle-Width="100" Visible="false">  
    <%--                               <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>   --%>                           
                                        <ItemStyle CssClass="gridviewheader" HorizontalAlign="Right"/>      <FooterStyle CssClass="gridviewheader" HorizontalAlign="Center"/>
                                        <ItemTemplate>                                     
                                             <%#  string.Format("{0:0.00}", Eval("Total"))%>     
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label runat="server" Text="" ID="lblTotal" Width="100"></asp:Label>
                                        </FooterTemplate>
                                   </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Currency" ControlStyle-Width="100">
    <%--                               <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>  --%>                            
                                        <ItemStyle CssClass="gridviewheader" HorizontalAlign="Center"/>      <FooterStyle CssClass="gridviewheader" HorizontalAlign="Center"/>
                                        <ItemTemplate>                                     
                                            <%# Eval("Currency")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlCurrency" runat="server"  CssClass="dropdownlist" Width="100" 
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"
                                            >
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                            <asp:RequiredFieldValidator ID="rfvddlCurrency" runat="server" CssClass="errormessage"
                                            ControlToValidate="ddlCurrency" ValidationGroup="Add" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                                        </FooterTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Conv Rate" HeaderStyle-HorizontalAlign="Right">
                                        <FooterStyle CssClass="gridviewitem"  />
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%# string.Format("{0:0.00}", Eval("ConvRate"))%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <ccs:CustomTextBox ID="txtConvRate"  data='3' runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                                CssClass="numerictextbox" Type="Decimal" Enabled="false"
                                                MaxLength="15" Precision="12" Scale="2" Width="70"></ccs:CustomTextBox><br />
                                            <asp:RequiredFieldValidator ID="rfvtxtConvRate" runat="server" CssClass="errormessage"
                                                ControlToValidate="txtConvRate" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
<%--
                                    <asp:TemplateField HeaderText="Conv Rate" ControlStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle CssClass="gridviewheader" HorizontalAlign="Center"/>      
                                        <FooterStyle CssClass="gridviewheader" HorizontalAlign="Center"/>
                                        <ItemTemplate>                                     
                                              <%#  string.Format("{0:0.00}", Eval("ConvRate"))%>     
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <ccs:CustomTextBox ID="txtConvRate" data='1' TextBoxType="2"  runat="server" CssClass="numerictextbox" MaxLength="60" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                                        Width="100"></ccs:CustomTextBox>
                                                    <br />
                                        <asp:RequiredFieldValidator ID="rfvtxtConvRate" runat="server" CssClass="errormessage" 
                                        ControlToValidate="txtConvRate" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Gross" HeaderStyle-HorizontalAlign="Right">
                                        <FooterStyle CssClass="gridviewheader"/>
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right"/>
        <%--                                <HeaderStyle CssClass="gridviewheader" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>     --%>                         
                                             <ItemTemplate>                                     
                                                <%#  string.Format("{0:0.00}", Eval("Gross"))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            <asp:Label runat="server" Text="" ID="lblGross" Width="100" style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                    </asp:TemplateField>
                                
<%--                                    <asp:TemplateField HeaderText="S.Tax %" ControlStyle-Width="100" Visible="false">
                                        <ItemStyle CssClass="gridviewheader" HorizontalAlign="Center"/>      <FooterStyle CssClass="gridviewheader" HorizontalAlign="Center"/>
                                        <ItemTemplate>                                     
                                            <%#  string.Format("{0:0.00}", Eval("STaxPercentage"))%>  
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <ccs:CustomTextBox ID="txtSTax" data='1' TextBoxType="2"  runat="server" CssClass="numerictextbox" MaxLength="60" 
                                                AutoPostBack="true" OnTextChanged="Text_TextChanged" Width="100">
                                            </ccs:CustomTextBox>
                                                        <br />                                      
                                            <asp:RequiredFieldValidator ID="rfvtxtxSTax" runat="server" CssClass="errormessage" 
                                            ControlToValidate="txtSTax" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        </FooterTemplate>          
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="S.Tax" HeaderStyle-HorizontalAlign="Right">
                                        <FooterStyle CssClass="gridviewitem"  />
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%# string.Format("{0:0.00}", Eval("STax"))%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <ccs:CustomTextBox ID="lblStax"  data='3' runat="server" AutoPostBack="true" OnTextChanged="lblStax_TextChanged"
                                                CssClass="numerictextbox" Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="70"></ccs:CustomTextBox><br />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Right">
                                        <FooterStyle CssClass="gridviewitem"  />
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%# string.Format("{0:0.00}", Eval("GTotal"))%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <ccs:CustomTextBox ID="lblGTotal"  data='3' runat="server" Enabled="false"
                                                CssClass="numerictextbox" Type="Decimal" 
                                                MaxLength="15" Precision="12" Scale="2" Width="100"></ccs:CustomTextBox><br />
                                               
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField >                         
                                        <ItemStyle CssClass="gridviewitem" HorizontalAlign="Center"/>
                                        <FooterStyle CssClass="gridviewheader" HorizontalAlign="Center"/>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="Server" Width="24" Height="24" ImageUrl="~/Images/Remove.png" ID="Remove" ValidationGroup="Remove"
                                                CommandName="Remove" CommandArgument='<%# Eval("CreditorInvoiceChargeId") %>' AlternateText="Remove" />
                                        </ItemTemplate>
                                        <FooterTemplate >
                                            <asp:ImageButton runat="Server" Width="24" Height="24" ImageUrl="~/Images/add.jpeg" ValidationGroup="Add"
                                                ID="Add" CommandName="Add" AlternateText="Add" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>  
                        </td>
                    </tr>        
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" 
                            Text="Cancel" onclick="btnCancel_Click" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
    </ContentTemplate>
    </asp:UpdatePanel>
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
                            <asp:FileUpload ID="InvoiceUpload" runat="server" />
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

