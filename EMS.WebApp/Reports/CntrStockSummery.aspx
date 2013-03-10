<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CntrStockSummery.aspx.cs" Inherits="EMS.WebApp.Reports.CntrStockSummery" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
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
         CONTAINER STOCK SUMMERY </div>
    <center>
        <fieldset style="width:700px; ">
            <legend> Container Stock Details </legend>
             <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                    <Triggers>
                       <%-- <asp:AsyncPostBackTrigger ControlID="ddlVoyage" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVessel" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLoc" EventName="SelectedIndexChanged" />--%>
                    </Triggers>
                    <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="3" width="100%">
            
            <tr>
              <td style="width:40%" >
              <table width="100%">
                    <tr>
                    <td style="text-align:right">Line:</td>
                    <td>
                     <asp:DropDownList ID="ddlLine" runat="server" Width="100%">
                         
                      </asp:DropDownList>
                    </td>
           
                    <td style="text-align:right">Location:<%--<span class="errormessage1">*</span>--%></td>
                    <td>
                      <asp:DropDownList ID="ddlLoc" runat="server" Width="100%" 
                            ></asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" 
                            ControlToValidate="ddlLoc" Display="Dynamic" InitialValue="0" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
              
              </table>
              </td>
              <td style="width:40%" colspan="2" align="right"><table width="100%">
                <tr>
                    <td style="text-align:right" >Stock Date:</td>
                    <td>
                        <asp:TextBox ID="txtdtStock" runat="server" CssClass="textboxuppercase" Width="150"></asp:TextBox>
                        <cc2:CalendarExtender ID="dtLand_" Format="dd/MM/yyyy" TargetControlID="txtdtStock" runat="server" />
                    </td>
             
                   
                </tr>
              </table></td>
                    
                <td colspan="2" style="text-align:right; width:10%">
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Show" 
                        ValidationGroup="Save" />
                    &nbsp;&nbsp;
                </td>
            </tr>
    
            </table>
             </ContentTemplate>
       </asp:UpdatePanel>
        </fieldset>
    </center>

    <div style="padding-top: 10px;">
        <div style="padding-left:5px;width:98%;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" >
                <%--<LocalReport ReportPath="RDLC\IGMForm2.rdlc">
                </LocalReport>--%>
            </rsweb:ReportViewer>        
        </div>    
    </div>
</center>
</asp:Content>
