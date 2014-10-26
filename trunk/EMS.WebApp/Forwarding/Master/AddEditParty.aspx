﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditParty.aspx.cs" Inherits="EMS.WebApp.Forwarding.Master.AddEditParty" %>

<%@ Register Src="~/CustomControls/AutoCompleteCountry.ascx" TagPrefix="uc1" TagName="AutoCompleteCountry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT PARTY</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Party</legend>
            <table border="0" cellpadding="2" cellspacing="3">
              <tr>
                    <td>Party Type:<span class="errormessage1">*</span></td>
                    <td>
                      <asp:DropDownList ID="ddlPartyType" runat="server"  CssClass="dropdownlist" >
                      <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                       <asp:ListItem Text="Custom Agent" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Transporter" Value="T"></asp:ListItem>
                        <asp:ListItem Text="Overseas Agent" Value="O"></asp:ListItem>
                         <asp:ListItem Text="Debtors" Value="D"></asp:ListItem>
                          <asp:ListItem Text="Creditors" Value="C"></asp:ListItem>
                      </asp:DropDownList>
                      <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlPartyType" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            
                    </td>
                </tr>

                <tr>
                    <td style="width:140px;">Party Name<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtPartyName" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvtxtPartyName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtPartyName" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <td>Country:<span class="errormessage1">*</span></td>
                    <td style="width:50%">                      
                        <uc1:AutoCompleteCountry ID="AutoCompleteCountry1" runat="server"  />                       
                   </td>
                </tr>
                
                  <tr>
                    <td>Line:<span class="errormessage1">*</span></td>
                    <td><asp:DropDownList ID="ddlLine" runat="server" CssClass="dropdownlist" 
                            onselectedindexchanged="ddlLine_SelectedIndexChanged" ></asp:DropDownList>
                          <br />
                          <asp:RequiredFieldValidator ID="rfvddlLine" runat="server" CssClass="errormessage"
                                    ControlToValidate="ddlLine" ValidationGroup="Save" Display="Dynamic"  InitialValue="0"
                                     ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            
                   </td>
                </tr>

                  <tr>
                    <td>Contact Person:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtContactPerson" runat="server" CssClass="textboxuppercase" MaxLength="60"
                           Width="250" ></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvtxtContactPerson" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtContactPerson" ValidationGroup="Save" Display="Dynamic"  ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            
                   </td>
                </tr>
                
                  <tr>
                    <td>Phone:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtPhone" runat="server" CssClass="textboxuppercase" MaxLength="10"
                           Width="250" TextMode="SingleLine"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvtxtPhone" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtPhone" ValidationGroup="Save" Display="Dynamic"  
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                   </td>
                </tr>

                  <tr>
                    <td>FAX:</td>
                    <td><asp:TextBox ID="txtFAX" runat="server" CssClass="textboxuppercase" 
                           Width="250" TextMode="SingleLine"></asp:TextBox><br />
                   </td>
                </tr>
                
                  <tr>
                    <td>Email ID:</td>
                    <td><asp:TextBox ID="txtEmailID" runat="server" Width="250" TextMode="SingleLine"></asp:TextBox><br />
                   </td>
                </tr>

                <tr>
                    <td>PAN:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtPAN" runat="server" CssClass="textboxuppercase" 
                           Width="250" TextMode="SingleLine"></asp:TextBox><br />
                     <asp:RequiredFieldValidator ID="rfvtxtPAN" runat="server" CssClass="errormessage" 
                        ControlToValidate="txtPAN" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator>
                   </td>
                </tr>

                <tr>
                    <td>TAN:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtTAN" runat="server" CssClass="textboxuppercase" 
                           Width="250" TextMode="SingleLine"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvtxtTAN" runat="server" CssClass="errormessage" 
                        ControlToValidate="txtTAN" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator>
                   </td>
                </tr> 
                <tr>
                    <td>Principal:<span class="errormessage1">*</span></td>
                    <td><asp:DropDownList ID="ddlPrincipal" runat="server" CssClass="dropdownlist" 
                            onselectedindexchanged="ddlPrincipal_SelectedIndexChanged" ></asp:DropDownList>
                          <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage" 
                        ControlToValidate="ddlPrincipal" Display="Dynamic" ErrorMessage="[Required]" InitialValue="0" ValidationGroup="Save"></asp:RequiredFieldValidator>
                   </td>
                </tr>                
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel"  />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>
