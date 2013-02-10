<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="EMS.WebApp.CustomControls.DatePIcker" %>
<table width="80%" cellpadding="0" cellspacing="0">
 <tr>
  <td style="width:25%">
      <asp:DropDownList ID="ddlDay" runat="server">
       
      </asp:DropDownList>
  </td>
  <td style="width:25%">
      <asp:DropDownList ID="ddlMonth" runat="server">
          <asp:ListItem Value="1">Jan</asp:ListItem>
          <asp:ListItem Value="2">Feb</asp:ListItem>
          <asp:ListItem Value="3">Mar</asp:ListItem>
          <asp:ListItem Value="4">Apr</asp:ListItem>
          <asp:ListItem Value="5">May</asp:ListItem>
          <asp:ListItem Value="6">Jun</asp:ListItem>
          <asp:ListItem Value="6">Jul</asp:ListItem>
          <asp:ListItem Value="8">Aug</asp:ListItem>
          <asp:ListItem Value="9">Sep</asp:ListItem>
          <asp:ListItem Value="10">Oct</asp:ListItem>
          <asp:ListItem Value="11">Nov</asp:ListItem>
          <asp:ListItem Value="12">Dec</asp:ListItem>
       
      </asp:DropDownList>
  </td>
  <td style="width:25%">
      <asp:DropDownList ID="ddlYear" runat="server">
       
      </asp:DropDownList>
  </td>
  <td style="width:25%">
      &nbsp;</td>
 </tr>
</table>