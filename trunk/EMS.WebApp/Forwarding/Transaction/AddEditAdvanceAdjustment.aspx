<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditAdvanceAdjustment.aspx.cs" Inherits="EMS.WebApp.Forwarding.Transaction.AddEditAdvanceAdjustment" %>

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
        ADD / EDIT Advance Adjustment</div>
    <center>
        <fieldset style="width:800px;">
            <legend>Add / Edit Advance Adjustment</legend>
            <table border="0" cellpadding="2" cellspacing="3" width="100%" >
            <tr>
                    <td>Adjustment Id:<span class="errormessage1">*</span></td>
                    <td><asp:Label ID="lblAdjustmentId" runat="server" Text="Label"></asp:Label>  
                    </td>
                </tr>
              <tr>
                    <td>Adjustment Date:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtAdjustmentDate" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="250"></asp:TextBox><br />
                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="txtReferenceDate"
                                TargetControlID="txtAdjustmentDate"></cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvtxtAdjustmentDate" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtAdjustmentDate" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td style="width:140px;">Job No<span class="errormessage1">*</span></td>
                    <td>
                      <asp:DropDownList ID="ddlJobNo" runat="server"  CssClass="dropdownlist" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectIndexChange">
                      <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                      </asp:DropDownList>
                      <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlJobNo" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            </td>
                </tr>
                
                <tr>
                    <td>Job Date:</td>
                    <td style="width:50%">                      
                          <asp:Label ID="lblJobDate" runat="server" Text="Label"></asp:Label>                      
                   </td>
                </tr>

                <tr>
                    <td>Debtor / Creditor:<span class="errormessage1">*</span></td>
                    <td> <asp:RadioButtonList ID="rdoDbCr" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoDbCr_SelectIndexChange" >
                    <asp:ListItem Text="Debtor" Value="D" ></asp:ListItem>
                    <asp:ListItem Text="Creditor" Value="C"></asp:ListItem>
                    </asp:RadioButtonList>
                   </td>
                </tr>
                
                <tr>
                    <td>Debtor / Creditor Name:<span class="errormessage1">*</span></td>
                    <td> <asp:DropDownList ID="ddlDrCrName" runat="server"  CssClass="dropdownlist" AutoPostBack="true"  OnSelectedIndexChanged="ddlDrCrName_SelectIndexChange">
                      <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                      </asp:DropDownList>
                      <br />
                        <asp:RequiredFieldValidator ID="rfvddlDrCrName" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlDrCrName" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            
                   </td>
                </tr>
                
                <tr>
                    <td>Adjustment No:<span class="errormessage1">*</span></td>
                    <td> <asp:DropDownList ID="ddlAdjustmentNo" runat="server"  CssClass="dropdownlist" AutoPostBack="true" 
                            Width="250" onselectedindexchanged="ddlAdjustmentNo_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                          </asp:DropDownList>
                          <br />
                        <asp:RequiredFieldValidator ID="rfvAdjustmentNo" runat="server" CssClass="errormessage"
                                        ControlToValidate="ddlAdjustmentNo" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]">
                        </asp:RequiredFieldValidator>
                   </td>
                </tr>
                <tr>
                    <td>Pending Adjustment:</td>
                    <td>  
                        <asp:Label ID="lblAdjustmentAmt" runat="server" Text="Label"></asp:Label>  
                   </td>
                </tr>               
                <tr>   
                    <td colspan="2">    
                       <asp:GridView ShowFooter="True" ShowHeaderWhenEmpty="True" ID="grvInvoice" runat="server"
                            AutoGenerateColumns="False"  OnRowCommand="grvInvoice_RowCommand" OnRowDataBound="grvInvoice_RowDataBound" Width="100%" >
                            <HeaderStyle CssClass="gridviewheader"></HeaderStyle>
                             <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                 <emptydatatemplate>No Invoice(s)/Advance(s) Found</emptydatatemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Invoice No"  ControlStyle-Width="205" >                                
                             <ItemStyle CssClass="gridviewitem" HorizontalAlign="Left"/>
                                    <ItemTemplate>
                                        <%# Eval("InvoiceOrAdvNo")%>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <FooterTemplate >
                                    <asp:DropDownList ID="ddlInvoiceOrAdvNo" runat="server"  CssClass="dropdownlist" Width="200" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceOrAdvNo_SelectIndexChange" >
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                    <asp:RequiredFieldValidator ID="rfvddlInvoiceOrAdvNo" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlInvoiceOrAdvNo" ValidationGroup="Add" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]">
                                    </asp:RequiredFieldValidator>
                            
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Date" ControlStyle-Width="205">
                                
                             <ItemStyle CssClass="gridviewitem" HorizontalAlign="Left"/>
                                <ItemTemplate>                                     
                                    <%# string.Format("{0:dd/MM/yyyy}", Eval("InvoiceOrAdvDate"))%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridviewheader" />
                                <FooterTemplate>
                                    <asp:TextBox ID="txtInvoiceOrAdvDate" Enabled="false" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="200"></asp:TextBox><br />
                                         <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="txtReferenceDate"
                                        TargetControlID="txtInvoiceOrAdvDate"></cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtAdjustmentDate" runat="server" CssClass="errormessage" 
                                    ControlToValidate="txtInvoiceOrAdvDate" Display="Dynamic" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                </FooterTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Dr Amount" ControlStyle-Width="205">
                                
                             <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right"/>
                                <ItemTemplate>                                     
                                    <%# Eval("DrAmount")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridviewheader_num" />
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDrAmount" Enabled="false" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="200" Style="text-align: right;">
                                    </asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvtxtDrAmount" runat="server" CssClass="errormessage" 
                                ControlToValidate="txtDrAmount" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add" InitialValue="0">
                                </asp:RequiredFieldValidator>
                                </FooterTemplate>

                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Cr Amount" ControlStyle-Width="205">                                
                            <ItemStyle CssClass="gridviewitem" HorizontalAlign="Right"/>
                                <ItemTemplate>                                     
                                    <%# Eval("CrAmount")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridviewheader_num" />
                                <FooterTemplate>
                                <asp:TextBox ID="txtCrAmount" Enabled="false" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="200" Style="text-align: right;">
                                </asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvtxtCrAmount" runat="server" CssClass="errormessage" 
                                ControlToValidate="txtCrAmount" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Add" InitialValue="0">
                                </asp:RequiredFieldValidator>
                                </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField >
                                
                             <ItemStyle  HorizontalAlign="Right"  />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="Server" Width="24" Height="24" ImageUrl="~/Images/remove.png" ID="Remove" ValidationGroup="Remove"
                                            CommandName="Remove" CommandArgument='<%# Eval("InvoiceJobAdjustmentPk") %>' AlternateText="Remove" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridviewheader" />
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
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" 
                            Text="Cancel" onclick="btnCancel_Click"  />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
