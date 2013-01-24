<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditRepairEstimate.aspx.cs" Inherits="EMS.WebApp.Equipment.AddEditRepairEstimate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        ADD / EDIT ESTIMATE</div>
    <center>
        <fieldset style="width:400px; ">
            <legend>Add / Edit Estimate</legend>
            <table border="0" cellpadding="2" cellspacing="3">
            <tr>
             
                    <td>Location:<span class="errormessage1">*</span></td>
                    <td>
                      <asp:DropDownList ID="ddlLoc" runat="server" Width="100%" 
                            ></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" 
                            ControlToValidate="ddlLoc" Display="Dynamic" InitialValue="0" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
              <tr>
                    <td style="width:140px;">Container No:<span class="errormessage1">*</span>
                   
                    </td>
                    <td><asp:TextBox ID="txtContainerNo" runat="server" CssClass="textboxuppercase" MaxLength="11" Width="160"> </asp:TextBox>
                    <asp:LinkButton ID="lnkStatus" runat="server" style="font-size:smaller; color:Blue" 
                            onclick="lnkStatus_Click">Check Status</asp:LinkButton><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtContainerNo" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </td>
                </tr>
              <tr>
                    <td style="width:140px;">Transaction Date:<span class="errormessage1">*</span></td>
                    <td>
                         <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox><br />
                        <cc2:CalendarExtender ID="dtTransDate" TargetControlID="txtTransactionDate" Format="dd/MM/yyyy" runat="server" />
                      
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtTransactionDate" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
              <tr>
                    <td style="width:140px;">Estimate Reference:</td>
                    <td><asp:TextBox ID="txtEstimateRef" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox>
                       </td>
                </tr>
              <tr>
                    <td>Line:</td>
                    <td>
                     <asp:DropDownList ID="ddlLine" runat="server" Width="100%">
                         
                      </asp:DropDownList>
                    </td>
                </tr>
              <tr>
                    <td>Material Estimate:</td>
                    <td>
                      <cc1:CustomTextBox ID="txtMaterialEst" runat="server" CssClass="numerictextbox" Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                    </td>
                </tr>
              <tr>
                    <td>Labour Estimate:</td>
                    <td>
                      <cc1:CustomTextBox ID="txtLabourEst" runat="server" CssClass="numerictextbox" Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                    </td>
                </tr>
              <tr>
                    <td>Material Billable:</td>
                    <td>
                      <cc1:CustomTextBox ID="txtMaterialBill" runat="server" CssClass="numerictextbox" Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                    </td>
                </tr>
              <tr>
                    <td>Labour Billable:</td>
                    <td>
                      <cc1:CustomTextBox ID="txtLabourBill" runat="server" CssClass="numerictextbox" Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                    </td>
                </tr>
             
               <tr>
                    <td> Material Approved:</td>
                    <td>
                      <cc1:CustomTextBox ID="txtMaterialApp" runat="server" CssClass="numerictextbox" Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                    </td>
                </tr>
          <tr>
                    <td>Labour Approved:</td>
                    <td>
                      <cc1:CustomTextBox ID="txtLabourApp" runat="server" CssClass="numerictextbox" Type="Decimal" MaxLength="15" Precision="12" Scale="2" Width="100"></cc1:CustomTextBox>
                    </td>
                </tr>
              <tr>
                    <td>User Approved:</td>
                    <td>
                     <asp:DropDownList ID="ddlUser" runat="server" Width="100%">
                         
                      </asp:DropDownList>
                    </td>
                </tr>
                  <tr>
                    <td></td>
                    <td>
                    OnHold:<asp:CheckBox ID="chkpOnHold" runat="server"></asp:CheckBox> &nbsp;
                    Damage: <asp:CheckBox ID="chkDamage" runat="server"></asp:CheckBox>
                    </td>
                </tr>
		       <%--<tr>
                    <td></td>
                    <td>
                   
                    </td>
                </tr>--%>
			   <tr>
                    <td style="width:140px;">Released On:</td>
                    <td>
                         <asp:TextBox ID="txtReleasedOn" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox>
                        <cc2:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtReleasedOn" runat="server" />
                       </td>
                </tr>
                  <tr>
                    <td style="width:140px;">Stock Return Date:</td>
                    <td>
                         <asp:TextBox ID="txtStockRetDate" runat="server" CssClass="textboxuppercase" Width="250"></asp:TextBox>
                        <cc2:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtStockRetDate" runat="server" />
                       </td>
                </tr>
               
		<tr>
                    <td style="width:140px;">Reason</td>
                    <td><asp:TextBox ID="txtReason" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox>
              
              </td>
              
            </tr>
         <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" onclick="btnSave_Click1" 
                              />&nbsp;&nbsp;<asp:Button 
                            ID="btnBack" runat="server" CssClass="button" Text="Back" 
                             />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>
