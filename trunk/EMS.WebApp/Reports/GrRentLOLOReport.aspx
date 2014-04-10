<%@ Page Title=":: Liner :: GROUND RENT & LOLO" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="GrRentLOLOReport.aspx.cs"  Inherits="EMS.WebApp.Reports.GrRentLOLOReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <%--<asp:UpdateProgress ID="uProgressLoc" runat="server" AssociatedUpdatePanelID="upLoc">
        <ProgressTemplate>
            <div class="progress">
                <div id="image">
                    <img src="../../Images/PleaseWait.gif" alt="" /></div>
                <div id="text">
                    Please Wait...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <center>

 <div id="headercaption">
         GROUND RENT & LOLO </div>
    <center>
        <fieldset style="width:1000px; ">
            <legend> Container Stock Details </legend>
<%--             <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">--%>

         <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="3" width="100%">
            
            <tr>
              <td style="width:60%" >
              <table width="100%">
                 <tr>
                    <td style="text-align:left">Line:</td>
                    <td>
                        <asp:DropDownList ID="ddlLine" runat="server" Width="250" 
                            onselectedindexchanged="ddlLine_SelectedIndexChanged" > 
                        </asp:DropDownList>
                    </td>
           
                    <td style="text-align:left">Location:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlLoc" runat="server" Width="150" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged"
                                        AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td style="text-align:left">Report For:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="150" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                        AutoPostBack="true">
  
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Ground Rent" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Lift On/Off" Value="2"></asp:ListItem>
                        </asp:DropDownList>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlCargoOrFreight" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                          --%>
                    </td>
                 </tr>
                 <tr>
                    
                    <td style="text-align:left">Empty Yard:<span class="errormessage1">*</span></td>
                    <td>
                      <asp:DropDownList ID="ddlEmptyYard" runat="server" Width="250" onselectedindexchanged="ddlEmptyYard_SelectedIndexChanged" 
                            ></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" 
                            ControlToValidate="ddlLoc" Display="Dynamic" InitialValue="0" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                       <td style="text-align:left" >Start Date:</td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="textboxuppercase" Width="150"></asp:TextBox>
                        <cc2:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtdtStock" runat="server" />
                    </td>
                    <td style="text-align:left" >End Date:</td>
                    <td>
                        <asp:TextBox ID="txtdtStock" runat="server" CssClass="textboxuppercase" Width="150"></asp:TextBox>
                        <cc2:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtdtStock" runat="server" />
                    </td>
                 </tr>                    
              </table>

                <td colspan="2" style="text-align:right; width:5%">
                    <asp:Button ID="btnExcel" runat="server" onclick="btnExcel_Click" Text="Generate Excel" 
                        ValidationGroup="Excel" />
                    <%--&nbsp;&nbsp;--%>
                </td>
            </tr>
    
            </table>
             </ContentTemplate>
<%--       </asp:UpdatePanel>--%>
        </fieldset>
    </center>

</center>
</asp:Content>