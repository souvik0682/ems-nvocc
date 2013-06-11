<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="COPRAR.aspx.cs"
    Inherits="EMS.WebApp.Reports.COPRAR" Title=":: Liner :: COPRAR" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <%@ Register Src="../CustomControls/AutoCompletepPort.ascx" TagName="AutoCompletepPort"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <script type="text/javascript">
      function AutoCompleteItemSelected(sender, e) {

      }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
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
    <center>

 <div id="headercaption">
 
         C O P R A R </div>
    <center>
    <asp:Label ID="lblError" runat="server" Text="Port of Discharge cannot be left blank" style="color:Red; display:none"></asp:Label>
        <fieldset style="width:1020px; ">
            <legend> COPRAR </legend>
            
             <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlVoyage" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVessel" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLoc" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="3" width="100%">
            
            <tr>
            
              <td style="width:100%" align="right">
              <table width="100%">
                <tr>
                  <td style="text-align:left; width:3%">Location:<%--<span class="errormessage1">*</span>--%></td>
                    <td style="width:23%;text-align:left;">
                      <asp:DropDownList ID="ddlLoc" runat="server" Width="100%" onselectedindexchanged="ddlLoc_SelectedIndexChanged" 
                            ></asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" 
                            ControlToValidate="ddlLoc" Display="Dynamic" InitialValue="0" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>

                    <td style="text-align:right;width:3%">Line:</td>
                    <td style="width:19%">
                     <asp:DropDownList ID="ddlLine" runat="server" Width="100%">
                         
                      </asp:DropDownList>
                    </td>

                    <td style="text-align:right;width:5%" >Vessel:</td>
                    <td style="width:25%">
                      <asp:DropDownList ID="ddlVessel" runat="server" Width="100%" AutoPostBack="True" onselectedindexchanged="ddlVessel_SelectedIndexChanged" 
                            ></asp:DropDownList>
                    </td>
             
                    <td style="text-align:center;width:5% ">Voyage:</td>
                    <td style="width:20%">
                      <asp:DropDownList ID="ddlVoyage" runat="server" Width="100%" style="min-width:100%"
                            onselectedindexchanged="ddlVoyage_SelectedIndexChanged"      >
                         
                            </asp:DropDownList>
                    </td>

                      <td style="width:5%">PoD:<span class="errormessage1">*</span></td>
                    <td >
                    <div style="width:230px">
                       <uc2:AutoCompletepPort ID="AutoCompletepPort1" runat="server" />
                       </div>
                   </td>
                      <td  style="text-align:right; width:5%; ">
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Show" 
                        ValidationGroup="Save" />
                   
                </td>
                </tr>
              </table></td>
                    
             
            </tr>
    
            </table>
             </ContentTemplate>
       </asp:UpdatePanel>
        </fieldset>
    </center>

<%--    <div style="padding-top: 10px;">
        <div style="padding-left:5px;width:98%;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" >
            </rsweb:ReportViewer>        
        </div>    
    </div>--%>
</center>
</asp:Content>
