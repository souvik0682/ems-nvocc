<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditParty.aspx.cs" Inherits="EMS.WebApp.Forwarding.Master.AddEditParty" %>

<%@ Register Src="~/CustomControls/AutoCompleteCountry.ascx" TagPrefix="uc1" TagName="AutoCompleteCountry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">

    <div id="headercaption">
        ADD / EDIT PARTY</div>
    <center>
        <fieldset style="width:600px;">
            <legend>Add / Edit Party</legend>
            <table border="0" cellpadding="2" cellspacing="3">
              <tr>
                    <asp:HiddenField ID="hdnKYCPath" runat="server" Value="0" />
                    <td>Party Type:<span class="errormessage1">*</span></td>
                    <td>
                      <asp:DropDownList ID="ddlPartyType" runat="server"  CssClass="dropdownlist" AutoPostBack="true"
                            onselectedindexchanged="ddlPartyType_SelectedIndexChanged" >
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <%--<asp:ListItem Text="Custom Agent" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Transporter" Value="T"></asp:ListItem>
                            <asp:ListItem Text="Overseas Agent" Value="O"></asp:ListItem>
                            <asp:ListItem Text="Debtors" Value="D"></asp:ListItem>
                            <asp:ListItem Text="Creditors" Value="C"></asp:ListItem>--%>
                      </asp:DropDownList>
                      <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" Width="300"
                                    ControlToValidate="ddlPartyType" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                            
                    </td>
                </tr>

                <tr>
                    <td style="width:100px;">Short Name<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtPartyName" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="300"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvtxtPartyName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtPartyName" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <td style="width:100px;">Full Name<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtFullName" runat="server" CssClass="textboxuppercase" MaxLength="60" Width="300"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtFullName" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>

                <tr>
                    <td>Group :<span class="errormessage1">*</span></td>
                    <td>
                      <asp:DropDownList ID="ddlGroup" runat="server"  CssClass="dropdownlist">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                      </asp:DropDownList>
                      <br />
                        <asp:RequiredFieldValidator ID="rfvGroup" runat="server" CssClass="errormessage" Width="300"
                                    ControlToValidate="ddlGroup" ValidationGroup="Save" Display="Dynamic"  InitialValue="0" ErrorMessage="[Required]">
                        </asp:RequiredFieldValidator>
                            
                    </td>
                </tr>

                <tr>
                    <td>Country:<span class="errormessage1">*</span></td>
                    <td style="width:50%">                      
                        <uc1:AutoCompleteCountry ID="AutoCompleteCountry1" runat="server"  />                       
                   </td>
                </tr>
                
                <tr>
                    <td>Address:</td>
                    <td><asp:TextBox ID="txtAddress" runat="server" CssClass="textboxuppercase" 
                            MaxLength="60" Width="300px" Height="63px" TextMode="MultiLine"></asp:TextBox>
                            
                   </td>
                </tr>

                  <tr>
                    <td>Contact Person:</td>
                    <td><asp:TextBox ID="txtContactPerson" runat="server" CssClass="textboxuppercase" MaxLength="60"
                           Width="300" ></asp:TextBox>
<%--                            <br />
                            <asp:RequiredFieldValidator ID="rfvtxtContactPerson" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtContactPerson" ValidationGroup="Save" Display="Dynamic"  ErrorMessage="[Required]"></asp:RequiredFieldValidator>--%>
                            
                   </td>
                </tr>
                
                  <tr>
                    <td>Phone:</td>
                    <td><asp:TextBox ID="txtPhone" runat="server" CssClass="textboxuppercase" MaxLength="100"
                           Width="300" TextMode="SingleLine"></asp:TextBox>
<%--                            <br />
                            <asp:RequiredFieldValidator ID="rfvtxtPhone" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtPhone" ValidationGroup="Save" Display="Dynamic"  
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>--%>
                   </td>
                </tr>
                <tr>
                    <td>Mobile No:</td>
                    <td><asp:TextBox ID="txtMob" runat="server" CssClass="textboxuppercase" MaxLength="100"
                           Width="300" TextMode="SingleLine"></asp:TextBox>
<%--                            <br />
                            <asp:RequiredFieldValidator ID="rfvtxtPhone" runat="server" CssClass="errormessage"
                                    ControlToValidate="txtPhone" ValidationGroup="Save" Display="Dynamic"  
                                    ErrorMessage="[Required]"></asp:RequiredFieldValidator>--%>
                   </td>
                </tr>
                  <tr>
                    <td>FAX:</td>
                    <td><asp:TextBox ID="txtFAX" runat="server" CssClass="textboxuppercase" 
                           Width="300" TextMode="SingleLine"></asp:TextBox><br />
                   </td>
                </tr>
                
                  <tr>
                    <td>Email ID:</td>
                    <td><asp:TextBox ID="txtEmailID" runat="server" Width="300" TextMode="SingleLine"></asp:TextBox><br />
                   </td>
                </tr>

                <tr>
                    <td>PAN:</td>
                    <td><asp:TextBox ID="txtPAN" runat="server" CssClass="textboxuppercase" 
                           Width="100" TextMode="SingleLine"></asp:TextBox><br />
<%--                     <asp:RequiredFieldValidator ID="rfvtxtPAN" runat="server" CssClass="errormessage" 
                        ControlToValidate="txtPAN" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                   </td>
                </tr>

                <tr>
                    <td>CIN:</td>
                    <td><asp:TextBox ID="txtTAN" runat="server" CssClass="textboxuppercase" 
                           Width="300" TextMode="SingleLine"></asp:TextBox><br />
<%--                        <asp:RequiredFieldValidator ID="rfvtxtTAN" runat="server" CssClass="errormessage" 
                        ControlToValidate="txtTAN" Display="Dynamic" ErrorMessage="[Required]" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                   </td>
                </tr> 
                <tr>
                    <%--<td>
                        Upload KYC
                    </td>--%>
                    <td>
                        Upload File
                    </td>

                    <td>            
                        <asp:FileUpload ID="KYCUpload" runat="server" width="200"/>
<%--                        <asp:RequiredFieldValidator ID="rfvRRUpload" runat="server" ControlToValidate="KYCUpload"
                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr id="UploadFileName" runat="server" >
                    <td>
                        Download File
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkKYCUpload" runat="server" Text="KYC Form"
                            ForeColor="Blue" Enabled="false" OnClick="lnkKYCUpload_Click"></asp:LinkButton>
                        <%--<asp:Label ID="lblUploadedFileName" runat="server" Text="" Width="200px"></asp:Label>--%>
                    </td>
                    
                </tr>
                <tr>
                    <td>Principal:</td>
                    <td><asp:DropDownList ID="ddlPrincipal" runat="server" CssClass="dropdownlist" Width="250" 
                            onselectedindexchanged="ddlPrincipal_SelectedIndexChanged" ></asp:DropDownList>
                          <br />
                        <asp:RequiredFieldValidator ID="rfvPrincipal" runat="server" CssClass="errormessage" 
                        ControlToValidate="ddlPrincipal" Display="Dynamic" ErrorMessage="[Required]" InitialValue="0" ValidationGroup="Save"></asp:RequiredFieldValidator>
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
</asp:Content>
